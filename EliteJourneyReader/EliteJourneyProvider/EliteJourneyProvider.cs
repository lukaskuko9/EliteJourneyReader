using System.Reflection;
using EliteJourneyReader.EventMessages;
using EliteJourneyReader.JourneyReader;

namespace EliteJourneyReader.EliteJourneyProvider;

public partial class EliteJourneyProvider : IEliteJourneyProvider
{
    private readonly IJourneyFileReader _reader;
    
    public EliteJourneyProvider(IJourneyEventProcessor processor, IJourneyFileReader reader)
    {
        _reader = reader;
        processor.OnNewEventProcessed += RaiseProperEvents;
    }

    #region Implementations
    
    public void SetJournalDirectoryPath(Uri uri)
    {
        _reader.SetNewJournalDirectoryPath(uri.AbsolutePath);
    }
    
    public void SetJournalDirectoryPath(string path)
    {
        _reader.SetNewJournalDirectoryPath(path);
    }

    public void EnableReading(bool enable)
    {
        _reader.EnableReading(enable);
    }
    
    #endregion

    #region Private
    
    private void RaiseProperEvents(JourneyEventMessage? eventMessage, string json)
    {
        if (eventMessage is null)
        {
            OnReaderError?.Invoke(json);
            return;
        }

        OnAnyEvent?.Invoke(eventMessage, json);
        
        var eventToInvoke = GetEventDelegate(eventMessage);
        eventToInvoke?.DynamicInvoke(this, eventMessage);
    }

    private Delegate? GetEventDelegate(JourneyEventMessage eventMessage)
    {
        //get and later invoke proper event dynamically using reflection
        //because there will be ton of them and I am lazy to do the mapping everytime
        //if this causes performance issues, optimize or do it manually!
        //if we dont find delegate here,it means that type of eventMessage is base JourneyEventMessage -> meaning we don't have types for this message and there is no event to trigger
        var eventDelegate = GetType()
            .GetFields(BindingFlags.Instance | BindingFlags.NonPublic)
            .FirstOrDefault(x => x.FieldType.GenericTypeArguments.Contains(eventMessage.GetType()));
        var eventToInvoke = (Delegate?)eventDelegate?.GetValue(this);
        return eventToInvoke;
    }

    #endregion
    
}