using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.API.Models
{
    public class Department
    {
        public int DepartmentId { get; set; }
        public string Name { get; set; }
        public List<Employee> Employees{get; set;}
    }
}