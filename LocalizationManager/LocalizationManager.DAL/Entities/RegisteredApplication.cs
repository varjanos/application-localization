namespace LocalizationManager.DAL.Entities;

public class RegisteredApplication
{
    public int Id { get; set; }
    public string AppName { get; set; } = null!;
    public string AppUrl { get; set; } = null!;

    // Contains the languge codes joined by ';'
    public string SupportedLanguages { get; set; } = null!;
}
