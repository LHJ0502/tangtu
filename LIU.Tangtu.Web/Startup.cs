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
                    //ValidateIssuer = true,//�Ƿ���֤Issuer
                    //ValidateAudience = true,//�Ƿ���֤Audience
                    ValidateLifetime = true,//�Ƿ���֤ʧЧʱ��
                    //ClockSkew = TimeSpan.FromSeconds(30),
                    ValidateIssuerSigningKey = true,//�Ƿ���֤SecurityKey
                    ValidAudience = JWTData.Audience,//Audience
                    ValidIssuer = JWTData.Issuer,//Issuer���������ǰ��ǩ��jwt������һ��
                    IssuerSigningKey = new SymmetricSecurityKey(JWTData.SecurityKey)//�õ�SecurityKey
                };
                p.Events = new JwtBearerEvents
                {
                    //�˴�ΪȨ����֤ʧ�ܺ󴥷����¼�  δ��Ȩʱ���á�
                    OnChallenge = context =>
                    {
                        //�˴�����Ϊ��ֹ.Net CoreĬ�ϵķ������ͺ����ݽ�����������ҪŶ������
                        context.HandleResponse();
                        string payload = "";
                        if (context.AuthenticateFailure.Message.IsNotNullOrWhiteSpace())
                            payload = context.AuthenticateFailure.Message;
                        else
                            //�Զ����Լ���Ҫ���ص����ݽ����������Ҫ���ص���Json����ͨ������Newtonsoft.Json�����ת��
                            payload = JsonConvert.SerializeObject(Result.Fail("�ܱ�Ǹ������Ȩ���ʸýӿ�", ResultStatus.ValidateAuthorityFail));
                        //�Զ��巵�ص���������
                        context.Response.ContentType = "application/json";
                        //�Զ��巵��״̬�룬Ĭ��Ϊ401 ������ĳ� 200
                        context.Response.StatusCode = StatusCodes.Status200OK;
                        //context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        //���Json���ݽ��
                        context.Response.WriteAsync(payload);
                        return Task.FromResult(0);
                    },
                    OnAuthenticationFailed = p =>
                    {
                        var payload = JsonConvert.SerializeObject(Result.Fail("��֤ʧ��", ResultStatus.ValidateAuthorityFail));
                        //�Զ��巵�ص���������
                        p.Response.ContentType = "application/json";
                        //�Զ��巵��״̬�룬Ĭ��Ϊ401 ������ĳ� 200
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
                            var payload = JsonConvert.SerializeObject(Result.Fail("����Ȩ���ʸýӿ�" + roleKey, ResultStatus.ValidateAuthorityFail));
                            context.Fail(payload);
                        }

                        return Task.CompletedTask;
                    }
                };
            });
            //services.AddControllersWithViews();
            services.AddMvc(p =>
            {
                p.Filters.Add<GlobalExceptionFilter>();//ע��ȫ�ֹ�����
            }).AddJsonOptions(p =>
            {
                p.JsonSerializerOptions.Converters.Add(new DatetimeJsonConverter());//ע��jsonʱ���ʽ

            });
        }
        public void ConfigureContainer(ContainerBuilder builder)
        {
            // ֱ����Autofacע�������Զ���� 
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

            //ʹ��Ĭ��ҳ
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
