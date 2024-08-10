using Common.TestUtilities.Entities;
using Common.TestUtilities.LoggedUser;
using Common.TestUtilities.Repositories;
using FinanceManager.Application.UseCases.Expenses.Reports.Pdf;
using FinanceManager.Domain.Entities;
using FluentAssertions;

namespace UseCases.Test.Expenses.Reports.Pdf;

public class GenerateExpensesReportPdfUseCaseTest
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
    
    private GenerateExpensesReportPdfUseCase CreateUseCase(User user, List<Expense> expenses)
    {
        var repository = new ExpensesReadyOnlyRepositoryBuilder().FilterByMonth(user, expenses).Build();
        var loggedUser = LoggedUserBuild.Build(user);

        return new GenerateExpensesReportPdfUseCase(repository, loggedUser);
    }
}