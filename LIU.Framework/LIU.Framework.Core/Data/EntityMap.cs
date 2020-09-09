using LIU.Framework.Core.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace LIU.Framework.Core.Data
{
    public abstract class EntityMap<T> : IEntityMap<T> where T : class, IEntity
    {
        /// <summary>
        /// 配置
        /// </summary>
        /// <param name="builder"></param>
        public abstract void Configure(EntityTypeBuilder<T> builder);
    }
}
