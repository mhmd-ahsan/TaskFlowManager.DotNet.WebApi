using Dapper;
using TaskFlowManager.Api.Data;
using TaskFlowManager.Api.Models;
using TaskFlowManager.Api.Repositories.Interfaces;

namespace TaskFlowManager.Api.Repositories.Implementations
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly DapperContext _context;

        public RefreshTokenRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<RefreshTokens?> GetByTokenAsync(string token)
        {
            var sql = @"SELECT * FROM RefreshTokens  WHERE Token = @Token";
            using var conn = _context.CreateConnection();
            return await conn.QueryFirstOrDefaultAsync<RefreshTokens?>(sql, token);   
        }

        public async Task<int> CreateAsync(RefreshTokens refreshToken)
        {
            var sql = @"INSERT INTO RefreshTokens (UserId, Token, ExpiryDate, CreatedAt, Revoked) 
                        VALUES (@UserId, @Token, @ExpiryDate, @CreatedAt, @Revoked);
                        SELECT LAST_INSERT_ID();";
            using var conn = _context.CreateConnection();
            return await conn.ExecuteScalarAsync<int>(sql, refreshToken);
        }

        public async Task<bool> RevokeAsync(int tokenId)
        {
            var sql = @"UPDATE RefreshTokens SET Revoked = TRUE WHERE TokenId = @TokenId";
            using var conn = _context.CreateConnection();
            var rowsAffected = await conn.ExecuteAsync(sql, new {TokenId = tokenId});
            return rowsAffected > 0;
        }

        public async Task<IEnumerable<RefreshTokens>> GetAllByUserIdAsync(int userId)
        {
            var sql = @"SELECT * FROM RefreshTokens WHERE UserId = @UserId";
            using var conn = _context.CreateConnection();
            return await conn.QueryAsync<RefreshTokens>(sql, userId);
        }
    }
}
