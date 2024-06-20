using FinanceManager.Domain.Repositories;
using Moq;

namespace Common.TestUtilities.Repositories;

public class UnityOfWorkBuilder
{
    public static IUnityOfWork Build()
    {
        var mock = new Mock<IUnityOfWork>();

        return mock.Object;
    }
}
