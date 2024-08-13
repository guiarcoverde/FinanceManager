using FinanceManager.Domain.Repositories;
using FinanceManager.Domain.Repositories.Users;
using FinanceManager.Domain.Services.LoggedUser;

namespace FinanceManager.Application.UseCases.Users.Delete;

public class DeleteUserUseCase(ILoggedUser loggedUser, IUserWriteOnlyRepository repository, IUnitOfWork unitOfWork) : IDeleteUserUseCase
{
    private readonly ILoggedUser _loggedUser = loggedUser;
    private readonly IUserWriteOnlyRepository _repository = repository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task Execute()
    {
        var user = await _loggedUser.Get();
        await _repository.Delete(user);
        await _unitOfWork.Commit();
    }
}