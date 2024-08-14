using AutoMapper;
using FinanceManager.Communication.Enums;
using FinanceManager.Communication.Requests;
using FinanceManager.Communication.Requests.Expenses;
using FinanceManager.Communication.Requests.Incomes;
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
        CreateMap<RequestExpenseJson, Expense>()
            .ForMember(dest => dest.Tags, config => config.MapFrom(source => source.Tags.Distinct()));

        CreateMap<RequestIncomeRegistrationJson, Income>();

        CreateMap<RequestRegisterUserJson, User>()
            .ForMember(dest => dest.Password, config => config.Ignore());

        CreateMap<Tags, Tag>()
            .ForMember(dest => dest.Value, config => config.MapFrom(source => source));
    }

    private void EntityToResponse()
    {
        CreateMap<Expense, ResponseRegisterExpenseJson>();
        CreateMap<Expense, ResponseShortExpensesJson>();
        CreateMap<Expense, ResponseExpenseJson>()
            .ForMember(dest => dest.Tags, config => config.MapFrom(source => source.Tags.Select(tag => tag.Value)));

        CreateMap<Income, ResponseRegisterIncomeJson>();
        CreateMap<Income, ResponseShortIncomesJson>();
        CreateMap<Income, ResponseIncomesJson>();

        CreateMap<User, ResponseUserProfileJson>();
    }
}
