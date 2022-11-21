using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YF.Application.Requests.Stocks;
using YF.Application.Requests.Stocks.Commands;
using YF.Application.Requests.Stocks.Queries;

namespace YF.Api.Controllers;

public class StocksController : BaseController
{
    [HttpPost]
    public async Task<Guid> CreateStockAsync(CreateStockRequest request)
        => await Mediator.Send(request);

    [HttpDelete]
    public async Task<Guid> DeleteStockAsync(DeleteStockRequest request)
        => await Mediator.Send(request);

    [HttpPut]
    public async Task<Guid> UpdateStockAsync(UpdateStockRequest request)
        => await Mediator.Send(request);

    [HttpGet]
    public async Task<StocksSummaryDto> GetAllStocks([FromQuery] GetAllStocksRequest request)
        => await Mediator.Send(request);
}
