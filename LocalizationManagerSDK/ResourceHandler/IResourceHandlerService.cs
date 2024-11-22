namespace LocalizationManagerSDK.ResourceHandler;

public interface IResourceHandlerService
{
    void HandleLocalizationAdded(string language, string key, string value);

    void HandleLocalizationUpdated(string language, string key, string value);

    void HandleLocalizationDeleted(string language, string key);
}
