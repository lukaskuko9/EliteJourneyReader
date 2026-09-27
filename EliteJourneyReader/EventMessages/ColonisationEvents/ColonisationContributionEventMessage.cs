using EliteJourneyReader.EventMessages.SubTypes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EliteJourneyReader.EventMessages.ColonisationEvents
{
    public sealed class ColonisationContributionEventMessage : JourneyEventMessage
    {

        [JsonProperty("MarketID")]
        public ulong MarketId { get; set; }
        public List<ColonisationContributionCommodity> Contributions { get; set; } = new List<ColonisationContributionCommodity>();

        public override string EventTypeName => "ColonisationContribution";
    }
}
