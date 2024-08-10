using Common.TestUtilities.Entities;
using Common.TestUtilities.LoggedUser;
using Common.TestUtilities.Repositories;
using FinanceManager.Application.UseCases.Expenses.Reports.Excel;
using FinanceManager.Domain.Entities;
using FluentAssertions;

namespace UseCases.Test.Expenses.Reports.Excel;

public class GenerateExpensesReportExcelUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        var loggedUser = UserBuilder.Build();
        var expenses = ExpenseBuilder.Collection(loggedUser);

        var useCase = CreateUseCase(loggedUser, expenses);

        var result = await useCase.Execute(DateOnly.FromDateTime(DateTime.Today));
        result.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async Task EmptySuccess()
    {
        var loggedUser = UserBuilder.Build();
        List<Expense> expenses = [];

        var useCase = CreateUseCase(loggedUser, expenses);

        var result = await useCase.Execute(DateOnly.FromDateTime(DateTime.Today));
        result.Should().BeEmpty();
    }
    
    private GenerateExpensesReportExcelUseCase CreateUseCase(User user, List<Expense> expenses)
    {
        var repository = new ExpensesReadyOnlyRepositoryBuilder().FilterByMonth(user, expenses).Build();
        var loggedUser = LoggedUserBuild.Build(user);

        return new GenerateExpensesReportExcelUseCase(repository, loggedUser);
    }
}
