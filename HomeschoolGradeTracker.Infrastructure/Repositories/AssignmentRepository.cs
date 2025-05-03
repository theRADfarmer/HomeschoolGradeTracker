using HomeschoolGradeTracker.Application.Interfaces;
using HomeschoolGradeTracker.Domain.Entities;
using HomeschoolGradeTracker.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace HomeschoolGradeTracker.Infrastructure.Repositories
{
    public class AssignmentRepository : IAssignmentRepository
    {
        private readonly ApplicationDbContext _db;

        public AssignmentRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<Assignment?> GetByIdAsync(int id)
        {
            return await _db.Assignments.Include(a => a.Subject)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<List<Assignment>> GetBySubjectIdAsync(int subjectId)
        {
            return await _db.Assignments
                .Where(a => a.SubjectId == subjectId)
                .ToListAsync();
        }

        public async Task AddAsync(Assignment assignment)
        {
            _db.Assignments.Add(assignment);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(Assignment assignment)
        {
            _db.Assignments.Update(assignment);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var assignment = await _db.Assignments.FindAsync(id);
            if (assignment != null)
            {
                _db.Assignments.Remove(assignment);
                await _db.SaveChangesAsync();
            }
        }
    }
}
