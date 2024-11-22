namespace LocalizationManagerSDK.Connection;

using LocalizationManagerSDK.Options;
using LocalizationManagerSDK.ResourceHandler;
using Microsoft.AspNetCore.SignalR.Client;

public class SignalRConnectorService : IAsyncDisposable
{
    private readonly HubConnection _connection;

    private readonly IResourceHandlerService _resourceHandlerService;

    public SignalRConnectorService(LocalizationOptions localizationOptions, IResourceHandlerService resourceHandlerService)
    {
        _resourceHandlerService = resourceHandlerService;

        _connection = new HubConnectionBuilder()
            .WithUrl(localizationOptions.ManagerUrl, options =>
            {
                options.Headers.Add("appId", localizationOptions.AppId);
                options.Headers.Add("appName", localizationOptions.AppName);
            })
            .WithAutomaticReconnect()
            .Build();

        RegisterMessageHandlers();

        _connection.StartAsync()
            .ContinueWith(task =>
            {
                if (!task.IsFaulted)
                {
                    // TODO: handle
                }
            });
    }

    private void RegisterMessageHandlers()
    {
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

