using LocalizationManagerSDK.Localizer;
using LocalizationManagerSDK.Options;
using System.Resources;

namespace LocalizationManagerSDK.ResourceHandler;

public class ResourceHandlerService(
    LocalizationOptions localizationOptions) : IResourceHandlerService
{
    public void HandleLocalizationAdded(string language, string key, string value)
    {
        Console.WriteLine("TODO");
    }

    public void HandleLocalizationUpdated(string language, string key, string value)
    {
        Console.WriteLine("HandleLocalizationUpdated called!");

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
        Console.WriteLine("HandleLocalizationDeleted called!");

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
}
