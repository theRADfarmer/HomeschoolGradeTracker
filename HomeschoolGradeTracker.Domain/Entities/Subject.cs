using System.ComponentModel.DataAnnotations;

namespace HomeschoolGradeTracker.Domain.Entities
{
    public class Subject
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; } = string.Empty;

        public ICollection<Assignment> Assignments { get; set; } = new List<Assignment>();

    }
}
