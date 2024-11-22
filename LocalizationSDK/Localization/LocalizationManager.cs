using System.Collections.Generic;
using System.IO;
using System.Resources;
using Microsoft.Extensions.Logging;
using LocalizationSDK.Events;
using LocalizationSDK.Connection;

namespace LocalizationSDK.Localization
{
    public class LocalizationManager
    {
        private readonly ILogger<LocalizationManager> _logger;
        private readonly SignalRConnectionManager _connectionManager;

        // Event triggered when a localization update is received
        public event EventHandler<LocalizationEventArgs>? LocalizationUpdateReceived;

        public LocalizationManager(ILogger<LocalizationManager> logger, SignalRConnectionManager connectionManager)
        {
            _logger = logger;
            _connectionManager = connectionManager;

            // Register a handler for receiving localization updates from the SignalR Hub
            _connectionManager.GetConnection().On<string, string>("ReceiveLocalizationUpdate", (key, value) =>
            {
                OnLocalizationUpdateReceived(new LocalizationEventArgs { Key = key, Value = value });
            });
        }

        // Update the RESX file with the provided key-value pairs
        public void UpdateResxFile(string filePath, Dictionary<string, string> values)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    BackupResxFile(filePath); // Create a backup before updating
                    using var writer = new ResXResourceWriter(filePath);
                    foreach (var item in values)
                    {
                        writer.AddResource(item.Key, item.Value); // Add each key-value pair to the RESX file
                    }
                    _logger.LogInformation("Successfully updated RESX file: {FilePath}", filePath);
                }
                else
                {
                    _logger.LogError("RESX file not found: {FilePath}", filePath);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to update RESX file: {FilePath}", filePath);
            }
        }

        // Handle the localization update event
        private void OnLocalizationUpdateReceived(LocalizationEventArgs e)
        {
            LocalizationUpdateReceived?.Invoke(this, e);
            UpdateLocalizationResource(e.Key, e.Value); // Update the resource file with the new value
        }

        // Update the localization resource with the specified key and value
        private void UpdateLocalizationResource(string key, string value)
        {
            _logger.LogInformation("Updating resource file with Key={Key} and Value={Value}", key, value);
        }

        // Create a backup of the RESX file before making changes
        private void BackupResxFile(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    var backupFilePath = $"{filePath}.bak";
                    File.Copy(filePath, backupFilePath, true); // Create a backup file with .bak extension
                    _logger.LogInformation("Backup created for RESX file: {FilePath}", filePath);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to create backup for RESX file: {FilePath}", filePath);
            }
        }

        // Method to add a new language by creating a RESX file
        public void AddLanguage(string filePath, string languageCode)
        {
            try
            {
                var newFilePath = Path.Combine(Path.GetDirectoryName(filePath), $"{Path.GetFileNameWithoutExtension(filePath)}.{languageCode}.resx");
                
                if (!File.Exists(newFilePath))
                {
                    using var writer = new ResXResourceWriter(newFilePath);
                    // Optionally, add some default values or placeholders here
                    writer.Generate();
                    _logger.LogInformation("New RESX file created for language {LanguageCode}: {FilePath}", languageCode, newFilePath);
                }
                else
                {
                    _logger.LogWarning("RESX file for language {LanguageCode} already exists.", languageCode);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to create RESX file for language {LanguageCode}.", languageCode);
            }
        }
    }
}