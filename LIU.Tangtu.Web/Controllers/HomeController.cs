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
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserInfoService userInfoService;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            userInfoService = ServiceBus.Get<IUserInfoService>();
        }

        public IActionResult GetUser()
        {
            return Json(userInfoService.GetUsers(p => true));
        }

        public IActionResult Index()
        {
            _logger.LogInformation("测试日志");
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
