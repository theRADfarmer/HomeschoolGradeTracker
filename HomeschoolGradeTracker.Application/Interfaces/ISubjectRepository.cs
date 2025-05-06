using HomeschoolGradeTracker.Domain.Entities;

// ISubjectRepository defines the contract for data access related to subjects.
// This abstraction allows the Application layer to remain decoupled from Infrastructure (EF Core, databases, etc).
// Infrastructure will provide the actual implementation of this interface.

namespace HomeschoolGradeTracker.Application.Interfaces
{
    public interface ISubjectRepository
    {
        Task<List<Subject>> GetAllAsync();
        Task AddAsync(Subject subject);
        Task UpdateAsync(Subject subject);
        Task DeleteAsync(Subject subject);
        Task<Subject?> GetByIdAsync(int id);
    }
}
