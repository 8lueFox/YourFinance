using CsvHelper;
using System.Globalization;
using YF.Domain.Entities;

namespace YF.Infrastructure.Persistence.Context;

public class ApplicationDbContextInitializator
{
    private readonly ApplicationDbContext _context;

    public ApplicationDbContextInitializator(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task SeedAsync()
    {
        await SeedCurrenciesAsync();
    }

    private async Task SeedCurrenciesAsync()
    {
        if (!_context.Currencies.Any())
        {
            using (var reader = new StreamReader("AdditionalFiles/currencies.csv"))
            using(var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var record = new Record();
                var records = csv.EnumerateRecords(record);
                foreach (var r in records)
                {
                    _context.Currencies.Add(new Currency { Code = r.CurrencyCode, Name = r.CurrencyName });
                }
            }
        }
        await _context.SaveChangesAsync();
    }

    class Record
    {
        public string CurrencyCode { get; set; }
        public string CurrencyName { get; set; }
    }
}
