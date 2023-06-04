using EliteJourneyReader.DI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WpfSampleApp.Options;
using WpfSampleApp.Views;

namespace WpfSampleApp;

public static class DiConfig
{
    public static void ConfigureServices(IServiceCollection services, IConfiguration config)
    {
        services.Configure<TestOptions>(config.GetSection(nameof(TestOptions)));
        services.AddTransient(typeof(MainWindow));
        
        services.ConfigureJourneyReader();

    }
}