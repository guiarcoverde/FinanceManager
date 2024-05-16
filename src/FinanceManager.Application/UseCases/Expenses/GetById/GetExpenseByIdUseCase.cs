using AutoMapper;
using FinanceManager.Communication.Responses.GetExpenseById;
using FinanceManager.Domain.Repositories.Expenses;
using FinanceManager.Exceptions;
using FinanceManager.Exceptions.ExceptionsBase;

namespace FinanceManager.Application.UseCases.Expenses.GetById;

public class GetExpenseByIdUseCase(IExpenseRepository expenseRepository, IMapper mapper) : IGetExpenseByIdUseCase
{
    private readonly IExpenseRepository _expenseRepository = expenseRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<ResponseExpenseJson> Execute(long id)
    {
        var result = await _expenseRepository.GetById(id) ?? throw new NotFoundException(ResourceErrorMessage.EXPENSE_NOT_FOUND);

        return _mapper.Map<ResponseExpenseJson>(result);
    }
}
