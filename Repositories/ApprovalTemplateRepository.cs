using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Owin.Security.Provider;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Transactions;
using System.Xml.Linq;
using TaskManagement.API.DapperDbConnections;
using TaskManagement.API.Interfaces;
using TaskManagement.API.Model;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace TaskManagement.API.Repositories
{
    public class ApprovalTemplateRepository : IApprovalTemplate
    {
        private static TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        public IDapperDbConnection _dapperDbConnection;
        private readonly string _connectionString;
        public ApprovalTemplateRepository(IDapperDbConnection dapperDbConnection, string connectionString)
        {
            _dapperDbConnection = dapperDbConnection;
            _connectionString = connectionString;
        }
        public async Task<IEnumerable<APPROVAL_TEMPLATE_HDR>> GetAllApprovalTemplateAsync(int LoggedIN)
        {
            int strMKEY;
            try
            {
                using (IDbConnection db = _dapperDbConnection.CreateConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@MKEY", null);
                    parmeters.Add("@ATTRIBUTE1", LoggedIN.ToString());
                    var approvalTemplates = await db.QueryAsync<APPROVAL_TEMPLATE_HDR>("SP_GET_APPROVAL_TEMPLATE", parmeters, commandType: CommandType.StoredProcedure);
                    //var approvalTemplates1 = await db.QueryAsync<APPROVAL_TEMPLATE_HDR>("select MKEY, DOCUMENT_NAME, count(DOCUMENT_NAME) from APPROVAL_TEMPLATE_TRL_ENDRESULT group by MKEY,DOCUMENT_NAME having count(DOCUMENT_NAME) > 1;", CommandType.Text);

                    if (approvalTemplates == null || !approvalTemplates.Any())
                    {
                        var approvalTemplate = new APPROVAL_TEMPLATE_HDR();
                        approvalTemplate.Status = "Error";
                        approvalTemplate.Message = "Not Found";
                        return new List<APPROVAL_TEMPLATE_HDR> { approvalTemplate };
                    }

                    // Iterate over each approval template header to populate subtasks, end result docs, and checklist docs
                    foreach (var approvalTemplate in approvalTemplates)
                    {
                        strMKEY = approvalTemplate.MKEY;
                        // Fetch the associated subtasks
                        var subtasks = await db.QueryAsync<APPROVAL_TEMPLATE_TRL_SUBTASK>(
                            "SELECT * FROM APPROVAL_TEMPLATE_TRL_SUBTASK WHERE HEADER_MKEY = @HEADER_MKEY AND DELETE_FLAG = 'N';",
                            new { HEADER_MKEY = approvalTemplate.MKEY });

                        approvalTemplate.SUBTASK_LIST = subtasks.ToList(); // Populate the SUBTASK_LIST property


                        string sql = "SELECT DOCUMENT_NAME, DOCUMENT_CATEGORY FROM APPROVAL_TEMPLATE_TRL_ENDRESULT WHERE MKEY = @MKEY AND DELETE_FLAG = 'N'; ";
                        var keyValuePairs = await db.QueryAsync(sql, new { MKEY = approvalTemplate.MKEY });

                        // Initialize the END_RESULT_DOC_LST dictionary
                        approvalTemplate.END_RESULT_DOC_LST = new Dictionary<string, object>();

                        // Populate the dictionary with the key-value pairs
                        foreach (var item in keyValuePairs)
                        {
                            // Assuming DOCUMENT_NAME is the key and DOCUMENT_CATEGORY is the value
                            approvalTemplate.END_RESULT_DOC_LST.Add(item.DOCUMENT_NAME.ToString(), item.DOCUMENT_CATEGORY);
                        }

                        sql = "SELECT DOCUMENT_NAME, DOCUMENT_CATEGORY FROM APPROVAL_TEMPLATE_TRL_CHECKLIST WHERE MKEY = @MKEY AND DELETE_FLAG = 'N';";
                        var keyValuePairsCheckList = await db.QueryAsync(sql, new { MKEY = approvalTemplate.MKEY });

                        // Initialize the END_RESULT_DOC_LST dictionary
                        approvalTemplate.CHECKLIST_DOC_LST = new Dictionary<string, object>();

                        // Populate the dictionary with the key-value pairs
                        foreach (var item in keyValuePairsCheckList)
                        {
                            // Assuming DOCUMENT_NAME is the key and DOCUMENT_CATEGORY is the value
                            approvalTemplate.CHECKLIST_DOC_LST.Add(item.DOCUMENT_NAME.ToString(), item.DOCUMENT_CATEGORY);
                        }

                        strMKEY = approvalTemplate.MKEY;
                        // Fetch the associated subtasks
                        var Sanctioning_Department = await db.QueryAsync<APPROVAL_TEMPLATE_TRL_SANCTIONING_DEPARTMENT>(
                            "SELECT * FROM V_APPROVAL_TEMPLATE_TRL_SANCTIONING_DEPARTMENT WHERE MKEY = @MKEY AND DELETE_FLAG = 'N';",
                            new { MKEY = approvalTemplate.MKEY });

                        approvalTemplate.SANCTIONING_DEPARTMENT_LIST = Sanctioning_Department.ToList();

                        approvalTemplate.Status = "OK";
                        approvalTemplate.Message = "Get data successfully";
                    }
                    return approvalTemplates;
                }
            }
            catch (Exception ex)
            {

                // Handle other unexpected exceptions
                var approvalTemplate = new APPROVAL_TEMPLATE_HDR();
                approvalTemplate.Status = "Error";
                approvalTemplate.Message = ex.Message;
                return new List<APPROVAL_TEMPLATE_HDR> { approvalTemplate };
            }
        }
        public async Task<APPROVAL_TEMPLATE_HDR> GetApprovalTemplateByIdAsync(int id, int LoggedIN)
        {
            try
            {
                using (IDbConnection db = _dapperDbConnection.CreateConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@MKEY", id);
                    parmeters.Add("@ATTRIBUTE1", LoggedIN);
                    var approvalTemplate = await db.QueryFirstOrDefaultAsync<APPROVAL_TEMPLATE_HDR>("SP_GET_APPROVAL_TEMPLATE", parmeters, commandType: CommandType.StoredProcedure);

                    if (approvalTemplate == null)
                    {
                        var ErrorTemplate = new APPROVAL_TEMPLATE_HDR();
                        ErrorTemplate.Status = "Error";
                        ErrorTemplate.Message = "Not Found";
                        return ErrorTemplate;
                    }


                    // Fetch the associated subtasks
                    // Fetch the associated Subtasks
                    var subtasks = await db.QueryAsync<APPROVAL_TEMPLATE_TRL_SUBTASK>(
                        "SELECT * FROM APPROVAL_TEMPLATE_TRL_SUBTASK WHERE HEADER_MKEY = @HEADER_MKEY AND DELETE_FLAG = 'N';",
                        new { HEADER_MKEY = approvalTemplate.MKEY });

                    approvalTemplate.SUBTASK_LIST = subtasks.ToList(); // Populate the SUBTASK_LIST property with subtasks

                    string sql = "SELECT DOCUMENT_NAME, DOCUMENT_CATEGORY FROM APPROVAL_TEMPLATE_TRL_ENDRESULT WHERE MKEY = @MKEY AND DELETE_FLAG = 'N';";
                    var keyValuePairs = await db.QueryAsync(sql, new { MKEY = approvalTemplate.MKEY });

                    // Initialize the END_RESULT_DOC_LST dictionary
                    approvalTemplate.END_RESULT_DOC_LST = new Dictionary<string, object>();

                    // Populate the dictionary with the key-value pairs
                    foreach (var item in keyValuePairs)
                    {
                        // Assuming DOCUMENT_NAME is the key and DOCUMENT_CATEGORY is the value
                        approvalTemplate.END_RESULT_DOC_LST.Add(item.DOCUMENT_NAME.ToString(), item.DOCUMENT_CATEGORY);
                    }

                    sql = "SELECT DOCUMENT_NAME, DOCUMENT_CATEGORY FROM APPROVAL_TEMPLATE_TRL_CHECKLIST WHERE MKEY = @MKEY AND DELETE_FLAG = 'N';";
                    var keyValuePairsCheckList = await db.QueryAsync(sql, new { MKEY = approvalTemplate.MKEY });

                    // Initialize the END_RESULT_DOC_LST dictionary
                    approvalTemplate.CHECKLIST_DOC_LST = new Dictionary<string, object>();

                    // Populate the dictionary with the key-value pairs
                    foreach (var item in keyValuePairsCheckList)
                    {
                        // Assuming DOCUMENT_NAME is the key and DOCUMENT_CATEGORY is the value
                        approvalTemplate.CHECKLIST_DOC_LST.Add(item.DOCUMENT_NAME.ToString(), item.DOCUMENT_CATEGORY);
                    }
                    var strMKEY = approvalTemplate.MKEY;
                    // Fetch the associated subtasks
                    var Sanctioning_Department = await db.QueryAsync<APPROVAL_TEMPLATE_TRL_SANCTIONING_DEPARTMENT>(
                        "SELECT * FROM V_APPROVAL_TEMPLATE_TRL_SANCTIONING_DEPARTMENT WHERE MKEY = @MKEY AND DELETE_FLAG = 'N';",
                        new { MKEY = approvalTemplate.MKEY });

                    approvalTemplate.SANCTIONING_DEPARTMENT_LIST = Sanctioning_Department.ToList();
                    return approvalTemplate;
                }
            }
            catch (Exception ex)
            {
                // Handle other unexpected exceptions
                var approvalTemplate = new APPROVAL_TEMPLATE_HDR();
                approvalTemplate.Status = "Error";
                approvalTemplate.Message = ex.Message;
                return approvalTemplate;
            }
        }
        public async Task<APPROVAL_TEMPLATE_HDR> CreateApprovalTemplateAsync(APPROVAL_TEMPLATE_HDR aPPROVAL_TEMPLATE_HDR)
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

                    var OBJ_APPROVAL_TEMPLATE_HDR = aPPROVAL_TEMPLATE_HDR;
                    var parameters = new DynamicParameters();
                    parameters.Add("@BUILDING_TYPE", aPPROVAL_TEMPLATE_HDR.BUILDING_TYPE);
                    parameters.Add("@BUILDING_STANDARD", aPPROVAL_TEMPLATE_HDR.BUILDING_STANDARD);
                    parameters.Add("@STATUTORY_AUTHORITY", aPPROVAL_TEMPLATE_HDR.STATUTORY_AUTHORITY);
                    parameters.Add("@SHORT_DESCRIPTION", aPPROVAL_TEMPLATE_HDR.SHORT_DESCRIPTION);
                    parameters.Add("@LONG_DESCRIPTION", aPPROVAL_TEMPLATE_HDR.LONG_DESCRIPTION);
                    parameters.Add("@ABBR", aPPROVAL_TEMPLATE_HDR.MAIN_ABBR);
                    parameters.Add("@APPROVAL_DEPARTMENT", aPPROVAL_TEMPLATE_HDR.AUTHORITY_DEPARTMENT);
                    parameters.Add("@RESPOSIBLE_EMP_MKEY", aPPROVAL_TEMPLATE_HDR.RESPOSIBLE_EMP_MKEY);
                    parameters.Add("@JOB_ROLE", aPPROVAL_TEMPLATE_HDR.JOB_ROLE);
                    parameters.Add("@NO_DAYS_REQUIRED", aPPROVAL_TEMPLATE_HDR.DAYS_REQUIERD);
                    parameters.Add("@SANCTION_AUTHORITY", aPPROVAL_TEMPLATE_HDR.SANCTION_AUTHORITY);
                    parameters.Add("@SANCTION_DEPARTMENT", aPPROVAL_TEMPLATE_HDR.SANCTION_DEPARTMENT);
                    parameters.Add("@END_RESULT_DOC", aPPROVAL_TEMPLATE_HDR.END_RESULT_DOC);
                    parameters.Add("@CHECKLIST_DOC", aPPROVAL_TEMPLATE_HDR.CHECKLIST_DOC);
                    parameters.Add("@TAGS", aPPROVAL_TEMPLATE_HDR.TAGS);
                    parameters.Add("@ATTRIBUTE1", aPPROVAL_TEMPLATE_HDR.ATTRIBUTE1);
                    parameters.Add("@ATTRIBUTE2", aPPROVAL_TEMPLATE_HDR.ATTRIBUTE2);
                    parameters.Add("@ATTRIBUTE3", aPPROVAL_TEMPLATE_HDR.ATTRIBUTE3);
                    parameters.Add("@ATTRIBUTE4", aPPROVAL_TEMPLATE_HDR.ATTRIBUTE4);
                    parameters.Add("@ATTRIBUTE5", aPPROVAL_TEMPLATE_HDR.ATTRIBUTE5);
                    parameters.Add("@CREATED_BY", aPPROVAL_TEMPLATE_HDR.CREATED_BY);
                    parameters.Add("@LAST_UPDATED_BY", aPPROVAL_TEMPLATE_HDR.LAST_UPDATED_BY);
                    parameters.Add("@TAGS", aPPROVAL_TEMPLATE_HDR.TAGS);
                    aPPROVAL_TEMPLATE_HDR = await db.QueryFirstOrDefaultAsync<APPROVAL_TEMPLATE_HDR>("SP_INSERT_APPROVAL_TEMPLATE", parameters, commandType: CommandType.StoredProcedure, transaction: transaction);

                    DateTime dateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);

                    //using var connection = new SqlConnection(_connectionString);
                    //await connection.OpenAsync();

                    // Use BeginTransaction() (synchronously) to get a SqlTransaction END_RESULT_DOC_LST
                    //using var transaction = connection.BeginTransaction();
                    try
                    {
                        //var sqlTransaction = transaction as SqlTransaction;

                        //if (sqlTransaction == null)
                        //{
                        //    throw new InvalidOperationException("Transaction is not of type SqlTransaction.");
                        //}

                        // Create a DataTable for bulk insert END_RESULT_DOC_LST
                        var dataTable = new DataTable();
                        dataTable.Columns.Add("MKEY", typeof(int));
                        dataTable.Columns.Add("SR_NO", typeof(int));
                        dataTable.Columns.Add("DOCUMENT_NAME", typeof(string));
                        dataTable.Columns.Add("DOCUMENT_CATEGORY", typeof(string));
                        dataTable.Columns.Add("ATTRIBUTE1", typeof(string));
                        dataTable.Columns.Add("ATTRIBUTE2", typeof(string));
                        dataTable.Columns.Add("ATTRIBUTE3", typeof(string));
                        dataTable.Columns.Add("ATTRIBUTE4", typeof(string));
                        dataTable.Columns.Add("ATTRIBUTE5", typeof(string));
                        dataTable.Columns.Add("CREATED_BY", typeof(int));
                        dataTable.Columns.Add("CREATION_DATE", typeof(DateTime));
                        dataTable.Columns.Add("LAST_UPDATED_BY", typeof(int));
                        dataTable.Columns.Add("LAST_UPDATE_DATE", typeof(DateTime));
                        dataTable.Columns.Add("DELETE_FLAG", typeof(char));

                        if (OBJ_APPROVAL_TEMPLATE_HDR.END_RESULT_DOC_LST != null)
                        {
                            var SR_No = await db.QuerySingleAsync<int>("SELECT isnull(max(t.SR_NO),0) + 1 FROM APPROVAL_TEMPLATE_TRL_ENDRESULT t WHERE MKEY = @MKEY AND DELETE_FLAG = 'N';", new { MKEY = aPPROVAL_TEMPLATE_HDR.MKEY }, commandType: CommandType.Text, transaction: transaction);


                            // Populate the DataTable with product data
                            foreach (var END_DOC_LIST in OBJ_APPROVAL_TEMPLATE_HDR.END_RESULT_DOC_LST)
                            {
                                dataTable.Rows.Add(aPPROVAL_TEMPLATE_HDR.MKEY, SR_No, END_DOC_LIST.Key, END_DOC_LIST.Value, null, null, null, null, null, aPPROVAL_TEMPLATE_HDR.CREATED_BY, dateTime.ToString("yyyy/MM/dd hh:mm:ss"), aPPROVAL_TEMPLATE_HDR.LAST_UPDATED_BY, dateTime.ToString("yyyy/MM/dd hh:mm:ss"), 'N');
                                SR_No = SR_No + 1;
                            }
                            SR_No = 0;

                            // Use SqlBulkCopy for bulk insert
                            using var bulkCopy = new SqlBulkCopy(sqlConnection, SqlBulkCopyOptions.Default, (SqlTransaction)transaction)
                            {
                                DestinationTableName = "APPROVAL_TEMPLATE_TRL_ENDRESULT"
                            };

                            // Execute the bulk copy
                            await bulkCopy.WriteToServerAsync(dataTable);

                            // Commit transaction
                            //await transaction.CommitAsync();

                            /*
                             * TO GET INSERTED VALUE IN END RESULT
                             * */
                            // Query the APPROVAL_TEMPLATE_TRL_CHECKLIST for key-value pairs
                            string sql = "SELECT DOCUMENT_NAME, DOCUMENT_CATEGORY FROM APPROVAL_TEMPLATE_TRL_ENDRESULT WHERE MKEY = @MKEY AND DELETE_FLAG = 'N';";
                            var keyValuePairs = await db.QueryAsync(sql, new { MKEY = aPPROVAL_TEMPLATE_HDR.MKEY }, transaction: transaction);

                            // Initialize the END_RESULT_DOC_LST dictionary
                            aPPROVAL_TEMPLATE_HDR.END_RESULT_DOC_LST = new Dictionary<string, object>();

                            // Populate the dictionary with the key-value pairs
                            foreach (var item in keyValuePairs)
                            {
                                // Assuming DOCUMENT_NAME is the key and DOCUMENT_CATEGORY is the value
                                aPPROVAL_TEMPLATE_HDR.END_RESULT_DOC_LST.Add(item.DOCUMENT_NAME.ToString(), item.DOCUMENT_CATEGORY);
                            }
                        }
                        /*-------------------------------------------------------------------------------------------------------------------
                        TO INSERT END RESULT LIST
                        */


                        //-------------------------------------------------------------------------------------------------------------------
                        /*-------------------------------------------------------------------------------------------------------------------
                       TO INSERT CHECK LIST
                       */
                        // Populate the DataTable with product data
                        dataTable.Rows.Clear();
                        //using var transactionCheckList = connection.BeginTransaction();

                        if (OBJ_APPROVAL_TEMPLATE_HDR.CHECKLIST_DOC_LST != null)
                        {
                            var SR_No = await db.QuerySingleAsync<int>("SELECT isnull(max(t.SR_NO),0) + 1 FROM APPROVAL_TEMPLATE_TRL_CHECKLIST t " +
                                "WHERE MKEY = @MKEY AND DELETE_FLAG = 'N'", new { MKEY = OBJ_APPROVAL_TEMPLATE_HDR.MKEY }, commandType: CommandType.Text, transaction: transaction);

                            foreach (var CHECK_LIST in OBJ_APPROVAL_TEMPLATE_HDR.CHECKLIST_DOC_LST)
                            {
                                dataTable.Rows.Add(aPPROVAL_TEMPLATE_HDR.MKEY, SR_No, CHECK_LIST.Key, CHECK_LIST.Value, null, null, null, null, null,
                                    aPPROVAL_TEMPLATE_HDR.CREATED_BY, dateTime.ToString("yyyy/MM/dd hh:mm:ss"), aPPROVAL_TEMPLATE_HDR.LAST_UPDATED_BY
                                    , dateTime.ToString("yyyy/MM/dd hh:mm:ss"), 'N');
                                SR_No = SR_No + 1;
                            }
                            SR_No = 0;

                            // Use SqlBulkCopy for bulk insert
                            using var bulkCopyCheckList = new SqlBulkCopy(sqlConnection, SqlBulkCopyOptions.Default, (SqlTransaction)transaction)
                            {
                                DestinationTableName = "APPROVAL_TEMPLATE_TRL_CHECKLIST"
                            };

                            // Execute the bulk copy
                            await bulkCopyCheckList.WriteToServerAsync(dataTable);

                            // Commit transaction
                            //await transactionCheckList.CommitAsync();


                            // Query the APPROVAL_TEMPLATE_TRL_CHECKLIST for key-value pairs
                            var sql = "SELECT DOCUMENT_NAME, DOCUMENT_CATEGORY FROM APPROVAL_TEMPLATE_TRL_CHECKLIST WHERE MKEY = @MKEY AND DELETE_FLAG = 'N';";
                            var keyValuePairs = await db.QueryAsync(sql, new { MKEY = aPPROVAL_TEMPLATE_HDR.MKEY }, transaction: transaction);

                            // Initialize the END_RESULT_DOC_LST dictionary
                            aPPROVAL_TEMPLATE_HDR.CHECKLIST_DOC_LST = new Dictionary<string, object>();

                            // Populate the dictionary with the key-value pairs
                            foreach (var item in keyValuePairs)
                            {
                                // Assuming DOCUMENT_NAME is the key and DOCUMENT_CATEGORY is the value
                                aPPROVAL_TEMPLATE_HDR.CHECKLIST_DOC_LST.Add(item.DOCUMENT_NAME.ToString(), item.DOCUMENT_CATEGORY);
                            }
                        }

                        //------------------------------------------------------------------------------------------------------------------------------
                    }
                    catch
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
                    }

                    // using var transactionSubTask = connection.BeginTransaction();
                    try
                    {
                        // Create a DataTable for bulk insert of subtasks (SEQNO, SRNO, ABBR)
                        var subtaskDataTable = new DataTable();
                        subtaskDataTable.Columns.Add("HEADER_MKEY", typeof(int));
                        subtaskDataTable.Columns.Add("SEQ_NO", typeof(string));  // task_no
                        subtaskDataTable.Columns.Add("SUBTASK_ABBR", typeof(string));
                        subtaskDataTable.Columns.Add("SUBTASK_MKEY", typeof(int));
                        subtaskDataTable.Columns.Add("SUBTASK_PARENT_ID", typeof(int));
                        subtaskDataTable.Columns.Add("ATTRIBUTE1", typeof(string));
                        subtaskDataTable.Columns.Add("ATTRIBUTE2", typeof(string));
                        subtaskDataTable.Columns.Add("ATTRIBUTE3", typeof(string));
                        subtaskDataTable.Columns.Add("ATTRIBUTE4", typeof(string));
                        subtaskDataTable.Columns.Add("ATTRIBUTE5", typeof(string));
                        subtaskDataTable.Columns.Add("ATTRIBUTE6", typeof(string));
                        subtaskDataTable.Columns.Add("ATTRIBUTE7", typeof(string));
                        subtaskDataTable.Columns.Add("ATTRIBUTE8", typeof(string));
                        subtaskDataTable.Columns.Add("CREATED_BY", typeof(int));
                        subtaskDataTable.Columns.Add("CREATION_DATE", typeof(DateTime));
                        subtaskDataTable.Columns.Add("LAST_UPDATED_BY", typeof(int));
                        subtaskDataTable.Columns.Add("LAST_UPDATE_DATE", typeof(DateTime));
                        subtaskDataTable.Columns.Add("DELETE_FLAG", typeof(char));
                        bool flagID = false;
                        if (OBJ_APPROVAL_TEMPLATE_HDR.SUBTASK_LIST != null && OBJ_APPROVAL_TEMPLATE_HDR.SUBTASK_LIST.Count > 0)
                        {
                            // Populate the DataTable with subtasks
                            foreach (var subtask in OBJ_APPROVAL_TEMPLATE_HDR.SUBTASK_LIST) // Assuming SUBTASK_LIST is a list of subtasks
                            {
                                subtaskDataTable.Rows.Add(aPPROVAL_TEMPLATE_HDR.MKEY, subtask.SEQ_NO, subtask.SUBTASK_ABBR, subtask.SUBTASK_MKEY
                                    , aPPROVAL_TEMPLATE_HDR.MKEY, null, null, null, null, null, null, null, null, OBJ_APPROVAL_TEMPLATE_HDR.CREATED_BY
                                    , dateTime.ToString("yyyy/MM/dd hh:mm:ss"), OBJ_APPROVAL_TEMPLATE_HDR.CREATED_BY, dateTime.ToString("yyyy/MM/dd hh:mm:ss"), 'N');
                            }

                            // Use SqlBulkCopy to insert subtasks
                            using var bulkCopy = new SqlBulkCopy(sqlConnection, SqlBulkCopyOptions.Default, (SqlTransaction)transaction)
                            {
                                DestinationTableName = "APPROVAL_TEMPLATE_TRL_SUBTASK"  // Ensure this matches your table name
                            };

                            await bulkCopy.WriteToServerAsync(subtaskDataTable);

                            // Commit the transactionSubTask
                            //await transactionSubTask.CommitAsync();

                            // Optionally, fetch the inserted values (if necessary)
                            string sql = "SELECT HEADER_MKEY,SEQ_NO,SUBTASK_MKEY,SUBTASK_ABBR FROM APPROVAL_TEMPLATE_TRL_SUBTASK WHERE HEADER_MKEY = @HEADER_MKEY AND DELETE_FLAG = 'N';";
                            var subtaskKeyValuePairs = await db.QueryAsync(sql, new { HEADER_MKEY = aPPROVAL_TEMPLATE_HDR.MKEY });

                            // Assuming the model has a SUBTASK_LIST dictionary to hold these values
                            aPPROVAL_TEMPLATE_HDR.SUBTASK_LIST = new List<APPROVAL_TEMPLATE_TRL_SUBTASK>();  // Assuming Subtask is a class for this data

                            foreach (var item in subtaskKeyValuePairs)
                            {
                                aPPROVAL_TEMPLATE_HDR.SUBTASK_LIST.Add(new APPROVAL_TEMPLATE_TRL_SUBTASK
                                {
                                    HEADER_MKEY = item.HEADER_MKEY,
                                    SEQ_NO = item.SEQ_NO,
                                    SUBTASK_MKEY = item.SUBTASK_MKEY,
                                    SUBTASK_ABBR = item.SUBTASK_ABBR
                                });
                            }
                        }
                        //return aPPROVAL_TEMPLATE_HDR;
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
                    }

                    try
                    {
                        var SanctioningDataTable = new DataTable();
                        SanctioningDataTable.Columns.Add("MKEY", typeof(int));
                        SanctioningDataTable.Columns.Add("SR_NO", typeof(int));
                        SanctioningDataTable.Columns.Add("LEVEL", typeof(string));
                        SanctioningDataTable.Columns.Add("SANCTIONING_DEPARTMENT", typeof(string));
                        SanctioningDataTable.Columns.Add("SANCTIONING_AUTHORITY", typeof(string));
                        SanctioningDataTable.Columns.Add("START_DATE", typeof(DateTime));
                        SanctioningDataTable.Columns.Add("END_DATE", typeof(DateTime));
                        SanctioningDataTable.Columns.Add("CREATED_BY", typeof(int));
                        SanctioningDataTable.Columns.Add("CREATION_DATE", typeof(DateTime));
                        SanctioningDataTable.Columns.Add("DELETE_FLAG", typeof(char));
                        bool flagID = false;
                        if (OBJ_APPROVAL_TEMPLATE_HDR.SANCTIONING_DEPARTMENT_LIST != null)
                        {
                            var SR_No = await db.QuerySingleAsync<int>("SELECT isnull(max(t.SR_NO),0) + 1 FROM APPROVAL_TEMPLATE_TRL_CHECKLIST t " +
                                "WHERE MKEY = @MKEY AND DELETE_FLAG = 'N'", new { MKEY = OBJ_APPROVAL_TEMPLATE_HDR.MKEY }, commandType: CommandType.Text, transaction: transaction);

                            // Populate the DataTable with subtasks
                            foreach (var SANCTIONING_DEPARTMENT in OBJ_APPROVAL_TEMPLATE_HDR.SANCTIONING_DEPARTMENT_LIST) // Assuming SUBTASK_LIST is a list of subtasks
                            {
                                SanctioningDataTable.Rows.Add(aPPROVAL_TEMPLATE_HDR.MKEY, SANCTIONING_DEPARTMENT.SR_NO, SANCTIONING_DEPARTMENT.LEVEL
                                    , SANCTIONING_DEPARTMENT.SANCTIONING_DEPARTMENT, SANCTIONING_DEPARTMENT.SANCTIONING_AUTHORITY
                                    , SANCTIONING_DEPARTMENT.START_DATE, SANCTIONING_DEPARTMENT.END_DATE, SANCTIONING_DEPARTMENT.CREATED_BY
                                    , dateTime.ToString("yyyy/MM/dd hh:mm:ss"), 'N');
                                SR_No = SR_No + 1;
                            }

                            // Use SqlBulkCopy to insert subtasks
                            using var bulkCopy = new SqlBulkCopy(sqlConnection, SqlBulkCopyOptions.Default, (SqlTransaction)transaction)
                            {
                                DestinationTableName = "APPROVAL_TEMPLATE_TRL_SANCTIONING_DEPARTMENT"  // Ensure this matches your table name
                            };

                            bulkCopy.ColumnMappings.Add("MKEY", "MKEY");
                            bulkCopy.ColumnMappings.Add("SR_NO", "SR_NO");
                            bulkCopy.ColumnMappings.Add("LEVEL", "LEVEL");
                            bulkCopy.ColumnMappings.Add("SANCTIONING_DEPARTMENT", "SANCTIONING_DEPARTMENT");
                            bulkCopy.ColumnMappings.Add("SANCTIONING_AUTHORITY", "SANCTIONING_AUTHORITY");
                            bulkCopy.ColumnMappings.Add("START_DATE", "START_DATE");
                            bulkCopy.ColumnMappings.Add("END_DATE", "END_DATE");
                            bulkCopy.ColumnMappings.Add("CREATED_BY", "CREATED_BY");
                            bulkCopy.ColumnMappings.Add("DELETE_FLAG", "DELETE_FLAG");

                            await bulkCopy.WriteToServerAsync(SanctioningDataTable);

                            // Commit the transactionSubTask
                            // await transactionSubTask.CommitAsync();

                            // Optionally, fetch the inserted values (if necessary)
                            string sql = "SELECT * FROM V_APPROVAL_TEMPLATE_TRL_SANCTIONING_DEPARTMENT WHERE MKEY = @MKEY AND DELETE_FLAG = 'N';";
                            var SANCTIONING_DEPARTMENT_TRL = await db.QueryAsync(sql, new { MKEY = aPPROVAL_TEMPLATE_HDR.MKEY }, transaction: transaction);

                            // Assuming the model has a SUBTASK_LIST dictionary to hold these values
                            aPPROVAL_TEMPLATE_HDR.SANCTIONING_DEPARTMENT_LIST = new List<APPROVAL_TEMPLATE_TRL_SANCTIONING_DEPARTMENT>();  // Assuming Subtask is a class for this data

                            foreach (var item in SANCTIONING_DEPARTMENT_TRL)
                            {
                                aPPROVAL_TEMPLATE_HDR.SANCTIONING_DEPARTMENT_LIST.Add(new APPROVAL_TEMPLATE_TRL_SANCTIONING_DEPARTMENT
                                {
                                    MKEY = item.MKEY,
                                    SR_NO = item.SR_NO,
                                    LEVEL = item.LEVEL,
                                    SANCTIONING_DEPARTMENT = item.SANCTIONING_DEPARTMENT,
                                    SANCTIONING_AUTHORITY = item.SANCTIONING_AUTHORITY,
                                    START_DATE = item.START_DATE,
                                    END_DATE = item.END_DATE
                                });
                            }
                        }
                        //var sqlTransaction = (SqlTransaction)transaction;
                        //await sqlTransaction.CommitAsync();
                        //transactionCompleted = true;
                        //return aPPROVAL_TEMPLATE_HDR;
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
                    }
                    var sqlTransaction = (SqlTransaction)transaction;
                    await sqlTransaction.CommitAsync();
                    transactionCompleted = true;
                    return aPPROVAL_TEMPLATE_HDR;
                }
            }
            catch (SqlException ex)
            {
                var Approval_hdr = new APPROVAL_TEMPLATE_HDR
                {
                    Status = "Error",
                    Message = ex.Message
                };
                return Approval_hdr;
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
                return aPPROVAL_TEMPLATE_HDR;
            }
        }
        public async Task<APPROVAL_TEMPLATE_HDR> CheckABBRAsync(string ABBR)
        {
            try
            {
                using (IDbConnection db = _dapperDbConnection.CreateConnection())
                {
                    return await db.QueryFirstOrDefaultAsync<APPROVAL_TEMPLATE_HDR>("SELECT HDR.MKEY,BUILDING_TYPE,BUILDING_STANDARD,STATUTORY_AUTHORITY," +
                        "SHORT_DESCRIPTION,LONG_DESCRIPTION,MAIN_ABBR,AUTHORITY_DEPARTMENT,RESPOSIBLE_EMP_MKEY,JOB_ROLE,DAYS_REQUIERD,HDR.ATTRIBUTE1,HDR.ATTRIBUTE2," +
                        "HDR.ATTRIBUTE3,HDR.ATTRIBUTE4,HDR.ATTRIBUTE5,HDR.CREATED_BY,HDR.CREATION_DATE,HDR.LAST_UPDATED_BY,HDR.LAST_UPDATE_DATE,SANCTION_AUTHORITY," +
                        "SANCTION_DEPARTMENT,END_RESULT_DOC,CHECKLIST_DOC,HDR.DELETE_FLAG  FROM APPROVAL_TEMPLATE_HDR HDR INNER JOIN APPROVAL_TEMPLATE_TRL_SUBTASK TRL_SUB " +
                        "ON HDR.MKEY = TRL_SUB.HEADER_MKEY WHERE LOWER(MAIN_ABBR) = LOWER(@ABBR) OR  LOWER(SUBTASK_ABBR) = LOWER(@ABBR) AND HDR.DELETE_FLAG = 'N'; " +
                        "AND TRL_SUB.DELETE_FLAG = 'N' ", new { ABBR = ABBR });
                }
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        public async Task<IEnumerable<APPROVAL_TEMPLATE_HDR>> AbbrAndShortDescAsync(string strBuilding, string strStandard, string strAuthority)
        {
            try
            {
                using (IDbConnection db = _dapperDbConnection.CreateConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@BUILDING_TYPE", strBuilding);
                    parmeters.Add("@BUILDING_STANDARD", strStandard);
                    parmeters.Add("@STATUTORY_AUTHORITY", strAuthority);
                    var Abbr_List = await db.QueryAsync<APPROVAL_TEMPLATE_HDR>("SP_GET_ABBR_AND_SHORT", parmeters, commandType: CommandType.StoredProcedure);
                    return Abbr_List;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<APPROVAL_TEMPLATE_HDR> UpdateApprovalTemplateAsync(APPROVAL_TEMPLATE_HDR aPPROVAL_TEMPLATE_HDR)
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
                        var approvalTemplate = new APPROVAL_TEMPLATE_HDR();
                        approvalTemplate.Status = "Error";
                        approvalTemplate.Message = "The connection must be a SqlConnection to use OpenAsync.";
                        return approvalTemplate;
                    }

                    if (sqlConnection.State != ConnectionState.Open)
                    {
                        await sqlConnection.OpenAsync();  // Ensure the connection is open
                    }

                    transaction = sqlConnection.BeginTransaction();  // Begin a SqlTransaction
                    transactionCompleted = false;  // Reset transaction state

                    var parameters = new DynamicParameters();
                    parameters.Add("@MKEY", aPPROVAL_TEMPLATE_HDR.MKEY);
                    parameters.Add("@BUILDING_TYPE", aPPROVAL_TEMPLATE_HDR.BUILDING_TYPE);
                    parameters.Add("@BUILDING_STANDARD", aPPROVAL_TEMPLATE_HDR.BUILDING_STANDARD);
                    parameters.Add("@STATUTORY_AUTHORITY", aPPROVAL_TEMPLATE_HDR.STATUTORY_AUTHORITY);
                    parameters.Add("@SHORT_DESCRIPTION", aPPROVAL_TEMPLATE_HDR.SHORT_DESCRIPTION);
                    parameters.Add("@LONG_DESCRIPTION", aPPROVAL_TEMPLATE_HDR.LONG_DESCRIPTION);
                    parameters.Add("@ABBR", aPPROVAL_TEMPLATE_HDR.MAIN_ABBR);
                    parameters.Add("@APPROVAL_DEPARTMENT", aPPROVAL_TEMPLATE_HDR.AUTHORITY_DEPARTMENT);
                    parameters.Add("@RESPOSIBLE_EMP_MKEY", aPPROVAL_TEMPLATE_HDR.RESPOSIBLE_EMP_MKEY);
                    parameters.Add("@JOB_ROLE", aPPROVAL_TEMPLATE_HDR.JOB_ROLE);
                    parameters.Add("@NO_DAYS_REQUIRED", aPPROVAL_TEMPLATE_HDR.DAYS_REQUIERD);
                    parameters.Add("@SANCTION_AUTHORITY", aPPROVAL_TEMPLATE_HDR.SANCTION_AUTHORITY);
                    parameters.Add("@SANCTION_DEPARTMENT", aPPROVAL_TEMPLATE_HDR.SANCTION_DEPARTMENT);
                    //parameters.Add("@CHECKLIST_DOC", aPPROVAL_TEMPLATE_HDR.CHECKLIST_DOC);
                    parameters.Add("@TAGS", aPPROVAL_TEMPLATE_HDR.TAGS);
                    parameters.Add("@ATTRIBUTE1", aPPROVAL_TEMPLATE_HDR.ATTRIBUTE1);
                    parameters.Add("@ATTRIBUTE2", aPPROVAL_TEMPLATE_HDR.ATTRIBUTE2);
                    parameters.Add("@LAST_UPDATED_BY", aPPROVAL_TEMPLATE_HDR.LAST_UPDATED_BY);

                    // Execute stored procedure to insert into APPROVAL_TEMPLATE_HDR
                    var Updated_APPROVAL_TEMPLATE_HDR = await db.QueryFirstOrDefaultAsync<APPROVAL_TEMPLATE_HDR>("SP_UPDATE_APPROVAL_TEMPLATE", parameters, commandType: CommandType.StoredProcedure, transaction: transaction);

                    if (Updated_APPROVAL_TEMPLATE_HDR == null)
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
                            }
                        }

                        var approvalTemplate = new APPROVAL_TEMPLATE_HDR();
                        approvalTemplate.Status = "Error";
                        approvalTemplate.Message = "Failed to insert APPROVAL_TEMPLATE_HDR.";
                        return approvalTemplate;
                    }

                    DateTime dateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);

                    #region Insert END_RESULT_DOC_LST
                    var parametersTRL = new DynamicParameters();
                    parametersTRL.Add("@MKEY", aPPROVAL_TEMPLATE_HDR.MKEY);
                    parametersTRL.Add("@LOGGED_IN", aPPROVAL_TEMPLATE_HDR.CREATED_BY);
                    parametersTRL.Add("@STATUS", aPPROVAL_TEMPLATE_HDR.Status);
                    var DeleteApprovalTrl = await db.QueryFirstOrDefaultAsync<dynamic>("SP_DELETE_APPROVAL_TEMPLATE_TRL", parametersTRL, commandType: CommandType.StoredProcedure, transaction: transaction);


                    if (aPPROVAL_TEMPLATE_HDR.END_RESULT_DOC_LST != null)
                    {
                        var dataTable = new DataTable();
                        dataTable.Columns.Add("MKEY", typeof(int));
                        dataTable.Columns.Add("SR_NO", typeof(int));
                        dataTable.Columns.Add("DOCUMENT_NAME", typeof(string));
                        dataTable.Columns.Add("DOCUMENT_CATEGORY", typeof(string));
                        dataTable.Columns.Add("ATTRIBUTE1", typeof(string));
                        dataTable.Columns.Add("ATTRIBUTE2", typeof(string));
                        dataTable.Columns.Add("ATTRIBUTE3", typeof(string));
                        dataTable.Columns.Add("ATTRIBUTE4", typeof(string));
                        dataTable.Columns.Add("ATTRIBUTE5", typeof(string));
                        dataTable.Columns.Add("CREATED_BY", typeof(int));
                        dataTable.Columns.Add("CREATION_DATE", typeof(DateTime));
                        dataTable.Columns.Add("LAST_UPDATED_BY", typeof(int));
                        dataTable.Columns.Add("LAST_UPDATE_DATE", typeof(DateTime));
                        dataTable.Columns.Add("DELETE_FLAG", typeof(char));

                        var SR_No = await db.QuerySingleAsync<int>("SELECT isnull(max(t.SR_NO), 0) + 1 FROM APPROVAL_TEMPLATE_TRL_ENDRESULT t WHERE MKEY = @MKEY AND DELETE_FLAG = 'N';",
                                                                   new { MKEY = aPPROVAL_TEMPLATE_HDR.MKEY },
                                                                   commandType: CommandType.Text,
                                                                   transaction: transaction);

                        foreach (var END_DOC_LIST in aPPROVAL_TEMPLATE_HDR.END_RESULT_DOC_LST)
                        {
                            dataTable.Rows.Add(aPPROVAL_TEMPLATE_HDR.MKEY, SR_No, END_DOC_LIST.Key, END_DOC_LIST.Value, null, null, null, null, null,
                                               aPPROVAL_TEMPLATE_HDR.CREATED_BY, dateTime, aPPROVAL_TEMPLATE_HDR.LAST_UPDATED_BY, dateTime, 'N');
                            SR_No++;
                        }

                        using var bulkCopy = new SqlBulkCopy(sqlConnection, SqlBulkCopyOptions.Default, (SqlTransaction)transaction)
                        {
                            DestinationTableName = "APPROVAL_TEMPLATE_TRL_ENDRESULT"
                        };

                        await bulkCopy.WriteToServerAsync(dataTable);
                    }
                    #endregion

                    #region Insert CHECKLIST_DOC_LST
                    if (aPPROVAL_TEMPLATE_HDR.CHECKLIST_DOC_LST != null)
                    {
                        var dataTable = new DataTable();
                        dataTable.Columns.Add("MKEY", typeof(int));
                        dataTable.Columns.Add("SR_NO", typeof(int));
                        dataTable.Columns.Add("DOCUMENT_NAME", typeof(string));
                        dataTable.Columns.Add("DOCUMENT_CATEGORY", typeof(string));
                        dataTable.Columns.Add("ATTRIBUTE1", typeof(string));
                        dataTable.Columns.Add("ATTRIBUTE2", typeof(string));
                        dataTable.Columns.Add("ATTRIBUTE3", typeof(string));
                        dataTable.Columns.Add("ATTRIBUTE4", typeof(string));
                        dataTable.Columns.Add("ATTRIBUTE5", typeof(string));
                        dataTable.Columns.Add("CREATED_BY", typeof(int));
                        dataTable.Columns.Add("CREATION_DATE", typeof(DateTime));
                        dataTable.Columns.Add("LAST_UPDATED_BY", typeof(int));
                        dataTable.Columns.Add("LAST_UPDATE_DATE", typeof(DateTime));
                        dataTable.Columns.Add("DELETE_FLAG", typeof(char));

                        var SR_No = await db.QuerySingleAsync<int>("SELECT isnull(max(t.SR_NO), 0) + 1 FROM APPROVAL_TEMPLATE_TRL_CHECKLIST t WHERE MKEY = @MKEY AND DELETE_FLAG = 'N';",
                                                                   new { MKEY = aPPROVAL_TEMPLATE_HDR.MKEY },
                                                                   commandType: CommandType.Text,
                                                                   transaction: transaction);

                        foreach (var CHECK_LIST in aPPROVAL_TEMPLATE_HDR.CHECKLIST_DOC_LST)
                        {
                            dataTable.Rows.Add(aPPROVAL_TEMPLATE_HDR.MKEY, SR_No, CHECK_LIST.Key, CHECK_LIST.Value, null, null, null, null, null,
                                               aPPROVAL_TEMPLATE_HDR.CREATED_BY, dateTime, aPPROVAL_TEMPLATE_HDR.LAST_UPDATED_BY, dateTime, 'N');
                            SR_No++;
                        }

                        using var bulkCopyCheckList = new SqlBulkCopy(sqlConnection, SqlBulkCopyOptions.Default, (SqlTransaction)transaction)
                        {
                            DestinationTableName = "APPROVAL_TEMPLATE_TRL_CHECKLIST"
                        };

                        await bulkCopyCheckList.WriteToServerAsync(dataTable);
                    }
                    #endregion

                    #region Insert SUBTASK_LIST
                    if (aPPROVAL_TEMPLATE_HDR.SUBTASK_LIST != null)
                    {
                        var subtaskDataTable = new DataTable();
                        subtaskDataTable.Columns.Add("HEADER_MKEY", typeof(int));
                        subtaskDataTable.Columns.Add("SEQ_NO", typeof(string));
                        subtaskDataTable.Columns.Add("SUBTASK_ABBR", typeof(string));
                        subtaskDataTable.Columns.Add("SUBTASK_MKEY", typeof(int));
                        subtaskDataTable.Columns.Add("SUBTASK_PARENT_ID", typeof(int));
                        subtaskDataTable.Columns.Add("ATTRIBUTE1", typeof(string));
                        subtaskDataTable.Columns.Add("ATTRIBUTE2", typeof(string));
                        subtaskDataTable.Columns.Add("ATTRIBUTE3", typeof(string));
                        subtaskDataTable.Columns.Add("ATTRIBUTE4", typeof(string));
                        subtaskDataTable.Columns.Add("ATTRIBUTE5", typeof(string));
                        subtaskDataTable.Columns.Add("ATTRIBUTE6", typeof(string));
                        subtaskDataTable.Columns.Add("ATTRIBUTE7", typeof(string));
                        subtaskDataTable.Columns.Add("ATTRIBUTE8", typeof(string));
                        subtaskDataTable.Columns.Add("CREATED_BY", typeof(int));
                        subtaskDataTable.Columns.Add("CREATION_DATE", typeof(DateTime));
                        subtaskDataTable.Columns.Add("LAST_UPDATED_BY", typeof(int));
                        subtaskDataTable.Columns.Add("LAST_UPDATE_DATE", typeof(DateTime));
                        subtaskDataTable.Columns.Add("DELETE_FLAG", typeof(char));

                        foreach (var subtask in aPPROVAL_TEMPLATE_HDR.SUBTASK_LIST)
                        {
                            subtaskDataTable.Rows.Add(aPPROVAL_TEMPLATE_HDR.MKEY, subtask.SEQ_NO, subtask.SUBTASK_ABBR, subtask.SUBTASK_MKEY, aPPROVAL_TEMPLATE_HDR.MKEY,
                                                      null, null, null, null, null, null, null, null, aPPROVAL_TEMPLATE_HDR.CREATED_BY, dateTime, aPPROVAL_TEMPLATE_HDR.CREATED_BY, dateTime, 'N');
                        }

                        using var bulkCopySubtask = new SqlBulkCopy(sqlConnection, SqlBulkCopyOptions.Default, (SqlTransaction)transaction)
                        {
                            DestinationTableName = "APPROVAL_TEMPLATE_TRL_SUBTASK"
                        };

                        await bulkCopySubtask.WriteToServerAsync(subtaskDataTable);
                    }
                    #endregion

                    var sqlTransaction = transaction as SqlTransaction;
                    if (sqlTransaction != null)
                    {
                        await sqlTransaction.CommitAsync();  // Commit the entire transaction
                    }

                    transactionCompleted = true;

                    aPPROVAL_TEMPLATE_HDR.Status = "Ok";
                    aPPROVAL_TEMPLATE_HDR.Message = "updated successfully";

                    return aPPROVAL_TEMPLATE_HDR;  // Return the updated object
                }
            }
            catch (Exception ex)
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
                        // Handle rollback exception (may occur if transaction is already completed)
                        // Log or handle the rollback failure if needed
                        Console.WriteLine($"Rollback failed: {rollbackEx.Message}");
                    }
                }

                var approvalTemplate = new APPROVAL_TEMPLATE_HDR();
                approvalTemplate.Status = "Error";
                approvalTemplate.Message = ex.Message;
                return approvalTemplate;
            }
        }
        public async Task<bool> DeleteApprovalTemplateAsync(int MKEY, int LAST_UPDATED_BY)
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
                        return true;
                    }

                    if (sqlConnection.State != ConnectionState.Open)
                    {
                        await sqlConnection.OpenAsync();  // Ensure the connection is open
                    }

                    transaction = sqlConnection.BeginTransaction();  // Begin a SqlTransaction
                    transactionCompleted = false;  // Reset transaction state

                    DateTime dateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);

                    var parametersTRL = new DynamicParameters();
                    parametersTRL.Add("@MKEY", MKEY);
                    parametersTRL.Add("@LAST_UPDATED_BY", LAST_UPDATED_BY);
                    var DeleteApprovalTrl = await db.QueryFirstOrDefaultAsync<dynamic>("SP_DELETE_APPROVAL_HDR_TRL_SUBTASK", parametersTRL, commandType: CommandType.StoredProcedure, transaction: transaction);

                    var sqlTransaction = transaction as SqlTransaction;
                    if (sqlTransaction != null)
                    {
                        await sqlTransaction.CommitAsync();  // Commit the entire transaction
                    }

                    transactionCompleted = true;
                    return true;
                }
            }
            catch (Exception ex)
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
                        // Handle rollback exception (may occur if transaction is already completed)
                        // Log or handle the rollback failure if needed
                        Console.WriteLine($"Rollback failed: {rollbackEx.Message}");
                    }
                }
                return false;
            }
        }

        ////public async Task<APPROVAL_TEMPLATE_HDR> UpdateApprovalTemplateAsync(APPROVAL_TEMPLATE_HDR aPPROVAL_TEMPLATE_HDR)
        ////{
        ////    IDbTransaction transaction = null;
        ////    bool transactionCompleted = false;
        ////    try
        ////    {

        ////        using (IDbConnection db = _dapperDbConnection.CreateConnection())
        ////        {
        ////            var sqlConnection = db as SqlConnection;
        ////            if (sqlConnection == null)
        ////            {
        ////                throw new InvalidOperationException("The connection must be a SqlConnection to use OpenAsync.");
        ////            }

        ////            if (sqlConnection.State != ConnectionState.Open)
        ////            {
        ////                await sqlConnection.OpenAsync();  // Ensure the connection is open
        ////            }

        ////            transaction = db.BeginTransaction();
        ////            transactionCompleted = false;  // Reset transaction state

        ////            var OBJ_APPROVAL_TEMPLATE_HDR = aPPROVAL_TEMPLATE_HDR;
        ////            var parameters = new DynamicParameters();
        ////            parameters.Add("@BUILDING_TYPE", aPPROVAL_TEMPLATE_HDR.BUILDING_TYPE);
        ////            parameters.Add("@BUILDING_STANDARD", aPPROVAL_TEMPLATE_HDR.BUILDING_STANDARD);
        ////            parameters.Add("@STATUTORY_AUTHORITY", aPPROVAL_TEMPLATE_HDR.STATUTORY_AUTHORITY);
        ////            parameters.Add("@SHORT_DESCRIPTION", aPPROVAL_TEMPLATE_HDR.SHORT_DESCRIPTION);
        ////            parameters.Add("@LONG_DESCRIPTION", aPPROVAL_TEMPLATE_HDR.LONG_DESCRIPTION);
        ////            parameters.Add("@ABBR", aPPROVAL_TEMPLATE_HDR.MAIN_ABBR);
        ////            parameters.Add("@APPROVAL_DEPARTMENT", aPPROVAL_TEMPLATE_HDR.AUTHORITY_DEPARTMENT);
        ////            parameters.Add("@RESPOSIBLE_EMP_MKEY", aPPROVAL_TEMPLATE_HDR.RESPOSIBLE_EMP_MKEY);
        ////            parameters.Add("@JOB_ROLE", aPPROVAL_TEMPLATE_HDR.JOB_ROLE);
        ////            parameters.Add("@NO_DAYS_REQUIRED", aPPROVAL_TEMPLATE_HDR.DAYS_REQUIERD);
        ////            parameters.Add("@SANCTION_AUTHORITY", aPPROVAL_TEMPLATE_HDR.SANCTION_AUTHORITY);
        ////            parameters.Add("@SANCTION_DEPARTMENT", aPPROVAL_TEMPLATE_HDR.SANCTION_DEPARTMENT);
        ////            parameters.Add("@END_RESULT_DOC", aPPROVAL_TEMPLATE_HDR.END_RESULT_DOC);
        ////            parameters.Add("@CHECKLIST_DOC", aPPROVAL_TEMPLATE_HDR.CHECKLIST_DOC);
        ////            parameters.Add("@ATTRIBUTE1", aPPROVAL_TEMPLATE_HDR.ATTRIBUTE1);
        ////            parameters.Add("@ATTRIBUTE2", aPPROVAL_TEMPLATE_HDR.ATTRIBUTE2);
        ////            parameters.Add("@ATTRIBUTE3", aPPROVAL_TEMPLATE_HDR.ATTRIBUTE3);
        ////            parameters.Add("@ATTRIBUTE4", aPPROVAL_TEMPLATE_HDR.ATTRIBUTE4);
        ////            parameters.Add("@ATTRIBUTE5", aPPROVAL_TEMPLATE_HDR.ATTRIBUTE5);
        ////            parameters.Add("@CREATED_BY", aPPROVAL_TEMPLATE_HDR.CREATED_BY);
        ////            parameters.Add("@LAST_UPDATED_BY", aPPROVAL_TEMPLATE_HDR.LAST_UPDATED_BY);
        ////            aPPROVAL_TEMPLATE_HDR = await db.QueryFirstOrDefaultAsync<APPROVAL_TEMPLATE_HDR>("SP_INSERT_APPROVAL_TEMPLATE", parameters, commandType: CommandType.StoredProcedure);

        ////            DateTime dateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);

        ////            // using var connection = new SqlConnection(_connectionString);
        ////            //await connection.OpenAsync();

        ////            // Use BeginTransaction() (synchronously) to get a SqlTransaction END_RESULT_DOC_LST
        ////            // using var transaction = connection.BeginTransaction();
        ////            try
        ////            {

        ////                var sqlTransaction = transaction as SqlTransaction;

        ////                if (sqlTransaction == null)
        ////                {
        ////                    throw new InvalidOperationException("Transaction is not of type SqlTransaction.");
        ////                }

        ////                // Create a DataTable for bulk insert END_RESULT_DOC_LST
        ////                var dataTable = new DataTable();
        ////                dataTable.Columns.Add("MKEY", typeof(int));
        ////                dataTable.Columns.Add("SR_NO", typeof(int));
        ////                dataTable.Columns.Add("DOCUMENT_NAME", typeof(string));
        ////                dataTable.Columns.Add("DOCUMENT_CATEGORY", typeof(string));
        ////                dataTable.Columns.Add("ATTRIBUTE1", typeof(string));
        ////                dataTable.Columns.Add("ATTRIBUTE2", typeof(string));
        ////                dataTable.Columns.Add("ATTRIBUTE3", typeof(string));
        ////                dataTable.Columns.Add("ATTRIBUTE4", typeof(string));
        ////                dataTable.Columns.Add("ATTRIBUTE5", typeof(string));
        ////                dataTable.Columns.Add("CREATED_BY", typeof(int));
        ////                dataTable.Columns.Add("CREATION_DATE", typeof(DateTime));
        ////                dataTable.Columns.Add("LAST_UPDATED_BY", typeof(int));
        ////                dataTable.Columns.Add("LAST_UPDATE_DATE", typeof(DateTime));
        ////                dataTable.Columns.Add("DELETE_FLAG", typeof(char));

        ////                if (OBJ_APPROVAL_TEMPLATE_HDR.END_RESULT_DOC_LST != null)
        ////                {
        ////                    var SR_No = await db.QuerySingleAsync<int>("SELECT isnull(max(t.SR_NO),0) + 1 FROM APPROVAL_TEMPLATE_TRL_ENDRESULT t WHERE MKEY = @MKEY ", new { MKEY = aPPROVAL_TEMPLATE_HDR.MKEY }, commandType: CommandType.Text);
        ////                    // Populate the DataTable with product data
        ////                    foreach (var END_DOC_LIST in OBJ_APPROVAL_TEMPLATE_HDR.END_RESULT_DOC_LST)
        ////                    {
        ////                        dataTable.Rows.Add(aPPROVAL_TEMPLATE_HDR.MKEY, SR_No, END_DOC_LIST.Key, END_DOC_LIST.Value, null, null, null, null, null, aPPROVAL_TEMPLATE_HDR.CREATED_BY, dateTime.ToString("yyyy/MM/dd hh:mm:ss"), aPPROVAL_TEMPLATE_HDR.LAST_UPDATED_BY, dateTime.ToString("yyyy/MM/dd hh:mm:ss"), 'N');
        ////                        SR_No = SR_No + 1;
        ////                    }
        ////                    SR_No = 0;

        ////                    // Use SqlBulkCopy for bulk insert
        ////                    using var bulkCopy = new SqlBulkCopy(sqlConnection, SqlBulkCopyOptions.Default, transaction)
        ////                    {
        ////                        DestinationTableName = "APPROVAL_TEMPLATE_TRL_ENDRESULT"
        ////                    };

        ////                    // Execute the bulk copy
        ////                    await bulkCopy.WriteToServerAsync(dataTable);

        ////                    // Commit transaction
        ////                    await transaction.CommitAsync();

        ////                    /*
        ////                     * TO GET INSERTED VALUE IN END RESULT
        ////                     * */
        ////                    // Query the APPROVAL_TEMPLATE_TRL_CHECKLIST for key-value pairs
        ////                    string sql = "SELECT DOCUMENT_NAME, DOCUMENT_CATEGORY FROM APPROVAL_TEMPLATE_TRL_ENDRESULT WHERE MKEY = @MKEY";
        ////                    var keyValuePairs = await db.QueryAsync(sql, new { MKEY = aPPROVAL_TEMPLATE_HDR.MKEY });

        ////                    // Initialize the END_RESULT_DOC_LST dictionary
        ////                    aPPROVAL_TEMPLATE_HDR.END_RESULT_DOC_LST = new Dictionary<string, object>();

        ////                    // Populate the dictionary with the key-value pairs
        ////                    foreach (var item in keyValuePairs)
        ////                    {
        ////                        // Assuming DOCUMENT_NAME is the key and DOCUMENT_CATEGORY is the value
        ////                        aPPROVAL_TEMPLATE_HDR.END_RESULT_DOC_LST.Add(item.DOCUMENT_NAME.ToString(), item.DOCUMENT_CATEGORY);
        ////                    }
        ////                }
        ////                /*-------------------------------------------------------------------------------------------------------------------
        ////                TO INSERT END RESULT LIST
        ////                */


        ////                //-------------------------------------------------------------------------------------------------------------------
        ////                /*-------------------------------------------------------------------------------------------------------------------
        ////               TO INSERT CHECK LIST
        ////               */
        ////                // Populate the DataTable with product data
        ////                dataTable.Rows.Clear();
        ////                using var transactionCheckList = sqlConnection.BeginTransaction();

        ////                if (OBJ_APPROVAL_TEMPLATE_HDR.CHECKLIST_DOC_LST != null)
        ////                {
        ////                    var SR_No = await db.QuerySingleAsync<int>("SELECT isnull(max(t.SR_NO),0) + 1 FROM APPROVAL_TEMPLATE_TRL_CHECKLIST t " +
        ////                        "WHERE MKEY = @MKEY ", new { MKEY = OBJ_APPROVAL_TEMPLATE_HDR.MKEY }, commandType: CommandType.Text);

        ////                    foreach (var CHECK_LIST in OBJ_APPROVAL_TEMPLATE_HDR.CHECKLIST_DOC_LST)
        ////                    {
        ////                        dataTable.Rows.Add(aPPROVAL_TEMPLATE_HDR.MKEY, SR_No, CHECK_LIST.Key, CHECK_LIST.Value, null, null, null, null, null,
        ////                            aPPROVAL_TEMPLATE_HDR.CREATED_BY, dateTime.ToString("yyyy/MM/dd hh:mm:ss"), aPPROVAL_TEMPLATE_HDR.LAST_UPDATED_BY
        ////                            , dateTime.ToString("yyyy/MM/dd hh:mm:ss"), 'N');
        ////                        SR_No = SR_No + 1;
        ////                    }
        ////                    SR_No = 0;

        ////                    // Use SqlBulkCopy for bulk insert
        ////                    using var bulkCopyCheckList = new SqlBulkCopy(connection, SqlBulkCopyOptions.Default, transactionCheckList)
        ////                    {
        ////                        DestinationTableName = "APPROVAL_TEMPLATE_TRL_CHECKLIST"
        ////                    };

        ////                    // Execute the bulk copy
        ////                    await bulkCopyCheckList.WriteToServerAsync(dataTable);

        ////                    // Commit transaction
        ////                    await transactionCheckList.CommitAsync();


        ////                    // Query the APPROVAL_TEMPLATE_TRL_CHECKLIST for key-value pairs
        ////                    var sql = "SELECT DOCUMENT_NAME, DOCUMENT_CATEGORY FROM APPROVAL_TEMPLATE_TRL_CHECKLIST WHERE MKEY = @MKEY";
        ////                    var keyValuePairs = await db.QueryAsync(sql, new { MKEY = aPPROVAL_TEMPLATE_HDR.MKEY });

        ////                    // Initialize the END_RESULT_DOC_LST dictionary
        ////                    aPPROVAL_TEMPLATE_HDR.CHECKLIST_DOC_LST = new Dictionary<string, object>();

        ////                    // Populate the dictionary with the key-value pairs
        ////                    foreach (var item in keyValuePairs)
        ////                    {
        ////                        // Assuming DOCUMENT_NAME is the key and DOCUMENT_CATEGORY is the value
        ////                        aPPROVAL_TEMPLATE_HDR.CHECKLIST_DOC_LST.Add(item.DOCUMENT_NAME.ToString(), item.DOCUMENT_CATEGORY);
        ////                    }
        ////                }

        ////                //------------------------------------------------------------------------------------------------------------------------------
        ////            }
        ////            catch
        ////            {
        ////                await transaction.RollbackAsync();
        ////                throw;
        ////            }

        ////            using var transactionSubTask = sqlConnection.BeginTransaction();
        ////            try
        ////            {
        ////                // Create a DataTable for bulk insert of subtasks (SEQNO, SRNO, ABBR)
        ////                var subtaskDataTable = new DataTable();
        ////                subtaskDataTable.Columns.Add("HEADER_MKEY", typeof(int));
        ////                subtaskDataTable.Columns.Add("SEQ_NO", typeof(string));  // task_no
        ////                subtaskDataTable.Columns.Add("SUBTASK_ABBR", typeof(string));
        ////                subtaskDataTable.Columns.Add("SUBTASK_MKEY", typeof(int));
        ////                subtaskDataTable.Columns.Add("SUBTASK_PARENT_ID", typeof(int));
        ////                subtaskDataTable.Columns.Add("ATTRIBUTE1", typeof(string));
        ////                subtaskDataTable.Columns.Add("ATTRIBUTE2", typeof(string));
        ////                subtaskDataTable.Columns.Add("ATTRIBUTE3", typeof(string));
        ////                subtaskDataTable.Columns.Add("ATTRIBUTE4", typeof(string));
        ////                subtaskDataTable.Columns.Add("ATTRIBUTE5", typeof(string));
        ////                subtaskDataTable.Columns.Add("ATTRIBUTE6", typeof(string));
        ////                subtaskDataTable.Columns.Add("ATTRIBUTE7", typeof(string));
        ////                subtaskDataTable.Columns.Add("ATTRIBUTE8", typeof(string));
        ////                subtaskDataTable.Columns.Add("CREATED_BY", typeof(int));
        ////                subtaskDataTable.Columns.Add("CREATION_DATE", typeof(DateTime));
        ////                subtaskDataTable.Columns.Add("LAST_UPDATED_BY", typeof(int));
        ////                subtaskDataTable.Columns.Add("LAST_UPDATE_DATE", typeof(DateTime));
        ////                subtaskDataTable.Columns.Add("DELETE_FLAG", typeof(char));
        ////                bool flagID = false;
        ////                if (OBJ_APPROVAL_TEMPLATE_HDR.SUBTASK_LIST != null)
        ////                {
        ////                    // Populate the DataTable with subtasks
        ////                    foreach (var subtask in OBJ_APPROVAL_TEMPLATE_HDR.SUBTASK_LIST) // Assuming SUBTASK_LIST is a list of subtasks
        ////                    {
        ////                        subtaskDataTable.Rows.Add(aPPROVAL_TEMPLATE_HDR.MKEY, subtask.SEQ_NO, subtask.SUBTASK_ABBR, subtask.SUBTASK_MKEY, aPPROVAL_TEMPLATE_HDR.MKEY, null, null, null, null, null, null, null, null, OBJ_APPROVAL_TEMPLATE_HDR.CREATED_BY, dateTime.ToString("yyyy/MM/dd hh:mm:ss"), OBJ_APPROVAL_TEMPLATE_HDR.CREATED_BY, dateTime.ToString("yyyy/MM/dd hh:mm:ss"), 'N');
        ////                    }

        ////                    // Use SqlBulkCopy to insert subtasks
        ////                    using var bulkCopy = new SqlBulkCopy(sqlConnection, SqlBulkCopyOptions.Default, transactionSubTask)
        ////                    {
        ////                        DestinationTableName = "APPROVAL_TEMPLATE_TRL_SUBTASK"  // Ensure this matches your table name
        ////                    };

        ////                    await bulkCopy.WriteToServerAsync(subtaskDataTable);

        ////                    // Commit the transactionSubTask
        ////                    await transactionSubTask.CommitAsync();

        ////                    // Optionally, fetch the inserted values (if necessary)
        ////                    string sql = "SELECT HEADER_MKEY,SEQ_NO,SUBTASK_MKEY,SUBTASK_ABBR FROM APPROVAL_TEMPLATE_TRL_SUBTASK WHERE HEADER_MKEY = @HEADER_MKEY";
        ////                    var subtaskKeyValuePairs = await db.QueryAsync(sql, new { HEADER_MKEY = aPPROVAL_TEMPLATE_HDR.MKEY });

        ////                    // Assuming the model has a SUBTASK_LIST dictionary to hold these values
        ////                    aPPROVAL_TEMPLATE_HDR.SUBTASK_LIST = new List<APPROVAL_TEMPLATE_TRL_SUBTASK>();  // Assuming Subtask is a class for this data

        ////                    foreach (var item in subtaskKeyValuePairs)
        ////                    {
        ////                        aPPROVAL_TEMPLATE_HDR.SUBTASK_LIST.Add(new APPROVAL_TEMPLATE_TRL_SUBTASK
        ////                        {
        ////                            HEADER_MKEY = item.HEADER_MKEY,
        ////                            SEQ_NO = item.SEQ_NO,
        ////                            SUBTASK_MKEY = item.SUBTASK_MKEY,
        ////                            SUBTASK_ABBR = item.SUBTASK_ABBR
        ////                        });
        ////                    }
        ////                }
        ////                return aPPROVAL_TEMPLATE_HDR;
        ////            }
        ////            catch (Exception ex)
        ////            {
        ////                await transaction.RollbackAsync();
        ////                throw;
        ////            }
        ////            return aPPROVAL_TEMPLATE_HDR;
        ////        }
        ////    }
        ////    catch (SqlException ex)
        ////    {
        ////        return aPPROVAL_TEMPLATE_HDR;
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        return aPPROVAL_TEMPLATE_HDR;
        ////    }
        ////}


    }
}
