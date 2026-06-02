using Application.Services;
using Application.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Application.DependencyInjection;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IBlogService, BlogService>();
        services.AddScoped<ICategoryService, CategoryService>();

        services.AddAutoMapper(cfg =>
        {
            cfg.AddProfile(typeof(Application.Mapper.DomainProfile));
        });

        return services;
    }
}