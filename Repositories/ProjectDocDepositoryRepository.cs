using Dapper;
using System.Data;
using System;
using TaskManagement.API.DapperDbConnections;
using TaskManagement.API.Interfaces;
using TaskManagement.API.Model;
using System.Data.SqlClient;
using System.Reflection.Metadata;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;

namespace TaskManagement.API.Repositories
{
    public class ProjectDocDepositoryRepository : IProjectDocDepository
    {
        private static TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        public IDapperDbConnection _dapperDbConnection;
        public ProjectDocDepositoryRepository(IDapperDbConnection dapperDbConnection)
        {
            _dapperDbConnection = dapperDbConnection;
        }
        public async Task<IEnumerable<dynamic>> GetAllProjectDocDeositoryAsync(string? ATTRIBUTE1, string? ATTRIBUTE2, string? ATTRIBUTE3)
        {
            try
            {
                using (IDbConnection db = _dapperDbConnection.CreateConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@MKEY", null);
                    parmeters.Add("@ATTRIBUTE1", ATTRIBUTE1);
                    parmeters.Add("@ATTRIBUTE2", ATTRIBUTE2);
                    parmeters.Add("@ATTRIBUTE3", ATTRIBUTE3);
                    var ProjDoc_Desp = await db.QueryAsync("SP_GET_PROJECT_DOC_DEPOSITORY", parmeters, commandType: CommandType.StoredProcedure);
                    return ProjDoc_Desp;
                }
            }
            catch (Exception ex)
            {
                return new List<dynamic>();
            }
        }
        public async Task<dynamic> GetProjectDocDeositoryByIDAsync(int? MKEY, string? ATTRIBUTE1, string? ATTRIBUTE2, string? ATTRIBUTE3)
        {
            try
            {
                using (IDbConnection db = _dapperDbConnection.CreateConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@MKEY", MKEY);
                    parmeters.Add("@ATTRIBUTE1", ATTRIBUTE1);
                    parmeters.Add("@ATTRIBUTE2", ATTRIBUTE2);
                    parmeters.Add("@ATTRIBUTE3", ATTRIBUTE3);
                    var ProjDoc_Desp = await db.QueryAsync("SP_GET_PROJECT_DOC_DEPOSITORY", parmeters, commandType: CommandType.StoredProcedure);
                    return ProjDoc_Desp;
                }
            }
            catch (Exception ex)
            {
                return new List<dynamic>();
            }
        }
        public async Task<PROJECT_DOC_DEPOSITORY_HDR> CreateProjectDocDeositoryAsync(PROJECT_DOC_DEPOSITORY_HDR pROJECT_DOC_DEPOSITORY_HDR)

        {
            try
            {
                using (IDbConnection db = _dapperDbConnection.CreateConnection())
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@BUILDING_TYPE", pROJECT_DOC_DEPOSITORY_HDR.BUILDING_TYPE);
                    parameters.Add("@PROPERTY_TYPE", pROJECT_DOC_DEPOSITORY_HDR.PROPERTY_TYPE);
                    parameters.Add("@DOC_NAME", pROJECT_DOC_DEPOSITORY_HDR.DOC_NAME);
                    parameters.Add("@DOC_NUMBER", pROJECT_DOC_DEPOSITORY_HDR.DOC_NUMBER);
                    parameters.Add("@DOC_DATE", pROJECT_DOC_DEPOSITORY_HDR.DOC_DATE);
                    parameters.Add("@DOC_ATTACHMENT", pROJECT_DOC_DEPOSITORY_HDR.DOC_ATTACHMENT);
                    parameters.Add("@VALIDITY_DATE", pROJECT_DOC_DEPOSITORY_HDR.VALIDITY_DATE);
                    parameters.Add("@ATTRIBUTE1", pROJECT_DOC_DEPOSITORY_HDR.ATTRIBUTE1);
                    parameters.Add("@ATTRIBUTE2", pROJECT_DOC_DEPOSITORY_HDR.ATTRIBUTE2);
                    parameters.Add("@ATTRIBUTE3", pROJECT_DOC_DEPOSITORY_HDR.ATTRIBUTE3);
                    parameters.Add("@CREATED_BY", pROJECT_DOC_DEPOSITORY_HDR.CREATED_BY);

                    var ProjectDocDsp = await db.QueryFirstOrDefaultAsync<PROJECT_DOC_DEPOSITORY_HDR>("SP_INSERT_PROJECT_DOCUMENT_DEPOSITORY", parameters, commandType: CommandType.StoredProcedure);
                    return ProjectDocDsp;
                }
            }
            catch (SqlException ex)
            {
                return null;
            }
            catch (Exception ex)
            {
                return pROJECT_DOC_DEPOSITORY_HDR;
            }
        }
        public async Task<dynamic> GetDocumentDetailsAsync(int? MKEY, string? ATTRIBUTE1, string? ATTRIBUTE2, string? ATTRIBUTE3)
        {
            try
            {
                using (IDbConnection db = _dapperDbConnection.CreateConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@MKEY", MKEY);
                    parmeters.Add("@ATTRIBUTE1", ATTRIBUTE1);
                    parmeters.Add("@ATTRIBUTE2", ATTRIBUTE2);
                    parmeters.Add("@ATTRIBUTE3", ATTRIBUTE3);
                    var ProjDoc_Desp = await db.QueryAsync("GET_SP_DOCUMENT_TEMPLATE_DETAILS", parmeters, commandType: CommandType.StoredProcedure);
                    return ProjDoc_Desp;
                }
            }
            catch (Exception ex)
            {
                return new List<dynamic>();
            }
        }
        public async Task<dynamic> GetPROJECT_DEPOSITORY_DOCUMENTAsync(int? BUILDING_TYPE, int? PROPERTY_TYPE, int? DOC_MKEY)
        {
            try
            {
                using (IDbConnection db = _dapperDbConnection.CreateConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@BUILDING_TYPE", BUILDING_TYPE);
                    parmeters.Add("@PROPERTY_TYPE", PROPERTY_TYPE);
                    parmeters.Add("@DOC_MKEY", DOC_MKEY);
                    int ProjDoc_Desp = await db.QueryFirstOrDefaultAsync<int>("SP_GET_PROJECT_DEPOSITORY_DOCUMENT", parmeters, commandType: CommandType.StoredProcedure);
                    return ProjDoc_Desp;
                }
            }
            catch (Exception ex)
            {
                return new List<dynamic>();
            }
        }
        public async Task<ActionResult<IEnumerable<UpdateProjectDocDepositoryHDROutput_List>>> UpdateProjectDepositoryDocumentAsync(UpdateProjectDocDepositoryHDRInput updateProjectDocDepositoryHDRInput)
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

