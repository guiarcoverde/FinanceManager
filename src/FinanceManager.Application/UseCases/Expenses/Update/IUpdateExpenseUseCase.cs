using FinanceManager.Communication.Requests;
using FinanceManager.Communication.Requests.Expenses;

namespace FinanceManager.Application.UseCases.Expenses.Update;

public interface IUpdateExpenseUseCase
{
    Task Execute(long id, RequestExpenseJson request);
}