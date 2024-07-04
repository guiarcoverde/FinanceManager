using FinanceManager.Domain.Entities;

namespace FinanceManager.Domain.Services.LoggedUser;
public interface ILoggedUser
{
    Task<User> Get();
}
