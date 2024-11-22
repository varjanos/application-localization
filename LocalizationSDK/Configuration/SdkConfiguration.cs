using Microsoft.Extensions.Configuration;
using System.IO;

namespace LocalizationSDK.Configuration
{
    public class SdkConfiguration
    {
        public string ServerUrl { get; set; } = null!; // The server URL
        public string HubEndpoint { get; set; } = null!; // The SignalR Hub endpoint

        // Load configuration from a JSON file
        public static SdkConfiguration LoadFromJson(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("Configuration file not found.", filePath);
            }

            var configuration = new ConfigurationBuilder()
                .AddJsonFile(filePath, optional: false, reloadOnChange: true)
                .Build();

            return configuration.Get<SdkConfiguration>();
        }
    }
}