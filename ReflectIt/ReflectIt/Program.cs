using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReflectIt
{
    class Program
    {
        static void Main(string[] args)
        {
            var employeeList = CreateCollection(typeof(List<Employee>));
            Console.Write(employeeList.GetType().Name); //Output: List`1 (output is compliant with the CLS [specification which allows all different languages like C#, VisBasic, F# to work together])
            //Console.WriteLine(employeeList.GetType().FullName);
            var genericArguments = employeeList.GetType().GenericTypeArguments;
            foreach (var arg in genericArguments)
            {
                Console.Write("[{0}]", arg.Name);
            }
            //^Even that we think of List<Employee> as a single type, at some lower level, it is
            //really a combination of generic type and generic type argument. Those 2 pieces of information
            //form a closed constructed type. Closed constructed type is something you can instantiate like:
            //List<int>, Dictionary<string, Employee>, Stack<double>

            Console.WriteLine();
            Console.WriteLine();
            var employeeList2 = CreateCollection(typeof(List<>), typeof(Employee)); //Leave out the generic param: List<> (this is called UNBOUND generic, this syntax is used inside of typeof() operator, because unbound generic never exists as an instantiated object)
            Console.Write(employeeList2.GetType().Name);
            var genericArguments2 = employeeList2.GetType().GenericTypeArguments;
            foreach (var arg in genericArguments2)
            {
                Console.Write("[{0}]", arg.Name);
            }
            Console.WriteLine();

            //var employee = new Employee();
            //var employeeType = typeof(Employee);
            //var methodInfo = employeeType.GetMethod("Speak");
            //methodInfo = methodInfo.MakeGenericMethod(typeof(DateTime));            
            //methodInfo.Invoke(employee, null);

            Console.Read();
        }

        private static object CreateCollection(Type type)
        {
            return Activator.CreateInstance(type);
        }

        //Detaching the 2 pieces of information (generic type and generic type argument)
        private static object CreateCollection(Type collectionType, Type itemType)
        {
            //collectionType = type of the collection (List / HashSet / SortedList etc.)
            //itemType = item type stored inside (double / Employee / DateTime etc.)

            var closedType = collectionType.MakeGenericType(itemType); //Transforming the unbound collectionType to a closed constructed type
            return Activator.CreateInstance(closedType);
        }
    }

    public class Employee
    {
        public string Name { get; set; }

        public void Speak<T>()
        {
            Console.WriteLine(typeof(T).Name);
        }
    }
}
