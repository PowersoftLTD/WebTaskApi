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
                    parameters.Add("@CREATED_BY", dOC_TEMPLATE_HDR.CREATED_BY);
                    parameters.Add("@ATTRIBUTE1", dOC_TEMPLATE_HDR.ATTRIBUTE1);
                    parameters.Add("@ATTRIBUTE2", dOC_TEMPLATE_HDR.ATTRIBUTE2);

                    dOC_TEMPLATE_HDR = await db.QueryFirstOrDefaultAsync<DOC_TEMPLATE_HDR>("SP_INSERT_DOCUMENT_TEMPLATES", parameters, commandType: CommandType.StoredProcedure);
                    //var pROJECT_APPROVAL_ABBR_LIST = await db.QueryAsync("select * FROM DOC_TEMPLATE_HDR",  commandType: CommandType.Text);

                    if (dOC_TEMPLATE_HDR == null)
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

                        return dOC_TEMPLATE_HDR;
                    }
                    var sqlTransaction = (SqlTransaction)transaction;
                    await sqlTransaction.CommitAsync();
                    transactionCompleted = true;

                    var TemplateSuccess = new DOC_TEMPLATE_HDR();
                    TemplateSuccess.Status = "Ok";
                    TemplateSuccess.Message = "Data save successfully";
                    return dOC_TEMPLATE_HDR;
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
                    var doc_delete =  await db.ExecuteAsync("SP_DELETE_DOCUMENT_TEMPLATES", parameters, commandType: CommandType.StoredProcedure, transaction:transaction);

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
    }
}
