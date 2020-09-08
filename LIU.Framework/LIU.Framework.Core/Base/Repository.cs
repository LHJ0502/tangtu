using LIU.Framework.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace LIU.Framework.Core.Base
{
    public class Repository : IRepository
    {
        /// <summary>
        /// 库上下文
        /// </summary>
        protected IDbContext Context { get; set; }

        public void Dispose()
        {
            this.Context.Dispose();
        }

        protected Repository(IDbContext dbContext)
        {
            this.Context = dbContext;
        }
    }

    public class Repository<T> : Repository, IRepository<T> where T : class, IEntity
    {
        public Repository(IDbContext dbContext) : base(dbContext)
        { }
        /// <inheritdoc/>
        public bool Add(T entity)
        {
            Context.Add<T>(entity);
            return Context.Commit() > 0;
        }

        /// <inheritdoc/>
        public int Add(IEnumerable<T> entities)
        {
            Context.Add<T>(entities);
            return Context.Commit();
        }

        /// <inheritdoc/>
        public bool Delete(T entity)
        {
            Context.Delete<T>(entity);
            return Context.Commit() > 0;
        }


        /// <inheritdoc/>
        public int Delete(IEnumerable<T> entities)
        {
            Context.Delete<T>(entities);
            return Context.Commit();
        }

        /// <inheritdoc/>
        public bool Exists(Expression<Func<T, bool>> condition)
        {
            return Context.Table<T>().Where(condition).Count() > 0;
        }

        /// <inheritdoc/>
        public IQueryable<T> Find()
        {
            return Context.Table<T>();
        }

        /// <inheritdoc/>
        public T FindByKeys(params object[] keys)
        {
            return Context.FindByKeys<T>(keys);
        }

        /// <inheritdoc/>
        public bool Update(T entity)
        {
            Context.Update<T>(entity);
            return Context.Commit() > 0;
        }

        /// <inheritdoc/>
        public int Update(IEnumerable<T> entities)
        {
            Context.Update<T>(entities);
            return Context.Commit();
        }

    }
}
