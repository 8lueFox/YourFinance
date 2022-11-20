namespace YF.Domain.Entities;

public class Transaction : BaseEntity, IAggregateRoot
{
    public Guid StockId { get; set; }

    public double Units { get; set; }

    public double Price { get; set; }

    public virtual Currency? Currency { get; set; }
}
