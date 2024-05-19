using FinanceManager.Application.UseCases.Incomes.Delete;
using FinanceManager.Application.UseCases.Incomes.GetAll;
using FinanceManager.Application.UseCases.Incomes.GetById;
using FinanceManager.Application.UseCases.Incomes.Register;
using FinanceManager.Application.UseCases.Incomes.Update;
using FinanceManager.Communication.Requests.Incomes;
using FinanceManager.Communication.Responses;
using FinanceManager.Communication.Responses.Incomes.GetAll;
using FinanceManager.Communication.Responses.Incomes.GetIncomeById;
using FinanceManager.Communication.Responses.Incomes.Register;
using Microsoft.AspNetCore.Mvc;

namespace FinanceManager.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class IncomesController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisterIncomeJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register([FromBody]RequestIncomeRegistrationJson request, [FromServices]IRegisterIncomeUseCase useCase)
    {
        var result = await useCase.Execute(request);

        return Created(string.Empty, result);
    }

    [HttpGet]
    [ProducesResponseType(typeof(ResponseIncomesJson), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetAll([FromServices] IGetAllIncomeUseCase useCase)
    {
        var result = await useCase.Execute();

        if (result.Incomes.Count == 0)
        {
            return NoContent();    
        }

        return Ok(result);

    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ResponseIncomeJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById([FromRoute]long id, [FromServices] IGetIncomeByIdUseCase useCase)
    {
        var result = await useCase.Execute(id);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete([FromRoute]long id, [FromServices]IDeleteIncomeUseCase useCase)
    {
        await useCase.Execute(id);
        return NoContent();
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update([FromRoute]long id, [FromServices]IUpdateIncomeUseCase useCase, [FromBody]RequestIncomeUpdateJson request)
    {
        await useCase.Execute(id, request);
        return NoContent();
    }
}