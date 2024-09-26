using System.Data;

namespace TaskManagement.API.Interfaces
{
    public interface IDapperDbConnection
    {
        public IDbConnection CreateConnection();
    }
}
