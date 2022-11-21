using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YF.Application.Requests.Currencies;
using YF.Application.Requests.Currencies.Commands;
using YF.Application.Requests.Currencies.Queries;
using YF.Domain.Entities;

namespace YF.Api.Controllers;

public class CurrencyController : BaseController
{
    [HttpPost]
    public async Task<Guid> CreateCurrencyAsync(CreateCurrencyRequest request)
        => await Mediator.Send(request);

    [HttpGet(Name = "GetAllCurrencies")]
    public async Task<CurrencyListDto> GetAllAsync([FromQuery] GetCurrenciesRequest request)
        => await Mediator.Send(request);
}
