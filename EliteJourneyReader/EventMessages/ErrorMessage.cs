namespace EliteJourneyReader.EventMessages;

public class ErrorMessage
{
    public readonly string Json;
    public readonly Exception Exception;

    public ErrorMessage(string json, Exception exception)
    {
        Json = json;
        Exception = exception;
    }

   // public override string EventTypeName => string.Empty;
}