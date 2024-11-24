using Blazored.LocalStorage;
using LocalizationManager.Web.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Http;

namespace LocalizationManager.Web.Handler;

public class IncludeRequestCredentialsHttpMessageHandler(ILocalStorageService _localStorageService) : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request, CancellationToken cancellationToken)
    {
        request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);

        var token = await _localStorageService.GetItemAsStringAsync(AuthService.tokenLocalStorageName, cancellationToken);

        string trimmedToken = token?.Substring(1, token.Length - 2) ?? "";

        request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", trimmedToken);

        return await base.SendAsync(request, cancellationToken);
    }
}