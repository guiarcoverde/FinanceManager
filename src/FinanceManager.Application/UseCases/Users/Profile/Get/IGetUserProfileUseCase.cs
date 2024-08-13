using FinanceManager.Communication.Responses.Users;

namespace FinanceManager.Application.UseCases.Users.Profile.Get;

public interface IGetUserProfileUseCase
{
    Task<ResponseUserProfileJson> Execute();
}