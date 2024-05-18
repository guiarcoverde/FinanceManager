using System.Net.Mime;
using FinanceManager.Application.UseCases.Expenses.Reports;
using FinanceManager.Communication.Requests;
using Microsoft.AspNetCore.Mvc;

namespace FinanceManager.API.Controllers;


[Route("api/[controller]")]
[ApiController]
public class ReportController : ControllerBase
{
    [HttpGet("excel")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetExcelReport([FromServices] IGenerateExpensesReportExcelUseCase useCase, [FromHeader]DateTime month)
    {
        var excelFile = await useCase.Execute(month);

        if (excelFile.Length > 0)
        {
            return File(excelFile, MediaTypeNames.Application.Octet, "expense_report.xlsx");    
        }

        return NoContent();

    }
}