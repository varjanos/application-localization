using LocalizationManagerSDK.Connection;
using Microsoft.Extensions.DependencyInjection;

namespace LocalizationManagerSDK;

public static class WireUp
{
    public static void RegisterLocalizationManager(this IServiceCollection services)
    {
        services.AddSingleton<ISignalRConnectorService, SignalRConnectorService>();
    }
}
