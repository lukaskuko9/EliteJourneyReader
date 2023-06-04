using Newtonsoft.Json;

namespace EliteJourneyReader.EventMessages;

public class JourneyEventMessage : IEventMessage
{
    [JsonProperty("timestamp")] public DateTimeOffset Timestamp { get; set; }
    [JsonProperty("event")] public string EventType { get; set; } = string.Empty;


    public virtual string EventTypeName { get; } = string.Empty;
}

public interface IEventMessage
{
    string EventTypeName { get; }
}