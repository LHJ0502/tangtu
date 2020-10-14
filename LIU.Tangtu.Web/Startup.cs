using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using LIU.Framework.Common.Extend;
using LIU.Framework.Core;
using LIU.Tangtu.IServices.Sys;
using LIU.Tangtu.Web.App_Code;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace LIU.Tangtu.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(p =>
            {
                p.TokenValidationParameters = new TokenValidationParameters
                {
                    //ValidateIssuer = true,//是否验证Issuer
                    //ValidateAudience = true,//是否验证Audience
                    ValidateLifetime = true,//是否验证失效时间
                    //ClockSkew = TimeSpan.FromSeconds(30),
                    ValidateIssuerSigningKey = true,//是否验证SecurityKey
                    ValidAudience = JWTData.Audience,//Audience
                    ValidIssuer = JWTData.Issuer,//Issuer，这两项和前面签发jwt的设置一致
                    IssuerSigningKey = new SymmetricSecurityKey(JWTData.SecurityKey)//拿到SecurityKey
                };
                p.Events = new JwtBearerEvents
                {
                    //此处为权限验证失败后触发的事件  未授权时调用。
                    OnChallenge = context =>
                    {
                        //此处代码为终止.Net Core默认的返回类型和数据结果，这个很重要哦，必须
                        context.HandleResponse();
                        string payload = "";
                        if (context.AuthenticateFailure.Message.IsNotNullOrWhiteSpace())
                            payload = context.AuthenticateFailure.Message;
                        else
                            //自定义自己想要返回的数据结果，我这里要返回的是Json对象，通过引用Newtonsoft.Json库进行转换
                            payload = JsonConvert.SerializeObject(Result.Fail("很抱歉，您无权访问该接口", ResultStatus.ValidateAuthorityFail));
                        //自定义返回的数据类型
                        context.Response.ContentType = "application/json";
                        //自定义返回状态码，默认为401 我这里改成 200
                        context.Response.StatusCode = StatusCodes.Status200OK;
                        //context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        //输出Json数据结果
                        context.Response.WriteAsync(payload);
                        return Task.FromResult(0);
                    },
                    OnAuthenticationFailed = p =>
                    {
                        var payload = JsonConvert.SerializeObject(Result.Fail("验证失败", ResultStatus.ValidateAuthorityFail));
                        //自定义返回的数据类型
                        p.Response.ContentType = "application/json";
                        //自定义返回状态码，默认为401 我这里改成 200
                        p.Response.StatusCode = StatusCodes.Status200OK;
                        p.Response.WriteAsync(payload);
                        return Task.CompletedTask;
                    },
                    OnTokenValidated = context =>
                    {
                        //string roleKey = (p.SecurityToken as JwtSecurityToken).Claims.First(p => p.Type == "sRoleKey").Value;
                        var roleKey = context.Principal.Claims.First(p => p.Type == "sRoleKey").Value.Split(',').ToList();
                        var rolekeys= roleKey.ConvertAll(p => Convert.ToInt64(p));
                        var roleMenuService = AppInstance.Current.Resolve<IRoleMenuService>();

                        if (!roleMenuService.CheckRole(rolekeys, context.HttpContext.Request.Path))
                        {
                            var payload = JsonConvert.SerializeObject(Result.Fail("您无权访问该接口" + roleKey, ResultStatus.ValidateAuthorityFail));
                            context.Fail(payload);
                        }

                        return Task.CompletedTask;
                    }
                };
            });
            //services.AddControllersWithViews();
            services.AddMvc(p =>
            {
                p.Filters.Add<GlobalExceptionFilter>();//注册全局过滤器
            }).AddJsonOptions(p =>
            {
                p.JsonSerializerOptions.Converters.Add(new DatetimeJsonConverter());//注册json时间格式

            });
        }
        public void ConfigureContainer(ContainerBuilder builder)
        {
            // 直接用Autofac注册我们自定义的 
            AppInstance.Current.AppBuilder(builder);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseAuthentication();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            //app.UseHttpsRedirection();

            //使用默认页
            DefaultFilesOptions defaultFilesOptions = new DefaultFilesOptions();
            defaultFilesOptions.DefaultFileNames.Clear();
            defaultFilesOptions.DefaultFileNames.Add("Main.html");
            app.UseDefaultFiles(defaultFilesOptions);
            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Default}/{action=Index}/{id?}");
            });


            AppInstance.Current.SetContainer(app.ApplicationServices.GetAutofacRoot());
        }
    }


    public class DatetimeJsonConverter : JsonConverter<DateTime>
    {
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                if (DateTime.TryParse(reader.GetString(), out DateTime date))
                    return date;
            }
            return reader.GetDateTime();
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString("yyyy-MM-dd HH:mm:ss"));
        }
    }
}
