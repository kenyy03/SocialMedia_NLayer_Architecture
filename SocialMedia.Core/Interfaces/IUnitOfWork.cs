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
        void RollBack();
        void BeginTransaction();
        List<T> RawSqlQuery<T>(string query, Func<DbDataReader, T> map, params object[] parameters) where T : class;
        void SetCommandTimeout(int seconds);
    }
}
