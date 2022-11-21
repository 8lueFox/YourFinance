using YF.Domain.Entities;

namespace YF.Application.Requests.Stocks.Commands;

public record UpdateStockRequest(Guid Id, string Company, string Ticker, string Sector, string Currency) : IRequest<Guid>;

public class UpdateStockRequestHandler : IRequestHandler<UpdateStockRequest, Guid>
{
    private readonly IRepository<Stock> _stockRepository;
    private readonly IRepository<Currency> _currencyRepository;

    public UpdateStockRequestHandler(IRepository<Stock> stockRepository, IRepository<Currency> currencyRepository)
    {
        _stockRepository = stockRepository;
        _currencyRepository = currencyRepository;
    }

    public async Task<Guid> Handle(UpdateStockRequest request, CancellationToken cancellationToken)
    {
        var stock = await _stockRepository.GetByIdAsync(request.Id, cancellationToken);

        _ = stock ?? throw new NotFoundException($"Stock {request.Id} not found.");

        var currency = await _currencyRepository.FirstOrDefaultAsync(new CurrencyByCodeSpec(request.Currency), cancellationToken);

        stock.Update(request.Company, request.Ticker, request.Sector, currency?.Id);

        await _stockRepository.UpdateAsync(stock, cancellationToken);

        return request.Id;
    }
}