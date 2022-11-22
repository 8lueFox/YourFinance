using FluentValidation;
using YF.Application.Requests.Stocks.Commands;
using YF.Domain.Entities;

namespace YF.Application.Requests.Transactions.Commands;

public record CreateTransactionRequest(double Units, double Price, string Currency, DateTime TimeOfBought, Guid StockId) : IRequest<Guid>;

public class CreateTransactionRequestValidator : AbstractValidator<CreateTransactionRequest>
{
    public CreateTransactionRequestValidator(IRepository<Currency> currencyRepository)
    {
        RuleFor(x => x.Units)
            .GreaterThan(0)
            .NotNull();

        RuleFor(x => x.Price)
            .GreaterThan(0)
            .NotNull();

        RuleFor(x => x.TimeOfBought)
            .NotNull();

        RuleFor(c => c.Currency)
            .NotEmpty()
            .MaximumLength(10)
            .MustAsync(async (currency, ct) => await currencyRepository.AnyAsync(new CurrencyByCodeSpec(currency), ct));
    }
}

public class CreateTransactionRequestHandler : IRequestHandler<CreateTransactionRequest, Guid>
{
    private readonly IRepository<Transaction> _transactionRepository;
    private readonly IRepository<Currency> _currencyRepository;

    public CreateTransactionRequestHandler(IRepository<Transaction> transactionRepository, IRepository<Currency> currencyRepository)
    {
        _transactionRepository = transactionRepository;
        _currencyRepository = currencyRepository;
    }

    public async Task<Guid> Handle(CreateTransactionRequest request, CancellationToken cancellationToken)
    {
        var transaction = new Transaction
        {
            Price = request.Price,
            StockId = request.StockId,
            TimeOfBought = request.TimeOfBought,
            Units = request.Units,
            Currency = await _currencyRepository.FirstOrDefaultAsync(new CurrencyByCodeSpec(request.Currency), cancellationToken)
        };

        await _transactionRepository.AddAsync(transaction, cancellationToken);

        return transaction.Id;
    }
}
