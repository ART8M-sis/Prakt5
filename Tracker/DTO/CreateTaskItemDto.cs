using System.ComponentModel.DataAnnotations;
using Tracker.Models;

namespace Tracker.DTO
{
    public class CreateTaskItemDto
    {
        [Required, MaxLength(150)]
        public string Title { get; set; } = string.Empty;

        [MaxLength(1000)]
        public string? Description { get; set; }

        public TaskPriority Priority { get; set; }

        public TaskStatus Status { get; set; }

        [Required]
        public Guid ProjectId { get; set; }
    }
}
