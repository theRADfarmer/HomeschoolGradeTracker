using Microsoft.AspNetCore.Mvc.RazorPages;
using HomeschoolGradeTracker.Domain.Entities;
using HomeschoolGradeTracker.Application.Services;

namespace HomeschoolGradeTracker.Web.Pages.Subjects
{
    public class IndexModel(SubjectService subjectService) : PageModel
    {
        private readonly SubjectService _subjectService = subjectService;

        public List<Subject> Subjects { get; set; } = [];

        public async Task OnGetAsync()
        {
            Subjects = await _subjectService.GetAllSubjectsAsync();
        }
    }
}
