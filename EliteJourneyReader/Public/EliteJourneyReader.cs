using EliteJourneyReader.Public.EventMessages;
using EliteJourneyReader.Public.JourneyReader;

namespace EliteJourneyReader.Public;

public class EliteJourneyReader
{

    public delegate void JourneyEventDelegate<in TMessage>(TMessage message) where TMessage : JourneyEventMessage;
    public delegate void JourneyEventDelegate(JourneyEventMessage? message, string jsonMessage);
    
    internal delegate void CallEventDelegate(JourneyEventMessage? message, string json);
    
    public event JourneyEventDelegate? OnAnyEvent;
    public event JourneyEventDelegate<FriendsEventMessage>? OnFriendsChange;
    public event JourneyEventDelegate<LoadGameEventMessage>? OnLoadGame;
    public event JourneyEventDelegate<FileHeaderEventMessage>? OnFileHeader;
    public event JourneyEventDelegate<MarketBuyEventMessage>? OnMarketBuy;
    public event JourneyEventDelegate<MarketSellEventMessage>? OnMarketSell;
    public event JourneyEventDelegate<ErrorMessage>? OnReaderError;

    public EliteJourneyReader(JourneyFileReader fileReader)
    {
        fileReader.Register(CallEvent, TypesDictionary);
    }
    
    private void CallEvent(JourneyEventMessage? eventMessage, string json)
    {
        if (eventMessage is ErrorMessage errorMessage)
        {
            OnReaderError?.Invoke(errorMessage);
            return;
        }

        OnAnyEvent?.Invoke(eventMessage, json);
        if (eventMessage is not null)
        {
            if (eventMessage is FriendsEventMessage friendsEventMessage)
                OnFriendsChange?.Invoke(friendsEventMessage);
            else if (eventMessage is LoadGameEventMessage loadGameEventMessage)
                OnLoadGame?.Invoke(loadGameEventMessage);
            else if (eventMessage is FileHeaderEventMessage fileHeaderEventMessage)
                OnFileHeader?.Invoke(fileHeaderEventMessage);
            else if (eventMessage is MarketBuyEventMessage marketBuyEventMessage)
                OnMarketBuy?.Invoke(marketBuyEventMessage);
            else if (eventMessage is MarketSellEventMessage marketSellEventMessage)
                OnMarketSell?.Invoke(marketSellEventMessage);
        }
    }

    private readonly Dictionary<string, Type> TypesDictionary = new()
    {
        { "Friends", typeof(FriendsEventMessage) },
        { "FileHeader", typeof(FileHeaderEventMessage) },
        { "LoadGame", typeof(LoadGameEventMessage) },
        { "MarketBuy", typeof(MarketBuyEventMessage) },
        { "MarketSell", typeof(MarketSellEventMessage) }
    };
}