# EliteJourneyReader
EliteJourneyReader is a library for game Elite Dangerous written in .Net for interacting with in-game events with easy-to-use interface and customizable options.

## How does it work? 
When an event happens in-game, a file in `%userprofile%\Saved Games\Frontier Developments\Elite Dangerous\` is appended (or new file is created) with [json](https://json-schema.org/learn/getting-started-step-by-step) message of this in-game event along with event description (json properties).
Each in-game event is unique, has (mostly) different properties and is triggered by different actions. 

EliteJourneyReader is capable of listening to these file changes, processing new in-game messages and exposing all necessary for you to easily react upon it.

Even without having supported all in-game event messages, EliteJourneyReader is fully capable of reacting to all messages, supported or not.

This is supported with `OnAnyEvent` event. This event is raised every time an in-game event is triggered. 
From here, you will get the base properties that every message has, like what type of event was triggered and when it was triggered,
along with json value of message that raised the event.

## Code sample
In order for EliteJourneyReader to work, you need to register it's services through DI first:
```C#
services.ConfigureJourneyReader();
```

You can set options to your liking, for example to disable default reading of messages right from application start:

```C#
services.ConfigureJourneyReader(options =>
{
    options.AutoStartProcessingMessages = false;
});
```
If you are not familiar with DI design pattern, it is explained in official microsoft documentation for [.NET dependency injection](https://learn.microsoft.com/en-us/dotnet/core/extensions/dependency-injection).
You can also check how it's done in <c>WpfSampleApp</c> in this solution. 

### Reacting to event not directly supported by EliteJourneyReader:

```C#
public MainWindow(IEliteJourneyProvider eliteJourneyProvider)
{
    eliteJourneyProvider.OnAnyEvent += EliteJourneyProviderOnAnyEvent;
}

private void EliteJourneyProviderOnAnyEvent(JourneyEventMessage message, string jsonMessage)
{
    if (message.EventType == "ReceiveText")
    {
        //do something
    }
}
```

Even though not much is given to us from ``message`` argument, we get the full ``jsonMessage``:
```json
{ 
  "timestamp":"2023-05-30T17:17:56Z", 
  "event":"ReceiveText", 
  "From":"", 
  "Message":"$COMMS_entered:#name=HIP 64726;", 
  "Message_Localised":"Entered Channel: HIP 64726", 
  "Channel":"npc"
}
```

We can then deserialize ``jsonMessage`` e.g. using [Newtonsoft json](https://www.newtonsoft.com/json):
```C#
var message = JsonConvert.DeserializeObject<ReceiveTextMessage>(jsonMessage);
```

Once you have deserialized the message, it's only up to you what to do!

### Reacting to supported event
For supported events it's even easier - we have all the types and even handled in EliteJourneyReader.

```C#
public MainWindow(IEliteJourneyProvider eliteJourneyProvider)
{
    eliteJourneyProvider.OnFriendsChange += OnFriendsChange;
}

private void OnFriendsChange(object? sender, FriendsEventMessage friendsChangeMessage)
{
    //"Your friend {username} is {online/offline/...}"
    Console.WriteLine($"Your friend {friendsChangeMessage.FriendName} is {friendsChangeMessage.Status}!");
}
```
