using System.Collections.Generic;

namespace CollectIt
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class DepartmentCollection : SortedDictionary<string, SortedSet<Employee>>
    {
        public DepartmentCollection Add(string departmentName, Employee employee)
        {
            if (!ContainsKey(departmentName))
            {
                Add(departmentName, new SortedSet<Employee>(new EmployeeComparer2()));
            }
            this[departmentName].Add(employee);
            return this;
        }
    }
}