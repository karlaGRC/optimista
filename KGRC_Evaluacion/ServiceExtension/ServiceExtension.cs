using KGRC_Evaluacion.Conexion;
using KGRC_Evaluacion.Conexion.Interface;
using KGRC_Evaluacion.Interface;
using KGRC_Evaluacion.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace KGRC_Evaluacion.ServiceExtension
{
    public static class ServiceExtension 
    {
        public static IServiceCollection AddDIServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CoctelDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("CoctelConnection"));
            });
          
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ICoctelRepository, CoctelRepository>();
            services.AddScoped<IUsuariosRepository, UsuariosRepository>();
            services.AddTransient<IConexionContext, ConexionContext>();
            return services;
        }
    }
}
