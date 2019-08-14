using System;
using System.Data.Entity;
using System.Linq;

namespace QueryIt
{
    public class EmployeeDb : DbContext
    {
        public EmployeeDb(string connectionString) : base(connectionString)
        {
        }

        public DbSet<Employee> Employees { get; set; }
    }

    /**
     * Each repository must be disposable
     */
    public interface IRepository<T> : IDisposable
    {
        void Add(T newEntity);
        void Delete(T entity);
        int Commit();
        T FindById(int id);
        IQueryable<T> FindAll(); //Return all entities as IQueryable<T>
    }

    //Generic constraint: where T : class, IEntity (generic param T has to be a class [a reference type] and also has to implement IEntity)
    //Adding constraint IEntity to force T to be something that has IsValid() method (used in Add() method below)
    public class SqlRepository<T> : IRepository<T> where T : class, IEntity
    {
        DbContext _ctx; 
        DbSet<T> _set;

        public SqlRepository(DbContext ctx)
        {
            _ctx = ctx;
            _set = _ctx.Set<T>(); //Finds the DbSet for that parameter T (in the DbContext ctx)
        }

        public void Add(T newEntity)
        {
            if (newEntity.IsValid()) //Here's why T must contain IsValid() method
            {
                _set.Add(newEntity);
            }
        }

        public void Delete(T entity)
        {
            _set.Remove(entity);
        }

        public T FindById(int id)
        {
            return _set.Find(id);
        }

        public IQueryable<T> FindAll()
        {
            return _set;
        }

        public int Commit()
        {
            return _ctx.SaveChanges();
        }

        public void Dispose()
        {
            _ctx.Dispose();
        }
    }
}
