namespace EliteJourneyReader.JourneyReader;

internal interface IJourneyEventProcessor
{
    public event EventProcessed? OnNewEventProcessed;
}