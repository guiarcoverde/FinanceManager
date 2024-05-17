using FinanceManager.Communication.Requests;
using FinanceManager.Communication.Responses.Register;

namespace FinanceManager.Application.UseCases.Expenses.Register;
public interface IRegisterExpenseUseCase
{
    Task<ResponseRegisterExpenseJson> Execute(RequestExpenseJson request);
}
