using DeviceStore.Domain.AbstractModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using DeviceStore.Domain.Entities;
using System.Data.Entity;

namespace DeviceStore.Domain.EFConcrete
{
    public class MemoryRepository<T> : IRepository<T> where T: BaseEntity
    {
        internal AppDbContext appDbContext;
        internal DbSet<T> dbSet;

        public MemoryRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
            this.dbSet = appDbContext.Set<T>();
        }

        public IQueryable<T> Collection()
        {
            return dbSet;
        }

        public void Commit()
        {
            appDbContext.SaveChanges();
        }

        public void Delete(string Id)
        {
            var t = Find(Id);
            if (appDbContext.Entry(t).State == EntityState.Detached)
                dbSet.Attach(t);

            dbSet.Remove(t);
        }

        public T Find(string Id)
        {
            return dbSet.Find(Id);
        }

        public void Insert(T t)
        {
            dbSet.Add(t);
        }

        public void Update(T t)
        {
            dbSet.Attach(t);
            appDbContext.Entry(t).State = EntityState.Modified;
        }
    }
}
