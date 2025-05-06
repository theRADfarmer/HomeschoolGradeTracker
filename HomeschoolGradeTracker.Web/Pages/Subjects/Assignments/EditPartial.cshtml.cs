using HomeschoolGradeTracker.Application.Services;
using HomeschoolGradeTracker.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HomeschoolGradeTracker.Web.Pages.Subjects.Assignments
{
    public class EditPartialModel(AssignmentService assignmentService) : PageModel
    {
        AssignmentService _assignmentService = assignmentService;

        [BindProperty]
        public Assignment Assignment { get; set; } = new();

        [BindProperty(SupportsGet = true)]
        public int SubjectId { get; set; }

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
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _assignmentService.UpdateAssignmentAsync(Assignment);
            return Partial("_SuccessPartial");
        }
    }
}
