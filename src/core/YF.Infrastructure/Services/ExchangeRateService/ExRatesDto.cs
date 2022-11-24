using System.Text.Json.Serialization;

namespace YF.Infrastructure.Services.ExchangeRateService;

public class ExRatesDto
{
    [JsonPropertyName("base")]
    public string? Base { get; set; }

    [JsonPropertyName("lastupdated")]
    public int LastUpdated { get; set; }

    [JsonPropertyName("exchangerates")]
    public Dictionary<string, double> ExchangeRates { get; set; } = new();
}
