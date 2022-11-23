using YF.Domain.Entities;

namespace YF.Application.Requests.Currencies.Commands;

public record UpdateCurrencyRequest(Guid Id, string Name, string Code) : IRequest<Guid>;

public class UpdateCurrencyRequestHandler : IRequestHandler<UpdateCurrencyRequest, Guid>
{
    private readonly IRepository<Currency> _repository;

    public UpdateCurrencyRequestHandler(IRepository<Currency> repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(UpdateCurrencyRequest request, CancellationToken cancellationToken)
    {
        var currency = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = currency ?? throw new NotFoundException($"Currency {request.Id} not found.");

        currency.Update(request.Name, request.Code);

        await _repository.UpdateAsync(currency, cancellationToken);

        return currency.Id;
    }
}
