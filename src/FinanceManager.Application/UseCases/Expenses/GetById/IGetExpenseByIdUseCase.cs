using FinanceManager.Communication.Responses.Expenses.GetExpenseById;

namespace FinanceManager.Application.UseCases.Expenses.GetById;
public interface IGetExpenseByIdUseCase
{
    Task<ResponseExpenseJson> Execute(long id);
}
