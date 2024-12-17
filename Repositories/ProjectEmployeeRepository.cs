using Dapper;
using System;
using System.Data;
using TaskManagement.API.Interfaces;
using TaskManagement.API.Model;

namespace TaskManagement.API.Repositories
{
    public class ProjectEmployeeRepository : IProjectEmployee
    {
        private static TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        public IDapperDbConnection _dapperDbConnection;
        private readonly string _connectionString;

        public ProjectEmployeeRepository(IDapperDbConnection dapperDbConnection, string connectionString)
        {
            _dapperDbConnection = dapperDbConnection;
            _connectionString = connectionString;
        }

        public async Task<EmployeeCompanyMST> Login_Validate(string Login_ID, string LOGIN_PASSWORD)
        {
            try
            {
                using (IDbConnection db = _dapperDbConnection.CreateConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@LoginName", Login_ID);
                    parmeters.Add("@P_LOGIN_PASSWORD", LOGIN_PASSWORD);

                    var LoginDetails = await db.QueryFirstOrDefaultAsync<EmployeeCompanyMST>("SP_GetLoginUser", parmeters, commandType: CommandType.StoredProcedure);

                    if (LoginDetails == null)
                    {
                        var employeeCompanyMST = new EmployeeCompanyMST();
                        employeeCompanyMST.STATUS = "Error";
                        employeeCompanyMST.MESSAGE = "An unexpected error occurred while retrieving the Login Details.";
                        return employeeCompanyMST; // Return null if no results
                    }

                    LoginDetails.STATUS = "Ok";
                    LoginDetails.MESSAGE = "Get Login Details.";
                    return LoginDetails; // Return null if no results
                }
            }
            catch (Exception ex)
            {
                var employeeCompanyMST = new EmployeeCompanyMST();
                employeeCompanyMST.STATUS = "Error";
                employeeCompanyMST.MESSAGE = ex.Message;
                return employeeCompanyMST; // Return null if no results
            }
        }

        public async Task<IEnumerable<V_Building_Classification>> GetProjectAsync(string TYPE_CODE, decimal MASTER_MKEY)
        {
            try
            {
                using (IDbConnection db = _dapperDbConnection.CreateConnection())
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@TYPE_CODE", TYPE_CODE);
                    parameters.Add("@MASTER_MKEY", MASTER_MKEY);

                    // Await the Task to get the IEnumerable result and then call ToList()
                    var ProjectDetails = (await db.QueryAsync<V_Building_Classification>("SP_GET_PROJECT", parameters, commandType: CommandType.StoredProcedure)).ToList();

                    return ProjectDetails;
                }
            }
            catch (Exception ex)
            {
                var errorResult = new List<V_Building_Classification>
                    {
                        new V_Building_Classification
                        {
                            Status = "Error",
                            Message = ex.Message
                        }
                    };
                return errorResult;
            }
        }

        public async Task<IEnumerable<V_Building_Classification>> GetSubProjectAsync(string Project_Mkey)
        {
            try
            {
                using (IDbConnection db = _dapperDbConnection.CreateConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@PROJECT_MKEY", Project_Mkey);
                    var ProjectDetails = (await db.QueryAsync<V_Building_Classification>("SP_GET_SUBPROJECT", parmeters, commandType: CommandType.StoredProcedure)).ToList();
                    return ProjectDetails;
                }
            }
            catch (Exception ex)
            {
                var errorResult = new List<V_Building_Classification>
                    {
                        new V_Building_Classification
                        {
                            Status = "Error",
                            Message = ex.Message
                        }
                    };
                return errorResult;
            }
        }

        public async Task<IEnumerable<EmployeeCompanyMST>> GetEmpAsync(int CURRENT_EMP_MKEY, string FILTER)
        {
            try
            {
                using (IDbConnection db = _dapperDbConnection.CreateConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@CURRENT_EMP_MKEY", CURRENT_EMP_MKEY);
                    parmeters.Add("@FILTER", FILTER);
                    var EmployeeDetails = (await db.QueryAsync<EmployeeCompanyMST>("SP_GET_EMP", parmeters, commandType: CommandType.StoredProcedure)).ToList();
                    return EmployeeDetails;
                }
            }
            catch (Exception ex)
            {
                var errorResult = new List<EmployeeCompanyMST>
                    {
                        new EmployeeCompanyMST
                        {
                            STATUS = "Error",
                            MESSAGE = ex.Message
                        }
                    };
                return errorResult;
            }
        }

        Task<TASK_HDR> IProjectEmployee.AddTaskAsync(TASK_HDR tASK_HDR)
        {
            throw new NotImplementedException();
        }

