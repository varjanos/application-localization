using LocalizationManager.Transfer.LocalizationDtos;

namespace LocalizationManager.BLL.Localization;

public interface ILocalizationService
{
    Task<List<LocalizationValueDto>> GetLocalizationValuesAsync(string clientId);

    Task AddOrUpdateLocalizationValueAsync(LocalizationValueDto request);

    Task AddOrUpdateLocalizationValuesAsync(List<LocalizationValueDto> requests);
}
