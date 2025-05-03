using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HomeschoolGradeTracker.Domain.Entities
{
    public class Assignment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Assignment")]
        public string AssignmentName { get; set; } = string.Empty;

        public string? Description { get; set; } = string.Empty;

        [DataType(DataType.Date)]
        [Display(Name = "Completion Date")]
        public DateTime? DateCompleted { get; set; }

        [Range(0, 100)]
        public double? Grade { get; set; }

        [ForeignKey(nameof(Subject))]
        public int SubjectId { get; set; }

        public Subject? Subject { get; set; }
    }
}
