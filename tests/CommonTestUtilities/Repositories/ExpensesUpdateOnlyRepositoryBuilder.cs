using FinanceManager.Domain.Entities;
using FinanceManager.Domain.Repositories.Expenses;
using Moq;

namespace Common.TestUtilities.Repositories;

public class ExpensesUpdateOnlyRepositoryBuilder
{
    private readonly Mock<IExpenseUpdateOnlyRepository> _repository;

    public ExpensesUpdateOnlyRepositoryBuilder()
    {
        _repository = new Mock<IExpenseUpdateOnlyRepository>();
    }

    public ExpensesUpdateOnlyRepositoryBuilder GetById(User user, Expense? expense)
    {
        if (expense is not null)
            _repository.Setup(repo => repo.GetById(user, expense.Id)).ReturnsAsync(expense);

        return this;
    }

    public IExpenseUpdateOnlyRepository Build() => _repository.Object;
}