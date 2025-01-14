using Dapper;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.API.DapperDbConnections;
using TaskManagement.API.Interfaces;
using TaskManagement.API.Model;
using System.Data.SqlClient;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TaskManagement.API.Repositories
{
    public class ComplianceRepository : ICompliance
    {
        private static TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        public IDapperDbConnection _dapperDbConnection;
        private readonly string _connectionString;

        public ComplianceRepository(IDapperDbConnection dapperDbConnection, string connectionString)
        {
            _dapperDbConnection = dapperDbConnection;
            _connectionString = connectionString;
        }
        public async Task<ActionResult<IEnumerable<ComplianceOutput_LIST>>> GetComplianceAsync(ComplianceGetInput complianceGetInput)
        {
            DateTime dateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
            IDbTransaction transaction = null;
            bool transactionCompleted = false;
            int SR_NO = 0;
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
                    parameters.Add("@MKEY", complianceGetInput.MKEY);
                    parameters.Add("@LOGGED_IN", complianceGetInput.LOGGED_IN);
                    var GetCompliance = await db.QueryAsync<ComplianceOutPut>("SP_GET_COMPLIANCE", parameters, commandType: CommandType.StoredProcedure, transaction: transaction);
                    //foreach (var compliance in GetCompliance)
                    //{
                    //    var GetComplianceTags = await db.QueryAsync<ComplianceTagsInput>("SELECT * FROM V_Compliance_TRL_TAGS WHERE COMPLIANCE_MKEY = @COMPLIANCE_MKEY ;", new { COMPLIANCE_MKEY = compliance.MKEY }, commandType: CommandType.Text, transaction: transaction);
                    //    compliance.Tags = GetComplianceTags.ToList();

                    //    if (GetCompliance == null)
                    //    {
                    //        // Handle other unexpected exceptions
                    //        if (transaction != null && !transactionCompleted)
                    //        {
                    //            try
                    //            {
                    //                // Rollback only if the transaction is not yet completed
                    //                transaction.Rollback();
                    //            }
                    //            catch (InvalidOperationException rollbackEx)
                    //            {

                    //                Console.WriteLine($"Rollback failed: {rollbackEx.Message}");
                    //                //TranError.Message = ex.Message;
                    //                //return TranError;
                    //            }
                    //        }

                    //        var ErroResponse = new List<ComplianceOutput_LIST>
                    //    {
                    //        new ComplianceOutput_LIST
                    //        {
                    //            Status = "Error",
                    //            Message = "An error occurd",
                    //            Data= null
                    //        }
                    //    };
                    //        return ErroResponse;
                    //    }
                    //}
                    var SuccessResponse = new List<ComplianceOutput_LIST>
                        {
                            new ComplianceOutput_LIST
                            {
                                Status = "Ok",
                                Message = "Data Get Successfuly",
                                Data= GetCompliance
                            }
                        };
                    return SuccessResponse;
                }

            }
            catch (Exception ex)
            {
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
                var ErrorResponse = new List<ComplianceOutput_LIST>
                        {
                            new ComplianceOutput_LIST
                            {
                                Status = "Error",
                                Message = ex.Message,
                                Data = null
                            }
                        };
                return ErrorResponse;
            }
        }
        public async Task<ActionResult<IEnumerable<ComplianceOutput_LIST>>> InsertUpdateComplianceAsync(ComplianceInsertUpdateInput complianceInsertUpdateInput)
        {
            DateTime dateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
            IDbTransaction transaction = null;
            bool transactionCompleted = false;
            int SR_NO = 0;
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
                    parameters.Add("@MKEY", complianceInsertUpdateInput.MKEY);
                    parameters.Add("@PROPERTY", complianceInsertUpdateInput.PROPERTY);
                    parameters.Add("@BUILDING", complianceInsertUpdateInput.BUILDING);
                    parameters.Add("@SHORT_DESCRIPTION", complianceInsertUpdateInput.SHORT_DESCRIPTION);
                    parameters.Add("@LONG_DESCRIPTION", complianceInsertUpdateInput.LONG_DESCRIPTION);
                    parameters.Add("@RAISED_AT", complianceInsertUpdateInput.RAISED_AT);
                    parameters.Add("@RESPONSIBLE_DEPARTMENT", complianceInsertUpdateInput.RESPONSIBLE_DEPARTMENT);
                    parameters.Add("@JOB_ROLE", complianceInsertUpdateInput.JOB_ROLE);
                    parameters.Add("@RESPONSIBLE_PERSON", complianceInsertUpdateInput.RESPONSIBLE_PERSON);
                    //parameters.Add("@TAGS", complianceInsertUpdateInput.Tags);
                    parameters.Add("@TO_BE_COMPLETED_BY", complianceInsertUpdateInput.TO_BE_COMPLETED_BY);
                    parameters.Add("@NO_DAYS", complianceInsertUpdateInput.NO_DAYS);
                    parameters.Add("@STATUS", complianceInsertUpdateInput.STATUS);
                    parameters.Add("@CREATED_BY", complianceInsertUpdateInput.CREATED_BY);
                    parameters.Add("@DELETE_FLAG", complianceInsertUpdateInput.DELETE_FLAG);

                    var GetCompliance = await db.QueryAsync<ComplianceOutPut>("SP_COMPLIANCE_INSERT_UPDATE", parameters, commandType: CommandType.StoredProcedure, transaction: transaction);

                    string[] Splittag = complianceInsertUpdateInput.Tags.Split(",");

                    foreach (var Tag in Splittag)
                    {

                    }

                    if (GetCompliance == null)
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
                            }
                        }
                        var ErroResponse = new List<ComplianceOutput_LIST>
                            {
                                new ComplianceOutput_LIST
                                {
                                    Status = "Error",
                                    Message = "An error occurd",
                                    Data= null
                                }
                            };
                        return ErroResponse;
                    }

                    #region tags comment
                    //var Mkey = GetCompliance.Select(x => x.MKEY.ToString()).First().ToString();
                    //foreach (var Tags in complianceInsertUpdateInput.Tags)
                    //{

                    //    var parametersTags = new DynamicParameters();
                    //    parametersTags.Add("@MKEY", Tags.MKEY);
                    //    parametersTags.Add("@SR_NO", Tags.SR_NO);
                    //    parametersTags.Add("@COMPLIANCE_MKEY", Mkey);
                    //    parametersTags.Add("@TAGS_NAME", Tags.TAGS_NAME);
                    //    parametersTags.Add("@CREATED_BY", complianceInsertUpdateInput.CREATED_BY);
                    //    parametersTags.Add("@DELETE_FLAG", Tags.DELETE_FLAG);

                    //    var GetComplianceTags = await db.QueryAsync<ComplianceTagsInput>("SP_COMPLIANCE_TAGS_INSERT_UPDATE", parametersTags, commandType: CommandType.StoredProcedure, transaction: transaction);

                    //    if (GetCompliance == null)
                    //    {
                    //        // Handle other unexpected exceptions
                    //        if (transaction != null && !transactionCompleted)
                    //        {
                    //            try
                    //            {
                    //                // Rollback only if the transaction is not yet completed
                    //                transaction.Rollback();
                    //            }
                    //            catch (InvalidOperationException rollbackEx)
                    //            {

                    //                Console.WriteLine($"Rollback failed: {rollbackEx.Message}");
                    //                //TranError.Message = ex.Message;
                    //                //return TranError;
                    //            }
                    //        }

                    //        var ErroResponse = new List<ComplianceOutput_LIST>
                    //        {
                    //            new ComplianceOutput_LIST
                    //            {
                    //                Status = "Error",
                    //                Message = "An error occurd",
                    //                Data= null
                    //            }
                    //        };
                    //        return ErroResponse;
                    //    }
                    //}
                    //foreach (var compliance in GetCompliance)
                    //{
                    //    var GetComplianceTags = await db.QueryAsync<ComplianceTagsInput>("SELECT * FROM V_Compliance_TRL_TAGS WHERE COMPLIANCE_MKEY = @COMPLIANCE_MKEY ;", new { COMPLIANCE_MKEY = compliance.MKEY }, commandType: CommandType.Text, transaction: transaction);
                    //    compliance.Tags = GetComplianceTags.ToList();
                    //}
                    #endregion

                    var sqlTransaction = (SqlTransaction)transaction;
                    await sqlTransaction.CommitAsync();
                    transactionCompleted = true;
                    var SuccessResponse = new List<ComplianceOutput_LIST>
                    {
                        new ComplianceOutput_LIST
                        {
                            Status = "Ok",
                            Message = "Successfuly Done",
                            Data= GetCompliance

                        }
                    };
                    return SuccessResponse;
                }

            }
            catch (Exception ex)
            {
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
                var ErroResponse = new List<ComplianceOutput_LIST>
                    {
                        new ComplianceOutput_LIST
                        {
                            Status = "Error",
                            Message = ex.Message,
                            Data= null

                        }
                    };
                return ErroResponse;
            }
        }
    }
}
