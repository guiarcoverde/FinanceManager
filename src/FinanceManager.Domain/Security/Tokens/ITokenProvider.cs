namespace FinanceManager.Domain.Security.Tokens;

public interface ITokenProvider
{
    string TokenOnRequest();
}
