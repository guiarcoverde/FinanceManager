using FinanceManager.Domain.Entities;
using FinanceManager.Domain.Repositories.Users;
using Moq;

namespace Common.TestUtilities.Repositories;

public class UpdateUserOnlyRepositoryBuilder
{
    public static IUserUpdateOnlyRepository Build(User user)
    {
        var mock = new Mock<IUserUpdateOnlyRepository>();
        mock.Setup(repo => repo.GetById(user.Id)).ReturnsAsync(user);
        return mock.Object;
    }  
}