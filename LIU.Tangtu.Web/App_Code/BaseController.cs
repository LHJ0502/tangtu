using LIU.Framework.Core;
using LIU.Framework.Core.Bus;
using LIU.Tangtu.Domian.Sys;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LIU.Tangtu.Web.App_Code
{
    /// <summary>
    /// 基础控制器
    /// </summary>
    [ApiController]
    public class BaseController : ControllerBase
    {
        public readonly IServiceBus ServiceBus;

        public BaseController()
        {
            ServiceBus = AppInstance.Current.Resolve<IServiceBus>();
        }

        /// <summary>
        /// 当前用户
        /// </summary>
        public virtual UserInfo CurrentUser { get; }
    }
}
