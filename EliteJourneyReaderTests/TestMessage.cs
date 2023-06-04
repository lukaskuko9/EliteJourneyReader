using EliteJourneyReader.EventMessages;

namespace EliteJourneyReaderTests;

public class TestMessage : JourneyEventMessage
{
    public override string EventTypeName => "TestMessageType";
}