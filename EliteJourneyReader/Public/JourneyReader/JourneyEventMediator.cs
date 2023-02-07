using System.Runtime.Serialization;
using EliteJourneyReader.Public.EventMessages;
using Newtonsoft.Json;

namespace EliteJourneyReader.Public.JourneyReader;

public class JourneyEventMediator
{
    private DateTimeOffset LastEventTime { get; set; } = DateTimeOffset.MinValue;
    private int _startIndex;
    private string _lastJson = string.Empty;

    public JourneyEventMediator()
    {

    }

    internal IEnumerable<(JourneyEventMessage?, string)> ProcessMessages(string[] events,
        Dictionary<string, Type> typesDictionary)
    {
        var newEvents = events[_startIndex..];
        var msgJson = string.Empty;
        foreach (var eventJson in newEvents)
        {
            JourneyEventMessage? eventMessage = null;
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
                typesDictionary.TryGetValue(msgBase.EventType, out var eventType);

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
