namespace FinanceManager.Communication.Responses.Incomes.GetAll;

public class ResponseShortIncome
{
    public long Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public decimal Amount { get; set; }

}