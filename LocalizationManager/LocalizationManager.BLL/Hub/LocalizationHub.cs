using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace LocalizationManager.BLL.Hub;

public class LocalizationHub(ILogger<LocalizationHub> _logger) : Hub<ILocalizationClient>
{
    public override async Task OnConnectedAsync()
    {
        var appId = Context.GetHttpContext()?.Request.Query["appId"];

        if (!string.IsNullOrEmpty(appId))
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, appId!);

            _logger.LogInformation("Client with id: {appId}, connectionId: {connectionId} successfully connected!", appId, Context.ConnectionId);
        }

        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var appId = Context.GetHttpContext()?.Request.Query["appId"];

        if (!string.IsNullOrEmpty(appId))
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, appId!);
        }

        _logger.LogInformation("Client with id: {appId}, connectionId: {connectionId} disconnected!", appId, Context.ConnectionId);

        await base.OnDisconnectedAsync(exception);
    }
}

public interface ILocalizationClient
{
    Task SendLocalizationAdded(string language, string key, string value);

    Task SendLocalizationUpdated(string language, string key, string value);

    Task SendLocalizationDeleted(string languge, string key);
}
