using AutoMapper;
using FinanceManager.Communication.Responses.GetExpenseById;
using FinanceManager.Domain.Repositories.Expenses;
using FinanceManager.Exceptions;
using FinanceManager.Exceptions.ExceptionsBase;

namespace FinanceManager.Application.UseCases.Expenses.GetById;

public class GetExpenseByIdUseCase : IGetExpenseByIdUseCase
{
    private readonly IExpenseRepository _expenseRepository;
    private readonly IMapper _mapper;

    public GetExpenseByIdUseCase(IExpenseRepository expenseRepository, IMapper mapper)
    {
        _expenseRepository = expenseRepository;
        _mapper = mapper;
    }

    public async Task<ResponseExpenseJson> Execute(long id)
    {
        var result = await _expenseRepository.GetById(id);

        if (result is null)
        {
            throw new NotFoundException(ResourceErrorMessage.EXPENSE_NOT_FOUND);
        };

        return _mapper.Map<ResponseExpenseJson>(result);
    }
}
