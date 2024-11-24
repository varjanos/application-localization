using LocalizationManagerSDK.Abstractions;
using LocalizationManagerSDK.Localizer;
using LocalizationManagerSDK.Options;
using System.Resources;

namespace LocalizationManagerSDK.ResourceHandler;

public class ResourceHandlerService(
    LocalizationOptions localizationOptions) : IResourceHandlerService
{
    public void HandleLocalizationAdded(string language, string key, string value)
    {
        Console.WriteLine($"{nameof(HandleLocalizationAdded)} called, received: [{language} - {key}: {value}]!");
    }

    public void HandleLocalizationUpdated(string language, string key, string value)
    {
        Console.WriteLine($"{nameof(HandleLocalizationUpdated)} called, received: [{language} - {key}: {value}]!");

        string fileName = $"Resource{language}.resx";
        var reader = new ResourceReader(localizationOptions.ResourceFilePath + fileName);
        var node = reader.GetEnumerator();
        var writer = new ResourceWriter(localizationOptions.ResourceFilePath + fileName);
        while (node.MoveNext())
        {
            if (node.Key.ToString() == key)
            {
                writer.AddResource(key, value);
                continue;
            }
            writer.AddResource(node.Key.ToString(), node.Value.ToString());
        }
        writer.Generate();
        writer.Close();
    }

    public void HandleLocalizationDeleted(string language, string key)
    {
        Console.WriteLine($"{nameof(HandleLocalizationDeleted)} called, received: [{language} - {key}]!");

        string fileName = $"Resource{language}.resx";
        var reader = new ResourceReader(localizationOptions.ResourceFilePath + fileName);
        var node = reader.GetEnumerator();
        var writer = new ResourceWriter(localizationOptions.ResourceFilePath + fileName);
        while (node.MoveNext())
        {
            if (node.Key.ToString() == key)
            {
                continue;
            }
            writer.AddResource(node.Key.ToString(), node.Value.ToString());
        }
        writer.Generate();
        writer.Close();
    }

    public void HandleAllLocalizationReceived(Dictionary<string, Dictionary<string, string>> dictionary)
    {
        Console.WriteLine("Received Localizations Dictionary, values: ");

        foreach (var dict in dictionary)
        {
            Console.WriteLine($"[{dict.Key}]:");

            foreach(var item in dict.Value)
            {
                Console.WriteLine($"\t[{dict.Key}]: [{dict.Value}]");
            }
        }
    }
}
