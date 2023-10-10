using SocialMedia.Core.Entities;
using System.Data.Common;

namespace SocialMedia.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity;
        void Save();
        Task SaveAsync();
        void Commit();
        Task CommitAsync();
        void RollBack();
        void BeginTransaction();
        IQueryable<T> RawSqlQuery<T>(string query, Func<DbDataReader, T> map, int timeOut=30, params object[] parameters) where T : class;
        void SetCommandTimeout(int seconds);
    }
}
