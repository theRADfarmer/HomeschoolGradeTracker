using HomeschoolGradeTracker.Application.Services;
using HomeschoolGradeTracker.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HomeschoolGradeTracker.Web.Pages.Subjects
{
    public class EditPartialModel(SubjectService subjectService) : PageModel
    {
        private readonly SubjectService _subjectService = subjectService;

        [BindProperty]
        public Subject Subject { get; set; } = new();

        public async Task<IActionResult> OnGetAsync([FromQuery] int id)
        {
            var subject = await _subjectService.GetSubjectByIdAsync(id);
            if (subject == null)
            {
                return NotFound();
            }

            Subject = subject;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _subjectService.UpdateSubjectAsync(Subject);
            return Partial("_SuccessPartial");
        }
    }
}
