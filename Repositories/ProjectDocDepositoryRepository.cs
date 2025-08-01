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
using Microsoft.AspNetCore.Rewrite;

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
        public async Task<ActionResult<IEnumerable<UpdateProjectDocDepositoryHDROutput_List>>> GetAllProjectDocDeositoryAsync(ProjectDocDepositoryInput projectDocDepositoryInput)
        {
            DateTime dateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
            IDbTransaction transaction = null;
            bool transactionCompleted = false;  // Track the transaction state
            bool flagCount = false;
            try
            {
                using (IDbConnection db = _dapperDbConnection.CreateConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@MKEY", projectDocDepositoryInput.MKEY);
                    parmeters.Add("@USER_ID", projectDocDepositoryInput.USER_ID);
                    parmeters.Add("@API_NAME", "GetAllProjDocDepsitory");
                    parmeters.Add("@API_METHOD", "Get");
                    var GetProjectDepst = await db.QueryAsync<UpdateProjectDocDepositoryHDROutput>("SP_GET_PROJECT_DOC_DEPOSITORY", parmeters, commandType: CommandType.StoredProcedure);

                    foreach (var ProjectDepst in GetProjectDepst)
                    {

                        if (ProjectDepst.MKEY < 1)
                        {
                            var errorResult = new List<UpdateProjectDocDepositoryHDROutput_List>
                                {
                                    new UpdateProjectDocDepositoryHDROutput_List
                                    {
                                        STATUS = "Ok",
                                        MESSAGE = "Data not found",
                                        DATA = null
                                    }
                                };
                            return errorResult;
                        }
                        flagCount = true;
                        var parametersDepost = new DynamicParameters();
                        parametersDepost.Add("@PROJECT_DOC_MKEY", ProjectDepst.MKEY);
                        parametersDepost.Add("@CREATED_BY", projectDocDepositoryInput.USER_ID);
                        parametersDepost.Add("@APINAME", "CreateProjectDocDeositoryAsync");
                        parametersDepost.Add("@API_METHOD", "Create");
                        var GetProjectDocFile = await db.QueryAsync<DocFileUploadOutPut>("SP_GET_PROJECT_DOC_DEPOSITORY_ATTACHMENT", parametersDepost, commandType: CommandType.StoredProcedure, transaction: transaction);

                        ProjectDepst.PROJECT_DOC_FILES = GetProjectDocFile;
                    }

                    if (flagCount = true)
                    {
                        var successsResult = new List<UpdateProjectDocDepositoryHDROutput_List>
                            {
                            new UpdateProjectDocDepositoryHDROutput_List
                                {
                                STATUS = "Ok",
                                MESSAGE = "Get data successfully!!!",
                                DATA= GetProjectDepst
                                }
                        };
                        return successsResult;
                    }
                    else
                    {
                        var errorResult = new List<UpdateProjectDocDepositoryHDROutput_List>
                                {
                                    new UpdateProjectDocDepositoryHDROutput_List
                                    {
                                        STATUS = "Ok",
                                        MESSAGE = "Data not found",
                                        DATA = null
                                    }
                                };
                        return errorResult;
                    }
                }
            }
            catch (Exception ex)
            {
                var errorResult = new List<UpdateProjectDocDepositoryHDROutput_List>
                    {
                        new UpdateProjectDocDepositoryHDROutput_List
                        {
                            STATUS = "Error",
                            MESSAGE = ex.Message,
                            DATA = null
                        }
                    };
                return errorResult;
            }
        }

        public async Task<ActionResult<IEnumerable<UpdateProjectDocDepositoryHDROutput_List_NT>>> GetAllProjectDocDeositoryAsyncNT(ProjectDocDepositoryInputNT projectDocDepositoryInput)
        {
            DateTime dateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
            IDbTransaction transaction = null;
            bool transactionCompleted = false;  // Track the transaction state
            bool flagCount = false;
            try
            {
                using (IDbConnection db = _dapperDbConnection.CreateConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@MKEY", projectDocDepositoryInput.MKEY);
                    parmeters.Add("@USER_ID", projectDocDepositoryInput.USER_ID);
                    parmeters.Add("@API_NAME", "GetAllProjDocDepsitory");
                    parmeters.Add("@API_METHOD", "Get");
                    parmeters.Add("@Session_User_Id", projectDocDepositoryInput.Session_User_Id);
                    parmeters.Add("@Business_Group_Id", projectDocDepositoryInput.Business_Group_Id);

                    var GetProjectDepst = await db.QueryAsync<UpdateProjectDocDepositoryNT>("SP_GET_PROJECT_DOC_DEPOSITORY_NT", parmeters, commandType: CommandType.StoredProcedure);

                    foreach (var ProjectDepst in GetProjectDepst)
                    {

                        if (ProjectDepst.MKEY < 1)
                        {
                            var errorResult = new List<UpdateProjectDocDepositoryHDROutput_List_NT>
                                {
                                    new UpdateProjectDocDepositoryHDROutput_List_NT
                                    {
                                        STATUS = "Ok",
                                        MESSAGE = "Data not found",
                                        DATA = null
                                    }
                                };
                            return errorResult;
                        }
                        flagCount = true;
                        var parametersDepost = new DynamicParameters();
                        parametersDepost.Add("@PROJECT_DOC_MKEY", ProjectDepst.MKEY);
                        parametersDepost.Add("@CREATED_BY", projectDocDepositoryInput.USER_ID);
                        parametersDepost.Add("@APINAME", "CreateProjectDocDeositoryAsync");
                        parametersDepost.Add("@API_METHOD", "Create");
                        var GetProjectDocFile = await db.QueryAsync<DocFileUploadNT>("SP_GET_PROJECT_DOC_DEPOSITORY_ATTACHMENT_NT", parametersDepost, commandType: CommandType.StoredProcedure, transaction: transaction);

                        ProjectDepst.PROJECT_DOC_FILES = GetProjectDocFile;
                    }

                    if (flagCount = true)
                    {
                        var successsResult = new List<UpdateProjectDocDepositoryHDROutput_List_NT>
                            {
                            new UpdateProjectDocDepositoryHDROutput_List_NT
                                {
                                STATUS = "Ok",
                                MESSAGE = "Get data successfully!!!",
                                DATA= GetProjectDepst
                                }
                        };
                        return successsResult;
                    }
                    else
                    {
                        var errorResult = new List<UpdateProjectDocDepositoryHDROutput_List_NT>
                                {
                                    new UpdateProjectDocDepositoryHDROutput_List_NT
                                    {
                                        STATUS = "Ok",
                                        MESSAGE = "Data not found",
                                        DATA = null
                                    }
                                };
                        return errorResult;
                    }
                }
            }
            catch (Exception ex)
            {
                var errorResult = new List<UpdateProjectDocDepositoryHDROutput_List_NT>
                    {
                        new UpdateProjectDocDepositoryHDROutput_List_NT
                        {
                            STATUS = "Error",
                            MESSAGE = ex.Message,
                            DATA = null
                        }
                    };
                return errorResult;
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
                    parmeters.Add("@USER_ID", ATTRIBUTE1);
                    parmeters.Add("@API_NAME", ATTRIBUTE2);
                    parmeters.Add("@API_METHOD", ATTRIBUTE3);
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
                    parameters.Add("@MKEY", pROJECT_DOC_DEPOSITORY_HDR.MKEY);
                    parameters.Add("@BUILDING_TYPE", pROJECT_DOC_DEPOSITORY_HDR.BUILDING_MKEY);
                    parameters.Add("@PROPERTY_TYPE", pROJECT_DOC_DEPOSITORY_HDR.PROPERTY_MKEY);
                    parameters.Add("@DOC_MKEY", pROJECT_DOC_DEPOSITORY_HDR.DOC_MKEY);
                    parameters.Add("@DOC_NUMBER", pROJECT_DOC_DEPOSITORY_HDR.DOC_NUMBER);
                    parameters.Add("@DOC_DATE", pROJECT_DOC_DEPOSITORY_HDR.DOC_DATE);
                    parameters.Add("@VALIDITY_DATE", pROJECT_DOC_DEPOSITORY_HDR.VALIDITY_DATE);
                    parameters.Add("@API_NAME", "CreateProjectDocDeositoryAsync");
                    parameters.Add("@API_METHOD", "Create");
                    parameters.Add("@CREATED_BY", pROJECT_DOC_DEPOSITORY_HDR.CREATED_BY);
                    parameters.Add("@DELETE_FLAG", pROJECT_DOC_DEPOSITORY_HDR.DELETE_FLAG);

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
                        if (GetCount == null || GetCount.MKEY == 0)
                        {
                            RollbackTransaction(transaction);
                            return GenerateErrorResponse("An error occurred");
                        }
                    }

                    try
                    {
                        int srNo = 0;
                        string filePathOpen = string.Empty;

                        if (pROJECT_DOC_DEPOSITORY_HDR.PROJECT_DOC_FILES != null)
                        {
                            var parametersDelete = new DynamicParameters();
                            parametersDelete.Add("@PROJECT_DOC_MKEY", pROJECT_DOC_DEPOSITORY_HDR.MKEY);
                            parametersDelete.Add("@CREATED_BY", pROJECT_DOC_DEPOSITORY_HDR.CREATED_BY);
                            parametersDelete.Add("@APINAME", "CreateProjectDocDeositoryAsync");
                            parametersDelete.Add("@APIMETHOD", "Update");

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

                                    var InsertProjectDocFile = await db.QueryAsync<DocFileUploadOutPut>("SP_INSERT_PROJECT_DOC_DEPOSITORY_ATTACHMENT", parametersFiles, commandType: CommandType.StoredProcedure, transaction: transaction);

                                    if (InsertProjectDocFile == null)
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
                        RollbackTransaction(transaction);
                        return GenerateErrorResponse(ex.Message);
                    }
                    if (pROJECT_DOC_DEPOSITORY_HDR.PROJECT_DOC_FILES != null)
                    {
                        var parametersDepost = new DynamicParameters();
                        parametersDepost.Add("@PROJECT_DOC_MKEY", pROJECT_DOC_DEPOSITORY_HDR.MKEY);
                        parametersDepost.Add("@CREATED_BY", pROJECT_DOC_DEPOSITORY_HDR.CREATED_BY);
                        parametersDepost.Add("@APINAME", "CreateProjectDocDeositoryAsync");
                        parametersDepost.Add("@API_METHOD", "Create");
                        var GetProjectDocFile = await db.QueryAsync<DocFileUploadOutPut>("SP_GET_PROJECT_DOC_DEPOSITORY_ATTACHMENT", parametersDepost, commandType: CommandType.StoredProcedure, transaction: transaction);

                        foreach (var AddAttachment in GetProjectDocDesp)
                        {
                            AddAttachment.PROJECT_DOC_FILES = GetProjectDocFile;
                        }
                    }

                    var sqlTransaction = (SqlTransaction)transaction;
                    await sqlTransaction.CommitAsync();
                    transactionCompleted = true;

                    return new List<UpdateProjectDocDepositoryHDROutput_List>
                    {
                        new UpdateProjectDocDepositoryHDROutput_List
                        {
                            STATUS = "Ok",
                            MESSAGE = "Successfully Inserted",
                            DATA = GetProjectDocDesp
                        }
                    };
                }

                RollbackTransaction(transaction);
                return GenerateErrorResponse("No Project Doc Depository data found or processed.");
                // Ensure that this method always returns a value
                //return new List<UpdateProjectDocDepositoryHDROutput_List>
                //    {
                //        new UpdateProjectDocDepositoryHDROutput_List
                //        {
                //            STATUS = "Error",
                //            MESSAGE = "No Project Doc Depository data found or processed.",
                //            DATA = null
                //        }
                //    };
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

        public async Task<ActionResult<IEnumerable<UpdateProjectDocDepositoryHDROutput_List_NT>>> CreateProjectDocDeositoryAsyncNT(PROJECT_DOC_DEPOSITORY_HDR_NT pROJECT_DOC_DEPOSITORY_HDR)
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
                    parameters.Add("@MKEY", pROJECT_DOC_DEPOSITORY_HDR.MKEY);
                    parameters.Add("@BUILDING_TYPE", pROJECT_DOC_DEPOSITORY_HDR.BUILDING_MKEY);
                    parameters.Add("@PROPERTY_TYPE", pROJECT_DOC_DEPOSITORY_HDR.PROPERTY_MKEY);
                    parameters.Add("@DOC_MKEY", pROJECT_DOC_DEPOSITORY_HDR.DOC_MKEY);
                    parameters.Add("@DOC_NUMBER", pROJECT_DOC_DEPOSITORY_HDR.DOC_NUMBER);
                    parameters.Add("@DOC_DATE", pROJECT_DOC_DEPOSITORY_HDR.DOC_DATE);
                    parameters.Add("@VALIDITY_DATE", pROJECT_DOC_DEPOSITORY_HDR.VALIDITY_DATE);
                    parameters.Add("@API_NAME", "CreateProjectDocDeositoryAsync");
                    parameters.Add("@API_METHOD", "Create");
                    parameters.Add("@CREATED_BY", pROJECT_DOC_DEPOSITORY_HDR.CREATED_BY);
                    parameters.Add("@DELETE_FLAG", pROJECT_DOC_DEPOSITORY_HDR.DELETE_FLAG);

                    var GetProjectDocDesp = await db.QueryAsync<UpdateProjectDocDepositoryNT>("SP_INSERT_PROJECT_DOCUMENT_DEPOSITORY", parameters, commandType: CommandType.StoredProcedure, transaction: transaction);

                    if (GetProjectDocDesp == null)
                    {
                        // Handle other unexpected exceptions
                        RollbackTransaction(transaction);
                        return GenerateErrorResponseNT("An error occurred");
                    }

                    foreach (var GetCount in GetProjectDocDesp)
                    {
                        pROJECT_DOC_DEPOSITORY_HDR.MKEY = GetCount.MKEY;
                        if (GetCount == null || GetCount.MKEY == 0)
                        {
                            RollbackTransaction(transaction);
                            return GenerateErrorResponseNT("An error occurred");
                        }
                    }

                    try
                    {
                        int srNo = 0;
                        string filePathOpen = string.Empty;

                        if (pROJECT_DOC_DEPOSITORY_HDR.PROJECT_DOC_FILES != null)
                        {
                            var parametersDelete = new DynamicParameters();
                            parametersDelete.Add("@PROJECT_DOC_MKEY", pROJECT_DOC_DEPOSITORY_HDR.MKEY);
                            parametersDelete.Add("@CREATED_BY", pROJECT_DOC_DEPOSITORY_HDR.CREATED_BY);
                            parametersDelete.Add("@APINAME", "CreateProjectDocDeositoryAsync");
                            parametersDelete.Add("@APIMETHOD", "Update");

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

                                    var InsertProjectDocFile = await db.QueryAsync<DocFileUploadOutPut>("SP_INSERT_PROJECT_DOC_DEPOSITORY_ATTACHMENT", parametersFiles, commandType: CommandType.StoredProcedure, transaction: transaction);

                                    if (InsertProjectDocFile == null)
                                    {
                                        // Handle other unexpected exceptions
                                        RollbackTransaction(transaction);
                                        return GenerateErrorResponseNT("An error occurred");
                                    }
                                }
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        RollbackTransaction(transaction);
                        return GenerateErrorResponseNT(ex.Message);
                    }
                    if (pROJECT_DOC_DEPOSITORY_HDR.PROJECT_DOC_FILES != null)
                    {
                        var parametersDepost = new DynamicParameters();
                        parametersDepost.Add("@PROJECT_DOC_MKEY", pROJECT_DOC_DEPOSITORY_HDR.MKEY);
                        parametersDepost.Add("@CREATED_BY", pROJECT_DOC_DEPOSITORY_HDR.CREATED_BY);
                        parametersDepost.Add("@APINAME", "CreateProjectDocDeositoryAsync");
                        parametersDepost.Add("@API_METHOD", "Create");
                        var GetProjectDocFile = await db.QueryAsync<DocFileUploadNT>("SP_GET_PROJECT_DOC_DEPOSITORY_ATTACHMENT", parametersDepost, commandType: CommandType.StoredProcedure, transaction: transaction);

                        foreach (var AddAttachment in GetProjectDocDesp)
                        {
                            AddAttachment.PROJECT_DOC_FILES = GetProjectDocFile;
                        }
                    }

                    var sqlTransaction = (SqlTransaction)transaction;
                    await sqlTransaction.CommitAsync();
                    transactionCompleted = true;

                    return new List<UpdateProjectDocDepositoryHDROutput_List_NT>
                    {
                        new UpdateProjectDocDepositoryHDROutput_List_NT
                        {
                            STATUS = "Ok",
                            MESSAGE = "Successfully Inserted",
                            DATA = GetProjectDocDesp
                        }
                    };
                }

                RollbackTransaction(transaction);
                return GenerateErrorResponseNT("No Project Doc Depository data found or processed.");
                // Ensure that this method always returns a value
                //return new List<UpdateProjectDocDepositoryHDROutput_List>
                //    {
                //        new UpdateProjectDocDepositoryHDROutput_List
                //        {
                //            STATUS = "Error",
                //            MESSAGE = "No Project Doc Depository data found or processed.",
                //            DATA = null
                //        }
                //    };
            }
            catch (Exception ex)
            {
                if (transaction != null && !transactionCompleted)
                {
                    RollbackTransaction(transaction);
                }

                return GenerateErrorResponseNT(ex.Message);
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

        private List<UpdateProjectDocDepositoryHDROutput_List_NT> GenerateErrorResponseNT(string message)
        {
            return new List<UpdateProjectDocDepositoryHDROutput_List_NT>
            {
                new UpdateProjectDocDepositoryHDROutput_List_NT
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
        public async Task<ActionResult<IEnumerable<ProjectDocOutput_NT>>> GetDocumentDetailsAsyncNT(ProjectDocInput_NT projectDocInput_NT)
        {
            try
            {
                using (IDbConnection db = _dapperDbConnection.CreateConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@MKEY", projectDocInput_NT.MKey);
                    parmeters.Add("@Session_User_Id", projectDocInput_NT.Session_User_Id);
                    parmeters.Add("@Business_Group_Id", projectDocInput_NT.Business_Group_Id);

                    var ProjDoc_Desp = await db.QueryAsync<ProjectDoc_NT>("GET_SP_DOCUMENT_TEMPLATE_DETAILS_NT", parmeters, commandType: CommandType.StoredProcedure);
                    if (ProjDoc_Desp.Any())
                    {
                        return new List<ProjectDocOutput_NT>
                        {
                            new ProjectDocOutput_NT
                            {
                                STATUS = "Ok",
                                MESSAGE = "Successfully Inserted",
                                DATA = ProjDoc_Desp
                            }
                        };
                    }
                    else
                    {
                        return new List<ProjectDocOutput_NT>
                        {
                            new ProjectDocOutput_NT
                            {
                                STATUS = "Ok",
                                MESSAGE = "Data not found",
                                DATA = null
                            }
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                return new List<ProjectDocOutput_NT>
                {
                    new ProjectDocOutput_NT
                    {
                        STATUS = "Error",
                        MESSAGE = ex.Message,
                        DATA = null
                    }
                };
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
        //public async Task<ActionResult<IEnumerable<UpdateProjectDocDepositoryHDROutput_List>>> UpdateProjectDepositoryDocumentAsync(UpdateProjectDocDepositoryHDRInput updateProjectDocDepositoryHDRInput)
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

        //            var parameters = new DynamicParameters();
        //            parameters.Add("@MKEY", updateProjectDocDepositoryHDRInput.MKEY);
        //            parameters.Add("@BUILDING_MKEY", updateProjectDocDepositoryHDRInput.BUILDING_MKEY);
        //            parameters.Add("@PROPERTY_MKEY", updateProjectDocDepositoryHDRInput.PROPERTY_MKEY);
        //            parameters.Add("@DOC_NAME", updateProjectDocDepositoryHDRInput.DOC_NAME);
        //            parameters.Add("@DOC_NUMBER", updateProjectDocDepositoryHDRInput.DOC_NUMBER);
        //            parameters.Add("@DOC_DATE", updateProjectDocDepositoryHDRInput.DOC_DATE);
        //            //parameters.Add("@DOC_ATTACHMENT", updateProjectDocDepositoryHDRInput.DOC_ATTACHMENT);
        //            parameters.Add("@VALIDITY_DATE", updateProjectDocDepositoryHDRInput.VALIDITY_DATE);
        //            parameters.Add("@CREATED_BY", updateProjectDocDepositoryHDRInput.CREATED_BY);
        //            parameters.Add("@DELETE_FLAG", updateProjectDocDepositoryHDRInput.DELETE_FLAG);
        //            var ProjectDocDsp = await db.QueryAsync<UpdateProjectDocDepositoryHDROutput>("SP_UPDATE_PROJECT_DOCUMENT_DEPOSITORY", parameters, commandType: CommandType.StoredProcedure, transaction: transaction);

        //            if (ProjectDocDsp == null)
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
        //                var errorResult = new List<UpdateProjectDocDepositoryHDROutput_List>
        //                    {
        //                        new UpdateProjectDocDepositoryHDROutput_List
        //                        {
        //                           STATUS = "Error",
        //                            MESSAGE= "An error occurd",
        //                            DATA= null
        //                        }
        //                    };
        //                return errorResult;
        //            }

        //            //try
        //            //{
        //            //    int srNo = 0;
        //            //    string filePathOpen = string.Empty;

        //            //    if (pROJECT_DOC_DEPOSITORY_HDR.PROJECT_DOC_FILES != null)
        //            //    {
        //            //        var parametersDelete = new DynamicParameters();
        //            //        parametersDelete.Add("@PROJECT_DOC_MKEY", pROJECT_DOC_DEPOSITORY_HDR.MKEY);
        //            //        parametersDelete.Add("@CREATED_BY", pROJECT_DOC_DEPOSITORY_HDR.CREATED_BY);
        //            //        parametersDelete.Add("@APINAME", "CreateProjectDocDeositoryAsync");
        //            //        parametersDelete.Add("@API_METHOD", "Update");

        //            //        var DeleteProjectDocFile = await db.QueryAsync<dynamic>("SP_UPDATE_PROJECT_DOC_DEPOSITORY_ATTACHMENT", parametersDelete, commandType: CommandType.StoredProcedure, transaction: transaction);

        //            //        foreach (var ProjectDocFile in pROJECT_DOC_DEPOSITORY_HDR.PROJECT_DOC_FILES)
        //            //        {
        //            //            if (ProjectDocFile.Length > 0)
        //            //            {
        //            //                srNo = srNo + 1;
        //            //                string FilePath = _fileSettings.FilePath;
        //            //                if (!Directory.Exists(FilePath + "\\Attachments\\" + "Document Depository\\" + pROJECT_DOC_DEPOSITORY_HDR.MKEY))
        //            //                {
        //            //                    Directory.CreateDirectory(FilePath + "\\Attachments\\" + "Document Depository\\" + pROJECT_DOC_DEPOSITORY_HDR.MKEY);
        //            //                }
        //            //                using (FileStream filestream = System.IO.File.Create(FilePath + "\\Attachments\\" + "Document Depository\\"
        //            //                    + pROJECT_DOC_DEPOSITORY_HDR.MKEY + "\\" + DateTime.Now.Day + "_" + DateTime.Now.ToShortTimeString().Replace(":", "_") + "_" + ProjectDocFile.FileName))
        //            //                {
        //            //                    ProjectDocFile.CopyTo(filestream);
        //            //                    filestream.Flush();
        //            //                }

        //            //                filePathOpen = "\\Attachments\\" + "Document Depository\\" + pROJECT_DOC_DEPOSITORY_HDR.MKEY + "\\"
        //            //                    + DateTime.Now.Day + "_" + DateTime.Now.ToShortTimeString().Replace(":", "_") + "_"
        //            //                    + ProjectDocFile.FileName;

        //            //                var parametersFiles = new DynamicParameters();
        //            //                parametersFiles.Add("@PROJECT_DOC_MKEY", pROJECT_DOC_DEPOSITORY_HDR.MKEY);
        //            //                parametersFiles.Add("@FILE_NAME", ProjectDocFile.FileName);
        //            //                parametersFiles.Add("@FILE_PATH", filePathOpen);
        //            //                parametersFiles.Add("@CREATED_BY", pROJECT_DOC_DEPOSITORY_HDR.CREATED_BY);
        //            //                parametersFiles.Add("@DELETE_FLAG", "N");
        //            //                parametersFiles.Add("@APINAME", "CreateProjectDocDeositoryAsync");
        //            //                parametersFiles.Add("@API_METHOD", "Create");

        //            //                var InsertProjectDocFile = await db.QueryAsync<DocFileUploadOutPut>("SP_INSERT_PROJECT_DOC_DEPOSITORY_ATTACHMENT", parametersFiles, commandType: CommandType.StoredProcedure, transaction: transaction);

        //            //                if (InsertProjectDocFile == null)
        //            //                {
        //            //                    // Handle other unexpected exceptions
        //            //                    RollbackTransaction(transaction);
        //            //                    return GenerateErrorResponse("An error occurred");
        //            //                }
        //            //            }
        //            //        }
        //            //    }

        //            //}
        //            //catch (Exception ex)
        //            //{
        //            //    RollbackTransaction(transaction);
        //            //    return GenerateErrorResponse(ex.Message);
        //            //}
        //            //if (pROJECT_DOC_DEPOSITORY_HDR.PROJECT_DOC_FILES != null)
        //            //{
        //            //    var parametersDepost = new DynamicParameters();
        //            //    parametersDepost.Add("@PROJECT_DOC_MKEY", pROJECT_DOC_DEPOSITORY_HDR.MKEY);
        //            //    parametersDepost.Add("@CREATED_BY", pROJECT_DOC_DEPOSITORY_HDR.CREATED_BY);
        //            //    parametersDepost.Add("@APINAME", "CreateProjectDocDeositoryAsync");
        //            //    parametersDepost.Add("@API_METHOD", "Create");
        //            //    var GetProjectDocFile = await db.QueryAsync<DocFileUploadOutPut>("SP_GET_PROJECT_DOC_DEPOSITORY_ATTACHMENT", parametersDepost, commandType: CommandType.StoredProcedure, transaction: transaction);

        //            //    foreach (var AddAttachment in GetProjectDocDesp)
        //            //    {
        //            //        AddAttachment.PROJECT_DOC_FILES = GetProjectDocFile;
        //            //    }
        //            //}

        //            var sqlTransaction = (SqlTransaction)transaction;
        //            await sqlTransaction.CommitAsync();
        //            transactionCompleted = true;
        //            var successsResult = new List<UpdateProjectDocDepositoryHDROutput_List>
        //            {
        //                new UpdateProjectDocDepositoryHDROutput_List
        //                {
        //                    STATUS = "Ok",
        //                    MESSAGE = "Message",
        //                    DATA= ProjectDocDsp
        //                }
        //            };
        //            return successsResult;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        var errorResult = new List<UpdateProjectDocDepositoryHDROutput_List>
        //            {
        //                new UpdateProjectDocDepositoryHDROutput_List
        //                {
        //                   STATUS = "Error",
        //                    MESSAGE= ex.Message,
        //                    DATA= null
        //                }
        //            };
        //        return errorResult;
        //    }
        //}
    }
}
