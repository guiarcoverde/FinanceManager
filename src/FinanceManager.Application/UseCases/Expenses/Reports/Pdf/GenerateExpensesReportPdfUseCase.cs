using FinanceManager.Application.UseCases.Expenses.Reports.Pdf.Fonts;
using FinanceManager.Domain.Extensions;
using FinanceManager.Domain.Reports;
using FinanceManager.Domain.Repositories.Expenses;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using PdfSharp.Fonts;

namespace FinanceManager.Application.UseCases.Expenses.Reports.Pdf;

public class GenerateExpensesReportPdfUseCase : IGenerateExpensesReportPdfUseCase
{
    private readonly IExpenseReadOnlyRepository _repository;
    private const string CurrencySymbol = "R$";
    private const int RowHeightExpenseTable = 25;

    public GenerateExpensesReportPdfUseCase(IExpenseReadOnlyRepository repository)
    {
        _repository = repository;

        GlobalFontSettings.FontResolver = new ExpensesReportFontResolver();
    }
    
    
    
    public async Task<byte[]> Execute(DateOnly month)
    {
        var expenses = await _repository.FilterByMonth(month);
        if (expenses.Count == 0)
        {
            return [];
        }
        
        var totalExpense = expenses.Sum(expense => expense.Amount);
        var document = CreateDocument(month);
        var page = CreatePage(document);
        
        CreateHeaderWithProfilePhotoAndName(page);
        
        CreateTotalSpentSection(page, month, totalExpense);

        foreach (var expense in expenses)
        {
            var table = CreateExpenseTable(page);

            var row = table.AddRow();
            row.Height = RowHeightExpenseTable;

            AddExpenseTitle(row.Cells[0], expense.Title);
            
            AddHeaderForAmount(row.Cells[3]);

            row = table.AddRow();
            row.Height = RowHeightExpenseTable;

            row.Cells[0].AddParagraph(expense.Date.ToString("D"));
            SetStyleBaseForExpenseInformation(row.Cells[0]);
            row.Cells[0].Format.LeftIndent = 20;

            row.Cells[1].AddParagraph(expense.Date.ToString("t"));
            SetStyleBaseForExpenseInformation(row.Cells[1]);

            row.Cells[2].AddParagraph(expense.PaymentType.PaymentTypeToString());
            SetStyleBaseForExpenseInformation(row.Cells[2]);
            
            AddExpenseValue(row.Cells[3], expense.Amount);

            if (!string.IsNullOrWhiteSpace(expense.Description))
            {
                var descriptionRow = table.AddRow();
                descriptionRow.Height = RowHeightExpenseTable;

                descriptionRow.Cells[0].AddParagraph(expense.Description);
                descriptionRow.Cells[0].Format.Font = new Font {Name = FontHelper.WorkSansRegular, Size = 10, Color = ColorsHelp.Black};
                descriptionRow.Cells[0].Shading.Color = ColorsHelp.GreenLight;
                descriptionRow.Cells[0].VerticalAlignment = VerticalAlignment.Center;
                descriptionRow.Cells[0].MergeRight = 2;
                descriptionRow.Cells[0].Format.LeftIndent = 20;

                row.Cells[3].MergeDown = 1;
            }
            
            AddWhiteSpace(table);
            
        }
        
        
        return RenderDocument(document);
    }

    private Document CreateDocument(DateOnly month)
    {
        var document = new Document();
        document.Info.Title = $"{ResourceReportGenerationMessages.EXPENSES_FOR} {month:Y}";
        document.Info.Author = "Guilherme Arcoverde";

        var style = document.Styles["Normal"];
        style!.Font.Name = FontHelper.RalewayRegular;

        return document;
    }

    private Section CreatePage(Document document)
    {
        var section = document.AddSection();
        section.PageSetup = document.DefaultPageSetup.Clone();
        section.PageSetup.PageFormat = PageFormat.A4;
        section.PageSetup.LeftMargin = 40;
        section.PageSetup.RightMargin = 40;
        section.PageSetup.TopMargin = 80;
        section.PageSetup.BottomMargin = 80;
        
        return section;
    }

