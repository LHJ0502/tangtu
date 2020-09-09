using System;
using System.Collections.Generic;
using System.Text;
using LIU.Framework.Core.Base;

namespace LIU.Framework.Core.Bus
{
    /// <summary>
    /// 服务总线
    /// </summary>
    public class ServiceBus : IServiceBus
    {
        public T Get<T>() where T : class, IContractService
        {
            return AppInstance.Current.Resolve<T>();
        }
    }
}
