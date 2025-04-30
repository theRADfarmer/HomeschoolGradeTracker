using HomeschoolGradeTracker.Application.Services;
using HomeschoolGradeTracker.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Reflection.Metadata.Ecma335;

namespace HomeschoolGradeTracker.Web.Pages.Subjects
{
    public class DeleteModel(SubjectService subjectService) : PageModel
    {
        private readonly SubjectService _subjectService = subjectService;

        [BindProperty]
        public Subject Subject { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int id)
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
            await _subjectService.DeleteSubjectAsync(Subject.Id);
            return RedirectToPage("/Subjects/Index");
        }
    }
}
