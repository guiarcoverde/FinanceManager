using FinanceManager.Domain.Enums;

namespace FinanceManager.Domain.Entities;

public class Income
{
    public long Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime Date { get; set; }
    public decimal Amount { get; set; }
    public SourceIncomes Source { get; set; }
    public Guid UserId { get; set; }
    public virtual User User { get; set; } = null!;
}