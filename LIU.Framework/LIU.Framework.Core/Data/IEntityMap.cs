using LIU.Framework.Core.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace LIU.Framework.Core.Data
{
    /// <summary>
    /// 实体映射
    /// </summary>
    public interface IEntityMap
    {
    }

    /// <summary>
    /// 实体映射
    /// </summary>
    public interface IEntityMap<T> : IEntityTypeConfiguration<T>, IEntityMap where T : class, IEntity
    {
    }
}
