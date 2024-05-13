using Microsoft.AspNetCore.Mvc;

namespace FinanceManager.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ExpensesController : ControllerBase
{
    [HttpPost]
    public IActionResult Register()
    {
        return Created();
    }
}
