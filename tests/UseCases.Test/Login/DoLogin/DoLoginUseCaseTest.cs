using Common.TestUtilities.Cryptography;
using Common.TestUtilities.Entities;
using Common.TestUtilities.Repositories;
using Common.TestUtilities.Requests;
using Common.TestUtilities.Token;
using FinanceManager.Application.UseCases.Login.DoLogin;
using FinanceManager.Domain.Entities;
using FinanceManager.Exceptions;
using FinanceManager.Exceptions.ExceptionsBase;
using FluentAssertions;

namespace UseCases.Test.Login.DoLogin;

public class DoLoginUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        var user = UserBuilder.Build();

        var request = RequestLoginJsonBuilder.Build();
        request.Email = user.Email;

        var useCase = CreateUseCase(user, request.Password);

        var result = await useCase.Execute(request);

        result.Should().NotBeNull();
        result.Name.Should().Be(user.Name);
        result.Token.Should().NotBeNullOrWhiteSpace();
    }

    [Fact]
    public async Task ErrorUserNotFound()
    {
        var user = UserBuilder.Build();

        var request = RequestLoginJsonBuilder.Build();

        var useCase = CreateUseCase(user, request.Password);

        var act = async () => await useCase.Execute(request);

        var result = await act.Should().ThrowAsync<InvalidLoginException>();

        result
            .Where(ex => ex.GetErrors().Count == 1 && ex.GetErrors().Contains(ResourceErrorMessage.INVALID_USERNAME_AND_PASSWORD));
    }

    [Fact]
    public async Task ErrorPasswordNotMatch()
    {
        var user = UserBuilder.Build();

        var request = RequestLoginJsonBuilder.Build();
        request.Email = user.Email;

        var useCase = CreateUseCase(user);

        var act = async () => await useCase.Execute(request);

        var result = await act.Should().ThrowAsync<InvalidLoginException>();

        result
            .Where(ex => ex.GetErrors().Count == 1 && ex.GetErrors().Contains(ResourceErrorMessage.INVALID_USERNAME_AND_PASSWORD));
    }

    private DoLoginUseCase CreateUseCase(User user, string? password = null)
    {
        var passwordEncriptor = new PasswordEncripterBuilder().Verify(password).Build();
        var tokenGenerator = JwtTokenGeneratorBuilder.Build();
        var readOnlyRepository = new UserReadOnlyRepositoryBuilder().GetUserByEmail(user).Build();
        

        return new DoLoginUseCase(readOnlyRepository, passwordEncriptor, tokenGenerator);
    }
}
