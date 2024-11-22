using LocalizationManager.DAL.Context;
using LocalizationManager.Transfer.Application;
using Microsoft.EntityFrameworkCore;

namespace LocalizationManager.BLL.Application;

internal class ApplicationManagingService(LocalizationDbContext _dbContext) : IApplicationManagingService
{
    public async Task<List<ApplicationDto>> GetRegisteredApplicationsAsync()
    {
        var apps =  await _dbContext.RegisteredApplications.ToListAsync();

        return apps.Select(x => new ApplicationDto
        {
            Id = x.Id,
            AppName = x.AppName,
            AppUrl = x.AppUrl,
            SupportedLanguages = [.. x.SupportedLanguages.Split(";")],
        }).ToList();
    }

    public async Task RegisterApplicationAsync(ApplicationDto dto)
    {
        var entity = await _dbContext.RegisteredApplications
            .Where(x => x.AppUrl == dto.AppUrl)
            .SingleOrDefaultAsync();

        if(entity != null) { return; }

        entity = new DAL.Entities.RegisteredApplication
        {
            AppName = dto.AppName,
            AppUrl = dto.AppUrl,
            SupportedLanguages = string.Join(";", dto.SupportedLanguages),
        };

        _dbContext.RegisteredApplications.Add(entity);

        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateApplicationAsync(ApplicationDto dto)
    {
        var entity = await _dbContext.RegisteredApplications
            .Where(x => dto.Id == x.Id)
            .SingleAsync();

        entity.AppUrl = dto.AppUrl;
        entity.AppName = dto.AppName;
        entity.SupportedLanguages = string.Join(";", dto.SupportedLanguages);

        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteApplicationAsync(ApplicationDto dto)
    {
        var entity = await _dbContext.RegisteredApplications
            .Where(x => dto.Id == x.Id)
            .SingleAsync();

        _dbContext.RegisteredApplications.Remove(entity);

        await _dbContext.SaveChangesAsync();
    }
}
