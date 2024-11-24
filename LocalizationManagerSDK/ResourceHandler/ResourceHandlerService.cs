using LocalizationManagerSDK.Abstractions;
using LocalizationManagerSDK.Options;
using LocalizationManagerSDK.Services;
using System.Resources;

namespace LocalizationManagerSDK.ResourceHandler;

public class ResourceHandlerService : IResourceHandlerService
{
    private LocalizationService _localizationService;
    public ResourceHandlerService(LocalizationService localizationService)
    {
        _localizationService = localizationService;
    }
    public void HandleLocalizationAdded(string language, string key, string value)
    {
        Console.WriteLine($"{nameof(HandleLocalizationAdded)} called, received: [{language} - {key}: {value}]!");
        _localizationService.AddOrUpdate(language, key, value);
    }

    public void HandleLocalizationUpdated(string language, string key, string value)
    {
        Console.WriteLine($"{nameof(HandleLocalizationUpdated)} called, received: [{language} - {key}: {value}]!");
        _localizationService.AddOrUpdate(language, key, value);
    }

    public void HandleLocalizationDeleted(string language, string key)
    {
        Console.WriteLine($"{nameof(HandleLocalizationDeleted)} called, received: [{language} - {key}]!");
        _localizationService.Remove(language, key);
    }

    public void HandleAllLocalizationReceived(Dictionary<string, Dictionary<string, string>> dictionary)
    {
        Console.WriteLine("Received Localizations Dictionary, values: ");

        _localizationService.AddList(dictionary);
    }
}
