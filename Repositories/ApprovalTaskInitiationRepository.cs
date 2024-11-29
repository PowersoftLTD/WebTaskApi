using Dapper;
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
        public IDapperDbConnection _dapperDbConnection;
        public ApprovalTaskInitiationRepository(IDapperDbConnection dapperDbConnection)
        {
            _dapperDbConnection = dapperDbConnection;
        }

        public async Task<APPROVAL_TASK_INITIATION> GetApprovalTemplateyIdAsync(int MKEY,int APPROVAL_MKEY)
        {
            try
            {
                using (IDbConnection db = _dapperDbConnection.CreateConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@MKEY", MKEY);
                    parmeters.Add("@APPROVAL_MKEY", APPROVAL_MKEY);
                    var approvalTemplate = await db.QueryFirstOrDefaultAsync<APPROVAL_TASK_INITIATION>("SP_GET_APPROVAL_TASK_INITIATION", parmeters, commandType: CommandType.StoredProcedure);

                    if (approvalTemplate == null)
                    {
                       return null; // Return an empty list if no results
                    }

                    parmeters.Add("@MKEY", MKEY);
                    parmeters.Add("@APPROVAL_MKEY", APPROVAL_MKEY);
                    var subtasks = await db.QueryAsync<APPROVAL_TASK_INITIATION_TRL_SUBTASK>("SP_GET_APPROVAL_TASK_INITIATION", parmeters, commandType: CommandType.StoredProcedure);
                    approvalTemplate.SUBTASK_LIST = subtasks.ToList(); // Populate the SUBTASK_LIST property with subtasks
                    return approvalTemplate;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while retrieving the approval template.");
            }

        }
    }
}
