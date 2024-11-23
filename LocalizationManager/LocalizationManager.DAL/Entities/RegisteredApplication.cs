namespace LocalizationManager.DAL.Entities;

public class RegisteredApplication
{
    public int Id { get; set; }

    public required string AppId { get; set; } = null!;
    public required string AppName { get; set; } = null!;

    // Contains the languge codes joined by ';'
    public required string SupportedLanguages { get; set; } = null!;
}
