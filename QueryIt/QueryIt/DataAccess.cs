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
     * Each repository must be disposable (provides a mechanism for releasing unmanaged resources)
     */
    public interface IRepository<T> : IDisposable
    {
        void Add(T newEntity);
        void Delete(T entity);
        int Commit();
        T FindById(int id);
        IQueryable<T> FindAll(); //Return all entities as IQueryable<T>
    }

    /**
     * Generic constraint(s)
     * where T : class, IEntity
     * Meaning: Generic param T has to be a class [a reference type] and also has to implement IEntity
     */

    //Forcing T to be a value type (like int or double) - use 'struct' constraint
    //A reference type - 'class' constraint
    //^Not both in same time obviously (either struct or class)
    //Always comes first if needed (must be in front of 'IEntity')

    //IEntity:
    //You can have as many interfaces as you like (separated by comma)
    //The goal is to force T to have certain features or API, so you can invoke methods or set specific
    //properties inside of the implementation of generic type (like SqlRepository<T>)
    //In our case: adding constraint IEntity to force T to be something that has IsValid() method
    //(used in Add() method implementation below)

    //new() constraint always has to be the last one.
    //With this constraint in place, the C# compiler will always check to make sure the type T has a 
    //default constructor. It something that can be constructed without needing any parameters passed
    //to the constructor. That gives ability to create a new T inside of the code (of the class):
    //T entity = new T();
    //Otherwise (without new()) - not possible

    //Should constraints be applied to interfaces (it's perfectly legal)? In general, avoid that. Constraints
    //are really implementation details (interfaces don't have any implementation details). That's just matter
    //of personal design style.

    public class SqlRepository<T> : IRepository<T> where T : class, IEntity, new()
    //public class SqlRepository<T, T2> : IRepository<T> where T : class, IEntity, new()
    //                                                   where T2 : class //Second 'where' keyword for the second generic type param
    //                                                                    //(you can have as many 'where' as generic type params)
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
            //Setting default value:
            //T entity = default(T); //Returns default value of T. If T is a reference type, a null value is assigned
            
            //T entity = new T();

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
