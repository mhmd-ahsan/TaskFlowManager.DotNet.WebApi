using Dapper;
using TaskFlowManager.Api.Data;
using TaskFlowManager.Api.Models;
using TaskFlowManager.Api.Repositories.Interfaces;

namespace TaskFlowManager.Api.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly DapperContext _context;

        public UserRepository(DapperContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<User>> GetAllAsync()
        {
            var sql = @"SELECT * FROM Users";
            using var conn = _context.CreateConnection();
            return await conn.QueryAsync<User>(sql);
        }

        public async Task<User?> GetByIdAsync(int userId)
        {
            var sql = @"SELECT * FROM Users WHERE UserId = @UserId";
            using var conn = _context.CreateConnection();
            return await conn.QueryFirstOrDefaultAsync<User>(sql, new { UserId = userId });
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            var sql = @"SELECT * FROM Users WHERE Email = @Email";
            using var conn = _context.CreateConnection();
            return await conn.QueryFirstOrDefaultAsync<User>(sql, new { Email = email });
        }

        public async Task<int> CreateAsync(User user)
        {
            var sql = @"INSERT INTO Users (FullName, Email, PasswordHash, Role,CreatedAt, IsActive)
            VALUES(@FullName, @Email, @PasswordHash, @Role, @CreatedAt, @IsActive); SELECT LAST_INSERT_ID();";
            using var conn = _context.CreateConnection();
            return await conn.ExecuteAsync(sql, user);
        }

        public async Task<bool> UpdateAsync(User user)
        {
            var sql = @"UPDATE Users 
                        SET FullName = @FullName, 
                            Email = @Email, 
                            Role = @Role, 
                            IsActive = @IsActive
                        WHERE UserId = @UserId";
            using var conn = _context.CreateConnection();
            var rowsAffected = await conn.ExecuteAsync(sql, user);
            return rowsAffected > 0;
        }

        public async Task<bool> DeleteAsync(int userId)
        {
            var sql = @"DELETE FROM Users WHERE UserId = @UserId";
            using var conn = _context.CreateConnection();
            var rowsAffected =  await conn.ExecuteAsync(sql, new { UserId = userId });
            return rowsAffected > 0;
        }

        public async Task<bool> ActivateDeactivateAsync(int userId, bool isActive)
        {
            var sql = @"UPDATE Users SET IsActive = @IsActive WHERE UserId = @UserId";
            using var conn = _context.CreateConnection();
            var rowsAffected = await conn.ExecuteAsync(sql, new {UserId = userId});
            return rowsAffected > 0;
        }
    }
}
