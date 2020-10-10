using System;
using System.Collections.Generic;
using System.Text;

namespace LIU.Tangtu.Domian.Sys
{
    /// <summary>
    /// 角色菜单/范围
    /// </summary>
    public class RoleMenu
    {
        /// <summary>
        /// 角色主键
        /// </summary>
        public Guid gRoleKey { get; set; }

        /// <summary>
        /// 菜单主键
        /// </summary>
        public Guid gMenuKey { get; set; }

        /// <summary>
        /// 添加人员主键
        /// </summary>
        public Guid? gInsertKey { get; set; }

        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime? dInsert { get; set; }

        /// <summary>
        /// 修改人员主键
        /// </summary>
        public Guid? gUpdateKey { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? dUpdate { get; set; }
    }
}
