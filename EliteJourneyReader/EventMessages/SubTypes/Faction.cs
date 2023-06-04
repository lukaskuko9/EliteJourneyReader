using Newtonsoft.Json;

namespace EliteJourneyReader.EventMessages.SubTypes;

public class Faction
{
    public string Name { get; set; } = default!;
    public string FactionState { get; set; } = default!;
    public string Government { get; set; } = default!;
    public decimal Influence { get; set; }
    public string Allegiance { get; set; } = default!;
    public string Happiness { get; set; } = default!;
    
    [JsonProperty("Happiness_Localised")]
    public string HappinessLocalised { get; set; } = default!;
    
    public decimal MyReputation { get; set; }

}