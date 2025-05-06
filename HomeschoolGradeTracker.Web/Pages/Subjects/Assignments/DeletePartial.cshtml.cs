using HomeschoolGradeTracker.Application.Services;
using HomeschoolGradeTracker.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HomeschoolGradeTracker.Web.Pages.Subjects.Assignments
{
    public class DeletePartialModel(AssignmentService assignmentService) : PageModel
    {
        private readonly AssignmentService _assignmentService = assignmentService;

        [BindProperty]
        public Assignment Assignment { get; set; } = new();

        public async Task<IActionResult> OnGetAsync([FromQuery] int id)
        {
            var assignment = await _assignmentService.GetAssignmentByIdAsync(id);
            if (assignment == null)
            {
                return NotFound();
            }

            Assignment = assignment;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _assignmentService.DeleteAssignmentAsync(Assignment.Id);
            return Partial("_SuccessPartial");
        }
    }
}
