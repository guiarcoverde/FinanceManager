﻿using FinanceManager.Communication.Enums;

namespace FinanceManager.Communication.Responses.Expenses.GetExpenseById;

public class ResponseExpenseJson
{
    public long Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public decimal Amount { get; set; }
    public PaymentType PaymentType { get; set; }
    public IList<Tags> Tags { get; set; } = []; 
}
