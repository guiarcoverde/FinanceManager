using Common.TestUtilities.Entities;
using Common.TestUtilities.LoggedUser;
using Common.TestUtilities.MapperBuilder;
using Common.TestUtilities.Repositories;
using FinanceManager.Application.UseCases.Expenses.GetById;
using FinanceManager.Communication.Enums;
using FinanceManager.Domain.Entities;
using FinanceManager.Exceptions;
using FinanceManager.Exceptions.ExceptionsBase;
using FluentAssertions;

namespace UseCases.Test.Expenses.GetById;

public class GetByIdUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        var loggedUser = UserBuilder.Build();
        var expense = ExpenseBuilder.Build(loggedUser);
        var useCase = CreateUseCase(loggedUser, expense);

        var result = await useCase.Execute(expense.Id);

        result.Should().NotBeNull();
        result.Id.Should().Be(expense.Id);
        result.Title.Should().Be(expense.Title);
        result.Amount.Should().Be(expense.Amount);
        result.Date.Should().Be(expense.Date);
        result.Description.Should().Be(expense.Description);
        result.PaymentType.Should().Be((PaymentType)expense.PaymentType);
    }
    
    [Fact]
    public async Task ExpenseNotFound()
    {
        var loggedUser = UserBuilder.Build();
        var useCase = CreateUseCase(loggedUser);

        var act = async () => await useCase.Execute(id: 1000);
        var result = await act.Should().ThrowAsync<NotFoundException>();

        result.Where(ex =>
            ex.GetErrors().Count == 1 && ex.GetErrors().Contains(ResourceErrorMessage.EXPENSE_NOT_FOUND));
    }
    
    private GetExpenseByIdUseCase CreateUseCase(User user, Expense? expense = null)
    {
        var repository = new ExpensesReadyOnlyRepositoryBuilder()
            .GetById(user, expense)
            .Build();
        var mapper = MapperBuilder.Build();
        var loggedUser = LoggedUserBuild.Build(user);

        return new GetExpenseByIdUseCase(repository, mapper, loggedUser);
    }
}