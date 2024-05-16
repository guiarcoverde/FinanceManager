using FinanceManager.Communication.Requests;
using FinanceManager.Application.UseCases.Expenses.Register;
using Microsoft.AspNetCore.Mvc;
using FinanceManager.Communication.Responses;
using FinanceManager.Exceptions.ExceptionsBase;

namespace FinanceManager.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ExpensesController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisterExpenseJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register(
        [FromBody]RequestRegisterExpenseJson request,
        [FromServices]IRegisterExpenseUseCase useCase)
    {
        ResponseRegisterExpenseJson response = await useCase.Execute(request);

        return Created(string.Empty, response);
    }
}
