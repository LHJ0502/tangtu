using LIU.Framework.Core.Inject;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace LIU.Framework.Core.Cache
{
    /// <summary>
    /// 系统默认缓存
    /// </summary>
    public class SystemCache : ICache, IDefaultImplementation
    {
        /// <summary>
        /// 系统默认缓存
        /// </summary>
        public SystemCache()
        {
            cache = new MemoryCache(Options.Create(new MemoryCacheOptions()));
        }
        /// <summary>
        /// 内部缓存
        /// </summary>
        private readonly IMemoryCache cache;

        /// <inheritdoc />
        public string Name { get; } = "SystemCache";

        /// <inheritdoc />
        public void SetObject(string key, object value, int seconds = 7200)
        {
            TimeSpan timeSpan = new TimeSpan(0, 0, seconds);
            cache.Set(key, value, timeSpan);
        }

        /// <inheritdoc />
        public object GetObject(string key)
        {
            cache.TryGetValue(key, out object value);
            return value;
        }

        /// <inheritdoc />
        public T GetObject<T>(string key)
        {
            cache.TryGetValue(key, out T value);
            return value;
        }

        /// <inheritdoc />
        public void RemoveObject(string key)
        {
            cache.Remove(key);
        }

        /// <inheritdoc />
        public bool IsContain(string key)
        {
            return cache.TryGetValue(key, out object value);
        }

        /// <inheritdoc />
        public void Dispose()
        {
            cache?.Dispose();
        }

    }
}
