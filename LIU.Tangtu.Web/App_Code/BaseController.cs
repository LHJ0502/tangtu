using LIU.Framework.Core;
using LIU.Framework.Core.Bus;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LIU.Tangtu.Web.App_Code
{
    [ApiController]
    public class BaseController : ControllerBase
    {
        public readonly IServiceBus ServiceBus;

        public BaseController()
        {
            ServiceBus = AppInstance.Current.Resolve<IServiceBus>();
        }



    }
}
