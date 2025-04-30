using HomeschoolGradeTracker.Application.Services;
using HomeschoolGradeTracker.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HomeschoolGradeTracker.Web.Pages.Subjects
{
    public class CreateModel : PageModel
    {
        private readonly SubjectService _subjectService;

        [BindProperty]
        public Subject Subject { get; set; } = new();

        public CreateModel(SubjectService subjectService)
        {
            _subjectService = subjectService;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _subjectService.AddSubjectAsync(Subject);

            return RedirectToPage("/Subjects/Index");
        }
    }
}
