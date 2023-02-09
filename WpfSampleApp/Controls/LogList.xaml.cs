using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using WpfSampleApp.Windows;

namespace WpfSampleApp.Controls;

public record LogMessage(string Event, DateTimeOffset Time, string Json);
public record LogErrorMessage(Exception Exception, DateTimeOffset Time, string Json);
public partial class LogList : UserControl
{
    public static readonly DependencyProperty ItemsDependency =
        DependencyProperty.Register(nameof(Items), 
            typeof(ObservableCollection<LogMessage>), 
            typeof(LogList),
            new FrameworkPropertyMetadata(null)
            );    
    public static readonly DependencyProperty HeaderDependency =
        DependencyProperty.Register(nameof(Header), 
            typeof(string), 
            typeof(LogList),
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

    public LogList()
    {
        InitializeComponent();
        DockPanel.DataContext = this;
    }
}