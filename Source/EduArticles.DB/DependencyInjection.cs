using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using EduArticles.BusinessLogic.Interfaces;

namespace EduArticles.DB;

public static class DependencyInjection
{
    public static IServiceCollection AddDataAccess(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<DataContext>(x
            => x.UseNpgsql(connectionString));
        services.AddScoped<IDataContext>(provider
            => provider.GetRequiredService<DataContext>());
        return services;
    }
}
