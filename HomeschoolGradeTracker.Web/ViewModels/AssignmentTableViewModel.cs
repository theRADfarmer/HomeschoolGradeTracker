using HomeschoolGradeTracker.Domain.Entities;

namespace HomeschoolGradeTracker.Web.ViewModels
{
    public class AssignmentTableViewModel
    {
        public int SubjectId { get; set; }
        public List<Assignment> Assignments { get; set; } = [];
    }
}
