using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace CollectIt
{
    /**
     * Working with generic collections and data structures
     */

    class Program
    {
        static void Main()
        {
            //Arrays();
            //Lists();
            //Queues();
            Stacks();
            //Sets();
            //Linked();
            //CountEmloyees();
            //Dictionary();
            //SortedDictionary();
            //SortedList();
            //SortedSet();

            //after interfaces
            //SortedWithComparer();
            //SortedAndClean();
        }

        private static void SortedSet()
        {
            var set = new SortedSet<int>();
            set.Add(3);
            set.Add(2);
            set.Add(1);

            foreach (var number in set)
            {
                Console.WriteLine(number);
            }
        }

        private static void SortedList()
        {
            var employeesByDepartment = new SortedList<string, List<Employee>>();

            employeesByDepartment.Add("Sales", new List<Employee>());
            employeesByDepartment["Sales"].Add(new Employee { Name = "Alex" });

            employeesByDepartment.Add("Engineering", new List<Employee>());
            employeesByDepartment["Engineering"].Add(new Employee { Name = "Scott" });
            employeesByDepartment["Engineering"].Add(new Employee { Name = "Joy" });

            foreach (var pair in employeesByDepartment)
            {
                Console.WriteLine("Department {0}", pair.Key);
                foreach (var employee in pair.Value)
                {
                    Console.WriteLine("\t{0}", employee.Name);
                }
            }
        }

        private static void SortedDictionary()
        {
            var employeesByDepartment = new SortedDictionary<string, List<Employee>>();

            employeesByDepartment.Add("Sales", new List<Employee>());
            employeesByDepartment["Sales"].Add(new Employee { Name = "Alex" });

            employeesByDepartment.Add("Engineering", new List<Employee>());
            employeesByDepartment["Engineering"].Add(new Employee { Name = "Scott" });
            employeesByDepartment["Engineering"].Add(new Employee { Name = "Joy" });

            foreach (var pair in employeesByDepartment)
            {
                Console.WriteLine("Department {0}", pair.Key);
                foreach (var employee in pair.Value)
                {
                    Console.WriteLine("\t{0}", employee.Name);
                }
            }

        }

        private static void Dictionary()
        {
            var employeesByName = new Dictionary<string, Employee>();
            employeesByName.Add("Scott", new Employee { Name="Scott"});
            employeesByName.Add("Alex", new Employee { Name="Alex"});

            foreach (var pair in employeesByName)
            {
                Console.WriteLine(pair.Value.Name);
            }

            Console.WriteLine("--");

            var employeesByDepartment = new Dictionary<string, List<Employee>>();

            employeesByDepartment.Add("Sales", new List<Employee>());
            employeesByDepartment["Sales"].Add(new Employee { Name = "Alex" });

            employeesByDepartment.Add("Engineering", new List<Employee>());
            employeesByDepartment["Engineering"].Add(new Employee {Name = "Scott"});
            employeesByDepartment["Engineering"].Add(new Employee { Name = "Joy" });

            foreach (var pair in employeesByDepartment)
            {
                Console.WriteLine("Department {0}", pair.Key);
                foreach (var employee in pair.Value)
                {
                    Console.WriteLine("\t{0}", employee.Name);
                }
            }

        }

        private static void Linked()
        {
            var employees = new LinkedList<Employee>();
            employees.AddFirst(new Employee {Name = "Scott"});

            var first = employees.First;
            employees.AddBefore(first, new Employee {Name = "Alex"});

            foreach (var employee in employees)
            {
                Console.WriteLine(employee.Name);
            }

            Console.WriteLine("--");

            var node = employees.Last;
            while (node != null)
            {
                Console.WriteLine(node.Value.Name);
                node = node.Previous;
            }
        }

        private static void Arrays()
        {
            //Array is a data structure

            Employee[] employees = new Employee[2]
            {
                new Employee { Name = "Scott"},
                new Employee { Name="Alex" }
             };

            // Two ways to iterate
            foreach (var employee in employees)
            {
                Console.Write(employee.Name);
            }

            for (int i = 0; i < employees.Length; i++)
            {
                Console.WriteLine(employees[i].Name);
            }

            //To add a new element (difficult because an arr has fixed size):
            //Array.Resize - but can be paintful if you consistently resize the array (+ might lead to performance problems)
            //Behind the scenes, the array is one contiguous block of memory. Everytime we resize it, it needs to be copied around into a new place
            Array.Resize(ref employees, 10);

            Console.Read();
        }

        private static void Lists()
        {
            //It doesn't care about the objects you are storing inside,
            //it does care about the types (since we are using generic type to parameterize methods like Add(), IndexOf(), etc.)

            List<Employee> employees = new List<Employee>
            {
                new Employee { Name = "Scott" },
                new Employee { Name="Alex" }
             };

            foreach (var employee in employees)
            {
                Console.Write(employee.Name);
            }

            employees.Add(new Employee() { Name="Chris" }); 
            for (int i = 0; i < employees.Count; i++)
            {
                Console.WriteLine(employees[i].Name);
            }

            //ListCapacity();

            Console.Read();
        }

        private static void ListCapacity()
        {
            var numbers = new List<int>();
            var capacity = -1;

            while(true)
            {
                //numbers.Capacity - Gets or sets the total number of elems the internal data structure can hold
                if (numbers.Capacity != capacity)
                {
                    capacity = numbers.Capacity;
                    Console.WriteLine(capacity);
                }

                numbers.Add(1);
            }

            //Eventually: 'Exception of type 'System.OutOfMemoryException' was thrown.'
            //What's the point:
            //List starts with cap. of 0 (no capacity to store anything).
            //As soon we insert the first number - capacity grew to hold 4 items.
            //When we add the second number - it doubles the size (8)
            //Third number - 16
            //Internal algorithm: When list capacity is exceeded - it simply DOUBLES the capacity (it can hold twice as many things)
        }

        private static void Queues() //FIFO
        {
            //Enqueue() - Adds an object to the end of the Queue<T>
            //Peek() - Returns the object at the beginning of the Queue<T> (without removing it)
            //Dequeue() - Removes and returns the object at the beginning of the Queue<T>
            //ToArray() - Copies the Queue<T> elements to a new arr

            var employees = new Queue<Employee>();
            employees.Enqueue(new Employee{Name="Scott"});
            employees.Enqueue(new Employee{Name="Alex"});
            employees.Enqueue(new Employee{Name="Jovan"});

            //employees[0] not possible

            while (employees.Count > 0)
            {
                var employee = employees.Dequeue();
                Console.WriteLine(employee.Name);
            }

            Console.Read();
        }

        private static void Stacks() //LIFO
        {
            //Push()
            //Pop()
            //Peek() - Returns the last added item to the Stack<T> (without removing it)
            //ToArray() - Copies the Stack<T> elements to a new arr

            var employees = new Stack<Employee>();
            employees.Push(new Employee { Name = "Scott" });
            employees.Push(new Employee { Name = "Alex" });

            //employees[0] not possible

            while (employees.Count > 0)
            {
                var employee = employees.Pop();
                Console.WriteLine(employee.Name);
            }

            Console.Read();
        }

        private static void Sets()
        {
            var numbers = new HashSet<int>();
            numbers.Add(3);
            numbers.Add(2);
            numbers.Add(1);            
            numbers.Add(2);

            foreach (var number in numbers)
            {
                Console.WriteLine(number);
            }

            Console.WriteLine("\n---\n");

            var employees = new HashSet<Employee>();
            var scott = new Employee() {Name = "Scott"};
            employees.Add(scott);
            employees.Add(scott);            
            employees.Add(new Employee {Name = "Scott"});

            foreach (var employee in employees)
            {
                Console.WriteLine(employee.Name);
            }
        }

        static void CountEmloyees()
        {
            var employeesByName = new SortedList<string, List<Employee>>();

            employeesByName.Add("Sales", new List<Employee> { new Employee(), new Employee(), new Employee() });
            employeesByName.Add("Engineering", new List<Employee> { new Employee(), new Employee() });

            foreach (var item in employeesByName)
            {
                Console.WriteLine("The count of employees for {0} is {1}",
                    item.Key, item.Value.Count
                    );
            }
        }

        private static void SortedWithComparer()
        {
            var employeesByDepartment = new SortedList<string, HashSet<Employee>>();

            employeesByDepartment.Add("Sales", new HashSet<Employee>(new EmployeeComparer()));
            employeesByDepartment["Sales"].Add(new Employee { Name = "Alex" });
            employeesByDepartment["Sales"].Add(new Employee { Name = "Alex" });

            employeesByDepartment.Add("Engineering", new HashSet<Employee>(new EmployeeComparer()));
            employeesByDepartment["Engineering"].Add(new Employee { Name = "Scott" });
            employeesByDepartment["Engineering"].Add(new Employee { Name = "Joy" });

            foreach (var pair in employeesByDepartment)
            {
                Console.WriteLine("Department {0}", pair.Key);
                foreach (var employee in pair.Value)
                {
                    Console.WriteLine("\t{0}", employee.Name);
                }
            }
        }

        static void SortedAndClean()
        {
             var employeesByDepartment = new DepartmentList();

            employeesByDepartment.Add("Sales", new Employee { Name = "Alex" })
                                 .Add("Sales", new Employee { Name = "Alex" });

            employeesByDepartment.Add("Engineering",new Employee { Name = "Scott" })
                                 .Add("Engineering", new Employee { Name = "Joy" });

            foreach (var pair in employeesByDepartment)
            {
                Console.WriteLine("Department {0}", pair.Key);
                foreach (var employee in pair.Value)
                {
                    Console.WriteLine("\t{0}", employee.Name);
                }
            }
        }
    }
}
