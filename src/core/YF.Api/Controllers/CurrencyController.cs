using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YF.Application.Requests.Currencies;
using YF.Application.Requests.Currencies.Commands;
using YF.Application.Requests.Currencies.Queries;
using YF.Domain.Entities;

namespace YF.Api.Controllers;

[Route("api/[controller]/[action]")]
public class CurrencyController : BaseController
{
    [HttpPost]
    public async Task<Guid> CreateCurrencyAsync(CreateCurrencyRequest request)
        => await Mediator.Send(request);

    [HttpDelete]
    public async Task<Guid> DeleteCurrencyAsync(DeleteCurrencyRequest request)
        => await Mediator.Send(request);

    [HttpPut]
    public async Task<Guid> UpdateCurrencyAsync(UpdateCurrencyRequest request)
        => await Mediator.Send(request);

    [HttpGet(Name = "GetAllCurrencies")]
    public async Task<CurrencyListDto> GetAllAsync([FromQuery] GetCurrenciesRequest request)
        => await Mediator.Send(request);

    [HttpGet(Name = "FiltrAllCurrencies")]
    public async Task<CurrencyListDto> FiltrCurrenciesAsync([FromQuery] FiltrCurrenciesRequest request)
        => await Mediator.Send(request);

    [HttpGet(Name = "GetExchangeRate")]
    public async Task<ExchangeRateDto> GetExchangeRateAsync([FromQuery] GetExchangeRateRequest request)
        => await Mediator.Send(request);

}
