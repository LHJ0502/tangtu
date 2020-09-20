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
using Microsoft.AspNetCore.Authorization;

namespace LIU.Tangtu.Web.Controllers
{

    [Route("api/{Controller}")]
    public class HomeController : AuthBaseController
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
            return await Result.OKAsync(CurrentUser);
            // return await Result.OKAsync(userInfoService.GetUsers(p => true));
        }

     

        [Route("test2")]
        [AllowAnonymous]
        public async Task<Result> Test2()
        {
            return await Result.OKAsync(2222);
        }
    }
}
