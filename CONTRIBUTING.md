## Adding new events
You can read about various events at [Elite Journal](https://elite-journal.readthedocs.io/en/latest/). 

### Creating integration for new event
After verifying the event is not already implemented (or in pull requests), start by creating new model in `EliteJourneyReader/EventMessages` for this event type with all it's properties from **Elite Journal docs**:
```cs 
public sealed class FriendsEventMessage : JourneyEventMessage
{
    [JsonProperty("status")]
    public string Status { get; set; } = string.Empty;
    
    [JsonProperty("name")]
    public string FriendName { get; set; } = string.Empty;

    public override string EventTypeName => "Friends";
}
```
`EventTypeName` override here is used to indicate when the event `OnFriendsChange` described below will be triggered. For this to work, it has to be the same value that is received in json message from journal file in `event` field! 

If you think it's needed, you can put in also [documentation comments](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/language-specification/documentation-comments/) for the properties.

Add the new `event` to interface `IEliteJourneyProvider` and add [documentation comments](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/language-specification/documentation-comments/) similar to how it's done with other event types:
```cs
/// <summary>
/// Triggered when receiving information about a change in a friend's status
/// </summary>
public event EventHandler<FriendsEventMessage>? OnFriendsChange;
```

After adding the `event` to interface, add it also to it's inheritor `EliteJourneyProvider` in `EliteJourneyProvider.cs` file:
```cs
/// <summary>
/// Triggered when receiving information about a change in a friend's status
/// </summary>
public event EventHandler<FriendsEventMessage>? OnFriendsChange;
```
### Testing new event type
The best way to test your changes is by running **WpfSampleApp** and test it live in-game by launching Elite Dangerous. Before that though, there are some changes you should do.
In `WpfSampleApp/Views/MainWindow.cs`, add new subscriber for your new event type in constructor. In this case, since we implemented `OnFriendsChange`, the code will look like this:
```cs
public MainWindow(IEliteJourneyProvider eliteJourneyProvider, IOptions<TestOptions> testOptions)
{
  InitializeComponent();
  eliteJourneyProvider.OnFriendsChange += EliteJourneyProviderOnFriendsChange; //add this line to subscribe to new event type
}
```
Add also the method `EliteJourneyProviderOnFriendsChange` in the same file, example:
```cs
private void EliteJourneyProviderOnFriendsChange(object? sender, FriendsEventMessage friendsChangeMessage)
{
    ///make sure this gets hit when you receive 
    Console.WriteLine($"Your friend {friendsChangeMessage.FriendName} is {friendsChangeMessage.Status}!");
}
```

Then by running the `WpfSampleApp` and launching `EliteDangerous`, you can trigger the in-game event, and see if it works with the `WpfSampleApp`. 
If you use the console output like in the above method, watch the console. You can set a breakpoint in your method so that the app will pause when it hits the breakpoint and you will immediately notice it. This is even better, because you can see that all properties gets filled with values! 

If it works, you can create a pull request to `develop` branch. 

If it doesn't work, feel free to contact me in case you don't know what's going on wrong in your changes.

