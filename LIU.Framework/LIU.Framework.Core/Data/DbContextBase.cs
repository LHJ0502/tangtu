using LIU.Framework.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
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

        public string ConnectionString { get; } = "jCBoUm8/Y7UYBYVJ1tNRxMsooJ6kwhYMjW2g4K7yIpoXV6CCdsJaY0H2u4dcPnG7xLj1NpBXiS8lo/1viYUjEIKfTw9eU24YDkbSd6pOSosWruT5nNZpVz1UT2loDMNOQiX9LwUSg/rIsfxOUpON5wfsEOLxArsRoHrS/mExhAItneTqnKnAI7JkAv17KeU6njAzDYG7Qr/v7H+ftneSGCPGcMoQ9Oo4M4cQkyp1uhw=";

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

        /// <summary>
        /// 使用 fluent API 配置模型
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            var applyGenericMethod = typeof(ModelBuilder).GetMethods( BindingFlags.Instance | BindingFlags.Public);
            var list = AppInstance.Current.Finder.FindTypes(p => p.IsAssignableFrom(typeof(IEntityMap)) && p != typeof(IEntityMap) && p.IsClass && !p.IsAbstract).ToArray();
            foreach (var item in list)
            {
                var iface = item.GetInterfaces().Where(p => p.IsConstructedGenericType && p.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>)).First();
                var applyConcreteMethod = applyGenericMethod.MakeGenericMethod(iface.GenericTypeArguments[0]);
                applyConcreteMethod.Invoke(modelBuilder, new object[] { Activator.CreateInstance(item) });
            }

        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(CryptoHelper.AesDecrypt(ConnectionString));
        }
    }
}
