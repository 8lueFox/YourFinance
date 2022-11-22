namespace YF.Application.Requests.Transactions;

public class TransactionDto
{
    public string? StockName { get; set; }

    public double Units { get; set; }

    public double Price { get; set; }

    public DateTime TimeOfBought { get; set; }
}