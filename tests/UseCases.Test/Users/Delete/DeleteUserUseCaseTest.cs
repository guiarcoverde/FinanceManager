using Common.TestUtilities.Entities;
using Common.TestUtilities.LoggedUser;
using Common.TestUtilities.Repositories;
using FinanceManager.Application.UseCases.Users.Delete;
using FinanceManager.Domain.Entities;
using FluentAssertions;

namespace UseCases.Test.Users.Delete;

public class DeleteUserUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        var user = UserBuilder.Build();
        var useCase = CreateUseCase(user);

        var act = async () => await useCase.Execute();

        await act.Should().NotThrowAsync();
    }


    private DeleteUserUseCase CreateUseCase(User user)
    {
        var repository = UserWriteOnlyRepositoryBuilder.Build();
        var loggedUser = LoggedUserBuild.Build(user);
        var unitOfWork = UnitOfWorkBuilder.Build();

        return new DeleteUserUseCase(loggedUser, repository, unitOfWork);
    }
}