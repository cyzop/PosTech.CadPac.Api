using PosTech.CadPac.Api.Converter;
using PosTech.CadPac.Api.Models;
using PosTech.CadPac.Domain.Entities;
using PosTech.CadPac.Domain.Shared.Converter;
using PosTech.CadPac.Services.Extensions;

namespace PosTech.CadPac.Api.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDtoConverters();
            services.AddServices(configuration);

            return services;
        }

        public static IServiceCollection AddDtoConverters(this IServiceCollection services)
        {
            services.AddScoped<IConverter<Paciente, PessoaDto>, PacienteToPessoaDtoConverter>();
            services.AddScoped<IConverter<PessoaDto, Paciente>, PessoaDtoConverter>();
            services.AddScoped<IConverter<LancamentoMedicoDto, RegistroMedico>, LancamentoMedicoDtoConverter>();
            services.AddScoped<IConverter<PacienteDto, Paciente>, PacienteDtoConverter>();
            services.AddScoped<IConverter<Paciente, PacienteDto>, PacienteConverter>();
            services.AddScoped<IConverter<RegistroMedico, LancamentoMedicoDto>, LancamentoMedicoConverter>();
            return services;
        }
    }
}
