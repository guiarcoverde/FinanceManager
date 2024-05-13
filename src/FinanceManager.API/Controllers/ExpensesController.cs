using FinanceManager.Communication.Requests;
using FinanceManager.Application.UseCases.Expenses.Register;
using Microsoft.AspNetCore.Mvc;
using FinanceManager.Communication.Responses;

namespace FinanceManager.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ExpensesController : ControllerBase
{
    [HttpPost]
    public IActionResult Register([FromBody]RequestRegisterExpenseJson request)
    {
        try
        {
            RegisterExpenseUseCase useCase = new RegisterExpenseUseCase();
            ResponseRegisterExpenseJson response = useCase.Execute(request);

            return Created(string.Empty, response);
        } 
        catch (ArgumentException ex)
        {
            ResponseErrorJson errorResponse = new(ex.Message);

           return BadRequest(errorResponse);
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Unknown Error");
        }
    }
}
