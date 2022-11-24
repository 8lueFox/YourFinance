namespace YF.Application.Common.Interfaces;

public interface ICurrenciesService : ISingleton
{
    /// <summary>
    /// </summary>
    /// <param name="key">Currencies pair e.g. "USD/EUR"</param>
    /// <returns>Exchange rate for given key</returns>
    double ValueFromKey(string key);

    /// <summary>
    /// Calling this method refreshes all currencies exchange rate.
    /// </summary>
    Task RefreshValues();
}
