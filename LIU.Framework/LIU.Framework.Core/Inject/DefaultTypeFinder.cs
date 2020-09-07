using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace LIU.Framework.Core.Inject
{
    /// <summary>
    /// 默认查找器
    /// </summary>
    public class DefaultTypeFinder : ITypeFinder
    {
        private readonly string path;

        /// <summary>
        /// 构造函数    
        /// </summary>
        /// <param name="path">程序集目录</param>
        public DefaultTypeFinder(string path = null)
        {
            this.path = path ?? AppDomain.CurrentDomain.BaseDirectory;
        }

        /// <summary>
        /// 根据指定条件查找类型
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public List<Type> FindTypes(Func<Type, bool> filter)
        {
            List<Type> types = new List<Type>();
            foreach (var item in FideAssembly())
            {
                if (filter != null)
                {
                    types.AddRange(item.GetTypes().Where(filter));
                }
                else
                {
                    types.AddRange(item.GetTypes());
                }
            }
            return types;
        }

        protected virtual List<Assembly> FideAssembly()
        {
            var list = new List<Assembly>();
            if (Directory.Exists(path))
            {
                LoadAssembly("*.dll", list);
            }

            return list;
        }

        private void LoadAssembly(string searchPattern, List<Assembly> list)
        {
            var dlls = Directory.GetFiles(path, searchPattern, SearchOption.TopDirectoryOnly).ToList();
            foreach (var dll in dlls)
            {
                var fileName = Path.GetFileName(dll);
                if (fileName != null)
                {
                    try
                    {
                        list.Add(Assembly.LoadFile(dll));
                    }
                    catch (Exception)
                    {
                        //ignore 忽略非net程序
                    }
                }
            }
        }
    }
}
