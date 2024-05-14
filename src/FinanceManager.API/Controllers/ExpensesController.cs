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
    public IActionResult Register([FromBody]RequestRegisterExpenseJson request)
    {
        RegisterExpenseUseCase useCase = new RegisterExpenseUseCase();
        ResponseRegisterExpenseJson response = useCase.Execute(request);

        return Created(string.Empty, response);
    }
}
