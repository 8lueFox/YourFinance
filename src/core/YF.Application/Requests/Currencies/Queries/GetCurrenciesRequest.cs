using YF.Domain.Entities;

namespace YF.Application.Requests.Currencies.Queries;

public record GetCurrenciesRequest() : IRequest<CurrencyListDto>;

public class CurrenciesSpec : Specification<Currency, CurrencyDto>
{
    public CurrenciesSpec()
    {
        Query.AsNoTracking();
    }
}

public class GetCurrenciesRequestHandler : IRequestHandler<GetCurrenciesRequest, CurrencyListDto>
{
    private readonly IRepository<Currency> _repository;

    public GetCurrenciesRequestHandler(IRepository<Currency> repository)
        => _repository = repository;

    public async Task<CurrencyListDto> Handle(GetCurrenciesRequest request, CancellationToken cancellationToken)
        => new CurrencyListDto
        {
            Currencies = await _repository.ListAsync(
            (ISpecification<Currency, CurrencyDto>)new CurrenciesSpec(), cancellationToken)
        };
}