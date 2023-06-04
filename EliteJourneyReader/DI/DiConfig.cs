using EliteJourneyReader.EliteJourneyProvider;
using EliteJourneyReader.EventMessages;
using EliteJourneyReader.JourneyReader;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Newtonsoft.Json;

namespace EliteJourneyReader.DI;

public static class DiConfig
{
    public static void ConfigureJourneyReader(this IServiceCollection services,
        Action<JourneyReaderOptions>? optionsAction = null)
    {
        services.AddSingleton<IJourneyFileReader, JourneyFileReader>();
        services.AddTransient<IJourneyEventProcessor, JourneyEventProcessor>();
        services.AddSingleton<IEliteJourneyProvider, EliteJourneyProvider.EliteJourneyProvider>();
        services.Configure(optionsAction ?? SetDefaultOptions());
        
        RegisterMessageTypes(services);
    }

    private static Action<JourneyReaderOptions> SetDefaultOptions()
    {
        return options =>
        {
            options.JsonSerializerSettings = new JsonSerializerSettings();
            options.AutoStartProcessingMessages = true;
        };
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