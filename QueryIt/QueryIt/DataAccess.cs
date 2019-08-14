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
    /**
     * IRepository<T> has NO generic modifier, this is called invariant generic type parameter. As the
     * name implies there is no variance (no "wiggle room"). The generic type used as an argument is the
     * type we have to use. No ability to treat IRepository<Employee> as an IRepository<Person>.
     * Doing that requires covariance (<out T>). Methods inside the interface are allowed to return a type
     * that is more DERIVED than the type specified by the generic type parameter. Example:
     * public interface IEnumerable<out T> : IEnumerable
     * {
     *     IEnumerator<T> GetEnumerator();
     * }
     * A covariant interface (like IEnumerable) would allow GetEnumerator() to return 'IEnumerator<Employee>' 
     * even when T is type 'Person'. Employee is more derived than Person. In C# that does require an 'out'
     * modifier to explicitely make the interface covariant. 'out' is a generic modifier, it makes a 
     * parameter covariant.
     * 
     * Covariance only works with delegates and interfaces.
     * 
     * Covariance is only supported when having methods RETURNING a covariant type parameter. It's
     * dangerous and illegal when methods take parameters of type T (like Add() & Delete() below).
     * If we want to share DumpPeople() with both Employee & Person objects, we can define another interface
     * IReadOnlyRepository<out T> (covariant) that includes JUST the methods that RETURN items of type T.
     * 
     * Contravariance uses the 'in' modifier. Allows to use an ISomething of Employee as an ISomething of
     * Manager (to use more derived type there).
     */
    public interface IRepository<T> : IReadOnlyRepository<T>, IWriteOnlyRepository<T>, IDisposable
    {
        //T FindById(int id);
        //IQueryable<T> FindAll(); //Return all entities as IQueryable<T>
    }

    public interface IReadOnlyRepository<out T> : IDisposable //Covariant
    {
        T FindById(int id);
        IQueryable<T> FindAll();
        //Covariance uses the generic modifier <out T> and T is used only as an OUTPUT for the
        //methods inside this interface
    }

    public interface IWriteOnlyRepository<in T> : IDisposable //Contravariant
    {
        void Add(T newEntity);
        void Delete(T entity);
        int Commit();
        //Contravariance uses the generic modifier <in T> and T is used only as an INPUT for the
        //methods inside this interface
        //This interface does not inherit any other interfaces where T is used as a return type. In this
        //interface definition, T is strictly just an input (so it can be contravariant). 
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
