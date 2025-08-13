namespace TaskFlowManager.Api.Dtos.Responses
{
    public class TaskResponse
    {
        public int TaskId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }     // matches DB
        public string Priority { get; set; }
        public string Status { get; set; }
        public int AssignedTo { get; set; }
        public string AssignedToName { get; set; }
    }
}
