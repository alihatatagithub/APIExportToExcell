using APIExportToExcell.Entities;
using Microsoft.EntityFrameworkCore;

namespace APIExportToExcell.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {

        }
        public DbSet<Employee> Employees { get; set; }
    }
}
