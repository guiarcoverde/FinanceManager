using Common.TestUtilities.Cryptography;
using Common.TestUtilities.Entities;
using Common.TestUtilities.LoggedUser;
using Common.TestUtilities.Repositories;
using Common.TestUtilities.Requests;
using FinanceManager.Application.UseCases.Users.ChangePassword;
using FinanceManager.Domain.Entities;
using FinanceManager.Exceptions;
using FinanceManager.Exceptions.ExceptionsBase;
using FluentAssertions;
using static System.String;

namespace UseCases.Test.Users.ChangePassword;

public class ChangePasswordUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        var user = UserBuilder.Build();
        var request = RequestChangePasswordJsonBuilder.Build();

        var useCase = CreateUseCase(user, request.Password);
        var act = async () => await useCase.Execute(request);

        await act.Should().NotThrowAsync();
    }

    [Fact]
    public async Task NewPasswordEmptyError()
    {
        var user = UserBuilder.Build();
        var request = RequestChangePasswordJsonBuilder.Build();
        request.NewPassword = string.Empty;

        var useCase = CreateUseCase(user, request.Password);
        var act = async () => await useCase.Execute(request);

        var result = 
            await act.Should().ThrowAsync<ErrorOnValidationException>();

        result.Where(e => e.GetErrors().Count == 1 &&
                          e.GetErrors().Contains(ResourceErrorMessage.PASSWORD_INVALID));

    }

    [Fact]
    public async Task CurrentPasswordDifferent()
    {
        var user = UserBuilder.Build();
        var request = RequestChangePasswordJsonBuilder.Build();
        

        var useCase = CreateUseCase(user);
        var act = async () => await useCase.Execute(request);

        var result = 
            await act.Should().ThrowAsync<ErrorOnValidationException>();

        result.Where(e => e.GetErrors().Count == 1 &&
                          e.GetErrors().Contains(ResourceErrorMessage.PASSWORD_DIFFERENT_CURRENT_PASSWORD));
    }
    
    
    private static ChangePasswordUseCase CreateUseCase(User user, string? password = null)
    {
        var unitOfWork = UnitOfWorkBuilder.Build();
        var userUpdateRepository = UpdateUserOnlyRepositoryBuilder.Build(user);
        var loggedUser = LoggedUserBuild.Build(user);
        var passwordEncripter = new PasswordEncripterBuilder().Verify(password).Build();

        return new ChangePasswordUseCase(loggedUser, userUpdateRepository, unitOfWork, passwordEncripter);
    }
}