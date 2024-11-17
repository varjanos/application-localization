using LocalizationManager.Transfer.LocalizationDtos;
using LocalizationManager.DAL.Context;
using Microsoft.EntityFrameworkCore;

namespace LocalizationManager.BLL.Localization;

internal class LocalizationService(LocalizationDbContext _dbContext) : ILocalizationService
{
    public async Task<List<LocalizationValueDto>> GetLocalizationValuesAsync(string clientId)
    {
        return await _dbContext.LocalizationValues
            .Where(x => x.ClientId == clientId)
            .Select(x => new LocalizationValueDto
            {
                ClientId = clientId,
                LanguageCode = x.LanguageCode,
                Key = x.Key,
                Value = x.Value
            }).ToListAsync();
    }

    public async Task AddOrUpdateLocalizationValueAsync(LocalizationValueDto request)
    {
        var value = await _dbContext.LocalizationValues
            .Where(x => x.ClientId == request.ClientId)
            .Where(x => x.LanguageCode == request.LanguageCode)
            .Where(x => x.Key == request.Key)
            .SingleOrDefaultAsync();

        if (value == null)
        {
            value = new DAL.Entities.LocalizationValue
            {
                LanguageCode = request.LanguageCode,
                Key = request.Key,
                ClientId = request.ClientId,
                Value = request.Value
            };
            _dbContext.LocalizationValues.Add(value);
        }
        else
        {
            value.Value = request.Value;
        }

        await _dbContext.SaveChangesAsync();
    }

    public Task AddOrUpdateLocalizationValuesAsync(List<LocalizationValueDto> values)
    {
        throw new NotImplementedException();
    }
}
