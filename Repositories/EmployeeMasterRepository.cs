using System.Data;
using TaskManagement.API.Interfaces;
using TaskManagement.API.Model;
using Dapper;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Mvc;

namespace TaskManagement.API.Repositories
{
    public class EmployeeMasterRepository : IEmployeeMst
    {
        public IDapperDbConnection _dapperDbConnection;
        private readonly ITokenRepository _tokenRepository;
        public EmployeeMasterRepository(IDapperDbConnection dapperDbConnection, ITokenRepository tokenRepository)
        {
            _dapperDbConnection = dapperDbConnection;
            _tokenRepository = tokenRepository;
        }

        public async Task<EMPLOYEE_MST> LoginAsync(string UserName)
        {
            using (IDbConnection db = _dapperDbConnection.CreateConnection())
            {
                return await db.QueryFirstOrDefaultAsync<EMPLOYEE_MST>("SELECT EMAIL_ID_OFFICIAL,LOGIN_NAME,cast(LOGIN_PASSWORD as varchar)" +
                    " as LOGIN_PASSWORD FROM EMPLOYEE_MST WHERE EMAIL_ID_OFFICIAL = @UserName OR LOGIN_NAME = @UserName ",
                    new { UserName = UserName });
            }
        }
        public async Task<ActionResult<EMPLOYEE_MST>> CheckPasswordAsync(string UserName, string Password)
        {
            //using (IDbConnection db = _dapperDbConnection.CreateConnection())
            //{
            //    var PassResponse = await db.QueryFirstOrDefaultAsync<EMPLOYEE_MST>("SELECT EMAIL_ID_OFFICIAL,LOGIN_NAME,cast(LOGIN_PASSWORD as varchar) " +
            //        " as LOGIN_PASSWORD FROM EMPLOYEE_MST WHERE (EMAIL_ID_OFFICIAL = @UserName OR LOGIN_NAME = @UserName) " +
            //        " AND [LOGIN_PASSWORD] = @Password", new { UserName = UserName, Password = Password });
            //    return PassResponse;
            //}

            using (IDbConnection db = _dapperDbConnection.CreateConnection())
            {
                var parmeters = new DynamicParameters();
                parmeters.Add("@LoginName", UserName);
                parmeters.Add("@P_LOGIN_PASSWORD", Password);
                var dtReponse = await db.QueryFirstOrDefaultAsync<EMPLOYEE_MST>("SP_GET_LOGIN_USER", parmeters, commandType: CommandType.StoredProcedure);
                return dtReponse;
                //var PassResponse = await db.QueryFirstOrDefaultAsync<EMPLOYEE_MST>("SELECT EMAIL_ID_OFFICIAL,LOGIN_NAME,cast(LOGIN_PASSWORD as varchar) " +
                //    " as LOGIN_PASSWORD FROM EMPLOYEE_MST WHERE (EMAIL_ID_OFFICIAL = @UserName OR LOGIN_NAME = @UserName) " +
                //    " AND [LOGIN_PASSWORD] = @Password", new { UserName = UserName, Password = Password });
                //return PassResponse;
            }
        }
        private bool IsValid(string token)
        {
            JwtSecurityToken jwtSecurityToken;
            try
            {
                jwtSecurityToken = new JwtSecurityToken(token);
            }
            catch (Exception)
            {
                return false;
            }

            return jwtSecurityToken.ValidTo > DateTime.UtcNow;
        }

        public async Task<EMPLOYEE_MST> LoginNTAsync(EMPLOYEE_MST_NT eMPLOYEE_MST_NT)
        {
            using (IDbConnection db = _dapperDbConnection.CreateConnection())
            {
                return await db.QueryFirstOrDefaultAsync<EMPLOYEE_MST>("SELECT EMAIL_ID_OFFICIAL,LOGIN_NAME,cast(LOGIN_PASSWORD as varchar)" +
                               " as LOGIN_PASSWORD FROM EMPLOYEE_MST WHERE EMAIL_ID_OFFICIAL = @UserName OR LOGIN_NAME = @UserName ",
                             new { UserName = eMPLOYEE_MST_NT.LOGIN_NAME });
            }
        }

        public async Task<EMPLOYEE_MST> CheckPasswordNTAsync(EMPLOYEE_MST_NT eMPLOYEE_MST_NT)
        {
            using (IDbConnection db = _dapperDbConnection.CreateConnection())
            {
                var PassResponse = await db.QueryFirstOrDefaultAsync<EMPLOYEE_MST>("SELECT EMAIL_ID_OFFICIAL,LOGIN_NAME,cast(LOGIN_PASSWORD as varchar) " +
                    " as LOGIN_PASSWORD FROM EMPLOYEE_MST WHERE (EMAIL_ID_OFFICIAL = @UserName OR LOGIN_NAME = @UserName) " +
                    " AND [LOGIN_PASSWORD] = @Password", new { UserName = eMPLOYEE_MST_NT.LOGIN_NAME, Password = eMPLOYEE_MST_NT.LOGIN_PASSWORD });
                return PassResponse;
            }
        }
    }
}
