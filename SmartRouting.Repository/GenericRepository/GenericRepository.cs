using Microsoft.EntityFrameworkCore;
using SmartRouting.Models.Context;
using SmartRouting.Models.Models;

namespace SmartRouting.Repository.GenericRepository
{
    public class GenericRepository<T> : IGenericRepository<T> where T: BaseEntity
    {
        Db_SmartRoutingContext db;
        DbSet<T> dbSet;

        public GenericRepository(Db_SmartRoutingContext context)
        {
            db = context;
            dbSet = db.Set<T>();
        }

        public List<T> GetAll()
        {
            return dbSet.ToList();
        }

        public T GetEntity(int id)
        {
            return dbSet.Find(id);
        }

        public bool Add(T entity)
        {
            try
            {
                dbSet.Add(entity);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Update(T entity)
        {
            try
            {
                dbSet.Entry(entity).State = EntityState.Modified;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Delete(T entity)
        {
            try
            {
                dbSet.Entry(entity).State = EntityState.Deleted;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                T entity = GetEntity(id);
                dbSet.Entry(entity).State = EntityState.Deleted;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public void Dispose()
        {
            db.Dispose();
        }

    }
}
