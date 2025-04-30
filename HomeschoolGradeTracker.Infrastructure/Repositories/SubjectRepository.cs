using HomeschoolGradeTracker.Application.Interfaces;
using HomeschoolGradeTracker.Domain.Entities;
using HomeschoolGradeTracker.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

// SubjectRepository is the concrete implementation of ISubjectRepository using EF Core.
// It connects to ApplicationDbContext and performs actual database operations.
// Registered in DI as ISubjectRepository so that the Application layer remains unaware of EF Core.

namespace HomeschoolGradeTracker.Infrastructure.Repositories
{
    public class SubjectRepository : IRepository
    {
        private readonly ApplicationDbContext _db;

        public SubjectRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<List<Subject>> GetAllAsync()
        {
            return await _db.Subjects.ToListAsync();
        }

        public Task AddAsync(Subject subject)
        {
            _db.Subjects.Add(subject);
            return _db.SaveChangesAsync();
        }
    }
}
