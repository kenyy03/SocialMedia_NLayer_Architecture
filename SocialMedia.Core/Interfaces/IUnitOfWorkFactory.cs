using Microsoft.EntityFrameworkCore;
using SocialMedia.Core.Enumerations;

namespace SocialMedia.Core.Interfaces
{
    public interface IUnitOfWorkFactory
    {
        void RegisterUnitOfWork<TDbContext>(UnitOfWorkType unitOfWorkType, Func<TDbContext> context) where TDbContext : DbContext;
        IUnitOfWork CreateUnitOfWork(UnitOfWorkType unitOfWorkType);
    }
}
