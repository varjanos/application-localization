using LocalizationManager.Transfer.LocalizationDtos;

namespace LocalizationManager.BLL.Localization;

public interface ILocalizationService
{
    Task<List<LocalizationValueDto>> GetLocalizationValuesAsync(string appId);

    Task AddLocalizationValueAsync(LocalizationValueDto request);

    Task UpdateLocalizationValueAsync(LocalizationValueDto request);

    Task DeleteLocalizationValueAsync(LocalizationValueDto request);
}
