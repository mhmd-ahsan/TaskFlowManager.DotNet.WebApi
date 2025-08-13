using TaskFlowManager.Api.Models;

namespace TaskFlowManager.Api.Repositories.Interfaces
{
    public interface IRefreshTokenRepository
    {
        Task<RefreshTokens?> GetByTokenAsync(string token);
        Task<int> CreateAsync(RefreshTokens refreshToken);
        Task<bool> RevokeAsync(int tokenId);
        Task<IEnumerable<RefreshTokens>> GetAllByUserIdAsync(int userId);
    }
}
