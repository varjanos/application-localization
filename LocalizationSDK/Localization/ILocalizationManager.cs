using System.Collections.Generic;
using LocalizationSDK.Events;

namespace LocalizationSDK.Localization
{
    public interface ILocalizationManager
    {
        event EventHandler<LocalizationEventArgs>? LocalizationUpdateReceived;
        void UpdateResxFile(string filePath, Dictionary<string, string> values);
        void AddLanguage(string filePath, string languageCode);
    }
}
