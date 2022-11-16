namespace YF.Application.Requests.Currencies;

public class CurrencyListDto
{
    public List<CurrencyDto> Currencies { get; set; } = new();

    public CurrencyListDto()
    {
    }

    public CurrencyListDto(List<CurrencyDto> currencies)
    {
        Currencies = currencies;
    }
}
