using Blazored.LocalStorage;
using LocalizationManager.Transfer.AuthDtos;
using LocalizationManager.Web.AuthProvider;
using LocalizationManager.Web.Client;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net;
using System.Net.Http.Headers;

namespace LocalizationManager.Web.Services;

public class AuthService(
    HttpClient _httpClient,
    IAuthenticationClient _authenticationClient,
    AuthenticationStateProvider _authenticationStateProvider,
    ILocalStorageService _localStorage) : IAuthService
{
    public static readonly string tokenLocalStorageName = "authToken";

    public async Task LoginAsync(LoginDto loginModel)
    {
        var token = await _authenticationClient.LoginAsync(loginModel);

        if (string.IsNullOrEmpty(token))
        {
            throw new Exception("Failed to login!");
        }

        await _localStorage.SetItemAsync(tokenLocalStorageName, token);

        ((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsAuthenticated(loginModel.Username);

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
    }

    public async Task LogoutAsync()
    {
        await _localStorage.RemoveItemAsync(tokenLocalStorageName);

        ((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsLoggedOut();

        _httpClient.DefaultRequestHeaders.Authorization = null;
    }

    public async Task RegisterAsync(RegisterDto registerModel)
    {
        var response = await _authenticationClient.RegisterAsync(registerModel);

        if(response.StatusCode != (int)HttpStatusCode.OK)
        {
            throw new Exception("Failed to register!");
        }
    }
}
