using LocalizationManager.BLL.Application;
using LocalizationManager.BLL.SdkLocalizer;
using LocalizationManager.Transfer.Application;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System.Text.RegularExpressions;

namespace LocalizationManager.BLL.Hub;

public class LocalizationHub(
    ILogger<LocalizationHub> _logger,
    ISdkLocalizerService _sdkLocalizerService,
    IApplicationManagingService _applicationManagingService) : Hub<ILocalizationClient>
{
    public override async Task OnConnectedAsync()
    {
        var context = Context.GetHttpContext() ?? throw new Exception("Something went wrong with the connection!");

        var applicationDto = new ApplicationDto();

        try
        {
            applicationDto.AppId = context.Request.Query["appId"].Single() ?? "";
            applicationDto.AppName = context.Request.Query["appName"].Single() ?? "";
            applicationDto.SupportedLanguages = context.Request.Query["supportedLanguages"].Single()!.Split(';').ToList();

            await _applicationManagingService.RegisterApplicationAsync(applicationDto);

            await Groups.AddToGroupAsync(Context.ConnectionId, applicationDto.AppId);

            _logger.LogInformation("Client with id: {appId}, connectionId: {connectionId} successfully connected!", applicationDto.AppId, Context.ConnectionId);

            var data = await _sdkLocalizerService.GetLocalizationsAsync(applicationDto.AppId);

            await Clients.Caller.SendAllLocalizations(data);

        } catch(Exception)
        {
            _logger.LogInformation("Client with connectionId: {connectionId} failed to connect!", Context.ConnectionId);
        }

        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var context = Context.GetHttpContext() ?? throw new Exception("Something went wrong with the connection!");

        var appId = context.Request.Query["appId"];

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
    Task SendAllLocalizations(Dictionary<string, Dictionary<string, string>> dictionary);

    Task SendLocalizationAdded(string language, string key, string value);

    Task SendLocalizationUpdated(string language, string key, string value);

    Task SendLocalizationDeleted(string languge, string key);
}
