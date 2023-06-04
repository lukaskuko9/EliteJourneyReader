namespace EliteJourneyReader.EventMessages;

public sealed class LoadGameEventMessage : JourneyEventMessage
{
    public string Commander { get; set; } = string.Empty;
    public string FID { get; set; } = string.Empty;
    public bool Horizons { get; set; }
    public string Ship { get; set; } = string.Empty;
    public long ShipId { get; set; }
    public string ShipName { get; set; } = string.Empty;
    public string ShipIdent { get; set; } = string.Empty;
    public decimal FuelLevel { get; set; }
    public decimal FuelCapacity { get; set; }
    public string GameMode { get; set; } = string.Empty;
    public string Credits { get; set; } = string.Empty;
    public string Loan { get; set; } = string.Empty;

    public override string EventTypeName => "LoadGame";
}