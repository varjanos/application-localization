using Microsoft.Extensions.Localization;
using System.Globalization;

namespace LocalizationManagerSDK.Localizer;

public class CustomStringLocalizer : IStringLocalizer, IUpdateableLocalizer
{
    private static Dictionary<string, Dictionary<string, string>> _localizationData;

    public CustomStringLocalizer(Dictionary<string, Dictionary<string, string>> localizationData)
    {
        _localizationData = localizationData;
    }

    public LocalizedString this[string name]
    {
        get
        {
            var value = GetLocalizedValue(name, CultureInfo.CurrentUICulture.Name);
            return new LocalizedString(name, value ?? name, resourceNotFound: value == null);
        }
    }

    public LocalizedString this[string name, params object[] arguments]
    {
        get
        {
            var value = GetLocalizedValue(name, CultureInfo.CurrentUICulture.Name);
            var formattedValue = string.Format(value ?? name, arguments);
            return new LocalizedString(name, formattedValue, resourceNotFound: value == null);
        }
    }

    public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
    {
        var currentCulture = CultureInfo.CurrentUICulture.Name;
        foreach (var kvp in _localizationData)
        {
            if (_localizationData[currentCulture].TryGetValue(kvp.Key, out var value))
            {
                yield return new LocalizedString(kvp.Key, value, resourceNotFound: false);
            }
        }
    }

    public void HandleLocalizationAdded(string language, string key, string value)
    {
        // TODO
    }

    public void HandleLocalizationDeleted(string language, string key, string value)
    {
        // TODO
    }

    public void HandleLocalizationDictReceived(Dictionary<string, Dictionary<string, string>> localizationData)
    {
        _localizationData = localizationData;
    }

    public void HandleLocalizationUpdated(string language, string key, string value)
    {
        // TODO 
    }

    private string? GetLocalizedValue(string key, string culture)
    {
        if (_localizationData.ContainsKey(culture) && _localizationData[culture].TryGetValue(key, out var value))
        {
            return value;
        }

        return null;
    }
}