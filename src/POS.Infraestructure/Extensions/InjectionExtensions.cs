using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using POS.Infraestructure.Persistences.Contexts;

namespace POS.Infraestructure.Extensions
{
    public static class InjectionExtensions
    {
        public static IServiceCollection AddInjectionInfraescture(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var assembly = typeof(POSContext).Assembly.FullName;

            services.AddDbContext<POSContext>(
                    options => options.UseSqlServer(
                        configuration.GetConnectionString("POSConnection"),
                            b => b.MigrationsAssembly(assembly)), ServiceLifetime.Transient);
 
                
            return services;
        }
    }
}
