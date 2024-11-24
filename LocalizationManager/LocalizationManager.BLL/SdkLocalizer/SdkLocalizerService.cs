using LocalizationManager.DAL.Context;
using Microsoft.EntityFrameworkCore;

namespace LocalizationManager.BLL.SdkLocalizer;

internal class SdkLocalizerService(LocalizationDbContext _dbContext) : ISdkLocalizerService
{
    public async Task<Dictionary<string, Dictionary<string, string>>> GetLocalizationsAsync(string appId)
    {
        var data = await _dbContext.LocalizationValues
            .Where(x => x.AppId == appId)
            .ToListAsync();

        var localizationDictionary = data
            .GroupBy(lv => lv.LanguageCode)
            .ToDictionary(
                group => group.Key,
                group => group
                    .GroupBy(lv => lv.Key)
                    .ToDictionary(
                        subgroup => subgroup.Key,
                        subgroup => subgroup.First().Value)
            );

        return localizationDictionary;

    }
}
