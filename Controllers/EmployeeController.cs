using Luftborn_CRUD.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Luftborn_CRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly Luftborn_dbcontext _context;

        public EmployeeController(Luftborn_dbcontext context)
        {
            this._context = context;
        }

        [HttpGet]
        public IActionResult GetAllEmployees()
        {
            List<Employee> empList = _context.Employees.ToList();
            return Ok(empList);
        }

        [HttpGet("{id}")]
        public IActionResult GetEmployee(int id)
        {
            Employee emp = _context.Employees.FirstOrDefault(o => o.id == id);

            if (emp == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(emp);
            }

        }


        [HttpPost]
        public IActionResult Postemployee(Employee employee)
        {
            _context.Employees.Add(employee);
            employee.depto = employee.deptID; //set non-foreign reference to dept to avoid including a reference
            _context.SaveChangesAsync();

            return Ok(employee);
        }

        private bool employeeExists(int id)
        {
            return _context.Employees.Any(o => o.id == id);
        }

        // PUT: api/employee/5
        [HttpPut("{id}")]
        public IActionResult Putemployee(int id, Employee employee)
        {
            if (id != employee.id)
            {
                return BadRequest();
            }

            try
            {
                _context.Entry(employee).State = EntityState.Modified;
                employee.depto = employee.deptID; //update depto
                _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) // to avoid concurrency violation
            {
                if (!employeeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Ok(employee);
        }

        [HttpDelete("{id}")]
        public IActionResult Deleteemployee(int id)
        {
            Employee employee = _context.Employees.Find(id);
            if (employee == null)
            {
                return NotFound();
            }

            _context.Employees.Remove(employee);
            _context.SaveChangesAsync();

            return Ok();
        }
    }
}
