using EmployeeManagement.API.Models;

using Microsoft.EntityFrameworkCore;


namespace EmployeeManagement.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> optins) : base (optins)
        {

        }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<Department> Departments {get; set;}
    }
}