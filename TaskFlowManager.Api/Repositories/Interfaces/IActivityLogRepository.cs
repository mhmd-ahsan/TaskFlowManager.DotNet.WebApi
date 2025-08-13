using TaskFlowManager.Api.Models;

namespace TaskFlowManager.Api.Repositories.Interfaces
{
    public interface IActivityLogRepository
    {
        Task<int> CreateAsync(ActivityLog log);
        Task<IEnumerable<ActivityLog>> GetAllByUserIdAsync(int userId);
    }
}
