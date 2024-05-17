using AutoMapper;
using FinanceManager.Communication.Requests;
using FinanceManager.Communication.Responses.GetAll;
using FinanceManager.Communication.Responses.GetExpenseById;
using FinanceManager.Communication.Responses.Register;
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
    }

    private void EntityToResponse()
    {
        CreateMap<Expense, ResponseRegisterExpenseJson>();
        CreateMap<Expense, ResponseShortExpensesJson>();
        CreateMap<Expense, ResponseExpenseJson>();

    }
}
