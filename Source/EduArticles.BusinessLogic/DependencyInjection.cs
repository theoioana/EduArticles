using MediatR;
using Microsoft.Extensions.DependencyInjection;
using EduArticles.BusinessLogic.Behaviours;
using System.Reflection;

namespace EduArticles.BusinessLogic;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>));
        services.AddMediatR(Assembly.GetExecutingAssembly());
        return services;
    }
}
