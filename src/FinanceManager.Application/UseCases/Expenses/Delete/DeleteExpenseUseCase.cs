using FinanceManager.Domain.Repositories;
using FinanceManager.Domain.Repositories.Expenses;
using FinanceManager.Domain.Services.LoggedUser;
using FinanceManager.Exceptions;
using FinanceManager.Exceptions.ExceptionsBase;
using Microsoft.AspNetCore.Http;

namespace FinanceManager.Application.UseCases.Expenses.Delete;

public class DeleteExpenseUseCase(IExpenseWriteOnlyRepository repository, IUnitOfWork unitOfWork, ILoggedUser loggedUser, IExpenseReadOnlyRepository expenseReadOnlyRepository) : IDeleteExpenseUseCase
{
    private readonly IExpenseReadOnlyRepository _expenseReadOnlyRepository = expenseReadOnlyRepository;
    private readonly IExpenseWriteOnlyRepository _repository = repository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ILoggedUser _loggedUser = loggedUser;
    

    public async Task Execute(long id)
    {
        var loggedUser = await _loggedUser.Get();

        var expense = await _expenseReadOnlyRepository.GetById(loggedUser, id) ?? throw new NotFoundException(ResourceErrorMessage.EXPENSE_NOT_FOUND);

        await _repository.Delete(id);

        await _unitOfWork.Commit();
    }
}