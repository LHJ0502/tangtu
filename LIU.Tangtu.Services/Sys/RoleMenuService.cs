using LIU.Framework.Common.Extend;
using LIU.Framework.Core.Base;
using LIU.Framework.Core.Cache;
using LIU.Tangtu.Domian.Sys;
using LIU.Tangtu.IServices.Sys;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace LIU.Tangtu.Services.Sys
{
    public class RoleMenuService : BaseService<RoleMenu>, IRoleMenuService
    {

        //readonly IRepository<Role> roleRepository;
        readonly IRepository<Menu> menuRepository;
        readonly ICache cache;
        public RoleMenuService(ICache cache)
        {
            //roleRepository = RepositoryBus.Get<Role>();
            menuRepository = RepositoryBus.Get<Menu>();
            this.cache = cache;
        }


        /// <summary>
        /// 检查角色权限
        /// </summary>
        /// <param name="rolekeys">角色id</param>
        /// <param name="url">请求的地址</param>
        /// <returns>true可以访问，false不可以访问</returns>
        public bool CheckRole(List<long> rolekeys, string url)
        {
            return GetCacheRoleMenu().Where(p => rolekeys.Contains(p.gRoleKey)).SelectMany(p => p.Urls).Distinct().Contains(url);
        }


        /// <summary>
        /// 获取缓存的权限菜单
        /// </summary>
        /// <returns></returns>
        private List<CacheRoleMenu> GetCacheRoleMenu()
        {
            var cacheRoleMenus = cache.GetObject<List<CacheRoleMenu>>("CacheRoleMenu");
            if (cacheRoleMenus == null || !cacheRoleMenus.Any())//没有缓存，那进行缓存
            {
                cacheRoleMenus = SetCacheRoleMenu();
            }
            return cacheRoleMenus;
        }

        /// <summary>
        /// 设置缓存权限菜单
        /// </summary>
        /// <returns></returns>
        private List<CacheRoleMenu> SetCacheRoleMenu()
        {
            var list = repository.Find().Join(menuRepository.Find(), p => p.gMenuKey, p => p.gKey, (x, y) => new
            {
                x.gRoleKey,
                y
            }).ToList();

            var rolekeys = list.Select(p => p.gRoleKey).Distinct();
            List<CacheRoleMenu> cacheRoleMenus = new List<CacheRoleMenu>();
            foreach (var item in rolekeys)
            {
                cacheRoleMenus.Add(new CacheRoleMenu()
                {
                    gRoleKey = item,
                    Urls = list.Where(p => p.gRoleKey == item && p.y.sHref.IsNotNullOrWhiteSpace()).Select(p => p.y.sHref)
                });
            }

            cache.SetObject("CacheRoleMenu", cacheRoleMenus, 24 * 3600);
            return cacheRoleMenus;
        }

    }

    /// <summary>
    /// 缓存角色菜单
    /// </summary>
    public class CacheRoleMenu
    {
        /// <summary>
        /// 角色主键
        /// </summary>
        public long gRoleKey { get; set; }

        /// <summary>
        /// 请求的地址
        /// </summary>
        public IEnumerable<string> Urls { get; set; }
    }
}
