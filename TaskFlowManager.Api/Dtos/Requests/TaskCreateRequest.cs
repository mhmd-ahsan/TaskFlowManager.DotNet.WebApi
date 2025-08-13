namespace TaskFlowManager.Api.Dtos.Requests
{
    public class TaskCreateRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }     // matches DB
        public string Priority { get; set; }       // High, Medium, Low
        public string Status { get; set; } = "Pending";
        public int AssignedTo { get; set; }        // UserId
    }
}
