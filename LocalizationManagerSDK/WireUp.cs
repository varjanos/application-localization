using LocalizationManagerSDK.Connection;
using LocalizationManagerSDK.Options;
using LocalizationManagerSDK.ResourceHandler;
using Microsoft.Extensions.DependencyInjection;

namespace LocalizationManagerSDK;

public static class WireUp
{
    public static void RegisterLocalizationManager(this IServiceCollection services, LocalizationOptions options)
    {
        services.AddScoped<IResourceHandlerService, ResourceHandlerService>();

        services.AddSingleton<SignalRConnectorService>();

        services.AddSingleton(options);
    }
}
