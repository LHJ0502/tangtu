using LIU.Framework.Core.Bus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace LIU.Framework.Core.Base
{
    public class BaseService : IBaseService
    {
        /// <summary>
        /// 仓储总线
        /// </summary>
        protected readonly IRepositoryBus RepositoryBus;
        /// <summary>
        /// 构造函数
        /// </summary>
        protected BaseService()
        {
            this.RepositoryBus = AppInstance.Current.Resolve<IRepositoryBus>();
        }

        public virtual void Dispose()
        {

        }
    }

    /// <summary>
    /// 基础服务类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseService<T> : BaseService, IBaseService<T>, IDisposable
    where T : class, IEntity
    {
        /// <summary>
        /// 当前服务需要的仓储
        /// </summary>
        protected readonly IRepository<T> repository;
        /// <summary>
        /// 构造函数
        /// </summary>
        protected BaseService()
        {
            this.repository = this.RepositoryBus.Get<T>();
        }
        /// <inheritdoc />
        public T GetByKeys(params object[] keys)
        {
            var entity = repository.FindByKeys(keys);
            return entity;
        }
        /// <inheritdoc />
        public virtual List<T> GetAll()
        {
            return repository.Find().ToList();
        }
        /// <inheritdoc />
        public virtual List<T> Get(Expression<Func<T, bool>> filter = null)
        {
            var query = repository.Find();
            if (filter != null)
            {
                query = query.Where(filter);
            }
            return query.ToList();
        }
        /// <inheritdoc />
        public virtual T GetOne(Expression<Func<T, bool>> filter = null)
        {
            var entity = filter != null
                ? repository.Find().FirstOrDefault(filter)
                : repository.Find().FirstOrDefault();
            return entity;
        }
       

        /// <inheritdoc />
        public virtual bool Add(T model)
        {
            return repository.Add(model);
        }

        /// <inheritdoc />
        public int Add(List<T> models)
        {
            return repository.Add(models);
        }
        /// <inheritdoc />
        public virtual bool DeleteByKey(object key)
        {
            var entity = repository.FindByKeys(key);
            return repository.Delete(entity);
        }
        /// <inheritdoc />
        public virtual bool Update(T model)
        {
            return repository.Update(model);
        }

        /// <inheritdoc />
        public virtual int Update(List<T> model)
        {
            return repository.Update(model);
        }
        /// <inheritdoc />
        public bool Exists(Expression<Func<T, bool>> condition = null)
        {
            return repository.Exists(condition);
        }

        public override void Dispose()
        {
            this.repository.Dispose();
            base.Dispose();
        }
    }
}
