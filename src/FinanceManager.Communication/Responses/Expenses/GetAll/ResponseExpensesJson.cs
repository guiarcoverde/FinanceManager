namespace FinanceManager.Communication.Responses.Expenses.GetAll;

public class ResponseExpensesJson
{
    public List<ResponseShortExpensesJson> Expenses { get; set; } = []; 
}
