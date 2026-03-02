using Dapper;
using Microsoft.AspNetCore.Mvc;
//using Microsoft.Owin.Security.Provider;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Xml.Linq;
using TaskManagement.API.DapperDbConnections;
using TaskManagement.API.Interfaces;
using TaskManagement.API.Model;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
        public async Task<IEnumerable<OutPutApprovalTemplates>> GetAllApprovalTemplateAsync(int LoggedIN)
        {
            int strMKEY = 0;
            try
            {
                using (IDbConnection db = _dapperDbConnection.CreateConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@MKEY", null);
                    parmeters.Add("@ATTRIBUTE1", LoggedIN.ToString());
                    var approvalTemplates = await db.QueryAsync<OutPutApprovalTemplates>("SP_GET_APPROVAL_TEMPLATE", parmeters, commandType: CommandType.StoredProcedure);
                    //var approvalTemplates1 = await db.QueryAsync<OutPutApprovalTemplates>("select MKEY, DOCUMENT_NAME, count(DOCUMENT_NAME) from APPROVAL_TEMPLATE_TRL_ENDRESULT group by MKEY,DOCUMENT_NAME having count(DOCUMENT_NAME) > 1;", CommandType.Text);
                   

                    if (approvalTemplates == null || !approvalTemplates.Any())
                    {
                        var approvalTemplate = new OutPutApprovalTemplates();
                        approvalTemplate.Status = "Error";
                        approvalTemplate.Message = "Not Found";
                        return new List<OutPutApprovalTemplates> { null };
                    }

                    // Iterate over each approval template header to populate subtasks, end result docs, and checklist docs
                    foreach (var approvalTemplate in approvalTemplates)
                    {
                        strMKEY = approvalTemplate.MKEY;
                        // Fetch the associated subtasks
                        var subtasks = await db.QueryAsync<OUTPUT_APPROVAL_TEMPLATE_TRL_SUBTASK>(
                            "SELECT * FROM APPROVAL_TEMPLATE_TRL_SUBTASK WHERE HEADER_MKEY = @HEADER_MKEY AND DELETE_FLAG = 'N';",
                            new { HEADER_MKEY = approvalTemplate.MKEY });

                        approvalTemplate.SUBTASK_LIST = subtasks.ToList(); // Populate the SUBTASK_LIST property


                        string sql = "SELECT DOCUMENT_NAME, DOCUMENT_CATEGORY FROM APPROVAL_TEMPLATE_TRL_ENDRESULT WHERE MKEY = @MKEY" +
                            " AND DELETE_FLAG = 'N'; ";
                        var keyValuePairs = await db.QueryAsync(sql, new { MKEY = approvalTemplate.MKEY });

                        // Initialize the END_RESULT_DOC_LST dictionary
                        approvalTemplate.END_RESULT_DOC_LST = new Dictionary<string, object>();

                        // Populate the dictionary with the key-value pairs
                        foreach (var item in keyValuePairs)
                        {
                            // Assuming DOCUMENT_NAME is the key and DOCUMENT_CATEGORY is the value
                            approvalTemplate.END_RESULT_DOC_LST.Add(item.DOCUMENT_NAME.ToString(), item.DOCUMENT_CATEGORY);
                        }

                        sql = "SELECT DOCUMENT_NAME, DOCUMENT_CATEGORY FROM APPROVAL_TEMPLATE_TRL_CHECKLIST" +
                            " WHERE MKEY = @MKEY AND DELETE_FLAG = 'N';";
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
                        var Sanctioning_Department = await db.QueryAsync<OUTPUT_APPROVAL_TEMPLATE_TRL_SANCTIONING_DEPARTMENT>(
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
                var approvalTemplate = new OutPutApprovalTemplates();
                approvalTemplate.Status = "Error";
                approvalTemplate.Message = ex.Message + ": " + strMKEY;
                return new List<OutPutApprovalTemplates> { approvalTemplate };
            }
        }
        public async Task<IEnumerable<OutPutApprovalTemplates_NT>> GetAllApprovalTemplateNTAsync(APPROVAL_TEMPLATE_HDR_INPUT_NT aPPROVAL_TEMPLATE_HDR_NT)
        {
            int strMKEY = 0;
            try
            {
                using (IDbConnection db = _dapperDbConnection.CreateConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@MKEY", aPPROVAL_TEMPLATE_HDR_NT.Mkey.ToString());
                    parmeters.Add("@Session_User_Id", aPPROVAL_TEMPLATE_HDR_NT.Session_User_Id.ToString());
                    parmeters.Add("@Business_Group_Id", aPPROVAL_TEMPLATE_HDR_NT.Business_Group_Id.ToString());
                    var approvalTemplates = await db.QueryAsync<OutPutApprovalTemplatesNT>("SP_GET_APPROVAL_TEMPLATE_NT", parmeters, commandType: CommandType.StoredProcedure);

                    if (approvalTemplates.Any())
                    {
                        // Iterate over each approval template header to populate subtasks, end result docs, and checklist docs
                        foreach (var approvalTemplate in approvalTemplates)
                        {
                            strMKEY = approvalTemplate.MKEY;
                            // Fetch the associated subtasks
                            var subtasks = await db.QueryAsync<OUTPUT_APPROVAL_TEMPLATE_TRL_SUBTASK>(
                                "SELECT * FROM APPROVAL_TEMPLATE_TRL_SUBTASK WHERE HEADER_MKEY = @HEADER_MKEY AND DELETE_FLAG = 'N';",
                                new { HEADER_MKEY = approvalTemplate.MKEY });

                            approvalTemplate.Subtask_List = subtasks.ToList(); // Populate the SUBTASK_LIST property


                            string sql = "SELECT DOCUMENT_NAME, DOCUMENT_CATEGORY FROM APPROVAL_TEMPLATE_TRL_ENDRESULT WHERE MKEY = @MKEY" +
                                " AND DELETE_FLAG = 'N'; ";
                            var keyValuePairs = await db.QueryAsync(sql, new { MKEY = approvalTemplate.MKEY });

                            // Initialize the END_RESULT_DOC_LST dictionary
                            approvalTemplate.End_Result_Doc_Lst = new Dictionary<string, object>();

                            // Populate the dictionary with the key-value pairs
                            foreach (var item in keyValuePairs)
                            {
                                // Assuming DOCUMENT_NAME is the key and DOCUMENT_CATEGORY is the value
                                approvalTemplate.End_Result_Doc_Lst.Add(item.DOCUMENT_NAME.ToString(), item.DOCUMENT_CATEGORY);
                            }

                            sql = "SELECT DOCUMENT_NAME, DOCUMENT_CATEGORY FROM APPROVAL_TEMPLATE_TRL_CHECKLIST" +
                                " WHERE MKEY = @MKEY AND DELETE_FLAG = 'N';";
                            var keyValuePairsCheckList = await db.QueryAsync(sql, new { MKEY = approvalTemplate.MKEY });

                            // Initialize the END_RESULT_DOC_LST dictionary
                            approvalTemplate.Checklist_Doc_Lst = new Dictionary<string, object>();

                            // Populate the dictionary with the key-value pairs
                            foreach (var item in keyValuePairsCheckList)
                            {
                                // Assuming DOCUMENT_NAME is the key and DOCUMENT_CATEGORY is the value
                                approvalTemplate.Checklist_Doc_Lst.Add(item.DOCUMENT_NAME.ToString(), item.DOCUMENT_CATEGORY);
                            }

                            strMKEY = approvalTemplate.MKEY;
                            // Fetch the associated subtasks
                            var Sanctioning_Department = await db.QueryAsync<OUTPUT_APPROVAL_TEMPLATE_TRL_SANCTIONING_DEPARTMENT>(
                                "SELECT * FROM V_APPROVAL_TEMPLATE_TRL_SANCTIONING_DEPARTMENT WHERE MKEY = @MKEY AND DELETE_FLAG = 'N';",
                                new { MKEY = approvalTemplate.MKEY });

                            approvalTemplate.Sanctioning_Department_List = Sanctioning_Department.ToList();
                        }
                        var successsResult = new List<OutPutApprovalTemplates_NT>
                    {
                        new OutPutApprovalTemplates_NT
                        {
                            Status = "Ok",
                            Message = "Get data successfully!!!",
                            Data = approvalTemplates
                        }
                    };
                        return successsResult;
                    }
                    else
                    {
                        var ErrorResult = new List<OutPutApprovalTemplates_NT>
                        {
                            new OutPutApprovalTemplates_NT
                            {
                                Status = "Ok",
                                Message = "Get Data Successfuly!!!",
                                Data = approvalTemplates
                            }
                        };
                        return ErrorResult;
                    }
                  
                }
            }
            catch (Exception ex)
            {
                var ErrorResult = new List<OutPutApprovalTemplates_NT>
                    {
                        new OutPutApprovalTemplates_NT
                        {
                            Status = "Ok",
                            Message = "Get data successfully!!!",
                            Data = null
                        }
                    };
                return ErrorResult;
            }
        }
        public async Task<OutPutApprovalTemplates> GetApprovalTemplateByIdAsync(int id, int LoggedIN)
        {
            try
            {
                using (IDbConnection db = _dapperDbConnection.CreateConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@MKEY", id);
                    parmeters.Add("@ATTRIBUTE1", LoggedIN);
                    var approvalTemplate = await db.QueryFirstOrDefaultAsync<OutPutApprovalTemplates>("SP_GET_APPROVAL_TEMPLATE", parmeters, commandType: CommandType.StoredProcedure);

                    if (approvalTemplate == null)
                    {
                        var ErrorTemplate = new OutPutApprovalTemplates();
                        ErrorTemplate.Status = "Error";
                        ErrorTemplate.Message = "Not Found";
                        return ErrorTemplate;
                    }
                    var subtasks = await db.QueryAsync<OUTPUT_APPROVAL_TEMPLATE_TRL_SUBTASK>(
                        "SELECT * FROM APPROVAL_TEMPLATE_TRL_SUBTASK WHERE HEADER_MKEY = @HEADER_MKEY AND DELETE_FLAG = 'N';",
                        new { HEADER_MKEY = approvalTemplate.MKEY });

                    approvalTemplate.SUBTASK_LIST = subtasks.ToList(); // Populate the SUBTASK_LIST property with subtasks

                    string sql = "SELECT DOCUMENT_NAME, DOCUMENT_CATEGORY FROM APPROVAL_TEMPLATE_TRL_ENDRESULT " +
                        "WHERE MKEY = @MKEY AND DELETE_FLAG = 'N';";
                    var keyValuePairs = await db.QueryAsync(sql, new { MKEY = approvalTemplate.MKEY });

                    // Initialize the END_RESULT_DOC_LST dictionary
                    approvalTemplate.END_RESULT_DOC_LST = new Dictionary<string, object>();

                    // Populate the dictionary with the key-value pairs
                    foreach (var item in keyValuePairs)
                    {
                        // Assuming DOCUMENT_NAME is the key and DOCUMENT_CATEGORY is the value
                        approvalTemplate.END_RESULT_DOC_LST.Add(item.DOCUMENT_NAME.ToString(), item.DOCUMENT_CATEGORY);
                    }

                    sql = "SELECT DOCUMENT_NAME, DOCUMENT_CATEGORY FROM APPROVAL_TEMPLATE_TRL_CHECKLIST " +
                        "WHERE MKEY = @MKEY AND DELETE_FLAG = 'N';";
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
                    var Sanctioning_Department = await db.QueryAsync<OUTPUT_APPROVAL_TEMPLATE_TRL_SANCTIONING_DEPARTMENT>(
                        "SELECT * FROM V_APPROVAL_TEMPLATE_TRL_SANCTIONING_DEPARTMENT WHERE MKEY = @MKEY AND DELETE_FLAG = 'N';",
                        new { MKEY = approvalTemplate.MKEY });

                    approvalTemplate.SANCTIONING_DEPARTMENT_LIST = Sanctioning_Department.ToList();
                    return approvalTemplate;
                }
            }
            catch (Exception ex)
            {
                // Handle other unexpected exceptions
                var approvalTemplate = new OutPutApprovalTemplates();
                approvalTemplate.Status = "Error";
                approvalTemplate.Message = ex.Message;
                return approvalTemplate;
            }
        }
        public async Task<ActionResult<OutPutApprovalTemplates>> CreateApprovalTemplateAsync(InsertApprovalTemplates insertApprovalTemplates)
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

                    var OBJ_APPROVAL_TEMPLATE_HDR = insertApprovalTemplates;
                    var parameters = new DynamicParameters();
                    parameters.Add("@BUILDING_TYPE", insertApprovalTemplates.BUILDING_TYPE);
                    parameters.Add("@BUILDING_STANDARD", insertApprovalTemplates.BUILDING_STANDARD);
                    parameters.Add("@STATUTORY_AUTHORITY", insertApprovalTemplates.STATUTORY_AUTHORITY);
                    parameters.Add("@SHORT_DESCRIPTION", insertApprovalTemplates.SHORT_DESCRIPTION);
                    parameters.Add("@LONG_DESCRIPTION", insertApprovalTemplates.LONG_DESCRIPTION);
                    parameters.Add("@ABBR", insertApprovalTemplates.MAIN_ABBR);
                    parameters.Add("@APPROVAL_DEPARTMENT", insertApprovalTemplates.AUTHORITY_DEPARTMENT);
                    parameters.Add("@RESPOSIBLE_EMP_MKEY", insertApprovalTemplates.RESPOSIBLE_EMP_MKEY);
                    parameters.Add("@JOB_ROLE", insertApprovalTemplates.JOB_ROLE);
                    parameters.Add("@NO_DAYS_REQUIRED", insertApprovalTemplates.DAYS_REQUIERD);
                    parameters.Add("@SEQ_ORDER", insertApprovalTemplates.SEQ_ORDER);
                    parameters.Add("@TAGS", insertApprovalTemplates.TAGS);
                    parameters.Add("@CREATED_BY", insertApprovalTemplates.CREATED_BY);
                    parameters.Add("@TAGS", insertApprovalTemplates.TAGS);
                    var objOutPutApprovalTemplates = await db.QueryFirstOrDefaultAsync<OutPutApprovalTemplates>("SP_INSERT_APPROVAL_TEMPLATE", parameters,
                        commandType: CommandType.StoredProcedure, transaction: transaction);

                    if (objOutPutApprovalTemplates.Status != "Ok")
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
                                objOutPutApprovalTemplates.Status = "Error";
                                objOutPutApprovalTemplates.Message = rollbackEx.Message;
                                Console.WriteLine($"Rollback failed: {rollbackEx.Message}");
                            }

                            var ErrorApprovalTemplates = new OutPutApprovalTemplates
                            {
                                MKEY = 0,
                                Status = objOutPutApprovalTemplates.Status,
                                Message = objOutPutApprovalTemplates.Message
                            };
                            return ErrorApprovalTemplates;
                        }
                    }

                    DateTime dateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
                    try
                    {
                        // Create a DataTable for bulk insert END_RESULT_DOC_LST
                        var dataTable = new DataTable();
                        dataTable.Columns.Add("MKEY", typeof(int));
                        dataTable.Columns.Add("SR_NO", typeof(int));
                        dataTable.Columns.Add("DOCUMENT_NAME", typeof(string));
                        dataTable.Columns.Add("DOCUMENT_CATEGORY", typeof(string));
                        dataTable.Columns.Add("CREATED_BY", typeof(int));
                        dataTable.Columns.Add("CREATION_DATE", typeof(DateTime));
                        dataTable.Columns.Add("DELETE_FLAG", typeof(char));

                        if (OBJ_APPROVAL_TEMPLATE_HDR.END_RESULT_DOC_LST != null)
                        {
                            var SR_No = await db.QuerySingleAsync<int>("SELECT isnull(max(t.SR_NO),0) + 1 FROM APPROVAL_TEMPLATE_TRL_ENDRESULT t " +
                                "WHERE MKEY = @MKEY AND DELETE_FLAG = 'N';", new { MKEY = objOutPutApprovalTemplates.MKEY }, commandType: CommandType.Text,
                                transaction: transaction);
                            // Populate the DataTable with product data
                            foreach (var END_DOC_LIST in OBJ_APPROVAL_TEMPLATE_HDR.END_RESULT_DOC_LST)
                            {
                                dataTable.Rows.Add(objOutPutApprovalTemplates.MKEY, SR_No, END_DOC_LIST.Key, END_DOC_LIST.Value,
                                    insertApprovalTemplates.CREATED_BY, dateTime.ToString("yyyy/MM/dd hh:mm:ss"), 'N');
                                SR_No = SR_No + 1;
                            }
                            SR_No = 0;

                            // Use SqlBulkCopy for bulk insert
                            using var bulkCopy = new SqlBulkCopy(sqlConnection, SqlBulkCopyOptions.Default, (SqlTransaction)transaction)
                            {
                                DestinationTableName = "APPROVAL_TEMPLATE_TRL_ENDRESULT"
                            };

                            bulkCopy.ColumnMappings.Add("MKEY", "MKEY");
                            bulkCopy.ColumnMappings.Add("SR_NO", "SR_NO");
                            bulkCopy.ColumnMappings.Add("DOCUMENT_NAME", "DOCUMENT_NAME");
                            bulkCopy.ColumnMappings.Add("DOCUMENT_CATEGORY", "DOCUMENT_CATEGORY");
                            bulkCopy.ColumnMappings.Add("CREATED_BY", "CREATED_BY");
                            bulkCopy.ColumnMappings.Add("CREATION_DATE", "CREATION_DATE");
                            bulkCopy.ColumnMappings.Add("DELETE_FLAG", "DELETE_FLAG");

                            // Execute the bulk copy
                            await bulkCopy.WriteToServerAsync(dataTable);

                            /*
                             * TO GET INSERTED VALUE IN END RESULT
                             * */
                            // Query the APPROVAL_TEMPLATE_TRL_CHECKLIST for key-value pairs
                            string sql = "SELECT DOCUMENT_NAME, DOCUMENT_CATEGORY FROM APPROVAL_TEMPLATE_TRL_ENDRESULT WHERE MKEY = @MKEY AND DELETE_FLAG = 'N';";
                            var keyValuePairs = await db.QueryAsync(sql, new { MKEY = objOutPutApprovalTemplates.MKEY }, transaction: transaction);

                            // Initialize the END_RESULT_DOC_LST dictionary
                            objOutPutApprovalTemplates.END_RESULT_DOC_LST = new Dictionary<string, object>();

                            // Populate the dictionary with the key-value pairs
                            foreach (var item in keyValuePairs)
                            {
                                // Assuming DOCUMENT_NAME is the key and DOCUMENT_CATEGORY is the value
                                objOutPutApprovalTemplates.END_RESULT_DOC_LST.Add(item.DOCUMENT_NAME.ToString(), item.DOCUMENT_CATEGORY);
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
                                "WHERE MKEY = @MKEY AND DELETE_FLAG = 'N'", new { MKEY = objOutPutApprovalTemplates.MKEY }, commandType: CommandType.Text
                                , transaction: transaction);

                            foreach (var CHECK_LIST in OBJ_APPROVAL_TEMPLATE_HDR.CHECKLIST_DOC_LST)
                            {
                                dataTable.Rows.Add(objOutPutApprovalTemplates.MKEY, SR_No, CHECK_LIST.Key, CHECK_LIST.Value,
                                    insertApprovalTemplates.CREATED_BY, dateTime.ToString("yyyy/MM/dd hh:mm:ss"), 'N');
                                SR_No = SR_No + 1;
                            }
                            SR_No = 0;

                            // Use SqlBulkCopy for bulk insert
                            using var bulkCopyCheckList = new SqlBulkCopy(sqlConnection, SqlBulkCopyOptions.Default, (SqlTransaction)transaction)
                            {
                                DestinationTableName = "APPROVAL_TEMPLATE_TRL_CHECKLIST"
                            };

                            bulkCopyCheckList.ColumnMappings.Add("MKEY", "MKEY");
                            bulkCopyCheckList.ColumnMappings.Add("SR_NO", "SR_NO");
                            bulkCopyCheckList.ColumnMappings.Add("DOCUMENT_NAME", "DOCUMENT_NAME");
                            bulkCopyCheckList.ColumnMappings.Add("DOCUMENT_CATEGORY", "DOCUMENT_CATEGORY");
                            bulkCopyCheckList.ColumnMappings.Add("CREATED_BY", "CREATED_BY");
                            bulkCopyCheckList.ColumnMappings.Add("CREATION_DATE", "CREATION_DATE");
                            bulkCopyCheckList.ColumnMappings.Add("DELETE_FLAG", "DELETE_FLAG");

                            // Execute the bulk copy
                            await bulkCopyCheckList.WriteToServerAsync(dataTable);

                            // Commit transaction
                            //await transactionCheckList.CommitAsync();


                            // Query the APPROVAL_TEMPLATE_TRL_CHECKLIST for key-value pairs
                            var sql = "SELECT DOCUMENT_NAME, DOCUMENT_CATEGORY FROM APPROVAL_TEMPLATE_TRL_CHECKLIST WHERE MKEY = @MKEY AND DELETE_FLAG = 'N';";
                            var keyValuePairs = await db.QueryAsync(sql, new { MKEY = objOutPutApprovalTemplates.MKEY }, transaction: transaction);

                            // Initialize the END_RESULT_DOC_LST dictionary
                            objOutPutApprovalTemplates.CHECKLIST_DOC_LST = new Dictionary<string, object>();

                            // Populate the dictionary with the key-value pairs
                            foreach (var item in keyValuePairs)
                            {
                                // Assuming DOCUMENT_NAME is the key and DOCUMENT_CATEGORY is the value
                                objOutPutApprovalTemplates.CHECKLIST_DOC_LST.Add(item.DOCUMENT_NAME.ToString(), item.DOCUMENT_CATEGORY);
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
                                objOutPutApprovalTemplates.Status = "Error";
                                objOutPutApprovalTemplates.Message = rollbackEx.Message;
                                Console.WriteLine($"Rollback failed: {rollbackEx.Message}");
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
                        subtaskDataTable.Columns.Add("CREATED_BY", typeof(int));
                        subtaskDataTable.Columns.Add("CREATION_DATE", typeof(DateTime));
                        subtaskDataTable.Columns.Add("DELETE_FLAG", typeof(char));

                        bool flagID = false;
                        if (OBJ_APPROVAL_TEMPLATE_HDR.SUBTASK_LIST.Any())
                        {
                            if (OBJ_APPROVAL_TEMPLATE_HDR.SUBTASK_LIST != null && OBJ_APPROVAL_TEMPLATE_HDR.SUBTASK_LIST.Count > 0)
                            {
                                foreach (var subtask in OBJ_APPROVAL_TEMPLATE_HDR.SUBTASK_LIST) // Assuming SUBTASK_LIST is a list of subtasks
                                {
                                    var parametersApproval = new DynamicParameters();
                                    parametersApproval.Add("@APPROVAL_MKEY", objOutPutApprovalTemplates.MKEY);
                                    parametersApproval.Add("@MKEY", subtask.SUBTASK_MKEY);
                                    var ApprovalTemplates = await db.QueryFirstOrDefaultAsync<OutPutApprovalTemplates>("SP_GET_CHECK_APPROVAL", parametersApproval,
                                        commandType: CommandType.StoredProcedure, transaction: transaction);

                                    if (objOutPutApprovalTemplates.MKEY == subtask.SUBTASK_MKEY || ApprovalTemplates.Status != "Ok")
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
                                                objOutPutApprovalTemplates.Status = "Error";
                                                objOutPutApprovalTemplates.Message = rollbackEx.Message;
                                                Console.WriteLine($"Rollback failed: {rollbackEx.Message}");
                                            }
                                            var ErrorApprovalTemplates = new OutPutApprovalTemplates
                                            {
                                                MKEY = 0,
                                                Status = "Error",
                                                Message = "Approval Header Mkey and Sub Approval is same"
                                            };
                                            return ErrorApprovalTemplates;
                                        }
                                    }
                                    else
                                    {
                                        subtaskDataTable.Rows.Add(objOutPutApprovalTemplates.MKEY, subtask.SEQ_NO, subtask.SUBTASK_ABBR, subtask.SUBTASK_MKEY
                                            , objOutPutApprovalTemplates.MKEY, OBJ_APPROVAL_TEMPLATE_HDR.CREATED_BY
                                            , dateTime.ToString("yyyy/MM/dd hh:mm:ss"), 'N');
                                    }
                                }

                                // Use SqlBulkCopy to insert subtasks
                                using var bulkCopy = new SqlBulkCopy(sqlConnection, SqlBulkCopyOptions.Default, (SqlTransaction)transaction)
                                {
                                    DestinationTableName = "APPROVAL_TEMPLATE_TRL_SUBTASK"  // Ensure this matches your table name
                                };

                                bulkCopy.ColumnMappings.Add("HEADER_MKEY", "HEADER_MKEY");
                                bulkCopy.ColumnMappings.Add("SUBTASK_ABBR", "SUBTASK_ABBR");
                                bulkCopy.ColumnMappings.Add("SEQ_NO", "SEQ_NO");
                                bulkCopy.ColumnMappings.Add("SUBTASK_MKEY", "SUBTASK_MKEY");
                                bulkCopy.ColumnMappings.Add("SUBTASK_PARENT_ID", "SUBTASK_PARENT_ID");
                                bulkCopy.ColumnMappings.Add("CREATED_BY", "CREATED_BY");
                                bulkCopy.ColumnMappings.Add("CREATION_DATE", "CREATION_DATE");
                                bulkCopy.ColumnMappings.Add("DELETE_FLAG", "DELETE_FLAG");

                                await bulkCopy.WriteToServerAsync(subtaskDataTable);

                                // Commit the transactionSubTask
                                //await transactionSubTask.CommitAsync();

                                // Optionally, fetch the inserted values (if necessary)
                                string sql = "SELECT HEADER_MKEY,SEQ_NO,SUBTASK_MKEY,SUBTASK_ABBR FROM APPROVAL_TEMPLATE_TRL_SUBTASK WHERE HEADER_MKEY = @HEADER_MKEY AND DELETE_FLAG = 'N';";
                                var subtaskKeyValuePairs = await db.QueryAsync(sql, new { HEADER_MKEY = objOutPutApprovalTemplates.MKEY }, transaction: transaction);

                                // Assuming the model has a SUBTASK_LIST dictionary to hold these values
                                objOutPutApprovalTemplates.SUBTASK_LIST = new List<OUTPUT_APPROVAL_TEMPLATE_TRL_SUBTASK>();  // Assuming Subtask is a class for this data

                                foreach (var item in subtaskKeyValuePairs)
                                {
                                    objOutPutApprovalTemplates.SUBTASK_LIST.Add(new OUTPUT_APPROVAL_TEMPLATE_TRL_SUBTASK
                                    {
                                        HEADER_MKEY = item.HEADER_MKEY,
                                        SEQ_NO = item.SEQ_NO,
                                        SUBTASK_MKEY = item.SUBTASK_MKEY,
                                        SUBTASK_ABBR = item.SUBTASK_ABBR
                                    });
                                }
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
                                objOutPutApprovalTemplates.Status = "Error";
                                objOutPutApprovalTemplates.Message = rollbackEx.Message;
                                Console.WriteLine($"Rollback failed: {rollbackEx.Message}");
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
                        if (OBJ_APPROVAL_TEMPLATE_HDR.SANCTIONING_DEPARTMENT_LIST.Count > 0)
                        {
                            var SR_No = await db.QuerySingleAsync<int>("SELECT isnull(max(t.SR_NO),0) + 1 FROM APPROVAL_TEMPLATE_TRL_SANCTIONING_DEPARTMENT t" +
                                " WHERE MKEY = @MKEY AND DELETE_FLAG = 'N';", new { MKEY = objOutPutApprovalTemplates.MKEY }, commandType: CommandType.Text,
                                transaction: transaction);

                            // Populate the DataTable with subtasks
                            foreach (var SANCTIONING_DEPARTMENT in OBJ_APPROVAL_TEMPLATE_HDR.SANCTIONING_DEPARTMENT_LIST) // Assuming SUBTASK_LIST is a list of subtasks
                            {

                                SanctioningDataTable.Rows.Add(objOutPutApprovalTemplates.MKEY, SR_No, SANCTIONING_DEPARTMENT.LEVEL
                                    , SANCTIONING_DEPARTMENT.SANCTIONING_DEPARTMENT, SANCTIONING_DEPARTMENT.SANCTIONING_AUTHORITY
                                    , SANCTIONING_DEPARTMENT.START_DATE, SANCTIONING_DEPARTMENT.END_DATE == null ? null : SANCTIONING_DEPARTMENT.END_DATE, insertApprovalTemplates.CREATED_BY
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
                            string sql = "SELECT * from APPROVAL_TEMPLATE_TRL_SANCTIONING_DEPARTMENT WHERE MKEY = @MKEY " +
                                "AND DELETE_FLAG = 'N';";
                            var SANCTIONING_DEPARTMENT_TRL = await db.QueryAsync(sql, new { MKEY = objOutPutApprovalTemplates.MKEY }, transaction: transaction);

                            // Assuming the model has a SUBTASK_LIST dictionary to hold these values
                            objOutPutApprovalTemplates.SANCTIONING_DEPARTMENT_LIST = new List<OUTPUT_APPROVAL_TEMPLATE_TRL_SANCTIONING_DEPARTMENT>();  // Assuming Subtask is a class for this data

                            foreach (var item in SANCTIONING_DEPARTMENT_TRL)
                            {
                                objOutPutApprovalTemplates.SANCTIONING_DEPARTMENT_LIST.Add(new OUTPUT_APPROVAL_TEMPLATE_TRL_SANCTIONING_DEPARTMENT
                                {
                                    MKEY = item.MKEY,
                                    LEVEL = item.LEVEL,
                                    SANCTIONING_DEPARTMENT = item.SANCTIONING_DEPARTMENT,
                                    SANCTIONING_AUTHORITY = item.SANCTIONING_AUTHORITY,
                                    START_DATE = item.START_DATE,
                                    END_DATE = item.END_DATE
                                });
                            }
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
                                objOutPutApprovalTemplates.Status = "Error";
                                objOutPutApprovalTemplates.Message = rollbackEx.Message;
                                Console.WriteLine($"Rollback failed: {rollbackEx.Message}");
                            }
                        }
                    }
                    var sqlTransaction = (SqlTransaction)transaction;
                    await sqlTransaction.CommitAsync();
                    transactionCompleted = true;
                    return objOutPutApprovalTemplates;
                }
            }
            catch (SqlException ex)
            {
                var ErrorApprovalTemplates = new OutPutApprovalTemplates
                {
                    MKEY = 0,
                    Status = "Error",
                    Message = ex.Message
                };
                return ErrorApprovalTemplates;
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
                    }
                }
                var ErrorApprovalTemplates = new OutPutApprovalTemplates
                {
                    MKEY = 0,
                    Status = "Error",
                    Message = ex.Message
                };
                return ErrorApprovalTemplates;
            }


        }
        public async Task<ActionResult<IEnumerable<OutPutApprovalTemplates_NT>>> CreateApprovalTemplateAsyncNT(InsertApprovalTemplatesNT insertApprovalTemplates)
        {
            IDbTransaction transaction = null;
            bool transactionCompleted = false;
            if(insertApprovalTemplates.Mkey == 0)
            {
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

                        var OBJ_APPROVAL_TEMPLATE_HDR = insertApprovalTemplates;
                        var parameters = new DynamicParameters();
                        parameters.Add("@BUILDING_TYPE", insertApprovalTemplates.BUILDING_TYPE);
                        parameters.Add("@BUILDING_STANDARD", insertApprovalTemplates.BUILDING_STANDARD);
                        parameters.Add("@STATUTORY_AUTHORITY", insertApprovalTemplates.STATUTORY_AUTHORITY);
                        parameters.Add("@SHORT_DESCRIPTION", insertApprovalTemplates.SHORT_DESCRIPTION);
                        parameters.Add("@LONG_DESCRIPTION", insertApprovalTemplates.LONG_DESCRIPTION);
                        parameters.Add("@ABBR", insertApprovalTemplates.MAIN_ABBR);
                        parameters.Add("@APPROVAL_DEPARTMENT", insertApprovalTemplates.AUTHORITY_DEPARTMENT);
                        parameters.Add("@RESPOSIBLE_EMP_MKEY", insertApprovalTemplates.RESPOSIBLE_EMP_MKEY);
                        parameters.Add("@JOB_ROLE", insertApprovalTemplates.JOB_ROLE);
                        parameters.Add("@NO_DAYS_REQUIRED", insertApprovalTemplates.DAYS_REQUIERD);
                        parameters.Add("@SEQ_ORDER", insertApprovalTemplates.SEQ_ORDER);
                        parameters.Add("@TAGS", insertApprovalTemplates.TAGS);
                        parameters.Add("@CREATED_BY", insertApprovalTemplates.CREATED_BY);
                        parameters.Add("@TAGS", insertApprovalTemplates.TAGS);
                        parameters.Add("@Session_User_Id", insertApprovalTemplates.Session_User_Id);
                        parameters.Add("@Business_Group_Id", insertApprovalTemplates.Business_Group_Id);
                        var objOutPutApprovalTemplates = await db.QueryAsync<OutPutApprovalTemplatesNT>("SP_INSERT_APPROVAL_TEMPLATE_NT", parameters,
                            commandType: CommandType.StoredProcedure, transaction: transaction);
                        int MkeyApprTemp = 0;
                        if (!objOutPutApprovalTemplates.Any())
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
                                    //objOutPutApprovalTemplates.Status = "Error";
                                    //objOutPutApprovalTemplates.Message = rollbackEx.Message;
                                    //Console.WriteLine($"Rollback failed: {rollbackEx.Message}");

                                    var errorResult = new List<OutPutApprovalTemplates_NT>
                                {
                                    new OutPutApprovalTemplates_NT
                                    {
                                        Status = "Error",
                                        Message = $"SQL Error: {rollbackEx.Message}",
                                        Data = null
                                    }
                                };
                                    return errorResult;
                                }
                            }

                        }

                        foreach (var MKeyApp in objOutPutApprovalTemplates)
                        {
                            MkeyApprTemp = MKeyApp.MKEY;


                            DateTime dateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
                            try
                            {
                                // Create a DataTable for bulk insert END_RESULT_DOC_LST
                                var dataTable = new DataTable();
                                dataTable.Columns.Add("MKEY", typeof(int));
                                dataTable.Columns.Add("SR_NO", typeof(int));
                                dataTable.Columns.Add("DOCUMENT_NAME", typeof(string));
                                dataTable.Columns.Add("DOCUMENT_CATEGORY", typeof(string));
                                dataTable.Columns.Add("CREATED_BY", typeof(int));
                                dataTable.Columns.Add("CREATION_DATE", typeof(DateTime));
                                dataTable.Columns.Add("DELETE_FLAG", typeof(char));
                                dataTable.Columns.Add("Doc_Cat_Mkey", typeof(int));

                                if (OBJ_APPROVAL_TEMPLATE_HDR.END_RESULT_DOC_LST != null)
                                {
                                    var SR_No = await db.QuerySingleAsync<int>("SELECT isnull(max(t.SR_NO),0) + 1 FROM APPROVAL_TEMPLATE_TRL_ENDRESULT t " +
                                        "WHERE MKEY = @MKEY AND DELETE_FLAG = 'N';", new { MKEY = MkeyApprTemp }, commandType: CommandType.Text,
                                        transaction: transaction);
                                    // Populate the DataTable with product data
                                    foreach (var END_DOC_LIST in OBJ_APPROVAL_TEMPLATE_HDR.END_RESULT_DOC_LST)
                                    {
                                        dataTable.Rows.Add(MkeyApprTemp, SR_No, END_DOC_LIST.Key, END_DOC_LIST.Value,
                                            insertApprovalTemplates.CREATED_BY, dateTime.ToString("yyyy/MM/dd hh:mm:ss"), 'N');
                                        SR_No = SR_No + 1;
                                    }
                                    SR_No = 0;

                                    // Use SqlBulkCopy for bulk insert
                                    using var bulkCopy = new SqlBulkCopy(sqlConnection, SqlBulkCopyOptions.Default, (SqlTransaction)transaction)
                                    {
                                        DestinationTableName = "APPROVAL_TEMPLATE_TRL_ENDRESULT"
                                    };

                                    bulkCopy.ColumnMappings.Add("MKEY", "MKEY");
                                    bulkCopy.ColumnMappings.Add("SR_NO", "SR_NO");
                                    bulkCopy.ColumnMappings.Add("DOCUMENT_NAME", "DOCUMENT_NAME");
                                    bulkCopy.ColumnMappings.Add("DOCUMENT_CATEGORY", "DOCUMENT_CATEGORY");
                                    bulkCopy.ColumnMappings.Add("CREATED_BY", "CREATED_BY");
                                    bulkCopy.ColumnMappings.Add("CREATION_DATE", "CREATION_DATE");
                                    bulkCopy.ColumnMappings.Add("DELETE_FLAG", "DELETE_FLAG");
                                    bulkCopy.ColumnMappings.Add("Doc_Cat_Mkey", "Doc_Cat_Mkey");

                                    // Execute the bulk copy
                                    await bulkCopy.WriteToServerAsync(dataTable);

                                    /*
                                     * TO GET INSERTED VALUE IN END RESULT
                                     * */
                                    // Query the APPROVAL_TEMPLATE_TRL_CHECKLIST for key-value pairs

                                    string sql = "SELECT DOCUMENT_NAME, DOCUMENT_CATEGORY FROM APPROVAL_TEMPLATE_TRL_ENDRESULT WHERE MKEY = @MKEY AND DELETE_FLAG = 'N';";
                                    var keyValuePairs = await db.QueryAsync(sql, new { MKEY = MkeyApprTemp }, transaction: transaction);

                                    // Initialize the END_RESULT_DOC_LST dictionary
                                    MKeyApp.End_Result_Doc_Lst = new Dictionary<string, object>();

                                    // Populate the dictionary with the key-value pairs
                                    foreach (var item in keyValuePairs)
                                    {
                                        // Assuming DOCUMENT_NAME is the key and DOCUMENT_CATEGORY is the value
                                        MKeyApp.End_Result_Doc_Lst.Add(item.DOCUMENT_NAME.ToString(), item.DOCUMENT_CATEGORY);
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
                                        "WHERE MKEY = @MKEY AND DELETE_FLAG = 'N'", new { MKEY = MKeyApp.MKEY }, commandType: CommandType.Text
                                        , transaction: transaction);

                                    foreach (var CHECK_LIST in OBJ_APPROVAL_TEMPLATE_HDR.CHECKLIST_DOC_LST)
                                    {
                                        dataTable.Rows.Add(MKeyApp.MKEY, SR_No, CHECK_LIST.Key, CHECK_LIST.Value,
                                            insertApprovalTemplates.CREATED_BY, dateTime.ToString("yyyy/MM/dd hh:mm:ss"), 'N');
                                        SR_No = SR_No + 1;
                                    }
                                    SR_No = 0;

                                    // Use SqlBulkCopy for bulk insert
                                    using var bulkCopyCheckList = new SqlBulkCopy(sqlConnection, SqlBulkCopyOptions.Default, (SqlTransaction)transaction)
                                    {
                                        DestinationTableName = "APPROVAL_TEMPLATE_TRL_CHECKLIST"
                                    };

                                    bulkCopyCheckList.ColumnMappings.Add("MKEY", "MKEY");
                                    bulkCopyCheckList.ColumnMappings.Add("SR_NO", "SR_NO");
                                    bulkCopyCheckList.ColumnMappings.Add("DOCUMENT_NAME", "DOCUMENT_NAME");
                                    bulkCopyCheckList.ColumnMappings.Add("DOCUMENT_CATEGORY", "DOCUMENT_CATEGORY");
                                    bulkCopyCheckList.ColumnMappings.Add("CREATED_BY", "CREATED_BY");
                                    bulkCopyCheckList.ColumnMappings.Add("CREATION_DATE", "CREATION_DATE");
                                    bulkCopyCheckList.ColumnMappings.Add("DELETE_FLAG", "DELETE_FLAG");

                                    // Execute the bulk copy
                                    await bulkCopyCheckList.WriteToServerAsync(dataTable);

                                    // Commit transaction
                                    //await transactionCheckList.CommitAsync();


                                    // Query the APPROVAL_TEMPLATE_TRL_CHECKLIST for key-value pairs
                                    var sql = "SELECT DOCUMENT_NAME, DOCUMENT_CATEGORY FROM APPROVAL_TEMPLATE_TRL_CHECKLIST WHERE MKEY = @MKEY AND DELETE_FLAG = 'N';";
                                    var keyValuePairs = await db.QueryAsync(sql, new { MKEY = MKeyApp.MKEY }, transaction: transaction);

                                    // Initialize the END_RESULT_DOC_LST dictionary
                                    MKeyApp.Checklist_Doc_Lst = new Dictionary<string, object>();

                                    // Populate the dictionary with the key-value pairs
                                    foreach (var item in keyValuePairs)
                                    {
                                        // Assuming DOCUMENT_NAME is the key and DOCUMENT_CATEGORY is the value
                                        MKeyApp.Checklist_Doc_Lst.Add(item.DOCUMENT_NAME.ToString(), item.DOCUMENT_CATEGORY);
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
                                        //objOutPutApprovalTemplates.Status = "Error";
                                        //objOutPutApprovalTemplates.Message = rollbackEx.Message;
                                        //Console.WriteLine($"Rollback failed: {rollbackEx.Message}");
                                        var errorResult = new List<OutPutApprovalTemplates_NT>
                                     {
                                         new OutPutApprovalTemplates_NT
                                         {
                                             Status = "Error",
                                             Message = $"SQL Error: {rollbackEx.Message}",
                                             Data = null
                                         }
                                     };
                                        return errorResult;
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
                                subtaskDataTable.Columns.Add("CREATED_BY", typeof(int));
                                subtaskDataTable.Columns.Add("CREATION_DATE", typeof(DateTime));
                                subtaskDataTable.Columns.Add("DELETE_FLAG", typeof(char));

                                bool flagID = false;
                                if (OBJ_APPROVAL_TEMPLATE_HDR.SUBTASK_LIST.Any())
                                {
                                    if (OBJ_APPROVAL_TEMPLATE_HDR.SUBTASK_LIST != null && OBJ_APPROVAL_TEMPLATE_HDR.SUBTASK_LIST.Count > 0)
                                    {
                                        foreach (var subtask in OBJ_APPROVAL_TEMPLATE_HDR.SUBTASK_LIST) // Assuming SUBTASK_LIST is a list of subtasks
                                        {
                                            var parametersApproval = new DynamicParameters();
                                            parametersApproval.Add("@APPROVAL_MKEY", MKeyApp.MKEY);
                                            parametersApproval.Add("@MKEY", subtask.SUBTASK_MKEY);
                                            var ApprovalTemplates = await db.QueryFirstOrDefaultAsync<OutPutApprovalTemplates>("SP_GET_CHECK_APPROVAL", parametersApproval,
                                                commandType: CommandType.StoredProcedure, transaction: transaction);

                                            if (MKeyApp.MKEY == subtask.SUBTASK_MKEY || ApprovalTemplates.Status != "Ok")
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
                                                        var errorResultNT = new List<OutPutApprovalTemplates_NT>
                                                     {
                                                         new OutPutApprovalTemplates_NT
                                                         {
                                                             Status = "Error",
                                                             Message = $" Error: {rollbackEx.Message}",
                                                             Data = null
                                                         }
                                                     };
                                                        return errorResultNT;
                                                    }

                                                    var errorResult = new List<OutPutApprovalTemplates_NT>
                                                 {
                                                     new OutPutApprovalTemplates_NT
                                                     {
                                                         Status = "Error",
                                                         Message = $" Error: Approval Header Mkey and Sub Approval is same",
                                                         Data = null
                                                     }
                                                 };
                                                    return errorResult;

                                                }
                                            }
                                            else
                                            {
                                                subtaskDataTable.Rows.Add(MKeyApp.MKEY, subtask.SEQ_NO, subtask.SUBTASK_ABBR, subtask.SUBTASK_MKEY
                                                    , MKeyApp.MKEY, OBJ_APPROVAL_TEMPLATE_HDR.CREATED_BY
                                                    , dateTime.ToString("yyyy/MM/dd hh:mm:ss"), 'N');
                                            }
                                        }

                                        // Use SqlBulkCopy to insert subtasks
                                        using var bulkCopy = new SqlBulkCopy(sqlConnection, SqlBulkCopyOptions.Default, (SqlTransaction)transaction)
                                        {
                                            DestinationTableName = "APPROVAL_TEMPLATE_TRL_SUBTASK"  // Ensure this matches your table name
                                        };

                                        bulkCopy.ColumnMappings.Add("HEADER_MKEY", "HEADER_MKEY");
                                        bulkCopy.ColumnMappings.Add("SUBTASK_ABBR", "SUBTASK_ABBR");
                                        bulkCopy.ColumnMappings.Add("SEQ_NO", "SEQ_NO");
                                        bulkCopy.ColumnMappings.Add("SUBTASK_MKEY", "SUBTASK_MKEY");
                                        bulkCopy.ColumnMappings.Add("SUBTASK_PARENT_ID", "SUBTASK_PARENT_ID");
                                        bulkCopy.ColumnMappings.Add("CREATED_BY", "CREATED_BY");
                                        bulkCopy.ColumnMappings.Add("CREATION_DATE", "CREATION_DATE");
                                        bulkCopy.ColumnMappings.Add("DELETE_FLAG", "DELETE_FLAG");

                                        await bulkCopy.WriteToServerAsync(subtaskDataTable);

                                        // Commit the transactionSubTask
                                        //await transactionSubTask.CommitAsync();

                                        // Optionally, fetch the inserted values (if necessary)
                                        string sql = "SELECT HEADER_MKEY,SEQ_NO,SUBTASK_MKEY,SUBTASK_ABBR FROM APPROVAL_TEMPLATE_TRL_SUBTASK WHERE HEADER_MKEY = @HEADER_MKEY AND DELETE_FLAG = 'N';";
                                        var subtaskKeyValuePairs = await db.QueryAsync(sql, new { HEADER_MKEY = MKeyApp.MKEY }, transaction: transaction);

                                        // Assuming the model has a SUBTASK_LIST dictionary to hold these values
                                        MKeyApp.Subtask_List = new List<OUTPUT_APPROVAL_TEMPLATE_TRL_SUBTASK>();  // Assuming Subtask is a class for this data

                                        foreach (var item in subtaskKeyValuePairs)
                                        {
                                            MKeyApp.Subtask_List.Add(new OUTPUT_APPROVAL_TEMPLATE_TRL_SUBTASK
                                            {
                                                HEADER_MKEY = item.HEADER_MKEY,
                                                SEQ_NO = item.SEQ_NO,
                                                SUBTASK_MKEY = item.SUBTASK_MKEY,
                                                SUBTASK_ABBR = item.SUBTASK_ABBR
                                            });
                                        }
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
                                        var errorResult = new List<OutPutApprovalTemplates_NT>
                                     {
                                         new OutPutApprovalTemplates_NT
                                         {
                                             Status = "Error",
                                             Message = $"SQL Error: {rollbackEx.Message}",
                                             Data = null
                                         }
                                     };
                                        return errorResult;
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
                                if (OBJ_APPROVAL_TEMPLATE_HDR.SANCTIONING_DEPARTMENT_LIST.Count > 0)
                                {
                                    var SR_No = await db.QuerySingleAsync<int>("SELECT isnull(max(t.SR_NO),0) + 1 FROM APPROVAL_TEMPLATE_TRL_SANCTIONING_DEPARTMENT t" +
                                        " WHERE MKEY = @MKEY AND DELETE_FLAG = 'N';", new { MKEY = MKeyApp.MKEY }, commandType: CommandType.Text,
                                        transaction: transaction);

                                    // Populate the DataTable with subtasks
                                    foreach (var SANCTIONING_DEPARTMENT in OBJ_APPROVAL_TEMPLATE_HDR.SANCTIONING_DEPARTMENT_LIST) // Assuming SUBTASK_LIST is a list of subtasks
                                    {

                                        SanctioningDataTable.Rows.Add(MKeyApp.MKEY, SR_No, SANCTIONING_DEPARTMENT.LEVEL
                                            , SANCTIONING_DEPARTMENT.SANCTIONING_DEPARTMENT, SANCTIONING_DEPARTMENT.SANCTIONING_AUTHORITY
                                            , SANCTIONING_DEPARTMENT.START_DATE, SANCTIONING_DEPARTMENT.END_DATE == null ? null : SANCTIONING_DEPARTMENT.END_DATE, insertApprovalTemplates.CREATED_BY
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
                                    string sql = "SELECT * from APPROVAL_TEMPLATE_TRL_SANCTIONING_DEPARTMENT WHERE MKEY = @MKEY " +
                                        "AND DELETE_FLAG = 'N';";
                                    var SANCTIONING_DEPARTMENT_TRL = await db.QueryAsync(sql, new { MKEY = MKeyApp.MKEY }, transaction: transaction);

                                    // Assuming the model has a SUBTASK_LIST dictionary to hold these values
                                    MKeyApp.Sanctioning_Department_List = new List<OUTPUT_APPROVAL_TEMPLATE_TRL_SANCTIONING_DEPARTMENT>();  // Assuming Subtask is a class for this data

                                    foreach (var item in SANCTIONING_DEPARTMENT_TRL)
                                    {
                                        MKeyApp.Sanctioning_Department_List.Add(new OUTPUT_APPROVAL_TEMPLATE_TRL_SANCTIONING_DEPARTMENT
                                        {
                                            MKEY = item.MKEY,
                                            LEVEL = item.LEVEL,
                                            SANCTIONING_DEPARTMENT = item.SANCTIONING_DEPARTMENT,
                                            SANCTIONING_AUTHORITY = item.SANCTIONING_AUTHORITY,
                                            START_DATE = item.START_DATE,
                                            END_DATE = item.END_DATE
                                        });
                                    }
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
                                        //objOutPutApprovalTemplates.Status = "Error";
                                        //objOutPutApprovalTemplates.Message = rollbackEx.Message;
                                        //Console.WriteLine($"Rollback failed: {rollbackEx.Message}");
                                        var errorResult = new List<OutPutApprovalTemplates_NT>
                                     {
                                         new OutPutApprovalTemplates_NT
                                         {
                                             Status = "Error",
                                             Message = $" Error: {rollbackEx.Message}",
                                             Data = null
                                         }
                                     };
                                        return errorResult;
                                    }
                                }
                            }
                            var sqlTransaction = (SqlTransaction)transaction;
                            await sqlTransaction.CommitAsync();
                            transactionCompleted = true;

                            //var successsResult = new List<OutPutApprovalTemplates_NT>
                            //{
                            //    new OutPutApprovalTemplates_NT
                            //    {
                            //        Status = "Ok",
                            //        Message = "Get data successfully!!!",
                            //        Data = objOutPutApprovalTemplates
                            //    }
                            //};
                            //return successsResult;
                        }
                        var successsResult = new List<OutPutApprovalTemplates_NT>
                {
                    new OutPutApprovalTemplates_NT
                    {
                        Status = "Ok",
                        Message = "Insert data successfully!!!",
                        Data = objOutPutApprovalTemplates
                    }
                };
                        return successsResult;
                        // return objOutPutApprovalTemplates;
                    }

                }
                catch (SqlException ex)
                {
                    var errorResult = new List<OutPutApprovalTemplates_NT>
                 {
                     new OutPutApprovalTemplates_NT
                     {
                         Status = "Error",
                         Message = $" Error: {ex.Message}",
                         Data = null
                     }
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
                            // Handle rollback exception (may occur if transaction is already completed)
                            // Log or handle the rollback failure if needed
                            Console.WriteLine($"Rollback failed: {rollbackEx.Message}");
                        }
                    }
                    var errorResult = new List<OutPutApprovalTemplates_NT>
                 {
                     new OutPutApprovalTemplates_NT
                     {
                         Status = "Error",
                         Message = $" Error: {ex.Message}",
                         Data = null
                     }
                 };
                    return errorResult;
                }
            }
            else {
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

                        var OBJ_APPROVAL_TEMPLATE_HDR = insertApprovalTemplates;
                        var parameters = new DynamicParameters();
                        parameters.Add("@MKEY", insertApprovalTemplates.Mkey);
                        parameters.Add("@BUILDING_TYPE", insertApprovalTemplates.BUILDING_TYPE);
                        parameters.Add("@BUILDING_STANDARD", insertApprovalTemplates.BUILDING_STANDARD);
                        parameters.Add("@STATUTORY_AUTHORITY", insertApprovalTemplates.STATUTORY_AUTHORITY);
                        parameters.Add("@SHORT_DESCRIPTION", insertApprovalTemplates.SHORT_DESCRIPTION);
                        parameters.Add("@LONG_DESCRIPTION", insertApprovalTemplates.LONG_DESCRIPTION);
                        parameters.Add("@ABBR", insertApprovalTemplates.MAIN_ABBR);
                        parameters.Add("@APPROVAL_DEPARTMENT", insertApprovalTemplates.AUTHORITY_DEPARTMENT);
                        parameters.Add("@RESPOSIBLE_EMP_MKEY", insertApprovalTemplates.RESPOSIBLE_EMP_MKEY);
                        parameters.Add("@JOB_ROLE", insertApprovalTemplates.JOB_ROLE);
                        parameters.Add("@NO_DAYS_REQUIRED", insertApprovalTemplates.DAYS_REQUIERD);
                        parameters.Add("@SEQ_ORDER", insertApprovalTemplates.SEQ_ORDER);
                        parameters.Add("@TAGS", insertApprovalTemplates.TAGS);
                        parameters.Add("@LAST_UPDATED_BY", insertApprovalTemplates.CREATED_BY);

                        var objOutPutApprovalTemplates = await db.QueryAsync<OutPutApprovalTemplatesNT>("SP_UPDATE_APPROVAL_TEMPLATE", parameters,
                            commandType: CommandType.StoredProcedure, transaction: transaction);
                        int MkeyApprTemp = 0;
                        if (!objOutPutApprovalTemplates.Any())
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
                                    //objOutPutApprovalTemplates.Status = "Error";
                                    //objOutPutApprovalTemplates.Message = rollbackEx.Message;
                                    //Console.WriteLine($"Rollback failed: {rollbackEx.Message}");

                                    var errorResult = new List<OutPutApprovalTemplates_NT>
                                {
                                    new OutPutApprovalTemplates_NT
                                    {
                                        Status = "Error",
                                        Message = $"SQL Error: {rollbackEx.Message}",
                                        Data = null
                                    }
                                };
                                    return errorResult;
                                }
                            }

                        }

                        #region Insert END_RESULT_DOC_LST
                        var parametersTRL = new DynamicParameters();
                        parametersTRL.Add("@MKEY", insertApprovalTemplates.Mkey);
                        parametersTRL.Add("@LOGGED_IN", insertApprovalTemplates.CREATED_BY);
                        parametersTRL.Add("@STATUS", null);
                        var DeleteApprovalTrl = await db.QueryFirstOrDefaultAsync<dynamic>("SP_DELETE_APPROVAL_TEMPLATE_TRL", parametersTRL, commandType: CommandType.StoredProcedure, transaction: transaction);
                        #endregion

                        foreach (var MKeyApp in objOutPutApprovalTemplates)
                        {
                            MkeyApprTemp = MKeyApp.MKEY;


                            DateTime dateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
                            try
                            {
                                // Create a DataTable for bulk insert END_RESULT_DOC_LST
                                var dataTable = new DataTable();
                                dataTable.Columns.Add("MKEY", typeof(int));
                                dataTable.Columns.Add("SR_NO", typeof(int));
                                dataTable.Columns.Add("DOCUMENT_NAME", typeof(string));
                                dataTable.Columns.Add("DOCUMENT_CATEGORY", typeof(string));
                                dataTable.Columns.Add("CREATED_BY", typeof(int));
                                dataTable.Columns.Add("CREATION_DATE", typeof(DateTime));
                                dataTable.Columns.Add("DELETE_FLAG", typeof(char));

                                if (OBJ_APPROVAL_TEMPLATE_HDR.END_RESULT_DOC_LST != null)
                                {
                                    var SR_No = await db.QuerySingleAsync<int>("SELECT isnull(max(t.SR_NO),0) + 1 FROM APPROVAL_TEMPLATE_TRL_ENDRESULT t " +
                                        "WHERE MKEY = @MKEY AND DELETE_FLAG = 'N';", new { MKEY = MkeyApprTemp }, commandType: CommandType.Text,
                                        transaction: transaction);
                                    // Populate the DataTable with product data
                                    foreach (var END_DOC_LIST in OBJ_APPROVAL_TEMPLATE_HDR.END_RESULT_DOC_LST)
                                    {
                                        dataTable.Rows.Add(MkeyApprTemp, SR_No, END_DOC_LIST.Key, END_DOC_LIST.Value,
                                            insertApprovalTemplates.CREATED_BY, dateTime.ToString("yyyy/MM/dd hh:mm:ss"), 'N');
                                        SR_No = SR_No + 1;
                                    }
                                    SR_No = 0;

                                    // Use SqlBulkCopy for bulk insert
                                    using var bulkCopy = new SqlBulkCopy(sqlConnection, SqlBulkCopyOptions.Default, (SqlTransaction)transaction)
                                    {
                                        DestinationTableName = "APPROVAL_TEMPLATE_TRL_ENDRESULT"
                                    };

                                    bulkCopy.ColumnMappings.Add("MKEY", "MKEY");
                                    bulkCopy.ColumnMappings.Add("SR_NO", "SR_NO");
                                    bulkCopy.ColumnMappings.Add("DOCUMENT_NAME", "DOCUMENT_NAME");
                                    bulkCopy.ColumnMappings.Add("DOCUMENT_CATEGORY", "DOCUMENT_CATEGORY");
                                    bulkCopy.ColumnMappings.Add("CREATED_BY", "CREATED_BY");
                                    bulkCopy.ColumnMappings.Add("CREATION_DATE", "CREATION_DATE");
                                    bulkCopy.ColumnMappings.Add("DELETE_FLAG", "DELETE_FLAG");

                                    // Execute the bulk copy
                                    await bulkCopy.WriteToServerAsync(dataTable);

                                    /*
                                     * TO GET INSERTED VALUE IN END RESULT
                                     * */
                                    // Query the APPROVAL_TEMPLATE_TRL_CHECKLIST for key-value pairs

                                    string sql = "SELECT DOCUMENT_NAME, DOCUMENT_CATEGORY FROM APPROVAL_TEMPLATE_TRL_ENDRESULT WHERE MKEY = @MKEY AND DELETE_FLAG = 'N';";
                                    var keyValuePairs = await db.QueryAsync(sql, new { MKEY = MkeyApprTemp }, transaction: transaction);

                                    // Initialize the END_RESULT_DOC_LST dictionary
                                    MKeyApp.End_Result_Doc_Lst = new Dictionary<string, object>();

                                    // Populate the dictionary with the key-value pairs
                                    foreach (var item in keyValuePairs)
                                    {
                                        // Assuming DOCUMENT_NAME is the key and DOCUMENT_CATEGORY is the value
                                        MKeyApp.End_Result_Doc_Lst.Add(item.DOCUMENT_NAME.ToString(), item.DOCUMENT_CATEGORY);
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
                                        "WHERE MKEY = @MKEY AND DELETE_FLAG = 'N'", new { MKEY = MKeyApp.MKEY }, commandType: CommandType.Text
                                        , transaction: transaction);

                                    foreach (var CHECK_LIST in OBJ_APPROVAL_TEMPLATE_HDR.CHECKLIST_DOC_LST)
                                    {
                                        dataTable.Rows.Add(MKeyApp.MKEY, SR_No, CHECK_LIST.Key, CHECK_LIST.Value,
                                            insertApprovalTemplates.CREATED_BY, dateTime.ToString("yyyy/MM/dd hh:mm:ss"), 'N');
                                        SR_No = SR_No + 1;
                                    }
                                    SR_No = 0;

                                    // Use SqlBulkCopy for bulk insert
                                    using var bulkCopyCheckList = new SqlBulkCopy(sqlConnection, SqlBulkCopyOptions.Default, (SqlTransaction)transaction)
                                    {
                                        DestinationTableName = "APPROVAL_TEMPLATE_TRL_CHECKLIST"
                                    };

                                    bulkCopyCheckList.ColumnMappings.Add("MKEY", "MKEY");
                                    bulkCopyCheckList.ColumnMappings.Add("SR_NO", "SR_NO");
                                    bulkCopyCheckList.ColumnMappings.Add("DOCUMENT_NAME", "DOCUMENT_NAME");
                                    bulkCopyCheckList.ColumnMappings.Add("DOCUMENT_CATEGORY", "DOCUMENT_CATEGORY");
                                    bulkCopyCheckList.ColumnMappings.Add("CREATED_BY", "CREATED_BY");
                                    bulkCopyCheckList.ColumnMappings.Add("CREATION_DATE", "CREATION_DATE");
                                    bulkCopyCheckList.ColumnMappings.Add("DELETE_FLAG", "DELETE_FLAG");

                                    // Execute the bulk copy
                                    await bulkCopyCheckList.WriteToServerAsync(dataTable);

                                    // Commit transaction
                                    //await transactionCheckList.CommitAsync();


                                    // Query the APPROVAL_TEMPLATE_TRL_CHECKLIST for key-value pairs
                                    var sql = "SELECT DOCUMENT_NAME, DOCUMENT_CATEGORY FROM APPROVAL_TEMPLATE_TRL_CHECKLIST WHERE MKEY = @MKEY AND DELETE_FLAG = 'N';";
                                    var keyValuePairs = await db.QueryAsync(sql, new { MKEY = MKeyApp.MKEY }, transaction: transaction);

                                    // Initialize the END_RESULT_DOC_LST dictionary
                                    MKeyApp.Checklist_Doc_Lst = new Dictionary<string, object>();

                                    // Populate the dictionary with the key-value pairs
                                    foreach (var item in keyValuePairs)
                                    {
                                        // Assuming DOCUMENT_NAME is the key and DOCUMENT_CATEGORY is the value
                                        MKeyApp.Checklist_Doc_Lst.Add(item.DOCUMENT_NAME.ToString(), item.DOCUMENT_CATEGORY);
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
                                        //objOutPutApprovalTemplates.Status = "Error";
                                        //objOutPutApprovalTemplates.Message = rollbackEx.Message;
                                        //Console.WriteLine($"Rollback failed: {rollbackEx.Message}");
                                        var errorResult = new List<OutPutApprovalTemplates_NT>
                                     {
                                         new OutPutApprovalTemplates_NT
                                         {
                                             Status = "Error",
                                             Message = $"SQL Error: {rollbackEx.Message}",
                                             Data = null
                                         }
                                     };
                                        return errorResult;
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
                                subtaskDataTable.Columns.Add("CREATED_BY", typeof(int));
                                subtaskDataTable.Columns.Add("CREATION_DATE", typeof(DateTime));
                                subtaskDataTable.Columns.Add("DELETE_FLAG", typeof(char));

                                bool flagID = false;
                                if (OBJ_APPROVAL_TEMPLATE_HDR.SUBTASK_LIST.Any())
                                {
                                    if (OBJ_APPROVAL_TEMPLATE_HDR.SUBTASK_LIST != null && OBJ_APPROVAL_TEMPLATE_HDR.SUBTASK_LIST.Count > 0)
                                    {
                                        foreach (var subtask in OBJ_APPROVAL_TEMPLATE_HDR.SUBTASK_LIST) // Assuming SUBTASK_LIST is a list of subtasks
                                        {
                                            var parametersApproval = new DynamicParameters();
                                            parametersApproval.Add("@APPROVAL_MKEY", MKeyApp.MKEY);
                                            parametersApproval.Add("@MKEY", subtask.SUBTASK_MKEY);
                                            var ApprovalTemplates = await db.QueryFirstOrDefaultAsync<OutPutApprovalTemplates>("SP_GET_CHECK_APPROVAL", parametersApproval,
                                                commandType: CommandType.StoredProcedure, transaction: transaction);

                                            if (MKeyApp.MKEY == subtask.SUBTASK_MKEY || ApprovalTemplates.Status != "Ok")
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
                                                        var errorResultNT = new List<OutPutApprovalTemplates_NT>
                                                     {
                                                         new OutPutApprovalTemplates_NT
                                                         {
                                                             Status = "Error",
                                                             Message = $" Error: {rollbackEx.Message}",
                                                             Data = null
                                                         }
                                                     };
                                                        return errorResultNT;
                                                    }

                                                    var errorResult = new List<OutPutApprovalTemplates_NT>
                                                 {
                                                     new OutPutApprovalTemplates_NT
                                                     {
                                                         Status = "Error",
                                                         Message = $" Error: Approval Header Mkey and Sub Approval is same",
                                                         Data = null
                                                     }
                                                 };
                                                    return errorResult;

                                                }
                                            }
                                            else
                                            {
                                                subtaskDataTable.Rows.Add(MKeyApp.MKEY, subtask.SEQ_NO, subtask.SUBTASK_ABBR, subtask.SUBTASK_MKEY
                                                    , MKeyApp.MKEY, OBJ_APPROVAL_TEMPLATE_HDR.CREATED_BY
                                                    , dateTime.ToString("yyyy/MM/dd hh:mm:ss"), 'N');
                                            }
                                        }

                                        // Use SqlBulkCopy to insert subtasks
                                        using var bulkCopy = new SqlBulkCopy(sqlConnection, SqlBulkCopyOptions.Default, (SqlTransaction)transaction)
                                        {
                                            DestinationTableName = "APPROVAL_TEMPLATE_TRL_SUBTASK"  // Ensure this matches your table name
                                        };

                                        bulkCopy.ColumnMappings.Add("HEADER_MKEY", "HEADER_MKEY");
                                        bulkCopy.ColumnMappings.Add("SUBTASK_ABBR", "SUBTASK_ABBR");
                                        bulkCopy.ColumnMappings.Add("SEQ_NO", "SEQ_NO");
                                        bulkCopy.ColumnMappings.Add("SUBTASK_MKEY", "SUBTASK_MKEY");
                                        bulkCopy.ColumnMappings.Add("SUBTASK_PARENT_ID", "SUBTASK_PARENT_ID");
                                        bulkCopy.ColumnMappings.Add("CREATED_BY", "CREATED_BY");
                                        bulkCopy.ColumnMappings.Add("CREATION_DATE", "CREATION_DATE");
                                        bulkCopy.ColumnMappings.Add("DELETE_FLAG", "DELETE_FLAG");

                                        await bulkCopy.WriteToServerAsync(subtaskDataTable);

                                        // Commit the transactionSubTask
                                        //await transactionSubTask.CommitAsync();

                                        // Optionally, fetch the inserted values (if necessary)
                                        string sql = "SELECT HEADER_MKEY,SEQ_NO,SUBTASK_MKEY,SUBTASK_ABBR FROM APPROVAL_TEMPLATE_TRL_SUBTASK WHERE HEADER_MKEY = @HEADER_MKEY AND DELETE_FLAG = 'N';";
                                        var subtaskKeyValuePairs = await db.QueryAsync(sql, new { HEADER_MKEY = MKeyApp.MKEY }, transaction: transaction);

                                        // Assuming the model has a SUBTASK_LIST dictionary to hold these values
                                        MKeyApp.Subtask_List = new List<OUTPUT_APPROVAL_TEMPLATE_TRL_SUBTASK>();  // Assuming Subtask is a class for this data

                                        foreach (var item in subtaskKeyValuePairs)
                                        {
                                            MKeyApp.Subtask_List.Add(new OUTPUT_APPROVAL_TEMPLATE_TRL_SUBTASK
                                            {
                                                HEADER_MKEY = item.HEADER_MKEY,
                                                SEQ_NO = item.SEQ_NO,
                                                SUBTASK_MKEY = item.SUBTASK_MKEY,
                                                SUBTASK_ABBR = item.SUBTASK_ABBR
                                            });
                                        }
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
                                        var errorResult = new List<OutPutApprovalTemplates_NT>
                                     {
                                         new OutPutApprovalTemplates_NT
                                         {
                                             Status = "Error",
                                             Message = $"SQL Error: {rollbackEx.Message}",
                                             Data = null
                                         }
                                     };
                                        return errorResult;
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
                                if (OBJ_APPROVAL_TEMPLATE_HDR.SANCTIONING_DEPARTMENT_LIST.Count > 0)
                                {
                                    var SR_No = await db.QuerySingleAsync<int>("SELECT isnull(max(t.SR_NO),0) + 1 FROM APPROVAL_TEMPLATE_TRL_SANCTIONING_DEPARTMENT t" +
                                        " WHERE MKEY = @MKEY AND DELETE_FLAG = 'N';", new { MKEY = MKeyApp.MKEY }, commandType: CommandType.Text,
                                        transaction: transaction);

                                    // Populate the DataTable with subtasks
                                    foreach (var SANCTIONING_DEPARTMENT in OBJ_APPROVAL_TEMPLATE_HDR.SANCTIONING_DEPARTMENT_LIST) // Assuming SUBTASK_LIST is a list of subtasks
                                    {

                                        SanctioningDataTable.Rows.Add(MKeyApp.MKEY, SR_No, SANCTIONING_DEPARTMENT.LEVEL
                                            , SANCTIONING_DEPARTMENT.SANCTIONING_DEPARTMENT, SANCTIONING_DEPARTMENT.SANCTIONING_AUTHORITY
                                            , SANCTIONING_DEPARTMENT.START_DATE, SANCTIONING_DEPARTMENT.END_DATE == null ? null : SANCTIONING_DEPARTMENT.END_DATE, insertApprovalTemplates.CREATED_BY
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
                                    string sql = "SELECT * from APPROVAL_TEMPLATE_TRL_SANCTIONING_DEPARTMENT WHERE MKEY = @MKEY " +
                                        "AND DELETE_FLAG = 'N';";
                                    var SANCTIONING_DEPARTMENT_TRL = await db.QueryAsync(sql, new { MKEY = MKeyApp.MKEY }, transaction: transaction);

                                    // Assuming the model has a SUBTASK_LIST dictionary to hold these values
                                    MKeyApp.Sanctioning_Department_List = new List<OUTPUT_APPROVAL_TEMPLATE_TRL_SANCTIONING_DEPARTMENT>();  // Assuming Subtask is a class for this data

                                    foreach (var item in SANCTIONING_DEPARTMENT_TRL)
                                    {
                                        MKeyApp.Sanctioning_Department_List.Add(new OUTPUT_APPROVAL_TEMPLATE_TRL_SANCTIONING_DEPARTMENT
                                        {
                                            MKEY = item.MKEY,
                                            LEVEL = item.LEVEL,
                                            SANCTIONING_DEPARTMENT = item.SANCTIONING_DEPARTMENT,
                                            SANCTIONING_AUTHORITY = item.SANCTIONING_AUTHORITY,
                                            START_DATE = item.START_DATE,
                                            END_DATE = item.END_DATE
                                        });
                                    }
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
                                        //objOutPutApprovalTemplates.Status = "Error";
                                        //objOutPutApprovalTemplates.Message = rollbackEx.Message;
                                        //Console.WriteLine($"Rollback failed: {rollbackEx.Message}");
                                        var errorResult = new List<OutPutApprovalTemplates_NT>
                                     {
                                         new OutPutApprovalTemplates_NT
                                         {
                                             Status = "Error",
                                             Message = $" Error: {rollbackEx.Message}",
                                             Data = null
                                         }
                                     };
                                        return errorResult;
                                    }
                                }
                            }
                            var sqlTransaction = (SqlTransaction)transaction;
                            await sqlTransaction.CommitAsync();
                            transactionCompleted = true;

                            //var successsResult = new List<OutPutApprovalTemplates_NT>
                            //{
                            //    new OutPutApprovalTemplates_NT
                            //    {
                            //        Status = "Ok",
                            //        Message = "Get data successfully!!!",
                            //        Data = objOutPutApprovalTemplates
                            //    }
                            //};
                            //return successsResult;
                        }
                        var successsResult = new List<OutPutApprovalTemplates_NT>
                            {
                                new OutPutApprovalTemplates_NT
                                {
                                    Status = "Ok",
                                    Message = "Insert data successfully!!!",
                                    Data = objOutPutApprovalTemplates
                                }
                            };
                        return successsResult;
                        // return objOutPutApprovalTemplates;
                    }

                }
                catch (SqlException ex)
                {
                    var errorResult = new List<OutPutApprovalTemplates_NT>
                 {
                     new OutPutApprovalTemplates_NT
                     {
                         Status = "Error",
                         Message = $" Error: {ex.Message}",
                         Data = null
                     }
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
                            // Handle rollback exception (may occur if transaction is already completed)
                            // Log or handle the rollback failure if needed
                            Console.WriteLine($"Rollback failed: {rollbackEx.Message}");
                        }
                    }
                    var errorResult = new List<OutPutApprovalTemplates_NT>
                 {
                     new OutPutApprovalTemplates_NT
                     {
                         Status = "Error",
                         Message = $" Error: {ex.Message}",
                         Data = null
                     }
                 };
                    return errorResult;
                }
            }
            
        }
        public async Task<APPROVAL_TEMPLATE_HDR> CheckABBRAsync(string ABBR)
        {
            try
            {
                using (IDbConnection db = _dapperDbConnection.CreateConnection())
                {
                    return await db.QueryFirstOrDefaultAsync<APPROVAL_TEMPLATE_HDR>("SELECT HDR.MKEY,BUILDING_TYPE,BUILDING_STANDARD" +
                        ",STATUTORY_AUTHORITY, SHORT_DESCRIPTION,LONG_DESCRIPTION,MAIN_ABBR,AUTHORITY_DEPARTMENT,RESPOSIBLE_EMP_MKEY" +
                        ",JOB_ROLE,DAYS_REQUIERD,HDR.SEQ_ORDER,HDR.ATTRIBUTE1,HDR.ATTRIBUTE2,HDR.ATTRIBUTE3,HDR.ATTRIBUTE4,HDR.ATTRIBUTE5,HDR.CREATED_BY" +
                        ",HDR.CREATION_DATE,HDR.LAST_UPDATED_BY,HDR.LAST_UPDATE_DATE,SANCTION_AUTHORITY, SANCTION_DEPARTMENT,END_RESULT_DOC" +
                        ",CHECKLIST_DOC,HDR.DELETE_FLAG  FROM APPROVAL_TEMPLATE_HDR HDR INNER JOIN APPROVAL_TEMPLATE_TRL_SUBTASK TRL_SUB " +
                        "ON HDR.MKEY = TRL_SUB.HEADER_MKEY WHERE LOWER(MAIN_ABBR) = LOWER(@ABBR) OR  LOWER(SUBTASK_ABBR) = LOWER(@ABBR)" +
                        " AND HDR.DELETE_FLAG = 'N' " +
                        "AND TRL_SUB.DELETE_FLAG = 'N' ", new { ABBR = ABBR });
                }
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        public async Task<ActionResult<IEnumerable<APPROVAL_TEMPLATE_HDR_NT_OUTPUT>>> CheckABBRAsyncNT(APPROVAL_TEMPLATE_HDR_INPUT aPPROVAL_TEMPLATE_HDR_INPUT)
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
                    parmeters.Add("@Session_User_Id", aPPROVAL_TEMPLATE_HDR_INPUT.Session_User_Id);
                    parmeters.Add("@Business_Group_Id", aPPROVAL_TEMPLATE_HDR_INPUT.Business_Group_Id);
                    parmeters.Add("@PropertyMkey", aPPROVAL_TEMPLATE_HDR_INPUT.strABBR);

                    var TaskDashFilter = await db.QueryAsync<APPROVAL_TEMPLATE_HDR_NT>("SP_GET_APPROVAL_TEMPLATE_ABBR_NT", parmeters, commandType: CommandType.StoredProcedure, transaction: transaction);

                    var sqlTransaction = (SqlTransaction)transaction;
                    await sqlTransaction.CommitAsync();
                    transactionCompleted = true;

                    var successsResult = new List<APPROVAL_TEMPLATE_HDR_NT_OUTPUT>
                    {
                        new APPROVAL_TEMPLATE_HDR_NT_OUTPUT
                        {
                            Status = "Ok",
                            Message = "Get data successfully!!!",
                            Data = TaskDashFilter
                        }
                    };
                    return successsResult;
                }
            }
            catch (SqlException sqlEx)
            {
                // Handle SQL exceptions specifically
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

                // Log the SQL error
                var errorResult = new List<APPROVAL_TEMPLATE_HDR_NT_OUTPUT>
                {
                    new APPROVAL_TEMPLATE_HDR_NT_OUTPUT
                    {
                        Status = "Error",
                        Message = $"SQL Error: {sqlEx.Message}",
                        Data = null
                    }
                };
                return errorResult;
            }
            catch (Exception ex)
            {
                // Generic error handling for non-SQL related issues
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

                // Log the generic error
                var errorResult = new List<APPROVAL_TEMPLATE_HDR_NT_OUTPUT>
                {
                    new APPROVAL_TEMPLATE_HDR_NT_OUTPUT
                    {
                        Status = "Error",
                        Message = $"Error: {ex.Message}",
                        Data = null
                    }
                };
                return errorResult;
            }
            finally
            {
                // Ensure transaction is committed or rolled back appropriately
                if (transaction != null && !transactionCompleted)
                {
                    try
                    {
                        transaction.Rollback();  // Rollback in case of any issues
                    }
                    catch (Exception rollbackEx)
                    {
                        Console.WriteLine($"Final rollback failed: {rollbackEx.Message}");
                    }
                }
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
        public async Task<ActionResult<IEnumerable<APPROVAL_TEMPLATE_HDR_NT_OUTPUT>>> AbbrAndShortDescAsyncNT(GetAbbrAndShortAbbrOutPutNT getAbbrAndShortAbbrOutPutNT)
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
                    parmeters.Add("@BUILDING_TYPE", getAbbrAndShortAbbrOutPutNT.Building);
                    parmeters.Add("@BUILDING_STANDARD", getAbbrAndShortAbbrOutPutNT.Standard);
                    parmeters.Add("@STATUTORY_AUTHORITY", getAbbrAndShortAbbrOutPutNT.Authority);
                    parmeters.Add("@Session_User_Id", getAbbrAndShortAbbrOutPutNT.Session_User_Id);
                    parmeters.Add("@Business_Group_Id", getAbbrAndShortAbbrOutPutNT.Business_Group_Id);

                    var TaskDashFilter = await db.QueryAsync<APPROVAL_TEMPLATE_HDR_NT>("SP_GET_ABBR_AND_SHORT_NT", parmeters, commandType: CommandType.StoredProcedure, transaction: transaction);

                    var sqlTransaction = (SqlTransaction)transaction;
                    await sqlTransaction.CommitAsync();
                    transactionCompleted = true;

                    var successsResult = new List<APPROVAL_TEMPLATE_HDR_NT_OUTPUT>
                    {
                        new APPROVAL_TEMPLATE_HDR_NT_OUTPUT
                        {
                            Status = "Ok",
                            Message = "Get data successfully!!!",
                            Data = TaskDashFilter
                        }
                    };
                    return successsResult;
                }
            }
            catch (SqlException sqlEx)
            {
                // Handle SQL exceptions specifically
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

                // Log the SQL error
                var errorResult = new List<APPROVAL_TEMPLATE_HDR_NT_OUTPUT>
                {
                    new APPROVAL_TEMPLATE_HDR_NT_OUTPUT
                    {
                        Status = "Error",
                        Message = $"SQL Error: {sqlEx.Message}",
                        Data = null
                    }
                };
                return errorResult;
            }
            catch (Exception ex)
            {
                // Generic error handling for non-SQL related issues
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

                // Log the generic error
                var errorResult = new List<APPROVAL_TEMPLATE_HDR_NT_OUTPUT>
                {
                    new APPROVAL_TEMPLATE_HDR_NT_OUTPUT
                    {
                        Status = "Error",
                        Message = $"Error: {ex.Message}",
                        Data = null
                    }
                };
                return errorResult;
            }
            finally
            {
                // Ensure transaction is committed or rolled back appropriately
                if (transaction != null && !transactionCompleted)
                {
                    try
                    {
                        transaction.Rollback();  // Rollback in case of any issues
                    }
                    catch (Exception rollbackEx)
                    {
                        Console.WriteLine($"Final rollback failed: {rollbackEx.Message}");
                    }
                }
            }
        }
        public async Task<ActionResult<OutPutApprovalTemplates>> UpdateApprovalTemplateAsync(UpdateApprovalTemplates updateApprovalTemplates)
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
                        var approvalTemplate = new OutPutApprovalTemplates();
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
                    parameters.Add("@MKEY", updateApprovalTemplates.MKEY);
                    parameters.Add("@BUILDING_TYPE", updateApprovalTemplates.BUILDING_TYPE);
                    parameters.Add("@BUILDING_STANDARD", updateApprovalTemplates.BUILDING_STANDARD);
                    parameters.Add("@STATUTORY_AUTHORITY", updateApprovalTemplates.STATUTORY_AUTHORITY);
                    parameters.Add("@SHORT_DESCRIPTION", updateApprovalTemplates.SHORT_DESCRIPTION);
                    parameters.Add("@LONG_DESCRIPTION", updateApprovalTemplates.LONG_DESCRIPTION);
                    parameters.Add("@ABBR", updateApprovalTemplates.MAIN_ABBR);
                    parameters.Add("@APPROVAL_DEPARTMENT", updateApprovalTemplates.AUTHORITY_DEPARTMENT);
                    parameters.Add("@RESPOSIBLE_EMP_MKEY", updateApprovalTemplates.RESPOSIBLE_EMP_MKEY);
                    parameters.Add("@JOB_ROLE", updateApprovalTemplates.JOB_ROLE);
                    parameters.Add("@NO_DAYS_REQUIRED", updateApprovalTemplates.DAYS_REQUIERD);
                    parameters.Add("@SEQ_ORDER", updateApprovalTemplates.SEQ_ORDER);
                    parameters.Add("@TAGS", updateApprovalTemplates.TAGS);
                    parameters.Add("@LAST_UPDATED_BY", updateApprovalTemplates.CREATED_BY);

                    // Execute stored procedure to insert into updateApprovalTemplates
                    var objAPPROVAL_TEMPLATE_HDR = await db.QueryFirstOrDefaultAsync<OutPutApprovalTemplates>("SP_UPDATE_APPROVAL_TEMPLATE", parameters,
                        commandType: CommandType.StoredProcedure, transaction: transaction);

                    if (objAPPROVAL_TEMPLATE_HDR == null)
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

                        var ErrorapprovalTemplate = new OutPutApprovalTemplates();
                        ErrorapprovalTemplate.Status = "Error";
                        ErrorapprovalTemplate.Message = "Failed to insert.";
                        return ErrorapprovalTemplate;
                    }

                    #region Insert END_RESULT_DOC_LST
                    var parametersTRL = new DynamicParameters();
                    parametersTRL.Add("@MKEY", updateApprovalTemplates.MKEY);
                    parametersTRL.Add("@LOGGED_IN", updateApprovalTemplates.CREATED_BY);
                    parametersTRL.Add("@STATUS", null);
                    var DeleteApprovalTrl = await db.QueryFirstOrDefaultAsync<dynamic>("SP_DELETE_APPROVAL_TEMPLATE_TRL", parametersTRL, commandType: CommandType.StoredProcedure, transaction: transaction);

                    //if (DeleteApprovalTrl  )
                    if (updateApprovalTemplates.END_RESULT_DOC_LST != null)
                    {
                        var dataTable = new DataTable();
                        dataTable.Columns.Add("MKEY", typeof(int));
                        dataTable.Columns.Add("SR_NO", typeof(int));
                        dataTable.Columns.Add("DOCUMENT_NAME", typeof(string));
                        dataTable.Columns.Add("DOCUMENT_CATEGORY", typeof(string));
                        dataTable.Columns.Add("LAST_UPDATED_BY", typeof(int));
                        dataTable.Columns.Add("LAST_UPDATE_DATE", typeof(DateTime));
                        dataTable.Columns.Add("DELETE_FLAG", typeof(char));

                        var SR_No = await db.QuerySingleAsync<int>("SELECT isnull(max(t.SR_NO), 0) + 1 FROM APPROVAL_TEMPLATE_TRL_ENDRESULT t" +
                            " WHERE MKEY = @MKEY " +
                            "AND DELETE_FLAG = 'N';",
                                                                   new { MKEY = objAPPROVAL_TEMPLATE_HDR.MKEY },
                                                                   commandType: CommandType.Text,
                                                                   transaction: transaction);

                        foreach (var END_DOC_LIST in updateApprovalTemplates.END_RESULT_DOC_LST)
                        {
                            dataTable.Rows.Add(objAPPROVAL_TEMPLATE_HDR.MKEY, SR_No, END_DOC_LIST.Key, END_DOC_LIST.Value,
                                               updateApprovalTemplates.CREATED_BY, dateTime, 'N');
                            SR_No++;
                        }

                        using var bulkCopy = new SqlBulkCopy(sqlConnection, SqlBulkCopyOptions.Default, (SqlTransaction)transaction)
                        {
                            DestinationTableName = "APPROVAL_TEMPLATE_TRL_ENDRESULT"
                        };

                        bulkCopy.ColumnMappings.Add("MKEY", "MKEY");
                        bulkCopy.ColumnMappings.Add("SR_NO", "SR_NO");
                        bulkCopy.ColumnMappings.Add("DOCUMENT_NAME", "DOCUMENT_NAME");
                        bulkCopy.ColumnMappings.Add("DOCUMENT_CATEGORY", "DOCUMENT_CATEGORY");
                        bulkCopy.ColumnMappings.Add("LAST_UPDATED_BY", "LAST_UPDATED_BY");
                        bulkCopy.ColumnMappings.Add("LAST_UPDATE_DATE", "LAST_UPDATE_DATE");
                        bulkCopy.ColumnMappings.Add("DELETE_FLAG", "DELETE_FLAG");


                        await bulkCopy.WriteToServerAsync(dataTable);

                        string sql = "SELECT DOCUMENT_NAME, DOCUMENT_CATEGORY FROM APPROVAL_TEMPLATE_TRL_ENDRESULT WHERE MKEY = @MKEY AND DELETE_FLAG = 'N';";
                        var keyValuePairs = await db.QueryAsync(sql, new { MKEY = objAPPROVAL_TEMPLATE_HDR.MKEY }, transaction: transaction);

                        // Initialize the END_RESULT_DOC_LST dictionary
                        objAPPROVAL_TEMPLATE_HDR.END_RESULT_DOC_LST = new Dictionary<string, object>();

                        // Populate the dictionary with the key-value pairs
                        foreach (var item in keyValuePairs)
                        {
                            // Assuming DOCUMENT_NAME is the key and DOCUMENT_CATEGORY is the value
                            objAPPROVAL_TEMPLATE_HDR.END_RESULT_DOC_LST.Add(item.DOCUMENT_NAME.ToString(), item.DOCUMENT_CATEGORY);
                        }

                    }
                    #endregion

                    #region Insert CHECKLIST_DOC_LST
                    if (updateApprovalTemplates.CHECKLIST_DOC_LST != null)
                    {
                        var dataTable = new DataTable();
                        dataTable.Columns.Add("MKEY", typeof(int));
                        dataTable.Columns.Add("SR_NO", typeof(int));
                        dataTable.Columns.Add("DOCUMENT_NAME", typeof(string));
                        dataTable.Columns.Add("DOCUMENT_CATEGORY", typeof(string));
                        dataTable.Columns.Add("LAST_UPDATED_BY", typeof(int));
                        dataTable.Columns.Add("LAST_UPDATE_DATE", typeof(DateTime));
                        dataTable.Columns.Add("DELETE_FLAG", typeof(char));

                        var SR_No = await db.QuerySingleAsync<int>("SELECT isnull(max(t.SR_NO), 0) + 1 FROM APPROVAL_TEMPLATE_TRL_CHECKLIST t " +
                            " WHERE MKEY = @MKEY AND DELETE_FLAG = 'N';",
                                                                   new { MKEY = objAPPROVAL_TEMPLATE_HDR.MKEY },
                                                                   commandType: CommandType.Text,
                                                                   transaction: transaction);

                        foreach (var CHECK_LIST in updateApprovalTemplates.CHECKLIST_DOC_LST)
                        {
                            dataTable.Rows.Add(updateApprovalTemplates.MKEY, SR_No, CHECK_LIST.Key, CHECK_LIST.Value,
                                               updateApprovalTemplates.CREATED_BY, dateTime, 'N');
                            SR_No++;
                        }

                        using var bulkCopyCheckList = new SqlBulkCopy(sqlConnection, SqlBulkCopyOptions.Default, (SqlTransaction)transaction)
                        {
                            DestinationTableName = "APPROVAL_TEMPLATE_TRL_CHECKLIST"
                        };

                        bulkCopyCheckList.ColumnMappings.Add("MKEY", "MKEY");
                        bulkCopyCheckList.ColumnMappings.Add("SR_NO", "SR_NO");
                        bulkCopyCheckList.ColumnMappings.Add("DOCUMENT_NAME", "DOCUMENT_NAME");
                        bulkCopyCheckList.ColumnMappings.Add("DOCUMENT_CATEGORY", "DOCUMENT_CATEGORY");
                        bulkCopyCheckList.ColumnMappings.Add("LAST_UPDATED_BY", "LAST_UPDATED_BY");
                        bulkCopyCheckList.ColumnMappings.Add("LAST_UPDATE_DATE", "LAST_UPDATE_DATE");
                        bulkCopyCheckList.ColumnMappings.Add("DELETE_FLAG", "DELETE_FLAG");

                        await bulkCopyCheckList.WriteToServerAsync(dataTable);

                        var sql = "SELECT DOCUMENT_NAME, DOCUMENT_CATEGORY FROM APPROVAL_TEMPLATE_TRL_CHECKLIST WHERE MKEY = @MKEY AND DELETE_FLAG = 'N';";
                        var keyValuePairsCheckList = await db.QueryAsync(sql, new { MKEY = objAPPROVAL_TEMPLATE_HDR.MKEY }, transaction: transaction);

                        // Initialize the END_RESULT_DOC_LST dictionary
                        objAPPROVAL_TEMPLATE_HDR.CHECKLIST_DOC_LST = new Dictionary<string, object>();

                        // Populate the dictionary with the key-value pairs
                        foreach (var item in keyValuePairsCheckList)
                        {
                            // Assuming DOCUMENT_NAME is the key and DOCUMENT_CATEGORY is the value
                            objAPPROVAL_TEMPLATE_HDR.CHECKLIST_DOC_LST.Add(item.DOCUMENT_NAME.ToString(), item.DOCUMENT_CATEGORY);
                        }
                    }
                    #endregion

                    #region Insert SUBTASK_LIST
                    if (objAPPROVAL_TEMPLATE_HDR.SUBTASK_LIST != null)
                    {
                        var subtaskDataTable = new DataTable();
                        subtaskDataTable.Columns.Add("HEADER_MKEY", typeof(int));
                        subtaskDataTable.Columns.Add("SEQ_NO", typeof(string));
                        subtaskDataTable.Columns.Add("SUBTASK_ABBR", typeof(string));
                        subtaskDataTable.Columns.Add("SUBTASK_MKEY", typeof(int));
                        subtaskDataTable.Columns.Add("SUBTASK_PARENT_ID", typeof(int));
                        subtaskDataTable.Columns.Add("LAST_UPDATED_BY", typeof(int));
                        subtaskDataTable.Columns.Add("LAST_UPDATE_DATE", typeof(DateTime));
                        subtaskDataTable.Columns.Add("DELETE_FLAG", typeof(char));

                        foreach (var subtask in updateApprovalTemplates.SUBTASK_LIST)
                        {
                            var parametersApprovalCheck = new DynamicParameters();
                            parametersApprovalCheck.Add("@HEADER_KMEY", objAPPROVAL_TEMPLATE_HDR.MKEY);
                            parametersApprovalCheck.Add("@MKEY_SUBAPPROVAL", subtask.SUBTASK_MKEY);
                            parametersApprovalCheck.Add("@BUILDING_TYPE", objAPPROVAL_TEMPLATE_HDR.BUILDING_TYPE);
                            parametersApprovalCheck.Add("@BUILDING_STANDARD", objAPPROVAL_TEMPLATE_HDR.BUILDING_STANDARD);
                            parametersApprovalCheck.Add("@STATUTORY_AUTHORITY", objAPPROVAL_TEMPLATE_HDR.STATUTORY_AUTHORITY);
                            parametersApprovalCheck.Add("@STATUS", string.Empty);
                            parametersApprovalCheck.Add("@MESSAGE", string.Empty);

                            var ApprovalSub = await db.QueryAsync<OutPutApprovalTemplates>("SP_GET_APPROVAL_HEADER_CHECK_SUBAPPROVAL", parametersApprovalCheck,
                                commandType: CommandType.StoredProcedure, transaction: transaction);

                            if (ApprovalSub.Any())
                            {
                                foreach (var ResponseStatus in ApprovalSub)
                                {
                                    if (ResponseStatus.Status != "Ok")
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
                                                objAPPROVAL_TEMPLATE_HDR.Status = "Error";
                                                objAPPROVAL_TEMPLATE_HDR.Message = rollbackEx.Message;
                                                Console.WriteLine($"Rollback failed: {rollbackEx.Message}");
                                            }
                                            return ResponseStatus;
                                        }
                                    }
                                }
                            }
                            subtaskDataTable.Rows.Add(objAPPROVAL_TEMPLATE_HDR.MKEY, subtask.SEQ_NO, subtask.SUBTASK_ABBR, subtask.SUBTASK_MKEY,
                           objAPPROVAL_TEMPLATE_HDR.MKEY,
                                                 updateApprovalTemplates.CREATED_BY, dateTime,
                                                 'N');
                        }
                        using var bulkCopySubtask = new SqlBulkCopy(sqlConnection, SqlBulkCopyOptions.Default, (SqlTransaction)transaction)
                        {
                            DestinationTableName = "APPROVAL_TEMPLATE_TRL_SUBTASK"
                        };

                        bulkCopySubtask.ColumnMappings.Add("HEADER_MKEY", "HEADER_MKEY");
                        bulkCopySubtask.ColumnMappings.Add("SUBTASK_ABBR", "SUBTASK_ABBR");
                        bulkCopySubtask.ColumnMappings.Add("SEQ_NO", "SEQ_NO");
                        bulkCopySubtask.ColumnMappings.Add("SUBTASK_MKEY", "SUBTASK_MKEY");
                        bulkCopySubtask.ColumnMappings.Add("SUBTASK_PARENT_ID", "SUBTASK_PARENT_ID");
                        bulkCopySubtask.ColumnMappings.Add("LAST_UPDATED_BY", "LAST_UPDATED_BY");
                        bulkCopySubtask.ColumnMappings.Add("LAST_UPDATE_DATE", "LAST_UPDATE_DATE");
                        bulkCopySubtask.ColumnMappings.Add("DELETE_FLAG", "DELETE_FLAG");

                        await bulkCopySubtask.WriteToServerAsync(subtaskDataTable);

                        var subtasks = await db.QueryAsync<OUTPUT_APPROVAL_TEMPLATE_TRL_SUBTASK>(
                       "SELECT * FROM APPROVAL_TEMPLATE_TRL_SUBTASK WHERE HEADER_MKEY = @HEADER_MKEY AND DELETE_FLAG = 'N';",
                       new { HEADER_MKEY = objAPPROVAL_TEMPLATE_HDR.MKEY }, transaction: transaction);

                        objAPPROVAL_TEMPLATE_HDR.SUBTASK_LIST = subtasks.ToList();
                    }
                    #endregion
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
                        if (updateApprovalTemplates.SANCTIONING_DEPARTMENT_LIST.Count > 0)
                        {
                            var SR_No = await db.QuerySingleAsync<int>("SELECT isnull(max(t.SR_NO),0) + 1 FROM APPROVAL_TEMPLATE_TRL_SANCTIONING_DEPARTMENT t" +
                                " WHERE MKEY = @MKEY;", new { MKEY = updateApprovalTemplates.MKEY }, commandType: CommandType.Text,
                                transaction: transaction);

                            // Populate the DataTable with subtasks
                            foreach (var SANCTIONING_DEPARTMENT in updateApprovalTemplates.SANCTIONING_DEPARTMENT_LIST)
                            {

                                SanctioningDataTable.Rows.Add(updateApprovalTemplates.MKEY, SR_No, SANCTIONING_DEPARTMENT.LEVEL
                                    , SANCTIONING_DEPARTMENT.SANCTIONING_DEPARTMENT, SANCTIONING_DEPARTMENT.SANCTIONING_AUTHORITY
                                    , SANCTIONING_DEPARTMENT.START_DATE
                                    , SANCTIONING_DEPARTMENT.END_DATE == null ? null : SANCTIONING_DEPARTMENT.END_DATE
                                    , updateApprovalTemplates.CREATED_BY
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
                            string sql = "SELECT * from APPROVAL_TEMPLATE_TRL_SANCTIONING_DEPARTMENT WHERE MKEY = @MKEY " +
                                "AND DELETE_FLAG = 'N';";
                            var SANCTIONING_DEPARTMENT_TRL = await db.QueryAsync(sql, new { MKEY = updateApprovalTemplates.MKEY }, transaction: transaction);

                            // Assuming the model has a SUBTASK_LIST dictionary to hold these values
                            objAPPROVAL_TEMPLATE_HDR.SANCTIONING_DEPARTMENT_LIST = new List<OUTPUT_APPROVAL_TEMPLATE_TRL_SANCTIONING_DEPARTMENT>();  // Assuming Subtask is a class for this data

                            foreach (var item in SANCTIONING_DEPARTMENT_TRL)
                            {
                                objAPPROVAL_TEMPLATE_HDR.SANCTIONING_DEPARTMENT_LIST.Add(new OUTPUT_APPROVAL_TEMPLATE_TRL_SANCTIONING_DEPARTMENT
                                {
                                    MKEY = item.MKEY,
                                    LEVEL = item.LEVEL,
                                    SANCTIONING_DEPARTMENT = item.SANCTIONING_DEPARTMENT,
                                    SANCTIONING_AUTHORITY = item.SANCTIONING_AUTHORITY,
                                    START_DATE = item.START_DATE,
                                    END_DATE = item.END_DATE
                                });
                            }
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
                                objAPPROVAL_TEMPLATE_HDR.Status = "Error";
                                objAPPROVAL_TEMPLATE_HDR.Message = rollbackEx.Message;
                                Console.WriteLine($"Rollback failed: {rollbackEx.Message}");
                            }
                        }
                    }

                    var sqlTransaction = transaction as SqlTransaction;
                    if (sqlTransaction != null)
                    {
                        await sqlTransaction.CommitAsync();  // Commit the entire transaction
                    }

                    transactionCompleted = true;

                    objAPPROVAL_TEMPLATE_HDR.Status = "Ok";
                    objAPPROVAL_TEMPLATE_HDR.Message = "updated successfully";

                    return objAPPROVAL_TEMPLATE_HDR;  // Return the updated object
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

                var approvalTemplate = new OutPutApprovalTemplates();
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

        #region

        public async Task<Add_ApprovalTemplate_TaskOutPut_List_NT> CreateApprovalTemplateTASKAsync_PS(AddApprovalTemplate_PS_Model approvaltemplate)
        {
            DateTime dateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
            var approvalTemplateHDR = new AddApprovalTemplate_PS_Model();
            var addApprovalTemplate_PS_Model = new AddApprovalTemplate_PS_Return_Models();
            approvalTemplateHDR = approvaltemplate;
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

                    if (approvaltemplate.Mkey == 0)
                    {
                        var approvalTemplateMap = await MapApproval_Template_HDR(approvaltemplate, approvaltemplate.CREATED_BY);
                        var parameters = new DynamicParameters();
                        // If MKEY is null or 0, it will insert; otherwise, it will update
                        parameters.Add("@MKEY", approvalTemplateMap.MKEY == 0 ? null : approvalTemplateMap.MKEY, DbType.Int32);
                        parameters.Add("@BUILDING_TYPE", approvalTemplateMap.BUILDING_TYPE, DbType.Int32);
                        parameters.Add("@BUILDING_STANDARD", approvalTemplateMap.BUILDING_STANDARD, DbType.Int32);
                        parameters.Add("@STATUTORY_AUTHORITY", approvalTemplateMap.STATUTORY_AUTHORITY, DbType.Int32);
                        parameters.Add("@MAIN_ABBR", approvalTemplateMap.MAIN_ABBR, DbType.String);
                        parameters.Add("@SHORT_DESCRIPTION", approvalTemplateMap.SHORT_DESCRIPTION, DbType.String);
                        parameters.Add("@LONG_DESCRIPTION", approvalTemplateMap.LONG_DESCRIPTION, DbType.String);
                        parameters.Add("@AUTHORITY_DEPARTMENT", approvalTemplateMap.AUTHORITY_DEPARTMENT, DbType.Int32);
                        parameters.Add("@RESPOSIBLE_EMP_MKEY", approvalTemplateMap.RESPOSIBLE_EMP_MKEY, DbType.Int32);
                        parameters.Add("@JOB_ROLE", approvalTemplateMap.JOB_ROLE, DbType.Int32);
                        parameters.Add("@DAYS_REQUIERD", approvalTemplateMap.DAYS_REQUIERD, DbType.String);
                        parameters.Add("@SEQ_ORDER", approvalTemplateMap.SEQ_ORDER, DbType.String);
                        parameters.Add("@SANCTIONING_DEPARTMENT_MKEY", approvalTemplateMap.SANCTIONING_DEPARTMENT_MKEY, DbType.Int32);
                        parameters.Add("@SANCTION_AUTHORITY", approvalTemplateMap.SANCTION_AUTHORITY, DbType.Int32);
                        parameters.Add("@SANCTION_DEPARTMENT", approvalTemplateMap.SANCTION_DEPARTMENT, DbType.String);
                        parameters.Add("@ATTRIBUTE1", approvalTemplateMap.ATTRIBUTE1, DbType.String);
                        parameters.Add("@ATTRIBUTE2", approvalTemplateMap.ATTRIBUTE2, DbType.String);
                        parameters.Add("@ATTRIBUTE3", approvalTemplateMap.ATTRIBUTE3, DbType.String);
                        parameters.Add("@ATTRIBUTE4", approvalTemplateMap.ATTRIBUTE4, DbType.String);
                        parameters.Add("@ATTRIBUTE5", approvalTemplateMap.ATTRIBUTE5, DbType.String);
                        parameters.Add("@END_RESULT_DOC", approvalTemplateMap.END_RESULT_DOC, DbType.String);
                        parameters.Add("@CHECKLIST_DOC", approvalTemplateMap.CHECKLIST_DOC, DbType.String);
                        parameters.Add("@TAGS", approvalTemplateMap.TAGS, DbType.String);
                        parameters.Add("@IS_NODE", approvalTemplateMap.IS_NODE, DbType.String);
                        parameters.Add("@CREATED_BY", approvalTemplateMap.CREATED_BY, DbType.Int32);
                        parameters.Add("@LAST_UPDATED_BY", approvalTemplateMap.LAST_UPDATED_BY, DbType.Int32);
                        parameters.Add("@Session_User_Id", approvaltemplate.Session_User_Id, DbType.Int32);
                        parameters.Add("@Business_Group_Id", approvaltemplate.Business_Group_Id, DbType.Int32);
                        parameters.Add("@DELETE_FLAG", approvaltemplate.DELETE_FLAG, DbType.String);

                        // Output parameters
                        parameters.Add("@OUT_STATUS", dbType: DbType.String, direction: ParameterDirection.Output, size: 50);
                        parameters.Add("@OUT_MESSAGE", dbType: DbType.String, direction: ParameterDirection.Output, size: 200);
                        parameters.Add("@MkeyResponse", dbType: DbType.String, direction: ParameterDirection.Output, size: 200);

                        // Execute SP
                        await db.ExecuteAsync(
                            "SP_INS_UPD_APPROVAL_TEMPLATE_HDR_PS",
                            parameters,
                            transaction: transaction,
                            commandType: CommandType.StoredProcedure
                        );

                        // Retrieve output values
                        var outStatus = parameters.Get<string>("@OUT_STATUS");
                        var Mkey = parameters.Get<string>("@MkeyResponse");
                        var outMessage = parameters.Get<string>("@OUT_MESSAGE");

                        // Map result
                        var response = new AddApprovalTemplate_PS_Return_Models
                        {
                            Status = outStatus,
                            Message = outMessage,
                            MKEY = Convert.ToDecimal(Mkey)  // Optionally, you can fetch inserted MKEY if needed
                        };

                        addApprovalTemplate_PS_Model = new AddApprovalTemplate_PS_Return_Models
                        {
                            MKEY = response.MKEY,
                            Status = response.Status,
                            Message = response.Message
                        };
                        approvaltemplate.Mkey = Convert.ToInt32(addApprovalTemplate_PS_Model.MKEY);


                        if (approvaltemplate.Mkey > 0)
                        {
                            if (approvaltemplate.tASK_CHECKLIST_TABLE_INPUT_NT.Any())
                            {
                                foreach (var TCheckList in approvaltemplate.tASK_CHECKLIST_TABLE_INPUT_NT)
                                {
                                    var parmetersCheckList = new DynamicParameters();
                                    parmetersCheckList.Add("@TASK_MKEY", approvaltemplate.Mkey, DbType.Int32);
                                    parmetersCheckList.Add("@SR_NO", TCheckList.SR_NO, DbType.Int32);
                                    parmetersCheckList.Add("@Doc_Type_Mkey", TCheckList.DOC_MKEY, DbType.Int32);
                                    parmetersCheckList.Add("@Doc_Cat_mkey", TCheckList.Doc_Cat_Mkey, DbType.Int32);
                                    parmetersCheckList.Add("@DOCUMENT_CATEGORY", TCheckList.DOCUMENT_CATEGORY, DbType.String, size: 200);
                                    parmetersCheckList.Add("@CREATED_BY", TCheckList.CREATED_BY, DbType.Int32);
                                    parmetersCheckList.Add("@DELETE_FLAG", TCheckList.DELETE_FLAG, DbType.String, size: 2);
                                    parmetersCheckList.Add("@COMMENT", null, DbType.String);
                                    parmetersCheckList.Add("@METHOD_NAME", "Task-CheckList-ApprovalTemplate-Doc-Insert-Update", DbType.String);
                                    parmetersCheckList.Add("@METHOD", "Insert", DbType.String);

                                    // Output parameters
                                    parmetersCheckList.Add("@OUT_STATUS", dbType: DbType.String, size: 200, direction: ParameterDirection.Output);
                                    parmetersCheckList.Add("@OUT_MESSAGE", dbType: DbType.String, size: 200, direction: ParameterDirection.Output);

                                    await db.ExecuteAsync("[dbo].[SP_INSERT_Checklist_In_ApprovalChecklist]", parmetersCheckList, commandType: CommandType.StoredProcedure, transaction: transaction);

                                    // Read output
                                    string status = parmetersCheckList.Get<string>("@OUT_STATUS");
                                    string message = parmetersCheckList.Get<string>("@OUT_MESSAGE");
                                    if (!status.Contains("OK"))
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

                                        var errorResult = new Add_ApprovalTemplate_TaskOutPut_List_NT
                                        {

                                            Status = "Error",
                                            Message = message,
                                            Data = null

                                        };
                                        return errorResult;
                                    }
                                }
                            }

                            if (approvaltemplate.tASK_ENDLIST_TABLE_INPUT_NTs.Any())
                            {
                                // To Insert EndList
                                foreach (var TEndList in approvaltemplate.tASK_ENDLIST_TABLE_INPUT_NTs)
                                {
                                    foreach (var docMkey in TEndList.OUTPUT_DOC_LST)
                                    {
                                        foreach (var DocCategory in docMkey.Value.ToString().Split(','))
                                        {
                                            var parametersEndList = new DynamicParameters();
                                            parametersEndList.Add("@MKEY", approvaltemplate.Mkey, DbType.Int32);
                                            parametersEndList.Add("@SR_NO", TEndList.SR_NO, DbType.Int32);
                                            parametersEndList.Add("@Category_Name", TEndList.Category_Name, DbType.String);
                                            parametersEndList.Add("@DOC_TYPE_MKEY", Convert.ToInt32(docMkey.Key), DbType.Int32);
                                            parametersEndList.Add("@DOC_CAT_MKEY", Convert.ToInt32(DocCategory), DbType.Int32);
                                            // parametersEndList.Add("@DOCUMENT_CATEGORY_MKEY", Convert.ToInt32(DocCategory));
                                            // parametersEndList.Add("@DOCUMENT_NAME", docMkey.Key.ToString());
                                            parametersEndList.Add("@CREATED_BY", TEndList.CREATED_BY, DbType.Int32);
                                            parametersEndList.Add("@DELETE_FLAG", TEndList.DELETE_FLAG, DbType.String, size: 2);
                                            parametersEndList.Add("@API_NAME", "Task-ApprovalTemplate-Output-Doc-Insert-Update", DbType.String);
                                            parametersEndList.Add("@API_METHOD", "Insert/Update", DbType.String);

                                            parametersEndList.Add("@OUT_STATUS", dbType: DbType.String, size: 200, direction: ParameterDirection.Output);
                                            parametersEndList.Add("@OUT_MESSAGE", dbType: DbType.String, size: 200, direction: ParameterDirection.Output);


                                            await db.QueryAsync<TASK_ENDLIST_DETAILS_OUTPUT>("[dbo].[SP_INSERT_UPDATE_ApprovalTemplate_ENDLIST_TABLE_NT]", parametersEndList, commandType: CommandType.StoredProcedure, transaction: transaction);
                                            string status = parametersEndList.Get<string>("@OUT_STATUS");
                                            string message = parametersEndList.Get<string>("@OUT_MESSAGE");
                                            //var taskEndlist = new List<TASK_CHECKLIST_TABLE_INPUT_NT>()
                                            //{
                                            //     new TASK_CHECKLIST_TABLE_INPUT_NT
                                            //     {
                                            //         TASK_MKEY= GetTaskEndList.Where(x=>x.)
                                            //     }
                                            //};

                                            if (!status.Contains("Ok"))
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

                                                var errorResult = new Add_ApprovalTemplate_TaskOutPut_List_NT
                                                {

                                                    Status = "Error",
                                                    Message = message,
                                                    Data = null

                                                };
                                                return errorResult;
                                            }
                                        }
                                    }
                                }
                            }

                            if (approvaltemplate.SUBTASK_LIST?.Any() == true)
                            {
                                foreach (var subtask in approvaltemplate.SUBTASK_LIST)
                                {
                                    var subtask_Nt = await MapApproval_Tempplate_TRL_SUBTASK_NT(
                                        subtask,
                                        approvaltemplate.Mkey,
                                        approvaltemplate.Session_User_Id
                                    );

                                    var Subparameters = new DynamicParameters();

                                    Subparameters.Add("@HEADER_MKEY", subtask_Nt.HEADER_MKEY, DbType.Int32);
                                    Subparameters.Add("@SEQ_NO", subtask_Nt.SEQ_NO, DbType.String);
                                    Subparameters.Add("@SUBTASK_ABBR", subtask_Nt.SUBTASK_ABBR, DbType.String);
                                    Subparameters.Add("@SUBTASK_MKEY", subtask_Nt.SUBTASK_MKEY, DbType.Int32);
                                    Subparameters.Add("@SUBTASK_PARENT_ID", subtask_Nt.SUBTASK_PARENT_ID, DbType.Int32);
                                    Subparameters.Add("@ATTRIBUTE1", subtask_Nt.ATTRIBUTE1, DbType.String, size: 200);
                                    Subparameters.Add("@ATTRIBUTE2", subtask_Nt.ATTRIBUTE2, DbType.String, size: 200);
                                    Subparameters.Add("@ATTRIBUTE3", subtask_Nt.ATTRIBUTE3, DbType.String, size: 200);
                                    Subparameters.Add("@ATTRIBUTE4", subtask_Nt.ATTRIBUTE4, DbType.String, size: 200);
                                    Subparameters.Add("@ATTRIBUTE5", subtask_Nt.ATTRIBUTE5, DbType.String, size: 200);
                                    Subparameters.Add("@ATTRIBUTE6", subtask_Nt.ATTRIBUTE6, DbType.String, size: 200);
                                    Subparameters.Add("@ATTRIBUTE7", subtask_Nt.ATTRIBUTE7, DbType.String, size: 200);
                                    Subparameters.Add("@ATTRIBUTE8", subtask_Nt.ATTRIBUTE8, DbType.String, size: 200);
                                    Subparameters.Add("@CREATED_BY", subtask_Nt.CREATED_BY, DbType.Int32);
                                    Subparameters.Add("@METHOD_NAME", "Task-ApprovalTemplate-Output-Doc-Insert-Update", DbType.String);
                                    Subparameters.Add("@METHOD", "Insert/Update", DbType.String);
                                    Subparameters.Add("@DELETE_FLAG", subtask_Nt.DELETE_FLAG, DbType.String);
                                    // OUTPUT parameters
                                    Subparameters.Add("@OUT_STATUS", dbType: DbType.String, size: 50, direction: ParameterDirection.Output);
                                    Subparameters.Add("@OUT_MESSAGE", dbType: DbType.String, size: 200, direction: ParameterDirection.Output);

                                    await db.ExecuteAsync(
                                        "[dbo].[SP_Insert_Update_APPROVAL_TEMPLATE_TRL_SUBTASK]",
                                        Subparameters,
                                        commandType: CommandType.StoredProcedure,
                                        transaction: transaction
                                    );

                                    string status = Subparameters.Get<string>("@OUT_STATUS");
                                    string message = Subparameters.Get<string>("@OUT_MESSAGE");

                                    if (!string.Equals(status, "OK", StringComparison.OrdinalIgnoreCase))
                                    {
                                        transaction?.Rollback();

                                        return new Add_ApprovalTemplate_TaskOutPut_List_NT
                                        {
                                            Status = "Error",
                                            Message = message,
                                            Data = null
                                        };
                                    }
                                }
                            }

                            if (approvaltemplate.SANCTIONING_DEPARTMENT_LIST.Any())
                            {
                                foreach (var TSanctioning in approvaltemplate.SANCTIONING_DEPARTMENT_LIST)
                                {
                                    var Tsanctionint_Nt = await MapApproval_Tempplate_TRL_Sactioning_NT(
                                        TSanctioning,
                                        approvaltemplate.Mkey,
                                        approvaltemplate.Session_User_Id
                                    );

                                    var parmetersSanctioning = new DynamicParameters();
                                    parmetersSanctioning.Add("@MKEY", approvaltemplate.Mkey, DbType.Int32);
                                    parmetersSanctioning.Add("@SR_NO", Tsanctionint_Nt.SR_NO, DbType.Int32);
                                    parmetersSanctioning.Add("@LEVEL", Tsanctionint_Nt.LEVEL, DbType.Int32);
                                    parmetersSanctioning.Add("@SANCTIONING_DEPARTMENT", Tsanctionint_Nt.SANCTIONING_DEPARTMENT, DbType.String);
                                    parmetersSanctioning.Add("@SANCTIONING_AUTHORITY", Tsanctionint_Nt.SANCTIONING_AUTHORITY, DbType.String);
                                    parmetersSanctioning.Add("@SANCTIONING_AUTHORITY_MKEY", Tsanctionint_Nt.SANCTIONING_AUTHORITY_MKEY, DbType.Int32);
                                    parmetersSanctioning.Add("@START_DATE", Tsanctionint_Nt.START_DATE, DbType.DateTime);
                                    parmetersSanctioning.Add("@END_DATE", Tsanctionint_Nt.END_DATE, DbType.DateTime);
                                    //parmetersSanctioning.Add("@ATTRIBUTE1", TSanctioning.A, DbType.DateTime);
                                    parmetersSanctioning.Add("@ATTRIBUTE1", Tsanctionint_Nt.ATTRIBUTE1, DbType.String, size: 200);
                                    parmetersSanctioning.Add("@ATTRIBUTE2", Tsanctionint_Nt.ATTRIBUTE2, DbType.String, size: 200);
                                    parmetersSanctioning.Add("@ATTRIBUTE3", Tsanctionint_Nt.ATTRIBUTE3, DbType.String, size: 200);
                                    parmetersSanctioning.Add("@ATTRIBUTE4", Tsanctionint_Nt.ATTRIBUTE4, DbType.String, size: 200);
                                    parmetersSanctioning.Add("@ATTRIBUTE5", Tsanctionint_Nt.ATTRIBUTE5, DbType.String, size: 200);
                                    parmetersSanctioning.Add("@DELETE_FLAG", TSanctioning.DELETE_FLAG, DbType.String);
                                    parmetersSanctioning.Add("@CREATED_BY", Tsanctionint_Nt.CREATED_BY, DbType.Int32);
                                    parmetersSanctioning.Add("@METHOD_NAME", "Task-Sanctioning-Table-Insert-Update", DbType.String);
                                    parmetersSanctioning.Add("@METHOD", "Insert/Update", DbType.String);

                                    parmetersSanctioning.Add("@OUT_STATUS",
                                        dbType: DbType.String,
                                        size: 200,
                                        direction: ParameterDirection.Output);

                                    parmetersSanctioning.Add("@OUT_MESSAGE",
                                        dbType: DbType.String,
                                        size: 500,
                                        direction: ParameterDirection.Output);

                                    await db.ExecuteAsync(
                                        "SP_INS_UPD_APPROVAL_TEMPLATE_TRL_SANCTIONING_DEPARTMENT",
                                        parmetersSanctioning,
                                        commandType: CommandType.StoredProcedure,
                                        transaction: transaction
                                    );

                                    string status = parmetersSanctioning.Get<string>("@OUT_STATUS");
                                    string message = parmetersSanctioning.Get<string>("@OUT_MESSAGE");

                                    if (!string.Equals(status, "OK", StringComparison.OrdinalIgnoreCase))
                                    {
                                        if (transaction != null && !transactionCompleted)
                                        {
                                            transaction.Rollback();
                                        }

                                        return new Add_ApprovalTemplate_TaskOutPut_List_NT
                                        {
                                            Status = "Error",
                                            Message = message,
                                            Data = null
                                        };
                                    }
                                }
                            }

                            var sqlTransaction = (SqlTransaction)transaction;
                            await sqlTransaction.CommitAsync();
                            transactionCompleted = true;

                            //var AssignEmail = await db.QueryAsync<string>("select EMAIL_ID_OFFICIAL from EMPLOYEE_MST where MKEY = " + tASK_RECURSIVE_HDR.CREATED_BY + "  ", commandType: CommandType.Text);
                            //string AssignByEmail = AssignEmail.FirstOrDefault();
                            //var CreatedEmail = await db.QueryAsync<string>("select EMAIL_ID_OFFICIAL from EMPLOYEE_MST where MKEY = " + tASK_RECURSIVE_HDR.ASSIGNED_TO + "  ", commandType: CommandType.Text);
                            //string CreatedByEmail = CreatedEmail.FirstOrDefault();
                            //var AssignedBy = await db.QueryAsync<string>("Select EMP_FULL_NAME from EMPLOYEE_MST where MKEY = " + tASK_RECURSIVE_HDR.ASSIGNED_TO + "  ", commandType: CommandType.Text);
                            //string AssignedByName = AssignedBy.FirstOrDefault();


                            //var parmetersMail = new DynamicParameters();
                            //parmetersMail.Add("@MAIL_TYPE", "Auto");
                            //var MailDetails = await db.QueryAsync<MailDetailsNT>("SP_GET_MAIL_TYPE", parmetersMail, commandType: CommandType.StoredProcedure);
                            //string completionDate = DateTime.Now.AddDays(Convert.ToDouble(tASK_RECURSIVE_HDR.NO_DAYS)).ToString("dd-MM-yyyy");
                            //StringBuilder htmlRows = new StringBuilder();
                            //htmlRows.Append("<tr>");
                            //htmlRows.AppendFormat("<td style='border:1px solid #9ec3ff; text-align:center; padding:5px;'>{0}</td>", tASK_RECURSIVE_HDR.MKEY);
                            //htmlRows.AppendFormat("<td style='border:1px solid #9ec3ff; text-align:center; padding:5px;'>{0}</td>", tASK_RECURSIVE_HDR.TASK_NAME);
                            //htmlRows.AppendFormat("<td style='border:1px solid #9ec3ff; text-align:center; padding:5px;'>{0}</td>", tASK_RECURSIVE_HDR.TASK_DESCRIPTION);
                            //htmlRows.AppendFormat("<td style='border:1px solid #9ec3ff; text-align:center; padding:5px;'>{0:dd-MMM-yyyy}</td>", completionDate);  // tASK_RECURSIVE_HDR.COMPLETION_DATE
                            //htmlRows.AppendFormat("<td style='border:1px solid #9ec3ff; text-align:center; padding:5px;'>{0}</td>", AssignedByName);
                            //htmlRows.Append("</tr>");

                            //string taskKickoffRowsHtml = htmlRows.ToString();

                            //string MailBody = "<!DOCTYPE html>\r\n<html>\r\n<head>\r\n    " +
                            //    "<meta charset=\"UTF-8\">\r\n    " +
                            //    "<title>Task Assignment</title>\r\n" +
                            //    "</head>\r\n<body style=\"font-family: Arial, sans-serif; font-size: 14px; color: #333;\">\r\n    " +
                            //    "<p>Dear <strong>" + AssignedByName + "</strong>,</p>\r\n\r\n    <p>The following task has been assigned to you:</p>\r\n\r\n    " +
                            //    "<form style='margin:1% auto;'>\r\n                       " +
                            //    "<fieldset style='border:2px solid #9ec3ff;margin:auto 1%;padding:5px;color:#003487;'>\r\n                         " +
                            //    "<legend style='padding:10px;font-size: 18px!important;'><b>Your Task list</b></legend>\r\n                         " +
                            //    "<table style='width:100%;padding:0px;margin:1% auto;border-radius:14px;font-size:14px;'>\r\n                           " +
                            //    "<tr>\r\n                             " +
                            //    "<td style='color:black; border-color:#9ec3ff;text-align:center; border-style:solid;border-radius:5px; padding: 5px; font-size: 14px;background-color:#9ec3ff;width:8%;'><b>Task No.</b></td>  \r\n                             " +
                            //    "<td style='color:black; border-color:#9ec3ff;text-align:center; border-style:solid;border-radius:5px; padding: 5px; font-size: 14px;background-color:#9ec3ff;'><b>Task Name</b></td>\r\n                            " +
                            //    " <td style='color:black; border-color:#9ec3ff;text-align:center; border-style:solid;border-radius:5px; padding: 5px; font-size: 14px;background-color:#9ec3ff;width:40%;'><b>Description</b></td>  \r\n                             " +
                            //    "<td style='color:black; border-color:#9ec3ff;text-align:center; border-style:solid;border-radius:5px; padding: 5px; font-size: 14px;background-color:#9ec3ff;width:10%;'><b>Due Date</b></td>  \r\n                             " +
                            //    "<td style='color:black; border-color:#9ec3ff;text-align:center; border-style:solid;border-radius:5px; padding: 5px; font-size: 14px;background-color:#9ec3ff;width:10%;'><b>Assigned By</b></td> \r\n                             " +
                            //    "</tr>\r\n                          " + taskKickoffRowsHtml +
                            //    "</table>\r\n                      " +
                            //    " </fieldset>\r\n                    " +
                            //    " </form>\r\n\r\n    " +
                            //    "<p style=\"margin-top: 20px;\">\r\n        If you have any questions or need assistance, feel free to contact us at \r\n        " +
                            //    "<a href=\"mailto:qui.support@powersoft.in\">qui.support@powersoft.in</a>.\r\n    </p>\r\n\r\n    <p>Best regards,<br>\r\n    " +
                            //    "<strong>QUI Team</strong></p>\r\n</body>\r\n</html>\r\n";

                            //foreach (var Mail in MailDetails)
                            //{
                            //    SendEmail(AssignByEmail.ToString(), null, null, "QUI-New Task Assigned : Task No-(" + TaskNo + " )", MailBody, Mail.MAIL_TYPE, "QUI", null, Mail);  // CreatedByEmail.ToString()
                            //}

                            var successsResult = new Add_ApprovalTemplate_TaskOutPut_List_NT
                            {
                                Status = "Ok",
                                Message = "Inserted Successfully",
                                Data = approvaltemplate
                            };
                            return successsResult;
                        }
                        else
                        {
                            var errorResult = new Add_ApprovalTemplate_TaskOutPut_List_NT();
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
                                     errorResult = new Add_ApprovalTemplate_TaskOutPut_List_NT
                                    {
                                        Status = "Error",
                                        Message = "Approval Template Insert Failled DUE TO" + addApprovalTemplate_PS_Model.Message
                                    };
                                }
                            }
                             errorResult = new Add_ApprovalTemplate_TaskOutPut_List_NT
                            {
                                Status = "Error",
                                Message = "Approval Template Insert Failled DUE TO " + addApprovalTemplate_PS_Model.Message
                            };
                            return errorResult;
                        }
                    }
                    else
                    {
                        var approvalTemplateMap = await MapApproval_Template_HDR(approvaltemplate, approvaltemplate.CREATED_BY);
                        var parameters = new DynamicParameters();
                        // If MKEY is null or 0, it will insert; otherwise, it will update
                        parameters.Add("@MKEY", approvalTemplateMap.MKEY == 0 ? null : approvalTemplateMap.MKEY, DbType.Int32);
                        parameters.Add("@BUILDING_TYPE", approvalTemplateMap.BUILDING_TYPE, DbType.Int32);
                        parameters.Add("@BUILDING_STANDARD", approvalTemplateMap.BUILDING_STANDARD, DbType.Int32);
                        parameters.Add("@STATUTORY_AUTHORITY", approvalTemplateMap.STATUTORY_AUTHORITY, DbType.Int32);
                        parameters.Add("@MAIN_ABBR", approvalTemplateMap.MAIN_ABBR, DbType.String);
                        parameters.Add("@SHORT_DESCRIPTION", approvalTemplateMap.SHORT_DESCRIPTION, DbType.String);
                        parameters.Add("@LONG_DESCRIPTION", approvalTemplateMap.LONG_DESCRIPTION, DbType.String);
                        parameters.Add("@AUTHORITY_DEPARTMENT", approvalTemplateMap.AUTHORITY_DEPARTMENT, DbType.Int32);
                        parameters.Add("@RESPOSIBLE_EMP_MKEY", approvalTemplateMap.RESPOSIBLE_EMP_MKEY, DbType.Int32);
                        parameters.Add("@JOB_ROLE", approvalTemplateMap.JOB_ROLE, DbType.Int32);
                        parameters.Add("@DAYS_REQUIERD", approvalTemplateMap.DAYS_REQUIERD, DbType.String);
                        parameters.Add("@SEQ_ORDER", approvalTemplateMap.SEQ_ORDER, DbType.String);
                        parameters.Add("@SANCTIONING_DEPARTMENT_MKEY", approvalTemplateMap.SANCTIONING_DEPARTMENT_MKEY, DbType.Int32);
                        parameters.Add("@SANCTION_AUTHORITY", approvalTemplateMap.SANCTION_AUTHORITY, DbType.Int32);
                        parameters.Add("@SANCTION_DEPARTMENT", approvalTemplateMap.SANCTION_DEPARTMENT, DbType.String);
                        parameters.Add("@ATTRIBUTE1", approvalTemplateMap.ATTRIBUTE1, DbType.String);
                        parameters.Add("@ATTRIBUTE2", approvalTemplateMap.ATTRIBUTE2, DbType.String);
                        parameters.Add("@ATTRIBUTE3", approvalTemplateMap.ATTRIBUTE3, DbType.String);
                        parameters.Add("@ATTRIBUTE4", approvalTemplateMap.ATTRIBUTE4, DbType.String);
                        parameters.Add("@ATTRIBUTE5", approvalTemplateMap.ATTRIBUTE5, DbType.String);
                        parameters.Add("@END_RESULT_DOC", approvalTemplateMap.END_RESULT_DOC, DbType.String);
                        parameters.Add("@CHECKLIST_DOC", approvalTemplateMap.CHECKLIST_DOC, DbType.String);
                        parameters.Add("@TAGS", approvalTemplateMap.TAGS, DbType.String);
                        parameters.Add("@IS_NODE", approvalTemplateMap.IS_NODE, DbType.String);
                        parameters.Add("@CREATED_BY", approvalTemplateMap.CREATED_BY, DbType.Int32);
                        parameters.Add("@LAST_UPDATED_BY", approvalTemplateMap.LAST_UPDATED_BY, DbType.Int32);
                        parameters.Add("@Session_User_Id", approvaltemplate.Session_User_Id, DbType.Int32);
                        parameters.Add("@Business_Group_Id", approvaltemplate.Business_Group_Id, DbType.Int32);
                        parameters.Add("@DELETE_FLAG", approvaltemplate.DELETE_FLAG, DbType.String);

                        // Output parameters
                        parameters.Add("@OUT_STATUS", dbType: DbType.String, direction: ParameterDirection.Output, size: 50);
                        parameters.Add("@OUT_MESSAGE", dbType: DbType.String, direction: ParameterDirection.Output, size: 200);
                        parameters.Add("@MkeyResponse", dbType: DbType.String, direction: ParameterDirection.Output, size: 200);
                        // Execute SP
                        await db.ExecuteAsync(
                            "SP_INS_UPD_APPROVAL_TEMPLATE_HDR_PS",
                            parameters,
                            transaction: transaction,
                            commandType: CommandType.StoredProcedure
                        );

                        // Retrieve output values
                        var outStatus = parameters.Get<string>("@OUT_STATUS");
                        var outMessage = parameters.Get<string>("@OUT_MESSAGE");
                        var Mkey = parameters.Get<string>("@MkeyResponse");
                        // Map result
                        var response = new AddApprovalTemplate_PS_Return_Models
                        {
                            Status = outStatus,
                            Message = outMessage,
                            MKEY = approvaltemplate.Mkey // Optionally, you can fetch inserted MKEY if needed
                        };

                        addApprovalTemplate_PS_Model = new AddApprovalTemplate_PS_Return_Models
                        {
                            MKEY = response.MKEY,
                            Status = response.Status,
                            Message = response.Message
                        };
                        approvaltemplate.Mkey = Convert.ToInt32(addApprovalTemplate_PS_Model.MKEY);


                        if (approvaltemplate.Mkey > 0)
                        {
                            if (approvaltemplate.tASK_CHECKLIST_TABLE_INPUT_NT.Any())
                            {
                                foreach (var TCheckList in approvaltemplate.tASK_CHECKLIST_TABLE_INPUT_NT)
                                {
                                    var parmetersCheckList = new DynamicParameters();
                                    parmetersCheckList.Add("@TASK_MKEY", approvaltemplate.Mkey, DbType.Int32);
                                    parmetersCheckList.Add("@SR_NO", TCheckList.SR_NO, DbType.Int32);
                                    parmetersCheckList.Add("@Doc_Type_Mkey", TCheckList.DOC_MKEY, DbType.Int32);
                                    parmetersCheckList.Add("@Doc_Cat_mkey", TCheckList.Doc_Cat_Mkey, DbType.Int32);
                                    parmetersCheckList.Add("@DOCUMENT_CATEGORY", TCheckList.DOCUMENT_CATEGORY, DbType.String, size: 200);
                                    parmetersCheckList.Add("@CREATED_BY", TCheckList.CREATED_BY, DbType.Int32);
                                    parmetersCheckList.Add("@DELETE_FLAG", TCheckList.DELETE_FLAG, DbType.String, size: 2);
                                    parmetersCheckList.Add("@COMMENT", null, DbType.String);
                                    parmetersCheckList.Add("@METHOD_NAME", "Task-CheckList-ApprovalTemplate-Doc-Insert-Update", DbType.String);
                                    parmetersCheckList.Add("@METHOD", "Insert", DbType.String);

                                    // Output parameters
                                    parmetersCheckList.Add("@OUT_STATUS", dbType: DbType.String, size: 200, direction: ParameterDirection.Output);
                                    parmetersCheckList.Add("@OUT_MESSAGE", dbType: DbType.String, size: 200, direction: ParameterDirection.Output);

                                    await db.ExecuteAsync("[dbo].[SP_INSERT_Checklist_In_ApprovalChecklist]", parmetersCheckList, commandType: CommandType.StoredProcedure, transaction: transaction);

                                    // Read output
                                    string status = parmetersCheckList.Get<string>("@OUT_STATUS");
                                    string message = parmetersCheckList.Get<string>("@OUT_MESSAGE");
                                    if (!status.Contains("OK"))
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

                                        var errorResult = new Add_ApprovalTemplate_TaskOutPut_List_NT
                                        {

                                            Status = "Error",
                                            Message = message,
                                            Data = null

                                        };
                                        return errorResult;
                                    }
                                }
                            }

                            if (approvaltemplate.tASK_ENDLIST_TABLE_INPUT_NTs.Any())
                            {
                                // To Insert EndList
                                foreach (var TEndList in approvaltemplate.tASK_ENDLIST_TABLE_INPUT_NTs)
                                {
                                    foreach (var docMkey in TEndList.OUTPUT_DOC_LST)
                                    {
                                        foreach (var DocCategory in docMkey.Value.ToString().Split(','))
                                        {
                                            var parametersEndList = new DynamicParameters();
                                            parametersEndList.Add("@MKEY", approvaltemplate.Mkey, DbType.Int32);
                                            parametersEndList.Add("@SR_NO", TEndList.SR_NO, DbType.Int32);
                                            parametersEndList.Add("@Category_Name", TEndList.Category_Name, DbType.String);
                                            parametersEndList.Add("@DOC_TYPE_MKEY", Convert.ToInt32(docMkey.Key), DbType.Int32);
                                            parametersEndList.Add("@DOC_CAT_MKEY", Convert.ToInt32(DocCategory), DbType.Int32);
                                            // parametersEndList.Add("@DOCUMENT_CATEGORY_MKEY", Convert.ToInt32(DocCategory));
                                            // parametersEndList.Add("@DOCUMENT_NAME", docMkey.Key.ToString());
                                            parametersEndList.Add("@CREATED_BY", TEndList.CREATED_BY, DbType.Int32);
                                            parametersEndList.Add("@DELETE_FLAG", TEndList.DELETE_FLAG, DbType.String, size: 2);
                                            parametersEndList.Add("@API_NAME", "Task-ApprovalTemplate-Output-Doc-Insert-Update", DbType.String);
                                            parametersEndList.Add("@API_METHOD", "Insert/Update", DbType.String);

                                            parametersEndList.Add("@OUT_STATUS", dbType: DbType.String, size: 200, direction: ParameterDirection.Output);
                                            parametersEndList.Add("@OUT_MESSAGE", dbType: DbType.String, size: 200, direction: ParameterDirection.Output);


                                            await db.QueryAsync<TASK_ENDLIST_DETAILS_OUTPUT>("[dbo].[SP_INSERT_UPDATE_ApprovalTemplate_ENDLIST_TABLE_NT]", parametersEndList, commandType: CommandType.StoredProcedure, transaction: transaction);
                                            string status = parametersEndList.Get<string>("@OUT_STATUS");
                                            string message = parametersEndList.Get<string>("@OUT_MESSAGE");
                                            //var taskEndlist = new List<TASK_CHECKLIST_TABLE_INPUT_NT>()
                                            //{
                                            //     new TASK_CHECKLIST_TABLE_INPUT_NT
                                            //     {
                                            //         TASK_MKEY= GetTaskEndList.Where(x=>x.)
                                            //     }
                                            //};

                                            if (!status.Contains("Ok"))
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

                                                var errorResult = new Add_ApprovalTemplate_TaskOutPut_List_NT
                                                {

                                                    Status = "Error",
                                                    Message = message,
                                                    Data = null

                                                };
                                                return errorResult;
                                            }
                                        }
                                    }
                                }
                            }

                            if (approvaltemplate.SUBTASK_LIST?.Any() == true)
                            {
                                foreach (var subtask in approvaltemplate.SUBTASK_LIST)
                                {
                                    var subtask_Nt = await MapApproval_Tempplate_TRL_SUBTASK_NT(
                                        subtask,
                                        approvaltemplate.Mkey,
                                        approvaltemplate.Session_User_Id
                                    );

                                    var Subparameters = new DynamicParameters();

                                    Subparameters.Add("@HEADER_MKEY", subtask_Nt.HEADER_MKEY, DbType.Int32);
                                    Subparameters.Add("@SEQ_NO", subtask_Nt.SEQ_NO, DbType.String);
                                    Subparameters.Add("@SUBTASK_ABBR", subtask_Nt.SUBTASK_ABBR, DbType.String);
                                    Subparameters.Add("@SUBTASK_MKEY", subtask_Nt.SUBTASK_MKEY, DbType.Int32);
                                    Subparameters.Add("@SUBTASK_PARENT_ID", subtask_Nt.SUBTASK_PARENT_ID, DbType.Int32);
                                    Subparameters.Add("@ATTRIBUTE1", subtask_Nt.ATTRIBUTE1, DbType.String, size: 200);
                                    Subparameters.Add("@ATTRIBUTE2", subtask_Nt.ATTRIBUTE2, DbType.String, size: 200);
                                    Subparameters.Add("@ATTRIBUTE3", subtask_Nt.ATTRIBUTE3, DbType.String, size: 200);
                                    Subparameters.Add("@ATTRIBUTE4", subtask_Nt.ATTRIBUTE4, DbType.String, size: 200);
                                    Subparameters.Add("@ATTRIBUTE5", subtask_Nt.ATTRIBUTE5, DbType.String, size: 200);
                                    Subparameters.Add("@ATTRIBUTE6", subtask_Nt.ATTRIBUTE6, DbType.String, size: 200);
                                    Subparameters.Add("@ATTRIBUTE7", subtask_Nt.ATTRIBUTE7, DbType.String, size: 200);
                                    Subparameters.Add("@ATTRIBUTE8", subtask_Nt.ATTRIBUTE8, DbType.String, size: 200);
                                    Subparameters.Add("@CREATED_BY", subtask_Nt.CREATED_BY, DbType.Int32);
                                    //parametersEndList.Add("@DELETE_FLAG", TEndList.DELETE_FLAG, DbType.String, size: 2);
                                    Subparameters.Add("@METHOD_NAME", "Task-ApprovalTemplate-Output-Doc-Insert-Update", DbType.String);
                                    Subparameters.Add("@METHOD", "Insert/Update", DbType.String);
                                    Subparameters.Add("@DELETE_FLAG", subtask_Nt.DELETE_FLAG, DbType.String);
                                    // OUTPUT parameters
                                    Subparameters.Add("@OUT_STATUS", dbType: DbType.String, size: 50, direction: ParameterDirection.Output);
                                    Subparameters.Add("@OUT_MESSAGE", dbType: DbType.String, size: 200, direction: ParameterDirection.Output);

                                    await db.ExecuteAsync("[dbo].[SP_Insert_Update_APPROVAL_TEMPLATE_TRL_SUBTASK]", // exact SP name
                                                              Subparameters,
                                                              commandType: CommandType.StoredProcedure,
                                                              transaction: transaction
                                                          );


                                    string status = Subparameters.Get<string>("@OUT_STATUS");
                                    string message = Subparameters.Get<string>("@OUT_MESSAGE");

                                    if (!string.Equals(status, "OK", StringComparison.OrdinalIgnoreCase))
                                    {
                                        transaction?.Rollback();

                                        return new Add_ApprovalTemplate_TaskOutPut_List_NT
                                        {
                                            Status = "Error",
                                            Message = message,
                                            Data = null
                                        };
                                    }
                                }
                            }

                            if (approvaltemplate.SANCTIONING_DEPARTMENT_LIST.Any())
                            {
                                foreach (var TSanctioning in approvaltemplate.SANCTIONING_DEPARTMENT_LIST)
                                {
                                    var Tsanctionint_Nt = await MapApproval_Tempplate_TRL_Sactioning_NT(
                                        TSanctioning,
                                        approvaltemplate.Mkey,
                                        approvaltemplate.Session_User_Id
                                    );

                                    var parmetersSanctioning = new DynamicParameters();
                                    parmetersSanctioning.Add("@MKEY", approvaltemplate.Mkey, DbType.Int32);
                                    parmetersSanctioning.Add("@SR_NO", Tsanctionint_Nt.SR_NO, DbType.Int32);
                                    parmetersSanctioning.Add("@LEVEL", Tsanctionint_Nt.LEVEL, DbType.Int32);
                                    parmetersSanctioning.Add("@SANCTIONING_DEPARTMENT", Tsanctionint_Nt.SANCTIONING_DEPARTMENT, DbType.String);
                                    parmetersSanctioning.Add("@SANCTIONING_AUTHORITY", Tsanctionint_Nt.SANCTIONING_AUTHORITY, DbType.String);
                                    parmetersSanctioning.Add("@SANCTIONING_AUTHORITY_MKEY", Tsanctionint_Nt.SANCTIONING_AUTHORITY_MKEY, DbType.String);
                                    parmetersSanctioning.Add("@START_DATE", Tsanctionint_Nt.START_DATE, DbType.DateTime);
                                    parmetersSanctioning.Add("@END_DATE", Tsanctionint_Nt.END_DATE, DbType.DateTime);
                                    //parmetersSanctioning.Add("@ATTRIBUTE1", TSanctioning.A, DbType.DateTime);
                                    parmetersSanctioning.Add("@ATTRIBUTE1", Tsanctionint_Nt.ATTRIBUTE1, DbType.String, size: 200);
                                    parmetersSanctioning.Add("@ATTRIBUTE2", Tsanctionint_Nt.ATTRIBUTE2, DbType.String, size: 200);
                                    parmetersSanctioning.Add("@ATTRIBUTE3", Tsanctionint_Nt.ATTRIBUTE3, DbType.String, size: 200);
                                    parmetersSanctioning.Add("@ATTRIBUTE4", Tsanctionint_Nt.ATTRIBUTE4, DbType.String, size: 200);
                                    parmetersSanctioning.Add("@ATTRIBUTE5", Tsanctionint_Nt.ATTRIBUTE5, DbType.String, size: 200);
                                    parmetersSanctioning.Add("@DELETE_FLAG", TSanctioning.DELETE_FLAG, DbType.String);
                                    parmetersSanctioning.Add("@CREATED_BY", Tsanctionint_Nt.CREATED_BY, DbType.Int32);
                                    parmetersSanctioning.Add("@METHOD_NAME", "Task-Sanctioning-Table-Insert-Update", DbType.String);
                                    parmetersSanctioning.Add("@METHOD", "Insert/Update", DbType.String);

                                    parmetersSanctioning.Add("@OUT_STATUS",
                                        dbType: DbType.String,
                                        size: 200,
                                        direction: ParameterDirection.Output);

                                    parmetersSanctioning.Add("@OUT_MESSAGE",
                                        dbType: DbType.String,
                                        size: 500,
                                        direction: ParameterDirection.Output);

                                    await db.ExecuteAsync(
                                        "SP_INS_UPD_APPROVAL_TEMPLATE_TRL_SANCTIONING_DEPARTMENT",
                                        parmetersSanctioning,
                                        commandType: CommandType.StoredProcedure,
                                        transaction: transaction
                                    );

                                    string status = parmetersSanctioning.Get<string>("@OUT_STATUS");
                                    string message = parmetersSanctioning.Get<string>("@OUT_MESSAGE");

                                    if (!string.Equals(status, "OK", StringComparison.OrdinalIgnoreCase))
                                    {
                                        if (transaction != null && !transactionCompleted)
                                        {
                                            transaction.Rollback();
                                        }

                                        return new Add_ApprovalTemplate_TaskOutPut_List_NT
                                        {
                                            Status = "Error",
                                            Message = message,
                                            Data = null
                                        };
                                    }
                                }
                            }

                            var sqlTransaction = (SqlTransaction)transaction;
                            await sqlTransaction.CommitAsync();
                            transactionCompleted = true;

                            //var AssignEmail = await db.QueryAsync<string>("select EMAIL_ID_OFFICIAL from EMPLOYEE_MST where MKEY = " + tASK_RECURSIVE_HDR.CREATED_BY + "  ", commandType: CommandType.Text);
                            //string AssignByEmail = AssignEmail.FirstOrDefault();
                            //var CreatedEmail = await db.QueryAsync<string>("select EMAIL_ID_OFFICIAL from EMPLOYEE_MST where MKEY = " + tASK_RECURSIVE_HDR.ASSIGNED_TO + "  ", commandType: CommandType.Text);
                            //string CreatedByEmail = CreatedEmail.FirstOrDefault();
                            //var AssignedBy = await db.QueryAsync<string>("Select EMP_FULL_NAME from EMPLOYEE_MST where MKEY = " + tASK_RECURSIVE_HDR.ASSIGNED_TO + "  ", commandType: CommandType.Text);
                            //string AssignedByName = AssignedBy.FirstOrDefault();


                            //var parmetersMail = new DynamicParameters();
                            //parmetersMail.Add("@MAIL_TYPE", "Auto");
                            //var MailDetails = await db.QueryAsync<MailDetailsNT>("SP_GET_MAIL_TYPE", parmetersMail, commandType: CommandType.StoredProcedure);
                            //string completionDate = DateTime.Now.AddDays(Convert.ToDouble(tASK_RECURSIVE_HDR.NO_DAYS)).ToString("dd-MM-yyyy");
                            //StringBuilder htmlRows = new StringBuilder();
                            //htmlRows.Append("<tr>");
                            //htmlRows.AppendFormat("<td style='border:1px solid #9ec3ff; text-align:center; padding:5px;'>{0}</td>", tASK_RECURSIVE_HDR.MKEY);
                            //htmlRows.AppendFormat("<td style='border:1px solid #9ec3ff; text-align:center; padding:5px;'>{0}</td>", tASK_RECURSIVE_HDR.TASK_NAME);
                            //htmlRows.AppendFormat("<td style='border:1px solid #9ec3ff; text-align:center; padding:5px;'>{0}</td>", tASK_RECURSIVE_HDR.TASK_DESCRIPTION);
                            //htmlRows.AppendFormat("<td style='border:1px solid #9ec3ff; text-align:center; padding:5px;'>{0:dd-MMM-yyyy}</td>", completionDate);  // tASK_RECURSIVE_HDR.COMPLETION_DATE
                            //htmlRows.AppendFormat("<td style='border:1px solid #9ec3ff; text-align:center; padding:5px;'>{0}</td>", AssignedByName);
                            //htmlRows.Append("</tr>");

                            //string taskKickoffRowsHtml = htmlRows.ToString();

                            //string MailBody = "<!DOCTYPE html>\r\n<html>\r\n<head>\r\n    " +
                            //    "<meta charset=\"UTF-8\">\r\n    " +
                            //    "<title>Task Assignment</title>\r\n" +
                            //    "</head>\r\n<body style=\"font-family: Arial, sans-serif; font-size: 14px; color: #333;\">\r\n    " +
                            //    "<p>Dear <strong>" + AssignedByName + "</strong>,</p>\r\n\r\n    <p>The following task has been assigned to you:</p>\r\n\r\n    " +
                            //    "<form style='margin:1% auto;'>\r\n                       " +
                            //    "<fieldset style='border:2px solid #9ec3ff;margin:auto 1%;padding:5px;color:#003487;'>\r\n                         " +
                            //    "<legend style='padding:10px;font-size: 18px!important;'><b>Your Task list</b></legend>\r\n                         " +
                            //    "<table style='width:100%;padding:0px;margin:1% auto;border-radius:14px;font-size:14px;'>\r\n                           " +
                            //    "<tr>\r\n                             " +
                            //    "<td style='color:black; border-color:#9ec3ff;text-align:center; border-style:solid;border-radius:5px; padding: 5px; font-size: 14px;background-color:#9ec3ff;width:8%;'><b>Task No.</b></td>  \r\n                             " +
                            //    "<td style='color:black; border-color:#9ec3ff;text-align:center; border-style:solid;border-radius:5px; padding: 5px; font-size: 14px;background-color:#9ec3ff;'><b>Task Name</b></td>\r\n                            " +
                            //    " <td style='color:black; border-color:#9ec3ff;text-align:center; border-style:solid;border-radius:5px; padding: 5px; font-size: 14px;background-color:#9ec3ff;width:40%;'><b>Description</b></td>  \r\n                             " +
                            //    "<td style='color:black; border-color:#9ec3ff;text-align:center; border-style:solid;border-radius:5px; padding: 5px; font-size: 14px;background-color:#9ec3ff;width:10%;'><b>Due Date</b></td>  \r\n                             " +
                            //    "<td style='color:black; border-color:#9ec3ff;text-align:center; border-style:solid;border-radius:5px; padding: 5px; font-size: 14px;background-color:#9ec3ff;width:10%;'><b>Assigned By</b></td> \r\n                             " +
                            //    "</tr>\r\n                          " + taskKickoffRowsHtml +
                            //    "</table>\r\n                      " +
                            //    " </fieldset>\r\n                    " +
                            //    " </form>\r\n\r\n    " +
                            //    "<p style=\"margin-top: 20px;\">\r\n        If you have any questions or need assistance, feel free to contact us at \r\n        " +
                            //    "<a href=\"mailto:qui.support@powersoft.in\">qui.support@powersoft.in</a>.\r\n    </p>\r\n\r\n    <p>Best regards,<br>\r\n    " +
                            //    "<strong>QUI Team</strong></p>\r\n</body>\r\n</html>\r\n";

                            //foreach (var Mail in MailDetails)
                            //{
                            //    SendEmail(AssignByEmail.ToString(), null, null, "QUI-New Task Assigned : Task No-(" + TaskNo + " )", MailBody, Mail.MAIL_TYPE, "QUI", null, Mail);  // CreatedByEmail.ToString()
                            //}

                            var successsResult = new Add_ApprovalTemplate_TaskOutPut_List_NT
                            {
                                Status = "Ok",
                                Message = "Inserted Successfully",
                                Data = approvaltemplate
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
                            var errorResult = new Add_ApprovalTemplate_TaskOutPut_List_NT
                            {
                                Status = "Error",
                                Message = "Approval Template Insert Failled"
                            };
                            return errorResult;
                        }
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
                var errorResult = new Add_ApprovalTemplate_TaskOutPut_List_NT
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
                var errorResult = new Add_ApprovalTemplate_TaskOutPut_List_NT
                {

                    Status = "Error",
                    Message = ex.Message

                };
                return errorResult;
            }
        }



        public Task<AddApprovalTemplate_HDR_PS_Model> MapApproval_Template_HDR(AddApprovalTemplate_PS_Model template_PS_Model , int userId)
        {
            var subtask_NT_PS = new AddApprovalTemplate_HDR_PS_Model
            {
                MKEY = template_PS_Model.Mkey,
                BUILDING_TYPE = template_PS_Model.BUILDING_TYPE,
                BUILDING_STANDARD = template_PS_Model.BUILDING_STANDARD,
                STATUTORY_AUTHORITY = template_PS_Model.STATUTORY_AUTHORITY,
                MAIN_ABBR = template_PS_Model.MAIN_ABBR,
                SHORT_DESCRIPTION = template_PS_Model.SHORT_DESCRIPTION,
                LONG_DESCRIPTION = template_PS_Model.LONG_DESCRIPTION,
                AUTHORITY_DEPARTMENT = template_PS_Model.AUTHORITY_DEPARTMENT,
                RESPOSIBLE_EMP_MKEY = template_PS_Model.RESPOSIBLE_EMP_MKEY,
                TAGS = template_PS_Model.TAGS,
                JOB_ROLE = template_PS_Model.JOB_ROLE,
                DAYS_REQUIERD = Convert.ToString(template_PS_Model.DAYS_REQUIERD),
                SEQ_ORDER = template_PS_Model.SEQ_ORDER,
                CREATED_BY = userId,
                //CHECKLIST_DOC = template_PS_Model.tASK_CHECKLIST_TABLE_INPUT_NT,
                CREATION_DATE = DateTime.UtcNow,
                DELETE_FLAG = template_PS_Model.DELETE_FLAG,
                ATTRIBUTE1 = null,
                ATTRIBUTE2 = null,
                ATTRIBUTE3 = null,
                ATTRIBUTE4 = null,
                ATTRIBUTE5 = null,
                LAST_UPDATED_BY = null,
                LAST_UPDATE_DATE = null


            };
            return Task.FromResult(subtask_NT_PS);
        }


        public Task<INSERT_APPROVAL_TEMPLATE_TRL_SUBTASK_NT_PS> MapApproval_Tempplate_TRL_SUBTASK_NT(INSERT_APPROVAL_TEMPLATE_TRL_SUBTASK__PS subtask , int header_Mkey ,int userId)
        {
            
              var subtask_NT_PS = new INSERT_APPROVAL_TEMPLATE_TRL_SUBTASK_NT_PS
                {
                    HEADER_MKEY = header_Mkey,
                    SEQ_NO = subtask.SEQ_NO,
                    SUBTASK_ABBR = subtask.SUBTASK_ABBR,
                    SUBTASK_MKEY = subtask.SUBTASK_MKEY,
                    SUBTASK_PARENT_ID = header_Mkey,
                    CREATED_BY = userId,
                    CREATION_DATE = DateTime.UtcNow,
                    DELETE_FLAG = subtask.DELETE_FLAG,
                    ATTRIBUTE1 = null,
                    ATTRIBUTE2 = null,
                    ATTRIBUTE3 = null,
                    ATTRIBUTE4 = null,
                    ATTRIBUTE5 = null,
                    LAST_UPDATED_BY = null,
                    LAST_UPDATE_DATE = null
                };
                return Task.FromResult(subtask_NT_PS);

            
        }


        public Task<INSERT_APPROVAL_TEMPLATE_TRL_SANCTIONING_DEPARTMENT_NT_Model_PS> MapApproval_Tempplate_TRL_Sactioning_NT(INSERT_APPROVAL_TEMPLATE_TRL_SANCTIONING_DEPARTMENT_NT_Model Tsanction, int header_Mkey, int userId)
        {

            var subtask_NT_PS = new INSERT_APPROVAL_TEMPLATE_TRL_SANCTIONING_DEPARTMENT_NT_Model_PS
            {
                HEADER_MKEY = header_Mkey,
                LEVEL = Tsanction.LEVEL,
                SR_NO = Tsanction.SR_NO,
                SANCTIONING_DEPARTMENT = Tsanction.SANCTIONING_DEPARTMENT,
                SANCTIONING_AUTHORITY = Tsanction.SANCTIONING_AUTHORITY,
                SANCTIONING_AUTHORITY_MKEY = Tsanction.SANCTIONING_AUTHORITY_MKEY,
                START_DATE = Tsanction.START_DATE,
                END_DATE = Tsanction.END_DATE,
                CREATED_BY = userId,
                CREATION_DATE = DateTime.UtcNow,
                DELETE_FLAG = Tsanction.DELETE_FLAG,
                ATTRIBUTE1 = null,
                ATTRIBUTE2 = null,
                ATTRIBUTE3 = null,
                ATTRIBUTE4 = null,
                ATTRIBUTE5 = null,
                LAST_UPDATED_BY = null,
                LAST_UPDATE_DATE = null


            };
            return Task.FromResult(subtask_NT_PS);


        }


        public async Task<IEnumerable<Commonresponse>> GetAllApprovalTemplatePSAsync(APPROVAL_TEMPLATE_HDR_INPUT_NT aPPROVAL_TEMPLATE_HDR_NT)
        {
            try
            {
                using (IDbConnection db = _dapperDbConnection.CreateConnection())
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@MKEY", aPPROVAL_TEMPLATE_HDR_NT.Mkey, DbType.Int32);
                    parameters.Add("@Session_User_Id", aPPROVAL_TEMPLATE_HDR_NT.Session_User_Id, DbType.Int32);
                    parameters.Add("@Business_Group_Id", aPPROVAL_TEMPLATE_HDR_NT.Business_Group_Id, DbType.Int32);

                    var approvalTemplates =
                        (await db.QueryAsync<ApprovalTemplatesNT_OUtPutResponse>(
                            "SP_GET_APPROVAL_TEMPLATE_NT",
                            parameters,
                            commandType: CommandType.StoredProcedure
                        )).ToList();

                    if (!approvalTemplates.Any())
                    {
                        return new List<Commonresponse>
                         {
                             new Commonresponse
                             {
                                 Status = "Ok",
                                 Message = "No data found",
                                 Data = approvalTemplates
                             }
                         };
                     }

                    foreach (var approvalTemplate in approvalTemplates)
                    {
                        /* =======================
                           SUBTASK LIST
                           ======================= */
                        approvalTemplate.Subtask_List =
                            (await db.QueryAsync<OUTPUT_APPROVAL_TEMPLATE_TRL_SUBTASK_Ps>(
                                @"SELECT *
                          FROM APPROVAL_TEMPLATE_TRL_SUBTASK
                          WHERE HEADER_MKEY = @HEADER_MKEY
                            AND DELETE_FLAG = 'N'",
                                new { HEADER_MKEY = approvalTemplate.MKEY }
                            )).ToList();

                        /* =======================
                           END RESULT DOCUMENTS
                           ======================= */
                        approvalTemplate.End_Result_Doc_Lst =
                                            (await db.QueryAsync<APPROVAL_TEMPLATE_TRL_ENDRESULT_PS_Model>(
                                                @"SELECT * FROM APPROVAL_TEMPLATE_TRL_ENDRESULT
                                                  WHERE MKEY = @MKEY
                                                    AND DELETE_FLAG = 'N'",
                                                new { MKEY = approvalTemplate.MKEY }
                                            )).ToList();


                        /* =======================
                           CHECKLIST DOCUMENTS
                           ======================= */
                        approvalTemplate.Checklist_Doc_Lst =
                                    (await db.QueryAsync<APPROVAL_TEMPLATE_TRL_CHECKLIST_PS_Model>(
                                        @"SELECT * FROM APPROVAL_TEMPLATE_TRL_CHECKLIST
                                          WHERE MKEY = @MKEY
                                            AND DELETE_FLAG = 'N'",
                                        new { MKEY = approvalTemplate.MKEY }
                                    )).ToList();
                                   

                        /* =======================
                           SANCTIONING DEPARTMENT
                           ======================= */
                        approvalTemplate.Sanctioning_Department_List =
                            (await db.QueryAsync<OUTPUT_APPROVAL_TEMPLATE_TRL_SANCTIONING_DEPARTMENT_Ps>(
                                @"SELECT *
                          FROM V_APPROVAL_TEMPLATE_TRL_SANCTIONING_DEPARTMENT
                          WHERE MKEY = @MKEY
                            AND DELETE_FLAG = 'N'",
                                new { MKEY = approvalTemplate.MKEY }
                            )).ToList();
                    }

                    return new List<Commonresponse>
                           {
                               new Commonresponse
                               {
                                   Status = "Ok",
                                   Message = "Get data successfully",
                                   Data = approvalTemplates
                               }
                           };
                }
            }
            catch (Exception ex)
            {
                return new List<Commonresponse>
                 {
                     new Commonresponse
                     {
                         Status = "Error",
                         Message = ex.Message,
                         Data = null
                     }
                 };
            }
        }
        #endregion
    }
}
