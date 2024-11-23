namespace LocalizationManager.Transfer.Application;

public class ApplicationDto
{
    public int Id { get; set; }

    public string AppId { get; set; } = null!;
    public string AppName { get; set; } = null!;
    
    public List<string> SupportedLanguages { get; set; } = [];
}
