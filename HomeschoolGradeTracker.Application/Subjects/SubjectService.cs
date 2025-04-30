using HomeschoolGradeTracker.Application.Interfaces;
using HomeschoolGradeTracker.Domain.Entities;

// SubjectService contains the core business logic for managing subjects.
// It depends on the ISubjectRepository interface rather than any concrete DbContext,
// which allows for loose coupling, easier testing, and adherence to Clean Architecture principles.

namespace HomeschoolGradeTracker.Application.Subjects
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
            if (subject == null)
            {
                throw new ArgumentNullException(nameof(subject));
            }
            await _subjectRepo.AddAsync(subject);
        }
    }
}
