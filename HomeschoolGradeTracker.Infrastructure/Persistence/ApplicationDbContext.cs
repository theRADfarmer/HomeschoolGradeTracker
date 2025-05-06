using HomeschoolGradeTracker.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HomeschoolGradeTracker.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Subject> Subjects => Set<Subject>();
        public DbSet<Assignment> Assignments => Set<Assignment>();
    }
}

// Add migration command (run from web project directory):
// dotnet ef migrations add [migration name] --project ../HomeschoolGradeTracker.Infrastructure --startup-project . --output-dir Persistence/Migrations

// Update database command (run from web project directory): 
// dotnet ef database update --project ../HomeschoolGradeTracker.Infrastructure --startup-project .