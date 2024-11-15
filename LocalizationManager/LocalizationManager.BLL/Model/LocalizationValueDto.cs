namespace LocalizationManager.BLL.Model;

public class LocalizationValueDto
{
    public string ClientId { get; set; } = null!;
    public string Key { get; set; } = null!;
    public string LanguageCode { get; set; } = null!;

    public string Value { get; set; } = null!;
}
