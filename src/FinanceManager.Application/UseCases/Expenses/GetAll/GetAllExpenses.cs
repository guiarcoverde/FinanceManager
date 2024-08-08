using AutoMapper;
using FinanceManager.Communication.Responses.Expenses.GetAll;
using FinanceManager.Domain.Repositories.Expenses;
using FinanceManager.Domain.Services.LoggedUser;

namespace FinanceManager.Application.UseCases.Expenses.GetAll;

public class GetAllExpenses(IExpenseReadOnlyRepository repository, IMapper mapper, ILoggedUser loggedUser) : IGetAllExpensesUseCase
{
    private readonly IExpenseReadOnlyRepository _expenseRepository = repository;
    private readonly IMapper _mapper = mapper;
    private readonly ILoggedUser _loggedUser = loggedUser;

    public async Task<ResponseExpensesJson> Execute()
    {
        var loggedUser = await _loggedUser.Get();

        var result = await _expenseRepository.GetAll(loggedUser);

        return new ResponseExpensesJson
        {
            Expenses = _mapper.Map<List<ResponseShortExpensesJson>>(result)
        };
    }
}
