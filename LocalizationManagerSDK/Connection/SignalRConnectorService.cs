using LocalizationManagerSDK.Abstractions;
using LocalizationManagerSDK.Options;
using Microsoft.AspNetCore.SignalR.Client;

namespace LocalizationManagerSDK.Connection;

public class SignalRConnectorService : IAsyncDisposable
{
    private readonly HubConnection _connection;

    private readonly IResourceHandlerService _resourceHandlerService;

    public SignalRConnectorService(IResourceHandlerService resourceHandlerService, LocalizationOptions localizationOptions)
    {
        ArgumentNullException.ThrowIfNull(localizationOptions);
        ArgumentException.ThrowIfNullOrEmpty(localizationOptions.ManagerUrl);
        ArgumentException.ThrowIfNullOrEmpty(localizationOptions.AppName);
        ArgumentException.ThrowIfNullOrEmpty(localizationOptions.AppId);

        _resourceHandlerService = resourceHandlerService;

        var hubUrlWithHeaders = localizationOptions.ManagerUrl;
        hubUrlWithHeaders += "/hubs/localization-hub";
        hubUrlWithHeaders += $"?appId={localizationOptions.AppId}";
        hubUrlWithHeaders += $"&appName={localizationOptions.AppName}";
        hubUrlWithHeaders += $"&supportedLanguages={localizationOptions.SupportedLanguages}";

        _connection = new HubConnectionBuilder()
            .WithUrl(localizationOptions.ManagerUrl, options =>
            {
                options.Url = new Uri(hubUrlWithHeaders);
            })
            .WithAutomaticReconnect()
            .Build();

        RegisterMessageHandlers();

        _connection.StartAsync()
            .ContinueWith(task =>
            {
                if (!task.IsFaulted)
                {
                    Console.WriteLine("LocalizationManager connection succeeded!");
                }
                else
                {
                    Console.WriteLine("Failed to connect to LocalizationManager!");
                }
            });
    }

    private void RegisterMessageHandlers()
    {
        _connection.On<Dictionary<string, Dictionary<string, string>>>("SendAllLocalizations", dict =>
        {
            _resourceHandlerService.HandleAllLocalizationReceived(dict);
        });

        _connection.On<string, string, string>("SendLocalizationAdded", (language, key, value) =>
        {
            _resourceHandlerService.HandleLocalizationAdded(language, key, value);
        });

        _connection.On<string, string, string>("SendLocalizationUpdated", (language, key, value) =>
        {
            _resourceHandlerService.HandleLocalizationUpdated(language, key, value);
        });

        _connection.On<string, string>("SendLocalizationDeleted", (language, key) =>
        {
            _resourceHandlerService.HandleLocalizationDeleted(language, key);
        });
    }

    public async ValueTask DisposeAsync()
    {
        await _connection.StopAsync();
        await _connection.DisposeAsync();
    }
}

