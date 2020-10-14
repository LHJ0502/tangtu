using LIU.Framework.Core.Base;
using LIU.Tangtu.Domian.Sys;
using LIU.Tangtu.IServices.Sys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace LIU.Tangtu.Services.Sys
{
    public class UserInfoService : BaseService<UserInfo>, IUserInfoService
    {
        /// <inheritdoc/>
        public List<UserInfo> GetUsers(Expression<Func<UserInfo, bool>> expression)
        {
            return repository.Find().Where(expression).ToList();
        }



    }
}
