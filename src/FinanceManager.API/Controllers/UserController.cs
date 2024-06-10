using FinanceManager.Application.UseCases.Users.Register;
using FinanceManager.Communication.Requests;
using FinanceManager.Communication.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinanceManager.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisteredUserJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register([FromServices]IRegisterUserUseCase useCase,[FromBody]RequestRegisterUserJson request)
    {
        var response = await useCase.Execute(request);
        return Created("", response);
    }
}
