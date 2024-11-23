namespace LocalizationManagerSDK.Options;

public class LocalizationOptions
{
    public string ManagerUrl { get; set; } = null!;

    public string AppName { get; set; } = null!;

    public string AppId { get; set; } = null!;

    public string ResourceFilePath { get; set; } = null!;

    public string SupportedLanguages { get; set; } = null!;
}
