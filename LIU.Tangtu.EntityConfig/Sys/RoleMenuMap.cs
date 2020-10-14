using LIU.Framework.Core.Data;
using LIU.Tangtu.Domian.Sys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LIU.Tangtu.EntityConfig.Sys
{
    public class RoleMenuMap : EntityMap<RoleMenu>
    {
        public override void Configure(EntityTypeBuilder<RoleMenu> builder)
        {
            builder.ToTable("sys_role_menu");
            builder.HasKey(p => p.gKey);
        }
    }
}
