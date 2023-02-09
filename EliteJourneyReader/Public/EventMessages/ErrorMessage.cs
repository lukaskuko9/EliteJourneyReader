namespace EliteJourneyReader.Public.EventMessages;

public class ErrorMessage : JourneyEventMessage
{
    public readonly string Json;
    public readonly Exception Exception;

    public ErrorMessage(string json, Exception exception)
    {
        Json = json;
        Exception = exception;
    }
}