using System.Reflection;
using EliteJourneyReader.JourneyReader;
using EliteJourneyReader.Public.EventMessages;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace EliteJourneyReader.Public.Extensions;

public static class DiConfig
{
    public static void ConfigureJourneyReader(this IServiceCollection services)
    {
        services.AddSingleton<JourneyFileReader>();
        services.AddTransient<JourneyEventProcessor>();
        services.AddSingleton<EliteJourneyProvider>();

        RegisterMessageTypes(services);
        
        var serviceProvider = services.BuildServiceProvider();
        var journeyFileReader = serviceProvider.GetRequiredService<JourneyFileReader>();
        JourneyFileReader.SetInstance(journeyFileReader);
    }

    private static void RegisterMessageTypes(IServiceCollection services)
    {
        var messagesTypes = typeof(JourneyEventMessage).Assembly.DefinedTypes
            .Where(type => type.IsSubclassOf(typeof(JourneyEventMessage)))
            .Select(x => x.UnderlyingSystemType)
            .ToList();

        foreach (var messageType in messagesTypes)
        {
            var service = ServiceDescriptor.Scoped(typeof(IEventMessage), messageType);
            services.TryAddEnumerable(service);
            
        }
    }
}