using FinanceManager.Communication.Requests;

namespace FinanceManager.Application.UseCases.Expenses.Update;

public interface IUpdateExpenseUseCase
{
    Task Execute(long id, RequestExpenseJson request);
}