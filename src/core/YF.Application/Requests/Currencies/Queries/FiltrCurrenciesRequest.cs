using YF.Domain.Entities;

namespace YF.Application.Requests.Currencies.Queries;

public record FiltrCurrenciesRequest(string Phrase) : IRequest<CurrencyListDto>;

public class CurrenciesByPhraseSpec : Specification<Currency, CurrencyDto>, ISpecification<Currency, CurrencyDto>
{
    public CurrenciesByPhraseSpec(string Phrase)
    {
        Phrase = Phrase.ToLowerInvariant();
        Query.Where(q => q.Code.ToLowerInvariant().StartsWith(Phrase) || q.Name.ToLowerInvariant().StartsWith(Phrase));
        Query.AsNoTracking();
    }
}

public class FiltrCurrenciesRequestHandler : IRequestHandler<FiltrCurrenciesRequest, CurrencyListDto>
{
    private readonly IRepository<Currency> _repository;

    public FiltrCurrenciesRequestHandler(IRepository<Currency> repository)
        => _repository = repository;

    public async Task<CurrencyListDto> Handle(FiltrCurrenciesRequest request, CancellationToken cancellationToken)
        => new CurrencyListDto
        {
            Currencies = await _repository.ListAsync(
                new CurrenciesByPhraseSpec(request.Phrase), cancellationToken)
        };
}