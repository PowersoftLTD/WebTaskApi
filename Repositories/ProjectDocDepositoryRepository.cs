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
using Microsoft.Extensions.Options;

namespace TaskManagement.API.Repositories
{
    public class ProjectDocDepositoryRepository : IProjectDocDepository
    {
        private static TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        private readonly FileSettings _fileSettings;
        public IDapperDbConnection _dapperDbConnection;
        public ProjectDocDepositoryRepository(IDapperDbConnection dapperDbConnection, IOptions<FileSettings> fileSettings)
        {
            _dapperDbConnection = dapperDbConnection;
            _fileSettings = fileSettings.Value;
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
        public async Task<ActionResult<IEnumerable<UpdateProjectDocDepositoryHDROutput_List>>> CreateProjectDocDeositoryAsync(PROJECT_DOC_DEPOSITORY_HDR pROJECT_DOC_DEPOSITORY_HDR)
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

                    transaction = db.BeginTransaction();
                    transactionCompleted = false;  // Reset transaction state

                    var parameters = new DynamicParameters();
                    parameters.Add("@BUILDING_TYPE", pROJECT_DOC_DEPOSITORY_HDR.BUILDING_TYPE);
                    parameters.Add("@PROPERTY_TYPE", pROJECT_DOC_DEPOSITORY_HDR.PROPERTY_TYPE);
                    parameters.Add("@DOC_MKEY", pROJECT_DOC_DEPOSITORY_HDR.DOC_MKEY);
                    parameters.Add("@DOC_NUMBER", pROJECT_DOC_DEPOSITORY_HDR.DOC_NUMBER);
                    parameters.Add("@DOC_DATE", pROJECT_DOC_DEPOSITORY_HDR.DOC_DATE);
                    parameters.Add("@VALIDITY_DATE", pROJECT_DOC_DEPOSITORY_HDR.VALIDITY_DATE);
                    parameters.Add("@API_NAME", "CreateProjectDocDeositoryAsync");
                    parameters.Add("@API_METHOD", "Create");
                    parameters.Add("@CREATED_BY", pROJECT_DOC_DEPOSITORY_HDR.CREATED_BY);

                    var GetProjectDocDesp = await db.QueryAsync<UpdateProjectDocDepositoryHDROutput>("SP_INSERT_PROJECT_DOCUMENT_DEPOSITORY", parameters, commandType: CommandType.StoredProcedure, transaction: transaction);

                    if (GetProjectDocDesp == null)
                    {
                        // Handle other unexpected exceptions
                        RollbackTransaction(transaction);
                        return GenerateErrorResponse("An error occurred");
                    }

                    foreach (var GetCount in GetProjectDocDesp)
                    {
                        pROJECT_DOC_DEPOSITORY_HDR.MKEY = GetCount.MKEY;
                        if (GetCount == null || GetCount.ResponseStatus == "ERROR" || GetCount.MKEY == 0)
                        {
                            return new List<UpdateProjectDocDepositoryHDROutput_List>
                            {
                                new UpdateProjectDocDepositoryHDROutput_List
                                {
                                    STATUS = "Error",
                                    MESSAGE = GetCount.Message,
                                    DATA = null
                                }
                            };
                        }
                    }


                    try
                    {
                        int srNo = 0;
                        string filePathOpen = string.Empty;

                        if (pROJECT_DOC_DEPOSITORY_HDR.PROJECT_DOC_FILES != null || pROJECT_DOC_DEPOSITORY_HDR.PROJECT_DOC_FILES.Count > 0)
                        {
                            var parametersDelete = new DynamicParameters();
                            parametersDelete.Add("@PROJECT_DOC_MKEY", pROJECT_DOC_DEPOSITORY_HDR.PROPERTY_TYPE);
                            parametersDelete.Add("@CREATED_BY", pROJECT_DOC_DEPOSITORY_HDR.VALIDITY_DATE);
                            parametersDelete.Add("@DELETE_FLAG", pROJECT_DOC_DEPOSITORY_HDR.ATTRIBUTE1);
                            parametersDelete.Add("@APINAME", "CreateProjectDocDeositoryAsync");
                            parametersDelete.Add("@API_METHOD", "Update");
                            
                            var DeleteProjectDocFile = await db.QueryAsync<dynamic>("SP_UPDATE_PROJECT_DOC_DEPOSITORY_ATTACHMENT", parametersDelete, commandType: CommandType.StoredProcedure, transaction: transaction); 

                            foreach (var ProjectDocFile in pROJECT_DOC_DEPOSITORY_HDR.PROJECT_DOC_FILES)
                            {
                                if (ProjectDocFile.Length > 0)
                                {
                                    srNo = srNo + 1;
                                    string FilePath = _fileSettings.FilePath;
                                    if (!Directory.Exists(FilePath + "\\Attachments\\" + "Document Depository\\" + pROJECT_DOC_DEPOSITORY_HDR.MKEY))
                                    {
                                        Directory.CreateDirectory(FilePath + "\\Attachments\\" + "Document Depository\\" + pROJECT_DOC_DEPOSITORY_HDR.MKEY);
                                    }
                                    using (FileStream filestream = System.IO.File.Create(FilePath + "\\Attachments\\" + "Document Depository\\"
                                        + pROJECT_DOC_DEPOSITORY_HDR.MKEY + "\\" + DateTime.Now.Day + "_" + DateTime.Now.ToShortTimeString().Replace(":", "_") + "_" + ProjectDocFile.FileName))
                                    {
                                        ProjectDocFile.CopyTo(filestream);
                                        filestream.Flush();
                                    }

                                    filePathOpen = "\\Attachments\\" + "Document Depository\\" + pROJECT_DOC_DEPOSITORY_HDR.MKEY + "\\"
                                        + DateTime.Now.Day + "_" + DateTime.Now.ToShortTimeString().Replace(":", "_") + "_"
                                        + ProjectDocFile.FileName;

                                    var parametersFiles = new DynamicParameters();
                                    parametersFiles.Add("@PROJECT_DOC_MKEY", pROJECT_DOC_DEPOSITORY_HDR.MKEY);
                                    parametersFiles.Add("@FILE_NAME", ProjectDocFile.FileName);
                                    parametersFiles.Add("@FILE_PATH", filePathOpen);
                                    parametersFiles.Add("@CREATED_BY", pROJECT_DOC_DEPOSITORY_HDR.CREATED_BY);
                                    parametersFiles.Add("@DELETE_FLAG", "N");
                                    parametersFiles.Add("@APINAME", "CreateProjectDocDeositoryAsync");
                                    parametersFiles.Add("@API_METHOD", "Create");

                                    var GetProjectDocFile = await db.QueryAsync<DocFileUploadOutPut>("SP_INSERT_PROJECT_DOC_DEPOSITORY_ATTACHMENT", parametersFiles, commandType: CommandType.StoredProcedure, transaction: transaction);

                                    if (GetProjectDocDesp == null)
                                    {
                                        // Handle other unexpected exceptions
                                        RollbackTransaction(transaction);
                                        return GenerateErrorResponse("An error occurred");
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        return new List<UpdateProjectDocDepositoryHDROutput_List>
                        {
                            new UpdateProjectDocDepositoryHDROutput_List
                            {
                                STATUS = "Error",
                                MESSAGE = ex.Message,
                                DATA = null
                            }
                        };
                    }

                    var sqlTransaction = (SqlTransaction)transaction;
                    await sqlTransaction.CommitAsync();
                    transactionCompleted = true;

                    return new List<UpdateProjectDocDepositoryHDROutput_List>
                    {
                        new UpdateProjectDocDepositoryHDROutput_List
                        {
                            STATUS = "Ok",
                            MESSAGE = "Successfully Done",
                            DATA = GetProjectDocDesp
                        }
                    };
                }

                // Ensure that this method always returns a value
                return new List<UpdateProjectDocDepositoryHDROutput_List>
                    {
                        new UpdateProjectDocDepositoryHDROutput_List
                        {
                            STATUS = "Error",
                            MESSAGE = "No Project Doc Depository data found or processed.",
                            DATA = null
                        }
                    };
            }
            catch (Exception ex)
            {
                if (transaction != null && !transactionCompleted)
                {
                    RollbackTransaction(transaction);
                }

                return GenerateErrorResponse(ex.Message);
            }
        }
        private void RollbackTransaction(IDbTransaction transaction)
        {
            try
            {
                transaction?.Rollback();
            }
            catch (InvalidOperationException rollbackEx)
            {
                Console.WriteLine($"Rollback failed: {rollbackEx.Message}");
            }
        }
        private List<UpdateProjectDocDepositoryHDROutput_List> GenerateErrorResponse(string message)
        {
            return new List<UpdateProjectDocDepositoryHDROutput_List>
            {
                new UpdateProjectDocDepositoryHDROutput_List
                {
                    STATUS = "Error",
                    MESSAGE = message,
                    DATA = null
                }
            };
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
                    var ProjectDocDsp = await db.QueryAsync<UpdateProjectDocDepositoryHDROutput>("SP_UPDATE_PROJECT_DOCUMENT_DEPOSITORY", parameters, commandType: CommandType.StoredProcedure, transaction: transaction);

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
                                   STATUS = "Error",
                                    MESSAGE= "An error occurd",
                                    DATA= null
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
                            STATUS = "Ok",
                            MESSAGE = "Message",
                            DATA= ProjectDocDsp
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
                           STATUS = "Error",
                            MESSAGE= ex.Message,
                            DATA= null
                        }
                    };
                return errorResult;
            }
        }
    }
}
