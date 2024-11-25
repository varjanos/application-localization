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
    ManagerUrl = builder.Configuration.GetSection("LocalizationOptions").GetSection("ManagerUrl").Value,
    AppName = builder.Configuration.GetSection("LocalizationOptions").GetSection("AppName").Value,
    AppId = builder.Configuration.GetSection("LocalizationOptions").GetSection("AppId").Value,
    SupportedLanguages = builder.Configuration.GetSection("LocalizationOptions").GetSection("SupportedLanguages").Value
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
