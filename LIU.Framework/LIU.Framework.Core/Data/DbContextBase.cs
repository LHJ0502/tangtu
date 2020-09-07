using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace LIU.Framework.Core.Data
{
    public class DbContextBase : DbContext, IDbContext
    {

        public DbContextBase(DbContextOptions<DbContextBase> options) : base(options)
        {
            this.dbConnection = Database.GetDbConnection();
            this.ConnectionString = dbConnection.ConnectionString;
        }

        public string ConnectionString { get; }

        public DbConnection dbConnection { get; }

        public IDbContext Add<TEntity>(IEnumerable<TEntity> entityObjects) where TEntity : class
        {
            foreach (var item in entityObjects)
            {
                this.Add<TEntity>(item);
            }
            return this;
        }

        public int Commit()
        {
            return SaveChanges();
        }

        public IDbContext Delete<TEntity>(IEnumerable<TEntity> entityObjects) where TEntity : class
        {
            foreach (var item in entityObjects)
            {
                Delete(item);
            }
            return this;
        }

        public IDbContext Delete<TEntity>(TEntity entity) where TEntity : class
        {
            if (entity != null)
            {
                Set<TEntity>().Attach(entity);
                Set<TEntity>().Remove(entity);
            }
            return this;
        }

        public TEntity FindByKeys<TEntity>(params object[] keys) where TEntity : class
        {
            return this.Set<TEntity>().Find(keys);
        }


        public IQueryable<TEntity> Table<TEntity>(bool trackable = false) where TEntity : class
        {
            if (trackable)
            {
                return Set<TEntity>();
            }
            else
            {
                return Set<TEntity>().AsNoTracking();
            }
        }

        public IDbContext Update<TEntity>(IEnumerable<TEntity> entityObjects) where TEntity : class
        {
            foreach (var item in entityObjects)
            {
                this.Update<TEntity>(item);
            }
            return this;
        }

        IDbContext IDbContext.Add<TEntity>(TEntity entity)
        {
            this.Add<TEntity>(entity);
            return this;
        }

        IDbContext IDbContext.Update<TEntity>(TEntity entity)
        {
            this.Update<TEntity>(entity);
            return this;
        }
    }
}
