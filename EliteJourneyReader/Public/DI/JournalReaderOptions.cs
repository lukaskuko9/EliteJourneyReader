using Newtonsoft.Json;

namespace EliteJourneyReader.Public.DI;

public record JournalReaderOptions
{
    public JsonSerializerSettings ? JsonSerializerSettings;
}