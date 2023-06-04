using EliteJourneyReader.EliteJourneyProvider;
using EliteJourneyReader.EventMessages;
using EliteJourneyReader.JourneyReader;
using Moq;

namespace EliteJourneyReaderTests.Tests;

public class EliteJourneyProviderTests
{
    [Fact]
    public void OnAnyEventShouldBeRaisedOnAnyMessageProcessed()
    {
        //Arrange
        var reader = new Mock<IJourneyFileReader>();
        var processor = new Mock<IJourneyEventProcessor>();
        var provider = new EliteJourneyProvider(processor.Object, reader.Object);
        var message = new TestMessage();
        var eventWasRaised = false;
        const string jsonMessage = "fake json";
        
        //Act
        provider.OnAnyEvent += AssertTest;
        processor.Raise(a => a.OnNewEventProcessed += null, message, jsonMessage);

        
        //Assert
        void AssertTest(JourneyEventMessage msg, string json)
        {
            eventWasRaised = true;
            Assert.True(msg == message);
            Assert.True(jsonMessage == json);
        }
        
        Assert.True(eventWasRaised);
    }
}