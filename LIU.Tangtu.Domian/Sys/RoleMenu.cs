using System;
using System.Collections.Generic;
using System.Text;

namespace LIU.Tangtu.Domian.Sys
{
    /// <summary>
    /// 角色菜单/范围
    /// </summary>
    public class RoleMenu : UpdateBaseEntity
    {
        /// <summary>
        /// 角色主键
        /// </summary>
        public long gRoleKey { get; set; }

        /// <summary>
        /// 菜单主键
        /// </summary>
        public long gMenuKey { get; set; }
    }
}
