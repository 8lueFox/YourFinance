namespace YF.Domain.Entities;

public class Stock : BaseEntity, IAggregateRoot
{
    public string Company { get; set; } = default!;

    public string Ticker { get; set; } = default!;

    public string Sector { get; set; } = default!;

    public virtual Currency? Currency { get; set; }

    public virtual List<Transaction> Transactions { get; set; } = new();
}
