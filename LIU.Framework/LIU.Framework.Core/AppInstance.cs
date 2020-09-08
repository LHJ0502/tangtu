using Autofac;
using LIU.Framework.Core.Base;
using LIU.Framework.Core.Data;
using LIU.Framework.Core.Inject;
using System;
using System.Collections.Generic;
using System.Text;

namespace LIU.Framework.Core
{

    /// <summary>
    /// 实例
    /// </summary>
    public class AppInstance
    {
        private ContainerBuilder _builder;
        private bool isBuilded;
        private IComponentContext context;
        /// <summary>
        /// 防止外部实例化 单例
        /// </summary>
        protected AppInstance()
        {
        }

        /// <summary>
        /// 当前实例
        /// </summary>
        private static AppInstance _current;

        /// <summary>
        /// 程序集查找器
        /// </summary>
        public ITypeFinder Finder { get; private set; }

        /// <summary>
        /// 依赖注入容器
        /// </summary>
        public ILifetimeScope Container { get; private set; }

        //public ContainerBuilder Builder
        //{
        //    get
        //    {
        //        return _builder;
        //    }
        //    private set
        //    {
        //        _builder = value;
        //    }
        //}

        /// <summary>
        /// 当前实例
        /// </summary>
        public static AppInstance Current
        {
            get
            {
                if (_current == null)
                    _current = new AppInstance();
                return _current;
            }
        }

        /// <summary>
        /// 设置实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public static void Set<T>() where T : AppInstance, new()
        {
            if (typeof(T) == typeof(AppInstance))
                _current = new AppInstance();
            else
                _current = new T();
        }

        /// <summary>
        /// 实例初始化
        /// </summary>
        /// <param name="finder"></param>
        /// <returns></returns>
        public AppInstance Init(ITypeFinder finder)
        {
            Finder = finder;
            return this;
        }

        /// <summary>
        /// 实例构建
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public AppInstance AppBuilder(ContainerBuilder builder, bool isBuild = false)
        {
            _builder = builder ?? _builder ?? new ContainerBuilder();
            //注入
            _builder.RegisterType<IDbContext>().As<DbContextBase>().InstancePerLifetimeScope();//目前单数据库，后期可以考虑使用工厂代替
            if (isBuild)
            {
                Container = _builder.Build();
                context = Container.Resolve<IComponentContext>();
                isBuilded = true;
            }
            return this;
        }


        /// <summary>
        /// 设置容器
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public AppInstance SetContainer(ILifetimeScope container)
        {
            if (Container == null && container != null)
            {
                Container = container;
                context = Container.Resolve<IComponentContext>();
                isBuilded = true;
            }
            return this;
        }
    }
}
