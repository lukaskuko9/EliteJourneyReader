using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EliteJourneyReader.EventMessages.SubTypes
{
    public class ColonisationConstructionDepotCommodity
    {

        public string Name { get; set; } = string.Empty;
        [JsonProperty("Name_Localised")]
        public string NameLocalised { get; set; } = string.Empty;
        public int RequiredAmount { get; set; }
        public int ProvidedAmount { get; set; }
        public int Payment { get; set; }
    }
}
