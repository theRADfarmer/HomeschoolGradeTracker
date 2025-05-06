using HomeschoolGradeTracker.Application.Services;
using HomeschoolGradeTracker.Domain.Entities;
using HomeschoolGradeTracker.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HomeschoolGradeTracker.Web.Pages.Subjects.Assignments
{
    public class IndexModel(AssignmentService assignmentService) : PageModel
    {
        private readonly AssignmentService _assignmentService = assignmentService;

        public AssignmentTableViewModel AssignmentTableViewModel { get; set; } = new();


        public async Task OnGetAsync(int subjectId)
        {
            this.AssignmentTableViewModel.SubjectId = subjectId;
            AssignmentTableViewModel.Assignments = await _assignmentService.GetAssignmentsBySubjectIdAsync(subjectId);
        }

    }
}
