using TaskFlowManager.Api.Helpers;
using TaskFlowManager.Api.Models;
using TaskFlowManager.Api.Repositories.Interfaces;
using TaskFlowManager.Api.Services.Interfaces;

namespace TaskFlowManager.Api.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repo;
        private readonly JwtHelper _jwtHelper;

        public UserService(IUserRepository repo, JwtHelper jwtHelper)
        {
            _repo = repo;
            _jwtHelper = jwtHelper;
        }

        // Get all users
        public async Task<ResponseHelper<IEnumerable<User>>> GetAllUsersAsync()
        {
            var users = await _repo.GetAllAsync();
            return ResponseHelper<IEnumerable<User>>.Ok(users);
        }

        // Get user by Id
        public async Task<ResponseHelper<User>> GetUserByIdAsync(int userId)
        {
            var user = await _repo.GetByIdAsync(userId);
            if (user == null)
                return ResponseHelper<User>.Fail("User not found");

            return ResponseHelper<User>.Ok(user);
        }

        // Get user by Email
        public async Task<ResponseHelper<User>> GetUserByEmailAsync(string email)
        {
            var user = await _repo.GetByEmailAsync(email);
            if (user == null)
                return ResponseHelper<User>.Fail($"User not found with email: {email}");

            return ResponseHelper<User>.Ok(user);
        }

        // Register user with password hashing
        public async Task<ResponseHelper<int>> RegisterUserAsync(User user, string password)
        {
            try
            {
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(password);
                user.CreatedAt = DateTime.Now;
                user.IsActive = true;

                var userId = await _repo.CreateAsync(user);
                return ResponseHelper<int>.Ok(userId, "User registered successfully");
            }
            catch (Exception ex)
            {
                return ResponseHelper<int>.Fail($"Registration failed: {ex.Message}");
            }
        }

        // Login user and return JWT token via JwtHelper
        public async Task<ResponseHelper<string>> LoginUserAsync(string email, string password)
        {
            var user = await _repo.GetByEmailAsync(email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
                return ResponseHelper<string>.Fail("Invalid email or password");

            var jwt = _jwtHelper.GenerateJwtToken(user.UserId, user.Email, user.Role);
            return ResponseHelper<string>.Ok(jwt, "Login successful");
        }

        // Activate or Deactivate user
        public async Task<ResponseHelper<bool>> ActivateDeactivateUserAsync(int userId, bool isActive)
        {
            var result = await _repo.ActivateDeactivateAsync(userId, isActive);
            if (!result)
                return ResponseHelper<bool>.Fail("User not found or update failed");

            return ResponseHelper<bool>.Ok(result, "User status updated");
        }
    }
}
