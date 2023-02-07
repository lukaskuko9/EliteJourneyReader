using Newtonsoft.Json;

namespace EliteJourneyReader.Public.EventMessages;

public class JourneyEventMessage
{
    [JsonProperty("timestamp")] public DateTimeOffset Timestamp { get; set; }
    [JsonProperty("event")] public string EventType { get; set; } = string.Empty;

}