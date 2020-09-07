using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace LIU.Framework.Core.Data
{
    /// <summary>
    /// EF数据上下文接口
    /// </summary>
    public interface IDbContext : IDisposable
    {
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        string ConnectionString { get; }

        TEntity FindByKeys<TEntity>(params object[] keys) where TEntity : class;

        /// <summary>
        /// 实体集合
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="trackable">是否跟踪对象变化</param>
        /// <returns>实体集合</returns>
        IQueryable<TEntity> Table<TEntity>(bool trackable = false)
            where TEntity : class;

        /// <summary>
        /// 添加多条记录
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entityObjects"></param>
        /// <returns></returns>
        IDbContext Add<TEntity>(IEnumerable<TEntity> entityObjects)
            where TEntity : class;

        /// <summary>
        /// 添加一条记录
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        IDbContext Add<TEntity>(TEntity entity)
            where TEntity : class;
        /// <summary>
        /// 删除多条记录
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entityObjects"></param>
        /// <returns></returns>
        IDbContext Delete<TEntity>(IEnumerable<TEntity> entityObjects)
            where TEntity : class;
        /// <summary>
        /// 删除一条记录
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        IDbContext Delete<TEntity>(TEntity entity)
            where TEntity : class;
        /// <summary>
        /// 更新多条记录
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entityObjects"></param>
        /// <returns></returns>
        IDbContext Update<TEntity>(IEnumerable<TEntity> entityObjects)
            where TEntity : class;
        /// <summary>
        /// 更新一条记录
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        IDbContext Update<TEntity>(TEntity entity)
            where TEntity : class;

        /// <summary>
        /// 同步保存数据
        /// </summary>
        /// <returns>影响行数</returns>
        int Commit();
    }
}
