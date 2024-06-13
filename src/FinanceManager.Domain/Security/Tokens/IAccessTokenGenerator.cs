using FinanceManager.Domain.Entities;

namespace FinanceManager.Domain.Security.Tokens;

public interface IAccessTokenGenerator
{
    string Generate(User user);
}