using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PosTech.CadPac.Domain.Entities;
using PosTech.CadPac.Domain.Repositories;
using PosTech.CadPac.Domain.Shared.Converter;
using PosTech.CadPac.Repository.DataModel;

namespace PosTech.CadPac.Repository.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services,
        IConfiguration configuration)
        {
            services.AddConvertes();
            
            
            var repositorySettings = new RepositorySettings();
            configuration.GetSection("RepositorySettings").Bind(repositorySettings);

            
            services.AddSingleton<IPacienteRepository, PacienteRepository>();
            return services;
        }

        public static IServiceCollection AddConvertes(this IServiceCollection services)
        {
            services.AddScoped<IConverter<RegistroMedicoDataModel, RegistroMedico>>();
            services.AddScoped<IConverter<RegistroMedico, RegistroMedicoDataModel>>();
            services.AddScoped<IConverter<PacienteDataModel, Paciente>>();
            services.AddScoped<IConverter<Paciente, PacienteDataModel>>();

            return services;
        }
    }
}
