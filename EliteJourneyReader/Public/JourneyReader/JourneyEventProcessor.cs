using System.Runtime.Serialization;
using EliteJourneyReader.Public.EventMessages;
using Newtonsoft.Json;

namespace EliteJourneyReader.Public.JourneyReader;

public class ProcessorConfig : IProcessorConfig
{
    public Dictionary<string, Type> ConfigDictionary => new()
    {
        {"Friends", typeof(FriendsEventMessage)},
        {"LoadGame", typeof(LoadGameEventMessage)},
        {"FileHeader", typeof(FileHeaderEventMessage)},
        {"MarketBuy", typeof(MarketBuyEventMessage)},
        {"MarketSell", typeof(MarketSellEventMessage)}
    };
}

public interface IProcessorConfig
{
    Dictionary<string, Type> ConfigDictionary { get; }
}

public class JourneyEventProcessor
{
    private readonly IProcessorConfig _processorConfig;
    private DateTimeOffset LastEventTime { get; set; } = DateTimeOffset.MinValue;
    private int _startIndex;
    private string _lastJson = string.Empty;

    public JourneyEventProcessor(IProcessorConfig processorConfig)
    {
        _processorConfig = processorConfig;
    }
    internal IEnumerable<(JourneyEventMessage?, string)> ProcessMessages(string[] events)
    {
        var newEvents = events[_startIndex..];
        var msgJson = string.Empty;
        foreach (var eventJson in newEvents)
        {
            JourneyEventMessage? eventMessage;
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
                _processorConfig.ConfigDictionary.TryGetValue(msgBase.EventType, out var eventType);

                if (eventType is not null)
                    eventMessage = (JourneyEventMessage) JsonConvert.DeserializeObject(eventJson, eventType)!;
                else
                    eventMessage = msgBase;
            }
            catch (Exception e)
            {
                eventMessage = new ErrorMessage(eventJson, e);
            }
            
            yield return (eventMessage, msgJson);
        }
    }
}
