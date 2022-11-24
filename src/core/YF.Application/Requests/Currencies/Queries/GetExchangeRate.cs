namespace YF.Application.Requests.Currencies.Queries;

public record GetExchangeRateRequest(string Key) : IRequest<ExchangeRateDto>;

public class GetExchangeRateRequestHandler : IRequestHandler<GetExchangeRateRequest, ExchangeRateDto>
{
    private readonly ICurrenciesService _currenciesService;

    public GetExchangeRateRequestHandler(ICurrenciesService currenciesService)
    {
        _currenciesService = currenciesService;
    }

    public Task<ExchangeRateDto> Handle(GetExchangeRateRequest request, CancellationToken cancellationToken)
    {
        var response = new ExchangeRateDto() { Key = request.Key };

        response.Value = _currenciesService.ValueFromKey(request.Key);

        return Task.FromResult(response);
    }
}
