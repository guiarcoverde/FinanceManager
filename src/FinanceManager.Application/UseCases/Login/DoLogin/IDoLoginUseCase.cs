using FinanceManager.Communication.Requests;
using FinanceManager.Communication.Responses;

namespace FinanceManager.Application.UseCases.Login.DoLogin;

public interface IDoLoginUseCase
{
    Task<ResponseRegisteredUserJson> Execute(RequestLoginJson request);
}