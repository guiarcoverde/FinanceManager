namespace FinanceManager.Application.UseCases.Incomes.Delete;

public interface IDeleteIncomeUseCase
{
    public Task Execute(long id);
}