namespace EliteJourneyReader.Public.EventMessages;

public sealed class FileHeaderEventMessage : JourneyEventMessage
{
    public int Part { get; set; }    
    
    public string Language { get; set; } = string.Empty;
    public bool Oddysey { get; set; }
    public string GameVersion { get; set; } = string.Empty;
    public string Build { get; set; } = string.Empty;
}