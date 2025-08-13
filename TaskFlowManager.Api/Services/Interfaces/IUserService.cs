using TaskFlowManager.Api.Helpers;
using TaskFlowManager.Api.Models;

namespace TaskFlowManager.Api.Services.Interfaces
{
    public interface IUserService
    {
        Task<ResponseHelper<IEnumerable<User>>> GetAllUsersAsync();
        Task<ResponseHelper<User>> GetUserByIdAsync(int userId);
        Task<ResponseHelper<User>> GetUserByEmailAsync(string email);
        Task<ResponseHelper<int>> RegisterUserAsync(User user, string password);
        Task<ResponseHelper<string>> LoginUserAsync(string email, string password);
        Task<ResponseHelper<bool>> ActivateDeactivateUserAsync(int userId, bool isActive);
    }
}
