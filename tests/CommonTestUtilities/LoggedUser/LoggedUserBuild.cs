using FinanceManager.Domain.Entities;
using FinanceManager.Domain.Services.LoggedUser;
using Moq;

namespace Common.TestUtilities.LoggedUser;

public class LoggedUserBuild
{
    public static ILoggedUser Build(User user)
    {
        var mock = new Mock<ILoggedUser>();
        mock.Setup(loggedUser => loggedUser.Get()).ReturnsAsync(user);

        return mock.Object;
    }
}