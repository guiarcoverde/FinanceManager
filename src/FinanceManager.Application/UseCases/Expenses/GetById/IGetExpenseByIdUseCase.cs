using FinanceManager.Communication.Responses.GetExpenseById;

namespace FinanceManager.Application.UseCases.Expenses.GetById;
public interface IGetExpenseByIdUseCase
{
    Task<ResponseExpenseJson> Execute(long id);
}
