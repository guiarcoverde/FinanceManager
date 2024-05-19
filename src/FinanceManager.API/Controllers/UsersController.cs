using FinanceManager.Application.UseCases.Users.Register;
using FinanceManager.Communication.Requests.Users;
using FinanceManager.Communication.Responses;
using FinanceManager.Communication.Responses.Users;
using Microsoft.AspNetCore.Mvc;

namespace FinanceManager.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisterUserJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Register([FromBody]RequestRegisterUserJson request, [FromServices]IRegisterUserUseCase useCase)
    {
        var response = await useCase.Execute(request);
        
        return Created("", response);
    }
}
