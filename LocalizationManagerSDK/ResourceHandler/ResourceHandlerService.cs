using LocalizationManagerSDK.Options;

namespace LocalizationManagerSDK.ResourceHandler;

internal class ResourceHandlerService(LocalizationOptions localizationOptions) : IResourceHandlerService
{
    public void HandleLocalizationAdded(string language, string key, string value)
    {
        // TODO

        throw new NotImplementedException();
    }

    public void HandleLocalizationUpdated(string language, string key, string value)
    {
        // TODO

        throw new NotImplementedException();
    }

    public void HandleLocalizationDeleted(string language, string key)
    {
        // TODO

        throw new NotImplementedException();
    }
}
