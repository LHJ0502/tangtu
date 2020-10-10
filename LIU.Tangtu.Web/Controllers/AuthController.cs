using LIU.Framework.Common;
using LIU.Framework.Common.Extend;
using LIU.Tangtu.IServices.Sys;
using LIU.Tangtu.Web.App_Code;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace LIU.Tangtu.Web.Controllers
{

    [Route("api/{Controller}")]
    public class AuthController : BaseController
    {
        readonly IUserInfoService userInfoService;
        public AuthController()
        {
            userInfoService = ServiceBus.Get<IUserInfoService>();
        }

        [Route("Login")]
        public async Task<Result> Get(string loginName, string pwd)
        {

            if (loginName.IsNotNullOrWhiteSpace() && pwd.IsNotNullOrWhiteSpace())
            {
                var user = userInfoService.GetOne(p => p.sLoginName == loginName);
                if (user != null && user.sPassWord == CryptoHelper.MD5Encrypt(pwd))
                {
                    user.LastDateTime = DateTime.Now;
                    userInfoService.Update(user);
                    user.sPassWord = null;
                    var claims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Nbf,$"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}") ,
                        //new Claim (JwtRegisteredClaimNames.Exp,$"{new DateTimeOffset(DateTime.Now.AddMinutes(30)).ToUnixTimeSeconds()}"),
                        new Claim("gKey",user.gKey.ToString()),
                        new Claim("sRoleKey",user.sRoleKey)
                    };
                    var key = new SymmetricSecurityKey(JWTData.SecurityKey);
                    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var accessToken = new JwtSecurityToken(
                        issuer: JWTData.Issuer,
                        audience: JWTData.Audience,
                        claims: claims,
                        expires: DateTime.Now.AddMinutes(30),
                        signingCredentials: creds);
                    var refreshToken = new JwtSecurityToken(
                        issuer: JWTData.Issuer + JWTData.Issuer,
                        audience: JWTData.Audience,
                        claims: claims,
                        expires: DateTime.Now.AddDays(30),
                        signingCredentials: creds);
                    return await Result.OKAsync(new
                    {
                        accessToken = new JwtSecurityTokenHandler().WriteToken(accessToken),
                        refreshToken = new JwtSecurityTokenHandler().WriteToken(refreshToken),
                        user
                    });
                }
                else
                {
                    return await Result.FailAsync("用户不存在");
                }
            }
            else
            {
                return await Result.FailAsync("参数错误");
            }
        }


    }
}
