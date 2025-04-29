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
