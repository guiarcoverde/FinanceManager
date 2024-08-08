using FinanceManager.Domain.Repositories.Expenses;
using Moq;

namespace Common.TestUtilities.Repositories;

public class ExpensesWriteOnlyRepositoryBuilder
{
    public static IExpenseWriteOnlyRepository Build()
    {
        var mock = new Mock<IExpenseWriteOnlyRepository>();

        return mock.Object;
    }
}