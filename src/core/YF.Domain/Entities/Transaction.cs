namespace YF.Domain.Entities;

public class Transaction : BaseEntity, IAggregateRoot
{
    public Guid StockId { get; set; }
    public Guid CurrencyId { get; set; }

    public double Units { get; set; }

    public double Price { get; set; }

    public DateTime TimeOfBought { get; set; }

    public virtual Currency? Currency { get; set; }
    public virtual Stock? Stock { get; set; }
}
