using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PosTech.CadPac.Domain.Entities;
using PosTech.CadPac.Domain.Repositories;
using PosTech.CadPac.Domain.Shared.Converter;
using PosTech.CadPac.Repository.Converter;
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
            services.AddScoped(r => repositorySettings);
            
            services.AddScoped<IPacienteRepository, PacienteRepository>();
            return services;
        }

        public static IServiceCollection AddConvertes(this IServiceCollection services)
        {
            services.AddScoped<IConverter<RegistroMedicoDataModel, RegistroMedico>, RegistroMedicoDataModelConverter>();
            services.AddScoped<IConverter<RegistroMedico, RegistroMedicoDataModel>, RegistroMedicoConverter>();
            services.AddScoped<IConverter<PacienteDataModel, Paciente>, PacienteDataModelConverter>();
            services.AddScoped<IConverter<Paciente, PacienteDataModel>, PacienteConverter>();

            return services;
        }
    }
}
