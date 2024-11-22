namespace LocalizationSDK.Configuration
{
    public interface ISdkConfiguration
    {
        string ServerUrl { get; set; }
        string HubEndpoint { get; set; }
    }
}