namespace TaskFlowManager.Api.Dtos.Responses
{
    public class LoginResponse
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public UserResponse User { get; set; }
    }
}
