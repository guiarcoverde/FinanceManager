using FinanceManager.Communication.Enums;

namespace FinanceManager.Communication.Responses.Incomes.GetIncomeById;

public class ResponseIncomeJson
{
    public string Title { get; set; } = "";
    public string Description { get; set; } = "";
    public decimal Amount { get; set; }
    public SourceIncomes Source { get; set; }
    public DateTime Date { get; set; }
}