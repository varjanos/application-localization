namespace LocalizationManager.Transfer.LocalizationDtos;

public class LocalizationValueDto
{
    public int ClientId { get; set; }
    public string Key { get; set; } = null!;
    public string LanguageCode { get; set; } = null!;

    public string Value { get; set; } = null!;
}
