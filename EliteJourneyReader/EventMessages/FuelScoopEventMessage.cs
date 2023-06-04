namespace EliteJourneyReader.EventMessages;

public class FuelScoopEventMessage : JourneyEventMessage
{
    public override string EventTypeName => "FuelScoop";
    
    public decimal Scooped { get; set; }
    public decimal Total { get; set; }
}
