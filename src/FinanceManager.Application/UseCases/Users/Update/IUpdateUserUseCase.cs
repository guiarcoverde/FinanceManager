using FinanceManager.Communication.Requests.Users;

namespace FinanceManager.Application.UseCases.Users.Update;

public interface IUpdateUserUseCase
{
    Task Execute(RequestUpdateUserJson request);
}