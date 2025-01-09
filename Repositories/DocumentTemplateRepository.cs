using Dapper;
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
                        transaction: transaction,  // Pass the transaction here
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
        public async Task<DocCategoryOutPut_List> InsertDocumentCategoryCheckList(DocCategoryCheckListInput docCategoryCheckListInput)
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
                        new { TYPE_DESC = docCategoryCheckListInput.DOC_CATEGORY }, transaction: transaction);

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
                        parmeters.Add("@DOC_CATEGORY", docCategoryCheckListInput.DOC_CATEGORY);
                        parmeters.Add("@CREATED_BY", docCategoryCheckListInput.CREATED_BY);
                        parmeters.Add("@COMPANY_ID", docCategoryCheckListInput.COMPANY_ID);

                        var InsertDocCategory = await db.QueryAsync<V_Building_Classification>("SP_INSERT_DOC_CATEGORY_CHECKLIST",
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

        public async Task<DocCategoryOutPut_List> UpdateDocumentCategoryCheckList(DocCategoryUpdateCheckListInput docCategoryUpdateCheckListInput)
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
                    parmeters.Add("@MKEY", docCategoryUpdateCheckListInput.MKEY);
                    parmeters.Add("@CREATED_BY", docCategoryUpdateCheckListInput.CREATED_BY);
                    parmeters.Add("@DOC_INSTR", docCategoryUpdateCheckListInput.DOC_INSTR);
                    parmeters.Add("@DELETE_FLAG", docCategoryUpdateCheckListInput.DELETE_FLAG);

                    var InsertDocCategory = await db.QueryAsync<V_Building_Classification>("SP_UPDATE_DOC_CATEGORY_CHECKLIST", parmeters, commandType: CommandType.StoredProcedure, transaction: transaction);

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
    }
}
