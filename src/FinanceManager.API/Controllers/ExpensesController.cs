using FinanceManager.Application.UseCases.Expenses.GetAll;
using FinanceManager.Application.UseCases.Expenses.GetById;
using FinanceManager.Application.UseCases.Expenses.Register;
using FinanceManager.Communication.Requests;
using FinanceManager.Communication.Responses.GetAll;
using FinanceManager.Communication.Responses.GetExpenseById;
using FinanceManager.Communication.Responses.Register;
using Microsoft.AspNetCore.Mvc;

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

    [HttpGet]
    [ProducesResponseType(typeof(ResponseExpensesJson), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetAllExpenses([FromServices] IGetAllExpensesUseCase useCase)
    {
        var response = await useCase.Execute();

        if (response.Expenses.Count != 0)
        {
            return Ok(response);
        }

        return NoContent();
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ResponseExpenseJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(
        [FromServices] IGetExpenseByIdUseCase useCase,
        [FromRoute] long id)
    {
        var response = await useCase.Execute(id);

        return Ok(response);
    }

}
