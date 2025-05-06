using HomeschoolGradeTracker.Application.Interfaces;
using HomeschoolGradeTracker.Domain.Entities;
using HomeschoolGradeTracker.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

// SubjectRepository is the concrete implementation of IRepository using EF Core.
// It connects to ApplicationDbContext and performs actual database operations.
// Registered in DI as IRepository so that the Application layer remains unaware of EF Core.

namespace HomeschoolGradeTracker.Infrastructure.Repositories
{
    public class SubjectRepository : ISubjectRepository
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

        public async Task AddAsync(Subject subject)
        {
            _db.Subjects.Add(subject);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(Subject subject)
        {
            _db.Subjects.Update(subject);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(Subject subject)
        {
            _db.Subjects.Remove(subject);
            await _db.SaveChangesAsync();
        }

        public async Task<Subject?> GetByIdAsync(int id)
        {
            return await _db.Subjects.FindAsync(id);
        }
    }
}
