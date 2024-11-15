using LocalizationManager.DAL.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace LocalizationManager.DAL;

public static class WireUp
{
    public static void ConfigureLocalizationDb(this IServiceCollection service, string connectionString)
    {
        service.AddDbContext<LocalizationDbContext>(options => options.UseSqlServer(connectionString));
    }
}
