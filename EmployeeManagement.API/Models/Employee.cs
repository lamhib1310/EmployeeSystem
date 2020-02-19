using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManagement.API.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public bool Active { get; set; }
        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; }


    }
    
    
}