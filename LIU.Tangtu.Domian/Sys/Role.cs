using System;
using System.Collections.Generic;
using System.Text;

namespace LIU.Tangtu.Domian.Sys
{
    /// <summary>
    /// 角色
    /// </summary>
    public class Role: UpdateBaseEntity
    {

        /// <summary>
        /// 角色名
        /// </summary>
        public string sName { get; set; }

        /// <summary>
        /// 角色说明
        /// </summary>
        public string sExplain { get; set; }

        /// <summary>
        /// 角色状态 0.停用 1.启用
        /// </summary>
        //public int iType { get; set; }
    }
}
