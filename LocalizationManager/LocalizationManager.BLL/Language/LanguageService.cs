using LocalizationManager.Transfer.LocalizationDtos;
using LocalizationManager.DAL.Context;
using Microsoft.EntityFrameworkCore;

namespace LocalizationManager.BLL.Language;

internal class LanguageService(LocalizationDbContext _dbContext) : ILanguageService
{
    public async Task<List<LanguageDto>> GetAllLanguagesAsync()
    {
        return await _dbContext.Languages
            .Select(x => new LanguageDto { LanguageCode = x.LanguageCode, LanguageName = x.LanguageName })
            .ToListAsync();
    }

    public async Task<List<LanguageDto>> GetSupportedLanguagesForApplicationAsync(int applicationId)
    {
        var app = await _dbContext.RegisteredApplications
            .SingleAsync(x => x.Id == applicationId);

        var languages = await GetAllLanguagesAsync();

        var supportedLangs = app.SupportedLanguages.Split(";").ToList();

        return languages.Where(x => supportedLangs.Contains(x.LanguageCode)).ToList();
    }
}
