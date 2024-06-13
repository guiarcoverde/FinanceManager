using FinanceManager.Application.UseCases.Expenses.Delete;
using FinanceManager.Application.UseCases.Expenses.GetAll;
using FinanceManager.Application.UseCases.Expenses.GetById;
using FinanceManager.Application.UseCases.Expenses.Register;
using FinanceManager.Application.UseCases.Expenses.Update;
using FinanceManager.Communication.Requests.Expenses;
using FinanceManager.Communication.Responses;
using FinanceManager.Communication.Responses.Expenses.GetAll;
using FinanceManager.Communication.Responses.Expenses.GetExpenseById;
using FinanceManager.Communication.Responses.Expenses.Register;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinanceManager.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ExpensesController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisterExpenseJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register(
        [FromBody]RequestExpenseJson request,
        [FromServices]IRegisterExpenseUseCase useCase)
    {
        var response = await useCase.Execute(request);

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

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete ([FromRoute] long id, [FromServices] IDeleteExpenseUseCase useCase)

    {
        await useCase.Execute(id);
        return NoContent();
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update([FromRoute] long id, [FromServices] IUpdateExpenseUseCase useCase, [FromBody]RequestExpenseJson request)
    {
        await useCase.Execute(id, request);

        return NoContent();
    }

}
