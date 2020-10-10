using LIU.Framework.Core.Data;
using LIU.Tangtu.Domian.Sys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace LIU.Tangtu.EntityConfig.Sys
{
    public class MenuMap : EntityMap<Menu>
    {
        public override void Configure(EntityTypeBuilder<Menu> builder)
        {
            builder.ToTable("sys_menu");
            builder.HasKey(p => p.gKey);
        }
    }
}
