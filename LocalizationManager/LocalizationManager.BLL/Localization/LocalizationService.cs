using LocalizationManager.Transfer.LocalizationDtos;
using LocalizationManager.DAL.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.SignalR;
using LocalizationManager.BLL.Hub;

namespace LocalizationManager.BLL.Localization;

internal class LocalizationService(
    LocalizationDbContext _dbContext,
    IHubContext<LocalizationHub, ILocalizationClient> _hubContext) : ILocalizationService
{
    public async Task<List<LocalizationValueDto>> GetLocalizationValuesAsync(int clientId)
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

        // TODO: send clients the data

        await _dbContext.SaveChangesAsync();
    }
}
