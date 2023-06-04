namespace EliteJourneyReader.JourneyReader;

public delegate void JourneyEventDelegate(string[] jsonLines);
public interface IJourneyFileReader
{
    event JourneyEventDelegate OnNewEventRegistered;
    void EnableReading(bool enable);
    void SetNewJournalDirectoryPath(string path);
}