using System.ComponentModel.DataAnnotations;

namespace HomeschoolGradeTracker.Domain.Entities
{
    public class Assignment
    {
        [Key]
        public int Id { get; set; }
        public int SubjectId { get; set; }
        public Subject Subject { get; set; } = default!;
        [Required]
        public string AssignmentName { get; set; } = string.Empty;
        public DateTime DateCompleted { get; set; }
        public double Grade { get; set; }
    }
}
