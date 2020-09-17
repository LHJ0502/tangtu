using System.Text;

namespace LIU.Tangtu.Web.App_Code
{
    public class JWTConstData
    {
        /// <summary>
        /// 发布者
        /// </summary>
        public const string issuer = "LiuHaojie";

        /// <summary>
        /// 受众
        /// </summary>
        public const string audience = "LiuHaojie";

        /// <summary>
        /// 秘钥
        /// </summary>
        public static byte[] SecurityKey
        {
            get
            {
                return Encoding.UTF8.GetBytes(key);
            }
        }

        /// <summary>
        /// 秘钥
        /// </summary>
        private const string key = "MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQDI2a2EJ7m872v0afyoSDJT2o1";
        //MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQDI2a2EJ7m872v0afyoSDJT2o1+SitIeJSWtLJU8/Wz2m7gStexajkeD+Lka6DSTy8gt9UwfgVQo6uKjVLG5Ex7PiGOODVqAEghBuS7JzIYU5RvI543nNDAPfnJsas96mSA7L/mD7RTE2drj6hf3oZjJpMPZUQI/B1Qjb5H3K3PNwIDAQAB
    }
}
