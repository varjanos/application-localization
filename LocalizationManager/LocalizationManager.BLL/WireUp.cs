using LocalizationManager.BLL.Application;
using LocalizationManager.BLL.Authentication;
using LocalizationManager.BLL.Language;
using LocalizationManager.BLL.Localization;
using LocalizationManager.BLL.SdkLocalizer;
using Microsoft.Extensions.DependencyInjection;

namespace LocalizationManager.BLL;

public static class WireUp
{
    public static void ConfigureAuthenticationService(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
    }

    public static void ConfigureApplicationService(this IServiceCollection services)
    {
        services.AddScoped<IApplicationManagingService, ApplicationManagingService>();
    }

    public static void ConfigureLocalizationService(this IServiceCollection services)
    {
        services.AddScoped<ILocalizationService, LocalizationService>();
        services.AddScoped<ISdkLocalizerService, SdkLocalizerService>();
    }

    public static void ConfigureLanguageService(this IServiceCollection services)
    {
        services.AddScoped<ILanguageService, LanguageService>();
    }
}
