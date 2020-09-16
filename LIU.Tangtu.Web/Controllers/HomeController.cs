using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using LIU.Tangtu.Web.Models;
using LIU.Tangtu.Web.App_Code;
using LIU.Tangtu.IServices.Sys;

namespace LIU.Tangtu.Web.Controllers
{

    [Route("api/{Controller}")]
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserInfoService userInfoService;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            userInfoService = ServiceBus.Get<IUserInfoService>();
        }

        [Route("GetUser")]
        public async Task<Result> GetUser()
        {
            return await Result.OKAsync(userInfoService.GetUsers(p => true));
        }


    }
}
