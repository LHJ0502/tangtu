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
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public int Add(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public bool Delete(T entity)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public bool Delete(params object[] keys)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public int Delete(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public bool Exists(Expression<Func<T, bool>> condition)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public IQueryable<T> Find()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public T FindByKeys(params object[] keys)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public bool Update(T entity)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public int Update(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public bool Update(object key, Action<T> action)
        {
            throw new NotImplementedException();
        }
    }
}
