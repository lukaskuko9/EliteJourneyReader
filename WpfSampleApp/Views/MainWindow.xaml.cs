using System;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using EliteJourneyReader.Public;
using EliteJourneyReader.Public.EliteJourneyProvider;
using EliteJourneyReader.Public.EventMessages;
using Microsoft.Extensions.Options;
using WpfSampleApp.Options;
using WpfSampleApp.ViewModels;

namespace WpfSampleApp.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly MainWindowViewModel _viewModel;
        
        public MainWindow(EliteJourneyProvider eliteJourneyProvider, IOptions<TestOptions> testOptions)
        {
            var testOptions1 = testOptions.Value;
            _viewModel = new MainWindowViewModel(testOptions1.Title);
            
            InitializeComponent();

            eliteJourneyProvider.OnAnyEvent += EliteJourneyProviderOnAnyEvent;
            eliteJourneyProvider.OnReaderError += EliteJourneyProviderOnReaderError;
            DataContext = _viewModel;
        }

        private void EliteJourneyProviderOnReaderError(string jsonMessage)
        {
            Dispatcher.Invoke(() =>
            {
                _viewModel.AddErrorMessage(jsonMessage);
            });
        }
        private void EliteJourneyProviderOnAnyEvent(JourneyEventMessage message, string jsonMessage)
        {
            Dispatcher.Invoke(() =>
            {
                _viewModel.AddEventMessage(message, jsonMessage);
            });
        }
        
        private void CommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var str = MyListView.SelectedValue.ToString();
            if (string.IsNullOrWhiteSpace(str) == false)
                Clipboard.SetDataObject(str);
        }
    }
}