        Task<TASK_HDR> IProjectEmployee.UpdateTaskAsync(TASK_HDR tASK_HDR)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<EmployeeCompanyMST>> GetAssignedToAsync(string AssignNameLike)
        {
            try
            {
                using (IDbConnection db = _dapperDbConnection.CreateConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@term", AssignNameLike);
                    var AssignToDetails = (await db.QueryAsync<EmployeeCompanyMST>("SP_AssignedTo", parmeters, commandType: CommandType.StoredProcedure)).ToList();
                    return AssignToDetails;
                }
            }
            catch (Exception ex)
            {
                var errorResult = new List<EmployeeCompanyMST>
                    {
                        new EmployeeCompanyMST
                        {
                            STATUS = "Error",
                            MESSAGE = ex.Message
                        }
                    };
                return errorResult;
            }
        }

        public async Task<IEnumerable<EmployeeCompanyMST>> GetEmpTagsAsync(string EMP_TAGS)
        {
            try
            {
                using (IDbConnection db = _dapperDbConnection.CreateConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@EMP_MKEY", EMP_TAGS);
                    var AssignToDetails = (await db.QueryAsync<EmployeeCompanyMST>("sp_EMP_TAGS", parmeters, commandType: CommandType.StoredProcedure)).ToList();
                    return AssignToDetails;
                }
            }
            catch (Exception ex)
            {
                var errorResult = new List<EmployeeCompanyMST>
                    {
                        new EmployeeCompanyMST
                        {
                            STATUS = "Error",
                            MESSAGE = ex.Message
                        }
                    };
                return errorResult;
            }
        }

        public async Task<IEnumerable<TASK_DASHBOARD>> GetTaskDetailsAsync(int CURRENT_EMP_MKEY, string FILTER)
        {
            try
            {
                using (IDbConnection db = _dapperDbConnection.CreateConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@CURRENT_EMP_MKEY", CURRENT_EMP_MKEY);
                    parmeters.Add("@FILTER", FILTER);
                    var TaskDashDetails = (await db.QueryAsync<TASK_DASHBOARD>("SP_TASK_DASHBOARD", parmeters, commandType: CommandType.StoredProcedure)).ToList();
                    return TaskDashDetails;
                }
            }
            catch (Exception ex)
            {
                var errorResult = new List<TASK_DASHBOARD>
                    {
                        new TASK_DASHBOARD
                        {
                            RESPONE_STATUS = "Error",
                            RESPONSE_MESSAGE = ex.Message
                        }
                    };
                return errorResult;
            }
        }

        public async Task<IEnumerable<TASK_DETAILS_BY_MKEY>> GetTaskDetailsByMkeyAsync(string Mkey)
        {
            try
            {
                using (IDbConnection db = _dapperDbConnection.CreateConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@HDR_MKEY", Mkey);
                    var TaskDashDetails = (await db.QueryAsync<TASK_DETAILS_BY_MKEY>("SP_TASK_DETAILS_BY_MKEY", parmeters, commandType: CommandType.StoredProcedure)).ToList();
                    return TaskDashDetails;
                }
            }
            catch (Exception ex)
            {
                var errorResult = new List<TASK_DETAILS_BY_MKEY>
                    {
                        new TASK_DETAILS_BY_MKEY
                        {
                            RESPONSE_STATUS = "Error",
                            RESPONSE_MESSAGE = ex.Message
                        }
                    };
                return errorResult;
            }
        }
        public async Task<IEnumerable<TASK_DASHBOARD>> GetTaskNestedGridAsync(string Mkey)
        {
            try
            {
                using (IDbConnection db = _dapperDbConnection.CreateConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@TASK_MKEY", Mkey);
                    parmeters.Add("@Completed", null);
                    var TaskTreeDetails = (await db.QueryAsync<TASK_DASHBOARD>("SP_GET_TASK_TREE", parmeters, commandType: CommandType.StoredProcedure)).ToList();
                    return TaskTreeDetails;
                }
            }
            catch (Exception ex)
            {
                var errorResult = new List<TASK_DASHBOARD>
                    {
                        new TASK_DASHBOARD
                        {
                            RESPONE_STATUS = "Error",
                            RESPONSE_MESSAGE = ex.Message
                        }
                    };
                return errorResult;
            }
        }

        public async Task<IEnumerable<TASK_ACTION_TRL>> GetActionsAsync(int TASK_MKEY, int CURRENT_EMP_MKEY, string CURR_ACTION)
        {
            try
            {
                using (IDbConnection db = _dapperDbConnection.CreateConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@TASK_MKEY", TASK_MKEY);
                    parmeters.Add("@CURRENT_EMP_MKEY", CURRENT_EMP_MKEY);
                    parmeters.Add("@CURR_ACTION", CURR_ACTION);
                    var TaskTreeDetails = (await db.QueryAsync<TASK_ACTION_TRL>("SP_GET_ACTIONS", parmeters, commandType: CommandType.StoredProcedure)).ToList();
                    return TaskTreeDetails;
                }
            }
            catch (Exception ex)
            {
                var errorResult = new List<TASK_ACTION_TRL>
                    {
                        new TASK_ACTION_TRL
                        {
                           RESPONSE_STATUS = "Error",
                            RESPONSE_MESSAGE = ex.Message
                        }
                    };
                return errorResult;
            }
        }

