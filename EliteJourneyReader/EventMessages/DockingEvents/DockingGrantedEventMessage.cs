using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EliteJourneyReader.EventMessages.DockingEvents
{
    public sealed class DockingGrantedEventMessage : JourneyEventMessage
    {

        public int LandingPad { get; set; }
        [JsonProperty("MarketID")]
        public ulong MarketId { get; set; }
        public string StationName { get; set; } = string.Empty;
        public string StationType { get; set; } = string.Empty;

        public override string EventTypeName => "DockingGranted";

    }
}
