using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace MigrationRuntime
{
    public static class ServiceExtensions
    {
        public static IServiceCollection RegisterEFCore(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("MsSql"), builder =>
                {
                    builder.MigrationsAssembly(Assembly.GetAssembly(typeof(AppDbContext)).GetName().Name);
                });
            });

            return services;
        }
    }
}
