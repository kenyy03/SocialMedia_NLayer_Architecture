using Microsoft.Extensions.DependencyInjection;
using SocialMedia.Core.Interfaces;
using SocialMedia.Infraestructure.Repositories;

namespace SocialMedia.Infraestructure.Extensions
{
    public static class UnitOfWorkBuilderExtensions
    {
        public static void AddUnitOfWorkBuilder(this IServiceCollection services, Action<IUnitOfWorkFactory, IServiceProvider> configure)
        {
            services.AddScoped((Func<IServiceProvider, IUnitOfWorkFactory>)delegate (IServiceProvider resolver) {
                UnitOfWorkFactory unitOfWorkFactory = new UnitOfWorkFactory();
                configure(unitOfWorkFactory, resolver);
                return unitOfWorkFactory;
            });
        }
    }
}
