namespace FinanceManager.Application.UseCases.Expenses.Delete;

public interface IDeleteExpenseUseCase
{
    Task Delete(long id);
}