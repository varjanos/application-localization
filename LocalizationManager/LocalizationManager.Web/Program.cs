using Blazored.LocalStorage;
using LocalizationManager.Web;
using LocalizationManager.Web.AuthProvider;
using LocalizationManager.Web.Client;
using LocalizationManager.Web.Constants;
using LocalizationManager.Web.Handler;
using LocalizationManager.Web.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var baseAddress = new Uri(builder.HostEnvironment.BaseAddress);

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = baseAddress });

builder.Services.AddTransient<IncludeRequestCredentialsHttpMessageHandler>();

builder.Services
    .AddHttpClient(HttpClientNames.ApiHttpClientName, client => client.BaseAddress = baseAddress)
    .AddHttpMessageHandler<IncludeRequestCredentialsHttpMessageHandler>();

builder.Services.AddHttpClient<IAuthenticationClient, AuthenticationClient>(HttpClientNames.ApiHttpClientName);
builder.Services.AddHttpClient<ILocalizationClient, LocalizationClient>(HttpClientNames.ApiHttpClientName);
builder.Services.AddHttpClient<IApplicationClient, ApplicationClient>(HttpClientNames.ApiHttpClientName);
builder.Services.AddHttpClient<ILanguageClient, LanguageClient>(HttpClientNames.ApiHttpClientName);

builder.Services.AddScoped<AuthenticationStateProvider, ApiAuthenticationStateProvider>();
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddAuthorizationCore();
builder.Services.AddMudServices();

await builder.Build().RunAsync();
