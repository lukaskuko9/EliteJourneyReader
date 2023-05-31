namespace EliteJourneyReader.Public.EventMessages;

public sealed class MarketBuyEventMessage : JourneyEventMessage
{
    public int Count { get; set; }
    public string Type { get; set; } = string.Empty;
    public ulong BuyPrice { get; set; }
    public ulong TotalCost { get; set; }
    public override string EventTypeName => "MarketBuy";
}