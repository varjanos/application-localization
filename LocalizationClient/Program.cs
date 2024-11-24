using LocalizationClient.Resources;
using LocalizationClient.View;
using Microsoft.Extensions.Localization;
using LocalizationManagerSDK;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddControllers();

var LocalizationManagerSDKOptions = new LocalizationManagerSDK.Options.LocalizationOptions()
{
    ManagerUrl = "https://localizationmanager.azurewebsites.net",
    AppName = "FinalLocalizationClient",
    AppId = "6110d570-fb2d-4421-0002-0b698ca6defe",
    SupportedLanguages = "en;hu;",
    ResourceFilePath = "/Resources/",
};

builder.Services.RegisterLocalizationManager(LocalizationManagerSDKOptions);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapControllers();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
