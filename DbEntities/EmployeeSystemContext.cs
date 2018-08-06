using DbEntities.Models;
using EmployeeSystem.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EmployeeSystem.Data
{
    public class EmployeeSystemContext : IdentityDbContext<AspUser>
    {
        public EmployeeSystemContext(DbContextOptions<EmployeeSystemContext> options)
            : base(options)
        {
        }

        public DbSet<EmployeeUser> EmployeeUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
