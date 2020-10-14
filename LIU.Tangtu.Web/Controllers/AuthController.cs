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

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="loginName"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        [Route("Login")]
        public async Task<Result> Login(string loginName, string pwd)
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
                        issuer: JWTData.Issuer + "$refresh",
                        audience: JWTData.Audience + "$refresh",
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

        /// <summary>
        /// 更新token值
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        [Route("UpdateToken"), HttpPost]
        public async Task<Result> UpdateToken(IDictionary<string, object> model)
        {
            if (model == null)
            {
                return await Result.FailAsync("参数错误");
            }
            var dic = new Dictionary<string, object>(model, StringComparer.OrdinalIgnoreCase);
            string oldtoken = dic["token"].ToString();
            string refreshToken = dic["refreshToken"].ToString();
            //解析旧的token     
            var param = new TokenValidationParameters
            {
                //ValidateIssuer = true,//是否验证Issuer
                //ValidateAudience = true,//是否验证Audience
                ValidateLifetime = false,//是否验证失效时间
                                         //ClockSkew = TimeSpan.FromSeconds(30),
                ValidateIssuerSigningKey = true,//是否验证SecurityKey
                ValidAudience = JWTData.Audience,//Audience
                ValidIssuer = JWTData.Issuer,//Issuer，这两项和前面签发jwt的设置一致
                IssuerSigningKey = new SymmetricSecurityKey(JWTData.SecurityKey)//拿到SecurityKey
            };
            SecurityToken securityToken = new JwtSecurityToken();
            try
            {
                new JwtSecurityTokenHandler().ValidateToken(oldtoken, param, out securityToken);
            }
            catch (Exception ex)
            {
                return await Result.FailAsync("Token验证失败", ResultStatus.TokenFail);
            }
            //验证刷新的token是否正确

            param.ValidateLifetime = true;
            param.ValidAudience = JWTData.Audience + "$refresh";
            param.ValidIssuer = JWTData.Issuer + "$refresh";
            try
            {
                new JwtSecurityTokenHandler().ValidateToken(refreshToken, param, out securityToken);
            }
            catch (Exception ex)
            {
                return await Result.FailAsync("刷新Token验证失败", ResultStatus.RefreshTokenFail);
            }

            var jwttoken = ((JwtSecurityToken)securityToken);
            var key = new SymmetricSecurityKey(JWTData.SecurityKey);
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var accessToken = new JwtSecurityToken(
                      issuer: JWTData.Issuer,
                      audience: JWTData.Audience,
                      claims: jwttoken.Claims,
                      expires: DateTime.Now.AddMinutes(30),
                      signingCredentials: creds);
            return await Result.OKAsync(new
            {
                accessToken = new JwtSecurityTokenHandler().WriteToken(accessToken)
            });
        }

    }
}
