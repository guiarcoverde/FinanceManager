using FinanceManager.Domain.Entities;
using FinanceManager.Domain.Repositories.Expenses;
using Moq;

namespace Common.TestUtilities.Repositories;

public class ExpensesReadyOnlyRepositoryBuilder
{
    private readonly Mock<IExpenseReadOnlyRepository> _repository = new();

    public ExpensesReadyOnlyRepositoryBuilder GetAll(User user, List<Expense> expenses)
    {
        _repository.Setup(repository => repository.GetAll(user)).ReturnsAsync(expenses);

        return this;
    }
    
    public ExpensesReadyOnlyRepositoryBuilder GetById(User user, Expense? expense)
    {
        if(expense is not null)
            _repository.Setup(repository => repository.GetById(user, expense.Id)).ReturnsAsync(expense);

        return this;
    }

    public ExpensesReadyOnlyRepositoryBuilder FilterByMonth(User user, List<Expense> expenses)
    {
        _repository.Setup(repo => repo.FilterByMonth(user, It.IsAny<DateOnly>())).ReturnsAsync(expenses);
        
        return this;
    }
    
    public IExpenseReadOnlyRepository Build() => _repository.Object;


}