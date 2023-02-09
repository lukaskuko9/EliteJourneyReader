using System;
using System.Collections.ObjectModel;
using System.Windows.Automation.Provider;
using System.Windows.Threading;
using EliteJourneyReader.Public.EventMessages;
using WpfSampleApp.Controls;

namespace WpfSampleApp.ViewModels;

public record MainWindowViewModel(string Title)
{
    public string Title { get; set; } = Title;
    public ObservableCollection<LogMessage> InGameMessages { get; set; } = new();
    public ObservableCollection<LogErrorMessage> ErrorMessages { get; set; } = new();
    public void AddEventMessage(JourneyEventMessage? message, string jsonMessage)
    {
        //message should not be null, unless there is uncaught exception
        //if message is null though, we still send json of message
        InGameMessages.Insert(0, 
            message is null 
                ? new LogMessage("Unknown", DateTimeOffset.Now, jsonMessage) 
                : new LogMessage(message.EventType, message.Timestamp, jsonMessage)
        );
    }
    
    public void AddErrorMessage(ErrorMessage message)
    {
        ErrorMessages.Insert(0, new LogErrorMessage(message.Exception, message.Timestamp, message.Json));
    }
}