using Microsoft.EntityFrameworkCore;
using WebApplication2.Models.Domain;

namespace WebApplication2.Data
{
    public class mvcDemoDbContext : DbContext
    {
        public mvcDemoDbContext(DbContextOptions<mvcDemoDbContext> options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
    }
}
