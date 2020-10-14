using LIU.Framework.Core.Base;
using LIU.Tangtu.Domian.Sys;
using System;
using System.Collections.Generic;
using System.Text;

namespace LIU.Tangtu.IServices.Sys
{
    public interface IRoleMenuService : IBaseService<RoleMenu>
    {

        /// <summary>
        /// 检查角色权限
        /// </summary>
        /// <param name="rolekeys">角色id</param>
        /// <param name="url">请求的地址</param>
        /// <returns>true可以访问，false不可以访问</returns>
        bool CheckRole(List<long> rolekeys, string url);
    }
}
