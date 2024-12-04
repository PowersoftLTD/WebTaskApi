using Dapper;
using Microsoft.VisualBasic;
using System.Collections.Immutable;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using TaskManagement.API.DapperDbConnections;
using TaskManagement.API.Interfaces;
using TaskManagement.API.Model;

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
                        approvalTemplate.ResponseStatus = "Error";
                        approvalTemplate.Message = "An unexpected error occurred while retrieving the approval template.";
                        return approvalTemplate; // Return null if no results
                    }

                    // Fetch subtasks
                    var subtasks = await db.QueryAsync<APPROVAL_TASK_INITIATION_TRL_SUBTASK>("SP_GET_APPROVAL_TASK_INITIATION_TRL_SUBTASK", parmeters, commandType: CommandType.StoredProcedure);
                    approvalTemplate.SUBTASK_LIST = subtasks.ToList(); // Populate the SUBTASK_LIST property with subtasks

                    //var subtasks1 = await db.QueryAsync<APPROVAL_TASK_INITIATION_TRL_SUBTASK>("select * from PROJECT_TRL_APPROVAL_ABBR", commandType: CommandType.Text);
                    approvalTemplate.ResponseStatus = "Ok";
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
            try
            {
                using (IDbConnection db = _dapperDbConnection.CreateConnection())
                {
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
                    parmeters.Add("@CLOSE_DATE", null);
                    parmeters.Add("@DUE_DATE", aPPROVAL_TASK_INITIATION.TENTATIVE_END_DATE);
                    parmeters.Add("@TASK_PARENT_ID", 0);
                    parmeters.Add("@STATUS", aPPROVAL_TASK_INITIATION.STATUS);
                    parmeters.Add("@STATUS_PERC", 0.0);
                    parmeters.Add("@TASK_CREATED_BY", aPPROVAL_TASK_INITIATION.CREATED_BY);
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
                    //parmeters.Add("@LAST_UPDATE_DATE", dateTime);

                    // Fetch approval template
                    var approvalTemplate = await db.QueryFirstOrDefaultAsync<APPROVAL_TASK_INITIATION>("SP_INSERT_TASK_DETAILS", parmeters, commandType: CommandType.StoredProcedure);
                    //int ParentMkeySubtask = 0;
                    foreach (var SubTask in aPPROVAL_TASK_INITIATION.SUBTASK_LIST)
                    {

                        var SubParentMkey = await db.QueryFirstOrDefaultAsync<APPROVAL_TASK_INITIATION_TRL_SUBTASK>("SELECT MKEY FROM TASK_HDR WHERE ATTRIBUTE4 IN " +
                            " (SELECT SUBTASK_PARENT_ID FROM APPROVAL_TEMPLATE_TRL_SUBTASK WHERE SUBTASK_MKEY = @APPROVAL_MKEY AND DELETE_FLAG = 'N') " +
                            " AND DELETE_FLAG = 'N' AND ATTRIBUTE5 IN (SELECT MKEY FROM PROJECT_HDR WHERE MKEY = @MKEY AND DELETE_FLAG = 'N') ",
                            new { APPROVAL_MKEY = SubTask.APPROVAL_MKEY, MKEY = SubTask.MKEY });
                        var Parent_Mkey = await db.QueryFirstOrDefaultAsync<APPROVAL_TASK_INITIATION_TRL_SUBTASK>("SELECT * FROM V_Task_Parent_ID WHERE SUBTASK_PARENT_ID = @SUBTASK_MKEY ", new { SUBTASK_MKEY = SubTask.APPROVAL_MKEY });

                        var parmetersSubtask = new DynamicParameters();
                        parmetersSubtask.Add("@TASK_NO", approvalTemplate.TASK_NO);
                        parmetersSubtask.Add("@TASK_NAME", SubTask.APPROVAL_ABBRIVATION);
                        parmetersSubtask.Add("@TASK_DESCRIPTION", SubTask.LONG_DESCRIPTION);
                        parmetersSubtask.Add("@CATEGORY", aPPROVAL_TASK_INITIATION.CAREGORY);
                        parmetersSubtask.Add("@PROJECT_ID", aPPROVAL_TASK_INITIATION.PROPERTY);
                        parmetersSubtask.Add("@SUBPROJECT_ID", aPPROVAL_TASK_INITIATION.BUILDING_MKEY);
                        parmetersSubtask.Add("@COMPLETION_DATE", SubTask.TENTATIVE_END_DATE);
                        parmetersSubtask.Add("@ASSIGNED_TO", SubTask.RESPOSIBLE_EMP_MKEY);
                        parmetersSubtask.Add("@TAGS", null);
                        parmetersSubtask.Add("@CLOSE_DATE", null);
                        parmetersSubtask.Add("@DUE_DATE", SubTask.TENTATIVE_END_DATE);
                        parmetersSubtask.Add("@TASK_PARENT_NODE_ID", approvalTemplate.MKEY);
                        parmetersSubtask.Add("@TASK_PARENT_NUMBER", approvalTemplate.TASK_NO.ToString());
                        parmetersSubtask.Add("@STATUS", SubTask.STATUS);
                        parmetersSubtask.Add("@STATUS_PERC", "0.0");
                        parmetersSubtask.Add("@TASK_CREATED_BY", aPPROVAL_TASK_INITIATION.CREATED_BY);
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
                        parmetersSubtask.Add("@Current_Task_Mkey", approvalTemplate.MKEY);

                        if (SubParentMkey != null)  // to check parent node
                        {
                            parmetersSubtask.Add("@TASK_PARENT_ID", SubParentMkey.MKEY);
                        }

                        if (Parent_Mkey != null) // IF THIS PARENT THEN IDNODE Y ELSE N
                        {
                            parmetersSubtask.Add("@ISNODE", "Y");
                        }
                        else
                        {
                            parmetersSubtask.Add("@ISNODE", "N");
                        }

                        var approvalSubTemplate = await db.QueryFirstOrDefaultAsync<TASK_HDR>("SP_INSERT_TASK_NODE_DETAILS", parmetersSubtask, commandType: CommandType.StoredProcedure);
                        //approvalSubTemplate.MKEY 
                        //approvalSubTemplate.TASK_NO
                    }
                    if (approvalTemplate == null)
                    {
                        approvalTemplate.Message = "Error";
                        approvalTemplate.Message = "Not Found";
                        return approvalTemplate; // Return null if no results
                    }
                    return approvalTemplate;
                }
            }
            catch (Exception ex)
            {
                // Handle other unexpected exceptions
                var approvalTemplate = new APPROVAL_TASK_INITIATION();
                approvalTemplate.ResponseStatus = "Error";
                approvalTemplate.Message = ex.Message;
                return approvalTemplate;
            }
        }
    }
}
