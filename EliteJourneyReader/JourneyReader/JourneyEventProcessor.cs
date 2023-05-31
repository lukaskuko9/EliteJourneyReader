using System.Runtime.Serialization;
using EliteJourneyReader.Public.EventMessages;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace EliteJourneyReader.JourneyReader;

internal class JourneyEventProcessor
{
    private DateTimeOffset LastEventTime { get; set; } = DateTimeOffset.MinValue;
    private string _lastJson = string.Empty;
    private readonly Dictionary<string, Type> _configDictionary;

    public JourneyEventProcessor(IEnumerable<IEventMessage> messageTypes)
    {
        //build dictionary of expected string literals with their types
        _configDictionary = messageTypes
            .Select(x => (x.EventTypeName, EventType: x.GetType()))
            .ToDictionary(x => x.EventTypeName, x => x.EventType);
    }
    internal IEnumerable<(JourneyEventMessage?, string)> ProcessMessages(string[] events)
    {
        var newMessages = events.Select(eventJson =>
            {
                try
                {
                    var parsedObject = JObject.Parse(eventJson);
                    
                    //error here
                    var timeStr = parsedObject["timestamp"]!.ToString();
                    
                    var timeStamp = DateTimeOffset.Parse(timeStr).LocalDateTime;
                
                    if (LastEventTime > timeStamp || _lastJson == eventJson)
                        return ((JourneyEventMessage?, string)?)null;

                    var eventType = parsedObject["event"]!.ToString();
                    var isPresent = _configDictionary.ContainsKey(eventType);

                    JourneyEventMessage msgBase;
                    
                    //if we have the type for this specific event, deserialize to that type,
                    //if not deserialize to general message
                    if (isPresent)
                        msgBase = (JsonConvert.DeserializeObject(eventJson, _configDictionary[eventType]) as JourneyEventMessage)!;
                    else
                        msgBase = JsonConvert.DeserializeObject<JourneyEventMessage>(eventJson)!;


                    LastEventTime = timeStamp;
                    _lastJson = eventJson;
                    return (msgBase, eventJson);
                }
                catch (Exception)
                {
                    return (null, eventJson);
                }
            })
            .Where(x => x is not null)
            .Select(x => x!.Value)
            .ToList();
        
        return newMessages;
    }
}
