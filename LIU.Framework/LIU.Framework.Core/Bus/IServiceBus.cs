using LIU.Framework.Core.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace LIU.Framework.Core.Bus
{
    /// <summary>
    /// 服务总线
    /// </summary>
    public interface IServiceBus
    {
        T Get<T>() where T : class, IContractService;
    }
}
