using System;
using System.Collections.Generic;
using System.Text;

namespace LIU.Tangtu.Domian.Sys
{
    /// <summary>
    /// 角色
    /// </summary>
    public class Role
    {
        /// <summary>
        /// 主键
        /// </summary>
        public long gkey { get; set; }

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

        /// <summary>
        /// 添加人员主键
        /// </summary>
        public long? gInsertKey { get; set; }

        /// <summary>
        /// 添加人
        /// </summary>
        public string sInsertName { get; set; }

        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime? dInsert { get; set; }

        /// <summary>
        /// 修改人员主键
        /// </summary>
        public long? gUpdateKey { get; set; }

        /// <summary>
        /// 修改人
        /// </summary>
        public string sUpdateName { get; set; }


        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? dUpdate { get; set; }
    }
}
