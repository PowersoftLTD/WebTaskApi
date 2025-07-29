using Azure;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.Reflection;
using System.Reflection.Metadata;
using System.Security.Policy;
using System.Threading.Tasks;
using System.Transactions;
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
                    //var APPROVAL_MKEY = await db.QueryAsync<dynamic>("UPDATE PROJECT_TRL_APPROVAL_ABBR SET APPROVAL_MKEY = 32 WHERE HEADER_MKEY = 5 ",
                    //commandType: CommandType.Text);
                    var pROJECT_HDRs = await db.QueryAsync<PROJECT_HDR>("SP_GET_PROJECT_DEFINATION", parmeters, commandType: CommandType.StoredProcedure);
                    //var pROJECT_HDRs122 = await db.QueryAsync<PROJECT_HDR>("SELECT\tMKEY ,BUILDING_MKEY AS PROJECT_NAME ,PROJECT_ABBR" +
                    //    " ,PROPERTY ,LEGAL_ENTITY ,PROJECT_ADDRESS ,BUILDING_CLASSIFICATION ,BUILDING_STANDARD" +
                    //    " ,STATUTORY_AUTHORITY ,ATTRIBUTE1 ,ATTRIBUTE2 ,ATTRIBUTE3 ,ATTRIBUTE4 " +
                    //    ",ATTRIBUTE5 ,CREATED_BY ,CREATION_DATE ,LAST_UPDATED_BY ,LAST_UPDATE_DATE " +
                    //    ",DELETE_FLAG FROM PROJECT_HDR  WHERE CREATED_BY = @ATTRIBUT1 AND DELETE_FLAG = 'N' " +
                    //    "ORDER BY  MKEY;", parmeters, commandType: CommandType.Text);

                    if (pROJECT_HDRs == null || !pROJECT_HDRs.Any())
                    {

                        return Enumerable.Empty<PROJECT_HDR>(); // Return an empty list if no results
                    }
                    // Iterate over each approval template header to populate subtasks, end result docs, and checklist docs
                    foreach (var apprvalProject in pROJECT_HDRs)
                    {
                        // Fetch the associated subtasks
                        var approvalAbbr = await db.QueryAsync<PROJECT_TRL_APPROVAL_ABBR>("SELECT * FROM  V_APPROVAL_SUBTASK_DETAILS " +
                            "WHERE HEADER_MKEY = @HEADER_MKEY;",
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

        public async Task<ActionResult<IEnumerable<PROJECT_HDR_NT_OUTPUT>>> GetAllProjectDefinationAsyncNT(ProjectHdrNT projectHdrNT)
        {
            try
            {
                using (IDbConnection db = _dapperDbConnection.CreateConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@MKEY", projectHdrNT.ProjectMkey);
                    parmeters.Add("@Session_User_Id", projectHdrNT.Session_User_Id);
                    parmeters.Add("@Business_Group_Id", projectHdrNT.Business_Group_Id);

                    var pROJECT_HDRs = await db.QueryAsync<PROJECT_HDR_NT>("SP_GET_PROJECT_DEFINATION_NT", parmeters, commandType: CommandType.StoredProcedure);
                    if (pROJECT_HDRs == null || !pROJECT_HDRs.Any())
                    {
                        var ProjectError = new List<PROJECT_HDR_NT_OUTPUT>
                        {
                            new PROJECT_HDR_NT_OUTPUT
                            {
                                Status = "OK",
                                Message = "Data not found",
                                Data = null
                            }
                        };
                        return ProjectError;
                    }
                    foreach (var apprvalProject in pROJECT_HDRs)
                    {
                        var parmetersApproval = new DynamicParameters();
                        parmetersApproval.Add("@HEADER_MKEY", apprvalProject.MKEY);
                        parmetersApproval.Add("@Session_User_Id", projectHdrNT.Session_User_Id);
                        parmetersApproval.Add("@Business_Group_Id", projectHdrNT.Business_Group_Id);

                        var approvalAbbr = await db.QueryAsync<PROJECT_TRL_APPROVAL_ABBR>("SP_GET_APPROVAL_SUBTASK_DETAILS_NT", parmetersApproval, commandType: CommandType.StoredProcedure);

                        //var approvalAbbr = await db.QueryAsync<PROJECT_TRL_APPROVAL_ABBR>("SELECT * FROM  V_APPROVAL_SUBTASK_DETAILS " +
                        //    "WHERE HEADER_MKEY = @HEADER_MKEY;",
                        //         new { HEADER_MKEY = apprvalProject.MKEY });
                        apprvalProject.APPROVALS_ABBR_LIST = approvalAbbr.ToList(); // Populate the SUBTASK_LIST property
                    }

                    var successResult = new List<PROJECT_HDR_NT_OUTPUT>
                    {
                        new PROJECT_HDR_NT_OUTPUT
                        {
                            Status = "Ok",
                            Message = $"Get Data Successfuly!!!",
                            Data = pROJECT_HDRs
                        }
                    };
                    return successResult;
                }
            }
            catch (Exception ex)
            {
                var errorResult = new List<PROJECT_HDR_NT_OUTPUT>
                {
                    new PROJECT_HDR_NT_OUTPUT
                    {
                        Status = "Error",
                        Message = $" Error: {ex.Message}",
                        Data = null
                    }
                };
                return errorResult;
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

                    if (pROJECT_HDRs == null)
                    {
                        return null;
                    }
                    else
                    {
                        var approvalAbbr = await db.QueryAsync<PROJECT_TRL_APPROVAL_ABBR>("SELECT * FROM  V_APPROVAL_SUBTASK_DETAILS WHERE HEADER_MKEY = @HEADER_MKEY;",
                     new { HEADER_MKEY = pROJECT_HDRs.MKEY });
                        pROJECT_HDRs.APPROVALS_ABBR_LIST = approvalAbbr.ToList(); // Populate the SUBTASK_LIST property
                    }
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
            IDbTransaction transaction = null;
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
                    transaction = db.BeginTransaction(); // Begin a transaction

                    var OBJ_PROJECT_HDR = pROJECT_HDR;
                    var parameters = new DynamicParameters();
                    parameters.Add("@BUILDING_MKEY", pROJECT_HDR.PROJECT_NAME);
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

                    // Insert project definition
                    pROJECT_HDR = await db.QueryFirstOrDefaultAsync<PROJECT_HDR>("SP_INSERT_PROJECT_DEFINATION", parameters, commandType: CommandType.StoredProcedure, transaction: transaction);

                    if (pROJECT_HDR.Status != "Error")
                    {
                        // Create a DataTable for bulk insert of approval data
                        var approvalsDataTable = new DataTable();
                        approvalsDataTable.Columns.Add("HEADER_MKEY", typeof(int));
                        approvalsDataTable.Columns.Add("SEQ_NO", typeof(string));
                        approvalsDataTable.Columns.Add("APPROVAL_MKEY", typeof(int));
                        approvalsDataTable.Columns.Add("APPROVAL_ABBRIVATION", typeof(string));
                        approvalsDataTable.Columns.Add("APPROVAL_DESCRIPTION", typeof(string));
                        approvalsDataTable.Columns.Add("DAYS_REQUIRED", typeof(int));
                        approvalsDataTable.Columns.Add("RESPOSIBLE_EMP_MKEY", typeof(int));
                        approvalsDataTable.Columns.Add("TENTATIVE_START_DATE", typeof(string));
                        approvalsDataTable.Columns.Add("TENTATIVE_END_DATE", typeof(string));
                        approvalsDataTable.Columns.Add("DEPARTMENT", typeof(int));
                        approvalsDataTable.Columns.Add("JOB_ROLE", typeof(int));
                        approvalsDataTable.Columns.Add("OUTPUT_DOCUMENT", typeof(string));
                        approvalsDataTable.Columns.Add("STATUS", typeof(string));
                        approvalsDataTable.Columns.Add("CREATED_BY", typeof(int));
                        approvalsDataTable.Columns.Add("CREATION_DATE", typeof(DateTime));

                        foreach (var approvalsList in OBJ_PROJECT_HDR.APPROVALS_ABBR_LIST)
                        {
                            var RESPOSIBLE_EMP_MKEY = approvalsList.RESPOSIBLE_EMP_MKEY ?? 0;
                            var row = approvalsDataTable.NewRow();
                            row["HEADER_MKEY"] = pROJECT_HDR.MKEY;
                            row["SEQ_NO"] = approvalsList.TASK_NO;
                            row["APPROVAL_MKEY"] = approvalsList.APPROVAL_MKEY;
                            row["APPROVAL_ABBRIVATION"] = approvalsList.APPROVAL_ABBRIVATION;
                            row["APPROVAL_DESCRIPTION"] = approvalsList.APPROVAL_DESCRIPTION;
                            row["DAYS_REQUIRED"] = approvalsList.DAYS_REQUIRED;
                            row["RESPOSIBLE_EMP_MKEY"] = RESPOSIBLE_EMP_MKEY;
                            row["TENTATIVE_START_DATE"] = string.IsNullOrEmpty(approvalsList.TENTATIVE_START_DATE)
                            ? DBNull.Value
                            : (object)DateTime.Parse(approvalsList.TENTATIVE_START_DATE);
                            row["TENTATIVE_END_DATE"] = string.IsNullOrEmpty(approvalsList.TENTATIVE_END_DATE)
                                   ? DBNull.Value
                                   : (object)DateTime.Parse(approvalsList.TENTATIVE_END_DATE);
                            row["DEPARTMENT"] = approvalsList.DEPARTMENT;
                            row["JOB_ROLE"] = approvalsList.JOB_ROLE;
                            row["OUTPUT_DOCUMENT"] = approvalsList.OUTPUT_DOCUMENT;
                            row["STATUS"] = "Ready to Initiate";//approvalsList.STATUS;
                            row["CREATED_BY"] = pROJECT_HDR.CREATED_BY;
                            row["CREATION_DATE"] = dateTime;

                            approvalsDataTable.Rows.Add(row);
                        }

                        // Use SqlBulkCopy to insert the approval data into the database
                        using var bulkCopy = new SqlBulkCopy((SqlConnection)db, SqlBulkCopyOptions.Default, (SqlTransaction)transaction)
                        {
                            DestinationTableName = "PROJECT_TRL_APPROVAL_ABBR"
                        };

                        // Map columns from DataTable to database columns
                        bulkCopy.ColumnMappings.Add("HEADER_MKEY", "HEADER_MKEY");
                        bulkCopy.ColumnMappings.Add("SEQ_NO", "SEQ_NO");
                        bulkCopy.ColumnMappings.Add("APPROVAL_MKEY", "APPROVAL_MKEY");
                        bulkCopy.ColumnMappings.Add("APPROVAL_ABBRIVATION", "APPROVAL_ABBRIVATION");
                        bulkCopy.ColumnMappings.Add("APPROVAL_DESCRIPTION", "APPROVAL_DESCRIPTION");
                        bulkCopy.ColumnMappings.Add("DAYS_REQUIRED", "DAYS_REQUIRED");
                        bulkCopy.ColumnMappings.Add("RESPOSIBLE_EMP_MKEY", "RESPOSIBLE_EMP_MKEY");
                        bulkCopy.ColumnMappings.Add("TENTATIVE_START_DATE", "TENTATIVE_START_DATE");
                        bulkCopy.ColumnMappings.Add("TENTATIVE_END_DATE", "TENTATIVE_END_DATE");
                        bulkCopy.ColumnMappings.Add("DEPARTMENT", "DEPARTMENT");
                        bulkCopy.ColumnMappings.Add("JOB_ROLE", "JOB_ROLE");
                        bulkCopy.ColumnMappings.Add("OUTPUT_DOCUMENT", "OUTPUT_DOCUMENT");
                        bulkCopy.ColumnMappings.Add("STATUS", "STATUS");
                        bulkCopy.ColumnMappings.Add("CREATED_BY", "CREATED_BY");
                        bulkCopy.ColumnMappings.Add("CREATION_DATE", "CREATION_DATE");

                        await bulkCopy.WriteToServerAsync(approvalsDataTable);
                    }
                    else
                    {
                        return pROJECT_HDR;
                    }

                    // Commit both transactions
                    var sqlTransaction = (SqlTransaction)transaction;
                    await sqlTransaction.CommitAsync();
                    return pROJECT_HDR;
                }
            }
            catch (Exception ex)
            {
                if (transaction != null)
                {
                    var sqlTransaction = (SqlTransaction)transaction;
                    await sqlTransaction.RollbackAsync(); // Rollback transaction if an exception occurs
                }

                pROJECT_HDR.Status = "Error";
                pROJECT_HDR.Message = ex.Message;
                return pROJECT_HDR;
            }
        }
        public async Task<PROJECT_HDR> UpdateProjectDefinationAsync(PROJECT_HDR pROJECT_HDR)
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
                    transaction = db.BeginTransaction(); // Begin a transaction

                    var OBJ_PROJECT_HDR = pROJECT_HDR;
                    var parameters = new DynamicParameters();
                    parameters.Add("@MKEY", pROJECT_HDR.MKEY);
                    parameters.Add("@BUILDING_MKEY", pROJECT_HDR.PROJECT_NAME);
                    parameters.Add("@PROJECT_ABBR", pROJECT_HDR.PROJECT_ABBR);
                    parameters.Add("@PROPERTY", pROJECT_HDR.PROPERTY);
                    parameters.Add("@LEGAL_ENTITY", pROJECT_HDR.LEGAL_ENTITY);
                    parameters.Add("@PROJECT_ADDRESS", pROJECT_HDR.PROJECT_ADDRESS);
                    parameters.Add("@BUILDING_CLASSIFICATION", pROJECT_HDR.BUILDING_CLASSIFICATION);
                    parameters.Add("@BUILDING_STANDARD", pROJECT_HDR.BUILDING_STANDARD);
                    parameters.Add("@STATUTORY_AUTHORITY", pROJECT_HDR.STATUTORY_AUTHORITY);
                    parameters.Add("@LAST_UPDATED_BY", pROJECT_HDR.LAST_UPDATED_BY);
                    //parameters.Add("@ATTRIBUTE1", pROJECT_HDR.ATTRIBUTE1);  // LoggedIN user
                    //parameters.Add("@ATTRIBUTE2", pROJECT_HDR.ATTRIBUTE2);  // FormName
                    //parameters.Add("@ATTRIBUTE3", pROJECT_HDR.ATTRIBUTE3);  // Method Name/ Function Name
                    var UpdateProjectHDR = await db.QueryAsync<PROJECT_HDR>("SP_UPDATE_PROJECT_DEFINATION", parameters, commandType: CommandType.StoredProcedure, transaction: transaction);

                    if (UpdateProjectHDR.Any() == null)
                    {
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

                        pROJECT_HDR.Status = "Error";
                        pROJECT_HDR.Message = "Error Occurd";
                        return pROJECT_HDR;
                    }

                    // TO CHECK SUBTASK IS PRESENT OR NOT PRESENT THEN NOT INSERT OR UPDATE
                    if (OBJ_PROJECT_HDR.APPROVALS_ABBR_LIST != null)
                    {
                        // TO CHECK CREATED OR INITIATED THE STATUS IN THE LIST
                        var InsertProjectList = OBJ_PROJECT_HDR.APPROVALS_ABBR_LIST.Find(x => x.STATUS.ToString().ToLower() == "Created".ToString().ToLower());
                        var UpdateProjectList = OBJ_PROJECT_HDR.APPROVALS_ABBR_LIST.Find(x => x.STATUS.ToString().ToLower() == "Ready to Initiate".ToString().ToLower());

                        // IF STATUS IS CREATED FOR INSERT THEN IT WILL BE INSERT THE VALUE
                        if (InsertProjectList != null)
                        {
                            //using var connection = new SqlConnection(_connectionString);
                            //await connection.OpenAsync();
                            //using var transactionApprovals = connection.BeginTransaction();

                            // Insert list of subtask that status is Created
                            var approvalsInsertDataTable = new DataTable();
                            approvalsInsertDataTable.Columns.Add("HEADER_MKEY", typeof(int));
                            approvalsInsertDataTable.Columns.Add("SEQ_NO", typeof(string));
                            approvalsInsertDataTable.Columns.Add("APPROVAL_ABBRIVATION", typeof(string));
                            approvalsInsertDataTable.Columns.Add("APPROVAL_DESCRIPTION", typeof(string));
                            approvalsInsertDataTable.Columns.Add("DAYS_REQUIRED", typeof(int));
                            approvalsInsertDataTable.Columns.Add("DEPARTMENT", typeof(int));
                            approvalsInsertDataTable.Columns.Add("JOB_ROLE", typeof(int));
                            approvalsInsertDataTable.Columns.Add("RESPOSIBLE_EMP_MKEY", typeof(int));
                            approvalsInsertDataTable.Columns.Add("OUTPUT_DOCUMENT", typeof(string));
                            approvalsInsertDataTable.Columns.Add("TENTATIVE_START_DATE", typeof(string));
                            approvalsInsertDataTable.Columns.Add("TENTATIVE_END_DATE", typeof(string));
                            approvalsInsertDataTable.Columns.Add("STATUS", typeof(string));
                            approvalsInsertDataTable.Columns.Add("APPROVAL_MKEY", typeof(int));
                            approvalsInsertDataTable.Columns.Add("CREATED_BY", typeof(int));
                            approvalsInsertDataTable.Columns.Add("CREATION_DATE", typeof(DateTime));
                            approvalsInsertDataTable.Columns.Add("LAST_UPDATED_BY", typeof(int));
                            approvalsInsertDataTable.Columns.Add("DELETE_FLAG", typeof(char));

                            foreach (var approvalsList in OBJ_PROJECT_HDR.APPROVALS_ABBR_LIST) // Assuming SUBTASK_LIST is a list of subtasks
                            {
                                if ((approvalsList.STATUS).ToString().ToLower() == "Created".ToString().ToLower())
                                {
                                    var ProojectApproval = await db.QueryAsync<bool>("SP_UPDATE_PROJECT_DEFINATION", parameters, commandType: CommandType.StoredProcedure, transaction: transaction);

                                    approvalsInsertDataTable.Rows.Add(pROJECT_HDR.MKEY, approvalsList.TASK_NO
                                        , approvalsList.APPROVAL_ABBRIVATION
                                    , approvalsList.APPROVAL_DESCRIPTION, approvalsList.DAYS_REQUIRED, approvalsList.DEPARTMENT, approvalsList.JOB_ROLE
                                    , approvalsList.RESPOSIBLE_EMP_MKEY, approvalsList.OUTPUT_DOCUMENT,
                                     string.IsNullOrEmpty(approvalsList.TENTATIVE_START_DATE) ? DBNull.Value
                                                : (object)DateTime.Parse(approvalsList.TENTATIVE_START_DATE)
                                    , string.IsNullOrEmpty(approvalsList.TENTATIVE_END_DATE)
                                           ? DBNull.Value
                                           : (object)DateTime.Parse(approvalsList.TENTATIVE_END_DATE)
                                    , approvalsList.STATUS
                                    , approvalsList.APPROVAL_MKEY, OBJ_PROJECT_HDR.CREATED_BY
                                    , dateTime.ToString("yyyy/MM/dd hh:mm:ss"), OBJ_PROJECT_HDR.CREATED_BY, 'N');
                                }
                            }
                            if (approvalsInsertDataTable.Rows.Count > 0)
                            {
                                // Use SqlBulkCopy to insert subtasks
                                using var bulkCopy = new SqlBulkCopy(sqlConnection, SqlBulkCopyOptions.Default, (SqlTransaction)transaction)
                                {
                                    DestinationTableName = "PROJECT_TRL_APPROVAL_ABBR"  // Ensure this matches your table name
                                };

                                bulkCopy.ColumnMappings.Add("HEADER_MKEY", "HEADER_MKEY");
                                bulkCopy.ColumnMappings.Add("SEQ_NO", "SEQ_NO");
                                bulkCopy.ColumnMappings.Add("APPROVAL_MKEY", "APPROVAL_MKEY");
                                //bulkCopy.ColumnMappings.Add("TASK_NO_MKEY", "TASK_NO_MKEY");
                                bulkCopy.ColumnMappings.Add("APPROVAL_ABBRIVATION", "APPROVAL_ABBRIVATION");
                                bulkCopy.ColumnMappings.Add("APPROVAL_DESCRIPTION", "APPROVAL_DESCRIPTION");
                                bulkCopy.ColumnMappings.Add("DAYS_REQUIRED", "DAYS_REQUIRED");
                                bulkCopy.ColumnMappings.Add("DEPARTMENT", "DEPARTMENT");
                                bulkCopy.ColumnMappings.Add("JOB_ROLE", "JOB_ROLE");
                                bulkCopy.ColumnMappings.Add("RESPOSIBLE_EMP_MKEY", "RESPOSIBLE_EMP_MKEY");
                                bulkCopy.ColumnMappings.Add("OUTPUT_DOCUMENT", "OUTPUT_DOCUMENT");
                                bulkCopy.ColumnMappings.Add("TENTATIVE_START_DATE", "TENTATIVE_START_DATE");
                                bulkCopy.ColumnMappings.Add("TENTATIVE_END_DATE", "TENTATIVE_END_DATE");
                                bulkCopy.ColumnMappings.Add("STATUS", "STATUS");
                                bulkCopy.ColumnMappings.Add("CREATED_BY", "CREATED_BY");
                                bulkCopy.ColumnMappings.Add("CREATION_DATE", "CREATION_DATE");
                                bulkCopy.ColumnMappings.Add("LAST_UPDATED_BY", "LAST_UPDATED_BY");

                                await bulkCopy.WriteToServerAsync(approvalsInsertDataTable);
                                //await transactionApprovals.CommitAsync();
                            }
                        }

                        // IF THE STATUS IS INITIATED THEN GO FOR UPDATE IN THE LIST
                        if (UpdateProjectList != null)
                        {
                            // Update list of subtask that status is Initiated
                            var approvalsUpdateDataTable = new DataTable();
                            approvalsUpdateDataTable.Columns.Add("HEADER_MKEY", typeof(int));
                            approvalsUpdateDataTable.Columns.Add("SEQ_NO", typeof(string));
                            approvalsUpdateDataTable.Columns.Add("APPROVAL_ABBRIVATION", typeof(string));
                            approvalsUpdateDataTable.Columns.Add("APPROVAL_DESCRIPTION", typeof(string));
                            approvalsUpdateDataTable.Columns.Add("DAYS_REQUIRED", typeof(int));
                            approvalsUpdateDataTable.Columns.Add("DEPARTMENT", typeof(int));
                            approvalsUpdateDataTable.Columns.Add("JOB_ROLE", typeof(int));
                            approvalsUpdateDataTable.Columns.Add("RESPOSIBLE_EMP_MKEY", typeof(int));
                            approvalsUpdateDataTable.Columns.Add("OUTPUT_DOCUMENT", typeof(string));
                            approvalsUpdateDataTable.Columns.Add("TENTATIVE_START_DATE", typeof(string));
                            approvalsUpdateDataTable.Columns.Add("TENTATIVE_END_DATE", typeof(string));
                            approvalsUpdateDataTable.Columns.Add("STATUS", typeof(string));
                            approvalsUpdateDataTable.Columns.Add("APPROVAL_MKEY", typeof(int));
                            approvalsUpdateDataTable.Columns.Add("LAST_UPDATED_BY", typeof(int));
                            approvalsUpdateDataTable.Columns.Add("LAST_UPDATE_DATE", typeof(DateTime));
                            approvalsUpdateDataTable.Columns.Add("DELETE_FLAG", typeof(char));

                            foreach (var approvalsList in OBJ_PROJECT_HDR.APPROVALS_ABBR_LIST) // Assuming SUBTASK_LIST is a list of subtasks
                            {
                                if ((approvalsList.STATUS).ToString().ToLower() == "Ready to Initiate".ToString().ToLower())
                                {
                                    approvalsUpdateDataTable.Rows.Add(pROJECT_HDR.MKEY, approvalsList.TASK_NO
                                    , approvalsList.APPROVAL_ABBRIVATION
                                    , approvalsList.APPROVAL_DESCRIPTION, approvalsList.DAYS_REQUIRED, approvalsList.DEPARTMENT
                                    , approvalsList.JOB_ROLE, approvalsList.RESPOSIBLE_EMP_MKEY, approvalsList.OUTPUT_DOCUMENT
                                    , approvalsList.TENTATIVE_START_DATE == null ? null : approvalsList.TENTATIVE_START_DATE
                                    , approvalsList.TENTATIVE_END_DATE == null ? null : approvalsList.TENTATIVE_END_DATE, approvalsList.STATUS
                                    , approvalsList.APPROVAL_MKEY
                                    , OBJ_PROJECT_HDR.CREATED_BY, dateTime.ToString("yyyy/MM/dd hh:mm:ss"), 'N');
                                }
                            }

                            // TO CHECK IF THE STATUS IS INITIATED AND HAVE LIST OF UPDATE
                            if (approvalsUpdateDataTable.Rows.Count > 0)
                            {
                                //using var connection = new SqlConnection(_connectionString);
                                //await connection.OpenAsync();
                                //using var transactionApprovals = connection.BeginTransaction();

                                foreach (DataRow row in approvalsUpdateDataTable.Rows)
                                {
                                    var Updateparameters = new DynamicParameters();
                                    Updateparameters.Add("@APPROVAL_ABBRIVATION", row["APPROVAL_ABBRIVATION"] == DBNull.Value ? null : row["APPROVAL_ABBRIVATION"]);
                                    Updateparameters.Add("@APPROVAL_DESCRIPTION", row["APPROVAL_DESCRIPTION"] == DBNull.Value ? null : row["APPROVAL_DESCRIPTION"]);
                                    Updateparameters.Add("@DAYS_REQUIRED", row["DAYS_REQUIRED"] == DBNull.Value ? null : row["DAYS_REQUIRED"]);
                                    Updateparameters.Add("@DEPARTMENT", row["DEPARTMENT"] == DBNull.Value ? null : row["DEPARTMENT"]);
                                    Updateparameters.Add("@JOB_ROLE", row["JOB_ROLE"] == DBNull.Value ? null : row["JOB_ROLE"]);
                                    Updateparameters.Add("@RESPOSIBLE_EMP_MKEY", row["RESPOSIBLE_EMP_MKEY"] == DBNull.Value ? null : row["RESPOSIBLE_EMP_MKEY"]);
                                    Updateparameters.Add("@OUTPUT_DOCUMENT", row["OUTPUT_DOCUMENT"] == DBNull.Value ? null : row["OUTPUT_DOCUMENT"]);
                                    Updateparameters.Add("@TENTATIVE_START_DATE", row["TENTATIVE_START_DATE"] == DBNull.Value ? (object)null : row["TENTATIVE_START_DATE"]);
                                    Updateparameters.Add("@TENTATIVE_END_DATE", row["TENTATIVE_END_DATE"] == DBNull.Value ? (object)null : row["TENTATIVE_END_DATE"]);
                                    Updateparameters.Add("@STATUS", row["STATUS"] == DBNull.Value ? null : row["STATUS"]);
                                    Updateparameters.Add("@MKEY", row["HEADER_MKEY"] == DBNull.Value ? null : row["HEADER_MKEY"]);
                                    Updateparameters.Add("@TASK_NO", row["SEQ_NO"] == DBNull.Value ? null : row["SEQ_NO"]);
                                    Updateparameters.Add("@APPROVAL_MKEY", row["APPROVAL_MKEY"] == DBNull.Value ? null : row["APPROVAL_MKEY"]);
                                    Updateparameters.Add("@ATTRIBUTE1", "UPDATE Project");
                                    Updateparameters.Add("@ATTRIBUTE2", "UPDATE");
                                    Updateparameters.Add("@LAST_UPDATED_BY", OBJ_PROJECT_HDR.CREATED_BY);  // Ensure this value is non-null

                                    await db.ExecuteAsync("SP_UPDATE_PROJECT_TRL_APPROVAL_ABBR", Updateparameters, commandType: CommandType.StoredProcedure, transaction: transaction);

                                    //var affectedRows = await connection.ExecuteAsync(updateQuery, Updateparameters, transactionApprovals);
                                    //await transactionApprovals.CommitAsync();
                                }
                            }

                        }
                        else
                        {
                            var parametersDeleteABBR = new DynamicParameters();
                            parametersDeleteABBR.Add("@HEADER_MKEY", pROJECT_HDR.MKEY);
                            parametersDeleteABBR.Add("@LAST_UPDATED_BY", pROJECT_HDR.CREATED_BY);
                            await db.ExecuteAsync("SP_DELETE_PROJECT_TRL_APPROVAL_ABBR", parametersDeleteABBR, commandType: CommandType.StoredProcedure, transaction: transaction);
                        }

                    }
                    var sqlTransaction = (SqlTransaction)transaction;
                    await sqlTransaction.CommitAsync();

                    pROJECT_HDR.Status = "OK";
                    pROJECT_HDR.Message = "Update Data";
                    return pROJECT_HDR;
                }

            }
            catch (Exception ex)
            {
                if (transaction != null)
                {
                    var sqlTransaction = (SqlTransaction)transaction;
                    await sqlTransaction.RollbackAsync(); // Rollback transaction if an exception occurs
                }
                pROJECT_HDR.Status = "Error";
                pROJECT_HDR.Message = ex.Message;
                return pROJECT_HDR;
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
        public async Task<IEnumerable<PROJECT_APPROVAL_DETAILS_OUTPUT>> GetApprovalDetails(int LoggedInID, int BUILDING_TYPE, string BUILDING_STANDARD,
            string STATUTORY_AUTHORITY)
        {
            try
            {
                using (IDbConnection db = _dapperDbConnection.CreateConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@BUILDING_TYPE", BUILDING_TYPE);
                    parmeters.Add("@BUILDING_STANDARD", BUILDING_STANDARD);
                    parmeters.Add("@STATUTORY_AUTHORITY", STATUTORY_AUTHORITY);
                    var pROJECT_APPROVAL_ABBR_LIST = await db.QueryAsync<PROJECT_APPROVAL_DETAILS_OUTPUT>("SP_GET_APPROVAL_SUBTASK_TREE_VIEW_LIST", parmeters, commandType: CommandType.StoredProcedure);
                    //var HDRaSYNCDetails = await db.QueryAsync<APPROVAL_TEMPLATE_HDR>("select * from APPROVAL_TEMPLATE_HDR", commandType: CommandType.Text);
                    //var SUBTAsyncASKDetails = await db.QueryAsync<APPROVAL_TEMPLATE_TRL_SUBTASK>("select * from APPROVAL_TEMPLATE_TRL_SUBTASK", commandType: CommandType.Text);
                    // var SUBTAsyncASKDetails = await db.QueryAsync("select * from DOC_TEMPLATE_HDR", commandType: CommandType.Text);
                    return pROJECT_APPROVAL_ABBR_LIST;
                }
            }
            catch //(Exception ex)
            {
                return Enumerable.Empty<PROJECT_APPROVAL_DETAILS_OUTPUT>(); //return ApprovalsKeyValuePairs;
            }

        }

        public async Task<ActionResult<IEnumerable<PROJECT_HDR_NT_OUTPUT>>> CreateProjectDefinationAsyncNT(PROJECT_HDR_INPUT_NT pROJECT_HDR_INPUT_NT)
        {
            DateTime dateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
            IDbTransaction transaction = null;
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
                    transaction = db.BeginTransaction(); // Begin a transaction

                    // var OBJ_PROJECT_HDR = pROJECT_HDR;
                    var parameters = new DynamicParameters();
                    parameters.Add("@BUILDING_MKEY", pROJECT_HDR_INPUT_NT.PROJECT_NAME);
                    parameters.Add("@PROJECT_ABBR", pROJECT_HDR_INPUT_NT.PROJECT_ABBR);
                    parameters.Add("@PROPERTY", pROJECT_HDR_INPUT_NT.PROPERTY);
                    parameters.Add("@LEGAL_ENTITY", pROJECT_HDR_INPUT_NT.LEGAL_ENTITY);
                    parameters.Add("@PROJECT_ADDRESS", pROJECT_HDR_INPUT_NT.PROJECT_ADDRESS);
                    parameters.Add("@BUILDING_CLASSIFICATION", pROJECT_HDR_INPUT_NT.BUILDING_CLASSIFICATION);
                    parameters.Add("@BUILDING_STANDARD", pROJECT_HDR_INPUT_NT.BUILDING_STANDARD);
                    parameters.Add("@STATUTORY_AUTHORITY", pROJECT_HDR_INPUT_NT.STATUTORY_AUTHORITY);
                    parameters.Add("@CREATED_BY", pROJECT_HDR_INPUT_NT.CREATED_BY);
                    parameters.Add("@LAST_UPDATED_BY", pROJECT_HDR_INPUT_NT.LAST_UPDATED_BY);
                    parameters.Add("@ATTRIBUTE1", pROJECT_HDR_INPUT_NT.ATTRIBUTE1);
                    parameters.Add("@ATTRIBUTE2", pROJECT_HDR_INPUT_NT.ATTRIBUTE2);
                    parameters.Add("@ATTRIBUTE3", pROJECT_HDR_INPUT_NT.ATTRIBUTE3);

                    // Insert project definition
                    var OBJ_PROJECT_HDR = await db.QueryAsync<PROJECT_HDR_NT>("SP_INSERT_PROJECT_DEFINATION", parameters, commandType: CommandType.StoredProcedure, transaction: transaction);

                    if (OBJ_PROJECT_HDR.Any())
                    {
                        // Create a DataTable for bulk insert of approval data
                        var approvalsDataTable = new DataTable();
                        approvalsDataTable.Columns.Add("HEADER_MKEY", typeof(int));
                        approvalsDataTable.Columns.Add("SEQ_NO", typeof(string));
                        approvalsDataTable.Columns.Add("APPROVAL_MKEY", typeof(int));
                        approvalsDataTable.Columns.Add("APPROVAL_ABBRIVATION", typeof(string));
                        approvalsDataTable.Columns.Add("APPROVAL_DESCRIPTION", typeof(string));
                        approvalsDataTable.Columns.Add("DAYS_REQUIRED", typeof(int));
                        approvalsDataTable.Columns.Add("RESPOSIBLE_EMP_MKEY", typeof(int));
                        approvalsDataTable.Columns.Add("TENTATIVE_START_DATE", typeof(string));
                        approvalsDataTable.Columns.Add("TENTATIVE_END_DATE", typeof(string));
                        approvalsDataTable.Columns.Add("DEPARTMENT", typeof(int));
                        approvalsDataTable.Columns.Add("JOB_ROLE", typeof(int));
                        approvalsDataTable.Columns.Add("OUTPUT_DOCUMENT", typeof(string));
                        approvalsDataTable.Columns.Add("STATUS", typeof(string));
                        approvalsDataTable.Columns.Add("CREATED_BY", typeof(int));
                        approvalsDataTable.Columns.Add("CREATION_DATE", typeof(DateTime));

                        foreach (var approvalsList in pROJECT_HDR_INPUT_NT.APPROVALS_ABBR_LIST)
                        {
                            var RESPOSIBLE_EMP_MKEY = approvalsList.RESPOSIBLE_EMP_MKEY ?? 0;
                            var row = approvalsDataTable.NewRow();
                            row["HEADER_MKEY"] = OBJ_PROJECT_HDR.Select(x => x.MKEY).ToString();
                            row["SEQ_NO"] = approvalsList.TASK_NO;
                            row["APPROVAL_MKEY"] = approvalsList.APPROVAL_MKEY;
                            row["APPROVAL_ABBRIVATION"] = approvalsList.APPROVAL_ABBRIVATION;
                            row["APPROVAL_DESCRIPTION"] = approvalsList.APPROVAL_DESCRIPTION;
                            row["DAYS_REQUIRED"] = approvalsList.DAYS_REQUIRED;
                            row["RESPOSIBLE_EMP_MKEY"] = RESPOSIBLE_EMP_MKEY;
                            row["TENTATIVE_START_DATE"] = string.IsNullOrEmpty(approvalsList.TENTATIVE_START_DATE)
                            ? DBNull.Value
                            : (object)DateTime.Parse(approvalsList.TENTATIVE_START_DATE);
                            row["TENTATIVE_END_DATE"] = string.IsNullOrEmpty(approvalsList.TENTATIVE_END_DATE)
                                   ? DBNull.Value
                                   : (object)DateTime.Parse(approvalsList.TENTATIVE_END_DATE);
                            row["DEPARTMENT"] = approvalsList.DEPARTMENT;
                            row["JOB_ROLE"] = approvalsList.JOB_ROLE;
                            row["OUTPUT_DOCUMENT"] = approvalsList.OUTPUT_DOCUMENT;
                            row["STATUS"] = "Ready to Initiate";//approvalsList.STATUS;
                            row["CREATED_BY"] = pROJECT_HDR_INPUT_NT.CREATED_BY;
                            row["CREATION_DATE"] = dateTime;

                            approvalsDataTable.Rows.Add(row);
                        }

                        // Use SqlBulkCopy to insert the approval data into the database
                        using var bulkCopy = new SqlBulkCopy((SqlConnection)db, SqlBulkCopyOptions.Default, (SqlTransaction)transaction)
                        {
                            DestinationTableName = "PROJECT_TRL_APPROVAL_ABBR"
                        };

                        // Map columns from DataTable to database columns
                        bulkCopy.ColumnMappings.Add("HEADER_MKEY", "HEADER_MKEY");
                        bulkCopy.ColumnMappings.Add("SEQ_NO", "SEQ_NO");
                        bulkCopy.ColumnMappings.Add("APPROVAL_MKEY", "APPROVAL_MKEY");
                        bulkCopy.ColumnMappings.Add("APPROVAL_ABBRIVATION", "APPROVAL_ABBRIVATION");
                        bulkCopy.ColumnMappings.Add("APPROVAL_DESCRIPTION", "APPROVAL_DESCRIPTION");
                        bulkCopy.ColumnMappings.Add("DAYS_REQUIRED", "DAYS_REQUIRED");
                        bulkCopy.ColumnMappings.Add("RESPOSIBLE_EMP_MKEY", "RESPOSIBLE_EMP_MKEY");
                        bulkCopy.ColumnMappings.Add("TENTATIVE_START_DATE", "TENTATIVE_START_DATE");
                        bulkCopy.ColumnMappings.Add("TENTATIVE_END_DATE", "TENTATIVE_END_DATE");
                        bulkCopy.ColumnMappings.Add("DEPARTMENT", "DEPARTMENT");
                        bulkCopy.ColumnMappings.Add("JOB_ROLE", "JOB_ROLE");
                        bulkCopy.ColumnMappings.Add("OUTPUT_DOCUMENT", "OUTPUT_DOCUMENT");
                        bulkCopy.ColumnMappings.Add("STATUS", "STATUS");
                        bulkCopy.ColumnMappings.Add("CREATED_BY", "CREATED_BY");
                        bulkCopy.ColumnMappings.Add("CREATION_DATE", "CREATION_DATE");

                        await bulkCopy.WriteToServerAsync(approvalsDataTable);
                    }
                    else
                    {
                        var errorResult = new List<PROJECT_HDR_NT_OUTPUT>
                        {
                            new PROJECT_HDR_NT_OUTPUT
                            {
                                Status = "Error",
                                Message = $" Error: " + OBJ_PROJECT_HDR.Select(x => x.Message),
                                Data = null
                            }
                        };
                        return errorResult;
                    }

                    // Commit both transactions
                    var sqlTransaction = (SqlTransaction)transaction;
                    await sqlTransaction.CommitAsync();

                    var SuccessResult = new List<PROJECT_HDR_NT_OUTPUT>
                        {
                            new PROJECT_HDR_NT_OUTPUT
                            {
                                Status = "Ok",
                                Message = $" Insert Successfuly",
                                Data = OBJ_PROJECT_HDR
                            }
                        };
                    return SuccessResult;

                }
            }
            catch (Exception ex)
            {
                if (transaction != null)
                {
                    var sqlTransaction = (SqlTransaction)transaction;
                    await sqlTransaction.RollbackAsync(); // Rollback transaction if an exception occurs
                }

                var errorResult = new List<PROJECT_HDR_NT_OUTPUT>
                {
                    new PROJECT_HDR_NT_OUTPUT
                    {
                        Status = "Error",
                        Message = $" Error: " + ex.Message,
                        Data = null
                    }
                };
                return errorResult;
            }
        }
    }
}
