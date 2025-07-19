using Dapper;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.API.DapperDbConnections;
using TaskManagement.API.Interfaces;
using TaskManagement.API.Model;
using System.Data.SqlClient;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Reflection.Metadata;
using System.Linq.Expressions;
using System.Linq;

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
                    parameters.Add("@USER_ID", complianceGetInput.USER_ID);
                    var GetCompliance = await db.QueryAsync<ComplianceOutPut>("SP_GET_COMPLIANCE", parameters, commandType: CommandType.StoredProcedure, transaction: transaction);

                    var SuccessResponse = new List<ComplianceOutput_LIST>
                        {
                            new ComplianceOutput_LIST
                            {
                                STATUS = "Ok",
                                MESSAGE = "Data Get Successfuly",
                                DATA= GetCompliance
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
                                STATUS = "Error",
                                MESSAGE = ex.Message,
                                DATA = null
                            }
                        };
                return ErrorResponse;

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
            }
        }
        public async Task<ActionResult<IEnumerable<ComplianceOutput_LIST>>> InsertUpdateComplianceAsync(ComplianceInsertUpdateInput complianceInsertUpdateInput)
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
                    parameters.Add("@MKEY", complianceInsertUpdateInput.MKEY);  // TASK_NO
                    parameters.Add("@PROPERTY_MKEY", complianceInsertUpdateInput.PROPERTY_MKEY); // PROJECT_ID
                    parameters.Add("@BUILDING_MKEY", complianceInsertUpdateInput.BUILDING_MKEY); // SUBPROJECT_ID
                    parameters.Add("@SHORT_DESCRIPTION", complianceInsertUpdateInput.SHORT_DESCRIPTION); // TASK_NAME
                    parameters.Add("@LONG_DESCRIPTION", complianceInsertUpdateInput.LONG_DESCRIPTION); // TASK_DESCRIPTION
                    parameters.Add("@CATEGORY", complianceInsertUpdateInput.CAREGORY);   //CATEGORY
                    parameters.Add("@RAISED_AT", complianceInsertUpdateInput.RAISED_AT);
                    parameters.Add("@RAISED_AT_BEFORE", complianceInsertUpdateInput.RAISED_AT_BEFORE);
                    parameters.Add("@RESPONSIBLE_DEPARTMENT", complianceInsertUpdateInput.RESPONSIBLE_DEPARTMENT);
                    parameters.Add("@JOB_ROLE", complianceInsertUpdateInput.JOB_ROLE);
                    parameters.Add("@RESPONSIBLE_PERSON", complianceInsertUpdateInput.RESPONSIBLE_PERSON); //ASSIGNED_TO
                    parameters.Add("@TAGS", complianceInsertUpdateInput.TAGS);  //TAGS
                    parameters.Add("@TASK_TYPE", complianceInsertUpdateInput.TASK_TYPE);  //TAGS
                    parameters.Add("@TO_BE_COMPLETED_BY", complianceInsertUpdateInput.TO_BE_COMPLETED_BY); //COMPLETION_DATE
                    parameters.Add("@NO_DAYS", complianceInsertUpdateInput.NO_DAYS);
                    parameters.Add("@STATUS", complianceInsertUpdateInput.STATUS);
                    parameters.Add("@CREATED_BY", complianceInsertUpdateInput.CREATED_BY); //TASK_CREATED_BY
                    parameters.Add("@DELETE_FLAG", complianceInsertUpdateInput.DELETE_FLAG);
                    parameters.Add("@RESPONSE_STATUS", null);
                    parameters.Add("@MESSAGE", null);

                    var GetCompliance = await db.QueryAsync<ComplianceOutPut>("SP_COMPLIANCE_INSERT_UPDATE", parameters, commandType: CommandType.StoredProcedure, transaction: transaction);


                    if (GetCompliance.Any())
                    {
                        var sqlTransaction = (SqlTransaction)transaction;
                        await sqlTransaction.CommitAsync();
                        transactionCompleted = true;

                        return new List<ComplianceOutput_LIST>
                    {
                        new ComplianceOutput_LIST
                        {
                            STATUS = "Ok",
                            MESSAGE = "Successfully Done",
                            DATA = GetCompliance
                        }
                    };
                    }
                    else
                    {
                        return new List<ComplianceOutput_LIST>
                            {
                                new ComplianceOutput_LIST
                                {
                                    STATUS = "Error",
                                    MESSAGE = "Error occured",
                                    DATA = null
                                }
                            };

                    }
                }

                // Ensure that this method always returns a value
                return new List<ComplianceOutput_LIST>
                    {
                        new ComplianceOutput_LIST
                        {
                            STATUS = "Error",
                            MESSAGE = "No compliance data found or processed.",
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
        public async Task<ActionResult<IEnumerable<ComplianceOutput_LIST_NT>>> InsertUpdateComplianceNTAsync(ComplianceInsertUpdateInputNT complianceInsertUpdateInput)
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

                    try
                    {
                        transaction = db.BeginTransaction();
                        transactionCompleted = false;  // Reset transaction state
                        var parameters = new DynamicParameters();
                        parameters.Add("@MKEY", complianceInsertUpdateInput.MKEY);  // TASK_NO
                        parameters.Add("@PROPERTY_MKEY", complianceInsertUpdateInput.PROPERTY_MKEY); // PROJECT_ID
                        parameters.Add("@BUILDING_MKEY", complianceInsertUpdateInput.BUILDING_MKEY); // SUBPROJECT_ID
                        parameters.Add("@SHORT_DESCRIPTION", complianceInsertUpdateInput.SHORT_DESCRIPTION); // TASK_NAME
                        parameters.Add("@LONG_DESCRIPTION", complianceInsertUpdateInput.LONG_DESCRIPTION); // TASK_DESCRIPTION
                        parameters.Add("@CATEGORY", complianceInsertUpdateInput.CAREGORY);   //CATEGORY
                        parameters.Add("@RAISED_AT", complianceInsertUpdateInput.RAISED_AT);
                        parameters.Add("@RAISED_AT_BEFORE", complianceInsertUpdateInput.RAISED_AT_BEFORE);
                        parameters.Add("@RESPONSIBLE_DEPARTMENT", complianceInsertUpdateInput.RESPONSIBLE_DEPARTMENT);
                        parameters.Add("@JOB_ROLE", complianceInsertUpdateInput.JOB_ROLE);
                        parameters.Add("@RESPONSIBLE_PERSON", complianceInsertUpdateInput.RESPONSIBLE_PERSON); //ASSIGNED_TO
                        parameters.Add("@TAGS", complianceInsertUpdateInput.TAGS);  //TAGS
                        parameters.Add("@TASK_TYPE", complianceInsertUpdateInput.TASK_TYPE);  //TAGS
                        parameters.Add("@TO_BE_COMPLETED_BY", complianceInsertUpdateInput.TO_BE_COMPLETED_BY); //COMPLETION_DATE
                        parameters.Add("@NO_DAYS", complianceInsertUpdateInput.NO_DAYS);
                        parameters.Add("@STATUS", complianceInsertUpdateInput.STATUS);
                        parameters.Add("@CREATED_BY", complianceInsertUpdateInput.CREATED_BY); //TASK_CREATED_BY
                        parameters.Add("@DELETE_FLAG", complianceInsertUpdateInput.DELETE_FLAG);
                        parameters.Add("@RESPONSE_STATUS", null);
                        parameters.Add("@MESSAGE", null);

                        var GetCompliance = await db.QueryAsync<ComplianceOutPutNT>("SP_COMPLIANCE_INSERT_UPDATE_NT", parameters, commandType: CommandType.StoredProcedure, transaction: transaction);
                        string ErrorMessage = string.Empty;

                        if (GetCompliance.Any())
                        {

                            foreach (var ErrorVar in GetCompliance)
                            {
                                if (ErrorVar.ResponseStatus == "Error")
                                {
                                    ErrorMessage = ErrorVar.Message;
                                    break;
                                }
                            }

                            if (ErrorMessage != string.Empty)
                            {
                                var sqlTransaction = (SqlTransaction)transaction;
                                await sqlTransaction.CommitAsync();
                                transactionCompleted = true;

                                return new List<ComplianceOutput_LIST_NT>
                                {
                                    new ComplianceOutput_LIST_NT
                                    {
                                        STATUS = "Error",
                                        MESSAGE = ErrorMessage,
                                        DATA = null
                                    }
                                };
                            }
                            else
                            {
                                var sqlTransaction = (SqlTransaction)transaction;
                                await sqlTransaction.CommitAsync();
                                transactionCompleted = true;

                                return new List<ComplianceOutput_LIST_NT>
                                {
                                    new ComplianceOutput_LIST_NT
                                    {
                                        STATUS = "Ok",
                                        MESSAGE = "Successfully Done",
                                        DATA = GetCompliance
                                    }
                                };
                            }

                        }
                        else
                        {
                            return new List<ComplianceOutput_LIST_NT>
                            {
                                new ComplianceOutput_LIST_NT
                                {
                                    STATUS = "Error",
                                    MESSAGE = "Error occured",
                                    DATA = null
                                }
                            };

                        }
                    }
                    catch (SqlException ex)
                    {
                        return new List<ComplianceOutput_LIST_NT>
                                {
                                    new ComplianceOutput_LIST_NT
                                    {
                                        STATUS = "Error",
                                        MESSAGE = ex.Message,
                                        DATA = null
                                    }
                                };

                    }

                }

                // Ensure that this method always returns a value
                return new List<ComplianceOutput_LIST_NT>
                    {
                        new ComplianceOutput_LIST_NT
                        {
                            STATUS = "Error",
                            MESSAGE = "No compliance data found or processed.",
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
        private List<ComplianceOutput_LIST> GenerateErrorResponse(string message)
        {
            return new List<ComplianceOutput_LIST>
            {
                new ComplianceOutput_LIST
                {
                    STATUS = "Error",
                    MESSAGE = message,
                    DATA = null
                }
            };
        }
        private List<ComplianceOutput_LIST_NT> GenerateErrorResponseNT(string message)
        {
            return new List<ComplianceOutput_LIST_NT>
            {
                new ComplianceOutput_LIST_NT
                {
                    STATUS = "Error",
                    MESSAGE = message,
                    DATA = null
                }
            };
        }
    }
}
