using System;
using System.Collections.Generic;
using System.Text;

namespace LIU.Framework.Core.Inject
{
    /// <summary>
    /// 程序查找器 批量依赖注入使用
    /// </summary>
    public interface ITypeFinder
    {

        /// <summary>
        /// 根据指定条件查找类型
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        List<Type> FindTypes(Func<Type, bool> filter);
    }
}
