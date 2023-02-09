using System.Configuration;
using EliteJourneyReader.Public.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WpfSampleApp.Options;
using WpfSampleApp.Windows;

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