using Application.Interfaces;
using Application.Mappings;
using Application.Services;
using Microsoft.Extensions.Configuration;
using Domain.Interfaces;
using Infrastructure.Repository;
using Microsoft.Extensions.DependencyInjection;
using Application;
using Infrastructure;

namespace WebAPI.Installers
{
    public class MvcInstaller : IInstaller
    {      
        public void InstallServices(IServiceCollection services, IConfiguration Configuration)
        {
            services.AddApplication();
            services.AddInfrastructure();
            services.AddControllers();            
        }
    }
}
