using System.ComponentModel.DataAnnotations;

namespace NotificationCenter.DTOs
{
    public class NotificationTaskDto
    {
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Title { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Description { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(255)]
        public string Email { get; set; } = string.Empty;

        [Required]
        public DateTime ScheduledTime { get; set; }
    }
}
