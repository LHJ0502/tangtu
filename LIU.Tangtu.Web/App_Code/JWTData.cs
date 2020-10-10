using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System.Text;

namespace LIU.Tangtu.Web.App_Code
{
    public class JWTData
    {
        public static IConfiguration Configuration { get; set; }
        static JWTData()
        {
            //ReloadOnChange = true 当appsettings.json被修改时重新加载            
            Configuration = new ConfigurationBuilder()
            .Add(new JsonConfigurationSource { Path = "appsettings.json", ReloadOnChange = true })
            .Build();


        }

        /// <summary>
        /// 发布者
        /// </summary>
        public static string Issuer
        {
            get
            {
                return Configuration["JWTSetting:issuer"] ?? "LiuHaojie";
            }
        }

        /// <summary>
        /// 受众
        /// </summary>
        public static string Audience
        {
            get
            {
                return Configuration["JWTSetting:audience"] ?? "LiuHaojie";
            }
        }

        /// <summary>
        /// 秘钥
        /// </summary>
        public static byte[] SecurityKey
        {
            get
            {
                return Encoding.UTF8.GetBytes(Configuration["JWTSetting:SecurityKey"] ?? key);
            }
        }

        /// <summary>
        /// 秘钥
        /// </summary>
        private const string key = "MIGfMLiuHaojieA0GCSqGSIb3DQEBAQUAA4GNADCBLiuHaojieiQKBgQDI2a2EJ7m872v0afLiuHaojieyoSDJT2o1";
        //MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQDI2a2EJ7m872v0afyoSDJT2o1+SitIeJSWtLJU8/Wz2m7gStexajkeD+Lka6DSTy8gt9UwfgVQo6uKjVLG5Ex7PiGOODVqAEghBuS7JzIYU5RvI543nNDAPfnJsas96mSA7L/mD7RTE2drj6hf3oZjJpMPZUQI/B1Qjb5H3K3PNwIDAQAB
    }
}
