using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EliteJourneyReader.EventMessages.DockingEvents
{
    public sealed class UndockedEventMessage : JourneyEventMessage
    {

        public string StationName { get; set; } = string.Empty;
        public string StationType { get; set; } = string.Empty;
        [JsonProperty("MarketID")]
        public ulong MarketId { get; set; }
        public bool Taxi { get; set; }
        public bool Multicrew { get; set; }

        public override string EventTypeName => "Undocked";
    }
}
