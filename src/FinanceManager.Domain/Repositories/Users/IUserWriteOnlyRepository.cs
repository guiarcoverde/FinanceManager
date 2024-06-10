using FinanceManager.Domain.Entities;

namespace FinanceManager.Domain.Repositories.Users;

public interface IUserWriteOnlyRepository
{
    Task Add(User user);
}