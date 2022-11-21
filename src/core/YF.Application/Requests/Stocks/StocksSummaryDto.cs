namespace YF.Application.Requests.Stocks;

public class StocksSummaryDto : IDto
{
    public List<StockDto> Stocks { get; set; } = new();
}
