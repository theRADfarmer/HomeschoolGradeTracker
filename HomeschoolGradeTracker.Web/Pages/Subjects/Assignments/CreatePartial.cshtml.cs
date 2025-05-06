using HomeschoolGradeTracker.Application.Services;
using HomeschoolGradeTracker.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HomeschoolGradeTracker.Web.Pages.Subjects.Assignments
{
    public class CreatePartialModel : PageModel
    {
        private readonly AssignmentService _assignmentService;

        public CreatePartialModel(AssignmentService assignmentService)
        {
            _assignmentService = assignmentService;
        }

        [BindProperty]
        public Assignment Assignment { get; set; } = new();

        [BindProperty(SupportsGet = true)]
        public int SubjectId { get; set; }

        public IActionResult OnGet() => Page();

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Partial("CreatePartial", this); // Re-render form with errors

            // Assign the SubjectId from the URL to the Assignment
            Assignment.SubjectId = SubjectId;

            await _assignmentService.AddAssignmentAsync(Assignment);

            return Partial("_SuccessPartial");
        }
    }
}
