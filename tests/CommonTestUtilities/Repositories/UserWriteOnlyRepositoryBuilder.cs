using FinanceManager.Domain.Repositories.Users;
using Moq;

namespace Common.TestUtilities.Repositories;

public class UserWriteOnlyRepositoryBuilder
{
    public static IUserWriteOnlyRepository Build()
    {
        var mock = new Mock<IUserWriteOnlyRepository>();
        
        return mock.Object;
    }
}
