using Common.TestUtilities.Entities;
using Common.TestUtilities.LoggedUser;
using Common.TestUtilities.MapperBuilder;
using Common.TestUtilities.Repositories;
using FinanceManager.Application.UseCases.Expenses.GetAll;
using FinanceManager.Domain.Entities;
using FluentAssertions;

namespace UseCases.Test.Expenses.GetAll;

public class GetAllExpensesUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        var loggedUser = UserBuilder.Build();
        var expenses = ExpenseBuilder.Collection(loggedUser);

        var useCase = CreateUseCase(loggedUser, expenses);

        var result = await useCase.Execute();

        result.Should().NotBeNull();

        result.Expenses.Should().NotBeNullOrEmpty().And.AllSatisfy(expense =>
        {
            expense.Id.Should().BeGreaterThan(0);
            expense.Title.Should().NotBeNullOrEmpty();
            expense.Amount.Should().BeGreaterThan(0);
        });
    }

    private GetAllExpenses CreateUseCase(User user, List<Expense> expenses)
    {
        var repository = new ExpensesReadyOnlyRepositoryBuilder()
            .GetAll(user, expenses)
            .Build();

        var mapper = MapperBuilder.Build();
        var loggedUser = LoggedUserBuild.Build(user);

        return new GetAllExpenses(repository, mapper, loggedUser);
    }
}