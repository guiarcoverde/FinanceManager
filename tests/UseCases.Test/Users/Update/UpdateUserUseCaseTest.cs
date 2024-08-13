using Common.TestUtilities.Entities;
using Common.TestUtilities.LoggedUser;
using Common.TestUtilities.Repositories;
using Common.TestUtilities.Requests;
using FinanceManager.Application.UseCases.Users.Update;
using FinanceManager.Domain.Entities;
using FinanceManager.Exceptions;
using FinanceManager.Exceptions.ExceptionsBase;
using FluentAssertions;

namespace UseCases.Test.Users.Update;

public class UpdateUserUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        var user = UserBuilder.Build();
        var request = RequestUpdateUserJsonBuilder.Build();
        var useCase = CreateUseCase(user);

        var act = async () => await useCase.Execute(request);

        await act.Should().NotThrowAsync();

        user.Name.Should().Be(request.Name);
        user.Email.Should().Be(request.Email);

    }

    [Fact]
    public async Task EmptyNameError()
    {
        
        var user = UserBuilder.Build();
        var request = RequestUpdateUserJsonBuilder.Build();
        var useCase = CreateUseCase(user);
        request.Name = String.Empty;

        var act = async () => await useCase.Execute(request);

        var result = await act.Should().ThrowAsync<ErrorOnValidationException>();

        result.Where(ex => ex.GetErrors().Count == 1 && ex.GetErrors().Contains(ResourceErrorMessage.NAME_EMPTY));

    }
    
    [Fact]
    public async Task EmailAlreadyExistsError()
    {
        
        var user = UserBuilder.Build();
        var request = RequestUpdateUserJsonBuilder.Build();
        var useCase = CreateUseCase(user, request.Email);

        var act = async () => await useCase.Execute(request);

        var result = await act.Should().ThrowAsync<ErrorOnValidationException>();

        result.Where(ex => ex.GetErrors().Count == 1 && ex.GetErrors().Contains(ResourceErrorMessage.EMAIL_ALREADY_REGISTERED));

    }

    private UpdateUserUseCase CreateUseCase(User user, string? email = null)
    {
        var unitOfWork = UnitOfWorkBuilder.Build();
        var readRepository = new UserReadOnlyRepositoryBuilder();
        var loggedUser = LoggedUserBuild.Build(user);
        var updateRepository = UpdateUserOnlyRepositoryBuilder.Build(user);
        
        if (string.IsNullOrWhiteSpace(email) == false)
            readRepository.ExistActiveUserWithEmail(email);

        return new UpdateUserUseCase(loggedUser, updateRepository, readRepository.Build(), unitOfWork);
    }
}