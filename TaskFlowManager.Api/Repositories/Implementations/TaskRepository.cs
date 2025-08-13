using Dapper;
using TaskFlowManager.Api.Data;
using TaskFlowManager.Api.Models;
using TaskFlowManager.Api.Repositories.Interfaces;

namespace TaskFlowManager.Api.Repositories.Implementations
{
    public class TaskRepository : ITaskRepository
    {
        private readonly DapperContext _context;

        public TaskRepository(DapperContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<TaskItem>> GetAllAsync()
        {
            var sql = @"SELECT * From Tasks";
            using var conn = _context.CreateConnection();
            return await conn.QueryAsync<TaskItem>(sql);
        }

        public async Task<TaskItem?> GetByIdAsync(int taskId)
        {
            var sql = @"SELECT * FROM Tasks WHERE TaskId = @TaskId";
            using var conn = _context.CreateConnection();
            return await conn.QueryFirstOrDefaultAsync<TaskItem>(sql, new { TaskId = taskId });
        }

        public async Task<int> CreateAsync(TaskItem task)
        {
            var sql = @"INSERT INTO Tasks 
                        (Title, Description, Priority, Status, StartDate, EndDate, AssignedTo, CreatedBy, CreatedAt) 
                        VALUES 
                        (@Title, @Description, @Priority, @Status, @StartDate, @EndDate, @AssignedTo, @CreatedBy, @CreatedAt);
                        SELECT LAST_INSERT_ID();";
            using var conn = _context.CreateConnection();
            return await conn.ExecuteScalarAsync<int>(sql, task);
        }

        public async Task<bool> UpdateAsync(TaskItem task)
        {
            var sql = @"UPDATE Tasks SET 
                        Title = @Title, 
                        Description = @Description, 
                        Priority = @Priority, 
                        Status = @Status,
                        StartDate = @StartDate,
                        EndDate = @EndDate,
                        AssignedTo = @AssignedTo
                        WHERE TaskId = @TaskId";
            using var conn = _context.CreateConnection();
            var rowsAffected = await conn.ExecuteAsync(sql,task);
            return rowsAffected > 0;
        }

        public async Task<bool> DeleteAsync(int taskId)
        {
            var sql = @"DELETE FROM Tasks WHERE TaskId = @TaskId";
            using var conn = _context.CreateConnection();
            var rowsAffected = await conn.ExecuteAsync(sql, new { TaskId = taskId });
            return rowsAffected > 0;
        }
    }
}
