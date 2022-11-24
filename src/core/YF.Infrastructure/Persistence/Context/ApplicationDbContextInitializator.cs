using CsvHelper;
using System.Globalization;
using YF.Application.Common.Interfaces;
using YF.Domain.Entities;

namespace YF.Infrastructure.Persistence.Context;

public class ApplicationDbContextInitializator
{
    private readonly ApplicationDbContext _context;
    private readonly ICurrenciesService _currenicesService;

    public ApplicationDbContextInitializator(ApplicationDbContext context, ICurrenciesService currenicesService)
    {
        _context = context;
        _currenicesService = currenicesService;
    }

    public async Task SeedAsync()
    {
        await SeedCurrenciesAsync();
        await _currenicesService.RefreshValues();
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
                    _context.Currencies.Add(new Currency { Code = r.CurrencyCode!, Name = r.CurrencyName! });
                }
            }
        }
        await _context.SaveChangesAsync();
    }

    class Record
    {
        public string? CurrencyCode { get; set; }
        public string? CurrencyName { get; set; }
    }
}
