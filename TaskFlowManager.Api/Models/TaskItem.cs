namespace TaskFlowManager.Api.Models
{
    public class TaskItem
    {
        public int TaskId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string Priority { get; set; } = "Medium"; // High, Medium, Low
        public string Status { get; set; } = "Pending"; // Pending, In Progress, Completed
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? AssignedTo { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
