namespace YF.Domain.Entities;

public class Currency : BaseEntity, IAggregateRoot
{
    public string Code { get; set; } = default!;

    public string Name { get; set; } = default!;

    public virtual List<Transaction> Transactions { get; private set; } = new ();
}
