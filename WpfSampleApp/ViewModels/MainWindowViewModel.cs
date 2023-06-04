using System;
using System.Collections.ObjectModel;
using System.Windows.Automation.Provider;
using System.Windows.Threading;
using EliteJourneyReader.EventMessages;
using WpfSampleApp.Controls;

namespace WpfSampleApp.ViewModels;

public record MainWindowViewModel(string Title)
{
    public string Title { get; set; } = Title;
    public ObservableCollection<LogMessage> InGameMessages { get; set; } = new();
    public ObservableCollection<string> ErrorMessages { get; set; } = new();

    public void AddEventMessage(JourneyEventMessage? message, string jsonMessage)
    {
        InGameMessages.Insert(0, 
            message is null 
                ? new LogMessage("Unknown", DateTimeOffset.Now, jsonMessage) 
                : new LogMessage(message.EventType, message.Timestamp, jsonMessage)
        );
    }
    public void AddErrorMessage(string jsonMessage)
    {
        ErrorMessages.Insert(0, jsonMessage);
    }
}