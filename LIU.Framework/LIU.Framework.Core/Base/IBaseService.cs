using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace LIU.Framework.Core.Base
{
    public interface IBaseService : IContractService, IDisposable
    {
    }

    public interface IBaseService<T> : IBaseService where T : class, IEntity
    {
        /// <summary> 按键查找单条记录 </summary>
        T GetByKeys(params object[] keys);
        /// <summary>取所有数据</summary>`
        List<T> GetAll();

        /// <summary> 根据过滤条件获取数据 </summary>
        List<T> Get(Expression<Func<T, bool>> filter = null);

        /// <summary> 根据过滤条件获取一条数据 </summary>
        T GetOne(Expression<Func<T, bool>> filter = null);
              
        /// <summary>添加一条记录</summary>
        bool Add(T model);

        /// <summary>添加多条记录</summary>
        int Add(List<T> models);

        /// <summary> 根据键删除一条数据 </summary>
        bool DeleteByKey(object key);

        /// <summary>
        /// 更新一条记录，需要更新的记录有实体的ID指定,更新内容由指定实体指定，如果该数据不存在，则添加
        /// <remarks>
        /// 没有关系属性的表模型可以使用此方法，否则不建议使用
        /// </remarks></summary>
        bool Update(T model);

        /// <summary>
        /// 新一组记录，需要更新的记录有实体的ID指定,更新内容由指定实体指定，如果该数据不存在，则添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        int Update(List<T> model);
        /// <summary> 判断是否存在满足指定条件的数据 </summary>
        bool Exists(Expression<Func<T, bool>> condition = null);
    }
}
