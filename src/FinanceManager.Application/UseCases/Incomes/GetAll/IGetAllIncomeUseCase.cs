using FinanceManager.Communication.Responses.Incomes;

namespace FinanceManager.Application.UseCases.Incomes.GetAll;

public interface IGetAllIncomeUseCase
{
    public Task<ResponseIncomesJson> Execute();
}