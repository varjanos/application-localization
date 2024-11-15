namespace LocalizationManager.DAL.Entities;

public class LocalizationValue
{
    public int Id { get; set; }

    public required string ClientId { get; set; } = null!;
    public required string Key { get; set; } = null!;
    public required string LanguageCode { get; set; } = null!;

    public required string Value { get; set; } = null!;
}
