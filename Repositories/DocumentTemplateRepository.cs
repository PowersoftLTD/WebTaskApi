﻿using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Reflection.Metadata;
using TaskManagement.API.Interfaces;
using TaskManagement.API.Model;

namespace TaskManagement.API.Repositories
{
    public class DocumentTemplateRepository : IDoc_Temp
    {
        private static TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        public IDapperDbConnection _dapperDbConnection;
        public DocumentTemplateRepository(IDapperDbConnection dapperDbConnection)
        {
            _dapperDbConnection = dapperDbConnection;
        }
        public async Task<IEnumerable<DOC_TEMPLATE_HDR>> GetAllDocumentTempAsync(int LoggedIN)
        {
            using (IDbConnection db = _dapperDbConnection.CreateConnection())
            {
                var parmeters = new DynamicParameters();
                parmeters.Add("@MKEY", null);
                parmeters.Add("@ATTRIBUT1", LoggedIN);
                parmeters.Add("@ATTRIBUT2", null);
                parmeters.Add("@ATTRIBUT3", null);
                return await db.QueryAsync<DOC_TEMPLATE_HDR>("SP_GET_DOCUMENT_TEMPLATES", parmeters, commandType: CommandType.StoredProcedure);
            }
        }
        public async Task<DOC_TEMPLATE_HDR> GetDocumentTempByIdAsync(int id, int? LoggedIN)
        {
            try
            {
                using (IDbConnection db = _dapperDbConnection.CreateConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@MKEY", id);
                    parmeters.Add("@ATTRIBUT1", LoggedIN);
                    parmeters.Add("@ATTRIBUT2", null);
                    parmeters.Add("@ATTRIBUT3", null);
                    return await db.QueryFirstOrDefaultAsync<DOC_TEMPLATE_HDR>("SP_GET_DOCUMENT_TEMPLATES", parmeters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        public async Task<ActionResult<IEnumerable<DOC_TEMPLATE_HDR_OUTPUT_NT>>> GetDocumentTempByIdAsyncNT(DocTemplateGetInputNT docTemplateGetInputNT)
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

                    //transaction = db.BeginTransaction();
                    //transactionCompleted = false;  // Reset transaction state

                    var parmeters = new DynamicParameters();

                    parmeters.Add("@MKEY", docTemplateGetInputNT.id);
                    parmeters.Add("@ATTRIBUT1", docTemplateGetInputNT.LoggedIN);
                    parmeters.Add("@ATTRIBUT2", null);
                    parmeters.Add("@ATTRIBUT3", null);
                    parmeters.Add("@Session_User_Id", docTemplateGetInputNT.Session_User_Id);
                    parmeters.Add("@Business_Group_Id", docTemplateGetInputNT.Business_Group_Id);

                    var taskStatusDistributonNTs = await db.QueryAsync<DOC_TEMPLATE_HDR_NT>("SP_GET_DOCUMENT_TEMPLATES_NT", parmeters, commandType: CommandType.StoredProcedure);

                    var successsResult = new List<DOC_TEMPLATE_HDR_OUTPUT_NT>
                    {
                        new DOC_TEMPLATE_HDR_OUTPUT_NT
                        {
                            STATUS = "Ok",
                            MESSAGE = "Get data successfully!!!",
                            Data = taskStatusDistributonNTs
                        }
                    };
                    return successsResult;
                }
            }
            catch (Exception ex)
            {
                // Log the generic error
                var errorResult = new List<DOC_TEMPLATE_HDR_OUTPUT_NT>
                        {
                            new DOC_TEMPLATE_HDR_OUTPUT_NT
                            {
                                STATUS = "Error",
                                MESSAGE = ex.Message,
                                Data = null
                            }
                        };
                return errorResult;
            }
        }
        public async Task<DOC_TEMPLATE_HDR> CreateDocumentTemplateAsync(DOC_TEMPLATE_HDR dOC_TEMPLATE_HDR)
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

                    // Start the transaction
                    transaction = db.BeginTransaction();
                    transactionCompleted = false;  // Reset transaction state

                    var parameters = new DynamicParameters();
                    parameters.Add("@DOC_CATEGORY", dOC_TEMPLATE_HDR.DOC_CATEGORY);
                    parameters.Add("@DOC_NAME", dOC_TEMPLATE_HDR.DOC_NAME);
                    parameters.Add("@DOC_ABBR", dOC_TEMPLATE_HDR.DOC_ABBR);
                    parameters.Add("@DOC_NUM_FIELD_NAME", dOC_TEMPLATE_HDR.DOC_NUM_FIELD_NAME);
                    parameters.Add("@DOC_NUM_DATE_NAME", dOC_TEMPLATE_HDR.DOC_NUM_DATE_NAME);
                    parameters.Add("@DOC_NUM_APP_FLAG", dOC_TEMPLATE_HDR.DOC_NUM_APP_FLAG);
                    parameters.Add("@DOC_NUM_VALID_FLAG", dOC_TEMPLATE_HDR.DOC_NUM_VALID_FLAG);
                    parameters.Add("@DOC_NUM_DATE_APP_FLAG", dOC_TEMPLATE_HDR.DOC_NUM_DATE_APP_FLAG);
                    parameters.Add("@DOC_ATTACH_APP_FLAG", dOC_TEMPLATE_HDR.DOC_ATTACH_APP_FLAG);
                    parameters.Add("@COMPANY_ID", dOC_TEMPLATE_HDR.COMPANY_ID);
                    parameters.Add("@CREATED_BY", dOC_TEMPLATE_HDR.CREATED_BY);
                    parameters.Add("@ATTRIBUTE1", dOC_TEMPLATE_HDR.ATTRIBUTE1);
                    parameters.Add("@ATTRIBUTE2", dOC_TEMPLATE_HDR.ATTRIBUTE2);

                    // Ensure the transaction is passed to the query
                    dOC_TEMPLATE_HDR = await db.QueryFirstOrDefaultAsync<DOC_TEMPLATE_HDR>("SP_INSERT_DOCUMENT_TEMPLATES",
                        parameters,
                        transaction: transaction, 
                        commandType: CommandType.StoredProcedure
                    );
                    
                    if (dOC_TEMPLATE_HDR == null)
                    {
                        // Handle error: rollback if necessary
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

                        var TemplateError = new DOC_TEMPLATE_HDR();
                        TemplateError.Status = "Error";
                        TemplateError.Message = "Error Occurred";
                        return TemplateError;
                    }

                    // Commit the transaction if everything succeeded
                    var sqlTransaction = (SqlTransaction)transaction;
                    await sqlTransaction.CommitAsync();
                    transactionCompleted = true;

                    dOC_TEMPLATE_HDR.Status = "Ok";
                    dOC_TEMPLATE_HDR.Message = "Data saved successfully";
                    return dOC_TEMPLATE_HDR;
                }
            }
            catch (Exception ex)
            {
                // Handle the exception and rollback the transaction if necessary
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

                var doc_insert = new DOC_TEMPLATE_HDR();
                doc_insert.Status = "Error";
                doc_insert.Message = ex.Message;
                return doc_insert;
            }
        }
        public async Task<bool> UpdateDocumentTemplateAsync(DOC_TEMPLATE_HDR dOC_TEMPLATE_HDR)
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

                    var parameters = new DynamicParameters();
                    parameters.Add("@MKEY", dOC_TEMPLATE_HDR.MKEY);
                    parameters.Add("@DOC_CATEGORY", dOC_TEMPLATE_HDR.DOC_CATEGORY);
                    parameters.Add("@DOC_NAME", dOC_TEMPLATE_HDR.DOC_NAME);
                    parameters.Add("@DOC_ABBR", dOC_TEMPLATE_HDR.DOC_ABBR);
                    parameters.Add("@DOC_NUM_FIELD_NAME", dOC_TEMPLATE_HDR.DOC_NUM_FIELD_NAME);
                    parameters.Add("@DOC_NUM_DATE_NAME", dOC_TEMPLATE_HDR.DOC_NUM_DATE_NAME);
                    parameters.Add("@DOC_NUM_APP_FLAG", dOC_TEMPLATE_HDR.DOC_NUM_APP_FLAG);
                    parameters.Add("@DOC_NUM_VALID_FLAG", dOC_TEMPLATE_HDR.DOC_NUM_VALID_FLAG);
                    parameters.Add("@DOC_NUM_DATE_APP_FLAG", dOC_TEMPLATE_HDR.DOC_NUM_DATE_APP_FLAG);
                    parameters.Add("@DOC_ATTACH_APP_FLAG", dOC_TEMPLATE_HDR.DOC_ATTACH_APP_FLAG);
                    parameters.Add("@LAST_UPDATED_BY", dOC_TEMPLATE_HDR.CREATED_BY);
                    parameters.Add("@DELETE_FLAG", "N");
                    parameters.Add("@ATTRIBUTE1", dOC_TEMPLATE_HDR.ATTRIBUTE1);
                    parameters.Add("@ATTRIBUTE2", dOC_TEMPLATE_HDR.ATTRIBUTE2);
                    var doc_update = await db.ExecuteAsync("SP_UPDATE_DOCUMENT_TEMPLATES", parameters, commandType: CommandType.StoredProcedure, transaction: transaction);

                    if (doc_update == null)
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

                        var TemplateError = new DOC_TEMPLATE_HDR();
                        TemplateError.Status = "Error";
                        TemplateError.Message = "Error Occurd";
                        return false;
                    }
                    var sqlTransaction = (SqlTransaction)transaction;
                    await sqlTransaction.CommitAsync();
                    transactionCompleted = true;

                    return true;
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
                        Console.WriteLine($"Rollback failed: {rollbackEx.Message}");
                        //var TranError = new APPROVAL_TASK_INITIATION();
                        //TranError.ResponseStatus = "Error";
                        //TranError.Message = ex.Message;
                        //return TranError;
                    }
                }
                var doc_update = new DOC_TEMPLATE_HDR();
                doc_update.Status = "Error";
                doc_update.Message = ex.Message;
                return false;
            }
        }
        public async Task<bool> DeleteDocumentTemplateAsync(int id, int LastUpatedBy)
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

                    var parameters = new DynamicParameters();
                    parameters.Add("@MKEY", id);
                    parameters.Add("@LAST_UPDATED_BY", LastUpatedBy);
                    var doc_delete = await db.ExecuteAsync("SP_DELETE_DOCUMENT_TEMPLATES", parameters, commandType: CommandType.StoredProcedure, transaction: transaction);

                    if (doc_delete == null)
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

                        var TemplateError = new DOC_TEMPLATE_HDR();
                        TemplateError.Status = "Error";
                        TemplateError.Message = "Error Occurd";
                        return false;
                    }
                    var sqlTransaction = (SqlTransaction)transaction;
                    await sqlTransaction.CommitAsync();
                    transactionCompleted = true;
                    return true;
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
                        Console.WriteLine($"Rollback failed: {rollbackEx.Message}");
                        //var TranError = new APPROVAL_TASK_INITIATION();
                        //TranError.ResponseStatus = "Error";
                        //TranError.Message = ex.Message;
                        //return TranError;
                    }
                }
                var doc_update = new DOC_TEMPLATE_HDR();
                doc_update.Status = "Error";
                doc_update.Message = ex.Message;
                return false;
            }
        }
        public async Task<DocCategoryOutPut_List> InsertDocumentCategory(DocCategoryInput docCategoryInput)
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

                    var CheckDocCategory = await db.QueryFirstOrDefaultAsync<int>("SELECT count(*) TOTAL_COUNT FROM TYPE_MST " +
                        " WHERE TYPE_CODE = 'DOC_CATEGORY' AND DELETE_FLAG = 'N' AND LOWER(TYPE_DESC) = LOWER(@TYPE_DESC) GROUP BY [TYPE_DESC]; ",
                        new { TYPE_DESC = docCategoryInput.DOC_CATEGORY }, transaction: transaction);

                    if (CheckDocCategory > 0)
                    {
                        var successsResult = new DocCategoryOutPut_List
                        {
                            Status = "Error",
                            Message = "Category already exists!!!",
                            Data = null
                        };
                        return successsResult;
                    }
                    else
                    {
                        var parmeters = new DynamicParameters();
                        parmeters.Add("@DOC_CATEGORY", docCategoryInput.DOC_CATEGORY);
                        parmeters.Add("@CREATED_BY", docCategoryInput.CREATED_BY);
                        parmeters.Add("@COMPANY_ID", docCategoryInput.COMPANY_ID);

                        var InsertDocCategory = await db.QueryAsync<V_Building_Classification>("SP_INSERT_DOC_CATEGORY", parmeters, commandType: CommandType.StoredProcedure, transaction: transaction);

                        var sqlTransaction = (SqlTransaction)transaction;
                        await sqlTransaction.CommitAsync();
                        transactionCompleted = true;

                        var successsResult = new DocCategoryOutPut_List
                        {
                            Status = "Ok",
                            Message = "Inserted Successfully",
                            Data = InsertDocCategory
                        };
                        return successsResult;
                    }

                }
            }
            catch (Exception ex)
            {
                var errorResult = new DocCategoryOutPut_List
                {
                    Status = "Error",
                    Message = ex.Message,
                    Data = null
                };
                return errorResult;
            }
        }
        public async Task<DocCategoryOutPut_List> InsertInstructionAsyn(InsertInstructionInput insertInstructionInput)
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

                    var CheckDocCategory = await db.QueryFirstOrDefaultAsync<int>("SELECT count(*) TOTAL_COUNT FROM TYPE_MST " +
                        " WHERE TYPE_CODE = 'INSTR' AND DELETE_FLAG = 'N' AND LOWER(TYPE_DESC) = LOWER(@TYPE_DESC) GROUP BY [TYPE_DESC]; ",
                        new { TYPE_DESC = insertInstructionInput.DOC_INSTR }, transaction: transaction);

                    if (CheckDocCategory > 0)
                    {
                        var successsResult = new DocCategoryOutPut_List
                        {
                            Status = "Error",
                            Message = "Category already exists!!!",
                            Data = null
                        };
                        return successsResult;
                    }
                    else
                    {
                        var parmeters = new DynamicParameters();
                        parmeters.Add("@DOC_INSTR", insertInstructionInput.DOC_INSTR);
                        parmeters.Add("@CREATED_BY", insertInstructionInput.CREATED_BY);
                        parmeters.Add("@COMPANY_ID", insertInstructionInput.COMPANY_ID);

                        var InsertDocCategory = await db.QueryAsync<V_Building_Classification>("SP_INSERT_INSTRUCTION",
                            parmeters, commandType: CommandType.StoredProcedure, transaction: transaction);

                        var sqlTransaction = (SqlTransaction)transaction;
                        await sqlTransaction.CommitAsync();
                        transactionCompleted = true;

                        var successsResult = new DocCategoryOutPut_List
                        {
                            Status = "Ok",
                            Message = "Inserted Successfully",
                            Data = InsertDocCategory
                        };
                        return successsResult;
                    }

                }
            }
            catch (Exception ex)
            {
                var errorResult = new DocCategoryOutPut_List
                {
                    Status = "Error",
                    Message = ex.Message,
                    Data = null
                };
                return errorResult;
            }
        }
        public async Task<ActionResult<IEnumerable<DocCategoryOutPutNT>>> InsertInstructionAsynNT(InsertInstructionInputNT insertInstructionInputNT)
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

                    var CheckDocCategory = await db.QueryFirstOrDefaultAsync<int>("SELECT count(*) TOTAL_COUNT FROM TYPE_MST " +
                        " WHERE TYPE_CODE = 'INSTR' AND DELETE_FLAG = 'N' AND LOWER(TYPE_DESC) = LOWER(@TYPE_DESC) GROUP BY [TYPE_DESC]; ",
                        new { TYPE_DESC = insertInstructionInputNT.DOC_INSTR }, transaction: transaction);

                    if (CheckDocCategory > 0)
                    {
                        var ErrorResult = new List<DocCategoryOutPutNT>
                            {
                                new DocCategoryOutPutNT
                                {
                                    Status = "Error",
                                    Message ="Category already exists!!!",
                                    Data = null
                                }
                            };
                        return ErrorResult;

                    }
                    else
                    {
                        var parmeters = new DynamicParameters();
                        parmeters.Add("@DOC_INSTR", insertInstructionInputNT.DOC_INSTR);
                        parmeters.Add("@CREATED_BY", insertInstructionInputNT.CREATED_BY);
                        parmeters.Add("@Session_User_Id", insertInstructionInputNT.Session_User_Id);
                        parmeters.Add("@Business_Group_Id", insertInstructionInputNT.Business_Group_Id);

                        var InsertDocCategory = await db.QueryAsync<GetTaskTypeOutPutNT>("SP_INSERT_INSTRUCTION_NT",
                            parmeters, commandType: CommandType.StoredProcedure, transaction: transaction);

                        var sqlTransaction = (SqlTransaction)transaction;
                        await sqlTransaction.CommitAsync();
                        transactionCompleted = true;
                        
                        var SuccessResult = new List<DocCategoryOutPutNT>
                            {
                                new DocCategoryOutPutNT
                                {
                                    Status = "Ok",
                                    Message ="Inserted Successfully!!!",
                                    Data = InsertDocCategory
                                }
                            };
                        return SuccessResult;
                    }
                }
            }
            catch (Exception ex)
            {
                var ErrorResult = new List<DocCategoryOutPutNT>
                {
                    new DocCategoryOutPutNT
                    {
                        Status = "Error",
                        Message = ex.Message,
                        Data = null
                    }
                };
                return ErrorResult;
            }
        }
        public async Task<DocCategoryOutPut_List> UpdateDocumentCategory(DocCategoryUpdateInput docCategoryUpdateInput)
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
                    parmeters.Add("@MKEY", docCategoryUpdateInput.MKEY);
                    parmeters.Add("@CREATED_BY", docCategoryUpdateInput.CREATED_BY);
                    parmeters.Add("@DOC_CATEGORY", docCategoryUpdateInput.DOC_CATEGORY);
                    parmeters.Add("@DELETE_FLAG", docCategoryUpdateInput.DELETE_FLAG);

                    var InsertDocCategory = await db.QueryAsync<V_Building_Classification>("SP_UPDATE_DOC_CATEGORY", parmeters, commandType: CommandType.StoredProcedure, transaction: transaction);

                    var sqlTransaction = (SqlTransaction)transaction;
                    await sqlTransaction.CommitAsync();
                    transactionCompleted = true;

                    var successsResult = new DocCategoryOutPut_List
                    {
                        Status = "Ok",
                        Message = "Update Successfully",
                        Data = InsertDocCategory
                    };
                    return successsResult;
                }
            }
            catch (Exception ex)
            {
                var errorResult = new DocCategoryOutPut_List
                {
                    Status = "Error",
                    Message = ex.Message,
                    Data = null
                };
                return errorResult;
            }
        }
        public async Task<DocCategoryOutPut_List> UpdateInstruction(UpdateInstructionInput updateInstructionInput)
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
                    parmeters.Add("@MKEY", updateInstructionInput.MKEY);
                    parmeters.Add("@CREATED_BY", updateInstructionInput.CREATED_BY);
                    parmeters.Add("@DOC_INSTR", updateInstructionInput.DOC_INSTR);
                    parmeters.Add("@DELETE_FLAG", updateInstructionInput.DELETE_FLAG);

                    var InsertDocCategory = await db.QueryAsync<V_Building_Classification>("SP_UPDATE_INSTRUCTION", parmeters, commandType: CommandType.StoredProcedure, transaction: transaction);

                    var sqlTransaction = (SqlTransaction)transaction;
                    await sqlTransaction.CommitAsync();
                    transactionCompleted = true;

                    var StatusResponse = InsertDocCategory
                                .Where(item => item.TYPE_DESC == "ERROR")
                                .Select(item => new { item.TYPE_DESC, item.TYPE_CODE })
                                .ToList();

                    if (StatusResponse.Count == 0)
                    {
                        var successsResult = new DocCategoryOutPut_List
                        {
                            Status = "Ok",
                            Message = "Update Successfully",
                            Data = InsertDocCategory
                        };
                        return successsResult;
                    }
                    else
                    {
                        //var ErrorMessage = InsertDocCategory.Select(X => X.TYPE_CODE.ToString()).ToString();
                        var successsResult = new DocCategoryOutPut_List
                        {
                            Status = "Error",
                            Message = StatusResponse[0].TYPE_CODE.ToString(),
                            Data = null
                        };
                        return successsResult;
                    }

                }
            }
            catch (Exception ex)
            {
                var errorResult = new DocCategoryOutPut_List
                {
                    Status = "Error",
                    Message = ex.Message,
                    Data = null
                };
                return errorResult;
            }
        }
        public async Task<ActionResult<IEnumerable<DOC_TEMPLATE_HDR_OUTPUT_NT>>> InsertUpdateDocTemplateAsyncNT(DOC_TEMPLATE_HDR_NT_INPUT dOC_TEMPLATE_HDR_NT_INPUT)
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
                    parameters.Add("@MKEY", dOC_TEMPLATE_HDR_NT_INPUT.MKEY);
                    parameters.Add("@DOC_CATEGORY", dOC_TEMPLATE_HDR_NT_INPUT.DOC_CATEGORY);
                    parameters.Add("@DOC_NAME", dOC_TEMPLATE_HDR_NT_INPUT.DOC_NAME);
                    parameters.Add("@DOC_ABBR", dOC_TEMPLATE_HDR_NT_INPUT.DOC_ABBR);
                    parameters.Add("@DOC_NUM_FIELD_NAME", dOC_TEMPLATE_HDR_NT_INPUT.DOC_NUM_FIELD_NAME);
                    parameters.Add("@DOC_NUM_DATE_NAME", dOC_TEMPLATE_HDR_NT_INPUT.DOC_NUM_DATE_NAME);
                    parameters.Add("@DOC_NUM_APP_FLAG", dOC_TEMPLATE_HDR_NT_INPUT.DOC_NUM_APP_FLAG);
                    parameters.Add("@DOC_NUM_VALID_FLAG", dOC_TEMPLATE_HDR_NT_INPUT.DOC_NUM_VALID_FLAG);
                    parameters.Add("@DOC_NUM_DATE_APP_FLAG", dOC_TEMPLATE_HDR_NT_INPUT.DOC_NUM_DATE_APP_FLAG);
                    parameters.Add("@DOC_ATTACH_APP_FLAG", dOC_TEMPLATE_HDR_NT_INPUT.DOC_ATTACH_APP_FLAG);
                    parameters.Add("@COMPANY_ID", dOC_TEMPLATE_HDR_NT_INPUT.COMPANY_ID);
                    parameters.Add("@CREATED_BY", dOC_TEMPLATE_HDR_NT_INPUT.CREATED_BY);
                    parameters.Add("@ATTRIBUTE1", dOC_TEMPLATE_HDR_NT_INPUT.ATTRIBUTE1);
                    parameters.Add("@ATTRIBUTE2", dOC_TEMPLATE_HDR_NT_INPUT.ATTRIBUTE2);
                    parameters.Add("@Delete_Flag", dOC_TEMPLATE_HDR_NT_INPUT.Delete_Flag);
                    parameters.Add("@Session_User_Id", dOC_TEMPLATE_HDR_NT_INPUT.Session_User_Id);
                    parameters.Add("@Business_Group_Id", dOC_TEMPLATE_HDR_NT_INPUT.Business_Group_Id);

                    // Ensure the transaction is passed to the query
                    var dOC_TEMPLATE = await db.QueryAsync<DOC_TEMPLATE_HDR_NT>("SP_INSERT_DOCUMENT_TEMPLATES_NT",
                        parameters,
                        transaction: transaction,
                        commandType: CommandType.StoredProcedure
                    );

                    if (dOC_TEMPLATE.Any())
                    {
                        var sqlTransaction = (SqlTransaction)transaction;
                        await sqlTransaction.CommitAsync();
                        transactionCompleted = true;

                        var successsResult = new List<DOC_TEMPLATE_HDR_OUTPUT_NT>
                            {
                            new DOC_TEMPLATE_HDR_OUTPUT_NT
                                {
                                STATUS = "Ok",
                                MESSAGE = "Inserted Successfully!!!",
                                Data= dOC_TEMPLATE
                                }
                        };
                        return successsResult;

                    }
                    else
                    {
                       

                        var successsResult = new List<DOC_TEMPLATE_HDR_OUTPUT_NT>
                            {
                            new DOC_TEMPLATE_HDR_OUTPUT_NT
                                {
                                STATUS = "Error",
                                MESSAGE = "Insertion failed!!!",
                                Data = null
                                }
                        };
                        return successsResult;
                    }
                }
            }
            catch (Exception ex)
            {
                var errorResult = new List<DOC_TEMPLATE_HDR_OUTPUT_NT>
                    {
                        new DOC_TEMPLATE_HDR_OUTPUT_NT
                        {
                            STATUS = "Error",
                            MESSAGE = ex.Message,
                            Data = null
                        }
                    };
                return errorResult;
            }
        }
        public async Task<ActionResult<IEnumerable<DocCategoryOutPutNT>>> DocTypeAsynNT(DocTypeInputNT docTypeInputNT)
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
                    parameters.Add("@DOC_CATEGORY", docTypeInputNT.DOC_INSTR);
                    parameters.Add("@CREATED_BY", docTypeInputNT.CREATED_BY);
                    parameters.Add("@Session_User_Id", docTypeInputNT.Session_User_Id);
                    parameters.Add("@Business_Group_Id", docTypeInputNT.Business_Group_Id);

                    // Ensure the transaction is passed to the query
                    var dOC_TEMPLATE = await db.QueryAsync<GetTaskTypeOutPutNT>("SP_INSERT_DOC_CATEGORY_NT",
                        parameters,
                        transaction: transaction,
                        commandType: CommandType.StoredProcedure
                    );

                    if (dOC_TEMPLATE.Any())
                    {
                        var sqlTransaction = (SqlTransaction)transaction;
                        await sqlTransaction.CommitAsync();
                        transactionCompleted = true;

                        var successsResult = new List<DocCategoryOutPutNT>
                            {
                            new DocCategoryOutPutNT
                                {
                                Status = "Ok",
                                Message = "Inserted Successfully!!!",
                                Data= dOC_TEMPLATE
                                }
                        };
                        return successsResult;

                    }
                    else
                    {
                        var successsResult = new List<DocCategoryOutPutNT>
                            {
                            new DocCategoryOutPutNT
                            {
                                Status = "Error",
                                Message = "Insertion failed!!!",
                                Data = null
                            }
                        };
                        return successsResult;
                    }
                }
            }
            catch (Exception ex)
            {
                var errorResult = new List<DocCategoryOutPutNT>
                    {
                        new DocCategoryOutPutNT
                        {
                            Status = "Error",
                            Message = ex.Message,
                            Data = null
                        }
                    };
                return errorResult;
            }
        }
    }
}
