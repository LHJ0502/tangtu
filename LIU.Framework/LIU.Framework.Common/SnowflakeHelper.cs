using System;
using System.Collections.Generic;
using System.Text;

namespace LIU.Framework.Common
{
    /// <summary>
    /// 雪花算法得到唯一id
    /// </summary>
    public static class SnowflakeHelper
    {
        /// <summary>
        /// 生成器
        /// </summary>
        private static IdWorker IdWorker = new IdWorker(1, 1);

        /// <summary>
        /// 获取ID
        /// </summary>
        /// <returns></returns>
        public static long GetID()
        {
            return IdWorker.NextId();
        }

    }
}
