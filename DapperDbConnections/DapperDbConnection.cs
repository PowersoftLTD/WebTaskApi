using System.Data;
using System.Data.SqlClient;
using TaskManagement.API.Interfaces;

namespace TaskManagement.API.DapperDbConnections
{
    public class DapperDbConnection : IDapperDbConnection
    {
        public readonly string _connectionString;

        public DapperDbConnection(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("AuthonticatServerConnectionString");
        }
        public IDbConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
