using FinanceManager.Communication.Requests;
using FinanceManager.Communication.Responses;

namespace FinanceManager.Application.UseCases.Expenses.Register;
public interface IRegisterExpenseUseCase
{
    Task<ResponseRegisterExpenseJson> Execute(RequestRegisterExpenseJson request);
}
