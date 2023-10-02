using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace PosTech.CadPac.Repository.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services,
        IConfiguration configuration)
        {
            //TODO: converters
            //TODO: config
            //TODO: respotiroty
            return services;
        }
    }
}
