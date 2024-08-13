using Common.TestUtilities.Entities;
using Common.TestUtilities.LoggedUser;
using Common.TestUtilities.MapperBuilder;
using FinanceManager.Application.UseCases.Users.Profile.Get;
using FinanceManager.Domain.Entities;
using FluentAssertions;

namespace UseCases.Test.Users.Profile;

public class GetUserProfileUseCaseTest
{
    
    [Fact]
    public async Task Success()
    {
        var user = UserBuilder.Build();
        var useCase = CreateUseCase(user);

        var result = await useCase.Execute();

        result.Should().NotBeNull();
        result.Name.Should().Be(user.Name);
        result.Email.Should().Be(user.Email);
    }

    private GetUserProfileUseCase CreateUseCase(User user)
    {
        var mapper = MapperBuilder.Build();
        var loggedUser = LoggedUserBuild.Build(user);

        return new GetUserProfileUseCase(loggedUser, mapper);
    }
}