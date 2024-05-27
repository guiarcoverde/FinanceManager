using System.Reflection;
using MigraDoc.DocumentObjectModel;
using PdfSharp.Fonts;

namespace FinanceManager.Application.UseCases.Expenses.Reports.Pdf.Fonts;

public class ExpensesReportFontResolver : IFontResolver
{
    public FontResolverInfo? ResolveTypeface(string familyName, bool bold, bool italic)
    {
        return new FontResolverInfo(familyName);
    }

    public byte[]? GetFont(string faceName)
    {
        var stream = ReadFontFile(faceName);
        stream ??= ReadFontFile(FontHelper.DefaultFont);

        var lenght = (int)stream!.Length;
        var data = new byte[lenght];
        stream.Read(buffer: data, offset: 0, count: lenght);

        return data;
    }

    private Stream? ReadFontFile(string faceName)
    {
        var assembly = Assembly.GetExecutingAssembly();

        return assembly.GetManifestResourceStream($"FinanceManager.Application.UseCases.Expenses.Reports.Pdf.Fonts.{faceName}.ttf");
    }

}