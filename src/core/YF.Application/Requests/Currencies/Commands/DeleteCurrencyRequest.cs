using YF.Domain.Entities;

namespace YF.Application.Requests.Currencies.Commands;

public record DeleteCurrencyRequest(Guid Id) : IRequest<Guid>;

public class DeleteCurrencyRequestHandler : IRequestHandler<DeleteCurrencyRequest, Guid>
{
    private readonly IRepository<Currency> _repository;

    public DeleteCurrencyRequestHandler(IRepository<Currency> repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(DeleteCurrencyRequest request, CancellationToken cancellationToken)
    {
        var currency = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = currency ?? throw new NotFoundException($"Currency {request.Id} not found.");

        await _repository.DeleteAsync(currency, cancellationToken);

        return currency.Id;
    }
}
