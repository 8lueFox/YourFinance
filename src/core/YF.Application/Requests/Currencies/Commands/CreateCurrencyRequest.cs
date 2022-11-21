using FluentValidation;
using YF.Domain.Entities;

namespace YF.Application.Requests.Currencies.Commands;

public record CreateCurrencyRequest(string Name, string Code) : IRequest<Guid>;

public class CreateCurrencyValdiator : AbstractValidator<Currency>
{
    public CreateCurrencyValdiator()
    {
        RuleFor(c => c.Name)
            .NotEmpty();

        RuleFor(c => c.Code)
            .NotEmpty();
    }
}

public class CreateCurrencyRequestHandler : IRequestHandler<CreateCurrencyRequest, Guid>
{
    private readonly IRepository<Currency> _currencyRepository;

    public CreateCurrencyRequestHandler(IRepository<Currency> currencyRepository)
    {
        _currencyRepository = currencyRepository;
    }

    public async Task<Guid> Handle(CreateCurrencyRequest request, CancellationToken cancellationToken)
    {
        var currency = new Currency
        {
            Code = request.Code,
            Name = request.Name
        };

        await _currencyRepository.AddAsync(currency, cancellationToken);

        return currency.Id;
    }
}
