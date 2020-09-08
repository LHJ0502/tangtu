using LIU.Framework.Core.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace LIU.Tangtu.Domian.Sys
{
    public class UserInfo: IEntity
    {
        /// <summary>
        /// 主键
        /// </summary>
        public Guid gkey { get; set; }

        /// <summary>
        /// 登录名
        /// </summary>
        public string sLoginName { get; set; }

        /// <summary>
        /// 密码（md5）
        /// </summary>
        public string sPassWord { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string sName { get; set; }

        /// <summary>
        /// 用户前后台转态 1前台用户 无法登陆后台系统 2后台用户 前后台都可以使用
        /// </summary>
        public int iState { get; set; }

        /// <summary>
        /// 头像地址
        /// </summary>
        public string sImageUrl { get; set; }

        /// <summary>
        /// 头像框地址
        /// </summary>
        public string sHerdImageUrl { get; set; }


        /// <summary>
        /// 邮箱地址
        /// </summary>
        public string sEmail { get; set; }

        /// <summary>
        /// 邮箱验证结果 0未验证通过 1验证通过
        /// </summary>
        public int iEmailValidate { get; set; }

        /// <summary>
        /// 金币
        /// </summary>
        public int iGold { get; set; }

        /// <summary>
        /// Vip级别
        /// </summary>
        public int iLevel { get; set; }

        /// <summary>
        /// vip到期时间
        /// </summary>
        public DateTime? dLevelTIme { get; set; }

        /// <summary>
        /// 角色主键组 以,分割
        /// </summary>
        public string sRoleKey { get; set; }


        /// <summary>
        /// 性别 1:男 2:女 9:未知
        /// </summary>
        public int? iSex { get; set; }

        /// <summary>
        /// 启用状态 1:启用 2:停用
        /// </summary>
        public int iFlag { get; set; }

        /// <summary>
        /// 最后一次登录时间
        /// </summary>
        public DateTime? LastDateTime { get; set; }

        /// <summary>
        /// 注册时间
        /// </summary>
        public DateTime? RegDateTime { get; set; }
    }
}
