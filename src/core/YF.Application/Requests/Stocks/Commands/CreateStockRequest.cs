using FluentValidation;
using YF.Domain.Entities;

namespace YF.Application.Requests.Stocks.Commands;

public record CreateStockRequest(string Company, string Ticker, string Sector, string Currency) : IRequest<Guid>;

public class CurrencyByCodeSpec : Specification<Currency>, ISingleResultSpecification
{
    public CurrencyByCodeSpec(string code)
        => Query.Where(b => b.Code == code);
}

public class CreateStockRequestValidator : AbstractValidator<CreateStockRequest>
{
    public CreateStockRequestValidator(IReadRepository<Currency> currencyRepository)
    {
        RuleFor(c => c.Company)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(c => c.Ticker)
            .NotEmpty()
            .MaximumLength(20);

        RuleFor(c => c.Sector)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(c => c.Currency)
            .NotEmpty()
            .MaximumLength(10)
            .MustAsync(async (currency, ct) => await currencyRepository.AnyAsync(new CurrencyByCodeSpec(currency), ct));

    }
}

public class CreateStockRequestHandler : IRequestHandler<CreateStockRequest, Guid>
{
    private readonly IRepository<Stock> _stockRepository;
    private readonly IRepository<Currency> _currencyRepository;

    public CreateStockRequestHandler(IRepository<Stock> stockRepository, IRepository<Currency> currencykRepository)
    {
        _stockRepository = stockRepository;
        _currencyRepository = currencykRepository;
    }

    public async Task<Guid> Handle(CreateStockRequest request, CancellationToken cancellationToken)
    {
        var stock = new Stock
        {
            Company = request.Company,
            Currency = await _currencyRepository.FirstOrDefaultAsync(new CurrencyByCodeSpec(request.Currency), cancellationToken),
            Sector = request.Sector,
            Ticker = request.Ticker
        };

        await _stockRepository.AddAsync(stock, cancellationToken);

        return stock.Id;
    }
} 
