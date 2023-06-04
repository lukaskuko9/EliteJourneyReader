using Newtonsoft.Json;

namespace EliteJourneyReader.EventMessages;

public class FssSignalDiscoveredEventMessage : JourneyEventMessage
{
    public string SystemAddress { get; set; } = default!;
    public string SignalName { get; set; } = default!;
    
    [JsonProperty("SignalName_Localised")]
    public string? SignalNameLocalised { get; set; }

    [JsonProperty("IsStation")]
    internal bool? IsStationNullable;
    
    [JsonIgnore]
    public bool IsStation => IsStationNullable ?? false;
    
    [JsonProperty("USSType")]
    public string? UssType;  
    
    [JsonProperty("USSType_Localised")]
    public string? UssTypeLocalised;

    [JsonProperty("SpawningFaction_Localised")]
    public string? SpawningFactionLocalised;
    
    [JsonProperty("SpawningState_Localised")]
    public string? SpawningStateLocalised;
    
    public string? SpawningState;
    public string? SpawningFaction;
    public uint? ThreatLevel;
    public decimal? TimeRemaining;
    
    
    public override string EventTypeName => "FSSSignalDiscovered";
}