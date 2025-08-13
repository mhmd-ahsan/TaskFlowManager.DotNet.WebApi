namespace TaskFlowManager.Api.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string Role { get; set; } = "User"; // Admin or User
        public DateTime CreatedAt { get; set; }
        public bool IsActive { get; set; }
    }
}
