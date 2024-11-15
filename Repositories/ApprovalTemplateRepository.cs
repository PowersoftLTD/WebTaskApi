using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Owin.Security.Provider;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Transactions;
using System.Xml.Linq;
using TaskManagement.API.Interfaces;
using TaskManagement.API.Model;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace TaskManagement.API.Repositories
{
    public class ApprovalTemplateRepository : IApprovalTemplate
    {
        private static TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        public IDapperDbConnection _dapperDbConnection;
        private readonly string _connectionString;
        public ApprovalTemplateRepository(IDapperDbConnection dapperDbConnection, string connectionString)
        {
            _dapperDbConnection = dapperDbConnection;
            _connectionString = connectionString;
        }
        public async Task<IEnumerable<APPROVAL_TEMPLATE_HDR>> GetAllApprovalTemplateAsync()
        {
            try
            {
                using (IDbConnection db = _dapperDbConnection.CreateConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@MKEY", null);
                    var approvalTemplates = await db.QueryAsync<APPROVAL_TEMPLATE_HDR>("SP_GET_APPROVAL_TEMPLATE", parmeters, commandType: CommandType.StoredProcedure);

                    if (approvalTemplates == null || !approvalTemplates.Any())
                    {
                        return Enumerable.Empty<APPROVAL_TEMPLATE_HDR>(); // Return an empty list if no results
                    }

                    // Iterate over each approval template header to populate subtasks, end result docs, and checklist docs
                    foreach (var approvalTemplate in approvalTemplates)
                    {
                        // Fetch the associated subtasks
                        var subtasks = await db.QueryAsync<APPROVAL_TEMPLATE_TRL_SUBTASK>(
                            "SELECT * FROM APPROVAL_TEMPLATE_TRL_SUBTASK WHERE APPROVAL_MKEY = @MKEY",
                            new { MKEY = approvalTemplate.MKEY });

                        approvalTemplate.SUBTASK_LIST = subtasks.ToList(); // Populate the SUBTASK_LIST property

                       
                        string sql = "SELECT DOCUMENT_NAME, DOCUMENT_CATEGORY FROM APPROVAL_TEMPLATE_TRL_ENDRESULT WHERE APPROVAL_MKEY = @APPROVAL_MKEY";
                        var keyValuePairs = await db.QueryAsync(sql, new { APPROVAL_MKEY = approvalTemplate.MKEY });

                        // Initialize the END_RESULT_DOC_LST dictionary
                        approvalTemplate.END_RESULT_DOC_LST = new Dictionary<string, object>();

                        // Populate the dictionary with the key-value pairs
                        foreach (var item in keyValuePairs)
                        {
                            // Assuming DOCUMENT_NAME is the key and DOCUMENT_CATEGORY is the value
                            approvalTemplate.END_RESULT_DOC_LST.Add(item.DOCUMENT_NAME.ToString(), item.DOCUMENT_CATEGORY);
                        }

                        sql = "SELECT DOCUMENT_NAME, DOCUMENT_CATEGORY FROM APPROVAL_TEMPLATE_TRL_CHECKLIST WHERE APPROVAL_MKEY = @APPROVAL_MKEY";
                        var keyValuePairsCheckList = await db.QueryAsync(sql, new { APPROVAL_MKEY = approvalTemplate.MKEY });

                        // Initialize the END_RESULT_DOC_LST dictionary
                        approvalTemplate.CHECKLIST_DOC_LST = new Dictionary<string, object>();

                        // Populate the dictionary with the key-value pairs
                        foreach (var item in keyValuePairsCheckList)
                        {
                            // Assuming DOCUMENT_NAME is the key and DOCUMENT_CATEGORY is the value
                            approvalTemplate.CHECKLIST_DOC_LST.Add(item.DOCUMENT_NAME.ToString(), item.DOCUMENT_CATEGORY);
                        }
                    }

                    return approvalTemplates;
                }
            }
            catch (Exception ex)
            {
                return null;
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
                    var approvalTemplate = await db.QueryFirstOrDefaultAsync<APPROVAL_TEMPLATE_HDR>("SP_GET_APPROVAL_TEMPLATE", parmeters, commandType: CommandType.StoredProcedure);

                    if (approvalTemplate == null)
                    {
                        return null; // Return an empty list if no results
                    }


                    // Fetch the associated subtasks
                    // Fetch the associated Subtasks
                    var subtasks = await db.QueryAsync<APPROVAL_TEMPLATE_TRL_SUBTASK>(
                        "SELECT * FROM APPROVAL_TEMPLATE_TRL_SUBTASK WHERE APPROVAL_MKEY = @MKEY",
                        new { MKEY = id });

                    approvalTemplate.SUBTASK_LIST = subtasks.ToList(); // Populate the SUBTASK_LIST property with subtasks
                    
                    string sql = "SELECT DOCUMENT_NAME, DOCUMENT_CATEGORY FROM APPROVAL_TEMPLATE_TRL_ENDRESULT WHERE APPROVAL_MKEY = @APPROVAL_MKEY";
                    var keyValuePairs = await db.QueryAsync(sql, new { APPROVAL_MKEY = approvalTemplate.MKEY });

                    // Initialize the END_RESULT_DOC_LST dictionary
                    approvalTemplate.END_RESULT_DOC_LST = new Dictionary<string, object>();

                    // Populate the dictionary with the key-value pairs
                    foreach (var item in keyValuePairs)
                    {
                        // Assuming DOCUMENT_NAME is the key and DOCUMENT_CATEGORY is the value
                        approvalTemplate.END_RESULT_DOC_LST.Add(item.DOCUMENT_NAME.ToString(), item.DOCUMENT_CATEGORY);
                    }

                    sql = "SELECT DOCUMENT_NAME, DOCUMENT_CATEGORY FROM APPROVAL_TEMPLATE_TRL_CHECKLIST WHERE APPROVAL_MKEY = @APPROVAL_MKEY";
                    var keyValuePairsCheckList = await db.QueryAsync(sql, new { APPROVAL_MKEY = approvalTemplate.MKEY });

                    // Initialize the END_RESULT_DOC_LST dictionary
                    approvalTemplate.CHECKLIST_DOC_LST = new Dictionary<string, object>();

                    // Populate the dictionary with the key-value pairs
                    foreach (var item in keyValuePairsCheckList)
                    {
                        // Assuming DOCUMENT_NAME is the key and DOCUMENT_CATEGORY is the value
                        approvalTemplate.CHECKLIST_DOC_LST.Add(item.DOCUMENT_NAME.ToString(), item.DOCUMENT_CATEGORY);
                    }


                    return approvalTemplate;
                }
            }
            catch (Exception ex)
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
                    var OBJ_APPROVAL_TEMPLATE_HDR = aPPROVAL_TEMPLATE_HDR;
                    var parameters = new DynamicParameters();
                    //var TEST = aPPROVAL_TEMPLATE_HDR.END_RESULT_DOC_LST;
                    parameters.Add("@BUILDING_TYPE", aPPROVAL_TEMPLATE_HDR.BUILDING_TYPE);
                    //parameters.Add("@SRNO", aPPROVAL_TEMPLATE_HDR.SRNO);
                    //parameters.Add("@SEQNO", aPPROVAL_TEMPLATE_HDR.SEQNO);
                    parameters.Add("@BUILDING_STANDARD", aPPROVAL_TEMPLATE_HDR.BUILDING_STANDARD);
                    parameters.Add("@STATUTORY_AUTHORITY", aPPROVAL_TEMPLATE_HDR.STATUTORY_AUTHORITY);
                    parameters.Add("@SHORT_DESCRIPTION", aPPROVAL_TEMPLATE_HDR.SHORT_DESCRIPTION);
                    parameters.Add("@LONG_DESCRIPTION", aPPROVAL_TEMPLATE_HDR.LONG_DESCRIPTION);
                    parameters.Add("@ABBR", aPPROVAL_TEMPLATE_HDR.MAIN_ABBR);
                    parameters.Add("@APPROVAL_DEPARTMENT", aPPROVAL_TEMPLATE_HDR.AUTHORITY_DEPARTMENT);
                    parameters.Add("@RESPOSIBLE_EMP_MKEY", aPPROVAL_TEMPLATE_HDR.RESPOSIBLE_EMP_MKEY);
                    parameters.Add("@JOB_ROLE", aPPROVAL_TEMPLATE_HDR.JOB_ROLE);
                    parameters.Add("@NO_DAYS_REQUIRED", aPPROVAL_TEMPLATE_HDR.DAYS_REQUIERD);
                    parameters.Add("@SANCTION_AUTHORITY", aPPROVAL_TEMPLATE_HDR.SANCTION_AUTHORITY);
                    parameters.Add("@SANCTION_DEPARTMENT", aPPROVAL_TEMPLATE_HDR.SANCTION_DEPARTMENT);
                    parameters.Add("@END_RESULT_DOC", aPPROVAL_TEMPLATE_HDR.END_RESULT_DOC);
                    parameters.Add("@CHECKLIST_DOC", aPPROVAL_TEMPLATE_HDR.CHECKLIST_DOC);
                    parameters.Add("@ATTRIBUTE1", aPPROVAL_TEMPLATE_HDR.ATTRIBUTE1);
                    parameters.Add("@ATTRIBUTE2", aPPROVAL_TEMPLATE_HDR.ATTRIBUTE2);
                    parameters.Add("@ATTRIBUTE3", aPPROVAL_TEMPLATE_HDR.ATTRIBUTE3);
                    parameters.Add("@ATTRIBUTE4", aPPROVAL_TEMPLATE_HDR.ATTRIBUTE4);
                    parameters.Add("@ATTRIBUTE5", aPPROVAL_TEMPLATE_HDR.ATTRIBUTE5);
                    parameters.Add("@CREATED_BY", aPPROVAL_TEMPLATE_HDR.CREATED_BY);
                    parameters.Add("@LAST_UPDATED_BY", aPPROVAL_TEMPLATE_HDR.LAST_UPDATED_BY);
                    aPPROVAL_TEMPLATE_HDR = await db.QueryFirstOrDefaultAsync<APPROVAL_TEMPLATE_HDR>("SP_INSERT_APPROVAL_TEMPLATE", parameters, commandType: CommandType.StoredProcedure);

                    DateTime dateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);

                    using var connection = new SqlConnection(_connectionString);
                    await connection.OpenAsync();

                    // Use BeginTransaction() (synchronously) to get a SqlTransaction
                    using var transaction = connection.BeginTransaction();
                    try
                    {

                        var sqlTransaction = transaction as SqlTransaction;

                        if (sqlTransaction == null)
                        {
                            throw new InvalidOperationException("Transaction is not of type SqlTransaction.");
                        }

                        // Create a DataTable for bulk insert
                        var dataTable = new DataTable();
                        dataTable.Columns.Add("MKEY", typeof(int));
                        dataTable.Columns.Add("APPROVAL_MKEY", typeof(int));
                        dataTable.Columns.Add("DOCUMENT_NAME", typeof(string));
                        dataTable.Columns.Add("DOCUMENT_CATEGORY", typeof(string));
                        dataTable.Columns.Add("ATTRIBUTE1", typeof(string));
                        dataTable.Columns.Add("ATTRIBUTE2", typeof(string));
                        dataTable.Columns.Add("ATTRIBUTE3", typeof(string));
                        dataTable.Columns.Add("ATTRIBUTE4", typeof(string));
                        dataTable.Columns.Add("ATTRIBUTE5", typeof(string));
                        dataTable.Columns.Add("CREATED_BY", typeof(int));
                        dataTable.Columns.Add("CREATION_DATE", typeof(DateTime));
                        dataTable.Columns.Add("LAST_UPDATED_BY", typeof(int));
                        dataTable.Columns.Add("LAST_UPDATE_DATE", typeof(DateTime));
                        dataTable.Columns.Add("DELETE_FLAG", typeof(char));

                        if (OBJ_APPROVAL_TEMPLATE_HDR.END_RESULT_DOC_LST != null)
                        {
                            // Populate the DataTable with product data
                            foreach (var END_DOC_LIST in OBJ_APPROVAL_TEMPLATE_HDR.END_RESULT_DOC_LST)
                            {
                                dataTable.Rows.Add(null, aPPROVAL_TEMPLATE_HDR.MKEY, END_DOC_LIST.Key, END_DOC_LIST.Value, null, null, null, null, null, aPPROVAL_TEMPLATE_HDR.CREATED_BY, dateTime.ToString("yyyy/MM/dd hh:mm:ss"), aPPROVAL_TEMPLATE_HDR.LAST_UPDATED_BY, dateTime.ToString("yyyy/MM/dd hh:mm:ss"), 'N');
                            }

                            // Use SqlBulkCopy for bulk insert
                            using var bulkCopy = new SqlBulkCopy(connection, SqlBulkCopyOptions.Default, transaction)
                            {
                                DestinationTableName = "APPROVAL_TEMPLATE_TRL_ENDRESULT"
                            };

                            // Execute the bulk copy
                            await bulkCopy.WriteToServerAsync(dataTable);

                            // Commit transaction
                            await transaction.CommitAsync();

                            /*
                             * TO GET INSERTED VALUE IN END RESULT
                             * */
                            // Query the APPROVAL_TEMPLATE_TRL_CHECKLIST for key-value pairs
                            string sql = "SELECT DOCUMENT_NAME, DOCUMENT_CATEGORY FROM APPROVAL_TEMPLATE_TRL_ENDRESULT WHERE APPROVAL_MKEY = @APPROVAL_MKEY";
                            var keyValuePairs = await db.QueryAsync(sql, new { APPROVAL_MKEY = aPPROVAL_TEMPLATE_HDR.MKEY });

                            // Initialize the END_RESULT_DOC_LST dictionary
                            aPPROVAL_TEMPLATE_HDR.END_RESULT_DOC_LST = new Dictionary<string, object>();

                            // Populate the dictionary with the key-value pairs
                            foreach (var item in keyValuePairs)
                            {
                                // Assuming DOCUMENT_NAME is the key and DOCUMENT_CATEGORY is the value
                                aPPROVAL_TEMPLATE_HDR.END_RESULT_DOC_LST.Add(item.DOCUMENT_NAME.ToString(), item.DOCUMENT_CATEGORY);
                            }
                        }
                        /*-------------------------------------------------------------------------------------------------------------------
                        TO INSERT END RESULT LIST
                        */


                        //-------------------------------------------------------------------------------------------------------------------
                        /*-------------------------------------------------------------------------------------------------------------------
                       TO INSERT CHECK LIST
                       */
                        // Populate the DataTable with product data
                        dataTable.Rows.Clear();
                        using var transactionCheckList = connection.BeginTransaction();

                        if (OBJ_APPROVAL_TEMPLATE_HDR.CHECKLIST_DOC_LST != null)
                        {
                            foreach (var CHECK_LIST in OBJ_APPROVAL_TEMPLATE_HDR.CHECKLIST_DOC_LST)
                            {
                                dataTable.Rows.Add(null, aPPROVAL_TEMPLATE_HDR.MKEY, CHECK_LIST.Key, CHECK_LIST.Value, null, null, null, null, null, aPPROVAL_TEMPLATE_HDR.CREATED_BY, dateTime.ToString("yyyy/MM/dd hh:mm:ss"), aPPROVAL_TEMPLATE_HDR.LAST_UPDATED_BY, dateTime.ToString("yyyy/MM/dd hh:mm:ss"), 'N');
                            }

                            // Use SqlBulkCopy for bulk insert
                            using var bulkCopyCheckList = new SqlBulkCopy(connection, SqlBulkCopyOptions.Default, transactionCheckList)
                            {
                                DestinationTableName = "APPROVAL_TEMPLATE_TRL_CHECKLIST"
                            };

                            // Execute the bulk copy
                            await bulkCopyCheckList.WriteToServerAsync(dataTable);

                            // Commit transaction
                            await transactionCheckList.CommitAsync();


                            // Query the APPROVAL_TEMPLATE_TRL_CHECKLIST for key-value pairs
                            var sql = "SELECT DOCUMENT_NAME, DOCUMENT_CATEGORY FROM APPROVAL_TEMPLATE_TRL_CHECKLIST WHERE APPROVAL_MKEY = @APPROVAL_MKEY";
                            var keyValuePairs = await db.QueryAsync(sql, new { APPROVAL_MKEY = aPPROVAL_TEMPLATE_HDR.MKEY });

                            // Initialize the END_RESULT_DOC_LST dictionary
                            aPPROVAL_TEMPLATE_HDR.CHECKLIST_DOC_LST = new Dictionary<string, object>();

                            // Populate the dictionary with the key-value pairs
                            foreach (var item in keyValuePairs)
                            {
                                // Assuming DOCUMENT_NAME is the key and DOCUMENT_CATEGORY is the value
                                aPPROVAL_TEMPLATE_HDR.CHECKLIST_DOC_LST.Add(item.DOCUMENT_NAME.ToString(), item.DOCUMENT_CATEGORY);
                            }
                        }

                        //------------------------------------------------------------------------------------------------------------------------------
                    }
                    catch
                    {
                        await transaction.RollbackAsync();
                        throw;
                    }
                    var strSUBTASK_MKEY = await db.QueryFirstOrDefaultAsync<int>("SELECT  ISNULL(MAX(subtask_mkey),0)+1 FROM APPROVAL_TEMPLATE_TRL_SUBTASK WHERE APPROVAL_MKEY = @MKEY", new { MKEY = OBJ_APPROVAL_TEMPLATE_HDR.MKEY });

                    using var transactionSubTask = connection.BeginTransaction();
                    try
                    {
                        // Create a DataTable for bulk insert of subtasks (SEQNO, SRNO, ABBR)
                        var subtaskDataTable = new DataTable();
                        subtaskDataTable.Columns.Add("MKEY", typeof(int));
                        subtaskDataTable.Columns.Add("APPROVAL_MKEY", typeof(int));
                        subtaskDataTable.Columns.Add("SRNO", typeof(int));
                        subtaskDataTable.Columns.Add("SEQNO", typeof(int));
                        subtaskDataTable.Columns.Add("SUBTASK_ABBR", typeof(string));
                        subtaskDataTable.Columns.Add("SUBTASK_MKEY", typeof(int));
                        subtaskDataTable.Columns.Add("ATTRIBUTE1", typeof(string));
                        subtaskDataTable.Columns.Add("ATTRIBUTE2", typeof(string));
                        subtaskDataTable.Columns.Add("ATTRIBUTE3", typeof(string));
                        subtaskDataTable.Columns.Add("ATTRIBUTE4", typeof(string));
                        subtaskDataTable.Columns.Add("ATTRIBUTE5", typeof(string));
                        subtaskDataTable.Columns.Add("ATTRIBUTE6", typeof(string));
                        subtaskDataTable.Columns.Add("ATTRIBUTE7", typeof(string));
                        subtaskDataTable.Columns.Add("ATTRIBUTE8", typeof(string));
                        subtaskDataTable.Columns.Add("CREATED_BY", typeof(int));
                        subtaskDataTable.Columns.Add("CREATION_DATE", typeof(DateTime));
                        subtaskDataTable.Columns.Add("LAST_UPDATED_BY", typeof(int));
                        subtaskDataTable.Columns.Add("LAST_UPDATE_DATE", typeof(DateTime));
                        subtaskDataTable.Columns.Add("DELETE_FLAG", typeof(char));
                        bool flagID = false;
                        if (OBJ_APPROVAL_TEMPLATE_HDR.SUBTASK_LIST != null)
                        {
                            // Populate the DataTable with subtasks
                            foreach (var subtask in OBJ_APPROVAL_TEMPLATE_HDR.SUBTASK_LIST) // Assuming SUBTASK_LIST is a list of subtasks
                            {
                                if (flagID != false)
                                {
                                    strSUBTASK_MKEY = strSUBTASK_MKEY + 1;
                                }
                                subtaskDataTable.Rows.Add(null, aPPROVAL_TEMPLATE_HDR.MKEY, subtask.SRNO, subtask.SEQNO, subtask.SUBTASK_ABBR, strSUBTASK_MKEY, null, null, null, null, null, null, null, null, OBJ_APPROVAL_TEMPLATE_HDR.CREATED_BY, dateTime.ToString("yyyy/MM/dd hh:mm:ss"), OBJ_APPROVAL_TEMPLATE_HDR.CREATED_BY, dateTime.ToString("yyyy/MM/dd hh:mm:ss"), 'N');
                                flagID = true;
                            }

                            // Use SqlBulkCopy to insert subtasks
                            using var bulkCopy = new SqlBulkCopy(connection, SqlBulkCopyOptions.Default, transactionSubTask)
                            {
                                DestinationTableName = "APPROVAL_TEMPLATE_TRL_SUBTASK"  // Ensure this matches your table name
                            };

                            await bulkCopy.WriteToServerAsync(subtaskDataTable);

                            // Commit the transactionSubTask
                            await transactionSubTask.CommitAsync();

                            // Optionally, fetch the inserted values (if necessary)
                            string sql = "SELECT SEQNO, SRNO, SUBTASK_ABBR FROM APPROVAL_TEMPLATE_TRL_SUBTASK WHERE APPROVAL_MKEY = @APPROVAL_MKEY";
                            var subtaskKeyValuePairs = await db.QueryAsync(sql, new { APPROVAL_MKEY = aPPROVAL_TEMPLATE_HDR.MKEY });

                            // Assuming the model has a SUBTASK_LIST dictionary to hold these values
                            aPPROVAL_TEMPLATE_HDR.SUBTASK_LIST = new List<APPROVAL_TEMPLATE_TRL_SUBTASK>();  // Assuming Subtask is a class for this data

                            foreach (var item in subtaskKeyValuePairs)
                            {
                                aPPROVAL_TEMPLATE_HDR.SUBTASK_LIST.Add(new APPROVAL_TEMPLATE_TRL_SUBTASK
                                {
                                    SEQNO = item.SEQNO,
                                    SRNO = item.SRNO,
                                    SUBTASK_ABBR = item.ABBR
                                });
                            }
                        }
                        return aPPROVAL_TEMPLATE_HDR;
                    }
                    catch (Exception ex)
                    {
                        await transaction.RollbackAsync();
                        throw;
                    }
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
            try
            {
                using (IDbConnection db = _dapperDbConnection.CreateConnection())
                {
                    return await db.QueryFirstOrDefaultAsync<APPROVAL_TEMPLATE_HDR>("SELECT HDR.MKEY,BUILDING_TYPE,BUILDING_STANDARD,STATUTORY_AUTHORITY,SHORT_DESCRIPTION,LONG_DESCRIPTION,MAIN_ABBR,AUTHORITY_DEPARTMENT,RESPOSIBLE_EMP_MKEY,JOB_ROLE,DAYS_REQUIERD,HDR.ATTRIBUTE1,HDR.ATTRIBUTE2,HDR.ATTRIBUTE3,HDR.ATTRIBUTE4,HDR.ATTRIBUTE5,HDR.CREATED_BY,HDR.CREATION_DATE,HDR.LAST_UPDATED_BY,HDR.LAST_UPDATE_DATE,SANCTION_AUTHORITY,SANCTION_DEPARTMENT,END_RESULT_DOC,CHECKLIST_DOC,HDR.DELETE_FLAG  FROM APPROVAL_TEMPLATE_HDR HDR INNER JOIN APPROVAL_TEMPLATE_TRL_SUBTASK TRL_SUB ON HDR.MKEY = TRL_SUB.APPROVAL_MKEY WHERE LOWER(MAIN_ABBR) = LOWER(@ABBR) OR  LOWER(SUBTASK_ABBR) = LOWER(@ABBR) AND HDR.DELETE_FLAG = 'N' AND TRL_SUB.DELETE_FLAG = 'N' ", new { ABBR = ABBR });
                }
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        public async Task<IEnumerable<APPROVAL_TEMPLATE_HDR>> AbbrAndShortDescAsync(string strBuilding, string strStandard, string strAuthority)
        {
            using (IDbConnection db = _dapperDbConnection.CreateConnection())
            {
                var Abbr_List = await db.QueryAsync<APPROVAL_TEMPLATE_HDR>("SELECT MKEY,MAIN_ABBR +' '+ SHORT_DESCRIPTION AS ABBR_SHORT_DESC,BUILDING_TYPE,BUILDING_STANDARD,STATUTORY_AUTHORITY,SHORT_DESCRIPTION,LONG_DESCRIPTION,MAIN_ABBR,AUTHORITY_DEPARTMENT,RESPOSIBLE_EMP_MKEY,JOB_ROLE,DAYS_REQUIERD,SANCTION_AUTHORITY FROM [dbo].[APPROVAL_TEMPLATE_hdr] WHERE BUILDING_TYPE = @strBuilding AND BUILDING_STANDARD = @strStandard AND STATUTORY_AUTHORITY = @strAuthority", new { strBuilding = strBuilding, strStandard = strStandard, strAuthority = strAuthority });
                return Abbr_List;
            }
        }
    }
}
