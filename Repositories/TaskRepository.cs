using Dapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Extensions;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;
using System.Reflection.Metadata;
using System.Text;
using TaskManagement.API.DapperDbConnections;
using TaskManagement.API.Interfaces;
using TaskManagement.API.Model;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace TaskManagement.API.Repositories
{
    public class TASKRepository : ITASKRepository
    {
        private readonly HostEnvironment _env;
        private static TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        public IDapperDbConnection _dapperDbConnection;
        private readonly FileSettings _fileSettings;
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
            try
            {
                using (IDbConnection db = _dapperDbConnection.CreateConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@MKEY", id);
                    return await db.QueryFirstOrDefaultAsync<TASK_RECURSIVE_HDR>("SP_GET_TASK_RECURSIVE", parmeters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        public async Task<TASK_RECURSIVE_HDR> CreateTASKAsync(TASK_RECURSIVE_HDR tASK_RECURSIVE_HDR)
        {
            try
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
                    tASK_RECURSIVE_HDR = await db.QueryFirstOrDefaultAsync<TASK_RECURSIVE_HDR>("SP_INSERT_TASK_RECURSIVE_DETAILS", parameters, commandType: CommandType.StoredProcedure);
                    return tASK_RECURSIVE_HDR;
                }

            }
            catch (SqlException ex)
            {
                return tASK_RECURSIVE_HDR;
            }
            catch (Exception ex)
            {
                return tASK_RECURSIVE_HDR;
            }
        }
        public async Task<bool> UpdateTASKAsync(TASK_RECURSIVE_HDR tASK_RECURSIVE_HDR)
        {
            try
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
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<bool> DeleteTASKAsync(int id, int Last_update_By)
        {
            try
            {
                using (IDbConnection db = _dapperDbConnection.CreateConnection())
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@MKEY", id);
                    parameters.Add("@LAST_UPDATED_BY", Last_update_By);
                    await db.ExecuteAsync("SP_DELETE_TASK_RECURSIVE_DETAILS", parameters, commandType: CommandType.StoredProcedure);
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }

        }
        public async Task<IEnumerable<FileUploadAPIOutPut>> TASKFileUpoadAsync(FileUploadAPI fileUploadAPI)
        {
            IDbTransaction transaction = null;
            bool transactionCompleted = false;

            try
            {
                using (IDbConnection db = _dapperDbConnection.CreateConnection())
                {
                    var sqlConnection = db as SqlConnection;
                    if (sqlConnection == null)
                    {
                        throw new InvalidOperationException("The connection must be a SqlConnection to use OpenAsync.");
                    }

                    if (sqlConnection.State != ConnectionState.Open)
                    {
                        await sqlConnection.OpenAsync();  // Ensure the connection is open
                    }

                    transaction = db.BeginTransaction();
                    transactionCompleted = false;  // Reset transaction state

                    var parameters = new DynamicParameters();
                    parameters.Add("@TASK_MKEY", fileUploadAPI.TASK_MKEY);
                    parameters.Add("@FILE_NAME", fileUploadAPI.FILE_NAME);
                    parameters.Add("@FILE_PATH", fileUploadAPI.FILE_PATH);
                    parameters.Add("@CREATED_BY", fileUploadAPI.CREATED_BY);
                    parameters.Add("@DELETE_FLAG", fileUploadAPI.DELETE_FLAG);
                    parameters.Add("@METHODNAME", "RecursiveFileUpload");
                    parameters.Add("@METHOD", "Add");

                    // Execute stored procedure and get the result
                    var taskRecursive = await db.QueryAsync<FileUploadAPIOutPut>("SP_INSERT_TASK_RECURSIVE_FILE_UPLOAD", parameters, commandType: CommandType.StoredProcedure, transaction: transaction);

                    if (taskRecursive != null)
                    {
                        foreach (var item in taskRecursive)
                        {
                            if (item.Status != "OK")
                            {
                                // Handle rollback in case of an error
                                if (transaction != null && !transactionCompleted)
                                {
                                    try
                                    {
                                        transaction.Rollback();
                                    }
                                    catch (InvalidOperationException rollbackEx)
                                    {
                                        Console.WriteLine($"Rollback failed: {rollbackEx.Message}");
                                    }
                                }

                                // Return error if task status is not Ok
                                return new List<FileUploadAPIOutPut>
                                {
                                    new FileUploadAPIOutPut
                                    {
                                        Status = "Error",
                                        Message = item.Message
                                    }
                                };
                            }
                        }

                        // Commit the transaction if everything was successful
                        transaction.Commit();
                        transactionCompleted = true; // Mark the transaction as complete
                        return taskRecursive;
                    }
                    else
                    {
                        // Return error if no data was returned from the stored procedure
                        return new List<FileUploadAPIOutPut>
                {
                    new FileUploadAPIOutPut
                    {
                        Status = "Error",
                        Message = "Not found"
                    }
                };
                    }
                }
            }
            catch (Exception ex)
            {
                // Rollback transaction in case of an exception
                if (transaction != null && !transactionCompleted)
                {
                    try
                    {
                        transaction.Rollback();
                    }
                    catch (InvalidOperationException rollbackEx)
                    {
                        Console.WriteLine($"Rollback failed: {rollbackEx.Message}");
                    }
                }

                // Return error if an exception occurs during processing
                return new List<FileUploadAPIOutPut>
                {
                    new FileUploadAPIOutPut
                    {
                        Status = "Error",
                        Message = ex.Message
                    }
                };
            }
        }

        //public async Task<IEnumerable<FileUploadAPIOutPut>> TASKFileUpoadAsync(FileUploadAPI fileUploadAPI)
        //{
        //    IDbTransaction transaction = null;
        //    bool transactionCompleted = false;

        //    try
        //    {
        //        using (IDbConnection db = _dapperDbConnection.CreateConnection())
        //        {
        //            var sqlConnection = db as SqlConnection;
        //            if (sqlConnection == null)
        //            {
        //                throw new InvalidOperationException("The connection must be a SqlConnection to use OpenAsync.");
        //            }

        //            if (sqlConnection.State != ConnectionState.Open)
        //            {
        //                await sqlConnection.OpenAsync();  // Ensure the connection is open
        //            }

        //            transaction = db.BeginTransaction();
        //            transactionCompleted = false;  // Reset transaction state

        //            var parameters = new DynamicParameters();
        //            parameters.Add("@TASK_MKEY", fileUploadAPI.TASK_MKEY);
        //            parameters.Add("@FILE_NAME", fileUploadAPI.FILE_NAME);
        //            parameters.Add("@FILE_PATH", fileUploadAPI.FILE_PATH);
        //            parameters.Add("@CREATED_BY", fileUploadAPI.CREATED_BY);
        //            parameters.Add("@DELETE_FLAG", fileUploadAPI.DELETE_FLAG);
        //            parameters.Add("@METHODNAME", "RecursiveFileUpload");
        //            parameters.Add("@METHOD", "Add");

        //            // Execute stored procedure and get the result
        //            //var taskRecursive = await db.QueryAsync<FileUploadAPIOutPut>("SP_INSERT_TASK_RECURSIVE_FILE_UPLOAD", parameters, transaction: transaction);
        //            var taskRecursive = await db.QueryAsync<FileUploadAPIOutPut>("SP_INSERT_TASK_RECURSIVE_FILE_UPLOAD", parameters, commandType: CommandType.StoredProcedure, transaction: transaction);
        //            if (taskRecursive != null)
        //            {
        //                foreach (var item in taskRecursive)
        //                {
        //                    if (item.Status != "OK")
        //                    {
        //                        // Handle rollback in case of an error
        //                        if (transaction != null && !transactionCompleted)
        //                        {
        //                            try
        //                            {
        //                                transaction.Rollback();
        //                            }
        //                            catch (InvalidOperationException rollbackEx)
        //                            {
        //                                Console.WriteLine($"Rollback failed: {rollbackEx.Message}");
        //                            }
        //                        }

        //                        // Return error if task status is not Ok
        //                        var ErroResult = new List<FileUploadAPIOutPut>
        //                                {
        //                                new FileUploadAPIOutPut
        //                                    {
        //                                    Status = "Error",
        //                                    Message = item.Message
        //                                    }
        //                            };
        //                        return ErroResult;
        //                    }
        //                }
        //                return taskRecursive;
        //            }
        //            else
        //            {
        //                // Return error if no data was returned from the stored procedure
        //                var ErroResult = new List<FileUploadAPIOutPut>
        //                {
        //                new FileUploadAPIOutPut
        //                    {
        //                    Status = "Error",
        //                    Message = "Not found"
        //                    }
        //            };
        //                return ErroResult;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        if (transaction != null && !transactionCompleted)
        //        {
        //            try
        //            {
        //                transaction.Rollback();
        //            }
        //            catch (InvalidOperationException rollbackEx)
        //            {
        //                Console.WriteLine($"Rollback failed: {rollbackEx.Message}");
        //            }
        //        }

        //        // Return error if an exception occurs during processing
        //        var ErroResult = new List<FileUploadAPIOutPut>
        //                {
        //                new FileUploadAPIOutPut
        //                    {
        //                    Status = "Error",
        //                    Message = ex.Message
        //                    }
        //            };
        //        return ErroResult;
        //    }
        //}

        /// <summary>
        /// Changes Done by Itemad Hyder 
        /// </summary>
        /// <param name="tASK_RECURSIVE_HDR"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>

        #region
        public async Task<IEnumerable<RECURSIVE_TASK_DETAILS_BY_MKEY_list_NT>> GetTaskDetailsByMkeyNTAsync(TASK_DETAILS_BY_MKEYInput_NT tASK_DETAILS_BY_MKEYInput_NT)
        {
            DateTime dateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
            IDbTransaction transaction = null;
            bool transactionCompleted = false;  // Track the transaction state
            try
            {
                using (IDbConnection db = _dapperDbConnection.CreateConnection())
                {
                    var sqlConnection = db as SqlConnection;
                    if (sqlConnection == null)
                    {
                        throw new InvalidOperationException("The connection must be a SqlConnection to use OpenAsync.");
                    }

                    if (sqlConnection.State != ConnectionState.Open)
                    {
                        await sqlConnection.OpenAsync();  // Ensure the connection is open
                    }

                    transaction = db.BeginTransaction();
                    transactionCompleted = false;  // Reset transaction state
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@MKEY", tASK_DETAILS_BY_MKEYInput_NT.Mkey);
                    parmeters.Add("@Session_User_Id", tASK_DETAILS_BY_MKEYInput_NT.Session_User_Id);
                    parmeters.Add("@Business_Group_Id", tASK_DETAILS_BY_MKEYInput_NT.Business_Group_Id);
                    var TaskDashDetails = await db.QueryAsync<Recursive_TASK_DETAILS_BY_MKEY_NT>("[dbo].[SP_GET_TASK_RECURSIVE]", parmeters, commandType: CommandType.StoredProcedure, transaction: transaction);

                    if (TaskDashDetails.Any())
                    {
                        //// Attachment Part in Recursive_Details_By_MKey 
                        var parmetersAttachment = new DynamicParameters();
                        parmetersAttachment.Add("@HDR_MKEY", tASK_DETAILS_BY_MKEYInput_NT.Mkey);
                        parmetersAttachment.Add("@Session_User_Id", tASK_DETAILS_BY_MKEYInput_NT.Session_User_Id);
                        parmetersAttachment.Add("@Business_Group_Id", tASK_DETAILS_BY_MKEYInput_NT.Business_Group_Id);
                        var TaskAttachmentDetails = await db.QueryAsync<Recursive_TASK_MEDIA_NT>("[dbo].[SP_RECURSIVE_TASK_MEDIA_TRL_NT]", parmetersAttachment, commandType: CommandType.StoredProcedure, transaction: transaction);

                        if (TaskAttachmentDetails.Any())
                        {
                            foreach (var TaskAttachment in TaskDashDetails)
                            {
                                TaskAttachment.tASK_MEDIA_NTs = TaskAttachmentDetails.ToList();
                            }
                        }

                        var parmetersCheckList = new DynamicParameters();
                        parmetersCheckList.Add("@PROPERTY_MKEY", 0);
                        parmetersCheckList.Add("@BUILDING_MKEY", 0);
                        parmetersCheckList.Add("@TASK_MKEY", tASK_DETAILS_BY_MKEYInput_NT.Mkey);
                        parmetersCheckList.Add("@USER_ID", tASK_DETAILS_BY_MKEYInput_NT.Session_User_Id);
                        parmetersCheckList.Add("@Session_User_Id", tASK_DETAILS_BY_MKEYInput_NT.Session_User_Id);
                        parmetersCheckList.Add("@Business_Group_Id", tASK_DETAILS_BY_MKEYInput_NT.Business_Group_Id);
                        parmetersCheckList.Add("@API_NAME", "GetTaskCompliance");
                        parmetersCheckList.Add("@API_METHOD", "Get");
                        var GetCheckList = await db.QueryAsync<TASK_COMPLIANCE_CHECK_END_LIST_OUTPUT_NT>("SP_GET_ Recursive_TASK_CHECKLIST_NT", parmetersCheckList, commandType: CommandType.StoredProcedure, transaction: transaction);

                        if (GetCheckList.Any())
                        {
                            foreach (var TaskMaster in TaskDashDetails)
                            {
                                TaskMaster.tASK_CHECKLIST_TABLE_INPUT_NT = GetCheckList.ToList();
                            }
                        }

                        ////////////////////////////////////////////////////////////////////////////////////////////////////////

                        var parmetersEndList = new DynamicParameters();
                        parmetersEndList.Add("@PROPERTY_MKEY", 0);
                        parmetersEndList.Add("@BUILDING_MKEY", 0);
                        parmetersEndList.Add("@TASK_MKEY", tASK_DETAILS_BY_MKEYInput_NT.Mkey);
                        parmetersEndList.Add("@USER_ID", tASK_DETAILS_BY_MKEYInput_NT.Session_User_Id);
                        parmetersEndList.Add("@API_NAME", "GetTaskCompliance");
                        parmetersEndList.Add("@API_METHOD", "Get");
                        parmetersEndList.Add("@Session_User_Id", tASK_DETAILS_BY_MKEYInput_NT.Session_User_Id);
                        parmetersEndList.Add("@Business_Group_Id", tASK_DETAILS_BY_MKEYInput_NT.Business_Group_Id);
                        var GetTaskEndList = await db.QueryAsync<TASK_ENDLIST_DETAILS_OUTPUT_NT>("SP_GET_RECURSIVETASK_ENDLIST_NT", parmetersEndList, commandType: CommandType.StoredProcedure, transaction: transaction);

                        if (GetTaskEndList.Any())
                        {
                            foreach (var TaskCompliance in GetTaskEndList)
                            {
                                var parmetersMedia = new DynamicParameters();
                                parmetersMedia.Add("@TASK_MKEY", tASK_DETAILS_BY_MKEYInput_NT.Mkey);
                                parmetersMedia.Add("@DOC_CATEGORY_MKEY", TaskCompliance.Doc_Type_Mkey);
                                parmetersMedia.Add("@USER_ID", tASK_DETAILS_BY_MKEYInput_NT.Session_User_Id);
                                parmetersEndList.Add("@Session_User_Id", tASK_DETAILS_BY_MKEYInput_NT.Session_User_Id);
                                parmetersEndList.Add("@Business_Group_Id", tASK_DETAILS_BY_MKEYInput_NT.Business_Group_Id);
                                var TaskEndListMedia = await db.QueryAsync<TASK_OUTPUT_MEDIA_NT>("SP_GET_RECURSIVE_TASK_ENDLIST_MEDIA", parmetersMedia, commandType: CommandType.StoredProcedure, transaction: transaction);

                                if (TaskEndListMedia.Any())
                                {
                                    foreach (var EndListAttch in GetTaskEndList)
                                    {
                                        if (EndListAttch.Doc_Type_Mkey == TaskCompliance.Doc_Type_Mkey)
                                        {
                                            EndListAttch.TASK_OUTPUT_ATTACHMENT = TaskEndListMedia.ToList();
                                        }
                                    }
                                }
                            }
                            foreach (var TaskMaster in TaskDashDetails)
                            {
                                TaskMaster.tASK_ENDLIST_TABLE_INPUT_NTs = GetTaskEndList.ToList();
                            }
                        }
                        //////////////////////////////////////////////////////////////////////////////////////////////////
                        //var parmetersSanctioning = new DynamicParameters();
                        //parmetersSanctioning.Add("@PROPERTY_MKEY", 0);
                        //parmetersSanctioning.Add("@BUILDING_MKEY", 0);
                        //parmetersSanctioning.Add("@MKEY", tASK_DETAILS_BY_MKEYInput_NT.Mkey);
                        //parmetersSanctioning.Add("@USER_ID", tASK_DETAILS_BY_MKEYInput_NT.Session_User_Id);
                        //parmetersSanctioning.Add("@API_NAME", "GetTaskCompliance");
                        //parmetersSanctioning.Add("@API_METHOD", "Get");
                        //var GetTaskSanDepart = await db.QueryAsync<TaskSanctioningDepartmentOutput_NT>("SP_GET_TASK_SANCTIONING_DEPARTMENT", parmetersSanctioning, commandType: CommandType.StoredProcedure, transaction: transaction);
                        //if (GetTaskSanDepart.Any())
                        //{
                        //    foreach (var TaskMaster in TaskDashDetails)
                        //    {
                        //        TaskMaster.tASK_SANCTIONING_INPUT_NT = GetTaskSanDepart.ToList();
                        //    }
                        //}

                        var SuccessResult = new List<RECURSIVE_TASK_DETAILS_BY_MKEY_list_NT>
                            {
                                new RECURSIVE_TASK_DETAILS_BY_MKEY_list_NT
                                {
                                    Status = "Ok",
                                    Message = "Get Data",
                                    Data = TaskDashDetails.ToList()
                                }
                            };
                        return SuccessResult;
                    }
                    else
                    {
                        var errorResult = new List<RECURSIVE_TASK_DETAILS_BY_MKEY_list_NT>
                            {
                                new RECURSIVE_TASK_DETAILS_BY_MKEY_list_NT
                                {
                                    Status = "Ok",
                                    Message = "Task not found"
                                }
                            };
                        return errorResult;
                    }
                }
            }
            catch (Exception ex)
            {
                var errorResult = new List<RECURSIVE_TASK_DETAILS_BY_MKEY_list_NT>
                    {
                        new RECURSIVE_TASK_DETAILS_BY_MKEY_list_NT
                        {
                            Status = "Error",
                            Message = ex.Message
                        }
                    };
                return errorResult;
            }
        }

        public async Task<Add_Recursive_TaskOutPut_List_NT> CreateRecursiveTASKAsync(TASK_RECURSIVE_HDR tASK_RECURSIVE_HDR)
        {
            DateTime dateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
            var taskRecursiveHDR = new TASK_RECURSIVE_HDR();
            taskRecursiveHDR = tASK_RECURSIVE_HDR;
            IDbTransaction transaction = null;
            string TaskNo = string.Empty;
            bool transactionCompleted = false;
            try
            {
                using (IDbConnection db = _dapperDbConnection.CreateConnection())
                {

                    var sqlConnection = db as SqlConnection;
                    if (sqlConnection == null)
                    {
                        throw new InvalidOperationException("The connection must be a SqlConnection to use OpenAsync.");
                    }

                    if (sqlConnection.State != ConnectionState.Open)
                    {
                        await sqlConnection.OpenAsync();  // Ensure the connection is open
                    }

                    transaction = db.BeginTransaction();
                    transactionCompleted = false;  // Reset transaction state

                    if (tASK_RECURSIVE_HDR.MKEY == 0)
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
                        parameters.Add("@ATTRIBUTE16", tASK_RECURSIVE_HDR.ATTRIBUTE16);
                        parameters.Add("@Priority", tASK_RECURSIVE_HDR.Priority);
                        tASK_RECURSIVE_HDR = await db.QueryFirstOrDefaultAsync<TASK_RECURSIVE_HDR>("SP_INSERT_TASK_RECURSIVE_DETAILS", parameters, transaction: transaction, commandType: CommandType.StoredProcedure);
                    }

                    if (tASK_RECURSIVE_HDR.MKEY > 0)
                    {
                        if (taskRecursiveHDR.tASK_CHECKLIST_TABLE_INPUT_NT.Any())
                        {
                            foreach (var TCheckList in taskRecursiveHDR.tASK_CHECKLIST_TABLE_INPUT_NT)
                            {
                                var parmetersCheckList = new DynamicParameters();
                                parmetersCheckList.Add("@TASK_MKEY", tASK_RECURSIVE_HDR.MKEY.ToString());
                                parmetersCheckList.Add("@SR_NO", TCheckList.SR_NO);
                                parmetersCheckList.Add("@Doc_Type_Mkey", TCheckList.DOC_MKEY);
                                parmetersCheckList.Add("@Doc_Cat_mkey", TCheckList.DOCUMENT_CATEGORY);
                                parmetersCheckList.Add("@CREATED_BY", TCheckList.CREATED_BY);
                                parmetersCheckList.Add("@DELETE_FLAG", TCheckList.DELETE_FLAG);
                                parmetersCheckList.Add("@COMMENT", null);
                                parmetersCheckList.Add("@METHOD_NAME", "Task-CheckList-Doc-Insert-Update");
                                parmetersCheckList.Add("@METHOD", "Insert");
                                parmetersCheckList.Add("@OUT_STATUS", null);
                                parmetersCheckList.Add("@OUT_MESSAGE", null);

                                var GetTaskCheckList = await db.QueryAsync<TASK_CHECKLIST_TABLE_OUTPUT>("[dbo].[SP_INSERT_TABLE_RecursiveTASK_CHECKLIST]", parmetersCheckList, commandType: CommandType.StoredProcedure, transaction: transaction);

                                if (GetTaskCheckList.Any())
                                {
                                    foreach (var Response in GetTaskCheckList)
                                    {
                                        if (Response.OUT_STATUS != "OK")
                                        {
                                            if (transaction != null && !transactionCompleted)
                                            {
                                                try
                                                {
                                                    // Rollback only if the transaction is not yet completed
                                                    transaction.Rollback();
                                                }
                                                catch (InvalidOperationException rollbackEx)
                                                {
                                                    Console.WriteLine($"Rollback failed: {rollbackEx.Message}");
                                                }
                                            }

                                            var errorResult = new Add_Recursive_TaskOutPut_List_NT
                                            {

                                                Status = "Error",
                                                Message = Response.OUT_MESSAGE,
                                                Data = null

                                            };
                                            return errorResult;
                                        }
                                        //else
                                        //{
                                        //    tASK_RECURSIVE_HDR.tASK_CHECKLIST_TABLE_INPUT_NT.Add(TCheckList);
                                        //}

                                    }
                                }

                            }
                        }
                        if (taskRecursiveHDR.tASK_ENDLIST_TABLE_INPUT_NTs.Any())
                        {
                            // To Insert EndList
                            foreach (var TEndList in taskRecursiveHDR.tASK_ENDLIST_TABLE_INPUT_NTs)
                            {
                                foreach (var docMkey in TEndList.OUTPUT_DOC_LST)
                                {
                                    foreach (var DocCategory in docMkey.Value.ToString().Split(','))
                                    {
                                        var parametersEndList = new DynamicParameters();
                                        parametersEndList.Add("@MKEY", tASK_RECURSIVE_HDR.MKEY.ToString());
                                        parametersEndList.Add("@SR_NO", TEndList.SR_NO);
                                        parametersEndList.Add("@DOC_TYPE_MKEY", Convert.ToInt32(DocCategory));
                                        parametersEndList.Add("@DOC_CAT_MKEY", docMkey.Key.ToString());
                                        // parametersEndList.Add("@DOCUMENT_CATEGORY_MKEY", Convert.ToInt32(DocCategory));
                                        // parametersEndList.Add("@DOCUMENT_NAME", docMkey.Key.ToString());
                                        parametersEndList.Add("@CREATED_BY", TEndList.CREATED_BY);
                                        parametersEndList.Add("@DELETE_FLAG", TEndList.DELETE_FLAG);
                                        parametersEndList.Add("@API_NAME", "Task-Output-Doc-Insert-Update");
                                        parametersEndList.Add("@API_METHOD", "Insert/Update");
                                        parametersEndList.Add("@OUT_STATUS", null);
                                        parametersEndList.Add("@OUT_MESSAGE", null);

                                        var GetTaskEndList = await db.QueryAsync<TASK_ENDLIST_DETAILS_OUTPUT>("[dbo].[SP_INSERT_UPDATE_Recursive_TASK_ENDLIST_TABLE_NT]", parametersEndList, commandType: CommandType.StoredProcedure, transaction: transaction);
                                        //var taskEndlist = new List<TASK_CHECKLIST_TABLE_INPUT_NT>()
                                        //{
                                        //     new TASK_CHECKLIST_TABLE_INPUT_NT
                                        //     {
                                        //         TASK_MKEY= GetTaskEndList.Where(x=>x.)
                                        //     }
                                        //};

                                        if (GetTaskEndList.Any())
                                        {
                                            foreach (var Response in GetTaskEndList)
                                            {
                                                if (Response.OUT_STATUS.ToLower() != "OK".ToLower())
                                                {
                                                    if (transaction != null && !transactionCompleted)
                                                    {
                                                        try
                                                        {
                                                            // Rollback only if the transaction is not yet completed
                                                            transaction.Rollback();
                                                        }
                                                        catch (InvalidOperationException rollbackEx)
                                                        {
                                                            Console.WriteLine($"Rollback failed: {rollbackEx.Message}");
                                                        }
                                                    }

                                                    var errorResult = new Add_Recursive_TaskOutPut_List_NT
                                                    {
                                                        Status = "Error",
                                                        Message = Response.OUT_MESSAGE,
                                                        Data = null

                                                    };
                                                    return errorResult;
                                                }
                                                //else
                                                //{
                                                //    tASK_RECURSIVE_HDR.tASK_ENDLIST_TABLE_INPUT_NTs.    .Add(Response);
                                                //}
                                            }
                                        }


                                    }
                                }
                            }


                        }

                        var sqlTransaction = (SqlTransaction)transaction;
                        await sqlTransaction.CommitAsync();
                        transactionCompleted = true;

                        var AssignEmail = await db.QueryAsync<string>("select EMAIL_ID_OFFICIAL from EMPLOYEE_MST where MKEY = " + tASK_RECURSIVE_HDR.CREATED_BY + "  ", commandType: CommandType.Text);
                        string AssignByEmail = AssignEmail.FirstOrDefault();
                        var CreatedEmail = await db.QueryAsync<string>("select EMAIL_ID_OFFICIAL from EMPLOYEE_MST where MKEY = " + tASK_RECURSIVE_HDR.ASSIGNED_TO + "  ", commandType: CommandType.Text);
                        string CreatedByEmail = CreatedEmail.FirstOrDefault();
                        var AssignedBy = await db.QueryAsync<string>("Select EMP_FULL_NAME from EMPLOYEE_MST where MKEY = " + tASK_RECURSIVE_HDR.ASSIGNED_TO + "  ", commandType: CommandType.Text);
                        string AssignedByName = AssignedBy.FirstOrDefault();


                        var parmetersMail = new DynamicParameters();
                        parmetersMail.Add("@MAIL_TYPE", "Auto");
                        var MailDetails = await db.QueryAsync<MailDetailsNT>("SP_GET_MAIL_TYPE", parmetersMail, commandType: CommandType.StoredProcedure);
                        string completionDate = DateTime.Now.AddDays(Convert.ToDouble(tASK_RECURSIVE_HDR.NO_DAYS)).ToString("dd-MM-yyyy");
                        StringBuilder htmlRows = new StringBuilder();
                        htmlRows.Append("<tr>");
                        htmlRows.AppendFormat("<td style='border:1px solid #9ec3ff; text-align:center; padding:5px;'>{0}</td>", TaskNo);
                        htmlRows.AppendFormat("<td style='border:1px solid #9ec3ff; text-align:center; padding:5px;'>{0}</td>", tASK_RECURSIVE_HDR.TASK_NAME);
                        htmlRows.AppendFormat("<td style='border:1px solid #9ec3ff; text-align:center; padding:5px;'>{0}</td>", tASK_RECURSIVE_HDR.TASK_DESCRIPTION);
                        htmlRows.AppendFormat("<td style='border:1px solid #9ec3ff; text-align:center; padding:5px;'>{0:dd-MMM-yyyy}</td>", completionDate);  // tASK_RECURSIVE_HDR.COMPLETION_DATE
                        htmlRows.AppendFormat("<td style='border:1px solid #9ec3ff; text-align:center; padding:5px;'>{0}</td>", AssignedByName);
                        htmlRows.Append("</tr>");

                        string taskKickoffRowsHtml = htmlRows.ToString();

                        string MailBody = "<!DOCTYPE html>\r\n<html>\r\n<head>\r\n    " +
                            "<meta charset=\"UTF-8\">\r\n    " +
                            "<title>Task Assignment</title>\r\n" +
                            "</head>\r\n<body style=\"font-family: Arial, sans-serif; font-size: 14px; color: #333;\">\r\n    " +
                            "<p>Dear <strong>" + AssignedByName + "</strong>,</p>\r\n\r\n    <p>The following task has been assigned to you:</p>\r\n\r\n    " +
                            "<form style='margin:1% auto;'>\r\n                       " +
                            "<fieldset style='border:2px solid #9ec3ff;margin:auto 1%;padding:5px;color:#003487;'>\r\n                         " +
                            "<legend style='padding:10px;font-size: 18px!important;'><b>Your Task list</b></legend>\r\n                         " +
                            "<table style='width:100%;padding:0px;margin:1% auto;border-radius:14px;font-size:14px;'>\r\n                           " +
                            "<tr>\r\n                             " +
                            "<td style='color:black; border-color:#9ec3ff;text-align:center; border-style:solid;border-radius:5px; padding: 5px; font-size: 14px;background-color:#9ec3ff;width:8%;'><b>Task No.</b></td>  \r\n                             " +
                            "<td style='color:black; border-color:#9ec3ff;text-align:center; border-style:solid;border-radius:5px; padding: 5px; font-size: 14px;background-color:#9ec3ff;'><b>Task Name</b></td>\r\n                            " +
                            " <td style='color:black; border-color:#9ec3ff;text-align:center; border-style:solid;border-radius:5px; padding: 5px; font-size: 14px;background-color:#9ec3ff;width:40%;'><b>Description</b></td>  \r\n                             " +
                            "<td style='color:black; border-color:#9ec3ff;text-align:center; border-style:solid;border-radius:5px; padding: 5px; font-size: 14px;background-color:#9ec3ff;width:10%;'><b>Due Date</b></td>  \r\n                             " +
                            "<td style='color:black; border-color:#9ec3ff;text-align:center; border-style:solid;border-radius:5px; padding: 5px; font-size: 14px;background-color:#9ec3ff;width:10%;'><b>Assigned By</b></td> \r\n                             " +
                            "</tr>\r\n                          " + taskKickoffRowsHtml +
                            "</table>\r\n                      " +
                            " </fieldset>\r\n                    " +
                            " </form>\r\n\r\n    " +
                            "<p style=\"margin-top: 20px;\">\r\n        If you have any questions or need assistance, feel free to contact us at \r\n        " +
                            "<a href=\"mailto:qui.support@powersoft.in\">qui.support@powersoft.in</a>.\r\n    </p>\r\n\r\n    <p>Best regards,<br>\r\n    " +
                            "<strong>QUI Team</strong></p>\r\n</body>\r\n</html>\r\n";

                        foreach (var Mail in MailDetails)
                        {
                            //SendEmail(AssignByEmail.ToString(), null, null, "QUI-New Task Assigned : Task No-(" + TaskNo + " )", MailBody, Mail.MAIL_TYPE, "QUI", null, Mail);  // CreatedByEmail.ToString()
                        }

                        var successsResult = new Add_Recursive_TaskOutPut_List_NT
                        {
                            Status = "Ok",
                            Message = "Inserted Successfully",
                            Data = tASK_RECURSIVE_HDR
                        };
                        return successsResult;
                    }
                    else
                    {
                        if (transaction != null && !transactionCompleted)
                        {
                            try
                            {
                                // Rollback only if the transaction is not yet completed
                                transaction.Rollback();
                            }
                            catch (InvalidOperationException rollbackEx)
                            {
                                Console.WriteLine($"Rollback failed: {rollbackEx.Message}");
                            }
                        }
                        var errorResult = new Add_Recursive_TaskOutPut_List_NT
                        {
                            Status = "Error",
                            Message = "Recursive Insert Failled"
                        };
                        return errorResult;
                    }
                }

            }
            catch (SqlException ex)
            {
                if (transaction != null && !transactionCompleted)
                {
                    try
                    {
                        // Rollback only if the transaction is not yet completed
                        transaction.Rollback();
                    }
                    catch (InvalidOperationException rollbackEx)
                    {
                        Console.WriteLine($"Rollback failed: {rollbackEx.Message}");
                    }
                }
                var errorResult = new Add_Recursive_TaskOutPut_List_NT
                {

                    Status = "Error",
                    Message = ex.Message

                };
                return errorResult;
            }
            catch (Exception ex)
            {
                if (transaction != null && !transactionCompleted)
                {
                    try
                    {
                        // Rollback only if the transaction is not yet completed
                        transaction.Rollback();
                    }
                    catch (InvalidOperationException rollbackEx)
                    {
                        Console.WriteLine($"Rollback failed: {rollbackEx.Message}");
                    }
                }
                var errorResult = new Add_Recursive_TaskOutPut_List_NT
                {

                    Status = "Error",
                    Message = ex.Message

                };
                return errorResult;
            }
        }
        public async Task<Add_TaskOutPut_List_NT> UpdateRecuriveTASKAsync(TASK_RECURSIVE_HDR tASK_RECURSIVE_HDR)
        {
            DateTime dateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
            IDbTransaction transaction = null;
            string TaskNo = string.Empty;
            bool transactionCompleted = false;
            string status = string.Empty;
            var taskRecursiveHDR = new TASK_RECURSIVE_HDR();
            taskRecursiveHDR = tASK_RECURSIVE_HDR;
            //var errorResult = null;
            try
            {

                using (IDbConnection db = _dapperDbConnection.CreateConnection())
                {
                    var sqlConnection = db as SqlConnection;
                    if (sqlConnection == null)
                    {
                        throw new InvalidOperationException("The connection must be a SqlConnection to use OpenAsync.");
                    }
                    if (sqlConnection.State != ConnectionState.Open)
                    {
                        await sqlConnection.OpenAsync();  // Ensure the connection is open
                    }
                    transaction = db.BeginTransaction();
                    transactionCompleted = false;  // Reset transaction state

                    if (tASK_RECURSIVE_HDR.MKEY > 0)
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
                        parameters.Add("@Priority", tASK_RECURSIVE_HDR.Priority);
                        parameters.Add("@Status", dbType: DbType.String, direction: ParameterDirection.Output, size: 100);
                        await db.ExecuteAsync("SP_UPDATE_RECURSIVETask_DETAILS", parameters, transaction: transaction, commandType: CommandType.StoredProcedure);
                        status = parameters.Get<string>("@Status");
                        if (!status.Contains("SUCCESS"))
                        {
                            if (transaction != null && !transactionCompleted)
                            {
                                try
                                {
                                    // Rollback only if the transaction is not yet completed
                                    transaction.Rollback();
                                }
                                catch (InvalidOperationException rollbackEx)
                                {
                                    Console.WriteLine($"Rollback failed: {rollbackEx.Message}");
                                }
                            }
                            // If neither "A" nor "M", you should return an appropriate result.
                            var errorResult = new List<Add_TaskOutPut_List_NT>
                                    {
                                        new Add_TaskOutPut_List_NT
                                        {
                                            Status = "Error",
                                            Message = status
                                        }
                                    };
                            //return errorResult;
                        }
                        if (taskRecursiveHDR.tASK_CHECKLIST_TABLE_INPUT_NT != null)
                        {
                            foreach (var TCheckList in taskRecursiveHDR.tASK_CHECKLIST_TABLE_INPUT_NT)
                            {
                                var parmetersCheckList = new DynamicParameters();
                                parmetersCheckList.Add("@TASK_MKEY", taskRecursiveHDR.MKEY);
                                parmetersCheckList.Add("@SR_NO", TCheckList.SR_NO);
                                parmetersCheckList.Add("@Doc_Type_Mkey", TCheckList.DOC_MKEY);
                                parmetersCheckList.Add("@Doc_Cat_mkey", TCheckList.DOCUMENT_CATEGORY);
                                parmetersCheckList.Add("@CREATED_BY", TCheckList.CREATED_BY);
                                parmetersCheckList.Add("@DELETE_FLAG", TCheckList.DELETE_FLAG);
                                parmetersCheckList.Add("@METHOD_NAME", "Task-CheckList-Doc-Insert-Update");
                                parmetersCheckList.Add("@METHOD", "Insert/Update");
                                parmetersCheckList.Add("@OUT_STATUS", null);
                                parmetersCheckList.Add("@OUT_MESSAGE", null);
                                var GetTaskCheckList = await db.QueryAsync<TASK_CHECKLIST_TABLE_OUTPUT>("SP_INSERT_UPDATE_Recursive_TASK_CHECKLIST_NT", parmetersCheckList, commandType: CommandType.StoredProcedure, transaction: transaction);
                                if (GetTaskCheckList.Any())
                                {
                                    foreach (var Response in GetTaskCheckList)
                                    {
                                        if (Response.OUT_STATUS != "OK")
                                        {
                                            if (transaction != null && !transactionCompleted)
                                            {
                                                try
                                                {
                                                    // Rollback only if the transaction is not yet completed
                                                    transaction.Rollback();
                                                }
                                                catch (InvalidOperationException rollbackEx)
                                                {
                                                    Console.WriteLine($"Rollback failed: {rollbackEx.Message}");
                                                }
                                            }
                                            var errorResult = new Add_TaskOutPut_List_NT
                                            {
                                                Status = "Error",
                                                Message = Response.OUT_MESSAGE,
                                                Data = null

                                            };
                                            return errorResult;
                                        }
                                    }
                                }
                            }
                        }

                        if (taskRecursiveHDR.tASK_ENDLIST_TABLE_INPUT_NTs.Any())
                        {
                            // To Insert EndList
                            foreach (var TEndList in taskRecursiveHDR.tASK_ENDLIST_TABLE_INPUT_NTs)
                            {
                                foreach (var docMkey in TEndList.OUTPUT_DOC_LST)
                                {
                                    foreach (var DocCategory in docMkey.Value.ToString().Split(','))
                                    {
                                        var parametersEndList = new DynamicParameters();
                                        parametersEndList.Add("@MKEY", taskRecursiveHDR.MKEY.ToString());
                                        parametersEndList.Add("@SR_NO", TEndList.SR_NO);
                                        parametersEndList.Add("@SR_NO", TEndList.SR_NO);
                                        parametersEndList.Add("@DOC_TYPE_MKEY", Convert.ToInt32(DocCategory));
                                        parametersEndList.Add("@DOC_CAT_MKEY", docMkey.Key.ToString());
                                        //parametersEndList.Add("@DOCUMENT_CATEGORY_MKEY", Convert.ToInt32(DocCategory));
                                        //parametersEndList.Add("@DOCUMENT_NAME", docMkey.Key.ToString());
                                        parametersEndList.Add("@CREATED_BY", TEndList.CREATED_BY);
                                        parametersEndList.Add("@DELETE_FLAG", TEndList.DELETE_FLAG);
                                        parametersEndList.Add("@API_NAME", "Task-Output-Doc-Insert-Update");
                                        parametersEndList.Add("@API_METHOD", "Insert/Update");
                                        parametersEndList.Add("@OUT_STATUS", null);
                                        parametersEndList.Add("@OUT_MESSAGE", null);
                                        var GetTaskEndList = await db.QueryAsync<TASK_ENDLIST_DETAILS_OUTPUT>("[dbo].[SP_INSERT_UPDATE_Recursive_TASK_ENDLIST_TABLE_NT]", parametersEndList, commandType: CommandType.StoredProcedure, transaction: transaction);
                                        //var taskEndlist = new List<TASK_CHECKLIST_TABLE_INPUT_NT>()
                                        //{
                                        //     new TASK_CHECKLIST_TABLE_INPUT_NT
                                        //     {
                                        //         TASK_MKEY= GetTaskEndList.Where(x=>x.)
                                        //     }
                                        //};

                                        if (GetTaskEndList.Any())
                                        {
                                            foreach (var Response in GetTaskEndList)
                                            {
                                                if (Response.OUT_STATUS.ToLower() != "OK".ToLower())
                                                {
                                                    if (transaction != null && !transactionCompleted)
                                                    {
                                                        try
                                                        {
                                                            // Rollback only if the transaction is not yet completed
                                                            transaction.Rollback();
                                                        }
                                                        catch (InvalidOperationException rollbackEx)
                                                        {
                                                            Console.WriteLine($"Rollback failed: {rollbackEx.Message}");
                                                        }
                                                    }

                                                    var errorResult = new Add_TaskOutPut_List_NT
                                                    {
                                                        Status = "Error",
                                                        Message = Response.OUT_MESSAGE,
                                                        Data = null

                                                    };
                                                    return errorResult;
                                                }
                                                //else
                                                //{
                                                //    tASK_RECURSIVE_HDR.tASK_ENDLIST_TABLE_INPUT_NTs.    .Add(Response);
                                                //}
                                            }
                                        }


                                    }
                                }
                            }


                        }

                        var sqlTransaction = (SqlTransaction)transaction;
                        await sqlTransaction.CommitAsync();
                        transactionCompleted = true;

                        var successsResult = new Add_TaskOutPut_List_NT
                        {
                            Status = "Ok",
                            Message = "Updated Successfully",
                            Data = null

                        };
                        return successsResult;

                    }
                    else
                    {
                        var errorResult = new Add_TaskOutPut_List_NT
                        {
                            Status = "Error",
                            Message = status
                        };
                        return errorResult;
                    }
                }
            }
            catch (SqlException ex)
            {
                if (transaction != null && !transactionCompleted)
                {
                    try
                    {
                        // Rollback only if the transaction is not yet completed
                        transaction.Rollback();
                    }
                    catch (InvalidOperationException rollbackEx)
                    {
                        Console.WriteLine($"Rollback failed: {rollbackEx.Message}");
                    }
                }
                var errorResult = new Add_TaskOutPut_List_NT
                {

                    Status = "Error",
                    Message = ex.Message

                };
                return errorResult;
            }
            catch (Exception ex)
            {
                if (transaction != null && !transactionCompleted)
                {
                    try
                    {
                        // Rollback only if the transaction is not yet completed
                        transaction.Rollback();
                    }
                    catch (InvalidOperationException rollbackEx)
                    {
                        Console.WriteLine($"Rollback failed: {rollbackEx.Message}");
                    }
                }
                var errorResult = new Add_TaskOutPut_List_NT
                {

                    Status = "Error",
                    Message = ex.Message

                };
                return errorResult;
            }
        }

        public async Task<int> TASKFileUpoadAsync(int srNo, int taskMkey, int taskParentId, string fileName, string filePath, int createdBy, char deleteFlag, int taskMainNodeId)
        {
            DateTime dateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
            IDbTransaction transaction = null;
            bool transactionCompleted = false;
            int SR_NO = 0;
            try
            {
                using (IDbConnection db = _dapperDbConnection.CreateConnection())
                {
                    var sqlConnection = db as SqlConnection;
                    if (sqlConnection == null)
                    {
                        throw new InvalidOperationException("The connection must be a SqlConnection to use OpenAsync.");
                    }

                    if (sqlConnection.State != ConnectionState.Open)
                    {
                        await sqlConnection.OpenAsync();  // Ensure the connection is open
                    }

                    transaction = db.BeginTransaction();
                    transactionCompleted = false;  // Reset transaction state

                    var parameters = new DynamicParameters();
                    parameters.Add("@SR_NO", srNo);
                    parameters.Add("@TASK_MKEY", taskMkey);
                    //parameters.Add("@TASK_PARENT_ID", taskParentId);
                    parameters.Add("@FILE_NAME", fileName);
                    parameters.Add("@FILE_PATH", filePath);
                    parameters.Add("@CREATED_BY", createdBy);
                    parameters.Add("@ATTRIBUTE1", null);
                    parameters.Add("@ATTRIBUTE2", null);
                    parameters.Add("@ATTRIBUTE3", null);
                    parameters.Add("@ATTRIBUTE4", null);
                    parameters.Add("@ATTRIBUTE5", null);
                    parameters.Add("@LAST_UPDATED_BY", createdBy);
                    parameters.Add("@LAST_UPDATE_DATE", null);
                    parameters.Add("@DELETE_FLAG", deleteFlag);
                    //parameters.Add("@TASK_MAIN_NODE_ID", taskMainNodeId);

                    var TaskFile = await db.ExecuteAsync("Sp_RECURSIVE_Insert_Attcahment", parameters, commandType: CommandType.StoredProcedure, transaction: transaction);

                    if (TaskFile == null)
                    {
                        // Handle other unexpected exceptions
                        if (transaction != null && !transactionCompleted)
                        {
                            try
                            {
                                // Rollback only if the transaction is not yet completed
                                transaction.Rollback();
                            }
                            catch (InvalidOperationException rollbackEx)
                            {

                                Console.WriteLine($"Rollback failed: {rollbackEx.Message}");
                                //TranError.Message = ex.Message;
                                //return TranError;
                            }
                        }

                        var TemplateError = new TASK_FILE_UPLOAD();
                        TemplateError.STATUS = "Error";
                        TemplateError.MESSAGE = "Error Occurd";
                        return 0;
                    }
                    var sqlTransaction = (SqlTransaction)transaction;
                    await sqlTransaction.CommitAsync();
                    transactionCompleted = true;
                    return 1;
                }

            }
            catch (Exception ex)
            {
                if (transaction != null && !transactionCompleted)
                {
                    try
                    {
                        // Rollback only if the transaction is not yet completed
                        transaction.Rollback();
                    }
                    catch (InvalidOperationException rollbackEx)
                    {
                        // Handle rollback exception (may occur if transaction is already completed)
                        // Log or handle the rollback failure if needed
                        Console.WriteLine($"Rollback failed: {rollbackEx.Message}");
                        //var TranError = new APPROVAL_TASK_INITIATION();
                        //TranError.ResponseStatus = "Error";
                        //TranError.Message = ex.Message;
                        //return TranError;
                    }
                }

                var ErrorFileDetails = new TASK_FILE_UPLOAD();
                ErrorFileDetails.STATUS = "Error";
                ErrorFileDetails.MESSAGE = ex.Message;
                return 1;
            }
        }

        public async Task<int> UpdateTASKFileUpoadAsync(string LastUpdatedBy, string taskMkey, string deleteFlag)
        {
            DateTime dateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
            IDbTransaction transaction = null;
            bool transactionCompleted = false;
            try
            {
                using (IDbConnection db = _dapperDbConnection.CreateConnection())
                {
                    var sqlConnection = db as SqlConnection;
                    if (sqlConnection == null)
                    {
                        throw new InvalidOperationException("The connection must be a SqlConnection to use OpenAsync.");
                    }

                    if (sqlConnection.State != ConnectionState.Open)
                    {
                        await sqlConnection.OpenAsync();  // Ensure the connection is open
                    }

                    transaction = db.BeginTransaction();
                    transactionCompleted = false;  // Reset transaction state

                    var parameters = new DynamicParameters();
                    parameters.Add("@LastUpdatedBy", LastUpdatedBy);
                    parameters.Add("@TASK_MKEY", taskMkey);
                    parameters.Add("@DELETE_FLAG", deleteFlag);

                    var TaskFile = await db.ExecuteAsync("SP_RECURSIVE_DEL_ATTCAHMENT", parameters, commandType: CommandType.StoredProcedure, transaction: transaction);

                    if (TaskFile == null)
                    {
                        // Handle other unexpected exceptions
                        if (transaction != null && !transactionCompleted)
                        {
                            try
                            {
                                // Rollback only if the transaction is not yet completed
                                transaction.Rollback();
                            }
                            catch (InvalidOperationException rollbackEx)
                            {

                                Console.WriteLine($"Rollback failed: {rollbackEx.Message}");
                                //TranError.Message = ex.Message;
                                //return TranError;
                            }
                        }

                        var TemplateError = new TASK_FILE_UPLOAD();
                        TemplateError.STATUS = "Error";
                        TemplateError.MESSAGE = "Error Occurd";
                        return 0;
                    }

                    var sqlTransaction = (SqlTransaction)transaction;
                    await sqlTransaction.CommitAsync();
                    transactionCompleted = true;

                    return 1;
                }
            }
            catch (Exception ex)
            {
                if (transaction != null && !transactionCompleted)
                {
                    try
                    {
                        // Rollback only if the transaction is not yet completed
                        transaction.Rollback();
                    }
                    catch (InvalidOperationException rollbackEx)
                    {
                        // Handle rollback exception (may occur if transaction is already completed)
                        // Log or handle the rollback failure if needed
                        Console.WriteLine($"Rollback failed: {rollbackEx.Message}");
                        //var TranError = new APPROVAL_TASK_INITIATION();
                        //TranError.ResponseStatus = "Error";
                        //TranError.Message = ex.Message;
                        //return TranError;
                    }
                }

                var ErrorFileDetails = new TASK_FILE_UPLOAD();
                ErrorFileDetails.STATUS = "Error";
                ErrorFileDetails.MESSAGE = ex.Message;
                return 1;
            }
        }
        public async Task<ActionResult<string>> FileDownload()
        {
            IDbTransaction transaction = null;
            bool transactionCompleted = false;
            try
            {
                string strFilePath = string.Empty;
                using (IDbConnection db = _dapperDbConnection.CreateConnection())
                {
                    var sqlConnection = db as SqlConnection;
                    if (sqlConnection == null)
                    {
                        throw new InvalidOperationException("The connection must be a SqlConnection to use OpenAsync.");
                    }

                    if (sqlConnection.State != ConnectionState.Open)
                    {
                        await sqlConnection.OpenAsync();
                    }

                    transaction = db.BeginTransaction();
                    transactionCompleted = false;
                    var parametersConfigure = new DynamicParameters();
                    parametersConfigure.Add("@Session_User_Id", null);

                    var FilePathtext = await db.QueryAsync("select * from ConfigureTbl", commandType: CommandType.Text, transaction: transaction);

                    var FilePath = await db.QueryAsync<ConfigureTbl>("SP_GET_CONFIGURATION", parametersConfigure, commandType: CommandType.StoredProcedure, transaction: transaction);
                    foreach (var FileConfig in FilePath)
                    {
                        if (FileConfig.Configure.ToString().ToLower() == "FilePathNT".ToString().ToLower())
                        {
                            strFilePath = FileConfig.ConfigureValue;
                        }
                    }

                    var sqlTransaction = (SqlTransaction)transaction;
                    await sqlTransaction.CommitAsync();
                    transactionCompleted = true;
                    if (FilePath != null)
                    {
                        return strFilePath;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<ActionResult<Add_TaskOutPut_List_NT>> TASKFileUpoadNTAsync(int srNo, int taskMkey, int taskParentId, string fileName, string filePath, int createdBy, char deleteFlag, int taskMainNodeId)    //  
        {
            DateTime dateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
            IDbTransaction transaction = null;
            bool transactionCompleted = false;
            int SR_NO = 0;
            try
            {
                using (IDbConnection db = _dapperDbConnection.CreateConnection())
                {
                    var sqlConnection = db as SqlConnection;
                    if (sqlConnection == null)
                    {
                        throw new InvalidOperationException("The connection must be a SqlConnection to use OpenAsync.");
                    }

                    if (sqlConnection.State != ConnectionState.Open)
                    {
                        await sqlConnection.OpenAsync();  // Ensure the connection is open
                    }

                    transaction = db.BeginTransaction();
                    transactionCompleted = false;  // Reset transaction state

                    var parameters = new DynamicParameters();
                    parameters.Add("@SR_NO", srNo);
                    parameters.Add("@TASK_MKEY", taskMkey);
                    parameters.Add("@TASK_PARENT_ID", taskParentId);
                    parameters.Add("@FILE_NAME", fileName);
                    parameters.Add("@FILE_PATH", filePath);
                    parameters.Add("@CREATED_BY", createdBy);
                    parameters.Add("@ATTRIBUTE1", null);
                    parameters.Add("@ATTRIBUTE2", null);
                    parameters.Add("@ATTRIBUTE3", null);
                    parameters.Add("@ATTRIBUTE4", null);
                    parameters.Add("@ATTRIBUTE5", null);
                    parameters.Add("@LAST_UPDATED_BY", createdBy);
                    parameters.Add("@LAST_UPDATE_DATE", null);
                    parameters.Add("@DELETE_FLAG", deleteFlag);
                    parameters.Add("@TASK_MAIN_NODE_ID", taskMainNodeId);

                    var TaskFile = await db.ExecuteAsync("Sp_RECURSIVE_Insert_Attcahment", parameters, commandType: CommandType.StoredProcedure, transaction: transaction);

                    if (TaskFile == null)
                    {
                        // Handle other unexpected exceptions
                        if (transaction != null && !transactionCompleted)
                        {
                            try
                            {
                                // Rollback only if the transaction is not yet completed
                                transaction.Rollback();
                            }
                            catch (InvalidOperationException rollbackEx)
                            {

                                Console.WriteLine($"Rollback failed: {rollbackEx.Message}");
                                //TranError.Message = ex.Message;
                                //return TranError;
                            }
                        }

                        var TemplateError = new Add_TaskOutPut_List_NT();
                        TemplateError.Status = "Error";
                        TemplateError.Message = "Error Occurd";
                        return TemplateError;
                    }
                    var TemplateSuccess = new Add_TaskOutPut_List_NT();
                    var sqlTransaction = (SqlTransaction)transaction;
                    await sqlTransaction.CommitAsync();
                    transactionCompleted = true;
                    return TemplateSuccess;
                }

            }
            catch (Exception ex)
            {
                if (transaction != null && !transactionCompleted)
                {
                    try
                    {
                        // Rollback only if the transaction is not yet completed
                        transaction.Rollback();
                    }
                    catch (InvalidOperationException rollbackEx)
                    {
                        // Handle rollback exception (may occur if transaction is already completed)
                        // Log or handle the rollback failure if needed
                        Console.WriteLine($"Rollback failed: {rollbackEx.Message}");
                        //var TranError = new APPROVAL_TASK_INITIATION();
                        //TranError.ResponseStatus = "Error";
                        //TranError.Message = ex.Message;
                        //return TranError;
                    }
                }

                var ErrorFileDetails = new Add_TaskOutPut_List_NT();
                ErrorFileDetails.Status = "Error";
                ErrorFileDetails.Message = ex.Message;
                return ErrorFileDetails;
            }
        }

        public string SendEmail(string sp_to, string sp_cc, string sp_bcc, string sp_subject, string sp_body, string sp_mailtype, string sp_display_name, List<string> lp_attachment, MailDetailsNT mailDetailsNT)
        {
            string strerror = string.Empty;
            try
            {
                if (_env.env == "Production")
                {
                    using (MailMessage mail1 = new MailMessage())
                    {
                        mail1.From = new System.Net.Mail.MailAddress(mailDetailsNT.MAIL_FROM, sp_display_name.ToUpper());//, sp_display_name == "" ? dt.Rows[0]["MAIL_DISPLAY_NAME"].ToString() : sp_display_name
                                                                                                                         //mail1.To.Add("narendrakumar.soni@powersoft.in");
                        foreach (var to_address in sp_to.Replace(",", ";").Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
                        {
                            mail1.To.Add(new MailAddress(to_address));
                            //mail1.To.Add(new MailAddress("narendrakumar.soni@powersoft.in"));
                            //mail.To.Add("ashish.tripathi@powersoft.in");
                            //mail.CC.Add("brijesh.tiwari@powersoft.in");
                        }
                        if (sp_cc != null)
                            foreach (var cc_address in sp_cc.Replace(",", ";").Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
                            {
                                mail1.CC.Add(new MailAddress(cc_address));
                                // mail.CC.Add("brijesh.tiwari@powersoft.in");
                            }
                        if (sp_bcc != null)
                            foreach (var bcc_address in sp_bcc.Replace(",", ";").Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
                            {
                                mail1.Bcc.Add(new MailAddress(bcc_address));
                            }

                        mail1.Subject = sp_subject;
                        mail1.Body = sp_body;
                        mail1.IsBodyHtml = true;
                        //mail1.Attachments.Add(new Attachment("C:\\file.zip"));

                        using (SmtpClient smtp1 = new SmtpClient(mailDetailsNT.SMTP_HOST.ToString(), Convert.ToInt32(mailDetailsNT.SMTP_PORT)))
                        {
                            smtp1.Credentials = new NetworkCredential(mailDetailsNT.MAIL_FROM, mailDetailsNT.SMTP_PASS.ToString());
                            //new NetworkCredential("autosupport@powersoft.in", "yivz qklg jsbv ttso");
                            smtp1.EnableSsl = mailDetailsNT.SMTP_ESSL.ToString() == "true" ? true : false;

                            if (lp_attachment != null)
                                foreach (var attach in lp_attachment)
                                {
                                    mail1.Attachments.Add(new Attachment(attach));
                                }

                            smtp1.Send(mail1);
                        }
                        foreach (Attachment attachment in mail1.Attachments)
                        {
                            attachment.Dispose();
                        }

                    }
                }
                strerror = "Sent Email";
                return strerror;


                /*MailMessage mail = new MailMessage();


                foreach (var to_address in sp_to.Replace(",", ";").Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
                {
                    // mail.To.Add(new MailAddress(to_address));
                    mail.To.Add(new MailAddress("narendrakumar.soni@powersoft.in"));
                    //mail.To.Add("ashish.tripathi@powersoft.in");
                    //mail.CC.Add("brijesh.tiwari@powersoft.in");
                }
                if (sp_cc != null)
                    foreach (var cc_address in sp_cc.Replace(",", ";").Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        mail.CC.Add(new MailAddress(cc_address));
                        // mail.CC.Add("brijesh.tiwari@powersoft.in");
                    }
                if (sp_bcc != null)
                    foreach (var bcc_address in sp_bcc.Replace(",", ";").Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        mail.Bcc.Add(new MailAddress(bcc_address));
                    }

                mail.Subject = sp_subject;
                //mail.From = new System.Net.Mail.MailAddress(mailDetailsNT.MAIL_FROM, sp_display_name);//, sp_display_name == "" ? dt.Rows[0]["MAIL_DISPLAY_NAME"].ToString() : sp_display_name
                mail.From = new System.Net.Mail.MailAddress("autosupport@powersoft.in");//, sp_display_name == "" ? dt.Rows[0]["MAIL_DISPLAY_NAME"].ToString() : sp_display_name
                SmtpClient smtp = new SmtpClient();
                smtp.Timeout = Convert.ToInt32(mailDetailsNT.SMTP_TIMEOUT);
                smtp.Port = Convert.ToInt32(mailDetailsNT.SMTP_PORT);
                smtp.UseDefaultCredentials = true;
                smtp.Host = mailDetailsNT.SMTP_HOST.ToString();
//                sc.Credentials = basicAuthenticationInfo;
                smtp.Credentials = new NetworkCredential("autosupport@powersoft.in", "yivz qklg jsbv ttso");
                smtp.EnableSsl = mailDetailsNT.SMTP_ESSL.ToString() == "true" ? true : false;
                mail.IsBodyHtml = true;
                mail.Body = sp_body;
                if (lp_attachment != null)
                    foreach (var attach in lp_attachment)
                    {
                        mail.Attachments.Add(new Attachment(attach));
                    }
                smtp.Send(mail);*/


            }
            catch (Exception ex)
            {
                string FileName = string.Empty;
                string strFolder = string.Empty;

                strFolder = _fileSettings.FilePath; // "D:\\Application\\TaskDeployment" + "\\ErrorFolder";
                if (!Directory.Exists(strFolder))
                {
                    Directory.CreateDirectory(strFolder);
                }

                if (File.Exists(strFolder + "\\ErrorLog.txt") == false)
                {
                    using (System.IO.StreamWriter sw = File.CreateText(strFolder + "\\ErrorLog.txt"))
                    {
                        sw.Write("\n");
                        sw.WriteLine("--------------------------------------------------------------" + "\n");
                        sw.WriteLine(System.DateTime.Now);
                        sw.WriteLine(FileName + "--> " + ex.Message.ToString() + "\n");
                        sw.WriteLine("--------------------------------------------------------------" + "\n");
                    }
                }
                else
                {
                    using (System.IO.StreamWriter sw = File.AppendText(strFolder + "\\ErrorLog.txt"))
                    {
                        sw.Write("\n");
                        sw.WriteLine("--------------------------------------------------------------" + "\n");
                        sw.WriteLine(System.DateTime.Now);
                        sw.WriteLine(FileName + "--> " + ex.Message.ToString() + "\n");
                        sw.WriteLine("--------------------------------------------------------------" + "\n");
                    }
                }

                strerror = "Error Sending Email : " + ex.Message;
                return strerror;
            }
        }
        #endregion

        #region

        //        public string SendEmail(string sp_to, string sp_cc, string sp_bcc, string sp_subject, string sp_body, string sp_mailtype, string sp_display_name, List<string> lp_attachment, MailDetailsNT mailDetailsNT)
        //        {
        //            string strerror = string.Empty;
        //            try
        //            {


        //                using (MailMessage mail1 = new MailMessage())
        //                {
        //                    mail1.From = new System.Net.Mail.MailAddress(mailDetailsNT.MAIL_FROM, sp_display_name.ToUpper());//, sp_display_name == "" ? dt.Rows[0]["MAIL_DISPLAY_NAME"].ToString() : sp_display_name
        //                    //mail1.To.Add("narendrakumar.soni@powersoft.in");
        //                    foreach (var to_address in sp_to.Replace(",", ";").Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
        //                    {
        //                        mail1.To.Add(new MailAddress(to_address));
        //                        //mail1.To.Add(new MailAddress("narendrakumar.soni@powersoft.in"));
        //                        //mail.To.Add("ashish.tripathi@powersoft.in");
        //                        //mail.CC.Add("brijesh.tiwari@powersoft.in");
        //                    }
        //                    if (sp_cc != null)
        //                        foreach (var cc_address in sp_cc.Replace(",", ";").Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
        //                        {
        //                            mail1.CC.Add(new MailAddress(cc_address));
        //                            // mail.CC.Add("brijesh.tiwari@powersoft.in");
        //                        }
        //                    if (sp_bcc != null)
        //                        foreach (var bcc_address in sp_bcc.Replace(",", ";").Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
        //                        {
        //                            mail1.Bcc.Add(new MailAddress(bcc_address));
        //                        }

        //                    mail1.Subject = sp_subject;
        //                    mail1.Body = sp_body;
        //                    mail1.IsBodyHtml = true;
        //                    //mail1.Attachments.Add(new Attachment("C:\\file.zip"));

        //                    using (SmtpClient smtp1 = new SmtpClient(mailDetailsNT.SMTP_HOST.ToString(), Convert.ToInt32(mailDetailsNT.SMTP_PORT)))
        //                    {
        //                        smtp1.Credentials = new NetworkCredential(mailDetailsNT.MAIL_FROM, mailDetailsNT.SMTP_PASS.ToString());
        //                        //new NetworkCredential("autosupport@powersoft.in", "yivz qklg jsbv ttso");
        //                        smtp1.EnableSsl = mailDetailsNT.SMTP_ESSL.ToString() == "true" ? true : false;

        //                        if (lp_attachment != null)
        //                            foreach (var attach in lp_attachment)
        //                            {
        //                                mail1.Attachments.Add(new Attachment(attach));
        //                            }

        //                        smtp1.Send(mail1);
        //                    }
        //                    foreach (Attachment attachment in mail1.Attachments)
        //                    {
        //                        attachment.Dispose();
        //                    }

        //                }

        //                strerror = "Sent Email";
        //                return strerror;

        //                /*MailMessage mail = new MailMessage();


        //                foreach (var to_address in sp_to.Replace(",", ";").Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
        //                {
        //                    // mail.To.Add(new MailAddress(to_address));
        //                    mail.To.Add(new MailAddress("narendrakumar.soni@powersoft.in"));
        //                    //mail.To.Add("ashish.tripathi@powersoft.in");
        //                    //mail.CC.Add("brijesh.tiwari@powersoft.in");
        //                }
        //                if (sp_cc != null)
        //                    foreach (var cc_address in sp_cc.Replace(",", ";").Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
        //                    {
        //                        mail.CC.Add(new MailAddress(cc_address));
        //                        // mail.CC.Add("brijesh.tiwari@powersoft.in");
        //                    }
        //                if (sp_bcc != null)
        //                    foreach (var bcc_address in sp_bcc.Replace(",", ";").Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
        //                    {
        //                        mail.Bcc.Add(new MailAddress(bcc_address));
        //                    }

        //                mail.Subject = sp_subject;
        //                //mail.From = new System.Net.Mail.MailAddress(mailDetailsNT.MAIL_FROM, sp_display_name);//, sp_display_name == "" ? dt.Rows[0]["MAIL_DISPLAY_NAME"].ToString() : sp_display_name
        //                mail.From = new System.Net.Mail.MailAddress("autosupport@powersoft.in");//, sp_display_name == "" ? dt.Rows[0]["MAIL_DISPLAY_NAME"].ToString() : sp_display_name
        //                SmtpClient smtp = new SmtpClient();
        //                smtp.Timeout = Convert.ToInt32(mailDetailsNT.SMTP_TIMEOUT);
        //                smtp.Port = Convert.ToInt32(mailDetailsNT.SMTP_PORT);
        //                smtp.UseDefaultCredentials = true;
        //                smtp.Host = mailDetailsNT.SMTP_HOST.ToString();
        ////                sc.Credentials = basicAuthenticationInfo;
        //                smtp.Credentials = new NetworkCredential("autosupport@powersoft.in", "yivz qklg jsbv ttso");
        //                smtp.EnableSsl = mailDetailsNT.SMTP_ESSL.ToString() == "true" ? true : false;
        //                mail.IsBodyHtml = true;
        //                mail.Body = sp_body;
        //                if (lp_attachment != null)
        //                    foreach (var attach in lp_attachment)
        //                    {
        //                        mail.Attachments.Add(new Attachment(attach));
        //                    }
        //                smtp.Send(mail);*/


        //            }
        //            catch (Exception ex)
        //            {
        //                string FileName = string.Empty;
        //                string strFolder = string.Empty;

        //                strFolder = "D:\\Application\\TaskDeployment" + "\\ErrorFolder";
        //                if (!Directory.Exists(strFolder))
        //                {
        //                    Directory.CreateDirectory(strFolder);
        //                }

        //                if (File.Exists(strFolder + "\\ErrorLog.txt") == false)
        //                {
        //                    using (System.IO.StreamWriter sw = File.CreateText(strFolder + "\\ErrorLog.txt"))
        //                    {
        //                        sw.Write("\n");
        //                        sw.WriteLine("--------------------------------------------------------------" + "\n");
        //                        sw.WriteLine(System.DateTime.Now);
        //                        sw.WriteLine(FileName + "--> " + ex.Message.ToString() + "\n");
        //                        sw.WriteLine("--------------------------------------------------------------" + "\n");
        //                    }
        //                }
        //                else
        //                {
        //                    using (System.IO.StreamWriter sw = File.AppendText(strFolder + "\\ErrorLog.txt"))
        //                    {
        //                        sw.Write("\n");
        //                        sw.WriteLine("--------------------------------------------------------------" + "\n");
        //                        sw.WriteLine(System.DateTime.Now);
        //                        sw.WriteLine(FileName + "--> " + ex.Message.ToString() + "\n");
        //                        sw.WriteLine("--------------------------------------------------------------" + "\n");
        //                    }
        //                }

        //                strerror = "Error Sending Email : " + ex.Message;
        //                return strerror;
        //            }
        //        }

        #endregion

    }
}
