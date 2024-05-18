using FinanceManager.Communication.Responses.Incomes.GetIncomeById;

namespace FinanceManager.Application.UseCases.Incomes.GetById;

public interface IGetIncomeByIdUseCase
{
    public Task<ResponseIncomeJson> Execute(long id);
}