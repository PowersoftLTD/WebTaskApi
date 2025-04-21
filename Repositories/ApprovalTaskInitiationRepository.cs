using Dapper;
using Microsoft.VisualBasic;
using System.Collections.Immutable;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
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

                        var SubParentMkey = await db.QueryFirstOrDefaultAsync<APPROVAL_TASK_INITIATION_TRL_SUBTASK>("SELECT MKEY FROM TASK_HDR " +
                            "WHERE ATTRIBUTE4 IN (SELECT SUBTASK_PARENT_ID FROM APPROVAL_TEMPLATE_TRL_SUBTASK " +
                            "WHERE SUBTASK_MKEY = @APPROVAL_MKEY AND DELETE_FLAG = 'N') " +
                            " AND DELETE_FLAG = 'N' AND ATTRIBUTE5 IN (SELECT MKEY FROM PROJECT_HDR WHERE MKEY = @MKEY AND DELETE_FLAG = 'N') ",
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
    }
}
