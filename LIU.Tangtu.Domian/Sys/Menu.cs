using System;
using System.Collections.Generic;
using System.Text;

namespace LIU.Tangtu.Domian.Sys
{
    /// <summary>
    /// 菜单
    /// </summary>
    public class Menu : UpdateBaseEntity
    {

        /// <summary>
        /// 菜单名称
        /// </summary>
        public string sName { get; set; }

        /// <summary>
        /// 菜单图标
        /// </summary>
        public string sIcon { get; set; }

        /// <summary>
        /// 菜单页面地址 或者 按钮请求菜单 
        /// </summary>
        public string sHref { get; set; }

        /// <summary>
        /// 按钮Id
        /// </summary>
        public string sBtnId { get; set; }

        /// <summary>
        /// 父菜单主键 为默认值表示一级菜单
        /// </summary>
        public long gParentKey { get; set; }

        /// <summary>
        /// 菜单类型 1.菜单文件 2.菜单页面 3.按钮
        /// </summary>
        public byte iType { get; set; }


    }
}
