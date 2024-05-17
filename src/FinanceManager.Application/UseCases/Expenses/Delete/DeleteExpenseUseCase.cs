using FinanceManager.Domain.Repositories;
using FinanceManager.Domain.Repositories.Expenses;
using FinanceManager.Exceptions;
using FinanceManager.Exceptions.ExceptionsBase;

namespace FinanceManager.Application.UseCases.Expenses.Delete;

public class DeleteExpenseUseCase(IExpenseWriteOnlyRepository repository, IUnityOfWork unityOfWork) : IDeleteExpenseUseCase
{
    private readonly IExpenseWriteOnlyRepository _repository = repository;
    private readonly IUnityOfWork _unityOfWork = unityOfWork;
    

    public async Task Execute(long id)
    {
        var result = await _repository.Delete(id);

        if (result is false)
        {
            throw new NotFoundException(ResourceErrorMessage.EXPENSE_NOT_FOUND);
        }

        await _unityOfWork.Commit();
    }
}