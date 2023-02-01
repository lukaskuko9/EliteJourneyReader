using Newtonsoft.Json;

namespace EliteJourneyReader.Journey.EventMessages;

public sealed class FriendsEventMessage : JourneyEventMessage
{
    [JsonProperty("status")]
    public string Status { get; set; }    
    
    [JsonProperty("name")]
    public string FriendName { get; set; }
}