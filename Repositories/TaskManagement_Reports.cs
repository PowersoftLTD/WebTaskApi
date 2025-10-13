using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Data;
using System.Data.SqlClient;
using TaskManagement.API.Interfaces;
using TaskManagement.API.Model;

namespace TaskManagement.API.Repositories
{
    public class TaskManagement_Reports : ITaskManagement_Reports
    {
        private readonly HostEnvironment _env;
        private static TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        public IDapperDbConnection _dapperDbConnection;
        private readonly FileSettings _fileSettings;
        public TaskManagement_Reports(IDapperDbConnection dapperDbConnection, IOptions<FileSettings> fileSettings, IOptions<HostEnvironment> env)
        {
            _env = env.Value;
            _dapperDbConnection = dapperDbConnection;
            _fileSettings = fileSettings.Value;
        }

        public async Task<ActionResult<IEnumerable<TaskDashBoardFilterOutputListNT>>> TaskDashBoardFilterAsynNT(Doc_Type_Doc_CategoryInput doc_Type_Doc_CategoryInput)
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
                    parmeters.Add("@Session_User_Id", doc_Type_Doc_CategoryInput.Session_User_Id);
                    parmeters.Add("@Business_Group_Id", doc_Type_Doc_CategoryInput.Business_Group_Id);
                    parmeters.Add("@PropertyMkey", doc_Type_Doc_CategoryInput.PropertyMkey);

                    var TaskDashFilter = await db.QueryAsync<TaskDashBoardUserFilterNT>("SP_GET_TASK_DASHBOARD_REPORT_FILTER", parmeters, commandType: CommandType.StoredProcedure, transaction: transaction);

                    var sqlTransaction = (SqlTransaction)transaction;
                    await sqlTransaction.CommitAsync();
                    transactionCompleted = true;

                    var successsResult = new List<TaskDashBoardFilterOutputListNT>
                    {
                        new TaskDashBoardFilterOutputListNT
                        {
                            STATUS = "Ok",
                            MESSAGE = "Get data successfully!!!",
                            User_Filter = TaskDashFilter
                            //Priority_Filter = PriorityFilter,
                            //Duration_Filter = DurationFilter,
                            //Task_Type = TaskType
                        }
                    };
                    return successsResult;
                }
            }
            catch (SqlException sqlEx)
            {
                // Handle SQL exceptions specifically
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

                // Log the SQL error
                var errorResult = new List<TaskDashBoardFilterOutputListNT>
                {
                    new TaskDashBoardFilterOutputListNT
                    {
                        STATUS = "Error",
                        MESSAGE = $"SQL Error: {sqlEx.Message}",
                        User_Filter = null
                    }
                };
                return errorResult;
            }
            catch (Exception ex)
            {
                // Generic error handling for non-SQL related issues
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

                // Log the generic error
                var errorResult = new List<TaskDashBoardFilterOutputListNT>
                        {
                            new TaskDashBoardFilterOutputListNT
                            {
                                STATUS = "Error",
                                MESSAGE = ex.Message,
                                User_Filter = null
                            }
                        };
                return errorResult;
            }
            finally
            {
                // Ensure transaction is committed or rolled back appropriately
                if (transaction != null && !transactionCompleted)
                {
                    try
                    {
                        transaction.Rollback();  // Rollback in case of any issues
                    }
                    catch (Exception rollbackEx)
                    {
                        Console.WriteLine($"Final rollback failed: {rollbackEx.Message}");
                    }
                }
            }
        }

        public async Task<IEnumerable<Task_DetailsOutPutNT_List>> GetTaskDetailsNTAsync(Task_DetailsInputNT task_DetailsInputNT)
        {
            try
            {
                DataSet dsTaskDash = new DataSet();
                using (IDbConnection db = _dapperDbConnection.CreateConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@CURRENT_EMP_MKEY", task_DetailsInputNT.CURRENT_EMP_MKEY);
                    parmeters.Add("@USER_FILTER", task_DetailsInputNT.FILTER);
                    //parmeters.Add("@DURATION_FILTER", task_DetailsInputNT.DURATION_FILTER);
                    //parmeters.Add("@STATUS_FILTER", task_DetailsInputNT.Status_Filter);
                    //parmeters.Add("@PriorityFilter", task_DetailsInputNT.PriorityFilter);
                    //parmeters.Add("@TypeFilter", task_DetailsInputNT.TypeFilter);
                    parmeters.Add("@Session_User_Id", task_DetailsInputNT.Session_User_ID);
                    parmeters.Add("@Business_Group_Id", task_DetailsInputNT.Business_Group_ID);
                    var result = await db.QueryMultipleAsync("SP_TASK_DASHBOARD_ REPORT_NT", parmeters, commandType: CommandType.StoredProcedure);

                    var data = result.Read<Task_DetailsOutPutNT>().ToList();
                    var data1 = result.Read<TaskDashboardCount_NT>().ToList();

                    var successsResult = new List<Task_DetailsOutPutNT_List>
                    {
                        new Task_DetailsOutPutNT_List
                        {
                            Status = "Ok",
                            Message = "Message",
                            Data= data,
                            Data1 = data1
                        }
                    };
                    return successsResult;
                }
            }
            catch (Exception ex)
            {
                var errorResult = new List<Task_DetailsOutPutNT_List>
                    {
                        new Task_DetailsOutPutNT_List
                        {
                            Status = "Error",
                            Message = ex.Message,
                            Data = null,
                            Data1 = null
                        }
                    };
                return errorResult;
            }
        }

    }
}
