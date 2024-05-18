using FinanceManager.Communication.Requests.Incomes;

namespace FinanceManager.Application.UseCases.Incomes.Update;

public interface IUpdateIncomeUseCase
{
    public Task Execute(long id, RequestIncomeUpdateJson request);
}