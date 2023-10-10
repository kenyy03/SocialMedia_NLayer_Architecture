using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using SocialMedia.Infraestructure.Data;
using SocialMedia.Infraestructure.Repositories.GenericRepository;
using System.Data.Common;
using System.Data;

namespace SocialMedia.Infraestructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;
        private IDbContextTransaction? _transaction;
        
        public UnitOfWork(DbContext context)
        {
            _context = context;
        }

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity
        {
            return new Repository<TEntity>(_context);
        }

        public void Save()
        {
            try
            {
                if(_transaction == null)
                {
                    BeginTransaction();
                    _context.SaveChanges();
                    Commit();
                    return;
                }
                _context.SaveChanges();
            }
            catch (Exception)
            {
                RollBack();
            }
        }

        public async Task SaveAsync()
        {
            try
            {
                if (_transaction == null)
                {
                    BeginTransaction();
                    await _context.SaveChangesAsync();
                    await CommitAsync();
                    return;
                }

                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                RollBack();
            }
        }

        public void BeginTransaction()
        {
            _transaction ??= _context.Database.BeginTransaction();
        }

        public void Commit()
        {
            if (_transaction != null)
            {
                _transaction.Commit();
                _transaction.Dispose();
                _transaction = null;
            }
        }

        public async Task CommitAsync()
        {
            if (_transaction != null)
            {
                await _transaction.CommitAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        public void RollBack()
        {
            if (_transaction != null)
            {
                _transaction.Rollback();
                _transaction.Dispose();
                _transaction = null;
            }
        }

        public void Dispose()
        {
            if (_context != null)
            {
                _context.Dispose();
            }
        }

        public IQueryable<T> RawSqlQuery<T>(string query, Func<DbDataReader, T> map, int timeOut = 30, params object[] parameters) where T : class
        {
            DbCommand command = _context.Database.GetDbConnection().CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = query;
            command.CommandTimeout = timeOut;
            command.Parameters.AddRange(parameters); 
            _context.Database.OpenConnection();

            List<T> result = new List<T>();
            DbDataReader reader= command.ExecuteReader();

            while (reader.Read())
            {
                result.Add(map(reader));
            }

            _context.Database.CloseConnection();
            return result.AsQueryable();
        }

        public void SetCommandTimeout(int seconds)
        {
            _context.Database.SetCommandTimeout(TimeSpan.FromSeconds(seconds));
        }
    }
}
