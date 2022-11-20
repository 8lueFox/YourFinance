using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YF.Application.Requests.Currencies;
using YF.Application.Requests.Currencies.Queries;
using YF.Domain.Entities;

namespace YF.Api.Controllers;

public class CurrencyController : BaseController
{
    [HttpGet(Name = "GetAllCurrencies")]
    public async Task<CurrencyListDto> GetAllAsync([FromQuery] GetCurrenciesRequest request)
        => await Mediator.Send(request);
}
