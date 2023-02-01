namespace EliteJourneyReader.Journey.EventMessages;

public sealed class LoadGameEventMessage : JourneyEventMessage
{
    public string Commander { get; set; }
    public string FID { get; set; }
    public bool Horizons { get; set; }
    public string Ship { get; set; }
    public long ShipId { get; set; }
    public string ShipName { get; set; }
    public string ShipIdent { get; set; }
    public decimal FuelLevel { get; set; }
    public int FuelCapacity { get; set; }
    public string GameMode { get; set; }
    public string Credits { get; set; }
    public string Loan { get; set; }
}