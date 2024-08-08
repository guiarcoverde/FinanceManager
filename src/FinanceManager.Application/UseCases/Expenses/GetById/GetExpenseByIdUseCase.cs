using AutoMapper;
using FinanceManager.Communication.Responses.Expenses.GetExpenseById;
using FinanceManager.Domain.Repositories.Expenses;
using FinanceManager.Domain.Services.LoggedUser;
using FinanceManager.Exceptions;
using FinanceManager.Exceptions.ExceptionsBase;

namespace FinanceManager.Application.UseCases.Expenses.GetById;

public class GetExpenseByIdUseCase(IExpenseReadOnlyRepository expenseRepository, IMapper mapper, ILoggedUser loggedUser) : IGetExpenseByIdUseCase
{
    private readonly IExpenseReadOnlyRepository _expenseRepository = expenseRepository;
    private readonly IMapper _mapper = mapper;
    private readonly ILoggedUser _loggedUser = loggedUser;

    public async Task<ResponseExpenseJson> Execute(long id)
    {
        var loggedUser = await _loggedUser.Get();

        var result = await _expenseRepository.GetById(loggedUser, id) ?? throw new NotFoundException(ResourceErrorMessage.EXPENSE_NOT_FOUND);

        return _mapper.Map<ResponseExpenseJson>(result);
    }
}
