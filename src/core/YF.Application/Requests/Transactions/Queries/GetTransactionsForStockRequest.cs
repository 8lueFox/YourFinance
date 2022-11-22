using YF.Domain.Entities;

namespace YF.Application.Requests.Transactions.Queries;

public record GetTransactionsForStockRequest(Guid Id) : IRequest<TransactionListDto>;

public class TransactionsForStockSpec : Specification<Transaction, TransactionDto>
{
    public TransactionsForStockSpec(Guid stockId)
    {
        Query.Where(t => t.StockId == stockId);
    }
}

public class GetTransactionsForStockRequestHandler : IRequestHandler<GetTransactionsForStockRequest, TransactionListDto>
{
    private readonly IRepository<Transaction> _repository;

    public GetTransactionsForStockRequestHandler(IRepository<Transaction> repository)
    {
        _repository = repository;
    }

    public async Task<TransactionListDto> Handle(GetTransactionsForStockRequest request, CancellationToken cancellationToken)
    {
        var transactions = await _repository.ListAsync(new TransactionsForStockSpec(request.Id), cancellationToken);

        return new TransactionListDto { Transactions = transactions };
    }
}
