using System.Runtime.Serialization;
using EliteJourneyReader.Public.DI;
using EliteJourneyReader.Public.EventMessages;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace EliteJourneyReader.JourneyReader;

internal class JourneyEventProcessor
{
    private DateTimeOffset LastEventTime { get; set; } = DateTimeOffset.MinValue;
    private string _lastJson = string.Empty;
    private readonly Dictionary<string, Type> _configDictionary;

    private readonly JournalReaderOptions _options = new()
    {
        JsonSerializerSettings = new JsonSerializerSettings()
        {
            DateParseHandling = DateParseHandling.DateTimeOffset,
            DateTimeZoneHandling = DateTimeZoneHandling.Local
        }
    };

    public JourneyEventProcessor(IEnumerable<IEventMessage> messageTypes)
    {
        //build dictionary of expected string literals with their types
        _configDictionary = messageTypes
            .Select(x => (x.EventTypeName, EventType: x.GetType()))
            .ToDictionary(x => x.EventTypeName, x => x.EventType);
    }

    internal void SetJournalReaderOptions(JournalReaderOptions options)
    {
        _options.JsonSerializerSettings = options.JsonSerializerSettings;
    }
    
    internal IEnumerable<(JourneyEventMessage?, string)> ProcessMessages(string[] events)
    {
        var newMessages = events
            .Select(DeserializeMessage)
            .Where(x => x is not null)
            .Select(x => x!.Value)
            .ToList();
        
        return newMessages;
    }

    private (JourneyEventMessage?, string)? DeserializeMessage(string eventJson)
    {
        try
        {
            var parsedObject = JObject.Parse(eventJson);
            var timeStamp = parsedObject["timestamp"]!.Value<DateTime>();

            //if message we already have
            if (LastEventTime > timeStamp || _lastJson == eventJson)
                return null;

            var eventType = parsedObject["event"]!.ToString();
            var isPresent = _configDictionary.ContainsKey(eventType);

            JourneyEventMessage msgBase;
                    
            //if we have the type for this specific event, deserialize to that type,
            //if not deserialize to general message
            if (isPresent)
                msgBase = (JsonConvert.DeserializeObject(eventJson, _configDictionary[eventType], _options.JsonSerializerSettings) as JourneyEventMessage)!;
            else
                msgBase = JsonConvert.DeserializeObject<JourneyEventMessage>(eventJson, _options.JsonSerializerSettings)!;


            LastEventTime = timeStamp;
            _lastJson = eventJson;
            return (msgBase, eventJson);
        }
        catch (Exception)
        {
            return (null, eventJson);
        }
    }
}
