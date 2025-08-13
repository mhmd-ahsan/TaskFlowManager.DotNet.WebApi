using MySql.Data.MySqlClient;
using System.Data;

namespace TaskFlowManager.Api.Data
{
    public class DapperContext
    {
        private readonly IConfiguration _config;
        private readonly string? _connectionString;

        public DapperContext(IConfiguration config)
        {
            _config = config;
            _connectionString = _config.GetConnectionString("DefaultConnection");
        }

        public IDbConnection CreateConnection()
            => new MySqlConnection(_connectionString);
    }
}
