using HomeschoolGradeTracker.Application.Interfaces;
using HomeschoolGradeTracker.Domain.Entities;

// SubjectService contains the core business logic for managing subjects.
// It depends on the ISubjectRepository interface rather than any concrete DbContext,
// which allows for loose coupling, easier testing, and adherence to Clean Architecture principles.

namespace HomeschoolGradeTracker.Application.Services
{
    public class SubjectService
    {
        private readonly IRepository _subjectRepo;

        public SubjectService(IRepository subjectRepo)
        {
            _subjectRepo = subjectRepo;
        }

        public async Task<List<Subject>> GetAllSubjectsAsync()
        {
            return await _subjectRepo.GetAllAsync();
        }

        public async Task AddSubjectAsync(Subject subject)
        {
            ArgumentNullException.ThrowIfNull(subject);
            await _subjectRepo.AddAsync(subject);
        }

        public async Task UpdateSubjectAsync(Subject updatedSubject)
        {
            var existing = await _subjectRepo.GetByIdAsync(updatedSubject.Id) ?? throw new KeyNotFoundException($"Subject with ID {updatedSubject.Id} not found.");
            existing.Name = updatedSubject.Name;
            existing.Description = updatedSubject.Description;

            await _subjectRepo.UpdateAsync(existing);
        }

        public async Task DeleteSubjectAsync(int id)
        {
            var subject = await _subjectRepo.GetByIdAsync(id) ?? throw new KeyNotFoundException($"Subject with ID {id} not found.");
            await _subjectRepo.DeleteAsync(subject);
        }

        public async Task<Subject> GetSubjectByIdAsync(int id)
        {
            return await _subjectRepo.GetByIdAsync(id);
        }
    }
}
