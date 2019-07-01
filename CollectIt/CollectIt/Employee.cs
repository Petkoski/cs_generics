
using System;
using System.Collections.Generic;

namespace CollectIt
{
    //It must implement IEqualityComparer<T>
    //public class EmployeeComparer<T> : IEqualityComparer<T> //Not like this, we know that this class is dedicated to Employee
    //The class is not generic, but implements generic interface
    public class EmployeeComparer : IEqualityComparer<Employee>
    {
        public bool Equals(Employee x, Employee y)
        {
            return x.Name == y.Name; //If names are equal, our employees are equal
        }

        public int GetHashCode(Employee obj)
        {
            return obj.Name.GetHashCode(); //This will make sure that 2 employees with the same name will generate the same hashcode
        }
    }

    public class EmployeeComparer2 : IEqualityComparer<Employee>, IComparer<Employee>
    {
        public int Compare(Employee x, Employee y)
        {
            return String.Compare(x.Name, y.Name);
        }

        public bool Equals(Employee x, Employee y)
        {
            return x.Name == y.Name; //If names are equal, our employees are equal
        }

        public int GetHashCode(Employee obj)
        {
            return obj.Name.GetHashCode(); //This will make sure that 2 employees with the same name will generate the same hashcode
        }
    }


    public class Employee
    {
        public string Name { get; set; }
        public int DepartmentId { get; set; }
    }    
}
