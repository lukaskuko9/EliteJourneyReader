using System.Reflection;
using System.Runtime.CompilerServices;
using EliteJourneyReader.JourneyReader;
using EliteJourneyReader.Public.EventMessages;

[assembly:InternalsVisibleTo("EliteJourneyReaderTests")]
namespace EliteJourneyReader.Public.EliteJourneyProvider;

public partial class EliteJourneyProvider
{
    private readonly JourneyFileReader _fileReader;

    public delegate void JourneyEventDelegate(JourneyEventMessage message, string jsonMessage);
    public delegate void JourneyErrorEventDelegate(string jsonMessage);
    
    internal delegate void CallEventDelegate(JourneyEventMessage? message, string json);

    public EliteJourneyProvider()
    {
        _fileReader = JourneyFileReader.Instance;
        StartReadingMessages();
    }

    #region Public
    
    public void SetJournalDirectoryPath(string path)
    {
        _fileReader.SetNewJournalDirectoryPath(path);
    }

    public void EnableReading(bool enable)
    {
        if(enable)
            StartReadingMessages();
        else
            StopReadingMessages();
    }
    
    #endregion

    #region Private

    private void StartReadingMessages()
    {
        _fileReader.Register(OnNewEventRead);
    }

    private void StopReadingMessages()
    {
        _fileReader.Unregister();
    }

    private void OnNewEventRead(JourneyEventMessage? eventMessage, string json)
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
        //if base JourneyEventMessage comes here, we don't have the model for the message,
        //therefore we don't have specific event to trigger either
        if (eventMessage.GetType() == typeof(JourneyEventMessage))
            return null;
        
        //get and later invoke proper event dynamically using reflection
        //because there will be ton of them and I am lazy to do the mapping everytime
        //if this causes performance issues, optimize or do it manually!
        var eventDelegate = GetType()
            .GetFields(BindingFlags.Instance | BindingFlags.NonPublic)
            .First(x => x.FieldType.GenericTypeArguments.Contains(eventMessage.GetType()));
        var eventToInvoke = (Delegate?)eventDelegate.GetValue(this);
        return eventToInvoke;
    }

    #endregion
    
}