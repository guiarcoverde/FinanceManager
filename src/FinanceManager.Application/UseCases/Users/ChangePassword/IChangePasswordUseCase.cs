using FinanceManager.Communication.Requests.Users;

namespace FinanceManager.Application.UseCases.Users.ChangePassword;

public interface IChangePasswordUseCase
{
    Task Execute(RequestChangePasswordJson request);
}