using Employee_Department.Models;
using Employee_Department.Models.View_Model;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Employee_Department.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        { }

        // DbSet for Employees
        public DbSet<Employee> Employees { get; set; }

        // DbSet for Departments
        public DbSet<Department> Departments { get; set; }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                var entity=entry.Entity;
                if (entry.Entity is Employee employee)
                {

                    if (entry.State == EntityState.Deleted && entity is ISoftDelete)
                    {
                        employee.IsDeleted = true;
                        entry.State = EntityState.Modified;
                    }
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Employee>()
                .HasQueryFilter(e => !e.IsDeleted);
        }

        [NotMapped]
        public DbSet<EmployeeDepartmentSummaryView> EmployeeDepartmentSummaryViews { get; set; }

      
    }
}
