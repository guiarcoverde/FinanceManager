namespace FinanceManager.Communication.Responses.Expenses.GetAll;

public class ResponseShortExpensesJson
{
    public long Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public decimal Amount { get; set; }
}
