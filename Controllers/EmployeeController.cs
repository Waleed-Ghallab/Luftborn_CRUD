//using Luftborn_CRUD.Models;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;

//namespace Luftborn_CRUD.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class EmployeeController : ControllerBase
//    {
//        private readonly Luftborn_dbcontext _context;

//        public EmployeeController(Luftborn_dbcontext context)
//        {
//            this._context = context;
//        }

//        [HttpGet]
//        public IActionResult GetAllEmployees()
//        {
//            List<Employee> empList = _context.Employees.ToList();
//            return Ok(empList);
//        }

//        [HttpGet("{id}")]
//        public IActionResult GetEmployee(int id)
//        {
//            Employee emp = _context.Employees.FirstOrDefault(o => o.id == id);

//            if (emp == null)
//            {
//                return BadRequest();
//            }
//            else
//            {
//                return Ok(emp);
//            }

//        }


//        [HttpPost]
//        public IActionResult PostEmployee(Employee Employee)
//        {
//            _context.Employees.Add(Employee);
//            Employee.depto = Employee.deptID; //set non-foreign reference to dept to avoid including a reference
//            _context.SaveChangesAsync();

//            return Ok(Employee);
//        }

//        private bool EmployeeExists(int id)
//        {
//            return _context.Employees.Any(o => o.id == id);
//        }

//        // PUT: api/Employee/5
//        [HttpPut("{id}")]
//        public IActionResult PutEmployee(int id, Employee Employee)
//        {
//            if (id != Employee.id)
//            {
//                return BadRequest();
//            }

//            try
//            {
//                _context.Entry(Employee).State = EntityState.Modified;
//                Employee.depto = Employee.deptID; //update depto
//                _context.SaveChangesAsync();
//            }
//            catch (DbUpdateConcurrencyException) // to avoid concurrency violation
//            {
//                if (!EmployeeExists(id))
//                {
//                    return NotFound();
//                }
//                else
//                {
//                    throw;
//                }
//            }
//            return Ok(Employee);
//        }

//        [HttpDelete("{id}")]
//        public IActionResult DeleteEmployee(int id)
//        {
//            Employee Employee = _context.Employees.Find(id);
//            if (Employee == null)
//            {
//                return NotFound();
//            }

//            _context.Employees.Remove(Employee);
//            _context.SaveChangesAsync();

//            return Ok();
//        }
//    }
//}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public Luftborn_dbcontext _context;

        public EmployeeController(Luftborn_dbcontext context)
        {
            _context = context;
        }

        // GET: api/Employee
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
        {
            return await _context.Employees.ToListAsync();
        }

        // GET: api/Employee/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
            var Employee = await _context.Employees.FindAsync(id);

            if (Employee == null)
            {
                return NotFound();
            }

            return Employee;
        }

        // PUT: api/Employee/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(int id, Employee Employee)
        {
            if (id != Employee.id)
            {
                return BadRequest();
            }

            _context.Entry(Employee).State = EntityState.Modified;
            Employee.depto = Employee.deptID; //update depto
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
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

        // POST: api/Employee
        [HttpPost]
        public async Task<ActionResult<Employee>> PostEmployee(Employee Employee)
        {
            Employee.depto = Employee.deptID;//set non-foreign reference to dept
            _context.Employees.Add(Employee);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEmployee", new { id = Employee.id }, Employee);
        }

        // DELETE: api/Employee/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var Employee = await _context.Employees.FindAsync(id);
            if (Employee == null)
            {
                return NotFound();
            }

            _context.Employees.Remove(Employee);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.id == id);
        }
    }
}
