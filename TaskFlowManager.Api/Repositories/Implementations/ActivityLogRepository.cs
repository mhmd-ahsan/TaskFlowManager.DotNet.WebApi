using Dapper;
using TaskFlowManager.Api.Data;
using TaskFlowManager.Api.Models;
using TaskFlowManager.Api.Repositories.Interfaces;

namespace TaskFlowManager.Api.Repositories.Implementations
{
    public class ActivityLogRepository : IActivityLogRepository
    {
        private readonly DapperContext _context;


        public ActivityLogRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<int> CreateAsync(ActivityLog log)
        {
            var sql = @"INSERT INTO ActivityLog (UserId, Action, CreatedAt) 
                        VALUES (@UserId, @Action, @CreatedAt);
                        SELECT LAST_INSERT_ID();";
            using var conn = _context.CreateConnection();
            return await conn.ExecuteScalarAsync<int>(sql, log);
        }

        public async Task<IEnumerable<ActivityLog>> GetAllByUserIdAsync(int userId)
        {
            var sql = @"Select * From ActivityLog WHERE UserId = @UserId";
            using var conn = _context.CreateConnection();
            return await conn.QueryAsync<ActivityLog>(sql, new {UserId = userId });
        }
    }
}
