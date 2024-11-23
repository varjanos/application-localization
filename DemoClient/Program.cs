using DemoClient;
using LocalizationManagerSDK;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Options;
using static System.Net.Mime.MediaTypeNames;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
Directory.SetCurrentDirectory("C:\\Users\\Peti\\source\\repos\\application-localization\\DemoClient");
Console.WriteLine(Directory.GetCurrentDirectory());
foreach (var item in Directory.EnumerateFiles(Directory.GetCurrentDirectory()))
{
    Console.WriteLine(item);
}

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddLocalization(opts => { opts.ResourcesPath = "Resources"; });

builder.Services.RegisterLocalizationManager(new LocalizationManagerSDK.Options.LocalizationOptions() {ManagerUrl = "valami", AppName = "app1", AppId = "1", ResourceFilePath = "/Resources/" });

await builder.Build().RunAsync();