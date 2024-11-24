namespace LocalizationManagerSDK.Localizer;

public interface IUpdateableLocalizer
{
    void HandleLocalizationDictReceived(Dictionary<string, Dictionary<string, string>> _localizationData);

    void HandleLocalizationAdded(string language, string key, string value);

    void HandleLocalizationUpdated(string language, string key, string value);

    void HandleLocalizationDeleted(string language, string key, string value);
}
