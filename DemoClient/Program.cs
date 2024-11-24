using DemoClient;
using LocalizationManagerSDK;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddLocalization(opts => { opts.ResourcesPath = "Resources"; });

var localizationOptions = new LocalizationManagerSDK.Options.LocalizationOptions()
{
    ManagerUrl = "https://localizationmanager.azurewebsites.net",
    AppName = "DemoClient",
    AppId = "6110d570-fb2d-4421-9510-0b698ca6defe",
    SupportedLanguages = "en;hu",
    ResourceFilePath = "/Resources/"
};

builder.Services.RegisterLocalizationManager(localizationOptions);

await builder.Build().RunAsync();