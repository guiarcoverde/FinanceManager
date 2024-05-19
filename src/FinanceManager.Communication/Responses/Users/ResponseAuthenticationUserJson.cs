namespace FinanceManager.Communication.Responses.Users;

public class ResponseAuthenticationUserJson
{
    public Guid Id { get; set; }
    public string Username { get; set; } = "";
    public string Email { get; set; } = "";
}