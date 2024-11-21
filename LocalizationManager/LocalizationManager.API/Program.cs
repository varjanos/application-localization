using LocalizationManager.API.Extensions;
using LocalizationManager.BLL;
using LocalizationManager.BLL.Hub;
using LocalizationManager.DAL;
using LocalizationManager.DAL.Context;
using LocalizationManager.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Primitives;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddOpenApiDocument(configure =>
{
    configure.Title = "LocalizationManager API";
});

builder.Services.ConfigureLocalizationDb(builder.Configuration.GetConnectionString("DefaultConnection")!);

builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<LocalizationDbContext>()
    .AddDefaultTokenProviders();

builder.AddJwtAuth();

builder.Services.AddAuthorization();

builder.Services.AddControllers();

builder.Services.ConfigureAuthenticationService();
builder.Services.ConfigureLocalizationService();
builder.Services.ConfigureLanguageService();
builder.Services.ConfigureApplicationService();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.AllowAnyOrigin();
            policy.AllowAnyHeader();
            policy.AllowAnyMethod();
        });
});

builder.Services.AddSignalR();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseWebAssemblyDebugging();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Use(async (context, next) =>
{
    context.Response.Headers.Append("X-Content-Type-Options", new StringValues("nosniff"));
    context.Response.Headers.Append("X-Frame-Options", new StringValues("SAMEORIGIN"));
    context.Response.Headers.Append("X-XSS-Protection", new StringValues("1; mode=block"));

    context.Response.Headers.Append("Cache-Control", new StringValues("no-store, no-cache"));
    context.Response.Headers.Append("Pragma", new StringValues("no-cache"));

    await next();
});

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.MapControllers();

app.MapHub<LocalizationHub>("/hubs/localization-hub");

app.MapFallbackToFile("index.html");

app.Run();
