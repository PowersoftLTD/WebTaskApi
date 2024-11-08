using Dapper;
using Microsoft.AspNetCore.Mvc;
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
        public IDapperDbConnection _dapperDbConnection;
        private readonly string _connectionString;
        public ApprovalTemplateRepository(IDapperDbConnection dapperDbConnection, string connectionString)
        {
            _dapperDbConnection = dapperDbConnection;
            _connectionString = connectionString;
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
                    var OBJ_APPROVAL_TEMPLATE_HDR = aPPROVAL_TEMPLATE_HDR;
                    var parameters = new DynamicParameters();
                    //var TEST = aPPROVAL_TEMPLATE_HDR.END_RESULT_DOC_LST;
                    parameters.Add("@BUILDING_TYPE", aPPROVAL_TEMPLATE_HDR.BUILDING_TYPE);
                    parameters.Add("@SRNO", aPPROVAL_TEMPLATE_HDR.SRNO);
                    parameters.Add("@SEQNO", aPPROVAL_TEMPLATE_HDR.SEQNO);
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
                    parameters.Add("@ATTRIBUTE1", aPPROVAL_TEMPLATE_HDR.ATTRIBUTE1);
                    parameters.Add("@ATTRIBUTE2", aPPROVAL_TEMPLATE_HDR.ATTRIBUTE2);
                    parameters.Add("@ATTRIBUTE3", aPPROVAL_TEMPLATE_HDR.ATTRIBUTE3);
                    parameters.Add("@ATTRIBUTE4", aPPROVAL_TEMPLATE_HDR.ATTRIBUTE4);
                    parameters.Add("@ATTRIBUTE5", aPPROVAL_TEMPLATE_HDR.ATTRIBUTE5);
                    parameters.Add("@CREATED_BY", aPPROVAL_TEMPLATE_HDR.CREATED_BY);
                    parameters.Add("@LAST_UPDATED_BY", aPPROVAL_TEMPLATE_HDR.LAST_UPDATED_BY);
                    aPPROVAL_TEMPLATE_HDR = await db.QueryFirstOrDefaultAsync<APPROVAL_TEMPLATE_HDR>("SP_INSERT_APPROVAL_TEMPLATE", parameters, commandType: CommandType.StoredProcedure);

                    DateTime dateTime = DateTime.UtcNow.Date;

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

                        /*-------------------------------------------------------------------------------------------------------------------
                        TO INSERT END RESULT LIST
                        */
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

                        //-------------------------------------------------------------------------------------------------------------------
                        /*-------------------------------------------------------------------------------------------------------------------
                       TO INSERT CHECK LIST
                       */
                        // Populate the DataTable with product data
                        dataTable.Rows.Clear();
                        using var transactionCheckList = connection.BeginTransaction();

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
                        sql = "SELECT DOCUMENT_NAME, DOCUMENT_CATEGORY FROM APPROVAL_TEMPLATE_TRL_CHECKLIST WHERE APPROVAL_MKEY = @APPROVAL_MKEY";
                        keyValuePairs = await db.QueryAsync(sql, new { APPROVAL_MKEY = aPPROVAL_TEMPLATE_HDR.MKEY });

                        // Initialize the END_RESULT_DOC_LST dictionary
                        aPPROVAL_TEMPLATE_HDR.CHECKLIST_DOC_LST = new Dictionary<string, object>();

                        // Populate the dictionary with the key-value pairs
                        foreach (var item in keyValuePairs)
                        {
                            // Assuming DOCUMENT_NAME is the key and DOCUMENT_CATEGORY is the value
                            aPPROVAL_TEMPLATE_HDR.CHECKLIST_DOC_LST.Add(item.DOCUMENT_NAME.ToString(), item.DOCUMENT_CATEGORY);
                        }

                        //------------------------------------------------------------------------------------------------------------------------------
                    }
                    catch
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
            using (IDbConnection db = _dapperDbConnection.CreateConnection())
            {
                return await db.QueryFirstOrDefaultAsync<APPROVAL_TEMPLATE_HDR>("SELECT MKEY,BUILDING_TYPE,BUILDING_STANDARD,STATUTORY_AUTHORITY,SHORT_DESCRIPTION,LONG_DESCRIPTION,ABBR,APPROVAL_DEPARTMENT,RESPOSIBLE_EMP_MKEY,JOB_ROLE,NO_DAYS_REQUIRED,ATTRIBUTE1,ATTRIBUTE2,ATTRIBUTE3,ATTRIBUTE4,ATTRIBUTE5,CREATED_BY,CREATION_DATE,LAST_UPDATED_BY,LAST_UPDATE_DATE,SANCTION_AUTHORITY,SANCTION_DEPARTMENT,END_RESULT_DOC,CHECKLIST_DOC,DELETE_FLAG FROM APPROVAL_TEMPLATE_HDR WHERE ABBR = @ABBR ", new { ABBR = ABBR });
            }
        }

        public async Task<IEnumerable<APPROVAL_TEMPLATE_HDR>> AbbrAndShortDescAsync(string strBuilding, string strStandard, string strAuthority)
        {
            using (IDbConnection db = _dapperDbConnection.CreateConnection())
            {
                return await db.QueryAsync<APPROVAL_TEMPLATE_HDR>("SELECT MKEY,ABBR +' '+ SHORT_DESCRIPTION AS ABBR_SHORT_DESC,BUILDING_TYPE,BUILDING_STANDARD,STATUTORY_AUTHORITY,SHORT_DESCRIPTION,LONG_DESCRIPTION,ABBR,APPROVAL_DEPARTMENT,RESPOSIBLE_EMP_MKEY,JOB_ROLE,NO_DAYS_REQUIRED,SANCTION_AUTHORITY FROM [dbo].[APPROVAL_TEMPLATE_hdr] WHERE BUILDING_TYPE = @strBuilding AND BUILDING_STANDARD = @strStandard AND STATUTORY_AUTHORITY = @strAuthority", new { strBuilding = strBuilding, strStandard = strStandard, strAuthority = strAuthority });
            }
        }
    }
}
