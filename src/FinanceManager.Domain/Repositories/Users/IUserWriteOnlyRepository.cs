using FinanceManager.Domain.Entities;

namespace FinanceManager.Domain.Repositories.Users;

public interface IUserWriteOnlyRepository
{
    public Task Add(User user);
}