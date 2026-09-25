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
        public List<Dictionary<string,string>> ResourcesRequired { get; set; } = new List<Dictionary<string,string>>();
        // Schema is [ { "Name":"$commodity_name", "Name_Localised":"Commodity", "RequiredAmount":123, "ProvidedAmount":456, "Payment":789 }, etc]
        // This implementation is not ideal as you will have to cast the numbers from string to int

        public override string EventTypeName => "ColonisationConstructionDepot";
    }
}
