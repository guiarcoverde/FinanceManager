using FinanceManager.Communication.Enums;

namespace FinanceManager.Communication.Requests.Incomes;

public class RequestIncomeRegistrationJson
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public decimal Amount { get; set; }
    public SourceIncomes Source { get; set; }
}