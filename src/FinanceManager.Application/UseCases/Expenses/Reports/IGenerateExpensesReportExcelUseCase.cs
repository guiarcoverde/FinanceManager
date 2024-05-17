namespace FinanceManager.Application.UseCases.Expenses.Reports;

public interface IGenerateExpensesReportExcelUseCase
{
    Task<byte[]> Execute(DateTime month);

}