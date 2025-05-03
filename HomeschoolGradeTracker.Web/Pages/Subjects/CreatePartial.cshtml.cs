using HomeschoolGradeTracker.Application.Services;
using HomeschoolGradeTracker.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HomeschoolGradeTracker.Web.Pages.Subjects
{
    public class CreatePartialModel : PageModel
    {
        private readonly SubjectService _subjectService;

        public CreatePartialModel(SubjectService subjectService)
        {
            _subjectService = subjectService;
        }

        [BindProperty]
        public Subject Subject { get; set; } = new();

        public IActionResult OnGet() => Page();

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Partial("CreatePartial", this); // Re-render form with errors

            await _subjectService.AddSubjectAsync(Subject);

            return Partial("_SuccessPartial");
        }

    }

}
