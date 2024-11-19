using Dapper;
using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Security.Policy;
using TaskManagement.API.Interfaces;
using TaskManagement.API.Model;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace TaskManagement.API.Repositories
{
    public class ProjectDefinationRepository : IProjectDefination
    {
        private static TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        public IDapperDbConnection _dapperDbConnection;
        private readonly string _connectionString;
        public ProjectDefinationRepository(IDapperDbConnection dapperDbConnection, string connectionString)
        {
            _dapperDbConnection = dapperDbConnection;
            _connectionString = connectionString;
        }
        public async Task<IEnumerable<PROJECT_HDR>> GetAllProjectDefinationAsync(int LoggedIN, string FormName, string MethodName)
        {
            try
            {
                using (IDbConnection db = _dapperDbConnection.CreateConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@MKEY", null);
                    parmeters.Add("@ATTRIBUT1", LoggedIN);
                    parmeters.Add("@ATTRIBUT2", FormName);
                    parmeters.Add("@ATTRIBUT3", MethodName);
                    var pROJECT_HDRs = await db.QueryAsync<PROJECT_HDR>("SP_GET_PROJECT_DEFINATION", parmeters, commandType: CommandType.StoredProcedure);

                    if (pROJECT_HDRs == null || !pROJECT_HDRs.Any())
                    {
                        return Enumerable.Empty<PROJECT_HDR>(); // Return an empty list if no results
                    }
                    // Iterate over each approval template header to populate subtasks, end result docs, and checklist docs
                    foreach (var apprvalProject in pROJECT_HDRs)
                    {
                        // Fetch the associated subtasks
                        var approvalAbbr = await db.QueryAsync<PROJECT_TRL_APPROVAL_ABBR>(
                                 "SELECT HEADER_MKEY,SEQ_NO AS TASK_NO,APPROVAL_ABBRIVATION,APPROVAL_DESCRIPTION,DAYS_REQUIRED,DEPARTMENT,JOB_ROLE,RESPOSIBLE_EMP_MKEY,OUTPUT_DOCUMENT,TENTATIVE_START_DATE,TENTATIVE_END_DATE,STATUS FROM PROJECT_TRL_APPROVAL_ABBR WHERE HEADER_MKEY = @HEADER_MKEY",
                                 new { HEADER_MKEY = apprvalProject.MKEY });

                        apprvalProject.APPROVALS_ABBR_LIST = approvalAbbr.ToList(); // Populate the SUBTASK_LIST property
                    }
                    return pROJECT_HDRs;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<PROJECT_HDR> GetProjectDefinationByIdAsync(int id, int LoggedIN, string FormName, string MethodName)
        {
            try
            {
                using (IDbConnection db = _dapperDbConnection.CreateConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@MKEY", id);
                    parmeters.Add("@ATTRIBUT1", LoggedIN);
                    parmeters.Add("@ATTRIBUT2", FormName);
                    parmeters.Add("@ATTRIBUT3", MethodName);

                    // Fetch the associated subtasks
                    var pROJECT_HDRs = await db.QueryFirstOrDefaultAsync<PROJECT_HDR>("SP_GET_PROJECT_DEFINATION", parmeters, commandType: CommandType.StoredProcedure);
                    var approvalAbbr = await db.QueryAsync<PROJECT_TRL_APPROVAL_ABBR>(
                             "SELECT HEADER_MKEY,SEQ_NO AS TASK_NO,APPROVAL_ABBRIVATION,APPROVAL_DESCRIPTION,DAYS_REQUIRED,DEPARTMENT,JOB_ROLE,RESPOSIBLE_EMP_MKEY" +
                             ",OUTPUT_DOCUMENT,TENTATIVE_START_DATE,TENTATIVE_END_DATE,STATUS FROM PROJECT_TRL_APPROVAL_ABBR WHERE HEADER_MKEY = @HEADER_MKEY;",
                             new { HEADER_MKEY = pROJECT_HDRs.MKEY });

                    pROJECT_HDRs.APPROVALS_ABBR_LIST = approvalAbbr.ToList(); // Populate the SUBTASK_LIST property
                    return pROJECT_HDRs;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<PROJECT_HDR> CreateProjectDefinationAsync(PROJECT_HDR pROJECT_HDR)
        {
            DateTime dateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
            try
            {

                using (IDbConnection db = _dapperDbConnection.CreateConnection())
                {
                    var OBJ_PROJECT_HDR = pROJECT_HDR;
                    var parameters = new DynamicParameters();
                    parameters.Add("@PROJECT_NAME", pROJECT_HDR.PROJECT_NAME);
                    parameters.Add("@PROJECT_ABBR", pROJECT_HDR.PROJECT_ABBR);
                    parameters.Add("@PROPERTY", pROJECT_HDR.PROPERTY);
                    parameters.Add("@LEGAL_ENTITY", pROJECT_HDR.LEGAL_ENTITY);
                    parameters.Add("@PROJECT_ADDRESS", pROJECT_HDR.PROJECT_ADDRESS);
                    parameters.Add("@BUILDING_CLASSIFICATION", pROJECT_HDR.BUILDING_CLASSIFICATION);
                    parameters.Add("@BUILDING_STANDARD", pROJECT_HDR.BUILDING_STANDARD);
                    parameters.Add("@STATUTORY_AUTHORITY", pROJECT_HDR.STATUTORY_AUTHORITY);
                    parameters.Add("@CREATED_BY", pROJECT_HDR.CREATED_BY);
                    parameters.Add("@LAST_UPDATED_BY", pROJECT_HDR.LAST_UPDATED_BY);
                    parameters.Add("@ATTRIBUTE1", pROJECT_HDR.ATTRIBUTE1);
                    parameters.Add("@ATTRIBUTE2", pROJECT_HDR.ATTRIBUTE2);
                    parameters.Add("@ATTRIBUTE3", pROJECT_HDR.ATTRIBUTE3);

                    pROJECT_HDR = await db.QueryFirstOrDefaultAsync<PROJECT_HDR>("SP_INSERT_PROJECT_DEFINATION", parameters, commandType: CommandType.StoredProcedure);

                    using var connection = new SqlConnection(_connectionString);
                    await connection.OpenAsync();
                    using var transactionApprovals = connection.BeginTransaction();
                    try
                    {
                        // Create a DataTable for bulk insert of subtasks (SEQNO, SRNO, ABBR)
                        var approvalsDataTable = new DataTable();
                        approvalsDataTable.Columns.Add("HEADER_MKEY", typeof(int));
                        approvalsDataTable.Columns.Add("SEQ_NO", typeof(string));
                        approvalsDataTable.Columns.Add("APPROVAL_ABBRIVATION", typeof(string));
                        approvalsDataTable.Columns.Add("APPROVAL_DESCRIPTION", typeof(string));
                        approvalsDataTable.Columns.Add("DAYS_REQUIRED", typeof(int));
                        approvalsDataTable.Columns.Add("DEPARTMENT", typeof(int));
                        approvalsDataTable.Columns.Add("JOB_ROLE", typeof(int));
                        approvalsDataTable.Columns.Add("RESPOSIBLE_EMP_MKEY", typeof(int));
                        approvalsDataTable.Columns.Add("OUTPUT_DOCUMENT", typeof(string));
                        approvalsDataTable.Columns.Add("TENTATIVE_START_DATE", typeof(DateTime));
                        approvalsDataTable.Columns.Add("TENTATIVE_END_DATE", typeof(DateTime));
                        approvalsDataTable.Columns.Add("STATUS", typeof(string));
                        approvalsDataTable.Columns.Add("ATTRIBUTE1", typeof(string));
                        approvalsDataTable.Columns.Add("ATTRIBUTE2", typeof(string));
                        approvalsDataTable.Columns.Add("ATTRIBUTE3", typeof(string));
                        approvalsDataTable.Columns.Add("ATTRIBUTE4", typeof(string));
                        approvalsDataTable.Columns.Add("ATTRIBUTE5", typeof(string));
                        approvalsDataTable.Columns.Add("CREATED_BY", typeof(int));
                        approvalsDataTable.Columns.Add("CREATION_DATE", typeof(DateTime));
                        approvalsDataTable.Columns.Add("LAST_UPDATED_BY", typeof(int));
                        approvalsDataTable.Columns.Add("LAST_UPDATE_DATE", typeof(DateTime));
                        approvalsDataTable.Columns.Add("DELETE_FLAG", typeof(char));

                        if (OBJ_PROJECT_HDR.APPROVALS_ABBR_LIST != null)
                        {
                            // Populate the DataTable with subtasks
                            foreach (var approvalsList in OBJ_PROJECT_HDR.APPROVALS_ABBR_LIST) // Assuming SUBTASK_LIST is a list of subtasks
                            {
                                approvalsDataTable.Rows.Add(pROJECT_HDR.MKEY, approvalsList.TASK_NO, approvalsList.APPROVAL_ABBRIVATION, approvalsList.APPROVAL_DESCRIPTION, approvalsList.DAYS_REQUIRED, approvalsList.DEPARTMENT, approvalsList.JOB_ROLE, approvalsList.RESPOSIBLE_EMP_MKEY, approvalsList.OUTPUT_DOCUMENT, approvalsList.TENTATIVE_START_DATE, approvalsList.TENTATIVE_END_DATE, approvalsList.STATUS, null, null, null, null, null, OBJ_PROJECT_HDR.CREATED_BY, dateTime.ToString("yyyy/MM/dd hh:mm:ss"), OBJ_PROJECT_HDR.CREATED_BY, dateTime.ToString("yyyy/MM/dd hh:mm:ss"), 'N');
                            }

                            // Use SqlBulkCopy to insert subtasks
                            using var bulkCopy = new SqlBulkCopy(connection, SqlBulkCopyOptions.Default, transactionApprovals)
                            {
                                DestinationTableName = "PROJECT_TRL_APPROVAL_ABBR"  // Ensure this matches your table name
                            };

                            await bulkCopy.WriteToServerAsync(approvalsDataTable);

                            // Commit the transactionApprovals
                            await transactionApprovals.CommitAsync();

                            // Optionally, fetch the inserted values (if necessary)
                            string sql = "SELECT  HEADER_MKEY,SEQ_NO, APPROVAL_ABBRIVATION,APPROVAL_DESCRIPTION," +
                                "DAYS_REQUIRED,DEPARTMENT,JOB_ROLE,RESPOSIBLE_EMP_MKEY,OUTPUT_DOCUMENT,TENTATIVE_START_DATE,TENTATIVE_END_DATE,STATUS," +
                                "ATTRIBUTE1,ATTRIBUTE2,ATTRIBUTE3,ATTRIBUTE4,ATTRIBUTE5,CREATED_BY,CREATION_DATE,LAST_UPDATED_BY,LAST_UPDATE_DATE,DELETE_FLAG " +
                                "FROM [dbo].[PROJECT_TRL_APPROVAL_ABBR] WHERE HEADER_MKEY = @HEADER_MKEY";
                            var ApprovalsKeyValuePairs = await db.QueryAsync(sql, new { HEADER_MKEY = pROJECT_HDR.MKEY });

                            // Assuming the model has a SUBTASK_LIST dictionary to hold these values
                            pROJECT_HDR.APPROVALS_ABBR_LIST = new List<PROJECT_TRL_APPROVAL_ABBR>();  // Assuming Subtask is a class for this data

                            foreach (var item in ApprovalsKeyValuePairs)
                            {
                                pROJECT_HDR.APPROVALS_ABBR_LIST.Add(new PROJECT_TRL_APPROVAL_ABBR
                                {
                                    HEADER_MKEY = item.MKEY,
                                    TASK_NO = item.SEQ_NO,
                                    APPROVAL_ABBRIVATION = item.APPROVAL_ABBRIVATION,
                                    APPROVAL_DESCRIPTION = item.APPROVAL_DESCRIPTION,
                                    DAYS_REQUIRED = item.DAYS_REQUIRED,
                                    DEPARTMENT = item.DEPARTMENT,
                                    JOB_ROLE = item.JOB_ROLE,
                                    RESPOSIBLE_EMP_MKEY = item.RESPOSIBLE_EMP_MKEY,
                                    OUTPUT_DOCUMENT = item.OUTPUT_DOCUMENT,
                                    TENTATIVE_START_DATE = item.TENTATIVE_START_DATE,
                                    TENTATIVE_END_DATE = item.TENTATIVE_END_DATE,
                                    STATUS = item.STATUS
                                });
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        await transactionApprovals.RollbackAsync();
                        throw;
                    }

                    return pROJECT_HDR;
                }

            }
            catch (SqlException ex)
            {
                return pROJECT_HDR;
            }
            catch (Exception ex)
            {
                return pROJECT_HDR;
            }
        }
        public async Task<bool> UpdateProjectDefinationAsync(PROJECT_HDR pROJECT_HDR)
        {
            try
            {
                using (IDbConnection db = _dapperDbConnection.CreateConnection())
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@MKEY", pROJECT_HDR.MKEY);
                    parameters.Add("@PROJECT_NAME", pROJECT_HDR.PROJECT_NAME);
                    parameters.Add("@PROJECT_ABBR", pROJECT_HDR.PROJECT_ABBR);
                    parameters.Add("@PROPERTY", pROJECT_HDR.PROPERTY);
                    parameters.Add("@LEGAL_ENTITY", pROJECT_HDR.LEGAL_ENTITY);
                    parameters.Add("@PROJECT_ADDRESS", pROJECT_HDR.PROJECT_ADDRESS);
                    parameters.Add("@BUILDING_CLASSIFICATION", pROJECT_HDR.BUILDING_CLASSIFICATION);
                    parameters.Add("@BUILDING_STANDARD", pROJECT_HDR.BUILDING_STANDARD);
                    parameters.Add("@STATUTORY_AUTHORITY", pROJECT_HDR.STATUTORY_AUTHORITY);
                    parameters.Add("@LAST_UPDATED_BY", pROJECT_HDR.LAST_UPDATED_BY);
                    parameters.Add("@ATTRIBUTE1", pROJECT_HDR.ATTRIBUTE1);  // LoggedIN user
                    parameters.Add("@ATTRIBUTE2", pROJECT_HDR.ATTRIBUTE2);  // FormName
                    parameters.Add("@ATTRIBUTE3", pROJECT_HDR.ATTRIBUTE3);  // Method Name/ Function Name
                    await db.ExecuteAsync("SP_UPDATE_PROJECT_DEFINATION", parameters, commandType: CommandType.StoredProcedure);
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<bool> DeleteProjectDefinationAsync(int id, int LastUpatedBy, string FormName, string MethodName)
        {
            try
            {
                using (IDbConnection db = _dapperDbConnection.CreateConnection())
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@MKEY", id);
                    parameters.Add("@LAST_UPDATED_BY", LastUpatedBy);
                    parameters.Add("@ATTRIBUTE1", LastUpatedBy);
                    parameters.Add("@ATTRIBUTE2", FormName);
                    parameters.Add("@ATTRIBUTE3", MethodName);
                    await db.ExecuteAsync("SP_DELETE_PROJECT_DEFINATION", parameters, commandType: CommandType.StoredProcedure);
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<IEnumerable<PROJECT_TRL_APPROVAL_ABBR_LIST>> GetApprovalDetails(int LoggedInID, int BUILDING_TYPE, string BUILDING_STANDARD,
            string STATUTORY_AUTHORITY)
        {
            try
            {
                using (IDbConnection db = _dapperDbConnection.CreateConnection())
                {
                    string sql = "SELECT TRL.HEADER_MKEY, SEQ_NO AS TASK_NO,MAIN_ABBR, TRL.SUBTASK_ABBR + ' ' + HDR2.SHORT_DESCRIPTION AS ABBR_SHORT_DESC,HDR2.DAYS_REQUIERD," +
                    " HDR2.AUTHORITY_DEPARTMENT,HDR2.JOB_ROLE,HDR2.RESPOSIBLE_EMP_MKEY,STUFF((    SELECT DISTINCT ', ' + CAST(ENDLIST2.DOCUMENT_NAME AS VARCHAR(MAX))    " +
                    " FROM APPROVAL_TEMPLATE_TRL_ENDRESULT ENDLIST2    WHERE ENDLIST2.MKEY = TRL.HEADER_MKEY      FOR XML PATH('')), 1, 1, '') AS END_RESULT_DOC " +
                    " FROM APPROVAL_TEMPLATE_HDR HDR2 INNER JOIN APPROVAL_TEMPLATE_TRL_SUBTASK TRL ON HDR2.MKEY = TRL.HEADER_MKEY " +
                    " LEFT JOIN APPROVAL_TEMPLATE_TRL_ENDRESULT ENDLIST1 ON ENDLIST1.MKEY = HDR2.MKEY " +
                    " WHERE BUILDING_TYPE = @BUILDING_TYPE " +
                    " AND BUILDING_STANDARD = @BUILDING_STANDARD and STATUTORY_AUTHORITY = @STATUTORY_AUTHORITY " +
                    " GROUP BY  TRL.HEADER_MKEY,SEQ_NO,MAIN_ABBR, TRL.SUBTASK_ABBR + ' ' + HDR2.SHORT_DESCRIPTION,HDR2.DAYS_REQUIERD,HDR2.AUTHORITY_DEPARTMENT" +
                    " ,HDR2.JOB_ROLE,HDR2.RESPOSIBLE_EMP_MKEY ORDER BY TRL.HEADER_MKEY,MAIN_ABBR;";
                    var ApprovalsKeyValuePairs = await db.QueryAsync<PROJECT_TRL_APPROVAL_ABBR_LIST>(sql, new
                    {
                        BUILDING_TYPE = BUILDING_TYPE,
                        BUILDING_STANDARD = BUILDING_STANDARD,
                        STATUTORY_AUTHORITY = STATUTORY_AUTHORITY
                    });
                    return ApprovalsKeyValuePairs;
                }
            }
            catch //(Exception ex)
            {
                return Enumerable.Empty<PROJECT_TRL_APPROVAL_ABBR_LIST>(); //return ApprovalsKeyValuePairs;
            }

        }
    }
}
