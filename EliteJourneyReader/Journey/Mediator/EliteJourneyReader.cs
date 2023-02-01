using System.Text;
using EliteJourneyReader.Journey.EventMessages;

namespace EliteJourneyReader.Journey.Mediator;

public class EliteJourneyReader
{
    private readonly JourneyEventMediator _mediator;

    private static string JourneyDirectoryPath => Environment.ExpandEnvironmentVariables(RawDirectoryPath);
    private const string RawDirectoryPath = "%userprofile%\\Saved Games\\Frontier Developments\\Elite Dangerous\\";

    public delegate void JourneyEventDelegate<in TMessage>(TMessage message) where TMessage : JourneyEventMessage;
    public delegate void JourneyEventDelegate(string jsonMessage);


    public event JourneyEventDelegate? OnAnyEvent;
    public event JourneyEventDelegate<FriendsEventMessage>? OnFriendsChange;
    public event JourneyEventDelegate<LoadGameEventMessage>? OnLoadGame;
    public event JourneyEventDelegate<FileHeaderEventMessage>? OnFileHeader;
    public event JourneyEventDelegate<MarketBuyEventMessage>? OnMarketBuy;
    public event JourneyEventDelegate<MarketSellEventMessage>? OnMarketSell;

    
    public EliteJourneyReader(JourneyEventMediator mediator)
    {
        _mediator = mediator;
        FileSystemWatcher = new FileSystemWatcher(JourneyDirectoryPath);
        FileSystemWatcher.EnableRaisingEvents = true;
        FileSystemWatcher.Filter = "*.log";
        FileSystemWatcher.Created += WatcherOnCreated;
        FileSystemWatcher.Changed += WatcherOnChanged;
    }

    private FileSystemWatcher FileSystemWatcher { get; set; }
    
    private void WatcherOnChanged(object sender, FileSystemEventArgs e)
    {

        var events = ReadLines(e.FullPath).ToArray();
        foreach (var eventMessage in _mediator.ProcessMessages(events))
        {
            var (msg, json) = eventMessage;
            CallEvent(msg, json);
        }
    }

    private IEnumerable<string> ReadLines(string path)
    {
        using var fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
        using var sr = new StreamReader(fs, Encoding.Default);
        while (!sr.EndOfStream)
        {
            var s = sr.ReadLine();
            if (s is not null)
                yield return s;
        }
    }
    

    private void WatcherOnCreated(object sender, FileSystemEventArgs e)
    {
        var events = ReadLines(e.FullPath).ToArray();
        foreach (var eventMessage in _mediator.ProcessMessages(events))
        {
            var (msg, json) = eventMessage;
            CallEvent(msg, json);
        }
    }
    
    private void CallEvent(JourneyEventMessage? eventMessage, string json)
    {
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

        OnAnyEvent?.Invoke(json);
    }
}