using YF.Domain.Entities;

namespace YF.Application.Requests.Transactions.Specs;

public class StockTransactionsSpec : Specification<Transaction>
{
    public StockTransactionsSpec(Guid stockId)
        => Query.Where(t => t.Id == stockId);
}
