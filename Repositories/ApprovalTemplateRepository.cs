using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using TaskManagement.API.Interfaces;
using TaskManagement.API.Model;

namespace TaskManagement.API.Repositories
{
    public class ApprovalTemplateRepository : IApprovalTemplate
    {
        public IDapperDbConnection _dapperDbConnection;
        public ApprovalTemplateRepository(IDapperDbConnection dapperDbConnection)
        {
            _dapperDbConnection = dapperDbConnection;
        }
        public async Task<IEnumerable<APPROVAL_TEMPLATE_HDR>> GetAllApprovalTemplateAsync()
        {
            using (IDbConnection db = _dapperDbConnection.CreateConnection())
            {
                var parmeters = new DynamicParameters();
                parmeters.Add("@MKEY", null);
                return await db.QueryAsync<APPROVAL_TEMPLATE_HDR>("SP_GET_APPROVAL_TEMPLATE", parmeters, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<APPROVAL_TEMPLATE_HDR> GetApprovalTemplateByIdAsync(int id)
        {
            try
            {
                using (IDbConnection db = _dapperDbConnection.CreateConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@MKEY", id);
                    return await db.QueryFirstOrDefaultAsync<APPROVAL_TEMPLATE_HDR>("SP_GET_APPROVAL_TEMPLATE", parmeters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<APPROVAL_TEMPLATE_HDR> CreateApprovalTemplateAsync(APPROVAL_TEMPLATE_HDR aPPROVAL_TEMPLATE_HDR)
        {
            try
            {
                using (IDbConnection db = _dapperDbConnection.CreateConnection())
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@BUILDING_TYPE", aPPROVAL_TEMPLATE_HDR.BUILDING_TYPE);
                    parameters.Add("@BUILDING_STANDARD", aPPROVAL_TEMPLATE_HDR.BUILDING_STANDARD);
                    parameters.Add("@STATUTORY_AUTHORITY", aPPROVAL_TEMPLATE_HDR.STATUTORY_AUTHORITY);
                    parameters.Add("@SHORT_DESCRIPTION", aPPROVAL_TEMPLATE_HDR.SHORT_DESCRIPTION);
                    parameters.Add("@LONG_DESCRIPTION", aPPROVAL_TEMPLATE_HDR.LONG_DESCRIPTION);
                    parameters.Add("@ABBR", aPPROVAL_TEMPLATE_HDR.ABBR);
                    parameters.Add("@APPROVAL_DEPARTMENT", aPPROVAL_TEMPLATE_HDR.APPROVAL_DEPARTMENT);
                    parameters.Add("@RESPOSIBLE_EMP_MKEY", aPPROVAL_TEMPLATE_HDR.RESPOSIBLE_EMP_MKEY);
                    parameters.Add("@JOB_ROLE", aPPROVAL_TEMPLATE_HDR.JOB_ROLE);
                    parameters.Add("@NO_DAYS_REQUIRED", aPPROVAL_TEMPLATE_HDR.NO_DAYS_REQUIRED);
                    parameters.Add("@SANCTION_AUTHORITY", aPPROVAL_TEMPLATE_HDR.SANCTION_AUTHORITY);
                    parameters.Add("@SANCTION_DEPARTMENT", aPPROVAL_TEMPLATE_HDR.SANCTION_DEPARTMENT);
                    parameters.Add("@END_RESULT_DOC", aPPROVAL_TEMPLATE_HDR.END_RESULT_DOC);
                    parameters.Add("@CHECKLIST_DOC", aPPROVAL_TEMPLATE_HDR.CHECKLIST_DOC);
                    parameters.Add("@DELETE_FLAG", aPPROVAL_TEMPLATE_HDR.DELETE_FLAG);
                    parameters.Add("@ATTRIBUTE1", aPPROVAL_TEMPLATE_HDR.ATTRIBUTE1);
                    parameters.Add("@ATTRIBUTE2", aPPROVAL_TEMPLATE_HDR.ATTRIBUTE2);
                    parameters.Add("@ATTRIBUTE3", aPPROVAL_TEMPLATE_HDR.ATTRIBUTE3);
                    parameters.Add("@ATTRIBUTE4", aPPROVAL_TEMPLATE_HDR.ATTRIBUTE4);
                    parameters.Add("@ATTRIBUTE5", aPPROVAL_TEMPLATE_HDR.ATTRIBUTE5);
                    parameters.Add("@CREATED_BY", aPPROVAL_TEMPLATE_HDR.CREATED_BY);
                    parameters.Add("@LAST_UPDATED_BY", aPPROVAL_TEMPLATE_HDR.LAST_UPDATED_BY);
                    aPPROVAL_TEMPLATE_HDR = await db.QueryFirstOrDefaultAsync<APPROVAL_TEMPLATE_HDR>("SP_INSERT_APPROVAL_TEMPLATE", parameters, commandType: CommandType.StoredProcedure);
                    return aPPROVAL_TEMPLATE_HDR;
                }

            }
            catch (SqlException ex)
            {
                return aPPROVAL_TEMPLATE_HDR;
            }
            catch (Exception ex)
            {
                return aPPROVAL_TEMPLATE_HDR;
            }
        }

        public async Task<APPROVAL_TEMPLATE_HDR> CheckABBRAsync(string ABBR)
        {
            using (IDbConnection db = _dapperDbConnection.CreateConnection())
            {
                return await db.QueryFirstOrDefaultAsync<APPROVAL_TEMPLATE_HDR>("SELECT MKEY,BUILDING_TYPE,BUILDING_STANDARD,STATUTORY_AUTHORITY,SHORT_DESCRIPTION,LONG_DESCRIPTION,ABBR,APPROVAL_DEPARTMENT,RESPOSIBLE_EMP_MKEY,JOB_ROLE,NO_DAYS_REQUIRED,ATTRIBUTE1,ATTRIBUTE2,ATTRIBUTE3,ATTRIBUTE4,ATTRIBUTE5,CREATED_BY,CREATION_DATE,LAST_UPDATED_BY,LAST_UPDATE_DATE,SANCTION_AUTHORITY,SANCTION_DEPARTMENT,END_RESULT_DOC,CHECKLIST_DOC,DELETE_FLAG FROM APPROVAL_TEMPLATE_HDR WHERE ABBR = @ABBR ", new { ABBR = ABBR });
            }
        }
    }
}
