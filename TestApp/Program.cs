using LocalizationSDK.Configuration;
using LocalizationSDK.Connection;
using LocalizationSDK.Localization;
using LocalizationSDK.Events;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        // Setup Dependency Injection and Logging
        var serviceProvider = new ServiceCollection()
            .AddLogging(config =>
            {
                config.AddConsole();
                config.SetMinimumLevel(LogLevel.Debug);
            })
            .AddTransient<SignalRConnectionManager>()
            .AddTransient<LocalizationManager>()
            .BuildServiceProvider();

        var logger = serviceProvider.GetService<ILogger<Program>>();

        // Load SDK Configuration
        logger.LogInformation("Loading SDK configuration...");
        SdkConfiguration config = null;
        try
        {
            config = SdkConfiguration.LoadFromJson("../localization_config.json"); // Update path as per your config
            logger.LogInformation($"Loaded ServerUrl: {config.ServerUrl}");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to load configuration.");
            return;
        }

        // Setup SignalR Connection
        var connectionManager = new SignalRConnectionManager(config.ServerUrl, serviceProvider.GetService<ILogger<SignalRConnectionManager>>());

        logger.LogInformation("Attempting to connect to SignalR Hub...");
        await connectionManager.ConnectAsync();

        // Test LocalizationManager
        var localizationManager = new LocalizationManager(serviceProvider.GetService<ILogger<LocalizationManager>>());
        var testResxFilePath = "test_resources.resx";

        // Create a new RESX file
        localizationManager.CreateResxFile(testResxFilePath);

        // Update the RESX file with new values
        var newValues = new Dictionary<string, string>
        {
            { "Hello", "Hello World" },
            { "Goodbye", "Goodbye World" }
        };
        localizationManager.UpdateResxFile(testResxFilePath, newValues);

        // Read the values back
        var resxValues = localizationManager.ReadResxFile(testResxFilePath);
        foreach (var kvp in resxValues)
        {
            logger.LogInformation($"{kvp.Key}: {kvp.Value}");
        }

        // Disconnect from SignalR Hub
        logger.LogInformation("Disconnecting from SignalR Hub...");
        await connectionManager.DisconnectAsync();
    }
}
