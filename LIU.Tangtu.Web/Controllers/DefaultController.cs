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
    [Route("{contriller}")]
    public class DefaultController : BaseController
    {
        private readonly ILogger<DefaultController> _logger;
        public DefaultController(ILogger<DefaultController> logger)
        {
            _logger = logger;
        }
        [Route("index")]
        public IActionResult Index()
        {
            _logger.LogInformation("服务启动成功(〃'▽'〃)");
            return Ok("服务启动成功(〃'▽'〃)");
        }
    }
}
