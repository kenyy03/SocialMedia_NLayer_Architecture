using Microsoft.Extensions.DependencyInjection;
using SocialMedia.Core.Interfaces;

namespace SocialMedia.Infraestructure.Repositories
{
    public class UnitOfWorkBuilder : IUnitOfWorkBuilder
    {
        private readonly IServiceCollection _services;

        public UnitOfWorkBuilder(IServiceCollection services)
        {
            _services = services;
        }

        public IServiceCollection Services => _services;
    }
}
