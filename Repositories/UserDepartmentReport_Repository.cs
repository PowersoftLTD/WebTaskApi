using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Data;
using System.Data.SqlClient;
using TaskManagement.API.Interfaces;
using TaskManagement.API.Model;

namespace TaskManagement.API.Repositories
{
    public class UserDepartmentReport_Repository : IUserDepartmentReport
    {
        private readonly HostEnvironment _env;
        private static TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        public IDapperDbConnection _dapperDbConnection;
        private readonly FileSettings _fileSettings;
        public UserDepartmentReport_Repository(IDapperDbConnection dapperDbConnection, IOptions<FileSettings> fileSettings, IOptions<HostEnvironment> env)
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

                    var TaskDashFilter = await db.QueryAsync<TaskDashBoardUserFilterNT>("SP_GET_USERDEPARTMENT_REPORT_FILTER", parmeters, commandType: CommandType.StoredProcedure, transaction: transaction);

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
        
        public async Task<IEnumerable<Task_DetailsOutPutNT_List>> GetTaskDetailsNTAsync(Task_UserDepartmentDownloadDetailsInputNT task_DetailsDownloadInputNT)
        {
            try
            {
                DataSet dsTaskDash = new DataSet();
                using (IDbConnection db = _dapperDbConnection.CreateConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@CURRENT_EMP_MKEY", task_DetailsDownloadInputNT.CURRENT_EMP_MKEY);
                    parmeters.Add("@USER_FILTER", task_DetailsDownloadInputNT.FILTER);
                    //parmeters.Add("@DURATION_FILTER", task_DetailsInputNT.DURATION_FILTER);0
                    //parmeters.Add("@STATUS_FILTER", task_DetailsInputNT.Status_Filter);
                    //parmeters.Add("@PriorityFilter", task_DetailsInputNT.PriorityFilter);
                    //parmeters.Add("@TypeFilter", task_DetailsInputNT.TypeFilter);
                    parmeters.Add("@Session_User_Id", task_DetailsDownloadInputNT.Session_User_ID);
                    parmeters.Add("@Business_Group_Id", task_DetailsDownloadInputNT.Business_Group_ID);
                    parmeters.Add("@Departmentfilter", task_DetailsDownloadInputNT.Departmentfilter);
                    parmeters.Add("@Employeefilter", task_DetailsDownloadInputNT.EmployeeFiletr);
                    parmeters.Add("@Type", task_DetailsDownloadInputNT.Type);
                    var result = await db.QueryMultipleAsync("SP_USERdEPARTMENT_REPORT_NT", parmeters, commandType: CommandType.StoredProcedure);

                    var data = result.Read<Task_DetailsOutPutNT>().ToList();
                    var data1 = result.Read<TaskDashboardCount_NT>().ToList();
                    //var totalCount = result.Read<int>();
                    var successsResult = new List<Task_DetailsOutPutNT_List>
                    {
                        new Task_DetailsOutPutNT_List
                        {
                            Status = "Ok",
                            Message = "Message",
                            Data= data,
                            Data1 = data1,
                            Data2= new List<int>()
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

        public async Task<IEnumerable<Task_UserDepartmentOutPutNT_List>> GetDepertmentListNTAsyn()
        {
            try
            {
                DataSet dsTaskDash = new DataSet();
                using (IDbConnection db = _dapperDbConnection.CreateConnection())
                {
                    var parmeters = new DynamicParameters();
                    var result = await db.QueryAsync<Department_V>("SELECT * FROM Dept_V", commandType: CommandType.Text);
                    var successsResult = new List<Task_UserDepartmentOutPutNT_List>
                    {
                        new Task_UserDepartmentOutPutNT_List
                        {
                            Status = "Ok",
                            Message = "Message",
                            Data= result,
                           // Data1 = data1
                        }
                    };
                    return successsResult;
                }
            }
            catch (Exception ex)
            {
                var errorResult = new List<Task_UserDepartmentOutPutNT_List>
                    {
                        new Task_UserDepartmentOutPutNT_List
                        {
                            Status = "Error",
                            Message = ex.Message,
                            Data = null,
                           // Data1 = null
                        }
                    };
                return errorResult;
            }
        }

        public async Task<IEnumerable<Task_userDepartMentResponseOutPUt_NT>> GetEmployeeDetails_ByDepartmentId(userDepartment department)
        {
            try
            {
                //if (string.IsNullOrEmpty(department.departmentId))
                //{
                //    var successsResult = new List<Task_userDepartMentResponseOutPUt_NT>
                //    {
                //        new Task_userDepartMentResponseOutPUt_NT
                //        {
                //            Status = "Ok",
                //            Message = "No Employee Available!",
                //            Data= null,
                //           // Data1 = data1
                //        }
                //    };
                //    return successsResult;
                //}
                using (IDbConnection db = _dapperDbConnection.CreateConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@departmentd", department.departmentId);
                    var result = await db.QueryAsync<EmployeeDetails_ByDepartmentId>("SP_GET_EmployeeListDetails_ByDepartmentid", parmeters, commandType: CommandType.StoredProcedure);
                    var successsResult = new List<Task_userDepartMentResponseOutPUt_NT>
                    {
                        new Task_userDepartMentResponseOutPUt_NT
                        {
                            Status = "Ok",
                            Message = "Message",
                            Data= result.ToList(),
                           // Data1 = data1
                        }
                    };
                    return successsResult;

                }
            }
            catch (Exception ex)
            {
                var errorResult = new List<Task_userDepartMentResponseOutPUt_NT>
                    {
                        new Task_userDepartMentResponseOutPUt_NT
                        {
                            Status = "Error",
                            Message = ex.Message,
                            Data = null,
                           // Data1 = null
                        }
                    };
                return errorResult;

            }
        }

        public async Task<IEnumerable<Task_DetailsOutPutNT_List>> GetTaskDetailsNTAsync_NT(Task_UserDepartmentDetailsInputNT task_DetailsInputNT)
        {
            try
            {
                DataSet dsTaskDash = new DataSet();
                using (IDbConnection db = _dapperDbConnection.CreateConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@CURRENT_EMP_MKEY", task_DetailsInputNT.CURRENT_EMP_MKEY);
                    parmeters.Add("@USER_FILTER", task_DetailsInputNT.FILTER);
                    //parmeters.Add("@DURATION_FILTER", task_DetailsInputNT.DURATION_FILTER);0
                    //parmeters.Add("@STATUS_FILTER", task_DetailsInputNT.Status_Filter);
                    //parmeters.Add("@PriorityFilter", task_DetailsInputNT.PriorityFilter);
                    //parmeters.Add("@TypeFilter", task_DetailsInputNT.TypeFilter);
                    parmeters.Add("@Session_User_Id", task_DetailsInputNT.Session_User_ID);
                    parmeters.Add("@Business_Group_Id", task_DetailsInputNT.Business_Group_ID);
                    parmeters.Add("@Departmentfilter", task_DetailsInputNT.Departmentfilter);
                    parmeters.Add("@Employeefilter", task_DetailsInputNT.EmployeeFiletr);
                    parmeters.Add("@Type", task_DetailsInputNT.Type);
                    // Pagination params only - no count
                    int? pageNumber = task_DetailsInputNT.PageNumber <= 0 ? 1 : task_DetailsInputNT.PageNumber;
                    int? pageSize = task_DetailsInputNT.PageSize <= 0 ? 50 : task_DetailsInputNT.PageSize;

                    // Set maximum page size to prevent excessive data retrieval
                    if (pageSize > 1000)
                        pageSize = 1000;

                    parmeters.Add("@PageNumber", pageNumber);
                    parmeters.Add("@PageSize", pageSize);
                    //var result = await db.QueryMultipleAsync("SP_USERdEPARTMENT_REPORT_NT", parmeters, commandType: CommandType.StoredProcedure);
                    var result = await db.QueryMultipleAsync("SP_DEPARTMENTVIEW_REPORT_NT", parmeters, commandType: CommandType.StoredProcedure);

                    var data = result.Read<Task_DetailsOutPutNT>().ToList();
                    var data1 = result.Read<TaskDashboardCount_NT>().ToList();
                    var totalCount = result.Read<int>();

                    var successsResult = new List<Task_DetailsOutPutNT_List>
                    {
                        new Task_DetailsOutPutNT_List
                        {
                            Status = "Ok",
                            Message = "Message",
                            Data= data,
                            Data1 = data1,
                            Data2=totalCount
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
        #region

        //public async Task<IEnumerable<Task_userDepartMentResponseOutPUt_NT>> GetEmployeeDetails_ByDepartmentId(string departmentd)
        //{
        //    try
        //    {
        //        using (IDbConnection db = _dapperDbConnection.CreateConnection())
        //        {
        //            var parmeters = new DynamicParameters();
        //            parmeters.Add("@departmentd", departmentd);
        //            var result = await db.QueryAsync<EmployeeDetails_ByDepartmentId>("SP_GET_EmployeeListDetails_ByDepartmentid", parmeters, commandType: CommandType.StoredProcedure);
        //            var successsResult = new List<Task_userDepartMentResponseOutPUt_NT>
        //            {
        //                new Task_userDepartMentResponseOutPUt_NT
        //                {
        //                    Status = "Ok",
        //                    Message = "Message",
        //                    Data= result.ToList(),
        //                   // Data1 = data1
        //                }
        //            };
        //            return successsResult;

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        var errorResult = new List<Task_userDepartMentResponseOutPUt_NT>
        //            {
        //                new Task_userDepartMentResponseOutPUt_NT
        //                {
        //                    Status = "Error",
        //                    Message = ex.Message,
        //                    Data = null,
        //                   // Data1 = null
        //                }
        //            };
        //        return errorResult;

        //    }
        //}

        #endregion
    }
}
