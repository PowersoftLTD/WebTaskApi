using TaskManagement.API.Model;
using TaskManagement.API.Interfaces;
using Dapper;
using System.Data;
using TaskManagement.API.DapperDbConnections;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace TaskManagement.API.Repositories
{
    public class TASKRepository : ITASKRepository
    {
        public IDapperDbConnection _dapperDbConnection;
        public TASKRepository(IDapperDbConnection dapperDbConnection)
        {
            _dapperDbConnection = dapperDbConnection;
        }
        public async Task<IEnumerable<TASK_RECURSIVE_HDR>> GetAllTASKsAsync()
        {
            using (IDbConnection db = _dapperDbConnection.CreateConnection())
            {
                var parmeters = new DynamicParameters();
                parmeters.Add("@MKEY", null);
                return await db.QueryAsync<TASK_RECURSIVE_HDR>("SP_GET_TASK_RECURSIVE", parmeters, commandType: CommandType.StoredProcedure);
            }
        }
        public async Task<TASK_RECURSIVE_HDR> GetTaskByIdAsync(int id)
        {
            using (IDbConnection db = _dapperDbConnection.CreateConnection())
            {
                var parmeters = new DynamicParameters();
                parmeters.Add("@MKEY", id);
                return await db.QueryFirstOrDefaultAsync<TASK_RECURSIVE_HDR>("SP_GET_TASK_RECURSIVE", parmeters, commandType: CommandType.StoredProcedure);
            }
        }
        public async Task<int> CreateTASKAsync(TASK_RECURSIVE_HDR tASK_RECURSIVE_HDR)
        {
            using (IDbConnection db = _dapperDbConnection.CreateConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@TASK_NAME", tASK_RECURSIVE_HDR.TASK_NAME);
                parameters.Add("@TASK_DESCRIPTION", tASK_RECURSIVE_HDR.TASK_DESCRIPTION);
                parameters.Add("@TERM", tASK_RECURSIVE_HDR.TERM);
                parameters.Add("@STARTS", tASK_RECURSIVE_HDR.STARTS);
                parameters.Add("@ENDS", tASK_RECURSIVE_HDR.ENDS);
                parameters.Add("@CREATED_BY", tASK_RECURSIVE_HDR.CREATED_BY);
                parameters.Add("@LAST_UPDATED_BY", tASK_RECURSIVE_HDR.LAST_UPDATED_BY);
                parameters.Add("@ATTRIBUTE1", tASK_RECURSIVE_HDR.ATTRIBUTE1);
                parameters.Add("@ATTRIBUTE2", tASK_RECURSIVE_HDR.ATTRIBUTE2);
                parameters.Add("@ATTRIBUTE3", tASK_RECURSIVE_HDR.ATTRIBUTE3);
                parameters.Add("@ATTRIBUTE4", tASK_RECURSIVE_HDR.ATTRIBUTE4);
                parameters.Add("@ATTRIBUTE5", tASK_RECURSIVE_HDR.ATTRIBUTE5);
                parameters.Add("@ATTRIBUTE6", tASK_RECURSIVE_HDR.ATTRIBUTE6);
                parameters.Add("@ATTRIBUTE7", tASK_RECURSIVE_HDR.ATTRIBUTE7);
                return await db.ExecuteScalarAsync<int>("SP_INSERT_TASK_RECURSIVE_DETAILS", parameters, commandType: CommandType.StoredProcedure);
            }
        }
        public async Task UpdateTASKAsync(TASK_RECURSIVE_HDR tASK_RECURSIVE_HDR)
        {
            using (IDbConnection db = _dapperDbConnection.CreateConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@MKEY", tASK_RECURSIVE_HDR.MKEY);
                parameters.Add("@TASK_NAME", tASK_RECURSIVE_HDR.TASK_NAME);
                parameters.Add("@TASK_DESCRIPTION", tASK_RECURSIVE_HDR.TASK_DESCRIPTION);
                parameters.Add("@TERM", tASK_RECURSIVE_HDR.TERM);
                parameters.Add("@STARTS", tASK_RECURSIVE_HDR.STARTS);
                parameters.Add("@ENDS", tASK_RECURSIVE_HDR.ENDS);
                parameters.Add("@CREATED_BY", tASK_RECURSIVE_HDR.CREATED_BY);
                parameters.Add("@LAST_UPDATED_BY", tASK_RECURSIVE_HDR.LAST_UPDATED_BY);
                parameters.Add("@ATTRIBUTE1", tASK_RECURSIVE_HDR.ATTRIBUTE1);
                parameters.Add("@ATTRIBUTE2", tASK_RECURSIVE_HDR.ATTRIBUTE2);
                parameters.Add("@ATTRIBUTE3", tASK_RECURSIVE_HDR.ATTRIBUTE3);
                parameters.Add("@ATTRIBUTE4", tASK_RECURSIVE_HDR.ATTRIBUTE4);
                parameters.Add("@ATTRIBUTE5", tASK_RECURSIVE_HDR.ATTRIBUTE5);
                parameters.Add("@ATTRIBUTE6", tASK_RECURSIVE_HDR.ATTRIBUTE6);
                parameters.Add("@ATTRIBUTE7", tASK_RECURSIVE_HDR.ATTRIBUTE7);
                await db.ExecuteAsync("SP_UPDATE_TASK_RECURSIVE_DETAILS", parameters, commandType: CommandType.StoredProcedure);
            }
        }
        public async Task DeleteTASKAsync(int id, int Last_update_By)
        {
            using (IDbConnection db = _dapperDbConnection.CreateConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@MKEY", id);
                parameters.Add("@LAST_UPDATED_BY", Last_update_By);
                await db.ExecuteAsync("SP_DELETE_TASK_RECURSIVE_DETAILS", parameters, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
