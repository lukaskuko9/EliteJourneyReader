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
        public List<Dictionary<string,string>> Contributions { get; set; } = new List<Dictionary<string,string>>();
        // Schema is [ { "Name":"$commodity_name", "Name_Localised":"Commodity", "Amount":123 }, etc]
        // This implementation is not ideal as you will have to cast the number from string to int

        public override string EventTypeName => "ColonisationContribution";
    }
}
