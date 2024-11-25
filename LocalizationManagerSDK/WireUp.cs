using LocalizationManagerSDK.Abstractions;
using LocalizationManagerSDK.Connection;
using LocalizationManagerSDK.ResourceHandler;
using LocalizationManagerSDK.Services;
using Microsoft.Extensions.DependencyInjection;

namespace LocalizationManagerSDK;

public static class WireUp
{
    public static void RegisterLocalizationManager(this IServiceCollection services, Options.LocalizationOptions options)
    {
        services.AddSingleton<IResourceHandlerService, ResourceHandlerService>();

        services.AddSingleton(options);

        services.AddSingleton<SignalRConnectorService>();

        services.AddSingleton<LocalizationService>();

        services.BuildServiceProvider().GetRequiredService<SignalRConnectorService>();
    }
}
