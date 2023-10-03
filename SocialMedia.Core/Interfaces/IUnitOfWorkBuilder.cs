using Microsoft.Extensions.DependencyInjection;

namespace SocialMedia.Core.Interfaces
{
    public interface IUnitOfWorkBuilder
    {
        IServiceCollection Services { get; }
    }
}
