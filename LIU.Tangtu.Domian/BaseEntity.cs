using LIU.Framework.Core.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace LIU.Tangtu.Domian
{
    /// <summary>
    /// 基础的实体
    /// </summary>
    public class BaseEntity : IEntity
    {
        /// <summary>
        /// 主键
        /// </summary>
        public long gKey { get; set; }
    }

    /// <summary>
    /// 包含创建
    /// </summary>
    public class CreateBaseEntity : BaseEntity
    {
        /// <summary>
        /// 创建人主键
        /// </summary>
        public long? gCreateKey { get; set; }

        /// <summary>
        /// 创建人名称
        /// </summary>
        public string sCreateName { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? dCreateTime { get; set; }
    }

    /// <summary>
    /// 包含更新 和创建
    /// </summary>
    public class UpdateBaseEntity : CreateBaseEntity
    {
        /// <summary>
        /// 更新人主键
        /// </summary>
        public long? gUpdateKey { get; set; }

        /// <summary>
        /// 更新人名称
        /// </summary>
        public string sUpdateName { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? dUpdateTime { get; set; }
    }
}
