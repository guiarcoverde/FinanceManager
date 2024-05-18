namespace FinanceManager.Communication.Requests.Incomes;

public class RequestIncomeUpdateJson
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public decimal Amount { get; set; }
}
