using System.Runtime.CompilerServices;
using EliteJourneyReader.Public.EventMessages;
using EliteJourneyReader.Public.JourneyReader;

[assembly:InternalsVisibleTo("EliteJourneyReaderTests")]
namespace EliteJourneyReader.Public;

public class EliteJourneyProvider
{
    private readonly JourneyFileReader _fileReader;

    public delegate void JourneyEventDelegate<in TMessage>(TMessage message) where TMessage : JourneyEventMessage;
    public delegate void JourneyEventDelegate(JourneyEventMessage? message, string jsonMessage);
    
    internal delegate void CallEventDelegate(JourneyEventMessage? message, string json);
    
    public event JourneyEventDelegate? OnAnyEvent;
    public event JourneyEventDelegate<ErrorMessage>? OnReaderError;
    
    public event JourneyEventDelegate<FriendsEventMessage>? OnFriendsChange;
    public event JourneyEventDelegate<LoadGameEventMessage>? OnLoadGame;
    public event JourneyEventDelegate<FileHeaderEventMessage>? OnFileHeader;
    public event JourneyEventDelegate<MarketBuyEventMessage>? OnMarketBuy;
    public event JourneyEventDelegate<MarketSellEventMessage>? OnMarketSell;

    public void SetJournalDirectoryPath(string path)
    {
        _fileReader.SetNewJournalDirectoryPath(path);
    }
    private List<Delegate?> _delegates =>
        new()
        {
            OnFriendsChange, OnLoadGame, OnFileHeader, OnMarketBuy, OnMarketSell
        };

    
    public EliteJourneyProvider(JourneyFileReader fileReader, IProcessorConfig processorConfig)
    {
        _fileReader = fileReader;
        fileReader.Register(OnNewEventRead);
    }

    private void OnNewEventRead(JourneyEventMessage? eventMessage, string json)
    {
        if (eventMessage is ErrorMessage errorMessage)
        {
            OnReaderError?.Invoke(errorMessage);
            return;
        }

        OnAnyEvent?.Invoke(eventMessage, json);

        if (eventMessage is null)
            return;

        var eventToInvoke = _delegates
            .Where(x => x is not null)
            .FirstOrDefault(x => eventMessage.GetType() == x!.GetType().GenericTypeArguments.First());

        eventToInvoke?.DynamicInvoke(eventMessage);
    }
}