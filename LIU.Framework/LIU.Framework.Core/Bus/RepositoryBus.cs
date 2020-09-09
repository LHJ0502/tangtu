using LIU.Framework.Core.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace LIU.Framework.Core.Bus
{
    /// <summary>
    /// 仓储总线
    /// </summary>
    public class RepositoryBus : IRepositoryBus
    {
        /// <inheritdoc />
        public IRepository<T> Get<T>() where T : class, IEntity
        {
            return AppInstance.Current.Resolve<IRepository<T>>();
        }

        /// <inheritdoc />
        public T GetRepository<T>() where T : class, IRepository
        {
            return AppInstance.Current.Resolve<T>();
        }
    }
}
