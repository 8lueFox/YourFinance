namespace YF.Application.Requests.Stocks;

public class StockDto : IDto
{
    public string? Company { get; set; }

    public string? Ticker { get; set; }

    public string? Sector { get; set; }

    public double Units { get; set; }

    public double AvgPrice { get; set; }

    public double CurrentPrice { get; set; }

    public double CostBasis { get; set; }

    public double CurrentBalance { get; set; }

    public double Allocation { get; set; }

    public double MaxCap { get; set; }

    public double GainLoss { get; set; }

    public double Growth { get; set; }

    public double CurrentBalanceInDefaultCurrency { get; set; }
}