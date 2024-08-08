using Common.TestUtilities.Entities;
using Common.TestUtilities.LoggedUser;
using Common.TestUtilities.MapperBuilder;
using Common.TestUtilities.Repositories;
using Common.TestUtilities.Requests;
using FinanceManager.Application.UseCases.Expenses.Register;
using FinanceManager.Domain.Entities;
using FinanceManager.Exceptions;
using FinanceManager.Exceptions.ExceptionsBase;
using FluentAssertions;

namespace UseCases.Test.Expenses.Register;

public class RegisterExpenseUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        var loggedUser = UserBuilder.Build();
        var request = RequestRegisterExpenseJsonBuilder.Build();

        var useCase = CreateUseCase(loggedUser);

        var result = await useCase.Execute(request);

        result.Should().NotBeNull();
        result.Title.Should().Be(request.Title);

    }
    
    [Fact]
    public async Task Error_Title_Empty()
    {
        var loggedUser = UserBuilder.Build();

        var request = RequestRegisterExpenseJsonBuilder.Build();
        request.Title = string.Empty;

        var useCase = CreateUseCase(loggedUser);

        var act = async () => await useCase.Execute(request);

        var result = await act.Should().ThrowAsync<ErrorOnValidationException>();

        result.Where(ex => ex.GetErrors().Count == 1 && ex.GetErrors().Contains(ResourceErrorMessage.TITLE_REQUIRED));
    }
    
    private RegisterExpenseUseCase CreateUseCase(User user)
    {
        var repository = ExpensesWriteOnlyRepositoryBuilder.Build();
        var mapper = MapperBuilder.Build();
        var unitOfWork = UnityOfWorkBuilder.Build();
        var loggedUser = LoggedUserBuild.Build(user);

        return new RegisterExpenseUseCase(repository, unitOfWork, mapper, loggedUser);
    }
}