using EliteJourneyReader.Public.JourneyReader;
using Microsoft.Extensions.DependencyInjection;

namespace EliteJourneyReader.Public.Extensions;

public static class DiConfig
{
    public static void ConfigureJourneyReader(this IServiceCollection services)
    {
        services.AddSingleton<JourneyFileReader>();
        services.AddTransient<JourneyEventMediator>();
        services.AddSingleton<EliteJourneyReader>();
    }
}