        public async Task<IEnumerable<TASK_DASHBOARD>> GetTaskTreeAsync(string Mkey)
        {
            try
            {
                using (IDbConnection db = _dapperDbConnection.CreateConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@TASK_MKEY", Mkey);
                    parmeters.Add("@Completed", null);
                    var TaskTreeDetails = (await db.QueryAsync<TASK_DASHBOARD>("SP_GET_TASK_TREE", parmeters, commandType: CommandType.StoredProcedure)).ToList();
                    return TaskTreeDetails;
                }
            }
            catch (Exception ex)
            {
                var errorResult = new List<TASK_DASHBOARD>
                    {
                        new TASK_DASHBOARD
                        {
                            RESPONE_STATUS = "Error",
                            RESPONSE_MESSAGE = ex.Message
                        }
                    };
                return errorResult;
            }
        }

        public async Task<EmployeeCompanyMST> PutChangePasswordAsync(string LoginName, string Old_Password, string New_Password)
        {
            try
            {
                using (IDbConnection db = _dapperDbConnection.CreateConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@LoginName", LoginName);
                    parmeters.Add("@Old_LOGIN_PASSWORD", Old_Password);
                    parmeters.Add("@New_LOGIN_PASSWORD", New_Password);
                    var ChangePass = await db.QueryFirstOrDefaultAsync<EmployeeCompanyMST>("Sp_USER_ChangeLOGIN_PASSWORD", parmeters, commandType: CommandType.StoredProcedure);
                    return ChangePass;
                }
            }
            catch (Exception ex)
            {
                var errorResult = new EmployeeCompanyMST
                {
                    STATUS = "Error",
                    MESSAGE = ex.Message
                };
                return errorResult;
            }
        }

        public async Task<EmployeeCompanyMST> GetForgotPasswordAsync(string LoginName)
        {
            try
            {
                using (IDbConnection db = _dapperDbConnection.CreateConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@LoginName", LoginName);
                    var ForgotPass = await db.QueryFirstOrDefaultAsync<EmployeeCompanyMST>("Sp_USER_ForgotPassword", parmeters, commandType: CommandType.StoredProcedure);
                    return ForgotPass;
                }
            }
            catch (Exception ex)
            {
                var errorResult = new EmployeeCompanyMST
                {
                    STATUS = "Error",
                    MESSAGE = ex.Message
                };
                return errorResult;
            }
        }


        public async Task<EmployeeCompanyMST> GetResetPasswordAsync(string TEMPPASSWORD, string LoginName)
        {
            try
            {
                using (IDbConnection db = _dapperDbConnection.CreateConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@TEMPPASSWORD", TEMPPASSWORD);
                    parmeters.Add("@LoginName", LoginName);
                    var ResetPass = await db.QueryFirstOrDefaultAsync<EmployeeCompanyMST>("sp_reset_password", parmeters, commandType: CommandType.StoredProcedure);
                    return ResetPass;
                }
            }
            catch (Exception ex)
            {
                var errorResult = new EmployeeCompanyMST
                {
                    STATUS = "Error",
                    MESSAGE = ex.Message
                };
                return errorResult;
            }
        }

        public async Task<EmployeeCompanyMST> GetValidateEmailAsync(string Login_ID)
        {
            try
            {
                using (IDbConnection db = _dapperDbConnection.CreateConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@LoginName", Login_ID);
                    var ValidateEmail = await db.QueryFirstOrDefaultAsync<EmployeeCompanyMST>("Sp_validate_login", parmeters, commandType: CommandType.StoredProcedure);
                    return ValidateEmail;
                }
            }
            catch (Exception ex)
            {
                var errorResult = new EmployeeCompanyMST
                {
                    STATUS = "Error",
                    MESSAGE = ex.Message
                };
                return errorResult;
            }
        }

        public async Task<IEnumerable<TASK_DASHBOARD>> GetTaskDashboardDetailsAsync(string CURRENT_EMP_MKEY, string CURR_ACTION)
        {
            try
            {
                using (IDbConnection db = _dapperDbConnection.CreateConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@CURRENT_EMP_MKEY", CURRENT_EMP_MKEY);
                    parmeters.Add("@CURR_ACTION", CURR_ACTION);
                    var ValidateEmail = (await db.QueryAsync<TASK_DASHBOARD>("SP_Get_Overall_DB", parmeters, commandType: CommandType.StoredProcedure)).ToList();
                    return ValidateEmail;
                }
            }
            catch (Exception ex)
            {
                var errorResult = new List<TASK_DASHBOARD>
                    {
                        new TASK_DASHBOARD
                        {
                            RESPONE_STATUS = "Error",
                            RESPONSE_MESSAGE = ex.Message
                        }
                    };
                return errorResult;
            }
        }

    }
}
