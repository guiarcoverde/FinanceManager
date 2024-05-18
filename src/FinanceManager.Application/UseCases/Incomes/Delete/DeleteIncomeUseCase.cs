using FinanceManager.Domain.Repositories;
using FinanceManager.Domain.Repositories.Incomes;
using FinanceManager.Exceptions;
using FinanceManager.Exceptions.ExceptionsBase;

namespace FinanceManager.Application.UseCases.Incomes.Delete;

public class DeleteIncomeUseCase(IIncomeWriteOnlyRepository repository, IUnityOfWork unityOfWork) : IDeleteIncomeUseCase
{
    private readonly IIncomeWriteOnlyRepository _repository = repository;
    private readonly IUnityOfWork _unityOfWork = unityOfWork;
    
    public async Task Execute(long id)
    {
        var result = await _repository.Delete(id);

        if (result is false)
        {
            throw new NotFoundException(ResourceErrorMessage.INCOME_NOT_FOUND);
        }

        await _unityOfWork.Commit();
    }
}