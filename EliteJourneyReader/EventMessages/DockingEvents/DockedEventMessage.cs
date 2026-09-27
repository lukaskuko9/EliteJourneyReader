using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EliteJourneyReader.EventMessages.DockingEvents
{
    public sealed class DockedEventMessage : JourneyEventMessage
    {

        public string StationName { get; set; } = string.Empty;
        public string StationType { get; set; } = string.Empty;
        public bool Taxi { get; set; }
        public bool Multicrew { get; set; }
        public string StarSystem { get; set; } = string.Empty;
        public ulong SystemAddress { get; set; }
        [JsonProperty("MarketID")]
        public ulong MarketId { get; set; }
        public Dictionary<string,string> StationFaction { get; set; } = new Dictionary<string,string>();
        public string StationGovernment { get; set; } = string.Empty;
        [JsonProperty("StationGovernment_Localised")]
        public string StationGovernmentLocalised { get; set; } = string.Empty;
        public List<string> StationServices { get; set; } = new List<string>();
        public string StationEconomy {  get; set; } = string.Empty;
        [JsonProperty("StationEconomy_Localised")]
        public string StationEconomyLocalised {  get; set; } = string.Empty;
        public List<Dictionary<string, string>> StationEconomies { get; set; } = new List<Dictionary<string, string>>();
        [JsonProperty("DistFromStarLS")]
        public decimal DistFromStarLs { get; set; }
        public Dictionary<string, int> LandingPads { get; set; } = new Dictionary<string, int>();

        public override string EventTypeName => "Docked";

    }
}
