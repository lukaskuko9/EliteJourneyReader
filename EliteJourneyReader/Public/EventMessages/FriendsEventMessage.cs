using Newtonsoft.Json;

namespace EliteJourneyReader.Public.EventMessages;

public sealed class FriendsEventMessage : JourneyEventMessage
{
    [JsonProperty("status")]
    public string Status { get; set; } = string.Empty;
    
    [JsonProperty("name")]
    public string FriendName { get; set; } = string.Empty;

    public override string EventTypeName => "Friends";
}