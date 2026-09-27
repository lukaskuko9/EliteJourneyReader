using EliteJourneyReader.EliteJourneyProvider;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EliteJourneyReader.EventMessages.DockingEvents
{
    public sealed class DockingRequestedEventMessage : JourneyEventMessage
    {
        [JsonProperty("MarketID")]
        public ulong MarketId { get; set; }
        public string StationName { get; set; } = string.Empty;
        public string StationType { get; set; } = string.Empty;
        public Dictionary<string, int> LandingPads { get; set; } = new Dictionary<string, int>();

        public override string EventTypeName => "DockingRequested";
    }
}
