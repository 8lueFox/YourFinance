namespace YF.Application.Requests.Transactions;

public class TransactionListDto
{
    public List<TransactionDto> Transactions { get; set; } = new();
}