                    var parameters = new DynamicParameters();
                    parameters.Add("@MKEY", updateProjectDocDepositoryHDRInput.MKEY);
                    parameters.Add("@BUILDING_TYPE", updateProjectDocDepositoryHDRInput.BUILDING_TYPE);
                    parameters.Add("@PROPERTY_TYPE", updateProjectDocDepositoryHDRInput.PROPERTY_TYPE);
                    parameters.Add("@DOC_NAME", updateProjectDocDepositoryHDRInput.DOC_NAME);
                    parameters.Add("@DOC_NUMBER", updateProjectDocDepositoryHDRInput.DOC_NUMBER);
                    parameters.Add("@DOC_DATE", updateProjectDocDepositoryHDRInput.DOC_DATE);
                    parameters.Add("@DOC_ATTACHMENT", updateProjectDocDepositoryHDRInput.DOC_ATTACHMENT);
                    parameters.Add("@VALIDITY_DATE", updateProjectDocDepositoryHDRInput.VALIDITY_DATE);
                    parameters.Add("@CREATED_BY", updateProjectDocDepositoryHDRInput.CREATED_BY);
                    parameters.Add("@DELETE_FLAG", updateProjectDocDepositoryHDRInput.DELETE_FLAG);
                    var ProjectDocDsp = await db.QueryAsync<UpdateProjectDocDepositoryHDROutput>("SP_UPDATE_PROJECT_DOCUMENT_DEPOSITORY", parameters, commandType: CommandType.StoredProcedure,transaction:transaction);

                    if (ProjectDocDsp == null)
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
                        var errorResult = new List<UpdateProjectDocDepositoryHDROutput_List>
                            {
                                new UpdateProjectDocDepositoryHDROutput_List
                                {
                                   Status = "Error",
                                    Message= "An error occurd",
                                    Data= null
                                }
                            };
                        return errorResult;
                    }

                    var sqlTransaction = (SqlTransaction)transaction;
                    await sqlTransaction.CommitAsync();
                    transactionCompleted = true;
                    var successsResult = new List<UpdateProjectDocDepositoryHDROutput_List>
                    {
                        new UpdateProjectDocDepositoryHDROutput_List
                        {
                            Status = "Ok",
                            Message = "Message",
                            Data= ProjectDocDsp
                        }
                    };
                    return successsResult;
                }
            }
            catch (Exception ex)
            {
                var errorResult = new List<UpdateProjectDocDepositoryHDROutput_List>
                    {
                        new UpdateProjectDocDepositoryHDROutput_List
                        {
                           Status = "Error",
                            Message= ex.Message,
                            Data= null
                        }
                    };
                return errorResult;
            }
        }
    }
}
