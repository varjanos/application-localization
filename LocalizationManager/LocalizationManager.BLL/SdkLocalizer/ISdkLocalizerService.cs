namespace LocalizationManager.BLL.SdkLocalizer;

public interface ISdkLocalizerService
{
    Task<Dictionary<string, Dictionary<string, string>>> GetLocalizationsAsync(string appId);
}
