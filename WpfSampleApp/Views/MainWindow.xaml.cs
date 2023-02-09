using System;
using System.Windows;
using EliteJourneyReader.Public.EventMessages;
using Microsoft.Extensions.Options;
using WpfSampleApp.Options;
using WpfSampleApp.ViewModels;

namespace WpfSampleApp.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly EliteJourneyReader.Public.EliteJourneyReader _eliteJourneyReader;
        private readonly TestOptions _testOptions;
        private readonly MainWindowViewModel viewModel;
        
        public MainWindow(EliteJourneyReader.Public.EliteJourneyReader eliteJourneyReader, IOptions<TestOptions> testOptions)
        {
            _eliteJourneyReader = eliteJourneyReader;
            _testOptions = testOptions.Value;
            viewModel = new MainWindowViewModel(_testOptions.Title);
            
            InitializeComponent();

            _eliteJourneyReader.OnAnyEvent += EliteJourneyReaderOnAnyEvent;
            _eliteJourneyReader.OnReaderError += EliteJourneyReaderOnOnReaderError;
            DataContext = viewModel;
        }

        private void EliteJourneyReaderOnOnReaderError(ErrorMessage message)
        {
            Dispatcher.Invoke(() =>
            {
                viewModel.AddErrorMessage(message);
            });
        }

        private void EliteJourneyReaderOnAnyEvent(JourneyEventMessage? message, string jsonMessage)
        {
            Dispatcher.Invoke(() =>
            {
                viewModel.AddEventMessage(message, jsonMessage);
            });
        }
    }
}