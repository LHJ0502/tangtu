using Autofac.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace LIU.Framework.Core.Base
{

    /// <summary>
    /// 仓储接口
    /// </summary>
    public interface IRepository : IDisposable
    {
    }

    /// <summary>
    /// 泛型仓储接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T> : IRepository where T : class, IEntity
    {
        T FindByKeys(params object[] keys);
        /// <summary> 查询实体 </summary>
        IQueryable<T> Find();
        /// <summary>添加一条记录</summary>
        bool Add(T entity);
        /// <summary> 添加多条记录 </summary>
        int Add(IEnumerable<T> entities);
        /// <summary> 更新一条记录 </summary>
        bool Update(T entity);
        /// <summary> 更新多条记录 </summary>
        int Update(IEnumerable<T> entities);
        /// <summary> 使用指定更新行为，更新指定键的记录 </summary>
        bool Update(object key, Action<T> action);
        /// <summary> 删除一条记录 </summary>
        bool Delete(T entity);
        /// <summary> 根据指定的键删除一条记录 </summary>
        bool Delete(params object[] keys);
        /// <summary> 删除多条记录 </summary>
        int Delete(IEnumerable<T> entities);
        /// <summary> 判断是否存在满足指定条件的记录 </summary>
        bool Exists(Expression<Func<T, bool>> condition);
    }
}
