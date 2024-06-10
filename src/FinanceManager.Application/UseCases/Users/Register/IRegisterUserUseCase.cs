using FinanceManager.Communication.Requests;
using FinanceManager.Communication.Responses;

namespace FinanceManager.Application.UseCases.Users.Register;

public interface IRegisterUserUseCase
{
    Task<ResponseRegisteredUserJson> Execute(RequestRegisterUserJson request);
}