using System.Text.Json.Serialization;

namespace YF.Infrastructure.Services.ExchangeRateService;

public class ExRatesCryptoDto
{
    [JsonPropertyName("data")]
    public List<Data> Data { get; set; } = new();
}

public class Data
{
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("symbol")]
    public string? Symbol { get; set; }

    [JsonPropertyName("quote")]
    public Quote? Quote { get; set; }
}

public class Quote
{
    public USD? USD { get; set; }
}

public class USD
{
    [JsonPropertyName("price")]
    public double Price { get; set; }
}