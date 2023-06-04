using EliteJourneyReader.EventMessages.SubTypes;
using Newtonsoft.Json;

namespace EliteJourneyReader.EventMessages;

public sealed class FsdJumpEventMessage : JourneyEventMessage
{
    public bool Taxi { get; set; }
    public bool Multicrew { get; set; }
    public string StarSystem { get; set; } = default!;
    public string SystemAddress { get; set; } = default!;
    public List<decimal> StarPos { get; set; } = default!;
    
    public string SystemAllegiance { get; set; } = default!;
    public string SystemEconomy { get; set; } = default!;
    
    [JsonProperty("SystemEconomy_Localised")]
    public string SystemEconomyLocalised { get; set; } = default!;
    
    public string SystemSecondEconomy { get; set; } = default!;    
    
    [JsonProperty("SystemSecondEconomy_Localised")]
    public string SystemSecondEconomyLocalised { get; set; } = default!;
    public string SystemGovernment { get; set; } = default!;    
    
    [JsonProperty("SystemGovernment_Localised")]
    public string SystemGovernmentLocalised { get; set; } = default!;
    
    public string SystemSecurity { get; set; } = default!;
    
    [JsonProperty("SystemSecurity_Localised")]
    public string SystemSecurityLocalised { get; set; } = default!;   
    
    public string Population { get; set; } = default!;
    public string Body { get; set; } = default!;
    
    [JsonProperty("BodyID")]
    public string BodyId { get; set; } = default!;    

    public string BodyType { get; set; } = default!;
    public List<string> Powers { get; set; } = new();
    public string? PowerplayState { get; set; }
    public decimal JumpDist { get; set; }
    public decimal FuelUsed { get; set; }
    public decimal FuelLevel { get; set; }
    public List<Faction> Factions = new();
    public SystemFaction? SystemFaction;


    public override string EventTypeName => "FSDJump";
}