namespace FinanceManager.Communication.Responses.GetAll;

public class ResponseExpensesJson
{
    public List<ResponseShortExpensesJson> Expenses { get; set; } = [];
}
