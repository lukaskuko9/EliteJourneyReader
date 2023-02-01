using System.Runtime.Serialization;
using EliteJourneyReader.Journey.EventMessages;
using Newtonsoft.Json;

namespace EliteJourneyReader.Journey.Mediator;

public class JourneyEventMediator
{
    private DateTimeOffset LastEventTime { get; set; } = DateTimeOffset.MinValue;
    private int _startIndex = 0;
    private string _lastJson = string.Empty;
    
    private static readonly Dictionary<string, Type> TypesDictionary = new()
    {
        { "Friends", typeof(FriendsEventMessage) },
        { "FileHeader", typeof(FileHeaderEventMessage) },
        { "LoadGame", typeof(LoadGameEventMessage) },
        { "MarketBuy", typeof(MarketBuyEventMessage) },
        { "MarketSell", typeof(MarketSellEventMessage) }
    };
    
    public JourneyEventMediator()
    {

    }

    public IEnumerable<(JourneyEventMessage?, string)> ProcessMessages(string[] events)
    {
        var newEvents = events[_startIndex..];
        JourneyEventMessage? eventMessage = null;
        string msgJson = string.Empty;
        foreach (var eventJson in newEvents)
        {
            try
            {
                var msgBase = JsonConvert.DeserializeObject<JourneyEventMessage>(eventJson);
                if (msgBase is null)            
                    throw new SerializationException($"Cannot serialize json {eventJson} to message");

                if (LastEventTime > msgBase.Timestamp && _lastJson != eventJson)
                    continue;

                msgJson = eventJson;
                LastEventTime = msgBase.Timestamp;
                _startIndex = events.Length;
                _lastJson = eventJson;
                var eventType = TypesDictionary[msgBase.EventType];
                eventMessage = (JourneyEventMessage)JsonConvert.DeserializeObject(eventJson, eventType)!;
            }
            catch (Exception e)
            {
                // ignored
            }

            yield return (eventMessage, msgJson);
        }
    }


}
