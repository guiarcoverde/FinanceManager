using FinanceManager.Communication.Requests.Expenses;
using FinanceManager.Communication.Responses.Expenses.Register;

namespace FinanceManager.Application.UseCases.Expenses.Register;
public interface IRegisterExpenseUseCase
{
    Task<ResponseRegisterExpenseJson> Execute(RequestExpenseJson request);
}
