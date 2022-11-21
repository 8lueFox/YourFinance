namespace YF.Domain.Entities;

public class Stock : BaseEntity, IAggregateRoot
{
    public Guid CurrencyId { get; set; }

    public string Company { get; set; } = default!;

    public string Ticker { get; set; } = default!;

    public string Sector { get; set; } = default!;

    public virtual Currency? Currency { get; set; }

    public virtual List<Transaction> Transactions { get; set; } = new();

    public Stock Update(string? company, string? ticker, string? sector, Guid? currencyId)
    {
        if (company is not null && Company?.Equals(company) is not true) Company = company;
        if (ticker is not null && Ticker?.Equals(ticker) is not true) Ticker = ticker;
        if (sector is not null && Sector?.Equals(sector) is not true) Sector = sector;
        if (currencyId is not null && currencyId.Value != Guid.Empty && !CurrencyId.Equals(currencyId.Value)) CurrencyId = currencyId.Value;

        return this;
    }
}
