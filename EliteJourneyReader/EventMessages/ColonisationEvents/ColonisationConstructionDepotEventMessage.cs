using EliteJourneyReader.EventMessages.SubTypes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EliteJourneyReader.EventMessages.ColonisationEvents
{
    public sealed class ColonisationConstructionDepotEventMessage : JourneyEventMessage
    {

        [JsonProperty("MarketID")]
        public ulong MarketId { get; set; }
        public decimal ConstructionProgress { get; set; }
        public bool ConstructionComplete { get; set; }
        public bool ConstructionFailed { get; set; }
        public List<ColonisationConstructionDepotCommodity> ResourcesRequired { get; set; } = new List<ColonisationConstructionDepotCommodity>();

        public override string EventTypeName => "ColonisationConstructionDepot";
    }
}
