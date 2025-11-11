namespace NotificationService.Models
{
    public class NotificationTask
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime ScheduledTime { get; set; }
        public string Email { get; set; } = string.Empty;
        public bool IsSent { get; set; } = false;
    }
}