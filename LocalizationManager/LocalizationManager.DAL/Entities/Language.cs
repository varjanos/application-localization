namespace LocalizationManager.DAL.Entities;

public class Language
{
    public int Id { get; set; }
    public required string LanguageName { get; set; } = null!;
    public required string LanguageCode { get; set; } = null!;
}
