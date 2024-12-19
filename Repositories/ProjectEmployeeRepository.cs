﻿using Dapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Data;
using System.Data.SqlClient;
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

        public async Task<IEnumerable<EmployeeLoginOutput_LIST>> Login_Validate(string Login_ID, string LOGIN_PASSWORD)
        {
            try
            {
                using (IDbConnection db = _dapperDbConnection.CreateConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@LoginName", Login_ID);
                    parmeters.Add("@P_LOGIN_PASSWORD", LOGIN_PASSWORD);

                    var dtReponse = await db.QueryAsync<EmployeeLoginOutput>("SP_GetLoginUser", parmeters, commandType: CommandType.StoredProcedure);

                    var successsResult = new List<EmployeeLoginOutput_LIST>
                    {
                        new EmployeeLoginOutput_LIST
                        {
                            Status = "Ok",
                            Message = "Message",
                            Data= dtReponse

                        }
                    };
                    return successsResult;
                }
            }
            catch (Exception ex)
            {
                var errorResult = new List<EmployeeLoginOutput_LIST>
                    {
                        new EmployeeLoginOutput_LIST
                        {
                            Status = "Error",
                            Message = ex.Message,
                            Data=null

                        }
                    };
                return errorResult;
            }
        }

        public async Task<IEnumerable<V_Building_Classification_new>> GetProjectAsync(string TYPE_CODE, string MASTER_MKEY)
        {
            try
            {
                using (IDbConnection db = _dapperDbConnection.CreateConnection())
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@TYPE_CODE", TYPE_CODE);
                    parameters.Add("@MASTER_MKEY", MASTER_MKEY);

                    var result = await db.QueryAsync<V_Building_Classification_TMS>("SP_GET_PROJECT", parameters, commandType: CommandType.StoredProcedure);

                    var successsResult = new List<V_Building_Classification_new>
                    {
                        new V_Building_Classification_new
                        {
                            Status = "Ok",
                            Message = "Message",
                            Data= result

                        }
                    };
                    return successsResult;
                }
            }
            catch (Exception ex)
            {
                var errorResult = new List<V_Building_Classification_new>
                    {
                        new V_Building_Classification_new
                        {
                            Status = "Error",
                            Message = ex.Message,
                            Data=null

                        }
                    };
                return errorResult;
            }
        }

        public async Task<IEnumerable<V_Building_Classification_new>> GetSubProjectAsync(string Project_Mkey)
        {
            try
            {
                using (IDbConnection db = _dapperDbConnection.CreateConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@PROJECT_MKEY", Project_Mkey);
                    var ProjectDetails = (await db.QueryAsync<V_Building_Classification_TMS>("SP_GET_SUBPROJECT", parmeters, commandType: CommandType.StoredProcedure));
                    //  return ProjectDetails;
                    var successsResult = new List<V_Building_Classification_new>
                    {
                        new V_Building_Classification_new
                        {
                            Status = "Ok",
                            Message = "Message",
                            Data = ProjectDetails

                        }
                    };
                    return successsResult;
                }
            }
            catch (Exception ex)
            {
                //var errorResult = new List<V_Building_Classification_new>
                //    {
                //        new V_Building_Classification_new
                //        {
                //            Status = "Error",
                //            Message = ex.Message
                //        }
                //    };
                //return errorResult;
                var errorResult = new List<V_Building_Classification_new>
                    {
                        new V_Building_Classification_new
                        {
                            Status = "Error",
                            Message = ex.Message,
                            Data=null

                        }
                    };
                return errorResult;
            }
        }

        public async Task<IEnumerable<EmployeeLoginOutput_LIST>> GetEmpAsync(string CURRENT_EMP_MKEY, string FILTER)
        {
            try
            {
                using (IDbConnection db = _dapperDbConnection.CreateConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@CURRENT_EMP_MKEY", CURRENT_EMP_MKEY);
                    parmeters.Add("@FILTER", FILTER);
                    var EmployeeDetails = await db.QueryAsync<EmployeeLoginOutput>("SP_GET_EMP", parmeters, commandType: CommandType.StoredProcedure);
                    var successsResult = new List<EmployeeLoginOutput_LIST>
                    {
                        new EmployeeLoginOutput_LIST
                        {
                            Status = "Ok",
                            Message = "Message",
                            Data= EmployeeDetails

                        }
                    };
                    return successsResult;
                }
            }
            catch (Exception ex)
            {
                var errorResult = new List<EmployeeLoginOutput_LIST>
                    {
                        new EmployeeLoginOutput_LIST
                        {
                            Status = "Error",
                            Message = ex.Message,
                            Data=null

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

        public async Task<IEnumerable<EmployeeLoginOutput_LIST>> GetAssignedToAsync(string AssignNameLike)
        {
            try
            {
                using (IDbConnection db = _dapperDbConnection.CreateConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@term", AssignNameLike);
                    var AssignToDetails = await db.QueryAsync<EmployeeLoginOutput>("SP_AssignedTo", parmeters, commandType: CommandType.StoredProcedure);
                    var successsResult = new List<EmployeeLoginOutput_LIST>
                    {
                        new EmployeeLoginOutput_LIST
                        {
                            Status = "Ok",
                            Message = "Message",
                            Data= AssignToDetails
                        }
                    };
                    return successsResult;
                }
            }
            catch (Exception ex)
            {
                var errorResult = new List<EmployeeLoginOutput_LIST>
                    {
                        new EmployeeLoginOutput_LIST
                        {
                            Status = "Error",
                            Message = ex.Message,
                            Data=null
                        }
                    };
                return errorResult;
            }
        }

        public async Task<IEnumerable<EmployeeTagsOutPut_list>> GetEmpTagsAsync(string EMP_TAGS)
        {
            try
            {
                using (IDbConnection db = _dapperDbConnection.CreateConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@EMP_MKEY", EMP_TAGS);

                    var EmployeeDetails = await db.QueryAsync<EmployeeTagsOutPut>("sp_EMP_TAGS", parmeters, commandType: CommandType.StoredProcedure);
                    var successsResult = new List<EmployeeTagsOutPut_list>
                    {
                        new EmployeeTagsOutPut_list
                        {
                            Status = "Ok",
                            Message = "Message",
                            Data= EmployeeDetails
                        }
                    };
                    return successsResult;

                    //var AssignToDetails = await db.QueryAsync<EmployeeTagsOutPut>("sp_EMP_TAGS", parmeters, commandType: CommandType.StoredProcedure);
                    //var successsResult = new List<EmployeeTagsOutPut_list>
                    //{
                    //    new EmployeeTagsOutPut_list
                    //    {
                    //        Status = "Ok",
                    //        Message = "Message",
                    //        Data= AssignToDetails

                    //    }
                    //};
                    //return successsResult;
                }
            }
            catch (Exception ex)
            {
                var errorResult = new List<EmployeeTagsOutPut_list>
                    {
                        new EmployeeTagsOutPut_list
                        {
                            Status = "Error",
                            Message = ex.Message,
                            Data = null
                        }
                    };
                return errorResult;
            }
        }

        public async Task<IEnumerable<Task_DetailsOutPut_List>> GetTaskDetailsAsync(string CURRENT_EMP_MKEY, string FILTER)
        {
            try
            {
                using (IDbConnection db = _dapperDbConnection.CreateConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@CURRENT_EMP_MKEY", CURRENT_EMP_MKEY);
                    parmeters.Add("@FILTER", FILTER);
                    var TaskDashDetails = await db.QueryAsync<Task_DetailsOutPut>("SP_TASK_DASHBOARD", parmeters, commandType: CommandType.StoredProcedure);
                    var successsResult = new List<Task_DetailsOutPut_List>
                    {
                        new Task_DetailsOutPut_List
                        {
                            Status = "Ok",
                            Message = "Message",
                            Data= TaskDashDetails

                        }
                    };
                    return successsResult;
                }
            }
            catch (Exception ex)
            {
                var errorResult = new List<Task_DetailsOutPut_List>
                    {
                        new Task_DetailsOutPut_List
                        {
                            Status = "Error",
                            Message = ex.Message,
                            Data = null
                        }
                    };
                return errorResult;
            }
        }

        public async Task<IEnumerable<TASK_DETAILS_BY_MKEY_list>> GetTaskDetailsByMkeyAsync(string Mkey)
        {
            try
            {
                using (IDbConnection db = _dapperDbConnection.CreateConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@HDR_MKEY", Mkey);
                    var TaskDashDetails = await db.QueryAsync<TASK_DETAILS_BY_MKEY>("SP_TASK_DETAILS_BY_MKEY", parmeters, commandType: CommandType.StoredProcedure);
                    var successsResult = new List<TASK_DETAILS_BY_MKEY_list>
                    {
                        new TASK_DETAILS_BY_MKEY_list
                        {
                            Status = "Ok",
                            Message = "Message",
                            Data= TaskDashDetails

                        }
                    };
                    return successsResult;
                }
            }
            catch (Exception ex)
            {
                var errorResult = new List<TASK_DETAILS_BY_MKEY_list>
                    {
                        new TASK_DETAILS_BY_MKEY_list
                        {
                            Status = "Error",
                            Message = ex.Message
                        }
                    };
                return errorResult;
            }
        }
        public async Task<IEnumerable<TASK_NESTED_GRIDOutPut_List>> GetTaskNestedGridAsync(string Mkey)
        {
            try
            {
                using (IDbConnection db = _dapperDbConnection.CreateConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@TASK_MKEY", Mkey);
                    parmeters.Add("@Completed", null);
                    var TaskTreeDetails = (await db.QueryAsync<TASK_NESTED_GRIDOutPut>("SP_GET_TASK_TREE", parmeters, commandType: CommandType.StoredProcedure)).ToList();
                    var successsResult = new List<TASK_NESTED_GRIDOutPut_List>
                    {
                        new TASK_NESTED_GRIDOutPut_List
                        {
                            Status = "Ok",
                            Message = "Message",
                            Data= TaskTreeDetails

                        }
                    };
                    return successsResult;
                }
            }
            catch (Exception ex)
            {
                var errorResult = new List<TASK_NESTED_GRIDOutPut_List>
                    {
                        new TASK_NESTED_GRIDOutPut_List
                        {
                            Status = "Error",
                            Message = ex.Message
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
        public async Task<IEnumerable<TASK_DASHBOARD>> GetTeamTaskAsync(string CURRENT_EMP_MKEY)
        {
            try
            {
                using (IDbConnection db = _dapperDbConnection.CreateConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@CURRENT_EMP_MKEY", CURRENT_EMP_MKEY);
                    var TeamTask = (await db.QueryAsync<TASK_DASHBOARD>("SP_GET_TEAM_PROGRESS", parmeters, commandType: CommandType.StoredProcedure)).ToList();
                    return TeamTask;
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

        public async Task<IEnumerable<TASK_DASHBOARD>> GetTeamTaskDetailsAsync(string CURRENT_EMP_MKEY)
        {
            try
            {
                using (IDbConnection db = _dapperDbConnection.CreateConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@CURRENT_EMP_MKEY", CURRENT_EMP_MKEY);
                    var TeamTask = (await db.QueryAsync<TASK_DASHBOARD>("SP_GET_TEAM_PROGRESS", parmeters, commandType: CommandType.StoredProcedure)).ToList();
                    return TeamTask;
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

        public async Task<IEnumerable<TASK_HDR>> GetProjectDetailsWithSubProjectAsync(string ProjectID, string SubProjectID)
        {
            try
            {
                using (IDbConnection db = _dapperDbConnection.CreateConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@PROJECTID", ProjectID);
                    parmeters.Add("@SUBPROJECTID", SubProjectID);
                    var ProjectDetails = (await db.QueryAsync<TASK_HDR>("USP_GET_pROJECTPREVIEW", parmeters, commandType: CommandType.StoredProcedure)).ToList();
                    return ProjectDetails;
                }
            }
            catch (Exception ex)
            {
                var errorResult = new List<TASK_HDR>
                    {
                        new TASK_HDR
                        {
                            STATUS = "Error",
                            Message = ex.Message
                        }
                    };
                return errorResult;
            }
        }

        public async Task<IEnumerable<TASK_HDR>> GetTaskTreeExportAsync(string Task_Mkey)
        {
            try
            {
                using (IDbConnection db = _dapperDbConnection.CreateConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@TASK_MKEY", Task_Mkey);
                    var TaskDetails = (await db.QueryAsync<TASK_HDR>("SP_GET_TASK_TREE", parmeters, commandType: CommandType.StoredProcedure)).ToList();
                    return TaskDetails;
                }
            }
            catch (Exception ex)
            {
                var errorResult = new List<TASK_HDR>
                    {
                        new TASK_HDR
                        {
                            STATUS = "Error",
                            Message = ex.Message
                        }
                    };
                return errorResult;
            }
        }

        public async Task<TASK_HDR> CreateAddTaskAsync(Add_TaskInput tASK_HDR)
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

                    if (tASK_HDR.TASK_NO == "0000")
                    {
                        var parmeters = new DynamicParameters();
                        parmeters.Add("@TASK_NO", tASK_HDR.TASK_NO);
                        parmeters.Add("@TASK_NAME", tASK_HDR.TASK_NAME);
                        parmeters.Add("@TASK_DESCRIPTION", tASK_HDR.TASK_DESCRIPTION);
                        parmeters.Add("@CATEGORY", tASK_HDR.CATEGORY);
                        parmeters.Add("@PROJECT_ID", tASK_HDR.PROJECT_ID);
                        parmeters.Add("@SUBPROJECT_ID", tASK_HDR.SUBPROJECT_ID);
                        parmeters.Add("@COMPLETION_DATE", tASK_HDR.COMPLETION_DATE);
                        parmeters.Add("@ASSIGNED_TO", tASK_HDR.ASSIGNED_TO);
                        parmeters.Add("@TAGS", tASK_HDR.TAGS);
                        parmeters.Add("@ISNODE", tASK_HDR.ISNODE);
                        parmeters.Add("@CLOSE_DATE", tASK_HDR.CLOSE_DATE);
                        parmeters.Add("@DUE_DATE", tASK_HDR.DUE_DATE);
                        parmeters.Add("@TASK_PARENT_ID", tASK_HDR.TASK_PARENT_ID);
                        parmeters.Add("@STATUS", tASK_HDR.STATUS);
                        parmeters.Add("@STATUS_PERC", tASK_HDR.STATUS_PERC);
                        parmeters.Add("@TASK_CREATED_BY", tASK_HDR.TASK_CREATED_BY);
                        parmeters.Add("@APPROVER_ID", tASK_HDR.APPROVER_ID);
                        parmeters.Add("@IS_ARCHIVE", tASK_HDR.IS_ARCHIVE);
                        parmeters.Add("@ATTRIBUTE1", tASK_HDR.ATTRIBUTE1);
                        parmeters.Add("@ATTRIBUTE2", tASK_HDR.ATTRIBUTE2);
                        parmeters.Add("@ATTRIBUTE3", tASK_HDR.ATTRIBUTE3);
                        parmeters.Add("@ATTRIBUTE4", tASK_HDR.ATTRIBUTE4);
                        parmeters.Add("@ATTRIBUTE5", tASK_HDR.ATTRIBUTE5);
                        parmeters.Add("@CREATED_BY", tASK_HDR.CREATED_BY);
                        parmeters.Add("@CREATION_DATE", tASK_HDR.CREATION_DATE);
                        parmeters.Add("@LAST_UPDATED_BY", tASK_HDR.LAST_UPDATED_BY);
                        parmeters.Add("@APPROVE_ACTION_DATE", tASK_HDR.APPROVE_ACTION_DATE);
                        var InsertTaskDetails = await db.QueryFirstOrDefaultAsync<TASK_HDR>("Sp_insert_task_details", parmeters, commandType: CommandType.StoredProcedure, transaction: transaction);

                        if (InsertTaskDetails == null)
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

                            var TemplateError = new TASK_HDR();
                            TemplateError.Status = "Error";
                            TemplateError.Message = "Error Occurd";
                            return TemplateError;
                        }
                        var sqlTransaction = (SqlTransaction)transaction;
                        await sqlTransaction.CommitAsync();
                        transactionCompleted = true;

                        return InsertTaskDetails;
                    }
                    else
                    {
                        var parmeters = new DynamicParameters();
                        parmeters.Add("@TASK_MKEY", tASK_HDR.TASK_NO);
                        parmeters.Add("@TASK_NAME", tASK_HDR.TASK_NAME);
                        parmeters.Add("@TASK_DESCRIPTION", tASK_HDR.TASK_DESCRIPTION);
                        parmeters.Add("@PROJECT_ID", tASK_HDR.PROJECT_ID);
                        parmeters.Add("@SUBPROJECT_ID", tASK_HDR.SUBPROJECT_ID);
                        parmeters.Add("@COMPLETION_DATE", tASK_HDR.COMPLETION_DATE);
                        parmeters.Add("@ASSIGNED_TO", tASK_HDR.ASSIGNED_TO);
                        parmeters.Add("@TAGS", tASK_HDR.TAGS);
                        parmeters.Add("@LAST_UPDATED_BY", tASK_HDR.LAST_UPDATED_BY);

                        var UpdateTaskHDR = await db.QueryFirstOrDefaultAsync<TASK_HDR>("update_task_details", parmeters, commandType: CommandType.StoredProcedure, transaction: transaction);


                        if (UpdateTaskHDR == null)
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

                            var TemplateError = new APPROVAL_TASK_INITIATION();
                            TemplateError.ResponseStatus = "Error";
                            TemplateError.Message = "Error Occurd";
                            return null;
                        }

                        var sqlTransaction = (SqlTransaction)transaction;
                        await sqlTransaction.CommitAsync();
                        transactionCompleted = true;

                        return UpdateTaskHDR;
                    }

                }
            }
            catch (Exception ex)
            {
                var errorResult = new TASK_HDR
                {
                    STATUS = "Error",
                    Message = ex.Message
                };
                return errorResult;
            }
        }

        public async Task<TASK_HDR> CreateAddSubTaskAsync(Add_Sub_TaskInput tASK_HDR)
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
                    parmeters.Add("@TASK_NO", tASK_HDR.TASK_NO);
                    parmeters.Add("@TASK_NAME", tASK_HDR.TASK_NAME);
                    parmeters.Add("@TASK_DESCRIPTION", tASK_HDR.TASK_DESCRIPTION);
                    parmeters.Add("@CATEGORY", tASK_HDR.CATEGORY);
                    parmeters.Add("@PROJECT_ID", tASK_HDR.PROJECT_ID);
                    parmeters.Add("@SUBPROJECT_ID", tASK_HDR.SUBPROJECT_ID);
                    parmeters.Add("@COMPLETION_DATE", tASK_HDR.COMPLETION_DATE);
                    parmeters.Add("@ASSIGNED_TO", tASK_HDR.ASSIGNED_TO);
                    parmeters.Add("@TAGS", tASK_HDR.TAGS);
                    parmeters.Add("@ISNODE", tASK_HDR.ISNODE);
                    parmeters.Add("@CLOSE_DATE", tASK_HDR.CLOSE_DATE);
                    parmeters.Add("@DUE_DATE", tASK_HDR.DUE_DATE);
                    parmeters.Add("@TASK_PARENT_ID", tASK_HDR.TASK_PARENT_ID);
                    parmeters.Add("@TASK_PARENT_NODE_ID", tASK_HDR.TASK_PARENT_NODE_ID);
                    parmeters.Add("@TASK_PARENT_NUMBER", tASK_HDR.TASK_PARENT_NUMBER);
                    parmeters.Add("@STATUS", tASK_HDR.STATUS);
                    parmeters.Add("@STATUS_PERC", tASK_HDR.STATUS_PERC);
                    parmeters.Add("@TASK_CREATED_BY", tASK_HDR.TASK_CREATED_BY);
                    parmeters.Add("@APPROVER_ID", tASK_HDR.APPROVER_ID);
                    parmeters.Add("@IS_ARCHIVE", tASK_HDR.IS_ARCHIVE);
                    parmeters.Add("@ATTRIBUTE1", tASK_HDR.ATTRIBUTE1);
                    parmeters.Add("@ATTRIBUTE2", tASK_HDR.ATTRIBUTE2);
                    parmeters.Add("@ATTRIBUTE3", tASK_HDR.ATTRIBUTE3);
                    parmeters.Add("@ATTRIBUTE4", tASK_HDR.ATTRIBUTE4);
                    parmeters.Add("@ATTRIBUTE5", tASK_HDR.ATTRIBUTE5);
                    parmeters.Add("@CREATED_BY", tASK_HDR.CREATED_BY);
                    parmeters.Add("@CREATION_DATE", tASK_HDR.CREATION_DATE);
                    parmeters.Add("@LAST_UPDATED_BY", tASK_HDR.LAST_UPDATED_BY);
                    parmeters.Add("@APPROVE_ACTION_DATE", tASK_HDR.APPROVE_ACTION_DATE);
                    parmeters.Add("@Current_Task_Mkey", tASK_HDR.Current_task_mkey);

                    var InsertTaskDetails = await db.QueryFirstOrDefaultAsync<TASK_HDR>("SP_INSERT_TASK_NODE_DETAILS", parmeters, commandType: CommandType.StoredProcedure, transaction: transaction);

                    if (InsertTaskDetails == null)
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

                        var TemplateError = new TASK_HDR();
                        TemplateError.Status = "Error";
                        TemplateError.Message = "Error Occurd";
                        return TemplateError;
                    }
                    var sqlTransaction = (SqlTransaction)transaction;
                    await sqlTransaction.CommitAsync();
                    transactionCompleted = true;

                    return InsertTaskDetails;

                }
            }
            catch (Exception ex)
            {
                var errorResult = new TASK_HDR
                {
                    STATUS = "Error",
                    Message = ex.Message
                };
                return errorResult;
            }
        }

        public async Task<int> TASKFileUpoadAsync(string srNo, string taskMkey, string taskParentId, string fileName, string filePath, string createdBy, string deleteFlag, string taskMainNodeId)
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
                    parameters.Add("@SR_NO", srNo);
                    parameters.Add("@TASK_MKEY", taskMkey);
                    parameters.Add("@TASK_PARENT_ID", taskParentId);
                    parameters.Add("@FILE_NAME", fileName);
                    parameters.Add("@FILE_PATH", filePath);
                    parameters.Add("@CREATED_BY", createdBy);
                    parameters.Add("@ATTRIBUTE1", null);
                    parameters.Add("@ATTRIBUTE2", null);
                    parameters.Add("@ATTRIBUTE3", null);
                    parameters.Add("@ATTRIBUTE4", null);
                    parameters.Add("@ATTRIBUTE5", null);
                    parameters.Add("@LAST_UPDATED_BY", createdBy);
                    parameters.Add("@LAST_UPDATE_DATE", null);
                    parameters.Add("@DELETE_FLAG", deleteFlag);
                    parameters.Add("@TASK_MAIN_NODE_ID", taskMainNodeId);

                    var TaskFile = await db.ExecuteAsync("sp_insert_attcahment", parameters, commandType: CommandType.StoredProcedure, transaction: transaction);

                    if (TaskFile == null)
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

                        var TemplateError = new TASK_FILE_UPLOAD();
                        TemplateError.STATUS = "Error";
                        TemplateError.MESSAGE = "Error Occurd";
                        return 0;
                    }
                    var sqlTransaction = (SqlTransaction)transaction;
                    await sqlTransaction.CommitAsync();
                    transactionCompleted = true;
                    return 1;
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

                var ErrorFileDetails = new TASK_FILE_UPLOAD();
                ErrorFileDetails.STATUS = "Error";
                ErrorFileDetails.MESSAGE = ex.Message;
                return 1;
            }
        }

        //Task<TASK_FILE_UPLOAD> IProjectEmployee.TASKFileUpoadAsync(string srNo, string taskMkey, string taskParentId, string fileName, string filePath, string createdBy, string deleteFlag, string taskMainNodeId)
        //{
        //    throw new NotImplementedException();
        //}

        public async Task<int> UpdateTASKFileUpoadAsync(string taskMkey, string deleteFlag)
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
                    parameters.Add("@TASK_MKEY", taskMkey);
                    parameters.Add("@DELETE_FLAG", deleteFlag);

                    var TaskFile = await db.ExecuteAsync("SP_DEL_ATTCAHMENT", parameters, commandType: CommandType.StoredProcedure, transaction: transaction);

                    if (TaskFile == null)
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

                        var TemplateError = new TASK_FILE_UPLOAD();
                        TemplateError.STATUS = "Error";
                        TemplateError.MESSAGE = "Error Occurd";
                        return 0;
                    }

                    var sqlTransaction = (SqlTransaction)transaction;
                    await sqlTransaction.CommitAsync();
                    transactionCompleted = true;

                    return 1;
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

                var ErrorFileDetails = new TASK_FILE_UPLOAD();
                ErrorFileDetails.STATUS = "Error";
                ErrorFileDetails.MESSAGE = ex.Message;
                return 1;
            }
        }

        public async Task<int> GetPostTaskActionAsync(string Mkey, string TASK_MKEY, string TASK_PARENT_ID, string ACTION_TYPE, string DESCRIPTION_COMMENT, string PROGRESS_PERC, string STATUS, string CREATED_BY, string TASK_MAIN_NODE_ID, string FILE_NAME, string FILE_PATH)
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
                    parameters.Add("@Parameter1", Mkey);
                    parameters.Add("@Parameter2", TASK_MKEY);
                    parameters.Add("@Parameter3", TASK_PARENT_ID);
                    parameters.Add("@Parameter4", ACTION_TYPE);
                    parameters.Add("@Parameter5", DESCRIPTION_COMMENT);
                    parameters.Add("@Parameter6", PROGRESS_PERC);
                    parameters.Add("@Parameter7", STATUS);
                    parameters.Add("@Parameter8", CREATED_BY);
                    parameters.Add("@Parameter9", TASK_MAIN_NODE_ID);
                    parameters.Add("@Parameter10", FILE_NAME);
                    parameters.Add("@Parameter11", FILE_PATH);

                    var TaskFile = await db.ExecuteAsync("SP_TASK_ACTION_TRL_Insert_Update", parameters, commandType: CommandType.StoredProcedure, transaction: transaction);

                    if (TaskFile == null)
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

                        var TemplateError = new TASK_FILE_UPLOAD();
                        TemplateError.STATUS = "Error";
                        TemplateError.MESSAGE = "Error Occurd";
                        return 0;
                    }

                    var sqlTransaction = (SqlTransaction)transaction;
                    await sqlTransaction.CommitAsync();
                    transactionCompleted = true;

                    return 1;
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

                var ErrorFileDetails = new TASK_FILE_UPLOAD();
                ErrorFileDetails.STATUS = "Error";
                ErrorFileDetails.MESSAGE = ex.Message;
                return 1;
            }
        }
    }
}
