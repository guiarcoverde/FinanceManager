using FinanceManager.Domain.Entities;

namespace FinanceManager.Domain.Repositories.Users;

public interface IUserUpdateOnlyRepository
{
    Task<User> GetById(long id);
    void Update(User user);
}