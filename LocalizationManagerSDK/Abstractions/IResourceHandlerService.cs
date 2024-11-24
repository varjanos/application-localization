namespace LocalizationManagerSDK.Abstractions;

public interface IResourceHandlerService
{
    void HandleAllLocalizationReceived(Dictionary<string, Dictionary<string, string>> dictionary);

    void HandleLocalizationAdded(string language, string key, string value);

    void HandleLocalizationUpdated(string language, string key, string value);

    void HandleLocalizationDeleted(string language, string key);
}