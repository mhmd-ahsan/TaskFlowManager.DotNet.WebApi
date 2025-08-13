namespace TaskFlowManager.Api.Models
{
    public class ActivityLog
    {
        public int LogId { get; set; }
        public int UserId { get; set; }
        public string Action { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
