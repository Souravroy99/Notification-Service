namespace NotificationService.DTOs
{
    public class NotificationTaskResponseDto {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Status => IsSent ? "Sent" : "Pending";
        public DateTime ScheduledTime { get; set; }
        public bool IsSent { get; set; }
    }
}