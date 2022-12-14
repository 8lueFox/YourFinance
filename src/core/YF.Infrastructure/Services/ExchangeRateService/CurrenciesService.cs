using Microsoft.Extensions.Options;
using YF.Api.Configurations.ConfigsDto;
using YF.Application.Common.Interfaces;
using Newtonsoft.Json;
using System.Web;
using System.Net;
using YF.Infrastructure.Common.ConfigsDto;

namespace YF.Infrastructure.Services.ExchangeRateService;

public class CurrenciesService : ICurrenciesService
{
    private Dictionary<string, double> _currenciesExRates;
    private readonly AbstractApiSettings _abstractSettings;
    private readonly CoinMarketCapApiSettings _coinMarketSettings;

    public CurrenciesService(IOptions<AbstractApiSettings> abstractSettings, IOptions<CoinMarketCapApiSettings> coinMarketSettings)
    {
        _abstractSettings = abstractSettings.Value;
        _coinMarketSettings = coinMarketSettings.Value;
        _currenciesExRates = new Dictionary<string, double>();
    }

    public double ValueFromKey(string key)
    {
        if (_currenciesExRates.ContainsKey(key))
            return _currenciesExRates[key];

        var splitedKeys = key.Split('/').Select(x => $"USD/{x}").ToArray();
        if (splitedKeys.All(k => _currenciesExRates.ContainsKey(k)))
        {
            var value = _currenciesExRates[splitedKeys[1]] / _currenciesExRates[splitedKeys[0]];
            _currenciesExRates[key] = value;
            return value;
        }

        splitedKeys = key.Split('/').Select(x => $"{x}/USD").ToArray();
        if (splitedKeys.All(k => _currenciesExRates.ContainsKey(k)))
        {
            var value = _currenciesExRates[splitedKeys[0]] / _currenciesExRates[splitedKeys[1]];
            _currenciesExRates[key] = value;
            return value;
        }

        return -1d;
    }

    public async Task RefreshValues()
    {
        var url = $"https://exchange-rates.abstractapi.com/v1/live/?api_key={_abstractSettings.ExchangeRateKey}&base=USD";

        _currenciesExRates.Clear();

        ExRatesDto? rates = null;

        using (var client = new HttpClient())
        {
         
            string contentString = "";
#if DEBUG
            contentString = "{\"base\":\"USD\",\"lastupdated\":1669036500,\"exchangerates\":{\"EUR\":0.973331,\"JPY\":141.327623,\"BGN\":1.90364,\"CZK\":23.701577,\"DKK\":7.239342,\"GBP\":0.840549,\"HUF\":397.342807,\"PLN\":4.586821,\"RON\":4.795503,\"SEK\":10.672864,\"CHF\":0.952988,\"ISK\":141.814288,\"NOK\":10.165953,\"HRK\":7.342612,\"RUB\":104.99999999999999,\"TRY\":18.612128,\"AUD\":1.506035,\"BRL\":5.312245,\"CAD\":1.33979,\"CNY\":7.138797,\"HKD\":7.817111,\"IDR\":15677.233794,\"ILS\":3.466517,\"INR\":81.639868,\"KRW\":1355.538252,\"MXN\":19.559179,\"MYR\":4.580008,\"NZD\":1.626144,\"PHP\":57.37006,\"SGD\":1.378918,\"THB\":36.120304,\"ZAR\":17.283239,\"ARS\":75.269373,\"DZD\":124.445887,\"MAD\":8.83269,\"TWD\":27.466513,\"BTC\":5.2e-05,\"ETH\":0.000764,\"BNB\":0.003633,\"DOGE\":16.654888,\"XRP\":2.07083,\"BCH\":0.008601,\"LTC\":0.018906}}";
#else
            var response = await client.GetAsync(url);
            contentString = await response.Content.ReadAsStringAsync();
            contentString = contentString.Replace("_", "");
#endif
            rates = JsonConvert.DeserializeObject<ExRatesDto>(contentString);
        }

        _currenciesExRates.Add("USD/USD", 1);

        foreach (var rate in rates!.ExchangeRates)
        {
            _currenciesExRates.Add($"{rates.Base}/{rate.Key}", rate.Value);
        }

        var cryptoRates = new ExRatesCryptoDto();
#if DEBUG
        using (StreamReader r = new StreamReader("AdditionalFiles/cryptoDataExample.json"))
        {
            string json = await r.ReadToEndAsync();
            cryptoRates = JsonConvert.DeserializeObject<ExRatesCryptoDto>(json);
        }
#else
        var URL = new UriBuilder("https://pro-api.coinmarketcap.com/v1/cryptocurrency/listings/latest");

        var queryString = HttpUtility.ParseQueryString(string.Empty);
        queryString["start"] = "1";
        queryString["limit"] = "5000";
        queryString["convert"] = "USD";

        URL.Query = queryString.ToString();

        var client2 = new WebClient();
        client2.Headers.Add("X-CMC_PRO_API_KEY", _coinMarketSettings.ExchangeRateKey);
        client2.Headers.Add("Accepts", "application/json");
        var response2 = client2.DownloadString(URL.ToString());

        cryptoRates = JsonConvert.DeserializeObject<ExRatesCryptoDto>(response2);  
#endif

        foreach (var row in cryptoRates!.Data)
        {
            if(!_currenciesExRates.ContainsKey($"{row.Symbol}/USD"))
                _currenciesExRates.Add($"{row.Symbol}/USD", row!.Quote!.USD!.Price);
        }
    }
}
