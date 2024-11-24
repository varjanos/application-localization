using Microsoft.Extensions.Localization;

namespace LocalizationManagerSDK.Localizer;

public class CustomStringLocalizerFactory : IStringLocalizerFactory
{
    private readonly Dictionary<string, Dictionary<string, string>> _localizationData;

    public CustomStringLocalizerFactory(Dictionary<string, Dictionary<string, string>> localizationData)
    {
        _localizationData = localizationData;
    }

    public IStringLocalizer Create(Type resourceSource)
    {
        return new CustomStringLocalizer(_localizationData);
    }

    public IStringLocalizer Create(string baseName, string location)
    {
        return new CustomStringLocalizer(_localizationData);
    }
}
