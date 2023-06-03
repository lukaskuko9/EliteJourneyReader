using EliteJourneyReader.Public.EventMessages;

namespace EliteJourneyReader.Public.EliteJourneyProvider;

public partial class EliteJourneyProvider
{
    /// <summary>
    /// Triggered by any in-game event. Contains deserialized message and json value of the message
    /// </summary>
    public event JourneyEventDelegate? OnAnyEvent;
    
    /// <summary>
    /// Triggered if message was received but there was error during deserializing the message,
    /// sending at least the json of original message
    /// </summary>
    public event JourneyErrorEventDelegate? OnReaderError;
    
   
#pragma warning disable CS0067
    /// <summary>
    /// Triggered when receiving information about a change in a friend's status
    /// </summary>
    public event EventHandler<FriendsEventMessage>? OnFriendsChange;
     
    /// <summary>
    /// Triggered at startup, when loading from main menu into game
    /// </summary>
    public event EventHandler<LoadGameEventMessage>? OnLoadGame;
    
    /// <summary>
    /// Heading entry. If the play session goes on a long time, and the journal gets very large,
    /// the file will be closed and a new file started with an increased part number: the heading entry is added at the beginning of every file.
    /// </summary>
    public event EventHandler<FileHeaderEventMessage>? OnFileHeader;
    
    /// <summary>
    /// Triggered when purchasing goods in the market
    /// </summary>
    public event EventHandler<MarketBuyEventMessage>? OnMarketBuy;
    
    /// <summary>
    /// Triggered when selling goods in the market
    /// </summary>
    public event EventHandler<MarketSellEventMessage>? OnMarketSell;
#pragma warning restore CS0067   
}