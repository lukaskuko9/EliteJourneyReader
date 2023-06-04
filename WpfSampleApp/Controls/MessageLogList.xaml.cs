using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace WpfSampleApp.Controls;

public record LogMessage(string Event, DateTimeOffset Time, string Json);
public record LogErrorMessage(Exception Exception, DateTimeOffset Time, string Json);
public partial class MessageLogList : UserControl
{
    public static readonly DependencyProperty ItemsDependency =
        DependencyProperty.Register(nameof(Items), 
            typeof(ObservableCollection<LogMessage>), 
            typeof(MessageLogList),
            new FrameworkPropertyMetadata(null)
            );    
    public static readonly DependencyProperty HeaderDependency =
        DependencyProperty.Register(nameof(Header), 
            typeof(string), 
            typeof(MessageLogList),
            new FrameworkPropertyMetadata(null)
            );

    public ObservableCollection<LogMessage> Items
    {
        get => (ObservableCollection<LogMessage>)GetValue(ItemsDependency);
        set => SetValue(ItemsDependency, value);
    }
    public string Header
    {
        get => (string)GetValue(HeaderDependency);
        set => SetValue(HeaderDependency, value);
    }

    public MessageLogList()
    {
        InitializeComponent();
        DockPanel.DataContext = this;
    }
}

