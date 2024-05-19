using AutoMapper;
using FinanceManager.Communication.Requests.Expenses;
using FinanceManager.Communication.Requests.Incomes;
using FinanceManager.Communication.Requests.Users;
using FinanceManager.Communication.Responses.Expenses.GetAll;
using FinanceManager.Communication.Responses.Expenses.GetExpenseById;
using FinanceManager.Communication.Responses.Expenses.Register;
using FinanceManager.Communication.Responses.Incomes.GetAll;
using FinanceManager.Communication.Responses.Incomes.Register;
using FinanceManager.Communication.Responses.Users;
using FinanceManager.Domain.Entities;

namespace FinanceManager.Application.AutoMapper;

public class AutoMapping : Profile
{
    public AutoMapping()
    {
        RequestToEntity();
        EntityToResponse();
    }

    private void RequestToEntity()
    {
        CreateMap<RequestExpenseJson, Expense>();
        CreateMap<RequestIncomeRegistrationJson, Income>();
        CreateMap<RequestRegisterUserJson, User>();
    }

    private void EntityToResponse()
    {
        CreateMap<Expense, ResponseRegisterExpenseJson>();
        CreateMap<Expense, ResponseShortExpensesJson>();
        CreateMap<Expense, ResponseExpenseJson>();
        CreateMap<Income, ResponseRegisterIncomeJson>();
        CreateMap<Income, ResponseShortIncomesJson>();
        CreateMap<Income, ResponseIncomesJson>();
        CreateMap<User, ResponseRegisterUserJson>();
    }
}
