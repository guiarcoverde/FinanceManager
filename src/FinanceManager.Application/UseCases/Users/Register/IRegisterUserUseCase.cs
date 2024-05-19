using FinanceManager.Communication.Requests.Users;
using FinanceManager.Communication.Responses.Users;

namespace FinanceManager.Application.UseCases.Users.Register;

public interface IRegisterUserUseCase
{
    public Task<ResponseRegisterUserJson> Execute(RequestRegisterUserJson request);
}