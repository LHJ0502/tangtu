using LIU.Framework.Core.Data;
using LIU.Tangtu.Domian.Sys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace LIU.Tangtu.EntityConfig.Sys
{
    public class RoleMap : EntityMap<Role>
    {
        public override void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("sys_role");
            builder.HasKey(p => p.gKey);
        }
    }
}
