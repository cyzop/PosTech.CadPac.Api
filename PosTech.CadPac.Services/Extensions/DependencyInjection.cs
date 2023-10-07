using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PosTech.CadPac.Domain.Services;
using PosTech.CadPac.Repository.Extensions;

namespace PosTech.CadPac.Services.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection services,
        IConfiguration configuration)
        {
            services.AddRepositories(configuration);

            //TODO: add services
            services.AddScoped<ICadastroPacienteService, CadastroPacienteService>();

            return services;
        }
    }
}
