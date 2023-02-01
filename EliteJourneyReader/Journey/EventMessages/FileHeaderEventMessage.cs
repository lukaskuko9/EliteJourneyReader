using Newtonsoft.Json;

namespace EliteJourneyReader.Journey.EventMessages;

public sealed class FileHeaderEventMessage : JourneyEventMessage
{
    public int Part { get; set; }    
    
    public string Language { get; set; }
    public bool Oddysey { get; set; }
    public string GameVersion { get; set; }
    public string Build { get; set; }
}

public sealed class MarketBuyEventMessage : JourneyEventMessage
{
    public int Count { get; set; }
    public string Type { get; set; }
    public ulong BuyPrice { get; set; }
    public ulong TotalCost { get; set; }
}

public sealed class MarketSellEventMessage : JourneyEventMessage
{
    public int Count { get; set; }
    public string Type { get; set; }
    public ulong SellPrice { get; set; }
    public ulong TotalSale { get; set; }
    public ulong AvgPricePaid { get; set; }
}