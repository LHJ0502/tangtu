using LIU.Framework.Core.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace LIU.Framework.Core.Bus
{
    /// <summary>
    /// 仓储总线
    /// </summary>
    public interface IRepositoryBus
    {
        /// <summary>
        /// 根据实体类型，获取一个泛型仓储
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IRepository<T> Get<T>() where T : class, IEntity;
        /// <summary>
        /// 更具指定成仓储接口类型，获取仓储
        /// </summary>
        /// <typeparam name="T">仓储接口类型</typeparam>
        /// <returns></returns>
        T GetRepository<T>() where T : class, IRepository;
    }
}
