﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using LIU.Tangtu.Domian.Sys;
using LIU.Framework.Common.Extend;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using LIU.Tangtu.IServices.Sys;

namespace LIU.Tangtu.Web.App_Code
{

    /// <summary>
    /// 带验证的基础控制器
    /// </summary>
    [Authorize]
    public class AuthBaseController : BaseController
    {
        /// <summary>
        /// 当前用户
        /// </summary>
        public override UserInfo CurrentUser
        {
            get
            {
                var authHeader = this.HttpContext.Request.Headers["Authorization"].ToString();
                if (authHeader.IsNullOrWhiteSpace())
                    return null;
                string tokenStr = authHeader.Replace("Bearer ", "");
                var handler = new JwtSecurityTokenHandler();
                var payload = handler.ReadJwtToken(tokenStr).Payload;
                var claims = payload.Claims;
                var gkey = Convert.ToInt64(claims.First(p => p.Type == "gKey").Value);
                return ServiceBus.Get<IUserInfoService>().GetOne(p => p.gKey == gkey);
            }
        }


    }
}
