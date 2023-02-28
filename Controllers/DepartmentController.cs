using Luftborn_CRUD.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Luftborn_CRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly Luftborn_dbcontext _context;
        public DepartmentController(Luftborn_dbcontext context) {
            this._context = context;
        }


        [HttpGet]
        public IActionResult GetAllDepartments()
        {
            List<Department> deptList=_context.Departments.ToList();
            return Ok(deptList);
        }

        [HttpGet("{id}")]
        public  IActionResult GetDepartment(int id)
        {
            Department dept = _context.Departments.FirstOrDefault(o=>o.Id==id);

            if (dept == null)
            {
                return NotFound();
            }

            return Ok(dept);
        }

        private bool DepartmentExists(int id)
        {
            return _context.Departments.Any(o => o.Id == id);
        }

        
        [HttpPut("{id}")]
        public  IActionResult PutDepartment(int id, Department department)
        {
            if (id != department.Id)
            {
                return BadRequest();
            }

            try
            {
                _context.Entry(department).State = EntityState.Modified;
                _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) // to avoid concurrency violation
            {
                if (!DepartmentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Ok(department);
        }

        
        [HttpPost]
        public IActionResult PostDepartment(Department dept)
        {
            
            _context.Departments.Add(dept);
            _context.SaveChangesAsync();

            return Ok(dept);
        }

        // DELETE: api/Department/5
        [HttpDelete("{id}")]
        public IActionResult DeleteDepartment(int id)
        {
            var department = _context.Departments.Find(id);
            if (department == null)
            {
                return NotFound();
            }

            _context.Departments.Remove(department);
             _context.SaveChangesAsync();

            return Ok();
        }
    }
}
