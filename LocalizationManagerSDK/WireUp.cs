using LocalizationManagerSDK.Connection;
using LocalizationManagerSDK.Localizer;
using LocalizationManagerSDK.Options;
using LocalizationManagerSDK.ResourceHandler;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;

namespace LocalizationManagerSDK;

public static class WireUp
{
    public static void RegisterLocalizationManager(this IServiceCollection services, Options.LocalizationOptions options)
    {
        services.AddScoped<IResourceHandlerService, ResourceHandlerService>();

        services.AddSingleton(options);

        services.AddSingleton<SignalRConnectorService>();

        services.BuildServiceProvider().GetRequiredService<SignalRConnectorService>();


        services.AddSingleton<IStringLocalizerFactory, CustomStringLocalizerFactory>();

        services.AddSingleton(typeof(IStringLocalizer<>), typeof(CustomStringLocalizer));

        services.AddSingleton<IUpdateableLocalizer, CustomStringLocalizer>();
    }
}
