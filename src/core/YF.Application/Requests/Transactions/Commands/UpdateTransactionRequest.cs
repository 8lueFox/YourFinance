using FluentValidation;
using YF.Application.Requests.Stocks.Commands;
using YF.Domain.Entities;

namespace YF.Application.Requests.Transactions.Commands;

public record UpdateTransactionRequest(double Units, double Price, string Currency, DateTime TimeOfBought) : IRequest<Guid>;

public class UpdateTransactionRequestValidator : AbstractValidator<CreateTransactionRequest>
{
    public UpdateTransactionRequestValidator(IRepository<Currency> currencyRepository)
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