using System;
using System.Data.Entity;
using System.Linq;

namespace QueryIt
{
    class Program
    {
        static void Main(string[] args)
        {
            Database.SetInitializer(new DropCreateDatabaseAlways<EmployeeDb>()); //Tell the EF that is okay to drop-and-recreate the db every time the app runs

            using (IRepository<Employee> employeeRepository = new SqlRepository<Employee>(new EmployeeDb("Data Source=DESKTOP-4Q6L9RH\\MSSQLSERVER17;Initial Catalog=GenericsDb1;Persist Security Info=True;User ID=jovan;Password=EX8O25um4n7fn")))
            {
                AddEmployees(employeeRepository);
                CountEmployees(employeeRepository);
                QueryEmployees(employeeRepository);
                //AddManagers(employeeRepository);
                //DumpPeople(employeeRepository);
            }

            Console.Read();
        }

        private static void AddManagers(IRepository<Employee> employeeRepository)
        {
            employeeRepository.Add(new Manager { Name = "Alex" });
            employeeRepository.Commit();
        }

        private static void DumpPeople(IRepository<Employee> employeeRepository)
        {
            var employees = employeeRepository.FindAll();
            foreach (var employee in employees)
            {
                Console.WriteLine(employee.Name);
            }
        }

        private static void QueryEmployees(IRepository<Employee> employeeRepository)
        {
            var employee = employeeRepository.FindById(1);
            Console.WriteLine(employee.Name);
        }

        private static void CountEmployees(IRepository<Employee> employeeRepository)
        {
            Console.WriteLine(employeeRepository.FindAll().Count());
        }

        private static void AddEmployees(IRepository<Employee> employeeRepository)
        {
            employeeRepository.Add(new Employee { Name = "Scott" });
            employeeRepository.Add(new Employee { Name = "Chris" });
            employeeRepository.Commit();
        }
    }
}
