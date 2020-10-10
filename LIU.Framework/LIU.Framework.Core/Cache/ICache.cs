using System;
using System.Collections.Generic;
using System.Text;

namespace LIU.Framework.Core.Cache
{
    /// <summary>
    /// 缓存接口
    /// </summary>
    public interface ICache : IDisposable
    {
        /// <summary>
        /// 缓存名称
        /// </summary>
        string Name { get; }


        /// <summary>
        /// 设置缓存对象
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="seconds">缓存时间，秒，默认7200秒</param>
        void SetObject(string key, object value, int seconds = 7200);

        /// <summary>
        /// 获取缓存对象
        /// </summary>
        /// <param name="key">键</param>
        /// <returns></returns>
        object GetObject(string key);

        /// <summary>
        /// 获取缓存对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="key">键</param>
        /// <returns></returns>
        T GetObject<T>(string key);

        /// <summary>
        /// 移除缓存对象
        /// </summary>
        /// <param name="key"></param>
        void RemoveObject(string key);


        /// <summary>
        /// 缓存是否已经包含了键
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>true：包含</returns>
        bool IsContain(string key);
    }
}
