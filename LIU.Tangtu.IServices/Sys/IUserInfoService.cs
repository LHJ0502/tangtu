using LIU.Framework.Core.Base;
using LIU.Tangtu.Domian.Sys;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace LIU.Tangtu.IServices.Sys
{
    public interface IUserInfoService : IBaseService<UserInfo>
    {
        /// <summary>
        /// 获取用户
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        List<UserInfo> GetUsers(Expression<Func<UserInfo, bool>> expression);
    }
}
