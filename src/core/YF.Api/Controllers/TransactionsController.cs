using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YF.Application.Requests.Transactions;
using YF.Application.Requests.Transactions.Commands;
using YF.Application.Requests.Transactions.Queries;

namespace YF.Api.Controllers;

public class TransactionsController : BaseController
{
    [HttpPost]
    public async Task<Guid> CreateTransactionRequest(CreateTransactionRequest request)
        => await Mediator.Send(request);

    [HttpPut]
    public async Task<Guid> UpdateTransactionRequest(UpdateTransactionRequest request)
        => await Mediator.Send(request);

    [HttpDelete]
    public async Task<Guid> DeleteTransactionRequest(DeleteTransactionRequest request)
        => await Mediator.Send(request);

    [HttpGet]
    public async Task<TransactionListDto> GetTransactionList([FromQuery] GetTransactionsForStockRequest request)
        => await Mediator.Send(request);
}
