using YF.Domain.Entities;

namespace YF.Application.Requests.Transactions.Commands;

public record DeleteTransactionRequest(Guid Id) : IRequest<Guid>;

public class DeleteTransactionRequestHandler : IRequestHandler<DeleteTransactionRequest, Guid>
{
    private readonly IRepository<Transaction> _repository;

    public DeleteTransactionRequestHandler(IRepository<Transaction> repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(DeleteTransactionRequest request, CancellationToken cancellationToken)
    {
        var transaction = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = transaction ?? throw new NotFoundException($"Transaction {request.Id} not found.");

        await _repository.DeleteAsync(transaction, cancellationToken);

        return transaction.Id;
    }
}
