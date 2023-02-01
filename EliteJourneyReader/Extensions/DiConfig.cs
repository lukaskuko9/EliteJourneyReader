using EliteJourneyReader.Journey;
using EliteJourneyReader.Journey.EventMessages;
using EliteJourneyReader.Journey.Mediator;
using Microsoft.Extensions.DependencyInjection;

namespace EliteJourneyReader.Extensions;

public static class DiConfig
{

    public static void ConfigureJourneyReader(this IServiceCollection services)
    {
        services.AddSingleton<JourneyEventMediator>();
        services.AddSingleton<Journey.Mediator.EliteJourneyReader>();
    }
}