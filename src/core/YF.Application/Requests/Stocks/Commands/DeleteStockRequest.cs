using YF.Application.Requests.Transactions.Specs;
using YF.Domain.Entities;

namespace YF.Application.Requests.Stocks.Commands;

public record DeleteStockRequest(Guid Id) : IRequest<Guid>;

public class DeleteStockRequestHandler : IRequestHandler<DeleteStockRequest, Guid>
{
    private readonly IRepository<Stock> _stockRepository;
    private readonly IRepository<Transaction> _transactionRepository;

    public DeleteStockRequestHandler(IRepository<Stock> stockRepository, IRepository<Transaction> transactionRepository)
    {
        _stockRepository = stockRepository;
        _transactionRepository = transactionRepository;
    }

    public async Task<Guid> Handle(DeleteStockRequest request, CancellationToken cancellationToken)
    {
        var stock = await _stockRepository.GetByIdAsync(request.Id, cancellationToken);

        _ = stock ?? throw new NotFoundException($"Stock {request.Id} not found.");

        var transactionList = await _transactionRepository.ListAsync(new StockTransactionsSpec(request.Id), cancellationToken);

        await _transactionRepository.DeleteRangeAsync(transactionList, cancellationToken);

        await _stockRepository.DeleteAsync(stock, cancellationToken);

        return request.Id;
    }
}
