namespace EliteJourneyReader.EventMessages;

public sealed class MarketSellEventMessage : JourneyEventMessage
{
    public int Count { get; set; }
    public string Type { get; set; } = default!;
    public ulong SellPrice { get; set; }
    public ulong TotalSale { get; set; }
    public ulong AvgPricePaid { get; set; }
    public override string EventTypeName => "MarketSell";
}