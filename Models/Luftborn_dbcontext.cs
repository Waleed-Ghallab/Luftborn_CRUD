using Microsoft.EntityFrameworkCore;

namespace Luftborn_CRUD.Models
{
    public class Luftborn_dbcontext:DbContext
    {
        public Luftborn_dbcontext()
        {

        }
        public Luftborn_dbcontext(DbContextOptions options) : base(options) 
        {
            
        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }

    }
}