    private byte[] RenderDocument(Document document)
    {
        var renderer = new PdfDocumentRenderer
        {
            Document = document
        };
        
        renderer.RenderDocument();

        using var file = new MemoryStream();
        renderer.PdfDocument.Save(file);
        return file.ToArray();
    }

    private void CreateHeaderWithProfilePhotoAndName(Section page)
    {
        var table = page.AddTable();
        table.AddColumn();
        table.AddColumn("300");

        var row = table.AddRow();
        
        row.Cells[1].AddParagraph("Hey, Guilherme Arcoverde");
        row.Cells[1].Format.Font = new Font { Name = FontHelper.RalewayBlack, Size = 16 };
        row.Cells[1].VerticalAlignment = MigraDoc.DocumentObjectModel.Tables.VerticalAlignment.Center;
    }

    private void CreateTotalSpentSection(Section page, DateOnly month, decimal totalExpense)
    {
        var paragraph = page.AddParagraph();
        paragraph.Format.SpaceBefore = "40";
        paragraph.Format.SpaceAfter = "40";
        
        var title = string.Format(ResourceReportGenerationMessages.TOTAL_SPENT_IN, month.ToString("Y"));

        paragraph.AddFormattedText(title, new Font { Name = FontHelper.RalewayRegular, Size = 15});
        
        paragraph.AddLineBreak();
        
        paragraph.AddFormattedText($"{CurrencySymbol} {totalExpense}", new Font { Name = FontHelper.WorkSansBlack, Size = 50});
    }

    private Table CreateExpenseTable(Section page)
    {
        var table = page.AddTable();

        table.AddColumn("195").Format.Alignment = ParagraphAlignment.Left;
        table.AddColumn("80").Format.Alignment = ParagraphAlignment.Center;
        table.AddColumn("120").Format.Alignment = ParagraphAlignment.Center;
        table.AddColumn("120").Format.Alignment = ParagraphAlignment.Right;
        
        return table;
    }

    private void AddExpenseTitle(Cell cell,string expenseTitle)
    {
        cell.AddParagraph(expenseTitle);
        cell.Format.Font = new Font {Name = FontHelper.RalewayBlack, Size = 14, Color = ColorsHelp.Black};
        cell.Shading.Color = ColorsHelp.RedLight;
        cell.VerticalAlignment = VerticalAlignment.Center;
        cell.MergeRight = 2;
        cell.Format.LeftIndent = 20;
    }

    private void AddHeaderForAmount(Cell cell)
    {
        cell.AddParagraph(ResourceReportGenerationMessages.AMOUNT);
        cell.Format.Font = new Font {Name = FontHelper.RalewayBlack, Size = 14, Color = ColorsHelp.White};
        cell.Shading.Color = ColorsHelp.RedDark;
        cell.VerticalAlignment = VerticalAlignment.Center;
    }

    private void SetStyleBaseForExpenseInformation(Cell cell)
    {
        cell.Format.Font = new Font { Name = FontHelper.WorkSansRegular, Size = 12, Color = ColorsHelp.Black };
        cell.Shading.Color = ColorsHelp.GreenDark;
        cell.VerticalAlignment = VerticalAlignment.Center;
    }

    private void AddExpenseValue(Cell cell, decimal expenseValue)
    {
        cell.AddParagraph($"-{CurrencySymbol} {expenseValue}");
        cell.Format.Font = new Font { Name = FontHelper.WorkSansRegular, Size = 14, Color = ColorsHelp.Black };
        cell.Shading.Color = ColorsHelp.White;
        cell.VerticalAlignment = VerticalAlignment.Center;
    }

    private void AddWhiteSpace(Table table)
    {
        var row = table.AddRow();
        row.Height = RowHeightExpenseTable;
        row.Borders.Visible = false;
    }
}