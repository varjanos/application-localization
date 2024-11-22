using LocalizationManager.Transfer.LocalizationDtos;

namespace LocalizationManager.BLL.Localization;

public interface ILocalizationService
{
    Task<List<LocalizationValueDto>> GetLocalizationValuesAsync(int clientId);

    Task AddOrUpdateLocalizationValueAsync(LocalizationValueDto request);
}
