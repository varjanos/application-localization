using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalizationManagerSDK.Services
{
    public class LocalizationService
    {
        private readonly Dictionary<string, Dictionary<string, string>> _localizations;
        public string CurrentCulture { get; private set; } = "en-US";

        public event Action OnLanguageChanged;
        public LocalizationService()
        {
            _localizations = new Dictionary<string, Dictionary<string, string>>
        {
            { "en-US", new Dictionary<string, string>
                {
                    { "Welcome", "Welcome to the Localization Client app!" },
                    { "Description", "We showcase how to change or update the the languages real-time" },
                    {"HowToUse","How to use" },
                    {"HowToUse1","Change the language to your preference using the dropdown menu" },
                    {"HowToUse2","You will see the changes instantly" },
                    {"LanguageEN","English" },
                    {"LanguageHU","Hungarian" },
                    {"LocalizationClient","Localization Client" },
                    {"MenuHome","Home" },
                    {"TryItOut","Try it out!" },
                    {"ChangeLanguage","Choose a language" }
                }
            },
            { "hu-HU", new Dictionary<string, string>
                {
                    { "Welcome", "Üdvözlünk a lokalizációs kliens appban!" },
                    { "Description", "Az applikációban bemutatjuk hogyan tudjuk egy saját SDK-val megváltoztatni a nyelvet valós időben" },
                    {"HowToUse","Hogyan használd" },
                    {"HowToUse1","Változtasd meg a nyelvet igényednek megfelelően a legördülő menüből" },
                    {"HowToUse2","Azonnal láthatod a változást" },
                    {"LanguageEN","Angol" },
                    {"LanguageHU","Magyar" },
                    {"LocalizationClient","Lokalizációs kliens" },
                    {"MenuHome","Kezdőlap" },
                    {"TryItOut","Próbáld ki!" },
                    {"ChangeLanguage","Válassz nyelvet" }
                }
            }
        };
        }

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
                _localizations[culture] = new Dictionary<string, string>();
            }
            _localizations[culture][key] = value;
            OnLanguageChanged?.Invoke();
        }
        public void Remove(string culture, string key)
        {
            _localizations[culture].Remove(key);
            OnLanguageChanged?.Invoke();
        }
    }
}
