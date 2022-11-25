using YF.Domain.Entities;

namespace YF.Application.Requests.Stocks.Queries;

public record GetAllStocksRequest() : IRequest<StocksSummaryDto>;

//in later need to implement searching by user
public class StocksByNothingSpec : Specification<Stock, StockDto>
{
    public StocksByNothingSpec()
    {
        Query.Select(s => new StockDto
        {
            Company = s.Company,
            Ticker = s.Ticker,
            Sector = s.Sector,
            Units = s.Transactions.Sum(t => t.Units),
            AvgPrice = s.Transactions.Sum(t => t.Units * t.Price) / s.Transactions.Sum(t => t.Units),
            CostBasis = s.Transactions.Sum(t => t.Units * t.Price)
        });
    }
}

public class GetAllStocksRequestHandler : IRequestHandler<GetAllStocksRequest, StocksSummaryDto>
{
    private readonly IReadRepository<Stock> _stockRepository;

    public GetAllStocksRequestHandler(IReadRepository<Stock> stockRepository)
    {
        _stockRepository = stockRepository;
    }

    public async Task<StocksSummaryDto> Handle(GetAllStocksRequest request, CancellationToken cancellationToken)
    {
        var allStocks = await _stockRepository.ListAsync(new StocksByNothingSpec(), cancellationToken);

        return new StocksSummaryDto { Stocks = allStocks };
    }
}