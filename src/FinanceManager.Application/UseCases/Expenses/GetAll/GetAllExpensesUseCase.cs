using AutoMapper;
using FinanceManager.Communication.Responses.GetAll;
using FinanceManager.Domain.Repositories.Expenses;

namespace FinanceManager.Application.UseCases.Expenses.GetAll;

public class GetAllExpensesUseCase(IExpenseReadOnlyRepository repository, IMapper mapper) : IGetAllExpensesUseCase
{
    private readonly IExpenseReadOnlyRepository _expenseRepository = repository;
    private readonly IMapper _mapper = mapper;

    public async Task<ResponseExpensesJson> Execute()
    {
        var result = await _expenseRepository.GetAll();

        return new ResponseExpensesJson
        {
            Expenses = _mapper.Map<List<ResponseShortExpensesJson>>(result)
        };
    }
}
