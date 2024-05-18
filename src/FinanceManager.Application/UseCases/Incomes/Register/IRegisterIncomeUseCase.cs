using FinanceManager.Communication.Requests.Incomes;
using FinanceManager.Communication.Responses.Incomes.Register;

namespace FinanceManager.Application.UseCases.Incomes.Register;

public interface IRegisterIncomeUseCase
{
    Task<ResponseRegisterIncomeJson> Execute(RequestIncomeRegistrationJson request);
}