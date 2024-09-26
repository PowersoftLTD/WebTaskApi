using System.Data;
using TaskManagement.API.Interfaces;
using TaskManagement.API.Model;
using Dapper;

namespace TaskManagement.API.Repositories
{
    public class EmployeeMasterRepository : IEmployeeMst
    {
        public IDapperDbConnection _dapperDbConnection;
        public EmployeeMasterRepository(IDapperDbConnection dapperDbConnection)
        {
            _dapperDbConnection = dapperDbConnection;
        }

        async Task<EMPLOYEE_MST> IEmployeeMst.LoginAsync(string UserName)
        {
            using (IDbConnection db = _dapperDbConnection.CreateConnection())
            {
                return await db.QueryFirstOrDefaultAsync<EMPLOYEE_MST>("SELECT EMAIL_ID_OFFICIAL,LOGIN_NAME,cast(LOGIN_PASSWORD as varchar) as LOGIN_PASSWORD FROM EMPLOYEE_MST WHERE EMAIL_ID_OFFICIAL = @UserName OR LOGIN_NAME = @UserName ", new { UserName = UserName });
            }
        }
        async Task<EMPLOYEE_MST> IEmployeeMst.CheckPasswordAsync(string Password)
        {
            using (IDbConnection db = _dapperDbConnection.CreateConnection())
            {
                return await db.QueryFirstOrDefaultAsync<EMPLOYEE_MST>("SELECT EMAIL_ID_OFFICIAL,LOGIN_NAME,cast(LOGIN_PASSWORD as varchar) as LOGIN_PASSWORD FROM EMPLOYEE_MST WHERE cast(LOGIN_PASSWORD as varchar) = @Password", new { Password = Password });
            }
        }

        
    }
}
