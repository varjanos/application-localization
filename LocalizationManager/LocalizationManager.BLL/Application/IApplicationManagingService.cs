using LocalizationManager.Transfer.Application;

namespace LocalizationManager.BLL.Application;

public interface IApplicationManagingService
{
    Task<List<ApplicationDto>> GetRegisteredApplicationsAsync();

    Task RegisterApplicationAsync(ApplicationDto dto);

    Task UpdateApplicationAsync(ApplicationDto dto);

    Task DeleteApplicationAsync(ApplicationDto dto);
}
