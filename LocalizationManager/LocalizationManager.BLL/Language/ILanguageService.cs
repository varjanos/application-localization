using LocalizationManager.BLL.Model;

namespace LocalizationManager.BLL.Language;

public interface ILanguageService
{
    public Task<List<LanguageDto>> GetAllLanguagesAsync();
}
