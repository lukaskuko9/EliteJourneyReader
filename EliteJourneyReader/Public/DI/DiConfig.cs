using EliteJourneyReader.JourneyReader;
using EliteJourneyReader.Public.EventMessages;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace EliteJourneyReader.Public.DI;

public static class DiConfig
{
    public static void ConfigureJourneyReader(this IServiceCollection services,
        Action<JournalReaderOptions>? optionsAction = null)
    {
        services.AddSingleton<JourneyFileReader>();
        services.AddTransient<JourneyEventProcessor>();
        services.AddSingleton<EliteJourneyProvider.EliteJourneyProvider>();


        RegisterMessageTypes(services);

        var serviceProvider = services.BuildServiceProvider();
        var journeyFileReader = serviceProvider.GetRequiredService<JourneyFileReader>();
        JourneyFileReader.SetInstance(journeyFileReader);

        if (optionsAction is not null)
         SetOptions(serviceProvider, optionsAction);

    }

    private static void SetOptions(ServiceProvider serviceProvider, Action<JournalReaderOptions> optionsAction)
    {
        var options = new JournalReaderOptions();
        optionsAction.Invoke(options);

        var processor = serviceProvider.GetRequiredService<JourneyEventProcessor>();
        processor.SetJournalReaderOptions(options);
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