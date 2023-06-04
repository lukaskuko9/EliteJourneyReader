namespace EliteJourneyReader.JourneyReader;

public interface IJourneyEventProcessor
{
    public event EventProcessed? OnNewEventProcessed;
}