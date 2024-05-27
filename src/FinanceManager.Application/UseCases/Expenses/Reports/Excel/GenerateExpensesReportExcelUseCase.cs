using ClosedXML.Excel;
using FinanceManager.Domain;
using FinanceManager.Domain.Enums;
using FinanceManager.Domain.Extensions;
using FinanceManager.Domain.Reports;
using FinanceManager.Domain.Repositories.Expenses;


namespace FinanceManager.Application.UseCases.Expenses.Reports.Excel;

public class GenerateExpensesReportExcelUseCase(IExpenseReadOnlyRepository repository) : IGenerateExpensesReportExcelUseCase
{
    private readonly IExpenseReadOnlyRepository _repository = repository;
    private const string CurrencySymbol = "R$";
    public async Task<byte[]> Execute(DateOnly month)
    {
        var expenses = await _repository.FilterByMonth(month);
        using var workBook = new XLWorkbook();
        
       var workSheet = workBook.Worksheets.Add(month.ToString("Y"));
       
       InsertHeader(workSheet);

       var line = 2;
       
       foreach (var expense in expenses)
       {
           workSheet.Cell($"A{line}").Value = expense.Title;
           workSheet.Cell($"B{line}").Value = expense.Date;
           workSheet.Cell($"C{line}").Value = expense.PaymentType.PaymentTypeToString();
           
           workSheet.Cell($"D{line}").Value = expense.Amount;
           workSheet.Cell($"D{line}").Style.NumberFormat.Format = $"-{CurrencySymbol} #,##0.00";
           
           
           workSheet.Cell($"E{line}").Value = expense.Description;
           
           line++;
       }

       workSheet.Columns().AdjustToContents();

       var excelFile = new MemoryStream();
       
       workBook.SaveAs(excelFile);

       return excelFile.ToArray();
    }
    
    private void InsertHeader(IXLWorksheet worksheet)
    {
        worksheet.Cell("A1").Value = ResourceReportGenerationMessages.TITLE;
        worksheet.Cell("B1").Value = ResourceReportGenerationMessages.DATE;
        worksheet.Cell("C1").Value = ResourceReportGenerationMessages.PAYMENT_TYPE;
        worksheet.Cell("D1").Value = ResourceReportGenerationMessages.AMOUNT;
        worksheet.Cell("E1").Value = ResourceReportGenerationMessages.DESCRIPTION;

        worksheet.Cells("A1:E1").Style.Font.Bold = true;
        worksheet.Cells("A1:E1").Style.Fill.BackgroundColor = XLColor.FromHtml("#D34625");

        worksheet.Cells("A1:C1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        worksheet.Cell("E1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        
        worksheet.Cell("D1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Right);
    }
    
    
}