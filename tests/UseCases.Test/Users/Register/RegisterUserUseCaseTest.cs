using Common.TestUtilities.MapperBuilder;
using Common.TestUtilities.Repositories;
using Common.TestUtilities.Requests;
using FinanceManager.Application.UseCases.Users.Register;
using FluentAssertions;

namespace UseCases.Test.Users.Register;

public class RegisterUserUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        var request = RequestRegisterUserJsonBuilder.Build();
        var useCase = CreateUseCase();

        var result = await useCase.Execute(request);

        result.Should().NotBeNull();
        result.Name.Should().Be(request.Name);
        result.Token.Should().NotBeNullOrWhiteSpace();
    }

    private RegisterUserUseCase CreateUseCase()
    {
        var mapper = MapperBuilder.Build();
        var unitOfWork = UnityOfWorkBuilder.Build();
        var writeRepository = UserWriteOnlyRepositoryBuilder.Build();

        return new RegisterUserUseCase(mapper, null, null, writeRepository, unitOfWork, null);
    }
}
