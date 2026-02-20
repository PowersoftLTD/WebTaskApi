using Azure;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using System.Collections.Immutable;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.Json;
using System.Transactions;
using TaskManagement.API.DapperDbConnections;
using TaskManagement.API.Interfaces;
using TaskManagement.API.Model;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace TaskManagement.API.Repositories
{
    public class ApprovalTaskInitiationRepository : IApprovalTaskInitiation
    {
        private static TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        public IDapperDbConnection _dapperDbConnection;
        private readonly string _connectionString;
        public ApprovalTaskInitiationRepository(IDapperDbConnection dapperDbConnection, string connectionString)
        {
            _dapperDbConnection = dapperDbConnection;
            _connectionString = connectionString;
        }
        public async Task<APPROVAL_TASK_INITIATION> GetApprovalTemplateByIdAsync(int MKEY, int APPROVAL_MKEY)
        {
            try
            {
                using (IDbConnection db = _dapperDbConnection.CreateConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@MKEY", MKEY);
                    parmeters.Add("@APPROVAL_MKEY", APPROVAL_MKEY);

                    // Fetch approval template
                    var approvalTemplate = await db.QueryFirstOrDefaultAsync<APPROVAL_TASK_INITIATION>("SP_GET_APPROVAL_TASK_INITIATION", parmeters, commandType: CommandType.StoredProcedure);

                    if (approvalTemplate == null)
                    {
                        var aPPROVAL_TASK_INITIATION = new APPROVAL_TASK_INITIATION();
                        aPPROVAL_TASK_INITIATION.ResponseStatus = "Error";
                        aPPROVAL_TASK_INITIATION.Message = "An unexpected error occurred while retrieving the approval template.";
                        return aPPROVAL_TASK_INITIATION; // Return null if no results
                    }
                    // Fetch subtasks
                    var subtasks = await db.QueryAsync<APPROVAL_TASK_INITIATION_TRL_SUBTASK>("SP_GET_APPROVAL_TASK_INITIATION_TRL_SUBTASK", parmeters, commandType: CommandType.StoredProcedure);
                    approvalTemplate.SUBTASK_LIST = subtasks.ToList(); // Populate the SUBTASK_LIST property with subtasks

                    //var subtasks1 = await db.QueryAsync<APPROVAL_TASK_INITIATION_TRL_SUBTASK>("select * from PROJECT_TRL_APPROVAL_ABBR", commandType: CommandType.Text);
                    approvalTemplate.STATUS = "Ok";
                    approvalTemplate.Message = "Get Data Sucessuly";
                    return approvalTemplate;
                }
            }
            catch (Exception ex)
            {
                var aPPROVAL_TASK_INITIATION = new APPROVAL_TASK_INITIATION();
                aPPROVAL_TASK_INITIATION.ResponseStatus = "Error";
                aPPROVAL_TASK_INITIATION.Message = ex.Message;
                return aPPROVAL_TASK_INITIATION; // Return null if no results
            }
        }
        public async Task<ActionResult<IEnumerable<APPROVAL_TASK_INITIATION_NT_OUTPUT>>> GetApprovalTemplateByIdAsyncNT(APPROVAL_TASK_INITIATION_NT_INUT aPPROVAL_TASK_INITIATION_NT_INUT)
        {
            try
            {
                using (IDbConnection db = _dapperDbConnection.CreateConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@MKEY", aPPROVAL_TASK_INITIATION_NT_INUT.MKEY);
                    parmeters.Add("@APPROVAL_MKEY", aPPROVAL_TASK_INITIATION_NT_INUT.APPROVAL_MKEY);

                    // Fetch approval template
                    var approvalTemplate = await db.QueryAsync<APPROVAL_TASK_INITIATION_NT>("SP_GET_APPROVAL_TASK_INITIATION", parmeters, commandType: CommandType.StoredProcedure);

                    if (approvalTemplate == null)
                    {
                        //var aPPROVAL_TASK_INITIATION = new APPROVAL_TASK_INITIATION();
                        //aPPROVAL_TASK_INITIATION.ResponseStatus = "Error";
                        //aPPROVAL_TASK_INITIATION.Message = "An unexpected error occurred while retrieving the approval template.";
                        //return aPPROVAL_TASK_INITIATION; // Return null if no results

                        var errorResult = new List<APPROVAL_TASK_INITIATION_NT_OUTPUT>
                            {
                                new APPROVAL_TASK_INITIATION_NT_OUTPUT
                                {
                                    Status = "Error",
                                    Message = "An unexpected error occurred while retrieving the approval template!!!",
                                    Data=null
                                }
                            };
                        return errorResult;

                    }
                    // Fetch subtasks
                    var subtasks = await db.QueryAsync<APPROVAL_TASK_INITIATION_TRL_SUBTASK_NT>("SP_GET_APPROVAL_TASK_INITIATION_TRL_SUBTASK", parmeters, commandType: CommandType.StoredProcedure);
                    var subtaskList = subtasks.ToList();
                    
                    foreach (var item in approvalTemplate)
                    {
                        item.SUBTASK_LIST = subtaskList.ToList();
                    }

                    //approvalTemplate.STATUS = "Ok";
                    //approvalTemplate.Message = "Get Data Sucessuly";
                    //return approvalTemplate;

                    var SuccessResult = new List<APPROVAL_TASK_INITIATION_NT_OUTPUT>
                    {
                        new APPROVAL_TASK_INITIATION_NT_OUTPUT
                        {
                            Status = "Error",
                            Message = "An unexpected error occurred while retrieving the approval template!!!",
                            Data= approvalTemplate
                        }
                    };
                    return SuccessResult;
                }
            }
            catch (Exception ex)
            {
                var errorResult = new List<APPROVAL_TASK_INITIATION_NT_OUTPUT>
                {
                    new APPROVAL_TASK_INITIATION_NT_OUTPUT
                    {
                        Status = "Error",
                        Message = ex.Message,
                        Data=null
                    }
                };
                return errorResult;
            }
        }
        //public async Task<APPROVAL_TASK_INITIATION> CreateTaskApprovalTemplateAsync(APPROVAL_TASK_INITIATION aPPROVAL_TASK_INITIATION)
        //{
        //    DateTime dateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
        //    IDbTransaction transaction = null;
        //    bool transactionCompleted = false;  // Track the transaction state

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

        //            var parmeters = new DynamicParameters();
        //            parmeters.Add("@TASK_NO", aPPROVAL_TASK_INITIATION.TASK_NO);
        //            parmeters.Add("@TASK_NAME", aPPROVAL_TASK_INITIATION.MAIN_ABBR);
        //            parmeters.Add("@TASK_DESCRIPTION", aPPROVAL_TASK_INITIATION.LONG_DESCRIPTION);
        //            parmeters.Add("@CATEGORY", aPPROVAL_TASK_INITIATION.CAREGORY);
        //            parmeters.Add("@PROJECT_ID", aPPROVAL_TASK_INITIATION.PROPERTY);
        //            parmeters.Add("@SUBPROJECT_ID", aPPROVAL_TASK_INITIATION.BUILDING_MKEY);
        //            parmeters.Add("@COMPLETION_DATE", aPPROVAL_TASK_INITIATION.COMPLITION_DATE);
        //            parmeters.Add("@ASSIGNED_TO", aPPROVAL_TASK_INITIATION.RESPOSIBLE_EMP_MKEY);
        //            parmeters.Add("@TAGS", aPPROVAL_TASK_INITIATION.TAGS);
        //            parmeters.Add("@ISNODE", "Y");
        //            parmeters.Add("@CLOSE_DATE", aPPROVAL_TASK_INITIATION.TENTATIVE_END_DATE);
        //            parmeters.Add("@DUE_DATE", aPPROVAL_TASK_INITIATION.TENTATIVE_END_DATE);
        //            parmeters.Add("@TASK_PARENT_ID", 0);
        //            parmeters.Add("@STATUS", aPPROVAL_TASK_INITIATION.STATUS);
        //            parmeters.Add("@TASK_TYPE", "359");
        //            parmeters.Add("@STATUS_PERC", 0.0);
        //            parmeters.Add("@TASK_CREATED_BY", aPPROVAL_TASK_INITIATION.RESPOSIBLE_EMP_MKEY);
        //            parmeters.Add("@APPROVER_ID", 0);
        //            parmeters.Add("@IS_ARCHIVE", null);
        //            parmeters.Add("@ATTRIBUTE1", null);
        //            parmeters.Add("@ATTRIBUTE2", null);
        //            parmeters.Add("@ATTRIBUTE3", null);
        //            parmeters.Add("@ATTRIBUTE4", aPPROVAL_TASK_INITIATION.MKEY);
        //            parmeters.Add("@ATTRIBUTE5", aPPROVAL_TASK_INITIATION.HEADER_MKEY);
        //            parmeters.Add("@CREATED_BY", aPPROVAL_TASK_INITIATION.RESPOSIBLE_EMP_MKEY);
        //            parmeters.Add("@CREATION_DATE", dateTime);
        //            parmeters.Add("@LAST_UPDATED_BY", aPPROVAL_TASK_INITIATION.RESPOSIBLE_EMP_MKEY);

        //            var approvalTemplate = await db.QueryFirstOrDefaultAsync<APPROVAL_TASK_INITIATION>("SP_INSERT_TASK_DETAILS", parmeters,
        //                commandType: CommandType.StoredProcedure, transaction: transaction);

        //            if (approvalTemplate == null)
        //            {
        //                // Handle other unexpected exceptions
        //                if (transaction != null && !transactionCompleted)
        //                {
        //                    try
        //                    {
        //                        // Rollback only if the transaction is not yet completed
        //                        transaction.Rollback();
        //                    }
        //                    catch (InvalidOperationException rollbackEx)
        //                    {

        //                        Console.WriteLine($"Rollback failed: {rollbackEx.Message}");
        //                        //TranError.Message = ex.Message;
        //                        //return TranError;
        //                    }
        //                }

        //                var TemplateError = new APPROVAL_TASK_INITIATION();
        //                TemplateError.ResponseStatus = "Error";
        //                TemplateError.Message = "Error Occurd";
        //                return TemplateError;
        //            }

        //            var parmetersTaskNo = new DynamicParameters();
        //            parmetersTaskNo.Add("@MKEY", aPPROVAL_TASK_INITIATION.HEADER_MKEY);
        //            parmetersTaskNo.Add("@APPROVAL_MKEY", aPPROVAL_TASK_INITIATION.MKEY);
        //            parmetersTaskNo.Add("@TASK_NO_MKEY", approvalTemplate.MKEY);
        //            parmetersTaskNo.Add("@TENTATIVE_START_DATE", aPPROVAL_TASK_INITIATION.TENTATIVE_START_DATE);
        //            parmetersTaskNo.Add("@RESPOSIBLE_EMP_MKEY", aPPROVAL_TASK_INITIATION.RESPOSIBLE_EMP_MKEY);
        //            parmetersTaskNo.Add("@LAST_UPDATED_BY", aPPROVAL_TASK_INITIATION.CREATED_BY);
        //            parmetersTaskNo.Add("@INITIATOR", aPPROVAL_TASK_INITIATION.INITIATOR);
        //            parmetersTaskNo.Add("@TAGS", aPPROVAL_TASK_INITIATION.TAGS);

        //            var UpadteTaskNo = await db.QueryFirstOrDefaultAsync<APPROVAL_TASK_INITIATION>("SP_UPDATE_APPROVAL_TASK_NO", parmetersTaskNo, commandType: CommandType.StoredProcedure, transaction: transaction);

        //            foreach (var SubTask in aPPROVAL_TASK_INITIATION.SUBTASK_LIST)
        //            {
        //                if (SubTask.APPROVAL_MKEY == aPPROVAL_TASK_INITIATION.MKEY)
        //                {
        //                    continue;
        //                }
        //                if (sqlConnection.State != ConnectionState.Open)
        //                {
        //                    await sqlConnection.OpenAsync();  // Ensure the connection is open
        //                }

        //                if (sqlConnection.State == ConnectionState.Open && transaction == null)
        //                {
        //                    transaction = sqlConnection.BeginTransaction(); // Start a new transaction
        //                }

        //                //var SubParentMkey = await db.QueryFirstOrDefaultAsync<APPROVAL_TASK_INITIATION_TRL_SUBTASK>("SELECT MKEY FROM TASK_HDR " +
        //                //    "WHERE ATTRIBUTE4 IN (SELECT SUBTASK_PARENT_ID FROM APPROVAL_TEMPLATE_TRL_SUBTASK " +
        //                //    "WHERE SUBTASK_MKEY = @APPROVAL_MKEY AND DELETE_FLAG = 'N') " +
        //                //    " AND DELETE_FLAG = 'N' AND ATTRIBUTE5 IN (SELECT MKEY FROM PROJECT_HDR WHERE MKEY = @MKEY AND DELETE_FLAG = 'N') ",
        //                //    new { APPROVAL_MKEY = SubTask.APPROVAL_MKEY, MKEY = SubTask.MKEY }, transaction: transaction);

        //                var parmetersParentApproval = new DynamicParameters();
        //                parmetersParentApproval.Add("@MKEY", aPPROVAL_TASK_INITIATION.HEADER_MKEY);
        //                parmetersParentApproval.Add("@APPROVAL_MKEY", aPPROVAL_TASK_INITIATION.MKEY);

        //                // Fetch approval template
        //                var SubParentMkey = await db.QueryFirstOrDefaultAsync<APPROVAL_TASK_INITIATION>("SP_GET_APPROVAL_TASK_INITIATION"
        //                    , parmetersParentApproval, commandType: CommandType.StoredProcedure, transaction: transaction);

        //                string TaskPrentNo = string.Empty;

        //                if (SubParentMkey.MKEY != null)
        //                {
        //                    var ParentTask_no = await db.QueryFirstOrDefaultAsync<APPROVAL_TASK_INITIATION_TRL_SUBTASK>("select " +
        //                        " CONVERT(VARCHAR(50),TASK_NO) AS TASK_NO from TASK_HDR WITH (NOLOCK)  WHERE MKEY = @MKEY",
        //                         new { MKEY = SubParentMkey.MKEY }, transaction: transaction);
        //                    try
        //                    {
        //                        TaskPrentNo = ParentTask_no.MKEY.ToString();
        //                    }
        //                    catch{}
        //                    if(TaskPrentNo == string.Empty)
        //                    {
        //                        var Task_noParent = await db.QueryFirstOrDefaultAsync<APPROVAL_TASK_INITIATION_TRL_SUBTASK>("select " +
        //                       " CONVERT(VARCHAR(50),TASK_NO) AS TASK_NO from TASK_HDR WITH (NOLOCK)  WHERE MKEY = @MKEY",
        //                        new { MKEY = approvalTemplate.MKEY }, transaction: transaction);
        //                        TaskPrentNo = Task_noParent.MKEY.ToString();
        //                    }
        //                }
        //                else
        //                {
        //                    TaskPrentNo = approvalTemplate.TASK_NO.ToString();
        //                }
        //                var Parent_Mkey = await db.QueryFirstOrDefaultAsync<APPROVAL_TASK_INITIATION_TRL_SUBTASK>("SELECT * " +
        //                    " FROM V_Task_Parent_ID " +
        //                    " WHERE SUBTASK_PARENT_ID = @SUBTASK_MKEY ", new { SUBTASK_MKEY = SubTask.APPROVAL_MKEY }, transaction: transaction);

        //                var parmetersSubtask = new DynamicParameters();
        //                parmetersSubtask.Add("@TASK_NO", SubParentMkey.MKEY);
        //                parmetersSubtask.Add("@TASK_NAME", SubTask.APPROVAL_ABBRIVATION);
        //                parmetersSubtask.Add("@TASK_DESCRIPTION", SubTask.LONG_DESCRIPTION);
        //                parmetersSubtask.Add("@CATEGORY", aPPROVAL_TASK_INITIATION.CAREGORY);
        //                parmetersSubtask.Add("@PROJECT_ID", aPPROVAL_TASK_INITIATION.PROPERTY);
        //                parmetersSubtask.Add("@SUBPROJECT_ID", aPPROVAL_TASK_INITIATION.BUILDING_MKEY);
        //                parmetersSubtask.Add("@COMPLETION_DATE", SubTask.TENTATIVE_END_DATE);
        //                parmetersSubtask.Add("@ASSIGNED_TO", SubTask.RESPOSIBLE_EMP_MKEY);
        //                parmetersSubtask.Add("@TAGS", SubTask.TAGS);
        //                parmetersSubtask.Add("@CLOSE_DATE", SubTask.TENTATIVE_END_DATE);
        //                parmetersSubtask.Add("@DUE_DATE", SubTask.TENTATIVE_END_DATE);
        //                parmetersSubtask.Add("@TASK_PARENT_NODE_ID", SubParentMkey.MKEY); // approvalTemplate.MKEY);
        //                parmetersSubtask.Add("@TASK_PARENT_NUMBER", TaskPrentNo); // ParentTask_no
        //                parmetersSubtask.Add("@TASK_TYPE", "359");
        //                parmetersSubtask.Add("@STATUS", SubTask.STATUS);
        //                parmetersSubtask.Add("@STATUS_PERC", "0.0");
        //                parmetersSubtask.Add("@TASK_CREATED_BY", aPPROVAL_TASK_INITIATION.RESPOSIBLE_EMP_MKEY); // header of task
        //                parmetersSubtask.Add("@APPROVER_ID", 0);
        //                parmetersSubtask.Add("@IS_ARCHIVE", 'N');
        //                parmetersSubtask.Add("@ATTRIBUTE1", null);
        //                parmetersSubtask.Add("@ATTRIBUTE2", null);
        //                parmetersSubtask.Add("@ATTRIBUTE3", null);
        //                parmetersSubtask.Add("@ATTRIBUTE4", SubTask.APPROVAL_MKEY);
        //                parmetersSubtask.Add("@ATTRIBUTE5", aPPROVAL_TASK_INITIATION.HEADER_MKEY);
        //                parmetersSubtask.Add("@CREATED_BY", aPPROVAL_TASK_INITIATION.CREATED_BY);
        //                parmetersSubtask.Add("@CREATION_DATE", dateTime);
        //                parmetersSubtask.Add("@LAST_UPDATED_BY", aPPROVAL_TASK_INITIATION.CREATED_BY);
        //                parmetersSubtask.Add("@APPROVE_ACTION_DATE", null);
        //                parmetersSubtask.Add("@ATTRIBUTE5", aPPROVAL_TASK_INITIATION.HEADER_MKEY); // PROJECT ID 
        //                parmetersSubtask.Add("@Current_Task_Mkey", SubParentMkey.MKEY);

        //                if (SubParentMkey != null)  // to check parent node
        //                {
        //                    parmetersSubtask.Add("@TASK_PARENT_ID", SubParentMkey.MKEY);
        //                }
        //                else
        //                {
        //                    parmetersSubtask.Add("@TASK_PARENT_ID", approvalTemplate.MKEY);
        //                }

        //                if (Parent_Mkey != null) // IF THIS PARENT THEN IDNODE Y ELSE N
        //                {
        //                    parmetersSubtask.Add("@ISNODE", "Y");
        //                }
        //                else
        //                {
        //                    parmetersSubtask.Add("@ISNODE", "N");
        //                }
        //                var approvalSubTemplate = await db.QueryFirstOrDefaultAsync<TASK_HDR>("SP_INSERT_TASK_NODE_DETAILS", parmetersSubtask, commandType: CommandType.StoredProcedure, transaction: transaction);
        //                var parmetersSubTaskNo = new DynamicParameters();
        //                parmetersSubTaskNo.Add("@MKEY", SubTask.MKEY);
        //                parmetersSubTaskNo.Add("@APPROVAL_MKEY", SubTask.APPROVAL_MKEY);
        //                parmetersSubTaskNo.Add("@TASK_NO_MKEY", approvalSubTemplate.MKEY);
        //                parmetersSubTaskNo.Add("@COMPLETION_DATE", SubTask.TENTATIVE_END_DATE);
        //                parmetersSubTaskNo.Add("@TENTATIVE_START_DATE", SubTask.TENTATIVE_START_DATE);
        //                parmetersSubTaskNo.Add("@TENTATIVE_END_DATE", SubTask.TENTATIVE_END_DATE);
        //                parmetersSubTaskNo.Add("@DAYS_REQUIRED", SubTask.DAYS_REQUIRED);
        //                parmetersSubTaskNo.Add("@LAST_UPDATED_BY", aPPROVAL_TASK_INITIATION.CREATED_BY);
        //                parmetersSubTaskNo.Add("@INITIATOR", aPPROVAL_TASK_INITIATION.INITIATOR);
        //                parmetersSubTaskNo.Add("@TAGS", SubTask.TAGS);

        //                var UpadteSubTaskNo = await db.QueryFirstOrDefaultAsync<APPROVAL_TASK_INITIATION>("SP_UPDATE_APPROVAL_TASK_NO", parmetersSubTaskNo, commandType: CommandType.StoredProcedure, transaction: transaction);
        //            }

        //            // Commit the transaction if everything is successful
        //            var sqlTransaction = (SqlTransaction)transaction;
        //            await sqlTransaction.CommitAsync();
        //            transactionCompleted = true;  // Mark the transaction as completed

        //            using (IDbConnection db_Approval = _dapperDbConnection.CreateConnection())
        //            {
        //                var parmetersApproval = new DynamicParameters();
        //                parmetersApproval.Add("@MKEY", aPPROVAL_TASK_INITIATION.HEADER_MKEY);
        //                parmetersApproval.Add("@APPROVAL_MKEY", aPPROVAL_TASK_INITIATION.MKEY);

        //                // Fetch approval template
        //                var AllApprovalTemplate = await db.QueryFirstOrDefaultAsync<APPROVAL_TASK_INITIATION>("SP_GET_APPROVAL_TASK_INITIATION", parmetersApproval, commandType: CommandType.StoredProcedure);

        //                if (AllApprovalTemplate == null)
        //                {
        //                    var TASK_INITIATION = new APPROVAL_TASK_INITIATION();
        //                    TASK_INITIATION.ResponseStatus = "Error";
        //                    TASK_INITIATION.Message = "An unexpected error occurred while retrieving the approval template.";
        //                    return TASK_INITIATION; // Return null if no results
        //                }

        //                // Fetch subtasks
        //                var subtasks = await db.QueryAsync<APPROVAL_TASK_INITIATION_TRL_SUBTASK>("SP_GET_APPROVAL_TASK_INITIATION_TRL_SUBTASK", parmetersApproval, commandType: CommandType.StoredProcedure);
        //                approvalTemplate.SUBTASK_LIST = subtasks.ToList(); // Populate the SUBTASK_LIST property with subtasks

        //                //var subtasks1 = await db.QueryAsync<APPROVAL_TASK_INITIATION_TRL_SUBTASK>("select * from PROJECT_TRL_APPROVAL_ABBR", commandType: CommandType.Text);
        //                AllApprovalTemplate.STATUS = "Ok";
        //                AllApprovalTemplate.Message = "Data save successfully";
        //                return AllApprovalTemplate;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Handle other unexpected exceptions
        //        if (transaction != null && !transactionCompleted)
        //        {
        //            try
        //            {
        //                // Rollback only if the transaction is not yet completed
        //                transaction.Rollback();
        //            }
        //            catch (InvalidOperationException rollbackEx)
        //            {
        //                // Handle rollback exception (may occur if transaction is already completed)
        //                // Log or handle the rollback failure if needed
        //                Console.WriteLine($"Rollback failed: {rollbackEx.Message}");
        //                //var TranError = new APPROVAL_TASK_INITIATION();
        //                //TranError.ResponseStatus = "Error";
        //                //TranError.Message = ex.Message;
        //                //return TranError;
        //            }
        //        }

        //        var approvalTemplate = new APPROVAL_TASK_INITIATION();
        //        approvalTemplate.ResponseStatus = "Error";
        //        approvalTemplate.Message = ex.Message;
        //        return approvalTemplate;
        //    }
        //}
        public async Task<APPROVAL_TASK_INITIATION> CreateTaskApprovalTemplateAsync(APPROVAL_TASK_INITIATION aPPROVAL_TASK_INITIATION)
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
                    parmeters.Add("@TASK_NO", aPPROVAL_TASK_INITIATION.TASK_NO);
                    parmeters.Add("@TASK_NAME", aPPROVAL_TASK_INITIATION.MAIN_ABBR); 
                    parmeters.Add("@TASK_DESCRIPTION", aPPROVAL_TASK_INITIATION.LONG_DESCRIPTION);
                    parmeters.Add("@CATEGORY", aPPROVAL_TASK_INITIATION.CAREGORY);
                    parmeters.Add("@PROJECT_ID", aPPROVAL_TASK_INITIATION.PROPERTY);
                    parmeters.Add("@SUBPROJECT_ID", aPPROVAL_TASK_INITIATION.BUILDING_MKEY);
                    parmeters.Add("@COMPLETION_DATE", aPPROVAL_TASK_INITIATION.COMPLITION_DATE);
                    parmeters.Add("@ASSIGNED_TO", aPPROVAL_TASK_INITIATION.RESPOSIBLE_EMP_MKEY);
                    parmeters.Add("@TAGS", aPPROVAL_TASK_INITIATION.TAGS);
                    parmeters.Add("@ISNODE", "Y");
                    parmeters.Add("@CLOSE_DATE", aPPROVAL_TASK_INITIATION.TENTATIVE_END_DATE);
                    parmeters.Add("@DUE_DATE", aPPROVAL_TASK_INITIATION.TENTATIVE_END_DATE);
                    parmeters.Add("@TASK_PARENT_ID", 0);
                    parmeters.Add("@STATUS", aPPROVAL_TASK_INITIATION.STATUS);
                    parmeters.Add("@TASK_TYPE", "359");
                    parmeters.Add("@STATUS_PERC", 0.0);
                    parmeters.Add("@TASK_CREATED_BY", aPPROVAL_TASK_INITIATION.INITIATOR);
                    parmeters.Add("@APPROVER_ID", 0);
                    parmeters.Add("@IS_ARCHIVE", null);
                    parmeters.Add("@ATTRIBUTE1", null);
                    parmeters.Add("@ATTRIBUTE2", null);
                    parmeters.Add("@ATTRIBUTE3", null);
                    parmeters.Add("@ATTRIBUTE4", aPPROVAL_TASK_INITIATION.MKEY);
                    parmeters.Add("@ATTRIBUTE5", aPPROVAL_TASK_INITIATION.HEADER_MKEY);
                    parmeters.Add("@CREATED_BY", aPPROVAL_TASK_INITIATION.CREATED_BY);
                    parmeters.Add("@CREATION_DATE", dateTime);
                    parmeters.Add("@LAST_UPDATED_BY", aPPROVAL_TASK_INITIATION.CREATED_BY);
                   

                    var approvalTemplate = await db.QueryFirstOrDefaultAsync<APPROVAL_TASK_INITIATION>("SP_INSERT_TASK_DETAILS", parmeters, commandType: CommandType.StoredProcedure, transaction: transaction);

                    if (approvalTemplate == null)
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

                        var TemplateError = new APPROVAL_TASK_INITIATION();
                        TemplateError.ResponseStatus = "Error";
                        TemplateError.Message = "Error Occurd";
                        return TemplateError;
                    }

                    var parmetersTaskNo = new DynamicParameters();
                    parmetersTaskNo.Add("@MKEY", aPPROVAL_TASK_INITIATION.HEADER_MKEY);
                    parmetersTaskNo.Add("@APPROVAL_MKEY", aPPROVAL_TASK_INITIATION.MKEY);
                    parmetersTaskNo.Add("@TASK_NO_MKEY", approvalTemplate.MKEY);
                    parmetersTaskNo.Add("@TENTATIVE_START_DATE", aPPROVAL_TASK_INITIATION.TENTATIVE_START_DATE);
                    parmetersTaskNo.Add("@RESPOSIBLE_EMP_MKEY", aPPROVAL_TASK_INITIATION.RESPOSIBLE_EMP_MKEY);
                    parmetersTaskNo.Add("@LAST_UPDATED_BY", aPPROVAL_TASK_INITIATION.CREATED_BY);
                    parmetersTaskNo.Add("@INITIATOR", aPPROVAL_TASK_INITIATION.INITIATOR);
                    parmetersTaskNo.Add("@TAGS", aPPROVAL_TASK_INITIATION.TAGS);

                    var UpadteTaskNo = await db.QueryFirstOrDefaultAsync<APPROVAL_TASK_INITIATION>("SP_UPDATE_APPROVAL_TASK_NO", parmetersTaskNo, commandType: CommandType.StoredProcedure, transaction: transaction);

                    foreach (var SubTask in aPPROVAL_TASK_INITIATION.SUBTASK_LIST)
                    {
                        if (SubTask.APPROVAL_MKEY == aPPROVAL_TASK_INITIATION.MKEY)
                        {
                            continue;
                        }
                        if (sqlConnection.State != ConnectionState.Open)
                        {
                            await sqlConnection.OpenAsync();  // Ensure the connection is open
                        }

                        if (sqlConnection.State == ConnectionState.Open && transaction == null)
                        {
                            transaction = sqlConnection.BeginTransaction(); // Start a new transaction
                        }

                        //var SubParentMkey = await db.QueryFirstOrDefaultAsync<APPROVAL_TASK_INITIATION_TRL_SUBTASK>("SELECT MKEY FROM TASK_HDR " +
                        //    " WHERE ATTRIBUTE4 IN (SELECT SUBTASK_PARENT_ID FROM APPROVAL_TEMPLATE_TRL_SUBTASK " +
                        //    " WHERE SUBTASK_MKEY = @APPROVAL_MKEY AND DELETE_FLAG = 'N') " +
                        //    " AND DELETE_FLAG = 'N' AND ATTRIBUTE5 IN (SELECT MKEY FROM PROJECT_HDR WHERE MKEY = @MKEY AND DELETE_FLAG = 'N') ",

                           var SubParentMkey = await db.QueryFirstOrDefaultAsync<APPROVAL_TASK_INITIATION_TRL_SUBTASK>(" SELECT MKEY  " +
                           " FROM TASK_HDR WHERE ATTRIBUTE4 = CAST((SELECT SUBTASK_PARENT_ID FROM APPROVAL_TEMPLATE_TRL_SUBTASK "+
                           " WHERE SUBTASK_MKEY = @APPROVAL_MKEY AND DELETE_FLAG = 'N') AS NVARCHAR(10)) " +
                           " AND DELETE_FLAG = 'N' " +
                           " AND ATTRIBUTE5 = CAST((SELECT MKEY FROM PROJECT_HDR WHERE MKEY = @MKEY AND DELETE_FLAG = 'N') AS NVARCHAR(10)) ", 
                            new { APPROVAL_MKEY = SubTask.APPROVAL_MKEY, MKEY = SubTask.MKEY }, transaction: transaction);
                            string TaskPrentNo = string.Empty;

                        if (SubParentMkey != null)
                        {
                            var ParentTask_no = await db.QueryFirstOrDefaultAsync<APPROVAL_TASK_INITIATION_TRL_SUBTASK>("select " +
                                " CONVERT(VARCHAR(50),TASK_NO)AS TASK_NO from TASK_HDR WITH (NOLOCK)  WHERE MKEY = @MKEY",
                                 new { MKEY = SubParentMkey.MKEY }, transaction: transaction);
                            TaskPrentNo = ParentTask_no.TASK_NO.ToString();
                        }
                        else
                        {
                            TaskPrentNo = approvalTemplate.TASK_NO.ToString();
                        }
                        var Parent_Mkey = await db.QueryFirstOrDefaultAsync<APPROVAL_TASK_INITIATION_TRL_SUBTASK>("SELECT * " +
                            " FROM V_Task_Parent_ID " +
                            " WHERE SUBTASK_PARENT_ID = @SUBTASK_MKEY ", new { SUBTASK_MKEY = SubTask.APPROVAL_MKEY }, transaction: transaction);

                        var parmetersSubtask = new DynamicParameters();
                        
                        parmetersSubtask.Add("@TASK_NO", SubParentMkey.MKEY ); 
                        parmetersSubtask.Add("@TASK_NAME", SubTask.APPROVAL_ABBRIVATION);    //Commented by Itemad Hyder 21-11-2025
                        parmetersSubtask.Add("@TASK_DESCRIPTION", SubTask.LONG_DESCRIPTION);
                        parmetersSubtask.Add("@CATEGORY", aPPROVAL_TASK_INITIATION.CAREGORY);
                        parmetersSubtask.Add("@PROJECT_ID", aPPROVAL_TASK_INITIATION.PROPERTY);
                        parmetersSubtask.Add("@SUBPROJECT_ID", aPPROVAL_TASK_INITIATION.BUILDING_MKEY);
                        parmetersSubtask.Add("@COMPLETION_DATE", SubTask.TENTATIVE_END_DATE);
                        parmetersSubtask.Add("@ASSIGNED_TO", SubTask.RESPOSIBLE_EMP_MKEY);
                        parmetersSubtask.Add("@TAGS", SubTask.TAGS);
                        parmetersSubtask.Add("@CLOSE_DATE", SubTask.TENTATIVE_END_DATE);
                        parmetersSubtask.Add("@DUE_DATE", SubTask.TENTATIVE_END_DATE);
                        parmetersSubtask.Add("@TASK_PARENT_NODE_ID", SubParentMkey.MKEY); // approvalTemplate.MKEY);
                        parmetersSubtask.Add("@TASK_PARENT_NUMBER", TaskPrentNo); // ParentTask_no
                        parmetersSubtask.Add("@TASK_TYPE", "359");
                        parmetersSubtask.Add("@STATUS", SubTask.STATUS);
                        parmetersSubtask.Add("@STATUS_PERC", "0.0");
                        parmetersSubtask.Add("@TASK_CREATED_BY", aPPROVAL_TASK_INITIATION.RESPOSIBLE_EMP_MKEY); // header of task
                        parmetersSubtask.Add("@APPROVER_ID", 0);
                        parmetersSubtask.Add("@IS_ARCHIVE", 'N');
                        parmetersSubtask.Add("@ATTRIBUTE1", null);
                        parmetersSubtask.Add("@ATTRIBUTE2", null);
                        parmetersSubtask.Add("@ATTRIBUTE3", null);
                        parmetersSubtask.Add("@ATTRIBUTE4", SubTask.APPROVAL_MKEY);
                        parmetersSubtask.Add("@ATTRIBUTE5", aPPROVAL_TASK_INITIATION.HEADER_MKEY);
                        parmetersSubtask.Add("@CREATED_BY", aPPROVAL_TASK_INITIATION.CREATED_BY);
                        parmetersSubtask.Add("@CREATION_DATE", dateTime);
                        parmetersSubtask.Add("@LAST_UPDATED_BY", aPPROVAL_TASK_INITIATION.CREATED_BY);
                        parmetersSubtask.Add("@APPROVE_ACTION_DATE", null);
                        parmetersSubtask.Add("@ATTRIBUTE5", aPPROVAL_TASK_INITIATION.HEADER_MKEY); // PROJECT ID 
                        parmetersSubtask.Add("@Current_Task_Mkey", SubParentMkey.MKEY);

                        if (SubParentMkey != null)  // to check parent node
                        {
                            parmetersSubtask.Add("@TASK_PARENT_ID", SubParentMkey.MKEY);
                        }
                        else
                        {
                            parmetersSubtask.Add("@TASK_PARENT_ID", approvalTemplate.MKEY);
                        }

                        if (Parent_Mkey != null) // IF THIS PARENT THEN IDNODE Y ELSE N
                        {
                            parmetersSubtask.Add("@ISNODE", "Y");
                        }
                        else
                        {
                            parmetersSubtask.Add("@ISNODE", "N");
                        }
                        var approvalSubTemplate = await db.QueryFirstOrDefaultAsync<TASK_HDR>("SP_INSERT_TASK_NODE_DETAILS", parmetersSubtask, commandType: CommandType.StoredProcedure, transaction: transaction);
                        var parmetersSubTaskNo = new DynamicParameters();
                        parmetersSubTaskNo.Add("@MKEY", SubTask.MKEY);
                        parmetersSubTaskNo.Add("@APPROVAL_MKEY", SubTask.APPROVAL_MKEY);
                        parmetersSubTaskNo.Add("@TASK_NO_MKEY", approvalSubTemplate.MKEY);
                        parmetersSubTaskNo.Add("@COMPLETION_DATE", SubTask.TENTATIVE_END_DATE);
                        parmetersSubTaskNo.Add("@TENTATIVE_START_DATE", SubTask.TENTATIVE_START_DATE);
                        parmetersSubTaskNo.Add("@TENTATIVE_END_DATE", SubTask.TENTATIVE_END_DATE);
                        parmetersSubTaskNo.Add("@DAYS_REQUIRED", SubTask.DAYS_REQUIRED);
                        parmetersSubTaskNo.Add("@LAST_UPDATED_BY", aPPROVAL_TASK_INITIATION.CREATED_BY);
                        parmetersSubTaskNo.Add("@INITIATOR", aPPROVAL_TASK_INITIATION.INITIATOR);
                        parmetersSubTaskNo.Add("@TAGS", SubTask.TAGS);

                        var UpadteSubTaskNo = await db.QueryFirstOrDefaultAsync<APPROVAL_TASK_INITIATION>("SP_UPDATE_APPROVAL_TASK_NO", parmetersSubTaskNo, commandType: CommandType.StoredProcedure, transaction: transaction);
                    }

                    // Commit the transaction if everything is successful
                    var sqlTransaction = (SqlTransaction)transaction;
                    await sqlTransaction.CommitAsync();
                    transactionCompleted = true;  // Mark the transaction as completed

                    using (IDbConnection db_Approval = _dapperDbConnection.CreateConnection())
                    {
                        var parmetersApproval = new DynamicParameters();
                        parmetersApproval.Add("@MKEY", aPPROVAL_TASK_INITIATION.HEADER_MKEY);
                        parmetersApproval.Add("@APPROVAL_MKEY", aPPROVAL_TASK_INITIATION.MKEY);

                        // Fetch approval template
                        var AllApprovalTemplate = await db.QueryFirstOrDefaultAsync<APPROVAL_TASK_INITIATION>("SP_GET_APPROVAL_TASK_INITIATION", parmetersApproval, commandType: CommandType.StoredProcedure);

                        if (AllApprovalTemplate == null)
                        {
                            var TASK_INITIATION = new APPROVAL_TASK_INITIATION();
                            TASK_INITIATION.ResponseStatus = "Error";
                            TASK_INITIATION.Message = "An unexpected error occurred while retrieving the approval template.";
                            return TASK_INITIATION; // Return null if no results
                        }

                        // Fetch subtasks
                        var subtasks = await db.QueryAsync<APPROVAL_TASK_INITIATION_TRL_SUBTASK>("SP_GET_APPROVAL_TASK_INITIATION_TRL_SUBTASK", parmetersApproval, commandType: CommandType.StoredProcedure);
                        approvalTemplate.SUBTASK_LIST = subtasks.ToList(); // Populate the SUBTASK_LIST property with subtasks

                        //var subtasks1 = await db.QueryAsync<APPROVAL_TASK_INITIATION_TRL_SUBTASK>("select * from PROJECT_TRL_APPROVAL_ABBR", commandType: CommandType.Text);
                        AllApprovalTemplate.STATUS = "Ok";
                        AllApprovalTemplate.Message = "Data save successfully";
                        return AllApprovalTemplate;
                    }
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
                        //var TranError = new APPROVAL_TASK_INITIATION();
                        //TranError.ResponseStatus = "Error";
                        //TranError.Message = ex.Message;
                        //return TranError;
                    }
                }

                var approvalTemplate = new APPROVAL_TASK_INITIATION();
                approvalTemplate.ResponseStatus = "Error";
                approvalTemplate.Message = ex.Message;
                return approvalTemplate;
            }
        }

        public async Task<ActionResult<IEnumerable<APPROVAL_TASK_INITIATION_NT_OUTPUT>>> CreateTaskApprovalTemplateAsyncNT(APPROVAL_TASK_INITIATION_INPUT_NT aPPROVAL_TASK_INITIATION)
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
                    parmeters.Add("@TASK_NO", aPPROVAL_TASK_INITIATION.TASK_NO);
                    parmeters.Add("@TASK_NAME", aPPROVAL_TASK_INITIATION.MAIN_ABBR);
                    parmeters.Add("@TASK_DESCRIPTION", aPPROVAL_TASK_INITIATION.LONG_DESCRIPTION);
                    parmeters.Add("@CATEGORY", aPPROVAL_TASK_INITIATION.CAREGORY);
                    parmeters.Add("@PROJECT_ID", aPPROVAL_TASK_INITIATION.PROPERTY);
                    parmeters.Add("@SUBPROJECT_ID", aPPROVAL_TASK_INITIATION.BUILDING_MKEY);
                    parmeters.Add("@COMPLETION_DATE", aPPROVAL_TASK_INITIATION.COMPLITION_DATE);
                    parmeters.Add("@ASSIGNED_TO", aPPROVAL_TASK_INITIATION.RESPOSIBLE_EMP_MKEY);
                    parmeters.Add("@TAGS", aPPROVAL_TASK_INITIATION.TAGS);
                    parmeters.Add("@ISNODE", "Y");
                    parmeters.Add("@CLOSE_DATE", aPPROVAL_TASK_INITIATION.TENTATIVE_END_DATE);
                    parmeters.Add("@DUE_DATE", aPPROVAL_TASK_INITIATION.TENTATIVE_END_DATE);
                    parmeters.Add("@TASK_PARENT_ID", 0);
                    parmeters.Add("@STATUS", aPPROVAL_TASK_INITIATION.STATUS);
                    parmeters.Add("@TASK_TYPE", "359");
                    parmeters.Add("@STATUS_PERC", 0.0);
                    parmeters.Add("@TASK_CREATED_BY", aPPROVAL_TASK_INITIATION.RESPOSIBLE_EMP_MKEY);
                    parmeters.Add("@APPROVER_ID", 0);
                    parmeters.Add("@IS_ARCHIVE", null);
                    parmeters.Add("@ATTRIBUTE1", null);
                    parmeters.Add("@ATTRIBUTE2", null);
                    parmeters.Add("@ATTRIBUTE3", null);
                    parmeters.Add("@ATTRIBUTE4", aPPROVAL_TASK_INITIATION.MKEY);
                    parmeters.Add("@ATTRIBUTE5", aPPROVAL_TASK_INITIATION.HEADER_MKEY);
                    parmeters.Add("@CREATED_BY", aPPROVAL_TASK_INITIATION.RESPOSIBLE_EMP_MKEY);
                    parmeters.Add("@CREATION_DATE", dateTime);
                    parmeters.Add("@LAST_UPDATED_BY", aPPROVAL_TASK_INITIATION.RESPOSIBLE_EMP_MKEY);

                    var approvalTemplate = await db.QueryFirstOrDefaultAsync<APPROVAL_TASK_INITIATION_NT>("SP_INSERT_TASK_DETAILS_NT", parmeters,
                        commandType: CommandType.StoredProcedure, transaction: transaction);

                    if (approvalTemplate == null)
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
                        var errorResult = new List<APPROVAL_TASK_INITIATION_NT_OUTPUT>
                        {
                            new APPROVAL_TASK_INITIATION_NT_OUTPUT
                            {
                                Status = "Error",
                                Message = "An unexpected error occurred while retrieving the approval template!!!",
                                Data=null
                            }
                        };
                        return errorResult;

                    }

                    var parmetersTaskNo = new DynamicParameters();
                    parmetersTaskNo.Add("@MKEY", aPPROVAL_TASK_INITIATION.HEADER_MKEY);
                    parmetersTaskNo.Add("@APPROVAL_MKEY", aPPROVAL_TASK_INITIATION.MKEY);
                    parmetersTaskNo.Add("@TASK_NO_MKEY", approvalTemplate.MKEY);
                    parmetersTaskNo.Add("@TENTATIVE_START_DATE", aPPROVAL_TASK_INITIATION.TENTATIVE_START_DATE);
                    parmetersTaskNo.Add("@RESPOSIBLE_EMP_MKEY", aPPROVAL_TASK_INITIATION.RESPOSIBLE_EMP_MKEY);
                    parmetersTaskNo.Add("@LAST_UPDATED_BY", aPPROVAL_TASK_INITIATION.CREATED_BY);
                    parmetersTaskNo.Add("@INITIATOR", aPPROVAL_TASK_INITIATION.INITIATOR);
                    parmetersTaskNo.Add("@TAGS", aPPROVAL_TASK_INITIATION.TAGS);

                    var UpadteTaskNo = await db.QueryFirstOrDefaultAsync<APPROVAL_TASK_INITIATION_INPUT_NT>("SP_UPDATE_APPROVAL_TASK_NO_NT", parmetersTaskNo, commandType: CommandType.StoredProcedure, transaction: transaction);

                    foreach (var SubTask in aPPROVAL_TASK_INITIATION.SUBTASK_LIST)
                    {
                        if (SubTask.APPROVAL_MKEY == aPPROVAL_TASK_INITIATION.MKEY)
                        {
                            continue;
                        }
                        if (sqlConnection.State != ConnectionState.Open)
                        {
                            await sqlConnection.OpenAsync();  // Ensure the connection is open
                        }

                        if (sqlConnection.State == ConnectionState.Open && transaction == null)
                        {
                            transaction = sqlConnection.BeginTransaction(); // Start a new transaction
                        }
                    
                        var parmetersParentApproval = new DynamicParameters();
                        parmetersParentApproval.Add("@MKEY", aPPROVAL_TASK_INITIATION.HEADER_MKEY);
                        parmetersParentApproval.Add("@APPROVAL_MKEY", aPPROVAL_TASK_INITIATION.MKEY);

                        // Fetch approval template
                        var SubParentMkey = await db.QueryFirstOrDefaultAsync<APPROVAL_TASK_INITIATION_NT>("SP_GET_APPROVAL_TASK_INITIATION"
                            , parmetersParentApproval, commandType: CommandType.StoredProcedure, transaction: transaction);

                        string TaskPrentNo = string.Empty;

                        if (SubParentMkey.MKEY != null)
                        {
                            var ParentTask_no = await db.QueryFirstOrDefaultAsync<APPROVAL_TASK_INITIATION_TRL_SUBTASK_NT>("select " +
                                " CONVERT(VARCHAR(50),TASK_NO)AS TASK_NO from TASK_HDR WITH (NOLOCK)  WHERE MKEY = @MKEY",
                                 new { MKEY = SubParentMkey.MKEY }, transaction: transaction);
                            TaskPrentNo = ParentTask_no.TASK_NO.ToString();
                        }
                        else
                        {
                            TaskPrentNo = approvalTemplate.TASK_NO.ToString();
                        }
                        var Parent_Mkey = await db.QueryFirstOrDefaultAsync<APPROVAL_TASK_INITIATION_TRL_SUBTASK_NT>("SELECT * " +
                            " FROM V_Task_Parent_ID " +
                            " WHERE SUBTASK_PARENT_ID = @SUBTASK_MKEY ", new { SUBTASK_MKEY = SubTask.APPROVAL_MKEY }, transaction: transaction);

                        var parmetersSubtask = new DynamicParameters();
                        parmetersSubtask.Add("@TASK_NO", SubParentMkey.MKEY);
                        parmetersSubtask.Add("@TASK_NAME", SubTask.APPROVAL_ABBRIVATION);
                        parmetersSubtask.Add("@TASK_DESCRIPTION", SubTask.LONG_DESCRIPTION);
                        parmetersSubtask.Add("@CATEGORY", aPPROVAL_TASK_INITIATION.CAREGORY);
                        parmetersSubtask.Add("@PROJECT_ID", aPPROVAL_TASK_INITIATION.PROPERTY);
                        parmetersSubtask.Add("@SUBPROJECT_ID", aPPROVAL_TASK_INITIATION.BUILDING_MKEY);
                        parmetersSubtask.Add("@COMPLETION_DATE", SubTask.TENTATIVE_END_DATE);
                        parmetersSubtask.Add("@ASSIGNED_TO", SubTask.RESPOSIBLE_EMP_MKEY);
                        parmetersSubtask.Add("@TAGS", SubTask.TAGS);
                        parmetersSubtask.Add("@CLOSE_DATE", SubTask.TENTATIVE_END_DATE);
                        parmetersSubtask.Add("@DUE_DATE", SubTask.TENTATIVE_END_DATE);
                        parmetersSubtask.Add("@TASK_PARENT_NODE_ID", SubParentMkey.MKEY); // approvalTemplate.MKEY);
                        parmetersSubtask.Add("@TASK_PARENT_NUMBER", TaskPrentNo); // ParentTask_no
                        parmetersSubtask.Add("@TASK_TYPE", "359");
                        parmetersSubtask.Add("@STATUS", SubTask.STATUS);
                        parmetersSubtask.Add("@STATUS_PERC", "0.0");
                        parmetersSubtask.Add("@TASK_CREATED_BY", aPPROVAL_TASK_INITIATION.RESPOSIBLE_EMP_MKEY); // header of task
                        parmetersSubtask.Add("@APPROVER_ID", 0);
                        parmetersSubtask.Add("@IS_ARCHIVE", 'N');
                        parmetersSubtask.Add("@ATTRIBUTE1", null);
                        parmetersSubtask.Add("@ATTRIBUTE2", null);
                        parmetersSubtask.Add("@ATTRIBUTE3", null);
                        parmetersSubtask.Add("@ATTRIBUTE4", SubTask.APPROVAL_MKEY);
                        parmetersSubtask.Add("@ATTRIBUTE5", aPPROVAL_TASK_INITIATION.HEADER_MKEY);
                        parmetersSubtask.Add("@CREATED_BY", aPPROVAL_TASK_INITIATION.CREATED_BY);
                        parmetersSubtask.Add("@CREATION_DATE", dateTime);
                        parmetersSubtask.Add("@LAST_UPDATED_BY", aPPROVAL_TASK_INITIATION.CREATED_BY);
                        parmetersSubtask.Add("@APPROVE_ACTION_DATE", null);
                        parmetersSubtask.Add("@ATTRIBUTE5", aPPROVAL_TASK_INITIATION.HEADER_MKEY); // PROJECT ID 
                        parmetersSubtask.Add("@Current_Task_Mkey", SubParentMkey.MKEY);

                        if (SubParentMkey != null)  // to check parent node
                        {
                            parmetersSubtask.Add("@TASK_PARENT_ID", SubParentMkey.MKEY);
                        }
                        else
                        {
                            parmetersSubtask.Add("@TASK_PARENT_ID", approvalTemplate.MKEY);
                        }

                        if (Parent_Mkey != null) // IF THIS PARENT THEN IDNODE Y ELSE N
                        {
                            parmetersSubtask.Add("@ISNODE", "Y");
                        }
                        else
                        {
                            parmetersSubtask.Add("@ISNODE", "N");
                        }
                        var approvalSubTemplate = await db.QueryFirstOrDefaultAsync<TASK_HDR_NT>("SP_INSERT_TASK_NODE_DETAILS_NT", parmetersSubtask, commandType: CommandType.StoredProcedure, transaction: transaction);
                        var parmetersSubTaskNo = new DynamicParameters();
                        parmetersSubTaskNo.Add("@MKEY", SubTask.MKEY);
                        parmetersSubTaskNo.Add("@APPROVAL_MKEY", SubTask.APPROVAL_MKEY);
                        parmetersSubTaskNo.Add("@TASK_NO_MKEY", approvalSubTemplate.MKEY);
                        parmetersSubTaskNo.Add("@COMPLETION_DATE", SubTask.TENTATIVE_END_DATE);
                        parmetersSubTaskNo.Add("@TENTATIVE_START_DATE", SubTask.TENTATIVE_START_DATE);
                        parmetersSubTaskNo.Add("@TENTATIVE_END_DATE", SubTask.TENTATIVE_END_DATE);
                        parmetersSubTaskNo.Add("@DAYS_REQUIRED", SubTask.DAYS_REQUIRED);
                        parmetersSubTaskNo.Add("@LAST_UPDATED_BY", aPPROVAL_TASK_INITIATION.CREATED_BY);
                        parmetersSubTaskNo.Add("@INITIATOR", aPPROVAL_TASK_INITIATION.INITIATOR);
                        parmetersSubTaskNo.Add("@TAGS", SubTask.TAGS);

                        var UpadteSubTaskNo = await db.QueryFirstOrDefaultAsync<APPROVAL_TASK_INITIATION_INPUT_NT>("SP_UPDATE_APPROVAL_TASK_NO_NT", parmetersSubTaskNo, commandType: CommandType.StoredProcedure, transaction: transaction);
                    }

                    // Commit the transaction if everything is successful
                    var sqlTransaction = (SqlTransaction)transaction;
                    await sqlTransaction.CommitAsync();
                    transactionCompleted = true;  // Mark the transaction as completed

                    using (IDbConnection db_Approval = _dapperDbConnection.CreateConnection())
                    {
                        var parmetersApproval = new DynamicParameters();
                        parmetersApproval.Add("@MKEY", aPPROVAL_TASK_INITIATION.HEADER_MKEY);
                        parmetersApproval.Add("@APPROVAL_MKEY", aPPROVAL_TASK_INITIATION.MKEY);

                        // Fetch approval template
                        //var AllApprovalTemplate = await db.QueryAsync<APPROVAL_TASK_INITIATION_INPUT_NT>("SP_GET_APPROVAL_TASK_INITIATION", parmetersApproval, commandType: CommandType.StoredProcedure);
                        var AllApprovalTemplate = await db.QueryAsync<APPROVAL_TASK_INITIATION_NT>("SP_GET_APPROVAL_TASK_INITIATION", parmetersApproval, commandType: CommandType.StoredProcedure);
                        if (AllApprovalTemplate == null)
                        {
                            //var TASK_INITIATION = new APPROVAL_TASK_INITIATION();
                            //TASK_INITIATION.ResponseStatus = "Error";
                            //TASK_INITIATION.Message = "An unexpected error occurred while retrieving the approval template.";
                            //return TASK_INITIATION; // Return null if no results

                            var finalResult = new List<APPROVAL_TASK_INITIATION_NT_OUTPUT>
                            {
                                new APPROVAL_TASK_INITIATION_NT_OUTPUT
                                {
                                    Status = "Error",
                                    Message = "An unexpected error occurred while retrieving the approval template!!!",
                                    Data=null
                                }
                            };
                            return finalResult;

                        }

                        // Fetch subtasks
                        var subtasks = await db.QueryAsync<APPROVAL_TASK_INITIATION_TRL_SUBTASK_NT>("SP_GET_APPROVAL_TASK_INITIATION_TRL_SUBTASK_NT", parmetersApproval, commandType: CommandType.StoredProcedure);
                        approvalTemplate.SUBTASK_LIST = subtasks.ToList(); // Populate the SUBTASK_LIST property with subtasks

                        //var subtasks1 = await db.QueryAsync<APPROVAL_TASK_INITIATION_TRL_SUBTASK>("select * from PROJECT_TRL_APPROVAL_ABBR", commandType: CommandType.Text);
                        //AllApprovalTemplate.STATUS = "Ok";
                        //AllApprovalTemplate.Message = "Data save successfully";
                        //return AllApprovalTemplate;

                        var errorResult = new List<APPROVAL_TASK_INITIATION_NT_OUTPUT>
                        {
                            new APPROVAL_TASK_INITIATION_NT_OUTPUT
                            {
                                Status = "Ok",
                                Message = "Data save successfully!!!",
                                Data=AllApprovalTemplate
                            }
                        };
                        return errorResult;
                    }
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
                        //var TranError = new APPROVAL_TASK_INITIATION();
                        //TranError.ResponseStatus = "Error";
                        //TranError.Message = ex.Message;
                        //return TranError;
                    }
                }

                var errorResult = new List<APPROVAL_TASK_INITIATION_NT_OUTPUT>
                {
                    new APPROVAL_TASK_INITIATION_NT_OUTPUT
                    {
                        Status = "Error",
                        Message = ex.Message,
                        Data= null
                    }
                };
                return errorResult;
            }
        }

        public async Task<APPROVAL_TASK_INITIATION_TRL_SUBTASK> UpdateApprovalSubtask(APPROVAL_TASK_INITIATION_TRL_SUBTASK aPPROVAL_TASK_INITIATION_TRL_SUBTASK)
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
                    parmeters.Add("@MKEY", aPPROVAL_TASK_INITIATION_TRL_SUBTASK.MKEY);
                    parmeters.Add("@APPROVAL_MKEY", aPPROVAL_TASK_INITIATION_TRL_SUBTASK.APPROVAL_MKEY);
                    parmeters.Add("@SHORT_DESCRIPTION", aPPROVAL_TASK_INITIATION_TRL_SUBTASK.SHORT_DESCRIPTION);
                    parmeters.Add("@LONG_DESCRIPTION", aPPROVAL_TASK_INITIATION_TRL_SUBTASK.LONG_DESCRIPTION);
                    parmeters.Add("@RESPOSIBLE_EMP_MKEY", aPPROVAL_TASK_INITIATION_TRL_SUBTASK.RESPOSIBLE_EMP_MKEY);
                    parmeters.Add("@START_DATE", aPPROVAL_TASK_INITIATION_TRL_SUBTASK.TENTATIVE_START_DATE);
                    parmeters.Add("@END_DATE", aPPROVAL_TASK_INITIATION_TRL_SUBTASK.TENTATIVE_END_DATE);
                    parmeters.Add("@TAGS", aPPROVAL_TASK_INITIATION_TRL_SUBTASK.TAGS);
                    parmeters.Add("@LAST_UPDATED_BY", aPPROVAL_TASK_INITIATION_TRL_SUBTASK.LAST_UPDATED_BY);
                    parmeters.Add("@DELETE_FLAG", aPPROVAL_TASK_INITIATION_TRL_SUBTASK.DELETE_FLAG);

                    var approvalTemplateSubtask = await db.QueryFirstOrDefaultAsync<APPROVAL_TASK_INITIATION_TRL_SUBTASK>("sp_update_delete_approval_task_initiation", parmeters, commandType: CommandType.StoredProcedure, transaction: transaction);

                    //var approvalTemplateSubtask1 = await db.QueryFirstOrDefaultAsync<APPROVAL_TASK_INITIATION_TRL_SUBTASK>("SELECT HEADER_MKEY AS MKEY, CASE WHEN ABBR.TASK_NO_MKEY IS " +
                    //    "NULL THEN ABBR.SEQ_NO ELSE(select task_no from task_hdr where mkey = ABBR.TASK_NO_MKEY) END as TASK_NO ,TEMPLATE_HDR.MKEY AS APPROVAL_MKEY" +
                    //    ", APPROVAL_ABBRIVATION		,TEMPLATE_HDR.LONG_DESCRIPTION ,TEMPLATE_HDR.SHORT_DESCRIPTION,ABBR.DAYS_REQUIRED,ABBR.DEPARTMENT,ABBR.JOB_ROLE,ABBR.RESPOSIBLE_EMP_MKEY" +
                    //    ",ABBR.TENTATIVE_START_DATE,ABBR.TENTATIVE_END_DATE,ABBR.STATUS, ABBR.OUTPUT_DOCUMENT " +
                    //    "FROM PROJECT_TRL_APPROVAL_ABBR ABBR INNER JOIN APPROVAL_TEMPLATE_HDR TEMPLATE_HDR ON ABBR.APPROVAL_MKEY = TEMPLATE_HDR.MKEY " +
                    //    " WHERE ABBR.HEADER_MKEY = 31 AND ABBR.APPROVAL_MKEY = 30", commandType: CommandType.Text, transaction: transaction);
                    //var approvalTemplateSubtask2 = await db.QueryFirstOrDefaultAsync<APPROVAL_TASK_INITIATION_TRL_SUBTASK>("select * from  PROJECT_TRL_APPROVAL_ABBR where APPROVAL_MKEY  = 31 ", commandType: CommandType.Text, transaction: transaction);
                    //var approvalTemplateSubtask3 = await db.QueryFirstOrDefaultAsync<APPROVAL_TASK_INITIATION>("select * from  PROJECT_HDR where mkey = 31", commandType: CommandType.Text, transaction: transaction);


                    if (approvalTemplateSubtask == null)
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

                        aPPROVAL_TASK_INITIATION_TRL_SUBTASK.TRLStatus = "Error";
                        aPPROVAL_TASK_INITIATION_TRL_SUBTASK.Message = "Error Occurd";
                        return aPPROVAL_TASK_INITIATION_TRL_SUBTASK;
                    }

                    TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);

                    var sqlTransaction = (SqlTransaction)transaction;
                    await sqlTransaction.CommitAsync();
                    transactionCompleted = true;

                    approvalTemplateSubtask.TRLStatus = "OK";
                    approvalTemplateSubtask.Message = "Row Updated";
                    return approvalTemplateSubtask;
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

                var approvalTemplate = new APPROVAL_TASK_INITIATION_TRL_SUBTASK();
                approvalTemplate.TRLStatus = "Error";
                approvalTemplate.Message = ex.Message;
                return approvalTemplate;
            }
        }

        public async Task<ActionResult<IEnumerable<APPROVAL_TASK_INITIATION_TRL_SUBTASK_NT_OUTPUT>>> UpdateApprovalSubtaskNT(APPROVAL_TASK_INITIATION_TRL_SUBTASK_NT aPPROVAL_TASK_INITIATION_TRL_SUBTASK)
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
                    parmeters.Add("@MKEY", aPPROVAL_TASK_INITIATION_TRL_SUBTASK.MKEY);
                    parmeters.Add("@APPROVAL_MKEY", aPPROVAL_TASK_INITIATION_TRL_SUBTASK.APPROVAL_MKEY);
                    parmeters.Add("@SHORT_DESCRIPTION", aPPROVAL_TASK_INITIATION_TRL_SUBTASK.SHORT_DESCRIPTION);
                    parmeters.Add("@LONG_DESCRIPTION", aPPROVAL_TASK_INITIATION_TRL_SUBTASK.LONG_DESCRIPTION);
                    parmeters.Add("@RESPOSIBLE_EMP_MKEY", aPPROVAL_TASK_INITIATION_TRL_SUBTASK.RESPOSIBLE_EMP_MKEY);
                    parmeters.Add("@START_DATE", aPPROVAL_TASK_INITIATION_TRL_SUBTASK.TENTATIVE_START_DATE);
                    parmeters.Add("@END_DATE", aPPROVAL_TASK_INITIATION_TRL_SUBTASK.TENTATIVE_END_DATE);
                    parmeters.Add("@TAGS", aPPROVAL_TASK_INITIATION_TRL_SUBTASK.TAGS);
                    parmeters.Add("@LAST_UPDATED_BY", aPPROVAL_TASK_INITIATION_TRL_SUBTASK.LAST_UPDATED_BY);
                    parmeters.Add("@DELETE_FLAG", aPPROVAL_TASK_INITIATION_TRL_SUBTASK.DELETE_FLAG);

                    var approvalTemplateSubtask = await db.QueryAsync<APPROVAL_TASK_INITIATION_TRL_SUBTASK_NT>("sp_update_delete_approval_task_initiation_NT", parmeters, commandType: CommandType.StoredProcedure, transaction: transaction);

                    if (approvalTemplateSubtask == null)
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

                        //aPPROVAL_TASK_INITIATION_TRL_SUBTASK.TRLStatus = "Error";
                        //aPPROVAL_TASK_INITIATION_TRL_SUBTASK.Message = "Error Occurd";
                        //return aPPROVAL_TASK_INITIATION_TRL_SUBTASK;

                        var ErrorResult = new List<APPROVAL_TASK_INITIATION_TRL_SUBTASK_NT_OUTPUT>
                        {
                            new APPROVAL_TASK_INITIATION_TRL_SUBTASK_NT_OUTPUT
                            {
                                Status = "Error",
                                Message = "Error Occurd",
                                Data= approvalTemplateSubtask
                            }
                        };
                        return ErrorResult;
                    }

                    TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);

                    var sqlTransaction = (SqlTransaction)transaction;
                    await sqlTransaction.CommitAsync();
                    transactionCompleted = true;

                    //approvalTemplateSubtask.TRLStatus = "OK";
                    //approvalTemplateSubtask.Message = "Row Updated";
                    //return approvalTemplateSubtask;

                    var SuccesResult = new List<APPROVAL_TASK_INITIATION_TRL_SUBTASK_NT_OUTPUT>
                        {
                            new APPROVAL_TASK_INITIATION_TRL_SUBTASK_NT_OUTPUT
                            {
                                Status = "OK",
                                Message = "Row Updated",
                                Data= approvalTemplateSubtask
                            }
                        };
                    return SuccesResult;
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

                //var approvalTemplate = new APPROVAL_TASK_INITIATION_TRL_SUBTASK();
                //approvalTemplate.TRLStatus = "Error";
                //approvalTemplate.Message = ex.Message;
                //return approvalTemplate;


                var ErrorResult = new List<APPROVAL_TASK_INITIATION_TRL_SUBTASK_NT_OUTPUT>
                        {
                            new APPROVAL_TASK_INITIATION_TRL_SUBTASK_NT_OUTPUT
                            {
                                Status = "Error",
                                Message = ex.Message,
                                Data= null
                            }
                        };
                return ErrorResult;
            }
        }

        #region
        // Working On GetApprovalTaskInitiation  With Checklist and Endlist 

        public async Task<Approval_Task_Initiation_OutPutResponse> GetApprovalTemplateByIdAsync_PS(int MKEY, int APPROVAL_MKEY)
        {
            try
            {
                using (IDbConnection db = _dapperDbConnection.CreateConnection())
                {
                    var parmeters = new DynamicParameters();
                    //parmeters.Add("@MKEY", MKEY);
                    parmeters.Add("@MKEY", MKEY);
                    parmeters.Add("@APPROVAL_MKEY", APPROVAL_MKEY);

                    // Fetch approval template
                    var approvalTemplate = await db.QueryFirstOrDefaultAsync<Approval_Task_Initiation_OutPutResponse>("SP_GET_APPROVAL_TASK_INITIATION", parmeters, commandType: CommandType.StoredProcedure);

                    if (approvalTemplate == null && approvalTemplate.HEADER_MKEY == 0 && approvalTemplate.MKEY == 0)
                    {
                        var aPPROVAL_TASK_INITIATION = new Approval_Task_Initiation_OutPutResponse();
                        aPPROVAL_TASK_INITIATION.ResponseStatus = "Error";
                        aPPROVAL_TASK_INITIATION.Message = "An unexpected error occurred while retrieving the approval template.";
                        return aPPROVAL_TASK_INITIATION; // Return null if no results
                    }

                    // Query To Get The Checklist and Endlist And Sanctioning 

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
                    approvalTemplate.APPROVAL_CHECK_LIST =
                                (await db.QueryAsync<APPROVAL_TEMPLATE_TRL_CHECKLIST_Model>(
                                    @"SELECT * FROM APPROVAL_TEMPLATE_TRL_CHECKLIST
                                          WHERE MKEY = @MKEY
                                            AND DELETE_FLAG = 'N'",
                                    new { MKEY = approvalTemplate.MKEY }
                                )).ToList();


                    /* =======================
                       SANCTIONING DEPARTMENT
                       ======================= */
                    approvalTemplate.SANCTIONING_DEPARTMENT_LIST =
                        (await db.QueryAsync<OUTPUT_APPROVAL_TEMPLATE_TRL_SANCTIONING_DEPARTMENT>(
                            @"SELECT *
                          FROM V_APPROVAL_TEMPLATE_TRL_SANCTIONING_DEPARTMENT
                          WHERE MKEY = @MKEY
                            AND DELETE_FLAG = 'N'",
                            new { MKEY = approvalTemplate.MKEY }
                        )).ToList();


                    // Fetch subtasks
                    var subtasks = await db.QueryAsync<APPROVAL_TASK_INITIATION_TRL_SUBTASK>("SP_GET_APPROVAL_TASK_INITIATION_TRL_SUBTASK", parmeters, commandType: CommandType.StoredProcedure);
                    approvalTemplate.SUBTASK_LIST = subtasks.ToList(); // Populate the SUBTASK_LIST property with subtasks
                    foreach(var item in approvalTemplate.SUBTASK_LIST)
                    {
                        var subTaksMap = await MapSubTaskTemplateApprovalList_Mkey(item);
                        var sub_SubEnd_Result_Doc_Lst= (await db.QueryAsync<APPROVAL_TEMPLATE_TRL_ENDRESULT_PS_Model>(
                                               @"SELECT * FROM APPROVAL_TEMPLATE_TRL_ENDRESULT
                                                  WHERE MKEY = @MKEY
                                                    AND DELETE_FLAG = 'N'",
                                               new { MKEY = item.APPROVAL_MKEY }
                                           )).ToList();
                        item.End_Result_Doc_Lst = sub_SubEnd_Result_Doc_Lst.ToList();

                        var sub_subTaskChecklist =
                                (await db.QueryAsync<APPROVAL_TEMPLATE_TRL_CHECKLIST_Model>(
                                    @"SELECT * FROM APPROVAL_TEMPLATE_TRL_CHECKLIST
                                          WHERE MKEY = @MKEY
                                            AND DELETE_FLAG = 'N'",
                                    new { MKEY = item.APPROVAL_MKEY }
                                )).ToList();
                        item.APPROVAL_CHECK_LIST = sub_subTaskChecklist.ToList();

                        var Sub_Subtask_SANCTIONING_DEPARTMENT_LIST =
                        (await db.QueryAsync<OUTPUT_APPROVAL_TEMPLATE_TRL_SANCTIONING_DEPARTMENT>(
                            @"SELECT *
                          FROM V_APPROVAL_TEMPLATE_TRL_SANCTIONING_DEPARTMENT
                          WHERE MKEY = @MKEY
                            AND DELETE_FLAG = 'N'",
                            new { MKEY = item.APPROVAL_MKEY }
                        )).ToList();

                        item.SANCTIONING_DEPARTMENT_LIST = Sub_Subtask_SANCTIONING_DEPARTMENT_LIST.ToList();
                    }
                    
                    //var subtasks1 = await db.QueryAsync<APPROVAL_TASK_INITIATION_TRL_SUBTASK>("select * from PROJECT_TRL_APPROVAL_ABBR", commandType: CommandType.Text);
                    approvalTemplate.STATUS = "Ok";
                    approvalTemplate.Message = "Get Data Sucessuly";
                    return approvalTemplate;
                }
            }
            catch (Exception ex)
            {
                var aPPROVAL_TASK_INITIATION = new Approval_Task_Initiation_OutPutResponse();
                aPPROVAL_TASK_INITIATION.ResponseStatus = "Error";
                aPPROVAL_TASK_INITIATION.Message = ex.Message;
                return aPPROVAL_TASK_INITIATION; // Return null if no results
            }
        }

        public  Task<Approval_Task_Initiation_OutPutResponse>MapSubTaskTemplateApprovalList_Mkey(APPROVAL_TASK_INITIATION_TRL_SUBTASK subtask)
        {
            var subtask_NT_PS = new Approval_Task_Initiation_OutPutResponse
            {
                MKEY = subtask.MKEY,
                HEADER_MKEY = subtask.HEADER_MKEY,
                TASK_NO = subtask.TASK_NO,
                //A = subtask.APPROVAL_MKEY,
                SHORT_DESCRIPTION = subtask.SHORT_DESCRIPTION,
                LONG_DESCRIPTION = subtask.LONG_DESCRIPTION,
                TAGS = subtask.TAGS,
                 //Approval_Abbrivation= subtask.APPROVAL_ABBRIVATION,
                DAYS_REQUIERD = subtask.DAYS_REQUIRED.ToString(),
                AUTHORITY_DEPARTMENT = subtask.DEPARTMENT,
                CREATED_BY = subtask.RESPOSIBLE_EMP_MKEY,
                TENTATIVE_START_DATE = subtask.TENTATIVE_START_DATE,
                TENTATIVE_END_DATE = subtask.TENTATIVE_END_DATE,
                COMPLITION_DATE = subtask.COMPLITION_DATE,
                STATUS = subtask.STATUS,
                Delete_flag = subtask.DELETE_FLAG,
                //A
                // = null,
                //LAST_UPDATE_DATE = null


            };
            return Task.FromResult(subtask_NT_PS);
        }

        public async Task<Add_ApprovalTemplateInitiation_OutPut_PS> UpdateApprovalSubtask_PS(APPROVAL_TASK_INITIATION_TRL_SUBTASK_PS aPPROVAL_TASK_INITIATION_TRL_SUBTASK)
        {
            DateTime dateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
            IDbTransaction transaction = null;
            bool transactionCompleted = false;  // Track the transaction state
            var errorResult = new Add_ApprovalTemplateInitiation_OutPut_PS();
            var ResResult = new Add_ApprovalTemplateInitiation_OutPut_PS();
            string msgStatus = string.Empty;
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
                    parmeters.Add("@MKEY", aPPROVAL_TASK_INITIATION_TRL_SUBTASK.MKEY);
                    parmeters.Add("@APPROVAL_MKEY", aPPROVAL_TASK_INITIATION_TRL_SUBTASK.APPROVAL_MKEY);
                    parmeters.Add("@SHORT_DESCRIPTION", aPPROVAL_TASK_INITIATION_TRL_SUBTASK.SHORT_DESCRIPTION);
                    parmeters.Add("@LONG_DESCRIPTION", aPPROVAL_TASK_INITIATION_TRL_SUBTASK.LONG_DESCRIPTION);
                    parmeters.Add("@RESPOSIBLE_EMP_MKEY", aPPROVAL_TASK_INITIATION_TRL_SUBTASK.RESPOSIBLE_EMP_MKEY);
                    parmeters.Add("@START_DATE", aPPROVAL_TASK_INITIATION_TRL_SUBTASK.TENTATIVE_START_DATE);
                    parmeters.Add("@END_DATE", aPPROVAL_TASK_INITIATION_TRL_SUBTASK.TENTATIVE_END_DATE);
                    parmeters.Add("@TAGS", aPPROVAL_TASK_INITIATION_TRL_SUBTASK.TAGS);
                    parmeters.Add("@LAST_UPDATED_BY", aPPROVAL_TASK_INITIATION_TRL_SUBTASK.LAST_UPDATED_BY);
                    parmeters.Add("@DELETE_FLAG", aPPROVAL_TASK_INITIATION_TRL_SUBTASK.DELETE_FLAG);

                    var approvalTemplateSubtask = await db.QueryFirstOrDefaultAsync<APPROVAL_TASK_INITIATION_TRL_SUBTASK_PS>("sp_update_delete_approval_task_initiation", parmeters, commandType: CommandType.StoredProcedure, transaction: transaction);

                    //var approvalTemplateSubtask1 = await db.QueryFirstOrDefaultAsync<APPROVAL_TASK_INITIATION_TRL_SUBTASK>("SELECT HEADER_MKEY AS MKEY, CASE WHEN ABBR.TASK_NO_MKEY IS " +
                    //    "NULL THEN ABBR.SEQ_NO ELSE(select task_no from task_hdr where mkey = ABBR.TASK_NO_MKEY) END as TASK_NO ,TEMPLATE_HDR.MKEY AS APPROVAL_MKEY" +
                    //    ", APPROVAL_ABBRIVATION		,TEMPLATE_HDR.LONG_DESCRIPTION ,TEMPLATE_HDR.SHORT_DESCRIPTION,ABBR.DAYS_REQUIRED,ABBR.DEPARTMENT,ABBR.JOB_ROLE,ABBR.RESPOSIBLE_EMP_MKEY" +
                    //    ",ABBR.TENTATIVE_START_DATE,ABBR.TENTATIVE_END_DATE,ABBR.STATUS, ABBR.OUTPUT_DOCUMENT " +
                    //    "FROM PROJECT_TRL_APPROVAL_ABBR ABBR INNER JOIN APPROVAL_TEMPLATE_HDR TEMPLATE_HDR ON ABBR.APPROVAL_MKEY = TEMPLATE_HDR.MKEY " +
                    //    " WHERE ABBR.HEADER_MKEY = 31 AND ABBR.APPROVAL_MKEY = 30", commandType: CommandType.Text, transaction: transaction);
                    //var approvalTemplateSubtask2 = await db.QueryFirstOrDefaultAsync<APPROVAL_TASK_INITIATION_TRL_SUBTASK>("select * from  PROJECT_TRL_APPROVAL_ABBR where APPROVAL_MKEY  = 31 ", commandType: CommandType.Text, transaction: transaction);
                    //var approvalTemplateSubtask3 = await db.QueryFirstOrDefaultAsync<APPROVAL_TASK_INITIATION>("select * from  PROJECT_HDR where mkey = 31", commandType: CommandType.Text, transaction: transaction);


                    if (approvalTemplateSubtask == null)
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
                                errorResult.Status = "Error";
                                errorResult.Message = rollbackEx.Message;
                                errorResult.Data = null;
                                Console.WriteLine($"Rollback failed: {rollbackEx.Message}");
                                //TranError.Message = ex.Message;
                                //return TranError;
                            }
                        }
                        else
                        {
                            errorResult = new Add_ApprovalTemplateInitiation_OutPut_PS
                            {

                                Status = "Error",
                                Message = " sp_update_delete_approval_task_initiation Getting Null Object",
                                Data = null

                            };
                        }
                        return errorResult;
                    }

                    if (aPPROVAL_TASK_INITIATION_TRL_SUBTASK.MKEY > 0)
                    {
                        if (aPPROVAL_TASK_INITIATION_TRL_SUBTASK.APPROVAL_CHECK_LIST.Any())
                        {
                            foreach (var TCheckList in aPPROVAL_TASK_INITIATION_TRL_SUBTASK.APPROVAL_CHECK_LIST)
                            {
                                var parmetersCheckList = new DynamicParameters();
                                parmetersCheckList.Add("@TASK_MKEY", aPPROVAL_TASK_INITIATION_TRL_SUBTASK.APPROVAL_MKEY, DbType.Int32);
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
                                            errorResult = new Add_ApprovalTemplateInitiation_OutPut_PS
                                            {

                                                Status = "Error",
                                                Message = message,
                                                Data = null

                                            };
                                            return errorResult;
                                        }
                                        catch (InvalidOperationException rollbackEx)
                                        {
                                            errorResult.Status = "Error";
                                            errorResult.Message = rollbackEx.Message;
                                            errorResult.Data = null;
                                            Console.WriteLine($"Rollback failed: {rollbackEx.Message}");
                                        }
                                    }

                                    errorResult = new Add_ApprovalTemplateInitiation_OutPut_PS
                                    {

                                        Status = "Error",
                                        Message = message,
                                        Data = null

                                    };
                                    return errorResult;
                                }
                                else
                                {
                                    msgStatus = "Success";
                                }
                            }
                        }

                        if (aPPROVAL_TASK_INITIATION_TRL_SUBTASK.End_Result_Doc_Lst.Any())
                        {
                            // To Insert EndList
                            foreach (var TEndList in aPPROVAL_TASK_INITIATION_TRL_SUBTASK.End_Result_Doc_Lst)
                            {
                                foreach (var docMkey in TEndList.OUTPUT_DOC_LST)
                                {
                                    foreach (var DocCategory in docMkey.Value.ToString().Split(','))
                                    {
                                        var parametersEndList = new DynamicParameters();
                                        parametersEndList.Add("@MKEY", aPPROVAL_TASK_INITIATION_TRL_SUBTASK.APPROVAL_MKEY, DbType.Int32);
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
                                                    errorResult = new Add_ApprovalTemplateInitiation_OutPut_PS
                                                    {

                                                        Status = "Error",
                                                        Message = message,
                                                        Data = null

                                                    };
                                                    return errorResult;
                                                }
                                                catch (InvalidOperationException rollbackEx)
                                                {


                                                    errorResult.Status = "Error";
                                                    errorResult.Message = rollbackEx.Message;
                                                    errorResult.Data = null;
                                                    Console.WriteLine($"Rollback failed: {rollbackEx.Message}");
                                                }
                                            }

                                            errorResult = new Add_ApprovalTemplateInitiation_OutPut_PS
                                            {

                                                Status = "Error",
                                                Message = message,
                                                Data = null

                                            };
                                            return errorResult;
                                        }
                                        else
                                        {
                                            msgStatus = "Success";
                                        }
                                    }
                                }
                            }
                        }

                        TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);

                        var sqlTransaction = (SqlTransaction)transaction;
                        await sqlTransaction.CommitAsync();
                        transactionCompleted = true;
                        if (msgStatus.Contains("Success"))
                        {

                            List<TASK_ENDLIST_TABLE_INPUT_PS> task_ENDList = new List<TASK_ENDLIST_TABLE_INPUT_PS>();
                            //foreach (var checkitem in aPPROVAL_TASK_INITIATION_TRL_SUBTASK.APPROVAL_CHECK_LIST)
                            //{
                            //    var checklist = await MapTask_ApprovalTemplate_Checklist(checkitem);
                            //    task_checkList.Add(checklist);
                            //}

                            var sub_subTaskChecklist =
                                (await db.QueryAsync<TASK_CHECKLIST_TABLE_INPUT_PS>(
                                    @"SELECT MKEY As TASK_MKEY,SR_NO,DOCUMENT_MKEY as DOC_MKEY,DOCUMENT_CATEGORY,DOCUMENT_CATEGORY As Doc_Cat_Mkey ,DOCUMENT_NAME,DELETE_FLAG ,CREATED_BY FROM APPROVAL_TEMPLATE_TRL_CHECKLIST
                                          WHERE MKEY = @MKEY
                                            AND DELETE_FLAG = 'N'",
                                    new { MKEY = aPPROVAL_TASK_INITIATION_TRL_SUBTASK.APPROVAL_MKEY }
                                )).ToList();


                            aPPROVAL_TASK_INITIATION_TRL_SUBTASK.APPROVAL_CHECK_LIST = sub_subTaskChecklist.ToList();


                            foreach(var item in aPPROVAL_TASK_INITIATION_TRL_SUBTASK.End_Result_Doc_Lst)
                            {
                                var sub_SubEnd_Result_Doc_Lst = await MapTask_ApprovalTemplate_ENDlist(item);
                                task_ENDList.Add(sub_SubEnd_Result_Doc_Lst);
                            }
                           


                            aPPROVAL_TASK_INITIATION_TRL_SUBTASK.End_Result_Doc_Lst = task_ENDList.ToList();
                        }
                        //approvalTemplateSubtask.TRLStatus = "OK";
                        //approvalTemplateSubtask.Message = "Row Updated";
                        //return approvalTemplateSubtask;

                        ResResult = new Add_ApprovalTemplateInitiation_OutPut_PS
                        {
                            Status = "OK",
                            Message = "Row Updated",
                            Data = aPPROVAL_TASK_INITIATION_TRL_SUBTASK
                        };
                    }
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
                        errorResult.Status = "Error";
                        errorResult.Message = rollbackEx.Message;
                        errorResult.Data = null;
                        // Handle rollback exception (may occur if transaction is already completed)
                        // Log or handle the rollback failure if needed
                        Console.WriteLine($"Rollback failed: {rollbackEx.Message}");
                      // throw new Exception($"Rollback failed: {rollbackEx.Message}");
                      return errorResult;
                    }
                }
            }
            return ResResult;
        }

        public async Task<TASK_ENDLIST_TABLE_INPUT_PS> MapTask_ApprovalTemplate_ENDlist(
    TASK_ENDLIST_TABLE_INPUT_PS endTasklist)
        {
            try
            {
                using (IDbConnection db = _dapperDbConnection.CreateConnection())
                {
                    if (endTasklist.OUTPUT_DOC_LST == null ||
                        !endTasklist.OUTPUT_DOC_LST.Any())
                    {
                        return endTasklist;
                    }

                    var value = endTasklist.OUTPUT_DOC_LST.Values.FirstOrDefault();
                    int Doc_Cat_KeyInt = 0;

                    if (value is JsonElement element)
                    {
                        Doc_Cat_KeyInt = element.ValueKind switch
                        {
                            JsonValueKind.Number => element.GetInt32(),
                            JsonValueKind.String => int.Parse(element.GetString()!),
                            _ => 0
                        };
                    }
                    else if (value != null)
                    {
                        Doc_Cat_KeyInt = Convert.ToInt32(value);
                    }

                    var srNo = await db.QueryFirstOrDefaultAsync<int>(
                        @"SELECT SR_NO 
                  FROM APPROVAL_TEMPLATE_TRL_ENDRESULT
                  WHERE MKEY = @MKEY
                    AND DOCUMENT_CATEGORY = @DOC_CAT_MKEY
                    AND DELETE_FLAG = 'N'",
                        new
                        {
                            MKEY = endTasklist.MKEY,
                            DOC_CAT_MKEY = Doc_Cat_KeyInt
                        }
                    );

                    endTasklist.SR_NO = srNo;

                    return endTasklist;
                }
            }
            catch
            {
                throw;
            }
        }




        //    public async Task<APPROVAL_TASK_INITIATION_PS> CreateTaskApprovalTemplateAsync_PS(
        //APPROVAL_TASK_INITIATION_PS model)
        //    {
        //        DateTime dateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);

        //        using (IDbConnection db = _dapperDbConnection.CreateConnection())
        //        {
        //            var sqlConnection = db as SqlConnection;
        //            if (sqlConnection == null)
        //                throw new InvalidOperationException("Connection must be SqlConnection.");

        //            if (sqlConnection.State != ConnectionState.Open)
        //                await sqlConnection.OpenAsync();

        //            using (var transaction = sqlConnection.BeginTransaction())
        //            {
        //                try
        //                {
        //                    #region INSERT MAIN TASK

        //                    var parameters = new DynamicParameters();
        //                    parameters.Add("@TASK_NO", model.TASK_NO);
        //                    parameters.Add("@TASK_NAME", model.MAIN_ABBR);
        //                    parameters.Add("@TASK_DESCRIPTION", model.LONG_DESCRIPTION);
        //                    parameters.Add("@CATEGORY", model.CAREGORY);
        //                    parameters.Add("@PROJECT_ID", model.PROPERTY);
        //                    parameters.Add("@SUBPROJECT_ID", model.BUILDING_MKEY);
        //                    parameters.Add("@COMPLETION_DATE", model.COMPLITION_DATE);
        //                    parameters.Add("@ASSIGNED_TO", model.RESPOSIBLE_EMP_MKEY);
        //                    parameters.Add("@TAGS", model.TAGS);
        //                    parameters.Add("@ISNODE", "Y");
        //                    parameters.Add("@CLOSE_DATE", model.TENTATIVE_END_DATE);
        //                    parameters.Add("@DUE_DATE", model.TENTATIVE_END_DATE);
        //                    parameters.Add("@TASK_PARENT_ID", 0);
        //                    parameters.Add("@STATUS", model.STATUS);
        //                    parameters.Add("@TASK_TYPE", "359");
        //                    parameters.Add("@STATUS_PERC", 0.0);
        //                    parameters.Add("@TASK_CREATED_BY", model.INITIATOR);
        //                    parameters.Add("@APPROVER_ID", 0);
        //                    parameters.Add("@ATTRIBUTE4", model.MKEY);
        //                    parameters.Add("@ATTRIBUTE5", model.HEADER_MKEY);
        //                    parameters.Add("@CREATED_BY", model.CREATED_BY);
        //                    parameters.Add("@CREATION_DATE", dateTime);
        //                    parameters.Add("@LAST_UPDATED_BY", model.CREATED_BY);

        //                    var approvalTemplate = await db.QueryFirstOrDefaultAsync<APPROVAL_TASK_INITIATION_PS>(
        //                        "SP_INSERT_TASK_DETAILS",
        //                        parameters,
        //                        commandType: CommandType.StoredProcedure,
        //                        transaction: transaction);

        //                    if (approvalTemplate == null)
        //                        throw new Exception("Main task insert failed.");

        //                    #endregion

        //                    #region UPDATE TASK NUMBER

        //                    var updateParams = new DynamicParameters();
        //                    updateParams.Add("@MKEY", model.HEADER_MKEY);
        //                    updateParams.Add("@APPROVAL_MKEY", model.MKEY);
        //                    updateParams.Add("@TASK_NO_MKEY", approvalTemplate.MKEY);

        //                    await db.ExecuteAsync(
        //                        "SP_UPDATE_APPROVAL_TASK_NO",
        //                        updateParams,
        //                        commandType: CommandType.StoredProcedure,
        //                        transaction: transaction);

        //                    #endregion

        //                    #region INSERT SUBTASKS

        //                    if (model.SUBTASK_LIST != null && model.SUBTASK_LIST.Any())
        //                    {
        //                        foreach (var subTask in model.SUBTASK_LIST)
        //                        {
        //                            if (subTask.APPROVAL_MKEY == model.MKEY)
        //                                continue;

        //                            // Get Parent MKEY
        //                            var subParent = await db.QueryFirstOrDefaultAsync<int?>(
        //                                @"SELECT MKEY FROM TASK_HDR 
        //                          WHERE ATTRIBUTE4 = CAST(
        //                              (SELECT SUBTASK_PARENT_ID 
        //                               FROM APPROVAL_TEMPLATE_TRL_SUBTASK 
        //                               WHERE SUBTASK_MKEY = @APPROVAL_MKEY 
        //                               AND DELETE_FLAG = 'N') AS NVARCHAR(10))
        //                          AND DELETE_FLAG = 'N'
        //                          AND ATTRIBUTE5 = CAST(
        //                              (SELECT MKEY FROM PROJECT_HDR 
        //                               WHERE MKEY = @MKEY 
        //                               AND DELETE_FLAG = 'N') AS NVARCHAR(10))",
        //                                new { APPROVAL_MKEY = subTask.APPROVAL_MKEY, MKEY = subTask.MKEY },
        //                                transaction);

        //                            string taskParentNo;

        //                            if (subParent.HasValue && subParent.Value > 0)
        //                            {
        //                                taskParentNo = await db.QueryFirstOrDefaultAsync<string>(
        //                                    "SELECT CONVERT(VARCHAR(50),TASK_NO) FROM TASK_HDR WHERE MKEY = @MKEY",
        //                                    new { MKEY = subParent.Value },
        //                                    transaction);
        //                            }
        //                            else
        //                            {
        //                                taskParentNo = approvalTemplate.TASK_NO;
        //                            }

        //                            var subParams = new DynamicParameters();
        //                            subParams.Add("@TASK_NO", taskParentNo);
        //                            subParams.Add("@TASK_NAME", subTask.APPROVAL_ABBRIVATION);
        //                            subParams.Add("@TASK_DESCRIPTION", subTask.LONG_DESCRIPTION);
        //                            subParams.Add("@CATEGORY", model.CAREGORY);
        //                            subParams.Add("@PROJECT_ID", model.PROPERTY);
        //                            subParams.Add("@SUBPROJECT_ID", model.BUILDING_MKEY);
        //                            subParams.Add("@COMPLETION_DATE", subTask.TENTATIVE_END_DATE);
        //                            subParams.Add("@ASSIGNED_TO", subTask.RESPOSIBLE_EMP_MKEY);
        //                            subParams.Add("@TAGS", subTask.TAGS);
        //                            subParams.Add("@TASK_PARENT_ID",
        //                                subParent.HasValue && subParent.Value > 0
        //                                    ? subParent.Value
        //                                    : approvalTemplate.MKEY);
        //                            subParams.Add("@TASK_PARENT_NUMBER", taskParentNo);
        //                            subParams.Add("@TASK_TYPE", "359");
        //                            subParams.Add("@STATUS", subTask.STATUS);
        //                            subParams.Add("@STATUS_PERC", 0.0);
        //                            subParams.Add("@TASK_CREATED_BY", model.CREATED_BY);
        //                            subParams.Add("@ATTRIBUTE4", subTask.APPROVAL_MKEY);
        //                            subParams.Add("@ATTRIBUTE5", model.HEADER_MKEY);
        //                            subParams.Add("@CREATED_BY", model.CREATED_BY);
        //                            subParams.Add("@CREATION_DATE", dateTime);
        //                            subParams.Add("@LAST_UPDATED_BY", model.CREATED_BY);

        //                            var insertedSubTask = await db.QueryFirstOrDefaultAsync<TASK_HDR>(
        //                                "SP_INSERT_TASK_NODE_DETAILS",
        //                                subParams,
        //                                commandType: CommandType.StoredProcedure,
        //                                transaction: transaction);

        //                            if (insertedSubTask == null)
        //                                throw new Exception("Subtask insert failed.");
        //                        }
        //                    }

        //                    #endregion

        //                    // Commit Transaction
        //                    await transaction.CommitAsync();

        //                    #region FETCH FINAL DATA (NO TRANSACTION)

        //                    var finalParams = new DynamicParameters();
        //                    finalParams.Add("@MKEY", model.HEADER_MKEY);
        //                    finalParams.Add("@APPROVAL_MKEY", model.MKEY);

        //                    var finalData = await db.QueryFirstOrDefaultAsync<APPROVAL_TASK_INITIATION_PS>(
        //                        "SP_GET_APPROVAL_TASK_INITIATION",
        //                        finalParams,
        //                        commandType: CommandType.StoredProcedure);

        //                    var subtasks = await db.QueryAsync<APPROVAL_TASK_INITIATION_TRL_SUBTASK>(
        //                        "SP_GET_APPROVAL_TASK_INITIATION_TRL_SUBTASK",
        //                        finalParams,
        //                        commandType: CommandType.StoredProcedure);

        //                    finalData.SUBTASK_LIST = subtasks.ToList();
        //                    finalData.ResponseStatus = "Ok";
        //                    finalData.Message = "Data saved successfully";

        //                    return finalData;

        //                    #endregion
        //                }
        //                catch (Exception ex)
        //                {
        //                    await transaction.RollbackAsync();

        //                    return new APPROVAL_TASK_INITIATION_PS
        //                    {
        //                        ResponseStatus = "Error",
        //                        Message = ex.Message
        //                    };
        //                }
        //            }
        //        }
        //    }

        #region
        public async Task<APPROVAL_TASK_INITIATION_PS> CreateTaskApprovalTemplateAsync_PS_Failed(APPROVAL_TASK_INITIATION_PS aPPROVAL_TASK_INITIATION)
        {
            DateTime dateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
            SqlTransaction transaction = null;
            bool transactionCommitted = false;
            int savedHeaderMKEY = 0;
            string savedTaskNo = string.Empty;

            try
            {
                using (SqlConnection sqlConnection = _dapperDbConnection.CreateConnection() as SqlConnection)
                {
                    if (sqlConnection == null)
                    {
                        throw new InvalidOperationException("The connection must be a SqlConnection to use OpenAsync.");
                    }

                    if (sqlConnection.State != ConnectionState.Open)
                    {
                        await sqlConnection.OpenAsync();
                    }

                    transaction = sqlConnection.BeginTransaction();
                    Console.WriteLine("Transaction started");

                    // Step 1: Insert Header
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@TASK_NO", aPPROVAL_TASK_INITIATION.TASK_NO);
                    parmeters.Add("@TASK_NAME", aPPROVAL_TASK_INITIATION.MAIN_ABBR);
                    parmeters.Add("@TASK_DESCRIPTION", aPPROVAL_TASK_INITIATION.LONG_DESCRIPTION);
                    parmeters.Add("@CATEGORY", aPPROVAL_TASK_INITIATION.CAREGORY);
                    parmeters.Add("@PROJECT_ID", aPPROVAL_TASK_INITIATION.PROPERTY);
                    parmeters.Add("@SUBPROJECT_ID", aPPROVAL_TASK_INITIATION.BUILDING_MKEY);
                    parmeters.Add("@COMPLETION_DATE", aPPROVAL_TASK_INITIATION.COMPLITION_DATE);
                    parmeters.Add("@ASSIGNED_TO", aPPROVAL_TASK_INITIATION.RESPOSIBLE_EMP_MKEY.ToString());
                    parmeters.Add("@TAGS", aPPROVAL_TASK_INITIATION.TAGS);
                    parmeters.Add("@ISNODE", "Y");
                    parmeters.Add("@CLOSE_DATE", aPPROVAL_TASK_INITIATION.TENTATIVE_END_DATE);
                    parmeters.Add("@DUE_DATE", aPPROVAL_TASK_INITIATION.TENTATIVE_END_DATE);
                    parmeters.Add("@TASK_PARENT_ID", 0);
                    parmeters.Add("@STATUS", aPPROVAL_TASK_INITIATION.STATUS);
                    parmeters.Add("@TASK_TYPE", "359");
                    parmeters.Add("@STATUS_PERC", 0.0);
                    parmeters.Add("@TASK_CREATED_BY", aPPROVAL_TASK_INITIATION.INITIATOR);
                    parmeters.Add("@APPROVER_ID", 0);
                    parmeters.Add("@IS_ARCHIVE", null);
                    parmeters.Add("@ATTRIBUTE1", null);
                    parmeters.Add("@ATTRIBUTE2", null);
                    parmeters.Add("@ATTRIBUTE3", null);
                    parmeters.Add("@ATTRIBUTE4", aPPROVAL_TASK_INITIATION.MKEY.ToString());
                    parmeters.Add("@ATTRIBUTE5", aPPROVAL_TASK_INITIATION.HEADER_MKEY.ToString());
                    parmeters.Add("@CREATED_BY", aPPROVAL_TASK_INITIATION.CREATED_BY);
                    parmeters.Add("@CREATION_DATE", dateTime);
                    parmeters.Add("@LAST_UPDATED_BY", aPPROVAL_TASK_INITIATION.CREATED_BY);

                    var approvalTemplate = await sqlConnection.QueryFirstOrDefaultAsync<APPROVAL_TASK_INITIATION_PS>(
                        "SP_INSERT_TASK_DETAILS",
                        parmeters,
                        commandType: CommandType.StoredProcedure,
                        transaction: transaction);

                    Console.WriteLine($"Header insert result - MKEY: {approvalTemplate?.MKEY}, Task_NO: {approvalTemplate?.TASK_NO}, Status: {approvalTemplate?.STATUS}, Message: {approvalTemplate?.Message}");

                    if (approvalTemplate == null || approvalTemplate.MKEY == 0)
                    {
                        if (transaction?.Connection != null)
                        {
                            transaction.Rollback();
                        }
                        return new APPROVAL_TASK_INITIATION_PS
                        {
                            ResponseStatus = "Error",
                            Message = "Failed to insert header data - no MKEY returned"
                        };
                    }

                    // Store the header MKEY and Task No for later use
                    savedHeaderMKEY = approvalTemplate.MKEY;
                    savedTaskNo = approvalTemplate.TASK_NO;

                    // Step 2: Update Task Number - FIX: Convert TASK_NO_MKEY to string
                    var parmetersTaskNo = new DynamicParameters();
                    parmetersTaskNo.Add("@MKEY", aPPROVAL_TASK_INITIATION.HEADER_MKEY);
                    parmetersTaskNo.Add("@APPROVAL_MKEY", aPPROVAL_TASK_INITIATION.MKEY);
                    parmetersTaskNo.Add("@TASK_NO_MKEY", savedHeaderMKEY.ToString()); // Convert to string to match NVARCHAR
                    parmetersTaskNo.Add("@TENTATIVE_START_DATE", aPPROVAL_TASK_INITIATION.TENTATIVE_START_DATE);
                    parmetersTaskNo.Add("@RESPOSIBLE_EMP_MKEY", aPPROVAL_TASK_INITIATION.RESPOSIBLE_EMP_MKEY);
                    parmetersTaskNo.Add("@LAST_UPDATED_BY", aPPROVAL_TASK_INITIATION.CREATED_BY);
                    parmetersTaskNo.Add("@INITIATOR", aPPROVAL_TASK_INITIATION.INITIATOR);
                    parmetersTaskNo.Add("@TAGS", aPPROVAL_TASK_INITIATION.TAGS);

                    try
                    {
                        var updateResult = await sqlConnection.QueryFirstOrDefaultAsync<dynamic>(
                            "SP_UPDATE_APPROVAL_TASK_NO_PS",
                            parmetersTaskNo,
                            commandType: CommandType.StoredProcedure,
                            transaction: transaction);

                        Console.WriteLine("SP_UPDATE_APPROVAL_TASK_NO executed");

                        // Check if the result contains error information
                        if (updateResult != null)
                        {
                            var errorNumber = updateResult.GetType().GetProperty("ERRORNUMBER")?.GetValue(updateResult);
                            if (errorNumber != null && Convert.ToInt32(errorNumber) > 0)
                            {
                                var errorMessage = updateResult.GetType().GetProperty("ERRORMESSAGE")?.GetValue(updateResult)?.ToString();
                                Console.WriteLine($"SQL Error: {errorMessage}");

                                // The transaction might be rolled back, check header
                                using (SqlConnection checkConnection = _dapperDbConnection.CreateConnection() as SqlConnection)
                                {
                                    await checkConnection.OpenAsync();
                                    var checkResult = await checkConnection.QueryFirstOrDefaultAsync<APPROVAL_TASK_INITIATION_PS>(
                                        "SELECT MKEY, TASK_NO FROM task_hdr WHERE MKEY = @MKEY",
                                        new { MKEY = savedHeaderMKEY },
                                        commandType: CommandType.Text);

                                    if (checkResult != null)
                                    {
                                        Console.WriteLine($"Header verified in database - MKEY: {checkResult.MKEY}, Task_NO: {checkResult.TASK_NO}");

                                        return new APPROVAL_TASK_INITIATION_PS
                                        {
                                            MKEY = savedHeaderMKEY,
                                            TASK_NO = savedTaskNo,
                                            MAIN_ABBR = aPPROVAL_TASK_INITIATION.MAIN_ABBR,
                                            LONG_DESCRIPTION = aPPROVAL_TASK_INITIATION.LONG_DESCRIPTION,
                                            CAREGORY = aPPROVAL_TASK_INITIATION.CAREGORY,
                                            PROPERTY = aPPROVAL_TASK_INITIATION.PROPERTY,
                                            BUILDING_MKEY = aPPROVAL_TASK_INITIATION.BUILDING_MKEY,
                                            COMPLITION_DATE = aPPROVAL_TASK_INITIATION.COMPLITION_DATE,
                                            RESPOSIBLE_EMP_MKEY = aPPROVAL_TASK_INITIATION.RESPOSIBLE_EMP_MKEY,
                                            TAGS = aPPROVAL_TASK_INITIATION.TAGS,
                                            STATUS = aPPROVAL_TASK_INITIATION.STATUS,
                                            CREATED_BY = aPPROVAL_TASK_INITIATION.CREATED_BY,
                                            ResponseStatus = "Partial Success",
                                            //St = "Warning",
                                            Message = $"Header saved but update failed: {errorMessage}"
                                        };
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Exception in SP_UPDATE_APPROVAL_TASK_NO: {ex.Message}");

                        // Check if header still exists
                        using (SqlConnection checkConnection = _dapperDbConnection.CreateConnection() as SqlConnection)
                        {
                            await checkConnection.OpenAsync();
                            var checkResult = await checkConnection.QueryFirstOrDefaultAsync<APPROVAL_TASK_INITIATION_PS>(
                                "SELECT MKEY, TASK_NO FROM task_hdr WHERE MKEY = @MKEY",
                                new { MKEY = savedHeaderMKEY },
                                commandType: CommandType.Text);

                            if (checkResult != null)
                            {
                                Console.WriteLine($"Header verified in database - MKEY: {checkResult.MKEY}, Task_NO: {checkResult.TASK_NO}");

                                return new APPROVAL_TASK_INITIATION_PS
                                {
                                    MKEY = savedHeaderMKEY,
                                    TASK_NO = savedTaskNo,
                                    MAIN_ABBR = aPPROVAL_TASK_INITIATION.MAIN_ABBR,
                                    LONG_DESCRIPTION = aPPROVAL_TASK_INITIATION.LONG_DESCRIPTION,
                                    CAREGORY = aPPROVAL_TASK_INITIATION.CAREGORY,
                                    PROPERTY = aPPROVAL_TASK_INITIATION.PROPERTY,
                                    BUILDING_MKEY = aPPROVAL_TASK_INITIATION.BUILDING_MKEY,
                                    COMPLITION_DATE = aPPROVAL_TASK_INITIATION.COMPLITION_DATE,
                                    RESPOSIBLE_EMP_MKEY = aPPROVAL_TASK_INITIATION.RESPOSIBLE_EMP_MKEY,
                                    TAGS = aPPROVAL_TASK_INITIATION.TAGS,
                                    STATUS = aPPROVAL_TASK_INITIATION.STATUS,
                                    CREATED_BY = aPPROVAL_TASK_INITIATION.CREATED_BY,
                                    ResponseStatus = "Partial Success",
                                    // = "Warning",
                                    Message = $"Header saved but update failed: {ex.Message}"
                                };
                            }
                        }
                    }

                    // Rest of your code...

                    // Fetch and return the complete data
                    return await FetchApprovalData(sqlConnection, savedHeaderMKEY, aPPROVAL_TASK_INITIATION.MKEY, aPPROVAL_TASK_INITIATION.HEADER_MKEY);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");

                // If header was saved, return partial success
                if (savedHeaderMKEY > 0)
                {
                    using (SqlConnection checkConnection = _dapperDbConnection.CreateConnection() as SqlConnection)
                    {
                        await checkConnection.OpenAsync();
                        var checkResult = await checkConnection.QueryFirstOrDefaultAsync<APPROVAL_TASK_INITIATION_PS>(
                            "SELECT MKEY, TASK_NO FROM task_hdr WHERE MKEY = @MKEY",
                            new { MKEY = savedHeaderMKEY },
                            commandType: CommandType.Text);

                        if (checkResult != null)
                        {
                            return new APPROVAL_TASK_INITIATION_PS
                            {
                                MKEY = savedHeaderMKEY,
                                TASK_NO = savedTaskNo,
                                ResponseStatus = "Partial Success",
                                STATUS = "Warning",
                                Message = $"Header saved but error occurred: {ex.Message}"
                            };
                        }
                    }
                }

                return new APPROVAL_TASK_INITIATION_PS
                {
                    ResponseStatus = "Error",
                    Message = ex.Message
                };
            }
        }
        public async Task<APPROVAL_TASK_INITIATION_PS> CreateTaskApprovalTemplateAsync_PS_3T(APPROVAL_TASK_INITIATION_PS aPPROVAL_TASK_INITIATION)
        {
            DateTime dateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
            SqlTransaction transaction = null;
            bool transactionCommitted = false;
            int savedHeaderMKEY = 0;
            string savedTaskNo = string.Empty;

            try
            {
                using (SqlConnection sqlConnection = _dapperDbConnection.CreateConnection() as SqlConnection)
                {
                    if (sqlConnection == null)
                    {
                        throw new InvalidOperationException("The connection must be a SqlConnection to use OpenAsync.");
                    }

                    if (sqlConnection.State != ConnectionState.Open)
                    {
                        await sqlConnection.OpenAsync();
                    }

                    transaction = sqlConnection.BeginTransaction();
                    Console.WriteLine("Transaction started");

                    // Step 1: Insert Header
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@TASK_NO", aPPROVAL_TASK_INITIATION.TASK_NO);
                    parmeters.Add("@TASK_NAME", aPPROVAL_TASK_INITIATION.MAIN_ABBR);
                    parmeters.Add("@TASK_DESCRIPTION", aPPROVAL_TASK_INITIATION.LONG_DESCRIPTION);
                    parmeters.Add("@CATEGORY", aPPROVAL_TASK_INITIATION.CAREGORY);
                    parmeters.Add("@PROJECT_ID", aPPROVAL_TASK_INITIATION.PROPERTY);
                    parmeters.Add("@SUBPROJECT_ID", aPPROVAL_TASK_INITIATION.BUILDING_MKEY);
                    parmeters.Add("@COMPLETION_DATE", aPPROVAL_TASK_INITIATION.COMPLITION_DATE);
                    parmeters.Add("@ASSIGNED_TO", aPPROVAL_TASK_INITIATION.RESPOSIBLE_EMP_MKEY.ToString());
                    parmeters.Add("@TAGS", aPPROVAL_TASK_INITIATION.TAGS);
                    parmeters.Add("@ISNODE", "Y");
                    parmeters.Add("@CLOSE_DATE", aPPROVAL_TASK_INITIATION.TENTATIVE_END_DATE);
                    parmeters.Add("@DUE_DATE", aPPROVAL_TASK_INITIATION.TENTATIVE_END_DATE);
                    parmeters.Add("@TASK_PARENT_ID", 0);
                    parmeters.Add("@STATUS", aPPROVAL_TASK_INITIATION.STATUS);
                    parmeters.Add("@TASK_TYPE", "359");
                    parmeters.Add("@STATUS_PERC", 0.0);
                    parmeters.Add("@TASK_CREATED_BY", aPPROVAL_TASK_INITIATION.INITIATOR);
                    parmeters.Add("@APPROVER_ID", 0);
                    parmeters.Add("@IS_ARCHIVE", null);
                    parmeters.Add("@ATTRIBUTE1", null);
                    parmeters.Add("@ATTRIBUTE2", null);
                    parmeters.Add("@ATTRIBUTE3", null);
                    parmeters.Add("@ATTRIBUTE4", aPPROVAL_TASK_INITIATION.MKEY.ToString());
                    parmeters.Add("@ATTRIBUTE5", aPPROVAL_TASK_INITIATION.HEADER_MKEY.ToString());
                    parmeters.Add("@CREATED_BY", aPPROVAL_TASK_INITIATION.CREATED_BY);
                    parmeters.Add("@CREATION_DATE", dateTime);
                    parmeters.Add("@LAST_UPDATED_BY", aPPROVAL_TASK_INITIATION.CREATED_BY);

                    var approvalTemplate = await sqlConnection.QueryFirstOrDefaultAsync<APPROVAL_TASK_INITIATION_PS>(
                        "SP_INSERT_TASK_DETAILS",
                        parmeters,
                        commandType: CommandType.StoredProcedure,
                        transaction: transaction);

                    Console.WriteLine($"Header insert result - MKEY: {approvalTemplate?.MKEY}, Task_NO: {approvalTemplate?.TASK_NO}, Status: {approvalTemplate?.STATUS}, Message: {approvalTemplate?.Message}");

                    if (approvalTemplate == null || approvalTemplate.MKEY == 0)
                    {
                        if (transaction?.Connection != null)
                        {
                            transaction.Rollback();
                        }
                        return new APPROVAL_TASK_INITIATION_PS
                        {
                            ResponseStatus = "Error",
                            Message = "Failed to insert header data - no MKEY returned"
                        };
                    }

                    // Store the header MKEY and Task No for later use
                    savedHeaderMKEY = approvalTemplate.MKEY;
                    savedTaskNo = approvalTemplate.TASK_NO;

                    // Step 2: Update Task Number (this SP might rollback the transaction)
                    var parmetersTaskNo = new DynamicParameters();
                    parmetersTaskNo.Add("@MKEY", aPPROVAL_TASK_INITIATION.HEADER_MKEY);
                    parmetersTaskNo.Add("@APPROVAL_MKEY", aPPROVAL_TASK_INITIATION.MKEY);
                    parmetersTaskNo.Add("@TASK_NO_MKEY", savedHeaderMKEY);
                    parmetersTaskNo.Add("@TENTATIVE_START_DATE", aPPROVAL_TASK_INITIATION.TENTATIVE_START_DATE);
                    parmetersTaskNo.Add("@RESPOSIBLE_EMP_MKEY", aPPROVAL_TASK_INITIATION.RESPOSIBLE_EMP_MKEY);
                    parmetersTaskNo.Add("@LAST_UPDATED_BY", aPPROVAL_TASK_INITIATION.CREATED_BY);
                    parmetersTaskNo.Add("@INITIATOR", aPPROVAL_TASK_INITIATION.INITIATOR);
                    parmetersTaskNo.Add("@TAGS", aPPROVAL_TASK_INITIATION.TAGS);

                    try
                    {
                        var updateResult = await sqlConnection.QueryFirstOrDefaultAsync<dynamic>(
                            "SP_UPDATE_APPROVAL_TASK_NO",
                            parmetersTaskNo,
                            commandType: CommandType.StoredProcedure,
                            transaction: transaction);

                        Console.WriteLine("SP_UPDATE_APPROVAL_TASK_NO executed successfully");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error in SP_UPDATE_APPROVAL_TASK_NO: {ex.Message}");
                        // The SP might have rolled back the transaction
                    }

                    // Check if transaction is still valid
                    if (transaction?.Connection == null)
                    {
                        Console.WriteLine("Transaction was completed in SP_UPDATE_APPROVAL_TASK_NO");

                        // Verify if header was actually saved
                        using (SqlConnection checkConnection = _dapperDbConnection.CreateConnection() as SqlConnection)
                        {
                            await checkConnection.OpenAsync();
                            var checkResult = await checkConnection.QueryFirstOrDefaultAsync<APPROVAL_TASK_INITIATION_PS>(
                                "SELECT MKEY, TASK_NO FROM task_hdr WHERE MKEY = @MKEY",
                                new { MKEY = savedHeaderMKEY },
                                commandType: CommandType.Text);

                            if (checkResult != null)
                            {
                                Console.WriteLine($"Header verified in database - MKEY: {checkResult.MKEY}, Task_NO: {checkResult.TASK_NO}");

                                // Header exists, return partial success
                                var result = new APPROVAL_TASK_INITIATION_PS
                                {
                                    MKEY = savedHeaderMKEY,
                                    TASK_NO = savedTaskNo,
                                    ResponseStatus = "Partial Success",
                                    STATUS = "Warning",
                                    Message = "Header saved successfully but task update failed. Subtasks may not be processed."
                                };

                                // Try to fetch any subtasks that might have been saved
                                try
                                {
                                    var subtasks = await checkConnection.QueryAsync<APPROVAL_TASK_INITIATION_TRL_SUBTASK>(
                                        "SELECT * FROM project_trl_approval_abbr WHERE header_mkey = @HEADER_MKEY AND approval_mkey = @APPROVAL_MKEY",
                                        new { HEADER_MKEY = aPPROVAL_TASK_INITIATION.HEADER_MKEY, APPROVAL_MKEY = aPPROVAL_TASK_INITIATION.MKEY },
                                        commandType: CommandType.Text);

                                    result.SUBTASK_LIST = subtasks?.ToList() ?? new List<APPROVAL_TASK_INITIATION_TRL_SUBTASK>();
                                }
                                catch (Exception subtaskEx)
                                {
                                    Console.WriteLine($"Error fetching subtasks: {subtaskEx.Message}");
                                }

                                return result;
                            }
                            else
                            {
                                return new APPROVAL_TASK_INITIATION_PS
                                {
                                    ResponseStatus = "Error",
                                    Message = "Transaction failed - header not found in database"
                                };
                            }
                        }
                    }

                    // Step 3: Process Subtasks (only if transaction is still valid)
                    if (aPPROVAL_TASK_INITIATION.SUBTASK_LIST != null && aPPROVAL_TASK_INITIATION.SUBTASK_LIST.Any())
                    {
                        foreach (var SubTask in aPPROVAL_TASK_INITIATION.SUBTASK_LIST)
                        {
                            if (SubTask.APPROVAL_MKEY == aPPROVAL_TASK_INITIATION.MKEY)
                            {
                                continue;
                            }

                            // Get parent MKEY
                            var SubParentMkey = await sqlConnection.QueryFirstOrDefaultAsync<APPROVAL_TASK_INITIATION_TRL_SUBTASK>(
                                "SELECT MKEY FROM TASK_HDR WHERE ATTRIBUTE4 = CAST((SELECT SUBTASK_PARENT_ID FROM APPROVAL_TEMPLATE_TRL_SUBTASK WHERE SUBTASK_MKEY = @APPROVAL_MKEY AND DELETE_FLAG = 'N') AS NVARCHAR(10)) AND DELETE_FLAG = 'N' AND ATTRIBUTE5 = CAST((SELECT MKEY FROM PROJECT_HDR WHERE MKEY = @MKEY AND DELETE_FLAG = 'N') AS NVARCHAR(10))",
                                new { APPROVAL_MKEY = SubTask.APPROVAL_MKEY, MKEY = SubTask.MKEY },
                                transaction: transaction);

                            string TaskPrentNo = string.Empty;

                            if (SubParentMkey != null)
                            {
                                var ParentTask_no = await sqlConnection.QueryFirstOrDefaultAsync<APPROVAL_TASK_INITIATION_TRL_SUBTASK>(
                                    "SELECT CONVERT(VARCHAR(50), TASK_NO) AS TASK_NO FROM TASK_HDR WITH (NOLOCK) WHERE MKEY = @MKEY",
                                    new { MKEY = SubParentMkey.MKEY },
                                    transaction: transaction);
                                TaskPrentNo = ParentTask_no?.TASK_NO?.ToString() ?? savedTaskNo;
                            }
                            else
                            {
                                TaskPrentNo = savedTaskNo;
                            }

                            var Parent_Mkey = await sqlConnection.QueryFirstOrDefaultAsync<APPROVAL_TASK_INITIATION_TRL_SUBTASK>(
                                "SELECT * FROM V_Task_Parent_ID WHERE SUBTASK_PARENT_ID = @SUBTASK_MKEY",
                                new { SUBTASK_MKEY = SubTask.APPROVAL_MKEY },
                                transaction: transaction);

                            // Insert subtask
                            var parmetersSubtask = new DynamicParameters();
                            parmetersSubtask.Add("@TASK_NO", SubTask.TASK_NO);
                            parmetersSubtask.Add("@TASK_NAME", SubTask.APPROVAL_ABBRIVATION);
                            parmetersSubtask.Add("@TASK_DESCRIPTION", SubTask.LONG_DESCRIPTION);
                            parmetersSubtask.Add("@CATEGORY", aPPROVAL_TASK_INITIATION.CAREGORY);
                            parmetersSubtask.Add("@PROJECT_ID", aPPROVAL_TASK_INITIATION.PROPERTY);
                            parmetersSubtask.Add("@SUBPROJECT_ID", aPPROVAL_TASK_INITIATION.BUILDING_MKEY);
                            parmetersSubtask.Add("@COMPLETION_DATE", SubTask.TENTATIVE_END_DATE);
                            parmetersSubtask.Add("@ASSIGNED_TO", SubTask.RESPOSIBLE_EMP_MKEY.ToString());
                            parmetersSubtask.Add("@TAGS", SubTask.TAGS);
                            parmetersSubtask.Add("@CLOSE_DATE", SubTask.TENTATIVE_END_DATE);
                            parmetersSubtask.Add("@DUE_DATE", SubTask.TENTATIVE_END_DATE);
                            parmetersSubtask.Add("@TASK_PARENT_NODE_ID", savedHeaderMKEY);
                            parmetersSubtask.Add("@TASK_PARENT_NUMBER", TaskPrentNo);
                            parmetersSubtask.Add("@TASK_TYPE", "359");
                            parmetersSubtask.Add("@STATUS", SubTask.STATUS);
                            parmetersSubtask.Add("@STATUS_PERC", "0.0");
                            parmetersSubtask.Add("@TASK_CREATED_BY", aPPROVAL_TASK_INITIATION.RESPOSIBLE_EMP_MKEY);
                            parmetersSubtask.Add("@APPROVER_ID", 0);
                            parmetersSubtask.Add("@IS_ARCHIVE", 'N');
                            parmetersSubtask.Add("@ATTRIBUTE1", null);
                            parmetersSubtask.Add("@ATTRIBUTE2", null);
                            parmetersSubtask.Add("@ATTRIBUTE3", null);
                            parmetersSubtask.Add("@ATTRIBUTE4", SubTask.APPROVAL_MKEY);
                            parmetersSubtask.Add("@ATTRIBUTE5", aPPROVAL_TASK_INITIATION.HEADER_MKEY);
                            parmetersSubtask.Add("@CREATED_BY", aPPROVAL_TASK_INITIATION.CREATED_BY);
                            parmetersSubtask.Add("@CREATION_DATE", dateTime);
                            parmetersSubtask.Add("@LAST_UPDATED_BY", aPPROVAL_TASK_INITIATION.CREATED_BY);
                            parmetersSubtask.Add("@APPROVE_ACTION_DATE", null);
                            parmetersSubtask.Add("@Current_Task_Mkey", savedHeaderMKEY);
                            parmetersSubtask.Add("@TASK_PARENT_ID", SubParentMkey?.MKEY ?? savedHeaderMKEY);
                            parmetersSubtask.Add("@ISNODE", Parent_Mkey != null ? "Y" : "N");

                            var approvalSubTemplate = await sqlConnection.QueryFirstOrDefaultAsync<TASK_HDR>(
                                "SP_INSERT_TASK_NODE_DETAILS",
                                parmetersSubtask,
                                commandType: CommandType.StoredProcedure,
                                transaction: transaction);

                            if (approvalSubTemplate != null)
                            {
                                // Update subtask number
                                var parmetersSubTaskNo = new DynamicParameters();
                                parmetersSubTaskNo.Add("@MKEY", SubTask.MKEY);
                                parmetersSubTaskNo.Add("@APPROVAL_MKEY", SubTask.APPROVAL_MKEY);
                                parmetersSubTaskNo.Add("@TASK_NO_MKEY", approvalSubTemplate.MKEY);
                                parmetersSubTaskNo.Add("@COMPLETION_DATE", SubTask.TENTATIVE_END_DATE);
                                parmetersSubTaskNo.Add("@TENTATIVE_START_DATE", SubTask.TENTATIVE_START_DATE);
                                parmetersSubTaskNo.Add("@TENTATIVE_END_DATE", SubTask.TENTATIVE_END_DATE);
                                parmetersSubTaskNo.Add("@DAYS_REQUIRED", SubTask.DAYS_REQUIRED);
                                parmetersSubTaskNo.Add("@LAST_UPDATED_BY", aPPROVAL_TASK_INITIATION.CREATED_BY);
                                parmetersSubTaskNo.Add("@INITIATOR", aPPROVAL_TASK_INITIATION.INITIATOR);
                                parmetersSubTaskNo.Add("@TAGS", SubTask.TAGS);

                                await sqlConnection.QueryFirstOrDefaultAsync<APPROVAL_TASK_INITIATION_PS>(
                                    "SP_UPDATE_APPROVAL_TASK_NO",
                                    parmetersSubTaskNo,
                                    commandType: CommandType.StoredProcedure,
                                    transaction: transaction);
                            }

                            // Check transaction state after each subtask
                            if (transaction?.Connection == null)
                            {
                                Console.WriteLine("Transaction was completed during subtask processing");
                                break;
                            }
                        }
                    }

                    // Commit the transaction if still valid
                    if (transaction?.Connection != null)
                    {
                        await transaction.CommitAsync();
                        transactionCommitted = true;
                        Console.WriteLine("Transaction committed successfully");
                    }

                    // Fetch and return the complete data
                    return await FetchApprovalData(sqlConnection, savedHeaderMKEY, aPPROVAL_TASK_INITIATION.MKEY, aPPROVAL_TASK_INITIATION.HEADER_MKEY);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner Error: {ex.InnerException.Message}");
                }

                // Rollback only if transaction exists and hasn't been committed
                if (transaction != null && !transactionCommitted && transaction.Connection != null)
                {
                    try
                    {
                        transaction.Rollback();
                        Console.WriteLine("Transaction rolled back");
                    }
                    catch (Exception rollbackEx)
                    {
                        Console.WriteLine($"Rollback failed: {rollbackEx.Message}");
                    }
                }

                // If header was saved, return partial success
                if (savedHeaderMKEY > 0)
                {
                    using (SqlConnection checkConnection = _dapperDbConnection.CreateConnection() as SqlConnection)
                    {
                        await checkConnection.OpenAsync();
                        var checkResult = await checkConnection.QueryFirstOrDefaultAsync<APPROVAL_TASK_INITIATION_PS>(
                            "SELECT MKEY, TASK_NO FROM task_hdr WHERE MKEY = @MKEY",
                            new { MKEY = savedHeaderMKEY },
                            commandType: CommandType.Text);

                        if (checkResult != null)
                        {
                            return new APPROVAL_TASK_INITIATION_PS
                            {
                                MKEY = savedHeaderMKEY,
                                TASK_NO = savedTaskNo,
                                ResponseStatus = "Partial Success",
                                STATUS = "Warning",
                                Message = $"Header saved but error occurred: {ex.Message}"
                            };
                        }
                    }
                }

                return new APPROVAL_TASK_INITIATION_PS
                {
                    ResponseStatus = "Error",
                    Message = ex.Message + (ex.InnerException != null ? " - " + ex.InnerException.Message : "")
                };
            }
        }

        private async Task<APPROVAL_TASK_INITIATION_PS> FetchApprovalData(SqlConnection connection, int taskMkey, int approvalMkey, int headerMkey)
        {
            using (SqlConnection fetchConnection = _dapperDbConnection.CreateConnection() as SqlConnection)
            {
                await fetchConnection.OpenAsync();

                // Fetch header
                var header = await fetchConnection.QueryFirstOrDefaultAsync<APPROVAL_TASK_INITIATION_PS>(
                    @"SELECT th.MKEY, th.TASK_NO, th.TASK_NAME as MAIN_ABBR, th.TASK_DESCRIPTION as LONG_DESCRIPTION,
                     th.CATEGORY as CAREGORY, th.PROJECT_ID as PROPERTY, th.SUB_PROJECT_ID as BUILDING_MKEY,
                     th.COMPLETION_DATE, th.ASSIGNED_TO as RESPOSIBLE_EMP_MKEY, th.TAGS, th.STATUS,
                     th.CREATED_BY, th.CREATION_DATE, th.ATTRIBUTE4 as MKEY, th.ATTRIBUTE5 as HEADER_MKEY
              FROM task_hdr th 
              WHERE th.MKEY = @MKEY AND th.DELETE_FLAG = 'N'",
                    new { MKEY = taskMkey },
                    commandType: CommandType.Text);

                if (header == null)
                {
                    return new APPROVAL_TASK_INITIATION_PS
                    {
                        ResponseStatus = "Error",
                        Message = "Failed to retrieve saved data."
                    };
                }

                // Fetch subtasks
                var subtasks = await fetchConnection.QueryAsync<APPROVAL_TASK_INITIATION_TRL_SUBTASK>(
                    @"SELECT pta.* FROM project_trl_approval_abbr pta
              WHERE pta.header_mkey = @HEADER_MKEY 
                AND pta.approval_mkey = @APPROVAL_MKEY 
                AND pta.delete_flag = 'N'",
                    new { HEADER_MKEY = headerMkey, APPROVAL_MKEY = approvalMkey },
                    commandType: CommandType.Text);

                header.SUBTASK_LIST = subtasks?.ToList() ?? new List<APPROVAL_TASK_INITIATION_TRL_SUBTASK>();
                header.ResponseStatus = "Success";
                header.STATUS = "Ok";
                header.Message = "Data retrieved successfully";

                return header;
            }
        }



        public async Task<APPROVAL_TASK_INITIATION_PS> CreateTaskApprovalTemplateAsync_PS_FT(APPROVAL_TASK_INITIATION_PS aPPROVAL_TASK_INITIATION)
        {
            DateTime dateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
            SqlTransaction transaction = null;
            bool transactionCommitted = false;

            try
            {
                using (SqlConnection sqlConnection = _dapperDbConnection.CreateConnection() as SqlConnection)
                {
                    if (sqlConnection == null)
                    {
                        throw new InvalidOperationException("The connection must be a SqlConnection to use OpenAsync.");
                    }

                    await sqlConnection.OpenAsync();
                    transaction = sqlConnection.BeginTransaction();
                    Console.WriteLine("Transaction started");

                    // First insert - Header
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@TASK_NO", aPPROVAL_TASK_INITIATION.TASK_NO);
                    parmeters.Add("@TASK_NAME", aPPROVAL_TASK_INITIATION.MAIN_ABBR);
                    parmeters.Add("@TASK_DESCRIPTION", aPPROVAL_TASK_INITIATION.LONG_DESCRIPTION);
                    parmeters.Add("@CATEGORY", aPPROVAL_TASK_INITIATION.CAREGORY);
                    parmeters.Add("@PROJECT_ID", aPPROVAL_TASK_INITIATION.PROPERTY);
                    parmeters.Add("@SUBPROJECT_ID", aPPROVAL_TASK_INITIATION.BUILDING_MKEY);
                    parmeters.Add("@COMPLETION_DATE", aPPROVAL_TASK_INITIATION.COMPLITION_DATE);
                    parmeters.Add("@ASSIGNED_TO", aPPROVAL_TASK_INITIATION.RESPOSIBLE_EMP_MKEY.ToString());
                    parmeters.Add("@TAGS", aPPROVAL_TASK_INITIATION.TAGS);
                    parmeters.Add("@ISNODE", "Y");
                    parmeters.Add("@CLOSE_DATE", aPPROVAL_TASK_INITIATION.TENTATIVE_END_DATE);
                    parmeters.Add("@DUE_DATE", aPPROVAL_TASK_INITIATION.TENTATIVE_END_DATE);
                    parmeters.Add("@TASK_PARENT_ID", 0);
                    parmeters.Add("@STATUS", aPPROVAL_TASK_INITIATION.STATUS);
                    parmeters.Add("@STATUS_PERC", 0.0);
                    parmeters.Add("@TASK_CREATED_BY", aPPROVAL_TASK_INITIATION.INITIATOR);
                    parmeters.Add("@APPROVER_ID", 0);
                    parmeters.Add("@TASK_TYPE", 359);
                    parmeters.Add("@IS_ARCHIVE", null);
                    parmeters.Add("@ATTRIBUTE1", null);
                    parmeters.Add("@ATTRIBUTE2", null);
                    parmeters.Add("@ATTRIBUTE3", null);
                    parmeters.Add("@ATTRIBUTE4", aPPROVAL_TASK_INITIATION.MKEY.ToString());
                    parmeters.Add("@ATTRIBUTE5", aPPROVAL_TASK_INITIATION.HEADER_MKEY.ToString());
                    parmeters.Add("@CREATED_BY", aPPROVAL_TASK_INITIATION.CREATED_BY);
                    parmeters.Add("@CREATION_DATE", dateTime);
                    parmeters.Add("@LAST_UPDATED_BY", aPPROVAL_TASK_INITIATION.CREATED_BY);
                    parmeters.Add("@APPROVE_ACTION_DATE", null);

                    var approvalTemplate = await sqlConnection.QueryFirstOrDefaultAsync<APPROVAL_TASK_INITIATION_PS>(
                        "SP_INSERT_TASK_DETAILS",
                        parmeters,
                        commandType: CommandType.StoredProcedure,
                        transaction: transaction);

                    Console.WriteLine($"Header insert result - MKEY: {approvalTemplate?.MKEY}, Task_NO: {approvalTemplate.TASK_NO}, Status: {approvalTemplate?.STATUS}, Message: {approvalTemplate?.Message}");

                    if (approvalTemplate == null || approvalTemplate.MKEY == 0)
                    {
                        if (transaction?.Connection != null)
                        {
                            transaction.Rollback();
                        }
                        return new APPROVAL_TASK_INITIATION_PS
                        {
                            ResponseStatus = "Error",
                            Message = "Failed to insert header data - no MKEY returned"
                        };
                    }

                    // Check transaction state before proceeding
                    if (transaction?.Connection == null)
                    {
                        // Transaction was already completed in the stored procedure
                        // The header data might still be saved, so let's check
                        Console.WriteLine("Warning: Transaction was completed in stored procedure");

                        // Verify if header was actually saved
                        using (SqlConnection checkConnection = _dapperDbConnection.CreateConnection() as SqlConnection)
                        {
                            await checkConnection.OpenAsync();
                            var checkResult = await checkConnection.QueryFirstOrDefaultAsync<APPROVAL_TASK_INITIATION_PS>(
                                "SELECT MKEY, TASK_NO FROM task_hdr WHERE MKEY = @MKEY",
                                new { MKEY = approvalTemplate.MKEY },
                                commandType: CommandType.Text);

                            if (checkResult != null)
                            {
                                // Header was saved, return success with warning
                                approvalTemplate.ResponseStatus = "Warning";
                                approvalTemplate.Message = "Header saved but transaction was completed in stored procedure";
                                return approvalTemplate;
                            }
                            else
                            {
                                return new APPROVAL_TASK_INITIATION_PS
                                {
                                    ResponseStatus = "Error",
                                    Message = "Transaction failed - header not saved"
                                };
                            }
                        }
                    }

                    // Update task number for header
                    var parmetersTaskNo = new DynamicParameters();
                    parmetersTaskNo.Add("@MKEY", aPPROVAL_TASK_INITIATION.HEADER_MKEY);
                    parmetersTaskNo.Add("@APPROVAL_MKEY", aPPROVAL_TASK_INITIATION.MKEY);
                    parmetersTaskNo.Add("@TASK_NO_MKEY", approvalTemplate.MKEY);
                    parmetersTaskNo.Add("@TENTATIVE_START_DATE", aPPROVAL_TASK_INITIATION.TENTATIVE_START_DATE);
                    parmetersTaskNo.Add("@RESPOSIBLE_EMP_MKEY", aPPROVAL_TASK_INITIATION.RESPOSIBLE_EMP_MKEY);
                    parmetersTaskNo.Add("@LAST_UPDATED_BY", aPPROVAL_TASK_INITIATION.CREATED_BY);
                    parmetersTaskNo.Add("@INITIATOR", aPPROVAL_TASK_INITIATION.INITIATOR);
                    parmetersTaskNo.Add("@TAGS", aPPROVAL_TASK_INITIATION.TAGS);

                    try
                    {
                        var updateResult = await sqlConnection.QueryFirstOrDefaultAsync<dynamic>(
                            "SP_UPDATE_APPROVAL_TASK_NO",
                            parmetersTaskNo,
                            commandType: CommandType.StoredProcedure,
                            transaction: transaction);

                        // Check if transaction is still valid after this call
                        if (transaction?.Connection == null)
                        {
                            Console.WriteLine("Transaction was completed in SP_UPDATE_APPROVAL_TASK_NO");

                            // Verify if data was saved
                            using (SqlConnection verifyConnection = _dapperDbConnection.CreateConnection() as SqlConnection)
                            {
                                await verifyConnection.OpenAsync();
                                // Verify header
                                var headerCheck = await verifyConnection.QueryFirstOrDefaultAsync<APPROVAL_TASK_INITIATION_PS>(
                                    "SELECT MKEY, TASK_NO FROM task_hdr WHERE MKEY = @MKEY",
                                    new { MKEY = approvalTemplate.MKEY },
                                    commandType: CommandType.Text);

                                if (headerCheck != null)
                                {
                                    approvalTemplate.ResponseStatus = "Warning";
                                    approvalTemplate.Message = "Data saved but transaction was completed in stored procedure";
                                    return approvalTemplate;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error in SP_UPDATE_APPROVAL_TASK_NO: {ex.Message}");
                        if (transaction?.Connection != null)
                        {
                            transaction.Rollback();
                        }

                        // Check if header was saved despite the error
                        using (SqlConnection checkConnection = _dapperDbConnection.CreateConnection() as SqlConnection)
                        {
                            await checkConnection.OpenAsync();
                            var headerCheck = await checkConnection.QueryFirstOrDefaultAsync<APPROVAL_TASK_INITIATION_PS>(
                                "SELECT MKEY, TASK_NO FROM task_hdr WHERE MKEY = @MKEY",
                                new { MKEY = approvalTemplate.MKEY },
                                commandType: CommandType.Text);

                            if (headerCheck != null)
                            {
                                return new APPROVAL_TASK_INITIATION_PS
                                {
                                    ResponseStatus = "Partial Success",
                                    Message = $"Header saved but update failed: {ex.Message}",
                                    MKEY = approvalTemplate.MKEY
                                };
                            }
                        }

                        return new APPROVAL_TASK_INITIATION_PS
                        {
                            ResponseStatus = "Error",
                            Message = $"Error in SP_UPDATE_APPROVAL_TASK_NO: {ex.Message}"
                        };
                    }

                    // If we get here, transaction is still valid
                    // Process subtasks (similar try-catch blocks for each subtask operation)

                    // Final commit
                    if (transaction?.Connection != null)
                    {
                        Console.WriteLine("Attempting to commit transaction...");
                        await transaction.CommitAsync();
                        transactionCommitted = true;
                        Console.WriteLine("Transaction committed successfully");
                    }

                    // Fetch and return the data
                    return await FetchApprovalData_TEST1(approvalTemplate.MKEY, aPPROVAL_TASK_INITIATION.MKEY, aPPROVAL_TASK_INITIATION.HEADER_MKEY);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner Error: {ex.InnerException.Message}");
                }

                // Rollback only if transaction exists and hasn't been committed
                if (transaction != null && !transactionCommitted && transaction.Connection != null)
                {
                    try
                    {
                        transaction.Rollback();
                        Console.WriteLine("Transaction rolled back");
                    }
                    catch (Exception rollbackEx)
                    {
                        Console.WriteLine($"Rollback failed: {rollbackEx.Message}");
                    }
                }

                return new APPROVAL_TASK_INITIATION_PS
                {
                    ResponseStatus = "Error",
                    Message = ex.Message + (ex.InnerException != null ? " - " + ex.InnerException.Message : "")
                };
            }
        }

        private async Task<APPROVAL_TASK_INITIATION_PS> FetchApprovalData_TEST1(int taskMkey, int approvalMkey, int headerMkey)
        {
            using (SqlConnection fetchConnection = _dapperDbConnection.CreateConnection() as SqlConnection)
            {
                await fetchConnection.OpenAsync();

                var parmetersApproval = new DynamicParameters();
                parmetersApproval.Add("@MKEY", taskMkey);
                parmetersApproval.Add("@APPROVAL_MKEY", approvalMkey);

                var AllApprovalTemplate = await fetchConnection.QueryFirstOrDefaultAsync<APPROVAL_TASK_INITIATION_PS>(
                    "SP_GET_APPROVAL_TASK_INITIATION",
                    parmetersApproval,
                    commandType: CommandType.StoredProcedure);

                if (AllApprovalTemplate == null)
                {
                    return new APPROVAL_TASK_INITIATION_PS
                    {
                        ResponseStatus = "Error",
                        Message = "Data saved but failed to retrieve the approval template."
                    };
                }

                // Fetch subtasks
                var subtasks = await fetchConnection.QueryAsync<APPROVAL_TASK_INITIATION_TRL_SUBTASK>(
                    "SP_GET_APPROVAL_TASK_INITIATION_TRL_SUBTASK",
                    parmetersApproval,
                    commandType: CommandType.StoredProcedure);

                AllApprovalTemplate.SUBTASK_LIST = subtasks?.ToList() ?? new List<APPROVAL_TASK_INITIATION_TRL_SUBTASK>();
                AllApprovalTemplate.STATUS = "Ok";
                AllApprovalTemplate.Message = "Data saved successfully";
                AllApprovalTemplate.MKEY = taskMkey;

                return AllApprovalTemplate;
            }
        }
        public async Task<APPROVAL_TASK_INITIATION_PS> CreateTaskApprovalTemplateAsync_PS_TEST2(APPROVAL_TASK_INITIATION_PS aPPROVAL_TASK_INITIATION)
        {
            DateTime dateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
            SqlTransaction transaction = null;
            bool transactionCommitted = false;

            try
            {
                using (SqlConnection sqlConnection = _dapperDbConnection.CreateConnection() as SqlConnection)
                {
                    if (sqlConnection == null)
                    {
                        throw new InvalidOperationException("The connection must be a SqlConnection to use OpenAsync.");
                    }

                    await sqlConnection.OpenAsync();
                    transaction = sqlConnection.BeginTransaction();
                    Console.WriteLine("Transaction started");

                    // First insert - Header
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@TASK_NO", aPPROVAL_TASK_INITIATION.TASK_NO);
                    parmeters.Add("@TASK_NAME", aPPROVAL_TASK_INITIATION.MAIN_ABBR);
                    parmeters.Add("@TASK_DESCRIPTION", aPPROVAL_TASK_INITIATION.LONG_DESCRIPTION);
                    parmeters.Add("@CATEGORY", aPPROVAL_TASK_INITIATION.CAREGORY);
                    parmeters.Add("@PROJECT_ID", aPPROVAL_TASK_INITIATION.PROPERTY);
                    parmeters.Add("@SUBPROJECT_ID", aPPROVAL_TASK_INITIATION.BUILDING_MKEY);
                    parmeters.Add("@COMPLETION_DATE", aPPROVAL_TASK_INITIATION.COMPLITION_DATE);
                    parmeters.Add("@ASSIGNED_TO", aPPROVAL_TASK_INITIATION.RESPOSIBLE_EMP_MKEY.ToString());
                    parmeters.Add("@TAGS", aPPROVAL_TASK_INITIATION.TAGS);
                    parmeters.Add("@ISNODE", "Y");
                    parmeters.Add("@CLOSE_DATE", aPPROVAL_TASK_INITIATION.TENTATIVE_END_DATE);
                    parmeters.Add("@DUE_DATE", aPPROVAL_TASK_INITIATION.TENTATIVE_END_DATE);
                    parmeters.Add("@TASK_PARENT_ID", 0);
                    parmeters.Add("@STATUS", aPPROVAL_TASK_INITIATION.STATUS);
                    parmeters.Add("@STATUS_PERC", 0.0);
                    parmeters.Add("@TASK_CREATED_BY", aPPROVAL_TASK_INITIATION.INITIATOR);
                    parmeters.Add("@APPROVER_ID", 0);
                    parmeters.Add("@TASK_TYPE", 359);
                    parmeters.Add("@IS_ARCHIVE", null);
                    parmeters.Add("@ATTRIBUTE1", null);
                    parmeters.Add("@ATTRIBUTE2", null);
                    parmeters.Add("@ATTRIBUTE3", null);
                    parmeters.Add("@ATTRIBUTE4", aPPROVAL_TASK_INITIATION.MKEY.ToString());
                    parmeters.Add("@ATTRIBUTE5", aPPROVAL_TASK_INITIATION.HEADER_MKEY.ToString());
                    parmeters.Add("@CREATED_BY", aPPROVAL_TASK_INITIATION.CREATED_BY);
                    parmeters.Add("@CREATION_DATE", dateTime);
                    parmeters.Add("@LAST_UPDATED_BY", aPPROVAL_TASK_INITIATION.CREATED_BY);
                    parmeters.Add("@APPROVE_ACTION_DATE", null);

                    var approvalTemplate = await sqlConnection.QueryFirstOrDefaultAsync<APPROVAL_TASK_INITIATION_PS>(
                        "SP_INSERT_TASK_DETAILS",
                        parmeters,
                        commandType: CommandType.StoredProcedure,
                        transaction: transaction);

                    Console.WriteLine($"Header insert result - MKEY: {approvalTemplate?.MKEY}, Status: {approvalTemplate?.STATUS}, Message: {approvalTemplate?.Message}");

                    if (approvalTemplate == null || approvalTemplate.MKEY == 0)
                    {
                        transaction.Rollback();
                        return new APPROVAL_TASK_INITIATION_PS
                        {
                            ResponseStatus = "Error",
                            Message = "Failed to insert header data - no MKEY returned"
                        };
                    }

                    // Update task number for header
                    var parmetersTaskNo = new DynamicParameters();
                    parmetersTaskNo.Add("@MKEY", aPPROVAL_TASK_INITIATION.HEADER_MKEY);
                    parmetersTaskNo.Add("@APPROVAL_MKEY", aPPROVAL_TASK_INITIATION.MKEY);
                    parmetersTaskNo.Add("@TASK_NO_MKEY", approvalTemplate.MKEY);
                    parmetersTaskNo.Add("@TENTATIVE_START_DATE", aPPROVAL_TASK_INITIATION.TENTATIVE_START_DATE);
                    parmetersTaskNo.Add("@RESPOSIBLE_EMP_MKEY", aPPROVAL_TASK_INITIATION.RESPOSIBLE_EMP_MKEY);
                    parmetersTaskNo.Add("@LAST_UPDATED_BY", aPPROVAL_TASK_INITIATION.CREATED_BY);
                    parmetersTaskNo.Add("@INITIATOR", aPPROVAL_TASK_INITIATION.INITIATOR);
                    parmetersTaskNo.Add("@TAGS", aPPROVAL_TASK_INITIATION.TAGS);

                    var updateResult = await sqlConnection.QueryFirstOrDefaultAsync<APPROVAL_TASK_INITIATION_PS>(
                        "SP_UPDATE_APPROVAL_TASK_NO",
                        parmetersTaskNo,
                        commandType: CommandType.StoredProcedure,
                        transaction: transaction);

                    // Check if transaction is still valid
                    if (transaction.Connection == null)
                    {
                        throw new InvalidOperationException("Transaction connection is null - transaction may have been completed");
                    }

                    // Process subtasks
                    if (aPPROVAL_TASK_INITIATION.SUBTASK_LIST != null && aPPROVAL_TASK_INITIATION.SUBTASK_LIST.Any())
                    {
                        foreach (var SubTask in aPPROVAL_TASK_INITIATION.SUBTASK_LIST)
                        {
                            if (SubTask.APPROVAL_MKEY == aPPROVAL_TASK_INITIATION.MKEY)
                            {
                                continue;
                            }

                            // Check transaction state before each subtask
                            if (transaction.Connection == null)
                            {
                                throw new InvalidOperationException("Transaction connection is null - transaction may have been completed");
                            }

                            // Get parent MKEY
                            var SubParentMkey = await sqlConnection.QueryFirstOrDefaultAsync<APPROVAL_TASK_INITIATION_TRL_SUBTASK>(
                                "SELECT MKEY FROM TASK_HDR WHERE ATTRIBUTE4 = CAST((SELECT SUBTASK_PARENT_ID FROM APPROVAL_TEMPLATE_TRL_SUBTASK WHERE SUBTASK_MKEY = @APPROVAL_MKEY AND DELETE_FLAG = 'N') AS NVARCHAR(10)) AND DELETE_FLAG = 'N' AND ATTRIBUTE5 = CAST((SELECT MKEY FROM PROJECT_HDR WHERE MKEY = @MKEY AND DELETE_FLAG = 'N') AS NVARCHAR(10))",
                                new { APPROVAL_MKEY = SubTask.APPROVAL_MKEY, MKEY = SubTask.MKEY },
                                transaction: transaction);

                            string TaskPrentNo;

                            if (SubParentMkey != null)
                            {
                                var ParentTask_no = await sqlConnection.QueryFirstOrDefaultAsync<APPROVAL_TASK_INITIATION_TRL_SUBTASK>(
                                    "SELECT CONVERT(VARCHAR(50), TASK_NO) AS TASK_NO FROM TASK_HDR WITH (NOLOCK) WHERE MKEY = @MKEY",
                                    new { MKEY = SubParentMkey.MKEY },
                                    transaction: transaction);
                                TaskPrentNo = ParentTask_no?.TASK_NO?.ToString() ?? approvalTemplate.TASK_NO.ToString();
                            }
                            else
                            {
                                TaskPrentNo = approvalTemplate.TASK_NO.ToString();
                            }

                            var Parent_Mkey = await sqlConnection.QueryFirstOrDefaultAsync<APPROVAL_TASK_INITIATION_TRL_SUBTASK>(
                                "SELECT * FROM V_Task_Parent_ID WHERE SUBTASK_PARENT_ID = @SUBTASK_MKEY",
                                new { SUBTASK_MKEY = SubTask.APPROVAL_MKEY },
                                transaction: transaction);

                            // Insert subtask
                            var parmetersSubtask = new DynamicParameters();
                            parmetersSubtask.Add("@TASK_NO", SubTask.TASK_NO);
                            parmetersSubtask.Add("@TASK_NAME", SubTask.APPROVAL_ABBRIVATION);
                            parmetersSubtask.Add("@TASK_DESCRIPTION", SubTask.LONG_DESCRIPTION);
                            parmetersSubtask.Add("@CATEGORY", aPPROVAL_TASK_INITIATION.CAREGORY);
                            parmetersSubtask.Add("@PROJECT_ID", aPPROVAL_TASK_INITIATION.PROPERTY);
                            parmetersSubtask.Add("@SUBPROJECT_ID", aPPROVAL_TASK_INITIATION.BUILDING_MKEY);
                            parmetersSubtask.Add("@COMPLETION_DATE", SubTask.TENTATIVE_END_DATE);
                            parmetersSubtask.Add("@ASSIGNED_TO", SubTask.RESPOSIBLE_EMP_MKEY.ToString());
                            parmetersSubtask.Add("@TAGS", SubTask.TAGS);
                            parmetersSubtask.Add("@CLOSE_DATE", SubTask.TENTATIVE_END_DATE);
                            parmetersSubtask.Add("@DUE_DATE", SubTask.TENTATIVE_END_DATE);
                            parmetersSubtask.Add("@TASK_PARENT_NODE_ID", approvalTemplate.MKEY);
                            parmetersSubtask.Add("@TASK_PARENT_NUMBER", TaskPrentNo);
                            parmetersSubtask.Add("@TASK_TYPE", "359");
                            parmetersSubtask.Add("@STATUS", SubTask.STATUS);
                            parmetersSubtask.Add("@STATUS_PERC", "0.0");
                            parmetersSubtask.Add("@TASK_CREATED_BY", aPPROVAL_TASK_INITIATION.RESPOSIBLE_EMP_MKEY);
                            parmetersSubtask.Add("@APPROVER_ID", 0);
                            parmetersSubtask.Add("@IS_ARCHIVE", 'N');
                            parmetersSubtask.Add("@ATTRIBUTE1", null);
                            parmetersSubtask.Add("@ATTRIBUTE2", null);
                            parmetersSubtask.Add("@ATTRIBUTE3", null);
                            parmetersSubtask.Add("@ATTRIBUTE4", SubTask.APPROVAL_MKEY);
                            parmetersSubtask.Add("@ATTRIBUTE5", aPPROVAL_TASK_INITIATION.HEADER_MKEY);
                            parmetersSubtask.Add("@CREATED_BY", aPPROVAL_TASK_INITIATION.CREATED_BY);
                            parmetersSubtask.Add("@CREATION_DATE", dateTime);
                            parmetersSubtask.Add("@LAST_UPDATED_BY", aPPROVAL_TASK_INITIATION.CREATED_BY);
                            parmetersSubtask.Add("@APPROVE_ACTION_DATE", null);
                            parmetersSubtask.Add("@Current_Task_Mkey", approvalTemplate.MKEY);
                            parmetersSubtask.Add("@TASK_PARENT_ID", SubParentMkey?.MKEY ?? approvalTemplate.MKEY);
                            parmetersSubtask.Add("@ISNODE", Parent_Mkey != null ? "Y" : "N");

                            var approvalSubTemplate = await sqlConnection.QueryFirstOrDefaultAsync<TASK_HDR>(
                                "SP_INSERT_TASK_NODE_DETAILS",
                                parmetersSubtask,
                                commandType: CommandType.StoredProcedure,
                                transaction: transaction);

                            if (approvalSubTemplate != null)
                            {
                                // Update subtask number
                                var parmetersSubTaskNo = new DynamicParameters();
                                parmetersSubTaskNo.Add("@MKEY", SubTask.MKEY);
                                parmetersSubTaskNo.Add("@APPROVAL_MKEY", SubTask.APPROVAL_MKEY);
                                parmetersSubTaskNo.Add("@TASK_NO_MKEY", approvalSubTemplate.MKEY);
                                parmetersSubTaskNo.Add("@COMPLETION_DATE", SubTask.TENTATIVE_END_DATE);
                                parmetersSubTaskNo.Add("@TENTATIVE_START_DATE", SubTask.TENTATIVE_START_DATE);
                                parmetersSubTaskNo.Add("@TENTATIVE_END_DATE", SubTask.TENTATIVE_END_DATE);
                                parmetersSubTaskNo.Add("@DAYS_REQUIRED", SubTask.DAYS_REQUIRED);
                                parmetersSubTaskNo.Add("@LAST_UPDATED_BY", aPPROVAL_TASK_INITIATION.CREATED_BY);
                                parmetersSubTaskNo.Add("@INITIATOR", aPPROVAL_TASK_INITIATION.INITIATOR);
                                parmetersSubTaskNo.Add("@TAGS", SubTask.TAGS);

                                await sqlConnection.QueryFirstOrDefaultAsync<APPROVAL_TASK_INITIATION_PS>(
                                    "SP_UPDATE_APPROVAL_TASK_NO",
                                    parmetersSubTaskNo,
                                    commandType: CommandType.StoredProcedure,
                                    transaction: transaction);
                            }
                        }
                    }

                    // Final check before commit
                    if (transaction.Connection == null)
                    {
                        throw new InvalidOperationException("Cannot commit - transaction connection is null");
                    }

                    // Commit the transaction
                    Console.WriteLine("Attempting to commit transaction...");
                    await transaction.CommitAsync();
                    transactionCommitted = true;
                    Console.WriteLine("Transaction committed successfully");

                    // Fetch the complete data after commit
                    using (SqlConnection fetchConnection = _dapperDbConnection.CreateConnection() as SqlConnection)
                    {
                        await fetchConnection.OpenAsync();

                        var parmetersApproval = new DynamicParameters();
                        parmetersApproval.Add("@MKEY", approvalTemplate.MKEY);
                        parmetersApproval.Add("@APPROVAL_MKEY", aPPROVAL_TASK_INITIATION.MKEY);

                        var AllApprovalTemplate = await fetchConnection.QueryFirstOrDefaultAsync<APPROVAL_TASK_INITIATION_PS>(
                            "SP_GET_APPROVAL_TASK_INITIATION",
                            parmetersApproval,
                            commandType: CommandType.StoredProcedure);

                        if (AllApprovalTemplate == null)
                        {
                            return new APPROVAL_TASK_INITIATION_PS
                            {
                                ResponseStatus = "Error",
                                Message = "Data saved but failed to retrieve the approval template."
                            };
                        }

                        // Fetch subtasks
                        var subtasks = await fetchConnection.QueryAsync<APPROVAL_TASK_INITIATION_TRL_SUBTASK>(
                            "SP_GET_APPROVAL_TASK_INITIATION_TRL_SUBTASK",
                            parmetersApproval,
                            commandType: CommandType.StoredProcedure);

                        AllApprovalTemplate.SUBTASK_LIST = subtasks?.ToList() ?? new List<APPROVAL_TASK_INITIATION_TRL_SUBTASK>();
                        AllApprovalTemplate.STATUS = "Ok";
                        AllApprovalTemplate.Message = "Data saved successfully";
                        AllApprovalTemplate.MKEY = approvalTemplate.MKEY;

                        return AllApprovalTemplate;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner Error: {ex.InnerException.Message}");
                }

                // Rollback only if transaction exists, hasn't been committed, and connection is still valid
                if (transaction != null && !transactionCommitted && transaction.Connection != null)
                {
                    try
                    {
                        transaction.Rollback();
                        Console.WriteLine("Transaction rolled back");
                    }
                    catch (Exception rollbackEx)
                    {
                        Console.WriteLine($"Rollback failed: {rollbackEx.Message}");
                    }
                }

                return new APPROVAL_TASK_INITIATION_PS
                {
                    ResponseStatus = "Error",
                    Message = ex.Message + (ex.InnerException != null ? " - " + ex.InnerException.Message : "")
                };
            }
        }
        public async Task<APPROVAL_TASK_INITIATION_PS> CreateTaskApprovalTemplateAsync_PS_Test(APPROVAL_TASK_INITIATION_PS aPPROVAL_TASK_INITIATION)
        {
            DateTime dateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
            SqlTransaction transaction = null;

            try
            {
                using (SqlConnection sqlConnection = _dapperDbConnection.CreateConnection() as SqlConnection)
                {
                    if (sqlConnection == null)
                    {
                        throw new InvalidOperationException("The connection must be a SqlConnection to use OpenAsync.");
                    }

                    await sqlConnection.OpenAsync();
                    transaction = sqlConnection.BeginTransaction();

                    // First insert - using the transaction
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@TASK_NO", aPPROVAL_TASK_INITIATION.TASK_NO);
                    parmeters.Add("@TASK_NAME", aPPROVAL_TASK_INITIATION.MAIN_ABBR);
                    parmeters.Add("@TASK_DESCRIPTION", aPPROVAL_TASK_INITIATION.LONG_DESCRIPTION);
                    parmeters.Add("@CATEGORY", aPPROVAL_TASK_INITIATION.CAREGORY);
                    parmeters.Add("@PROJECT_ID", aPPROVAL_TASK_INITIATION.PROPERTY);
                    parmeters.Add("@SUBPROJECT_ID", aPPROVAL_TASK_INITIATION.BUILDING_MKEY);
                    parmeters.Add("@COMPLETION_DATE", aPPROVAL_TASK_INITIATION.COMPLITION_DATE);
                    parmeters.Add("@ASSIGNED_TO", aPPROVAL_TASK_INITIATION.RESPOSIBLE_EMP_MKEY);
                    parmeters.Add("@TAGS", aPPROVAL_TASK_INITIATION.TAGS);
                    parmeters.Add("@ISNODE", "Y");
                    parmeters.Add("@CLOSE_DATE", aPPROVAL_TASK_INITIATION.TENTATIVE_END_DATE);
                    parmeters.Add("@DUE_DATE", aPPROVAL_TASK_INITIATION.TENTATIVE_END_DATE);
                    parmeters.Add("@TASK_PARENT_ID", 0);
                    parmeters.Add("@STATUS", aPPROVAL_TASK_INITIATION.STATUS);
                    parmeters.Add("@TASK_TYPE", "359");
                    parmeters.Add("@STATUS_PERC", 0.0);
                    parmeters.Add("@TASK_CREATED_BY", aPPROVAL_TASK_INITIATION.INITIATOR);
                    parmeters.Add("@APPROVER_ID", 0);
                    parmeters.Add("@IS_ARCHIVE", null);
                    parmeters.Add("@ATTRIBUTE1", null);
                    parmeters.Add("@ATTRIBUTE2", null);
                    parmeters.Add("@ATTRIBUTE3", null);
                    parmeters.Add("@ATTRIBUTE4", aPPROVAL_TASK_INITIATION.MKEY);
                    parmeters.Add("@ATTRIBUTE5", aPPROVAL_TASK_INITIATION.HEADER_MKEY);
                    parmeters.Add("@CREATED_BY", aPPROVAL_TASK_INITIATION.CREATED_BY);
                    parmeters.Add("@CREATION_DATE", dateTime);
                    parmeters.Add("@LAST_UPDATED_BY", aPPROVAL_TASK_INITIATION.CREATED_BY);

                    var approvalTemplate = await sqlConnection.QueryFirstOrDefaultAsync<APPROVAL_TASK_INITIATION_PS>(
                        "SP_INSERT_TASK_DETAILS",
                        parmeters,
                        commandType: CommandType.StoredProcedure,
                        transaction: transaction);

                    if (approvalTemplate == null)
                    {
                        transaction.Rollback();
                        return new APPROVAL_TASK_INITIATION_PS
                        {
                            ResponseStatus = "Error",
                            Message = "Error Occurred"
                        };
                    }

                    // Update task number
                    var parmetersTaskNo = new DynamicParameters();
                    parmetersTaskNo.Add("@MKEY", aPPROVAL_TASK_INITIATION.HEADER_MKEY);
                    parmetersTaskNo.Add("@APPROVAL_MKEY", aPPROVAL_TASK_INITIATION.MKEY);
                    parmetersTaskNo.Add("@TASK_NO_MKEY", approvalTemplate.MKEY);
                    parmetersTaskNo.Add("@TENTATIVE_START_DATE", aPPROVAL_TASK_INITIATION.TENTATIVE_START_DATE);
                    parmetersTaskNo.Add("@RESPOSIBLE_EMP_MKEY", aPPROVAL_TASK_INITIATION.RESPOSIBLE_EMP_MKEY);
                    parmetersTaskNo.Add("@LAST_UPDATED_BY", aPPROVAL_TASK_INITIATION.CREATED_BY);
                    parmetersTaskNo.Add("@INITIATOR", aPPROVAL_TASK_INITIATION.INITIATOR);
                    parmetersTaskNo.Add("@TAGS", aPPROVAL_TASK_INITIATION.TAGS);

                    await sqlConnection.QueryFirstOrDefaultAsync<APPROVAL_TASK_INITIATION_PS>(
                        "SP_UPDATE_APPROVAL_TASK_NO",
                        parmetersTaskNo,
                        commandType: CommandType.StoredProcedure,
                        transaction: transaction);

                    // Process subtasks
                    if (aPPROVAL_TASK_INITIATION.SUBTASK_LIST != null)
                    {
                        foreach (var SubTask in aPPROVAL_TASK_INITIATION.SUBTASK_LIST)
                        {
                            if (SubTask.APPROVAL_MKEY == aPPROVAL_TASK_INITIATION.MKEY)
                            {
                                continue;
                            }

                            // Get parent MKEY
                            var SubParentMkey = await sqlConnection.QueryFirstOrDefaultAsync<APPROVAL_TASK_INITIATION_TRL_SUBTASK>(
                                "SELECT MKEY FROM TASK_HDR WHERE ATTRIBUTE4 = CAST((SELECT SUBTASK_PARENT_ID FROM APPROVAL_TEMPLATE_TRL_SUBTASK WHERE SUBTASK_MKEY = @APPROVAL_MKEY AND DELETE_FLAG = 'N') AS NVARCHAR(10)) AND DELETE_FLAG = 'N' AND ATTRIBUTE5 = CAST((SELECT MKEY FROM PROJECT_HDR WHERE MKEY = @MKEY AND DELETE_FLAG = 'N') AS NVARCHAR(10))",
                                new { APPROVAL_MKEY = SubTask.APPROVAL_MKEY, MKEY = SubTask.MKEY },
                                transaction: transaction);

                            string TaskPrentNo;

                            if (SubParentMkey != null)
                            {
                                var ParentTask_no = await sqlConnection.QueryFirstOrDefaultAsync<APPROVAL_TASK_INITIATION_TRL_SUBTASK>(
                                    "SELECT CONVERT(VARCHAR(50), TASK_NO) AS TASK_NO FROM TASK_HDR WITH (NOLOCK) WHERE MKEY = @MKEY",
                                    new { MKEY = SubParentMkey.MKEY },
                                    transaction: transaction);
                                TaskPrentNo = ParentTask_no?.TASK_NO?.ToString() ?? approvalTemplate.TASK_NO.ToString();
                            }
                            else
                            {
                                TaskPrentNo = approvalTemplate.TASK_NO.ToString();
                            }

                            var Parent_Mkey = await sqlConnection.QueryFirstOrDefaultAsync<APPROVAL_TASK_INITIATION_TRL_SUBTASK>(
                                "SELECT * FROM V_Task_Parent_ID WHERE SUBTASK_PARENT_ID = @SUBTASK_MKEY",
                                new { SUBTASK_MKEY = SubTask.APPROVAL_MKEY },
                                transaction: transaction);

                            // Insert subtask
                            var parmetersSubtask = new DynamicParameters();
                            parmetersSubtask.Add("@TASK_NO", SubTask.TASK_NO);
                            parmetersSubtask.Add("@TASK_NAME", SubTask.APPROVAL_ABBRIVATION);
                            parmetersSubtask.Add("@TASK_DESCRIPTION", SubTask.LONG_DESCRIPTION);
                            parmetersSubtask.Add("@CATEGORY", aPPROVAL_TASK_INITIATION.CAREGORY);
                            parmetersSubtask.Add("@PROJECT_ID", aPPROVAL_TASK_INITIATION.PROPERTY);
                            parmetersSubtask.Add("@SUBPROJECT_ID", aPPROVAL_TASK_INITIATION.BUILDING_MKEY);
                            parmetersSubtask.Add("@COMPLETION_DATE", SubTask.TENTATIVE_END_DATE);
                            parmetersSubtask.Add("@ASSIGNED_TO", SubTask.RESPOSIBLE_EMP_MKEY);
                            parmetersSubtask.Add("@TAGS", SubTask.TAGS);
                            parmetersSubtask.Add("@CLOSE_DATE", SubTask.TENTATIVE_END_DATE);
                            parmetersSubtask.Add("@DUE_DATE", SubTask.TENTATIVE_END_DATE);
                            parmetersSubtask.Add("@TASK_PARENT_NODE_ID", approvalTemplate.MKEY);
                            parmetersSubtask.Add("@TASK_PARENT_NUMBER", TaskPrentNo);
                            parmetersSubtask.Add("@TASK_TYPE", "359");
                            parmetersSubtask.Add("@STATUS", SubTask.STATUS);
                            parmetersSubtask.Add("@STATUS_PERC", "0.0");
                            parmetersSubtask.Add("@TASK_CREATED_BY", aPPROVAL_TASK_INITIATION.RESPOSIBLE_EMP_MKEY);
                            parmetersSubtask.Add("@APPROVER_ID", 0);
                            parmetersSubtask.Add("@IS_ARCHIVE", 'N');
                            parmetersSubtask.Add("@ATTRIBUTE1", null);
                            parmetersSubtask.Add("@ATTRIBUTE2", null);
                            parmetersSubtask.Add("@ATTRIBUTE3", null);
                            parmetersSubtask.Add("@ATTRIBUTE4", SubTask.APPROVAL_MKEY);
                            parmetersSubtask.Add("@ATTRIBUTE5", aPPROVAL_TASK_INITIATION.HEADER_MKEY);
                            parmetersSubtask.Add("@CREATED_BY", aPPROVAL_TASK_INITIATION.CREATED_BY);
                            parmetersSubtask.Add("@CREATION_DATE", dateTime);
                            parmetersSubtask.Add("@LAST_UPDATED_BY", aPPROVAL_TASK_INITIATION.CREATED_BY);
                            parmetersSubtask.Add("@APPROVE_ACTION_DATE", null);
                            parmetersSubtask.Add("@Current_Task_Mkey", approvalTemplate.MKEY);
                            parmetersSubtask.Add("@TASK_PARENT_ID", SubParentMkey?.MKEY ?? approvalTemplate.MKEY);
                            parmetersSubtask.Add("@ISNODE", Parent_Mkey != null ? "Y" : "N");

                            var approvalSubTemplate = await sqlConnection.QueryFirstOrDefaultAsync<TASK_HDR>(
                                "SP_INSERT_TASK_NODE_DETAILS",
                                parmetersSubtask,
                                commandType: CommandType.StoredProcedure,
                                transaction: transaction);

                            // Update subtask number
                            var parmetersSubTaskNo = new DynamicParameters();
                            parmetersSubTaskNo.Add("@MKEY", SubTask.MKEY);
                            parmetersSubTaskNo.Add("@APPROVAL_MKEY", SubTask.APPROVAL_MKEY);
                            parmetersSubTaskNo.Add("@TASK_NO_MKEY", approvalSubTemplate?.MKEY);
                            parmetersSubTaskNo.Add("@COMPLETION_DATE", SubTask.TENTATIVE_END_DATE);
                            parmetersSubTaskNo.Add("@TENTATIVE_START_DATE", SubTask.TENTATIVE_START_DATE);
                            parmetersSubTaskNo.Add("@TENTATIVE_END_DATE", SubTask.TENTATIVE_END_DATE);
                            parmetersSubTaskNo.Add("@DAYS_REQUIRED", SubTask.DAYS_REQUIRED);
                            parmetersSubTaskNo.Add("@LAST_UPDATED_BY", aPPROVAL_TASK_INITIATION.CREATED_BY);
                            parmetersSubTaskNo.Add("@INITIATOR", aPPROVAL_TASK_INITIATION.INITIATOR);
                            parmetersSubTaskNo.Add("@TAGS", SubTask.TAGS);

                            await sqlConnection.QueryFirstOrDefaultAsync<APPROVAL_TASK_INITIATION_PS>(
                                "SP_UPDATE_APPROVAL_TASK_NO",
                                parmetersSubTaskNo,
                                commandType: CommandType.StoredProcedure,
                                transaction: transaction);
                        }
                    }

                    // Commit the transaction
                    await transaction.CommitAsync();

                    // Fetch data using a new connection (don't reuse the committed transaction)
                    using (SqlConnection fetchConnection = _dapperDbConnection.CreateConnection() as SqlConnection)
                    {
                        await fetchConnection.OpenAsync();

                        var parmetersApproval = new DynamicParameters();
                        parmetersApproval.Add("@MKEY", aPPROVAL_TASK_INITIATION.HEADER_MKEY);
                        parmetersApproval.Add("@APPROVAL_MKEY", aPPROVAL_TASK_INITIATION.MKEY);

                        var AllApprovalTemplate = await fetchConnection.QueryFirstOrDefaultAsync<APPROVAL_TASK_INITIATION_PS>(
                            "SP_GET_APPROVAL_TASK_INITIATION",
                            parmetersApproval,
                            commandType: CommandType.StoredProcedure);

                        if (AllApprovalTemplate == null)
                        {
                            return new APPROVAL_TASK_INITIATION_PS
                            {
                                ResponseStatus = "Error",
                                Message = "An unexpected error occurred while retrieving the approval template."
                            };
                        }

                        var subtasks = await fetchConnection.QueryAsync<APPROVAL_TASK_INITIATION_TRL_SUBTASK>(
                            "SP_GET_APPROVAL_TASK_INITIATION_TRL_SUBTASK",
                            parmetersApproval,
                            commandType: CommandType.StoredProcedure);

                        AllApprovalTemplate.SUBTASK_LIST = subtasks.ToList();
                        AllApprovalTemplate.STATUS = "Ok";
                        AllApprovalTemplate.Message = "Data saved successfully";

                        return AllApprovalTemplate;
                    }
                }
            }
            catch (Exception ex)
            {
                // Rollback if transaction exists and hasn't been committed
                if (transaction != null && transaction.Connection != null)
                {
                    try
                    {
                        transaction.Rollback();
                    }
                    catch (Exception rollbackEx)
                    {
                        Console.WriteLine($"Rollback failed: {rollbackEx.Message}");
                    }
                }

                return new APPROVAL_TASK_INITIATION_PS
                {
                    ResponseStatus = "Error",
                    Message = ex.Message
                };
            }
        }

        #endregion

        #region
        //public async Task<APPROVAL_TASK_INITIATION_PS> CreateTaskApprovalTemplateAsync_PS(APPROVAL_TASK_INITIATION_PS aPPROVAL_TASK_INITIATION)
        //{
        //    DateTime dateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);

        //    using (IDbConnection db = _dapperDbConnection.CreateConnection())
        //    {
        //        var sqlConnection = db as SqlConnection;
        //        if (sqlConnection == null)
        //        {
        //            throw new InvalidOperationException("The connection must be a SqlConnection.");
        //        }

        //        if (sqlConnection.State != ConnectionState.Open)
        //        {
        //            await sqlConnection.OpenAsync();
        //        }

        //        using (var transaction = sqlConnection.BeginTransaction())
        //        {
        //            try
        //            {
        //                // 1️⃣ Insert main task
        //                var parameters = new DynamicParameters();
        //                parameters.Add("@TASK_NO", aPPROVAL_TASK_INITIATION.TASK_NO);
        //                parameters.Add("@TASK_NAME", aPPROVAL_TASK_INITIATION.MAIN_ABBR);
        //                parameters.Add("@TASK_DESCRIPTION", aPPROVAL_TASK_INITIATION.LONG_DESCRIPTION);
        //                parameters.Add("@CATEGORY", aPPROVAL_TASK_INITIATION.CAREGORY);
        //                parameters.Add("@PROJECT_ID", aPPROVAL_TASK_INITIATION.PROPERTY);
        //                parameters.Add("@SUBPROJECT_ID", aPPROVAL_TASK_INITIATION.BUILDING_MKEY);
        //                parameters.Add("@COMPLETION_DATE", aPPROVAL_TASK_INITIATION.COMPLITION_DATE);
        //                parameters.Add("@ASSIGNED_TO", aPPROVAL_TASK_INITIATION.RESPOSIBLE_EMP_MKEY);
        //                parameters.Add("@TAGS", aPPROVAL_TASK_INITIATION.TAGS);
        //                parameters.Add("@ISNODE", "Y");
        //                parameters.Add("@CLOSE_DATE", aPPROVAL_TASK_INITIATION.TENTATIVE_END_DATE);
        //                parameters.Add("@DUE_DATE", aPPROVAL_TASK_INITIATION.TENTATIVE_END_DATE);
        //                parameters.Add("@TASK_PARENT_ID", 0);
        //                parameters.Add("@STATUS", aPPROVAL_TASK_INITIATION.STATUS);
        //                parameters.Add("@TASK_TYPE", "359");
        //                parameters.Add("@STATUS_PERC", 0.0);
        //                parameters.Add("@TASK_CREATED_BY", aPPROVAL_TASK_INITIATION.INITIATOR);
        //                parameters.Add("@APPROVER_ID", 0);
        //                parameters.Add("@ATTRIBUTE4", aPPROVAL_TASK_INITIATION.MKEY);
        //                parameters.Add("@ATTRIBUTE5", aPPROVAL_TASK_INITIATION.HEADER_MKEY);
        //                parameters.Add("@CREATED_BY", aPPROVAL_TASK_INITIATION.CREATED_BY);
        //                parameters.Add("@CREATION_DATE", dateTime);
        //                parameters.Add("@LAST_UPDATED_BY", aPPROVAL_TASK_INITIATION.CREATED_BY);

        //                var approvalTemplate = await db.QueryFirstOrDefaultAsync<APPROVAL_TASK_INITIATION_PS>(
        //                    "SP_INSERT_TASK_DETAILS",
        //                    parameters,
        //                    commandType: CommandType.StoredProcedure,
        //                    transaction: transaction
        //                );

        //                if (approvalTemplate == null)
        //                {
        //                    transaction.Rollback();
        //                    return new APPROVAL_TASK_INITIATION_PS
        //                    {
        //                        ResponseStatus = "Error",
        //                        Message = "Failed to insert main task."
        //                    };
        //                }

        //                // 2️⃣ Update task number
        //                var parametersTaskNo = new DynamicParameters();
        //                parametersTaskNo.Add("@MKEY", aPPROVAL_TASK_INITIATION.HEADER_MKEY);
        //                parametersTaskNo.Add("@APPROVAL_MKEY", aPPROVAL_TASK_INITIATION.MKEY);
        //                parametersTaskNo.Add("@TASK_NO_MKEY", approvalTemplate.MKEY);
        //                parametersTaskNo.Add("@TENTATIVE_START_DATE", aPPROVAL_TASK_INITIATION.TENTATIVE_START_DATE);
        //                parametersTaskNo.Add("@RESPOSIBLE_EMP_MKEY", aPPROVAL_TASK_INITIATION.RESPOSIBLE_EMP_MKEY);
        //                parametersTaskNo.Add("@LAST_UPDATED_BY", aPPROVAL_TASK_INITIATION.CREATED_BY);
        //                parametersTaskNo.Add("@INITIATOR", aPPROVAL_TASK_INITIATION.INITIATOR);
        //                parametersTaskNo.Add("@TAGS", aPPROVAL_TASK_INITIATION.TAGS);

        //                await db.QueryFirstOrDefaultAsync<APPROVAL_TASK_INITIATION_PS>(
        //                    "SP_UPDATE_APPROVAL_TASK_NO",
        //                    parametersTaskNo,
        //                    commandType: CommandType.StoredProcedure,
        //                    transaction: transaction
        //                );

        //                // 3️⃣ Insert subtasks
        //                foreach (var SubTask in aPPROVAL_TASK_INITIATION.SUBTASK_LIST)
        //                {
        //                    if (SubTask.APPROVAL_MKEY == aPPROVAL_TASK_INITIATION.MKEY)
        //                        continue;

        //                    var subParentMkey = await db.QueryFirstOrDefaultAsync<APPROVAL_TASK_INITIATION_TRL_SUBTASK>(
        //                        @"SELECT MKEY FROM TASK_HDR 
        //                  WHERE ATTRIBUTE4 = CAST((SELECT SUBTASK_PARENT_ID FROM APPROVAL_TEMPLATE_TRL_SUBTASK 
        //                  WHERE SUBTASK_MKEY = @APPROVAL_MKEY AND DELETE_FLAG = 'N') AS NVARCHAR(10))
        //                  AND DELETE_FLAG = 'N' 
        //                  AND ATTRIBUTE5 = CAST((SELECT MKEY FROM PROJECT_HDR WHERE MKEY = @MKEY AND DELETE_FLAG = 'N') AS NVARCHAR(10))",
        //                        new { APPROVAL_MKEY = SubTask.APPROVAL_MKEY, MKEY = SubTask.MKEY },
        //                        transaction: transaction
        //                    );

        //                    //if (SubParentMkey != null)
        //                    //{
        //                    //    var ParentTask_no = await db.QueryFirstOrDefaultAsync<APPROVAL_TASK_INITIATION_TRL_SUBTASK>("select " +
        //                    //        " CONVERT(VARCHAR(50),TASK_NO)AS TASK_NO from TASK_HDR WITH (NOLOCK)  WHERE MKEY = @MKEY",
        //                    //         new { MKEY = SubParentMkey.MKEY }, transaction: transaction);
        //                    //    TaskPrentNo = ParentTask_no.TASK_NO.ToString();
        //                    //}
        //                    //else
        //                    //{
        //                    //    TaskPrentNo = approvalTemplate.TASK_NO.ToString();
        //                    //}
        //                    //var Parent_Mkey = await db.QueryFirstOrDefaultAsync<APPROVAL_TASK_INITIATION_TRL_SUBTASK>("SELECT * " +
        //                    //    " FROM V_Task_Parent_ID " +
        //                    //    " WHERE SUBTASK_PARENT_ID = @SUBTASK_MKEY ", new { SUBTASK_MKEY = SubTask.APPROVAL_MKEY }, transaction: transaction);

        //                    //var parmetersSubtask = new DynamicParameters();
        //                    //string taskNo = string.Empty;
        //                    //if (SubParentMkey != null && SubParentMkey.MKEY > 0)
        //                    //{
        //                    //    taskNo = string.IsNullOrEmpty(SubParentMkey.MKEY.ToString()) ? "0" : SubParentMkey.MKEY.ToString();
        //                    //}
        //                    //else
        //                    //{
        //                    //    Parent_Mkey = null;
        //                    //}



        //                    string taskParentNo = subParentMkey != null ? subParentMkey.MKEY.ToString() : approvalTemplate.TASK_NO.ToString();
        //                    string parentId = subParentMkey != null ? subParentMkey.MKEY.ToString() : approvalTemplate.MKEY.ToString();
        //                    var parametersSubtask = new DynamicParameters();
        //                    parametersSubtask.Add("@TASK_NO", SubTask.TASK_NO);
        //                    parametersSubtask.Add("@TASK_NAME", SubTask.APPROVAL_ABBRIVATION);
        //                    parametersSubtask.Add("@TASK_DESCRIPTION", SubTask.LONG_DESCRIPTION);
        //                    parametersSubtask.Add("@CATEGORY", aPPROVAL_TASK_INITIATION.CAREGORY);
        //                    parametersSubtask.Add("@PROJECT_ID", aPPROVAL_TASK_INITIATION.PROPERTY);
        //                    parametersSubtask.Add("@SUBPROJECT_ID", aPPROVAL_TASK_INITIATION.BUILDING_MKEY);
        //                    parametersSubtask.Add("@COMPLETION_DATE", SubTask.TENTATIVE_END_DATE);
        //                    parametersSubtask.Add("@ASSIGNED_TO", SubTask.RESPOSIBLE_EMP_MKEY);
        //                    parametersSubtask.Add("@CLOSE_DATE", SubTask.TENTATIVE_END_DATE);
        //                    parametersSubtask.Add("@DUE_DATE", SubTask.TENTATIVE_END_DATE);
        //                    parametersSubtask.Add("@TAGS", SubTask.TAGS);
        //                    parametersSubtask.Add("@TASK_PARENT_NODE_ID", parentId);
        //                    parametersSubtask.Add("@TASK_PARENT_NUMBER", taskParentNo != null ? taskParentNo : approvalTemplate.TASK_NO.ToString());
        //                    parametersSubtask.Add("@TASK_TYPE", "359");
        //                    parametersSubtask.Add("@STATUS", SubTask.STATUS);
        //                    parametersSubtask.Add("@STATUS_PERC", 0.0);
        //                    parametersSubtask.Add("@TASK_CREATED_BY", aPPROVAL_TASK_INITIATION.RESPOSIBLE_EMP_MKEY);
        //                    parametersSubtask.Add("@APPROVER_ID", 0);
        //                    parametersSubtask.Add("@IS_ARCHIVE", 'N');
        //                    parametersSubtask.Add("@ISNODE", parentId != null ? "Y" : "N");
        //                    parametersSubtask.Add("@ATTRIBUTE1", null);
        //                    parametersSubtask.Add("@ATTRIBUTE2", null);
        //                    parametersSubtask.Add("@ATTRIBUTE3", null);
        //                    parametersSubtask.Add("@ATTRIBUTE4", SubTask.APPROVAL_MKEY);
        //                    parametersSubtask.Add("@ATTRIBUTE5", aPPROVAL_TASK_INITIATION.HEADER_MKEY);
        //                    parametersSubtask.Add("@CREATED_BY", aPPROVAL_TASK_INITIATION.CREATED_BY);
        //                    parametersSubtask.Add("@CREATION_DATE", dateTime);
        //                    parametersSubtask.Add("@LAST_UPDATED_BY", aPPROVAL_TASK_INITIATION.CREATED_BY);
        //                    parametersSubtask.Add("@Current_Task_Mkey", approvalTemplate.MKEY);
        //                    parametersSubtask.Add("@TASK_PARENT_ID", parentId);
        //                    parametersSubtask.Add("@APPROVE_ACTION_DATE", null);
        //                    var approvalSubTemplate = await db.QueryFirstOrDefaultAsync<TASK_HDR>(
        //                        "SP_INSERT_TASK_NODE_DETAILS",
        //                        parametersSubtask,
        //                        commandType: CommandType.StoredProcedure,
        //                        transaction: transaction
        //                    );

        //                    // Update subtask number
        //                    var parametersSubTaskNo = new DynamicParameters();
        //                    parametersSubTaskNo.Add("@MKEY", SubTask.MKEY);
        //                    parametersSubTaskNo.Add("@APPROVAL_MKEY", SubTask.APPROVAL_MKEY);
        //                    parametersSubTaskNo.Add("@TASK_NO_MKEY", approvalSubTemplate.MKEY);
        //                    parametersSubTaskNo.Add("@LAST_UPDATED_BY", aPPROVAL_TASK_INITIATION.CREATED_BY);
        //                    parametersSubTaskNo.Add("@INITIATOR", aPPROVAL_TASK_INITIATION.INITIATOR);
        //                    parametersSubTaskNo.Add("@TAGS", SubTask.TAGS);

        //                    await db.QueryFirstOrDefaultAsync<APPROVAL_TASK_INITIATION_PS>(
        //                        "SP_UPDATE_APPROVAL_TASK_NO",
        //                        parametersSubTaskNo,
        //                        commandType: CommandType.StoredProcedure,
        //                        transaction: transaction
        //                    );
        //                }

        //                // 4️⃣ Commit transaction if everything is OK
        //                //await ((SqlTransaction)transaction).CommitAsync();
        //                await transaction.CommitAsync();

        //                // 5️⃣ Fetch final data
        //                var parametersApproval = new DynamicParameters();
        //                parametersApproval.Add("@MKEY", aPPROVAL_TASK_INITIATION.HEADER_MKEY);
        //                parametersApproval.Add("@APPROVAL_MKEY", aPPROVAL_TASK_INITIATION.MKEY);

        //                var allApprovalTemplate = await db.QueryFirstOrDefaultAsync<APPROVAL_TASK_INITIATION_PS>(
        //                    "SP_GET_APPROVAL_TASK_INITIATION",
        //                    parametersApproval,
        //                    commandType: CommandType.StoredProcedure
        //                );

        //                if (allApprovalTemplate == null)
        //                {
        //                    return new APPROVAL_TASK_INITIATION_PS
        //                    {
        //                        ResponseStatus = "Error",
        //                        Message = "Failed to fetch approval template."
        //                    };
        //                }

        //                var subtasksList = await db.QueryAsync<APPROVAL_TASK_INITIATION_TRL_SUBTASK>(
        //                    "SP_GET_APPROVAL_TASK_INITIATION_TRL_SUBTASK",
        //                    parametersApproval,
        //                    commandType: CommandType.StoredProcedure
        //                );

        //                allApprovalTemplate.SUBTASK_LIST = subtasksList.ToList();
        //                allApprovalTemplate.ResponseStatus = "Ok";
        //                allApprovalTemplate.Message = "Data saved successfully";

        //                return allApprovalTemplate;
        //            }
        //            catch (Exception ex)
        //            {
        //                // Rollback safely if anything fails
        //                try
        //                {
        //                    transaction.Rollback();
        //                }
        //                catch
        //                {
        //                    // ignore rollback exceptions
        //                }

        //                return new APPROVAL_TASK_INITIATION_PS
        //                {
        //                    ResponseStatus = "Error",
        //                    Message = ex.Message
        //                };
        //            }
        //        }
        //    }
        //}

        #endregion

        public async Task<APPROVAL_TASK_INITIATION_PS> CreateTaskApprovalTemplateAsync_PS(APPROVAL_TASK_INITIATION_PS aPPROVAL_TASK_INITIATION)
        {
            DateTime dateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
            IDbTransaction transaction = null;
            bool transactionCompleted = false;  // Track the transaction state
            try
            {
                using (IDbConnection db = _dapperDbConnection.CreateConnection())
                {

                    var response = await InsertHeaderTaskAsync(aPPROVAL_TASK_INITIATION);
                    if(response.STATUS == "Error")
                    {
                        return new APPROVAL_TASK_INITIATION_PS
                        {
                            ResponseStatus = "Error",
                            Message = response.Message                             
                        };
                    }
                    else
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

                        var subresponse = await InsertSUbTaskApprovalInitiationAsync(aPPROVAL_TASK_INITIATION, response.TASK_NO ,response.MKEY , aPPROVAL_TASK_INITIATION.CREATED_BY , aPPROVAL_TASK_INITIATION.INITIATOR);
                        
                        if(subresponse.Status == "Error")
                        {
                            transaction.Rollback();
                            return new APPROVAL_TASK_INITIATION_PS
                            {
                                ResponseStatus = "Error",
                                Message = subresponse.Message
                            };
                        }
                    }

                    using (IDbConnection db_Approval = _dapperDbConnection.CreateConnection())
                    {
                       
                        var parmetersApproval = new DynamicParameters();
                        parmetersApproval.Add("@MKEY", aPPROVAL_TASK_INITIATION.HEADER_MKEY);
                        parmetersApproval.Add("@APPROVAL_MKEY", aPPROVAL_TASK_INITIATION.MKEY);

                        // Fetch approval template
                        var AllApprovalTemplate = await db.QueryFirstOrDefaultAsync<APPROVAL_TASK_INITIATION_PS>("SP_GET_APPROVAL_TASK_INITIATION", parmetersApproval, commandType: CommandType.StoredProcedure , transaction: transaction);

                        if (AllApprovalTemplate == null)
                        {
                            var TASK_INITIATION = new APPROVAL_TASK_INITIATION_PS();
                            TASK_INITIATION.ResponseStatus = "Error";
                            TASK_INITIATION.Message = "An unexpected error occurred while retrieving the approval template.";
                            return TASK_INITIATION; // Return null if no results
                        }

                        // Fetch subtasks
                        var subtasks = await db.QueryAsync<APPROVAL_TASK_INITIATION_TRL_SUBTASK>("SP_GET_APPROVAL_TASK_INITIATION_TRL_SUBTASK", parmetersApproval, commandType: CommandType.StoredProcedure , transaction: transaction);
                        response.SUBTASK_LIST = subtasks.ToList(); // Populate the SUBTASK_LIST property with subtasks

                        //var subtasks1 = await db.QueryAsync<APPROVAL_TASK_INITIATION_TRL_SUBTASK>("select * from PROJECT_TRL_APPROVAL_ABBR", commandType: CommandType.Text);
                        AllApprovalTemplate.STATUS = "Ok";
                        AllApprovalTemplate.Message = "Data save successfully";
                        return AllApprovalTemplate;
                    }
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
                        //var TranError = new APPROVAL_TASK_INITIATION();
                        //TranError.ResponseStatus = "Error";
                        //TranError.Message = ex.Message;
                        //return TranError;
                    }
                }

                var approvalTemplate = new APPROVAL_TASK_INITIATION_PS();
                approvalTemplate.ResponseStatus = "Error";
                approvalTemplate.Message = ex.Message;
                return approvalTemplate;
            }
        }




        public async Task<APPROVAL_TASK_INITIATION_PS> InsertHeaderTaskAsync(APPROVAL_TASK_INITIATION_PS model)
        {
            DateTime dateTime = DateTime.Now;

            using (IDbConnection db = _dapperDbConnection.CreateConnection())
            {
                var sqlConnection = db as SqlConnection;

                if (sqlConnection == null)
                    throw new InvalidOperationException("Connection must be SqlConnection.");

                if (sqlConnection.State != ConnectionState.Open)
                    await sqlConnection.OpenAsync();

                using (var transaction = sqlConnection.BeginTransaction())
                {
                    try
                    {
                        // =========================
                        // 1️⃣ Insert Header Task
                        // =========================
                        var parameters = new DynamicParameters();

                        parameters.Add("@TASK_NO", model.TASK_NO);
                        parameters.Add("@TASK_NAME", model.MAIN_ABBR);
                        parameters.Add("@TASK_DESCRIPTION", model.LONG_DESCRIPTION);
                        parameters.Add("@CATEGORY", model.CAREGORY);
                        parameters.Add("@PROJECT_ID", model.PROPERTY);
                        parameters.Add("@SUBPROJECT_ID", model.BUILDING_MKEY);
                        parameters.Add("@COMPLETION_DATE", model.COMPLITION_DATE);
                        parameters.Add("@ASSIGNED_TO", model.RESPOSIBLE_EMP_MKEY);
                        parameters.Add("@TAGS", model.TAGS);
                        parameters.Add("@ISNODE", "Y");
                        parameters.Add("@CLOSE_DATE", model.TENTATIVE_END_DATE);
                        parameters.Add("@DUE_DATE", model.TENTATIVE_END_DATE);
                        parameters.Add("@TASK_PARENT_ID", 0);
                        parameters.Add("@STATUS", model.STATUS);
                        parameters.Add("@TASK_TYPE", "359");
                        parameters.Add("@STATUS_PERC", 0.0);
                        parameters.Add("@TASK_CREATED_BY", model.INITIATOR);
                        parameters.Add("@APPROVER_ID", 0);
                        parameters.Add("@IS_ARCHIVE", null);
                        parameters.Add("@ATTRIBUTE1", null);
                        parameters.Add("@ATTRIBUTE2", null);
                        parameters.Add("@ATTRIBUTE3", null);
                        parameters.Add("@ATTRIBUTE4", model.MKEY);
                        parameters.Add("@ATTRIBUTE5", model.HEADER_MKEY);
                        parameters.Add("@CREATED_BY", model.CREATED_BY);
                        parameters.Add("@CREATION_DATE", dateTime);
                        parameters.Add("@LAST_UPDATED_BY", model.CREATED_BY);

                        var approvalTemplate =
                            await db.QueryFirstOrDefaultAsync<APPROVAL_TASK_INITIATION_PS>(
                                "SP_INSERT_TASK_DETAILS_PS",
                                parameters,
                                commandType: CommandType.StoredProcedure ,
                                transaction: transaction
                            );
                        //transaction: transaction

                        if (approvalTemplate == null)
                        {
                            transaction.Rollback();
                            return new APPROVAL_TASK_INITIATION_PS
                            {
                                ResponseStatus = "Error",
                                Message = "Header Insert Failed"
                            };
                        }

                        // =========================
                        // 3️⃣ Commit Transaction
                        // =========================
                        transaction.Commit();


                        approvalTemplate.ResponseStatus = "OK";
                        approvalTemplate.Message = "Header Inserted Successfully";
                        if(approvalTemplate.Message == "Header Inserted Successfully")
                        {
                            approvalTemplate.TASK_NO = approvalTemplate.TASK_NO;
                            approvalTemplate.MKEY = approvalTemplate.MKEY;
                            await UpdateTaskNumberAsync_ApprovalInitiation(model, approvalTemplate.MKEY);
                        }else
                        {
                            approvalTemplate.ResponseStatus = "Error";
                            approvalTemplate.Message = "Failed to  header insertion.";
                        }


                        return approvalTemplate;
                    }
                    catch (Exception ex)
                    {
                        try
                        {
                            transaction.Rollback();
                        }
                        catch { }

                        return new APPROVAL_TASK_INITIATION_PS
                        {
                            ResponseStatus = "Error",
                            Message = ex.Message
                        };
                    }
                }
            }
        }

        public async Task UpdateTaskNumberAsync_ApprovalInitiation(APPROVAL_TASK_INITIATION_PS model,int insertedTaskMkey)
        {
            using (IDbConnection db = _dapperDbConnection.CreateConnection())
            {
                if (db.State != ConnectionState.Open)
                     db.Open();

                var parametersTaskNo = new DynamicParameters();
                parametersTaskNo.Add("@MKEY", model.HEADER_MKEY);
                parametersTaskNo.Add("@APPROVAL_MKEY", model.MKEY);
                parametersTaskNo.Add("@TASK_NO_MKEY", insertedTaskMkey);
                parametersTaskNo.Add("@TENTATIVE_START_DATE", model.TENTATIVE_START_DATE);
                parametersTaskNo.Add("@RESPOSIBLE_EMP_MKEY", model.RESPOSIBLE_EMP_MKEY);
                parametersTaskNo.Add("@LAST_UPDATED_BY", model.CREATED_BY);
                parametersTaskNo.Add("@INITIATOR", model.INITIATOR);
                parametersTaskNo.Add("@TAGS", model.TAGS);

                // Just execute, no transaction
                await db.ExecuteAsync(
                    "SP_UPDATE_APPROVAL_TASK_NO_PS",
                    parametersTaskNo,
                    commandType: CommandType.StoredProcedure
                );
            }
        }



        public async Task<Commonresponse> InsertSUbTaskApprovalInitiationAsync(APPROVAL_TASK_INITIATION_PS model ,string tASK_NO , int? Task_Mkey ,int? CREATED_BY , int? INITIATOR)
        {
            Commonresponse response = new Commonresponse();
            //APPROVAL_TASK_INITIATION_PS approvalSubTemplate = new APPROVAL_TASK_INITIATION_PS();
            DateTime dateTime = DateTime.Now;
            using (IDbConnection db = _dapperDbConnection.CreateConnection())
            {
                var sqlConnection = db as SqlConnection;

                if (sqlConnection == null)
                    throw new InvalidOperationException("Connection must be SqlConnection.");

                if (sqlConnection.State != ConnectionState.Open)
                    await sqlConnection.OpenAsync();

                //using (var transaction = sqlConnection.BeginTransaction())
                //{
                try
                   {
                    foreach (var SubTask in model.SUBTASK_LIST)
                    {
                        using (var transaction = sqlConnection.BeginTransaction())
                        {
                            try
                            {
                                var SubParentMkey = await db.QueryFirstOrDefaultAsync<APPROVAL_TASK_INITIATION_TRL_SUBTASK>(" SELECT MKEY  " +
                                " FROM TASK_HDR WHERE ATTRIBUTE4 = CAST((SELECT SUBTASK_PARENT_ID FROM APPROVAL_TEMPLATE_TRL_SUBTASK " +
                                " WHERE SUBTASK_MKEY = @APPROVAL_MKEY AND DELETE_FLAG = 'N') AS NVARCHAR(10)) " +
                                " AND DELETE_FLAG = 'N' " +
                                " AND ATTRIBUTE5 = CAST((SELECT MKEY FROM PROJECT_HDR WHERE MKEY = @MKEY AND DELETE_FLAG = 'N') AS NVARCHAR(10)) ",
                                 new { APPROVAL_MKEY = SubTask.APPROVAL_MKEY, MKEY = SubTask.MKEY }, transaction: transaction);
                                string TaskPrentNo = string.Empty;

                                if (SubParentMkey != null)
                                {
                                    var ParentTask_no = await db.QueryFirstOrDefaultAsync<APPROVAL_TASK_INITIATION_TRL_SUBTASK>("select " +
                                        " CONVERT(VARCHAR(50),TASK_NO)AS TASK_NO from TASK_HDR WITH (NOLOCK)  WHERE MKEY = @MKEY",
                                         new { MKEY = SubParentMkey.MKEY }, transaction: transaction);
                                    TaskPrentNo = ParentTask_no.TASK_NO.ToString();
                                }
                                else
                                {
                                    TaskPrentNo = tASK_NO.ToString();
                                }
                                var Parent_Mkey = await db.QueryFirstOrDefaultAsync<APPROVAL_TASK_INITIATION_TRL_SUBTASK>("SELECT * " +
                                    " FROM V_Task_Parent_ID " +
                                    " WHERE SUBTASK_PARENT_ID = @SUBTASK_MKEY ", new { SUBTASK_MKEY = SubTask.APPROVAL_MKEY }, transaction: transaction);

                                var parmetersSubtask = new DynamicParameters();
                                string taskNo = string.Empty;
                                if (SubParentMkey != null && SubParentMkey.MKEY > 0)
                                {
                                    taskNo = string.IsNullOrEmpty(SubParentMkey.MKEY.ToString()) ? "0" : SubParentMkey.MKEY.ToString();
                                }
                                else
                                {
                                    Parent_Mkey = null;
                                }

                                //parmetersSubtask.Add("@TASK_NO", SubParentMkey.MKEY);
                                parmetersSubtask.Add("@TASK_NO", SubTask.TASK_NO);
                                parmetersSubtask.Add("@TASK_NAME", SubTask.APPROVAL_ABBRIVATION);    //Commented by Itemad Hyder 21-11-2025
                                parmetersSubtask.Add("@TASK_DESCRIPTION", SubTask.LONG_DESCRIPTION);
                                parmetersSubtask.Add("@CATEGORY", model.CAREGORY);
                                parmetersSubtask.Add("@PROJECT_ID", model.PROPERTY);
                                parmetersSubtask.Add("@SUBPROJECT_ID", model.BUILDING_MKEY);
                                parmetersSubtask.Add("@COMPLETION_DATE", SubTask.TENTATIVE_END_DATE);
                                parmetersSubtask.Add("@ASSIGNED_TO", SubTask.RESPOSIBLE_EMP_MKEY);
                                parmetersSubtask.Add("@TAGS", SubTask.TAGS);
                                parmetersSubtask.Add("@CLOSE_DATE", SubTask.TENTATIVE_END_DATE);
                                parmetersSubtask.Add("@DUE_DATE", SubTask.TENTATIVE_END_DATE);
                                //parmetersSubtask.Add("@TASK_PARENT_NODE_ID", SubParentMkey.MKEY); // approvalTemplate.MKEY);
                                parmetersSubtask.Add("@TASK_PARENT_NODE_ID", Task_Mkey); // approvalTemplate.MKEY);
                                parmetersSubtask.Add("@TASK_PARENT_NUMBER", TaskPrentNo); // ParentTask_no
                                parmetersSubtask.Add("@TASK_TYPE", "359");
                                parmetersSubtask.Add("@STATUS", SubTask.STATUS);
                                parmetersSubtask.Add("@STATUS_PERC", "0.0");
                                parmetersSubtask.Add("@TASK_CREATED_BY", model.RESPOSIBLE_EMP_MKEY); // header of task
                                parmetersSubtask.Add("@APPROVER_ID", 0);
                                parmetersSubtask.Add("@IS_ARCHIVE", 'N');
                                parmetersSubtask.Add("@ATTRIBUTE1", null);
                                parmetersSubtask.Add("@ATTRIBUTE2", null);
                                parmetersSubtask.Add("@ATTRIBUTE3", null);
                                parmetersSubtask.Add("@ATTRIBUTE4", SubTask.APPROVAL_MKEY);
                                parmetersSubtask.Add("@ATTRIBUTE5", model.HEADER_MKEY);
                                parmetersSubtask.Add("@CREATED_BY", model.CREATED_BY);
                                //parmetersSubtask.Add("@ASSIGNED_TO", aPPROVAL_TASK_INITIATION.RESPOSIBLE_EMP_MKEY);
                                parmetersSubtask.Add("@CREATION_DATE", dateTime);
                                parmetersSubtask.Add("@LAST_UPDATED_BY", model.CREATED_BY);
                                parmetersSubtask.Add("@APPROVE_ACTION_DATE", null);
                                parmetersSubtask.Add("@ATTRIBUTE5", model.HEADER_MKEY); // PROJECT ID 
                                                                                                           //parmetersSubtask.Add("@Current_Task_Mkey", SubParentMkey.MKEY);
                                parmetersSubtask.Add("@Current_Task_Mkey", Task_Mkey);

                                if (SubParentMkey != null)  // to check parent node
                                {
                                    parmetersSubtask.Add("@TASK_PARENT_ID", SubParentMkey.MKEY);
                                }
                                else
                                {
                                    parmetersSubtask.Add("@TASK_PARENT_ID", Task_Mkey);
                                }

                                if (Parent_Mkey != null) // IF THIS PARENT THEN IDNODE Y ELSE N
                                {
                                    parmetersSubtask.Add("@ISNODE", "Y");
                                }
                                else
                                {
                                    parmetersSubtask.Add("@ISNODE", "N");
                                }

                                // Add all parameters here...

                                var approvalTemplate = await db.QueryFirstOrDefaultAsync<TASK_HDR>(
                                    "SP_INSERT_TASK_NODE_DETAILS_PS",
                                    parmetersSubtask,
                                    commandType: CommandType.StoredProcedure,
                                    transaction: transaction);

                                if (approvalTemplate == null)
                                {
                                    transaction.Rollback();

                                    return new Commonresponse
                                    {
                                        Status = "Error",
                                        Message = "Header Insert Failed"
                                    };
                                }
                                transaction.Commit(); // ✅ Commit for this loop only
                                // Call update inside same transaction if possible
                                await UpdateTaskNumberAsync_ApprovalInitiation_SUBTask(
                                    SubTask, approvalTemplate.MKEY, CREATED_BY, INITIATOR);

                                
                            }
                            catch (Exception ex)
                            {
                                transaction.Rollback();

                                return new Commonresponse
                                {
                                    Status = "Error",
                                    Message = ex.Message
                                };
                            }
                        }
                    }
                    return response;
             }
             catch (Exception ex)
             {
                 return new Commonresponse
                 {
                     Status = "Error",
                     Message = ex.Message
                 };
             }                
            }
        }

        public async Task UpdateTaskNumberAsync_ApprovalInitiation_SUBTask(APPROVAL_TASK_INITIATION_TRL_SUBTASK SubTask, int? tASK_NO_MKEY, int? createdby ,int? initiator)
        {
            IDbTransaction transaction = null;
            using (IDbConnection db = _dapperDbConnection.CreateConnection())
            {
                if (db.State != ConnectionState.Open)
                    db.Open();
                //transaction = db.BeginTransaction();
                var parmetersSubTaskNo = new DynamicParameters();
                parmetersSubTaskNo.Add("@MKEY", SubTask.MKEY);
                parmetersSubTaskNo.Add("@APPROVAL_MKEY", SubTask.APPROVAL_MKEY);
                parmetersSubTaskNo.Add("@TASK_NO_MKEY", tASK_NO_MKEY.ToString());
                parmetersSubTaskNo.Add("@COMPLETION_DATE", SubTask.TENTATIVE_END_DATE);
                parmetersSubTaskNo.Add("@TENTATIVE_START_DATE", SubTask.TENTATIVE_START_DATE);
                parmetersSubTaskNo.Add("@TENTATIVE_END_DATE", SubTask.TENTATIVE_END_DATE);
                parmetersSubTaskNo.Add("@DAYS_REQUIRED", SubTask.DAYS_REQUIRED);
                parmetersSubTaskNo.Add("@LAST_UPDATED_BY", createdby);
                parmetersSubTaskNo.Add("@INITIATOR", initiator);
                parmetersSubTaskNo.Add("@TAGS", SubTask.TAGS);

                // Just execute, no transaction
                //await db.ExecuteAsync("SP_UPDATE_APPROVAL_TASK_NO", parmetersSubTaskNo, commandType: CommandType.StoredProcedure , commandTimeout:300);
                //await db.ExecuteAsync("SP_UPDATE_APPROVAL_TASK_NO_OPTIMIZED", parmetersSubTaskNo, commandType: CommandType.StoredProcedure , commandTimeout:300);
              var affectedRows=  await db.ExecuteAsync("SP_UPDATE_APPROVAL_TASK_NO_PS",parmetersSubTaskNo,commandType: CommandType.StoredProcedure, commandTimeout: 300);

                if (affectedRows == 0)
                {
                    throw new Exception("No rows were updated. Check parameter values.");
                }
                await db.ExecuteAsync("SP_UPDATE_APPROVAL_TASK_NO_PS", parmetersSubTaskNo, commandType: CommandType.StoredProcedure); // transaction: transaction
            }
        }






        #endregion



    }
}
