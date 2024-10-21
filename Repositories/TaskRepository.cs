using TaskManagement.API.Model;
using TaskManagement.API.Interfaces;
using Dapper;
using System.Data;
using TaskManagement.API.DapperDbConnections;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Microsoft.OpenApi.Extensions;

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
                //db.QueryAsync<TASK_RECURSIVE_HDR>("SP_SCHEDULE_RECURSIVE_TASK", commandType: CommandType.StoredProcedure);
                //var TABLEVALUE = await db.QueryAsync<TASK_RECURSIVE_HDR>("SELECT HDR.MKEY,HDR.TASK_NAME,HDR.TASK_DESCRIPTION,HDR.TERM,CAREGORY,PROJECT_ID,SUB_PROJECT_ID,\r\n\t\t\t\t\t\tASSIGNED_TO,TAGS,\t\t\t\t\t\tNO_DAYS,\t\t\t\t\t   CONVERT(DATETIME2,HDR.[START_DATE]) AS 'START_DATE',\r\n                       HDR.ENDS,\t\t\t\t\t   CONVERT(DATETIME2,HDR.END_DATE) AS 'END_DATE',                       HDR.STATUS,                       HDR.CREATED_BY,\r\n                       HDR.CREATION_DATE,     HDR.LAST_UPDATED_BY,                       HDR.LAST_UPDATE_DATE,                       TRL.ATTRIBUTE1,\r\n                       TRL.ATTRIBUTE2,     TRL.ATTRIBUTE3,  TRL.ATTRIBUTE4,                       TRL.ATTRIBUTE5,                       TRL.ATTRIBUTE6,\r\n                       TRL.ATTRIBUTE7,\tTRL.ATTRIBUTE8,  TRL.ATTRIBUTE9,                       TRL.ATTRIBUTE10,                       TRL.ATTRIBUTE11,\r\n\t\t\t\t\t   TRL.ATTRIBUTE12,\tTRL.ATTRIBUTE13,  TRL.SR_NO,                       TRL.TERM_TYPE,                       TRL.CREATED_BY,\r\n                       TRL.CREATION_DATE, TRL.LAST_UPDATED_BY, TRL.LAST_UPDATE_DATE,TRM.MKEY AS FILE_MKEY,\r\n\t\t\t\t\t   TRM.SR_NO AS FILE_SR_NO,TRM.FILE_NAME,TRM.FILE_PATH FROM   [DBO].[TASK_RECURSIVE_HDR] HDR\r\n                       INNER JOIN [DBO].[TASK_RECURSIVE_TRL] TRL ON HDR.MKEY = TRL.MKEY\tLEFT JOIN TASK_RECURSIVE_MEDIA_TRL TRM\r\n\t\t\t\t\t   ON TRM.TASK_MKEY = HDR.MKEY WHERE  1 = 1 AND HDR.DELETE_FLAG = 'N' AND TRL.DELETE_FLAG = 'N' ORDER BY  HDR.MKEY;");
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
                parameters.Add("@START_DATE", tASK_RECURSIVE_HDR.START_DATE);
                parameters.Add("@ENDS", tASK_RECURSIVE_HDR.ENDS);
                parameters.Add("@END_DATE", tASK_RECURSIVE_HDR.END_DATE);
                parameters.Add("@CREATED_BY", tASK_RECURSIVE_HDR.CREATED_BY);
                parameters.Add("@LAST_UPDATED_BY", tASK_RECURSIVE_HDR.LAST_UPDATED_BY);
                parameters.Add("@CAREGORY", tASK_RECURSIVE_HDR.CAREGORY);
                parameters.Add("@PROJECT_ID", tASK_RECURSIVE_HDR.PROJECT_ID);
                parameters.Add("@SUB_PROJECT_ID", tASK_RECURSIVE_HDR.SUB_PROJECT_ID); 
                parameters.Add("@NO_DAYS", tASK_RECURSIVE_HDR.NO_DAYS);
                parameters.Add("@ASSIGNED_TO", tASK_RECURSIVE_HDR.ASSIGNED_TO);
                parameters.Add("@TAGS", tASK_RECURSIVE_HDR.TAGS);
                parameters.Add("@FILE_NAME", tASK_RECURSIVE_HDR.files.FileName);
                parameters.Add("@FILE_PATH", "Attachment");
                parameters.Add("@ATTRIBUTE1", tASK_RECURSIVE_HDR.ATTRIBUTE1);
                parameters.Add("@ATTRIBUTE2", tASK_RECURSIVE_HDR.ATTRIBUTE2);
                parameters.Add("@ATTRIBUTE3", tASK_RECURSIVE_HDR.ATTRIBUTE3);
                parameters.Add("@ATTRIBUTE4", tASK_RECURSIVE_HDR.ATTRIBUTE4);
                parameters.Add("@ATTRIBUTE5", tASK_RECURSIVE_HDR.ATTRIBUTE5);
                parameters.Add("@ATTRIBUTE6", tASK_RECURSIVE_HDR.ATTRIBUTE6);
                parameters.Add("@ATTRIBUTE7", tASK_RECURSIVE_HDR.ATTRIBUTE7);
                parameters.Add("@ATTRIBUTE8", tASK_RECURSIVE_HDR.ATTRIBUTE8);
                parameters.Add("@ATTRIBUTE9", tASK_RECURSIVE_HDR.ATTRIBUTE9);
                parameters.Add("@ATTRIBUTE10", tASK_RECURSIVE_HDR.ATTRIBUTE10);
                parameters.Add("@ATTRIBUTE11", tASK_RECURSIVE_HDR.ATTRIBUTE11);
                parameters.Add("@ATTRIBUTE12", tASK_RECURSIVE_HDR.ATTRIBUTE12);
                parameters.Add("@ATTRIBUTE13", tASK_RECURSIVE_HDR.ATTRIBUTE13);
                parameters.Add("@ATTRIBUTE14", tASK_RECURSIVE_HDR.ATTRIBUTE14);
                parameters.Add("@ATTRIBUTE15", tASK_RECURSIVE_HDR.ATTRIBUTE15);
                parameters.Add("@ATTRIBUTE16", tASK_RECURSIVE_HDR.ATTRIBUTE16);
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
                parameters.Add("@START_DATE", tASK_RECURSIVE_HDR.START_DATE);
                parameters.Add("@ENDS", tASK_RECURSIVE_HDR.ENDS);
                parameters.Add("@END_DATE", tASK_RECURSIVE_HDR.END_DATE);
                parameters.Add("@CAREGORY", tASK_RECURSIVE_HDR.CAREGORY);
                parameters.Add("@PROJECT_ID", tASK_RECURSIVE_HDR.PROJECT_ID);
                parameters.Add("@SUB_PROJECT_ID", tASK_RECURSIVE_HDR.SUB_PROJECT_ID);
                parameters.Add("@NO_DAYS", tASK_RECURSIVE_HDR.NO_DAYS);
                parameters.Add("@ASSIGNED_TO", tASK_RECURSIVE_HDR.ASSIGNED_TO);
                parameters.Add("@TAGS", tASK_RECURSIVE_HDR.TAGS);
                parameters.Add("@FILE_NAME", tASK_RECURSIVE_HDR.FILE_NAME);
                parameters.Add("@FILE_PATH", tASK_RECURSIVE_HDR.FILE_PATH);
                parameters.Add("@FILE_MKEY", tASK_RECURSIVE_HDR.FILE_MKEY);
                parameters.Add("@FILE_SR_NO", tASK_RECURSIVE_HDR.FILE_SR_NO);
                parameters.Add("@CREATED_BY", tASK_RECURSIVE_HDR.CREATED_BY);
                parameters.Add("@LAST_UPDATED_BY", tASK_RECURSIVE_HDR.LAST_UPDATED_BY);
                parameters.Add("@ATTRIBUTE1", tASK_RECURSIVE_HDR.ATTRIBUTE1);
                parameters.Add("@ATTRIBUTE2", tASK_RECURSIVE_HDR.ATTRIBUTE2);
                parameters.Add("@ATTRIBUTE3", tASK_RECURSIVE_HDR.ATTRIBUTE3);
                parameters.Add("@ATTRIBUTE4", tASK_RECURSIVE_HDR.ATTRIBUTE4);
                parameters.Add("@ATTRIBUTE5", tASK_RECURSIVE_HDR.ATTRIBUTE5);
                parameters.Add("@ATTRIBUTE6", tASK_RECURSIVE_HDR.ATTRIBUTE6);
                parameters.Add("@ATTRIBUTE7", tASK_RECURSIVE_HDR.ATTRIBUTE7);
                parameters.Add("@ATTRIBUTE8", tASK_RECURSIVE_HDR.ATTRIBUTE8);
                parameters.Add("@ATTRIBUTE9", tASK_RECURSIVE_HDR.ATTRIBUTE9);
                parameters.Add("@ATTRIBUTE10", tASK_RECURSIVE_HDR.ATTRIBUTE10);
                parameters.Add("@ATTRIBUTE11", tASK_RECURSIVE_HDR.ATTRIBUTE11);
                parameters.Add("@ATTRIBUTE12", tASK_RECURSIVE_HDR.ATTRIBUTE12);
                parameters.Add("@ATTRIBUTE13", tASK_RECURSIVE_HDR.ATTRIBUTE13);
                parameters.Add("@ATTRIBUTE14", tASK_RECURSIVE_HDR.ATTRIBUTE14);
                parameters.Add("@ATTRIBUTE15", tASK_RECURSIVE_HDR.ATTRIBUTE15);
                parameters.Add("@ATTRIBUTE16", tASK_RECURSIVE_HDR.ATTRIBUTE16);
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
