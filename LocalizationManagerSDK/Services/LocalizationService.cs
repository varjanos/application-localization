using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalizationManagerSDK.Services
{
    public class LocalizationService
    {
        private static Dictionary<string, Dictionary<string, string>> _localizations; 
        public string CurrentCulture { get; private set; } = "en";

        public event Action OnLanguageChanged;

        public string GetString(string key)
        {
            if (_localizations.TryGetValue(CurrentCulture, out var cultureLocalizations))
            {
                return cultureLocalizations.TryGetValue(key, out var value) ? value : $"[{key}]";
            }

            return $"[{key}]";
        }

        public void SetCulture(string culture)
        {
            if (_localizations.ContainsKey(culture))
            {
                CurrentCulture = culture;
            }
            OnLanguageChanged?.Invoke();
        }

        public void AddOrUpdate(string culture, string key, string value)
        {
            if (!_localizations.ContainsKey(culture))
            {
                _localizations.Add(culture, new Dictionary<string, string>());
            }
            if (_localizations[culture].ContainsKey(key))
            {
                _localizations[culture][key] = value;
            }
            else
            {
                _localizations[culture].Add(key, value);
            }
            OnLanguageChanged?.Invoke();
           
        }
        public void Remove(string culture, string key)
        {
            _localizations[culture].Remove(key);
        }
        public void AddList(Dictionary<string, Dictionary<string, string>> dict)
        {
            _localizations = dict;
        }
    }
}
