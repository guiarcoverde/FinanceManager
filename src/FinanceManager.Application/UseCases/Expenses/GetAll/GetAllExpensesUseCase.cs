using AutoMapper;
using FinanceManager.Communication.Responses.GetAll;
using FinanceManager.Domain.Repositories.Expenses;

namespace FinanceManager.Application.UseCases.Expenses.GetAll;

public class GetAllExpensesUseCase : IGetAllExpensesUseCase
{
    private readonly IExpenseRepository _expenseRepository;
    private readonly IMapper _mapper;
    public GetAllExpensesUseCase(IExpenseRepository repository, IMapper mapper)
    {
        _expenseRepository = repository;
        _mapper = mapper;
    }
    public async Task<ResponseExpensesJson> Execute()
    {
        var result = await _expenseRepository.GetAll();

        return new ResponseExpensesJson
        {
            Expenses = _mapper.Map<List<ResponseShortExpensesJson>>(result)
        };
    }
}
