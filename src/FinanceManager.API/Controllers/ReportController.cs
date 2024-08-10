using System.Net.Mime;
using FinanceManager.Application.UseCases.Expenses.Reports.Excel;
using FinanceManager.Application.UseCases.Expenses.Reports.Pdf;
using FinanceManager.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinanceManager.API.Controllers;


[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = Roles.Admin)]
public class ReportController : ControllerBase
{
    [HttpGet("excel")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetExcelReport([FromServices] IGenerateExpensesReportExcelUseCase useCase, [FromQuery]DateOnly month)
    {
        var excelFile = await useCase.Execute(month);

        if (excelFile.Length > 0)
        {
            return File(excelFile, MediaTypeNames.Application.Octet, "expense_report.xlsx");    
        }

        return NoContent();

    }
    [HttpGet("pdf")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetExcelReport([FromServices]IGenerateExpensesReportPdfUseCase useCase, [FromQuery]DateOnly month)
    {
        var pdfFile = await useCase.Execute(month);

        if (pdfFile.Length > 0)
        {
            return File(pdfFile, MediaTypeNames.Application.Pdf, "expense_report.pdf");    
        }

        return NoContent();

    }
}