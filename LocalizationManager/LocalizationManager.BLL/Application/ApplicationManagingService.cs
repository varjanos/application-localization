using LocalizationManager.DAL.Context;
using LocalizationManager.Transfer.Application;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LocalizationManager.BLL.Application;

internal class ApplicationManagingService(
    LocalizationDbContext _dbContext,
    ILogger<ApplicationManagingService> _logger) : IApplicationManagingService
{
    public async Task<List<ApplicationDto>> GetRegisteredApplicationsAsync()
    {
        var apps =  await _dbContext.RegisteredApplications.ToListAsync();

        return apps.Select(x => new ApplicationDto
        {
            Id = x.Id,
            AppId = x.AppId,
            AppName = x.AppName,
            SupportedLanguages = [.. x.SupportedLanguages.Split(";")],
        }).ToList();
    }

    public async Task RegisterApplicationAsync(ApplicationDto dto)
    {
        ArgumentNullException.ThrowIfNull(dto);
        ArgumentException.ThrowIfNullOrEmpty(dto.AppId);
        ArgumentException.ThrowIfNullOrEmpty(dto.AppName);
        ArgumentNullException.ThrowIfNull(dto.SupportedLanguages);

        var entity = await _dbContext.RegisteredApplications.SingleOrDefaultAsync(x => x.AppId == dto.AppId);

        if(entity != null) { return; }

        entity = new DAL.Entities.RegisteredApplication
        {
            AppName = dto.AppName,
            AppId = dto.AppId,
            SupportedLanguages = string.Join(";", dto.SupportedLanguages),
        };

        _dbContext.RegisteredApplications.Add(entity);

        await _dbContext.SaveChangesAsync();

        _logger.LogInformation($"[{nameof(ApplicationManagingService)}] Successfully registered application with id: {entity.AppId}");
    }

    public async Task UpdateApplicationAsync(ApplicationDto dto)
    {
        ArgumentNullException.ThrowIfNull(dto);
        ArgumentException.ThrowIfNullOrEmpty(dto.AppId);
        ArgumentException.ThrowIfNullOrEmpty(dto.AppName);
        ArgumentNullException.ThrowIfNull(dto.SupportedLanguages);

        var entity = await _dbContext.RegisteredApplications.SingleAsync(x => x.AppId == dto.AppId);

        entity.AppName = dto.AppName;
        entity.SupportedLanguages = string.Join(";", dto.SupportedLanguages);

        await _dbContext.SaveChangesAsync();

        _logger.LogInformation($"[{nameof(ApplicationManagingService)}] Successfully updated application with id: {entity.AppId}");
    }

    public async Task DeleteApplicationAsync(ApplicationDto dto)
    {
        ArgumentNullException.ThrowIfNull(dto);
        ArgumentException.ThrowIfNullOrEmpty(dto.AppId);

        var entity = await _dbContext.RegisteredApplications.SingleAsync(x => x.AppId == dto.AppId);

        _dbContext.RegisteredApplications.Remove(entity);

        await _dbContext.SaveChangesAsync();

        _logger.LogInformation($"[{nameof(ApplicationManagingService)}] Successfully deleted application with id: {entity.AppId}");
    }
}
