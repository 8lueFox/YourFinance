namespace YF.Application.Requests.Currencies;

public class CurrencyDto : IDto
{
    public string Code { get; set; } = default!;

    public string Name { get; set; } = default!;
}
