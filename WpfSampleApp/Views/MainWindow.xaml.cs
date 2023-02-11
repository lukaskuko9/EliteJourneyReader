using System;
using System.Windows;
using EliteJourneyReader.Public;
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
        private readonly EliteJourneyReader.Public.EliteJourneyProvider _eliteJourneyProvider;
        private readonly TestOptions _testOptions;
        private readonly MainWindowViewModel viewModel;
        
        public MainWindow(EliteJourneyReader.Public.EliteJourneyProvider eliteJourneyProvider, IOptions<TestOptions> testOptions)
        {
            _eliteJourneyProvider = eliteJourneyProvider;
            _testOptions = testOptions.Value;
            viewModel = new MainWindowViewModel(_testOptions.Title);
            
            InitializeComponent();

            _eliteJourneyProvider.OnAnyEvent += EliteJourneyProviderOnAnyEvent;
            _eliteJourneyProvider.OnFriendsChange += EliteJourneyProviderOnOnFriendsChange;
            _eliteJourneyProvider.OnReaderError += EliteJourneyProviderOnOnProviderError;
            DataContext = viewModel;
        }

        private void EliteJourneyProviderOnOnFriendsChange(FriendsEventMessage message)
        {
            Console.WriteLine();
        }

        private void EliteJourneyProviderOnOnProviderError(ErrorMessage message)
        {
            Dispatcher.Invoke(() =>
            {
                viewModel.AddErrorMessage(message);
            });
        }

        private void EliteJourneyProviderOnAnyEvent(JourneyEventMessage? message, string jsonMessage)
        {
            Dispatcher.Invoke(() =>
            {
                viewModel.AddEventMessage(message, jsonMessage);
            });
        }
    }
}