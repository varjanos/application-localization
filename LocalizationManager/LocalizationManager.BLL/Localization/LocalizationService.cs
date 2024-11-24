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
    public async Task<List<LocalizationValueDto>> GetLocalizationValuesAsync(string appId)
    {
        return await _dbContext.LocalizationValues
            .Where(x => x.AppId == appId)
            .Select(x => new LocalizationValueDto
            {
                AppId = x.AppId,
                LanguageCode = x.LanguageCode,
                Key = x.Key,
                Value = x.Value
            }).ToListAsync();
    }

    public async Task AddLocalizationValueAsync(LocalizationValueDto request)
    {
        ArgumentNullException.ThrowIfNull(request);
        ArgumentException.ThrowIfNullOrEmpty(request.AppId);
        ArgumentException.ThrowIfNullOrEmpty(request.Key);
        ArgumentException.ThrowIfNullOrEmpty(request.LanguageCode);
        ArgumentException.ThrowIfNullOrEmpty(request.Value);

        var value = await _dbContext.LocalizationValues
            .Where(x => x.AppId == request.AppId)
            .Where(x => x.LanguageCode == request.LanguageCode)
            .Where(x => x.Key == request.Key)
            .SingleOrDefaultAsync();

        if (value != null) { throw new ArgumentException("Duplicate value!"); }

        value = new DAL.Entities.LocalizationValue
        {
            AppId = request.AppId,
            Key = request.Key,
            LanguageCode = request.LanguageCode,
            Value = request.Value,
        };

        _dbContext.LocalizationValues.Add(value);

        await _dbContext.SaveChangesAsync();

        await _hubContext.Clients.Group(value.AppId).SendLocalizationAdded(value.LanguageCode, value.Key, value.Value);
    }

    public async Task UpdateLocalizationValueAsync(LocalizationValueDto request)
    {
        ArgumentNullException.ThrowIfNull(request);
        ArgumentException.ThrowIfNullOrEmpty(request.AppId);
        ArgumentException.ThrowIfNullOrEmpty(request.Key);
        ArgumentException.ThrowIfNullOrEmpty(request.LanguageCode);
        ArgumentException.ThrowIfNullOrEmpty(request.Value);

        var value = await _dbContext.LocalizationValues
            .Where(x => x.AppId == request.AppId)
            .Where(x => x.LanguageCode == request.LanguageCode)
            .Where(x => x.Key == request.Key)
            .SingleAsync();

        value.Value = request.Value;

        await _dbContext.SaveChangesAsync();

        await _hubContext.Clients.Group(value.AppId).SendLocalizationUpdated(value.LanguageCode, value.Key, value.Value);
    }

    public async Task DeleteLocalizationValueAsync(LocalizationValueDto request)
    {
        ArgumentNullException.ThrowIfNull(request);
        ArgumentException.ThrowIfNullOrEmpty(request.AppId);
        ArgumentException.ThrowIfNullOrEmpty(request.Key);
        ArgumentException.ThrowIfNullOrEmpty(request.LanguageCode);

        var value = await _dbContext.LocalizationValues
            .Where(x => x.AppId == request.AppId)
            .Where(x => x.LanguageCode == request.LanguageCode)
            .Where(x => x.Key == request.Key)
            .SingleAsync();

        _dbContext.LocalizationValues.Remove(value);

        await _dbContext.SaveChangesAsync();

        await _hubContext.Clients.Group(value.AppId).SendLocalizationDeleted(value.LanguageCode, value.Key);
    }
}
