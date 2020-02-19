using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManagement.API.Data;
using EmployeeManagement.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.API.Controllers
{
    [Route("api")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly DataContext _context;
        public EmployeeController(DataContext context)
        {
            _context = context;

        }
        // GET api/employees
        [HttpGet("employees")]
        public async Task<IActionResult> GetEmployees()
        {
            var values = await _context.Employees.Include(x => x.Department).ToListAsync();

            return Ok(values);
        }

        // GET api/employees/true|false
        [HttpGet("employees/{active:bool}")]
        public async Task<IActionResult> GetActiveOrInactiveEmployee(bool active)
        {
            var values = await _context.Employees.Where(x => x.Active == active).ToListAsync();

            return Ok(values);
   
        }

        // GET api/employees/4
        [HttpGet("employees/{id}")]
        public async Task<IActionResult> GetEmployee(int id)
        {
            var value = await _context.Employees.FirstOrDefaultAsync(x => x.EmployeeId == id);

            return Ok(value);
        }

        // GET api/department/1/employees/false?
        /*[HttpGet("department/{departmentId:range(0,2)}/employees/{active:bool?}")]
        public async Task<IActionResult> GetEmployeesOfDepartment(int departmentId, bool active = true)
        {
            var values = await _context.Employees.Where(x => x.Employees.DepartmentId == departmentId && x.Active == active).ToListAsync();

            return Ok(values);
        }*/

        // POST api/employees
        [HttpPost("employees")]
        public async Task<ActionResult> PostEmployee(Employee employee)
        {
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEmployees", new Employee{ EmployeeId = employee.EmployeeId}, employee);
        }

        [HttpPut("employees/{id}")]
        public async Task<ActionResult> PutEmployee(int id, Employee employee)
        {
            if (id != employee.EmployeeId)
            {
                return BadRequest();
            }

            _context.Entry(employee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }


            return NoContent();
        }

         private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.EmployeeId == id);
        }

        // DELETE api/employees/5
        [HttpDelete("employees/{id}")]
        public async Task<ActionResult<Employee>> DeleteEmployee(int id)
        {
            var deleteEmployee = await _context.Employees.FindAsync(id);
            if (deleteEmployee == null)
            {
                return NotFound();
            }

            _context.Employees.Remove(deleteEmployee);
            await _context.SaveChangesAsync();

            return deleteEmployee;
        }
        
    }
}