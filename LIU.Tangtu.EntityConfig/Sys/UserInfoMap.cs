using LIU.Framework.Core.Data;
using LIU.Tangtu.Domian.Sys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace LIU.Tangtu.EntityConfig.Sys
{
    public class UserInfoMap : EntityMap<UserInfo>
    {
        public override void Configure(EntityTypeBuilder<UserInfo> builder)
        {
            builder.ToTable("sys_UserInfo");
            builder.HasKey(p => p.gkey);
        }
    }
}
