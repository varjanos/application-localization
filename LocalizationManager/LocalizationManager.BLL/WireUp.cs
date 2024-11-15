using LocalizationManager.BLL.Authentication;
using LocalizationManager.BLL.Localization;
using Microsoft.Extensions.DependencyInjection;

namespace LocalizationManager.BLL;

public static class WireUp
{
    public static void ConfigureAuthenticationService(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
    }

    public static void ConfigureLocalizationService(this IServiceCollection services)
    {
        services.AddScoped<ILocalizationService, LocalizationService>();
    }
}
