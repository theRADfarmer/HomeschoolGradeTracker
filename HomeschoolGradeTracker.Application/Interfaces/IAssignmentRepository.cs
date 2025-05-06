using HomeschoolGradeTracker.Domain.Entities;

namespace HomeschoolGradeTracker.Application.Interfaces
{
    public interface IAssignmentRepository
    {
        Task<Assignment?> GetByIdAsync(int id);
        Task<List<Assignment>> GetBySubjectIdAsync(int subjectId);
        Task AddAsync(Assignment assignment);
        Task UpdateAsync(Assignment assignment);
        Task DeleteAsync(int id);
    }
}
