using HomeschoolGradeTracker.Application.Interfaces;
using HomeschoolGradeTracker.Domain.Entities;

namespace HomeschoolGradeTracker.Application.Services
{
    public class AssignmentService(IAssignmentRepository assignmentRepo)
    {
        private readonly IAssignmentRepository _assignmentRepo = assignmentRepo;

        public Task<Assignment?> GetAssignmentByIdAsync(int id) => _assignmentRepo.GetByIdAsync(id);

        public Task<List<Assignment>> GetAssignmentsBySubjectIdAsync(int subjectId) => _assignmentRepo.GetBySubjectIdAsync(subjectId);

        public Task AddAssignmentAsync(Assignment assignment) => _assignmentRepo.AddAsync(assignment);

        public async Task UpdateAssignmentAsync(Assignment updatedAssignment)
        {
            var existing = await _assignmentRepo.GetByIdAsync(updatedAssignment.Id) ?? throw new KeyNotFoundException($"Assignment with ID {updatedAssignment.Id} not found.");
            existing.AssignmentName = updatedAssignment.AssignmentName;
            existing.Description = updatedAssignment.Description;
            existing.DateCompleted = updatedAssignment.DateCompleted;
            existing.Grade = updatedAssignment.Grade;

            await _assignmentRepo.UpdateAsync(existing);
        }

        public Task DeleteAssignmentAsync(int id) => _assignmentRepo.DeleteAsync(id);
    }
}
