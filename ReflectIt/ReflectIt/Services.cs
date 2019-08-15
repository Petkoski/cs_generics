using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReflectIt
{
    public interface ILogger
    {
        //Typically it would have methods like LogMessage(), Audit() or Warning()
    }

    public class SqlServerLogger : ILogger
    {

    }

    public interface IRepository<T>
    {

    }

    public class SqlRepository<T> : IRepository<T>
    {
        //Does not have a default ctor

        public SqlRepository(ILogger logger) //This constructor needs a logger to do any work
        {

        }
        //Constructing SqlRepository<T> is trickier than constructing SqlServerLogger because 
        //SqlRepository<T> needs an ILogger dependency
    }

    public class Customer
    {

    }

    public class InvoiceService
    {
        public InvoiceService(IRepository<Customer> repository, ILogger logger)
        {

        }
    }
}
