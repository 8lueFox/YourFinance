using YF.Domain.Entities;

namespace YF.Application.Requests.Currencies;

public class CurrencyListDto : IDto
{
    public List<CurrencyDto> Currencies { get; set; } = new();
}
