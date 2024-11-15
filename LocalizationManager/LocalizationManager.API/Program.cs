using LocalizationManager.API.Extensions;
using LocalizationManager.BLL;
using LocalizationManager.DAL;
using LocalizationManager.DAL.Context;
using LocalizationManager.DAL.Entities;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "LocalizationManager API", Version = "v1" });
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

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
