using Newtonsoft.Json;

namespace EliteJourneyReader.EventMessages;

public class ScanEventMessage : JourneyEventMessage
{
    public override string EventTypeName => "Scan";
    
    public string ScanType { get; set; } = default!;
    public string BodyName { get; set; } = default!;
    
    [JsonProperty("BodyID")]
    public string BodyId { get; set; } = default!;

    public string StarSystem { get; set; } = default!;
    public string SystemAddress { get; set; } = default!;
    public string? StarType { get; set; } = default!;
    public string? Luminosity { get; set; } = default!;
    public int Subclass { get; set; } = default!;
    public decimal StellarMass { get; set; } = default!;
    public decimal Radius { get; set; } = default!;
    public decimal AbsoluteMagnitude { get; set; } = default!;
    public decimal RotationPeriod { get; set; } = default!;
    public decimal AxialTilt { get; set; } = default!;
    public bool WasDiscovered { get; set; } = default!;
    public bool WasMapped { get; set; } = default!;
    
    /// <summary>
    /// Age in million years
    /// </summary>
    [JsonProperty("Age_MY")]
    public long AgeInMillionYears { get; set; }
    
    [JsonProperty("DistanceFromArrivalLS")]
    public decimal DistanceFromArrivalLs { get; set; }
}