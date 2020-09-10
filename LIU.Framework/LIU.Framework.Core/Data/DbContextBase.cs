using LIU.Framework.Common;
using LIU.Framework.Common.Extend;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace LIU.Framework.Core.Data
{
    public class DbContextBase : DbContext, IDbContext
    {

        //public DbContextBase(DbContextOptions<DbContextBase> options) : base(options)
        //{
        //    this.dbConnection = Database.GetDbConnection();
        //    this.ConnectionString = dbConnection.ConnectionString;

        //}
        //private static MethodInfo applyConcreteMethod;

        public DbContextBase()
        {
            //if (applyConcreteMethod == null)
            //{
            //    //applyConcreteMethod = typeof(ModelBuilder).GetMethods(BindingFlags.Instance | BindingFlags.Public).Where
            //    //    (p => p.Name == "ApplyConfiguration" && p.GetParameters().FirstOrDefault().ParameterType.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>)).FirstOrDefault();

            //    applyConcreteMethod = typeof(ModelBuilder).GetMethods().Single(p => p.Name == "ApplyConfiguration" && p.GetParameters().SingleOrDefault().ParameterType.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>));
            //}
            if (ConnectionString.IsNullOrWhiteSpace())
            {
                FileInfo fileInfo = new FileInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Config\conn.json"));
                using (var s = fileInfo.OpenText())
                {
                    var json = JsonConvert.DeserializeObject<dynamic>(s.ReadToEnd());
                    ConnectionString = CryptoHelper.AesDecrypt((string)(json.DB));
                }
            }
        }

        public string ConnectionString { get; }

        public DbConnection dbConnection { get; }

        ///<inheritdoc/>
        public IDbContext Add<TEntity>(IEnumerable<TEntity> entityObjects) where TEntity : class
        {
            foreach (var item in entityObjects)
            {
                this.Add<TEntity>(item);
            }
            return this;
        }

        ///<inheritdoc/>
        public int Commit()
        {
            return SaveChanges();
        }

        ///<inheritdoc/>
        public IDbContext Delete<TEntity>(IEnumerable<TEntity> entityObjects) where TEntity : class
        {
            foreach (var item in entityObjects)
            {
                Delete(item);
            }
            return this;
        }

        ///<inheritdoc/>
        public IDbContext Delete<TEntity>(TEntity entity) where TEntity : class
        {
            if (entity != null)
            {
                Set<TEntity>().Attach(entity);
                Set<TEntity>().Remove(entity);
            }
            return this;
        }

        ///<inheritdoc/>
        public TEntity FindByKeys<TEntity>(params object[] keys) where TEntity : class
        {
            return this.Set<TEntity>().Find(keys);
        }

        ///<inheritdoc/>
        public IQueryable<TEntity> Table<TEntity>(bool trackable = false) where TEntity : class
        {
            if (trackable)
            {
                return Set<TEntity>();
            }
            else
            {
                return Set<TEntity>().AsNoTracking();
            }
        }

        ///<inheritdoc/>
        public IDbContext Update<TEntity>(IEnumerable<TEntity> entityObjects) where TEntity : class
        {
            foreach (var item in entityObjects)
            {
                this.Update<TEntity>(item);
            }
            return this;
        }

        ///<inheritdoc/>
        IDbContext IDbContext.Add<TEntity>(TEntity entity)
        {
            this.Add<TEntity>(entity);
            return this;
        }

        ///<inheritdoc/>
        IDbContext IDbContext.Update<TEntity>(TEntity entity)
        {
            this.Update<TEntity>(entity);
            return this;
        }

        ///// <summary>
        ///// 使用 fluent API 配置模型
        ///// </summary>
        ///// <param name="modelBuilder"></param>
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    var list = AppInstance.Current.Finder.FindTypes(p => p.GetInterfaces().Contains(typeof(IEntityMap)) && p != typeof(IEntityMap) && p.IsClass && !p.IsAbstract).ToArray();
        //    //AppInstance.Current.Finder.FindTypes(p => p.IsAssignableFrom(typeof(IEntityMap)) && p != typeof(IEntityMap) && p.IsClass && !p.IsAbstract).ToArray();

        //    foreach (var item in list)
        //    {
        //        object obj = Activator.CreateInstance(item);
        //        applyConcreteMethod.Invoke(modelBuilder, new object[] { });
        //    }
        //    base.OnModelCreating(modelBuilder);
        //}

        /// <summary>
        /// 使用 fluent API 配置模型
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var list = AppInstance.Current.ResolveALL<IEntityMap>();
            foreach (var item in list)
            {
                item.Config(modelBuilder);
            }
            base.OnModelCreating(modelBuilder);
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString);
        }
    }
}
