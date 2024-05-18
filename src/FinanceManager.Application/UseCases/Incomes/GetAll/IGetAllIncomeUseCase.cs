using FinanceManager.Communication.Responses.Incomes;
using FinanceManager.Communication.Responses.Incomes.GetAll;

namespace FinanceManager.Application.UseCases.Incomes.GetAll;

public interface IGetAllIncomeUseCase
{
    public Task<ResponseIncomesJson> Execute();
}