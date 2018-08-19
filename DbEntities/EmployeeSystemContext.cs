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
        public DbSet<Department> Departments { get; set; }
        public DbSet<RequestType> RequestTypes { get; set; }
        public DbSet<Request> Request { get; set; }
        public DbSet<EmployeeUserProject> EmployeeUserProjects { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<EmployeePosition> EmployeePositions { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
