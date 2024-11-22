using LocalizationManager.Transfer.LocalizationDtos;

namespace LocalizationManager.BLL.Language;

public interface ILanguageService
{
    public Task<List<LanguageDto>> GetAllLanguagesAsync();

    public Task<List<LanguageDto>> GetSupportedLanguagesForApplicationAsync(int applicationId);
}
