using Newtonsoft.Json;

namespace EliteJourneyReader.DI;

public record JourneyReaderOptions
{
    /// <summary>
    /// Json serializer settings for deserializing message
    /// </summary>
    public JsonSerializerSettings JsonSerializerSettings = default!;

    /// <summary>
    /// Starts processing messages on startup immediately.
    /// Default is true.
    /// If you don't want to start processing messages right away, set this to false. 
    /// </summary>
    public bool AutoStartProcessingMessages { get; set; }
}