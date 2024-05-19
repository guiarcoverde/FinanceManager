namespace FinanceManager.Communication.Requests.Users;

public class RequestRegisterUserJson
{
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Email { get; set; } = null!;

}
