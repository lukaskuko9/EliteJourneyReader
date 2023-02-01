using Newtonsoft.Json;

namespace EliteJourneyReader.Journey.EventMessages;

public class JourneyEventMessage
{
    [JsonProperty("timestamp")] public DateTimeOffset Timestamp { get; set; }
    [JsonProperty("event")] public string EventType { get; set; }

}