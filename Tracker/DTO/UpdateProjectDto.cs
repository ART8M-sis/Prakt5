using System.ComponentModel.DataAnnotations;

namespace Tracker.DTO
{
    public class UpdateProjectDto
    {
        [Required, MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(1000)]
        public string? Description { get; set; }
    }
}
