using Dapper;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Reflection.Metadata;
using TaskManagement.API.Interfaces;
using TaskManagement.API.Model;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TaskManagement.API.Repositories
{
    public class ProjectEmployeeRepository : IProjectEmployee
    {
        private static TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        public IDapperDbConnection _dapperDbConnection;
        private readonly string _connectionString;
        private readonly FileSettings _fileSettings;
        private readonly ITokenRepository _tokenRepository;
        public ProjectEmployeeRepository(IDapperDbConnection dapperDbConnection, string connectionString, IOptions<FileSettings> fileSettings, ITokenRepository tokenRepository)
        {
            _dapperDbConnection = dapperDbConnection;
            _connectionString = connectionString;
            _fileSettings = fileSettings.Value;
            _tokenRepository = tokenRepository;
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
                    if (dtReponse.Any())
                    {
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
                    else
                    {
                        var errorResult = new List<EmployeeLoginOutput_LIST>
                            {
                                new EmployeeLoginOutput_LIST
                                {
                                    Status = "Error",
                                    Message = "User name password is incorrect!!!",
                                    Data=null

                                }
                            };
                        return errorResult;
                    }
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
        public async Task<IEnumerable<EmployeeLoginOutput_LIST_NT>> Login_Validate_NT(EmployeeCompanyMSTInput_NT employeeCompanyMSTInput_NT)
        {
            try
            {
                using (IDbConnection db = _dapperDbConnection.CreateConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@LoginName", employeeCompanyMSTInput_NT.Login_ID);
                    parmeters.Add("@P_LOGIN_PASSWORD", employeeCompanyMSTInput_NT.Login_Password);

                    var dtReponse = await db.QueryAsync<EmployeeLoginOutput__Session_NT>("SP_GetLoginUser", parmeters, commandType: CommandType.StoredProcedure);
                    if (dtReponse.Any())
                    {
                        //create token
                        var jwtToken = await _tokenRepository.CreateJWTToken_NT(employeeCompanyMSTInput_NT.Login_ID);
                        if (IsValid(jwtToken))
                        {
                            var successsResult = new List<EmployeeLoginOutput_LIST_NT>
                            {
                                new EmployeeLoginOutput_LIST_NT
                                {
                                    Status = "Ok",
                                    Message = "Message",
                                    JwtToken = jwtToken,
                                    Data= dtReponse

                                }
                            };
                            return successsResult;
                        }
                        else
                        {
                            var errorResult = new List<EmployeeLoginOutput_LIST_NT>
                            {
                                new EmployeeLoginOutput_LIST_NT
                                {
                                    Status = "Error",
                                    Message = "Session expired!!!",
                                    JwtToken = null,
                                    Data=null
                                }
                            };
                            return errorResult;
                        }
                        //var successsResult = new List<EmployeeLoginOutput_LIST_NT>
                        //    {
                        //        new EmployeeLoginOutput_LIST_NT
                        //        {
                        //            Status = "Ok",
                        //            Message = "Message",
                        //            Data= dtReponse

                        //        }
                        //    };
                        //return successsResult;
                    }
                    else
                    {
                        var errorResult = new List<EmployeeLoginOutput_LIST_NT>
                            {
                                new EmployeeLoginOutput_LIST_NT
                                {
                                    Status = "Error",
                                    Message = "Username or password is incorrect.",
                                    JwtToken = null,
                                    Data=null

                                }
                            };
                        return errorResult;
                    }
                }
            }
            catch (Exception ex)
            {
                var errorResult = new List<EmployeeLoginOutput_LIST_NT>
                    {
                        new EmployeeLoginOutput_LIST_NT
                        {
                            Status = "Error",
                            Message = ex.Message,
                            JwtToken = null,
                            Data=null
                        }
                    };
                return errorResult;
            }
        }
        private bool IsValid(string token)
        {
            JwtSecurityToken jwtSecurityToken;
            try
            {
                jwtSecurityToken = new JwtSecurityToken(token);
            }
            catch (Exception)
            {
                return false;
            }

            return jwtSecurityToken.ValidTo > DateTime.UtcNow;
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
        public async Task<IEnumerable<V_Building_Classification_NT>> GetProjectNTAsync(BuildingClassInput_NT v_Building_Classification)
        {
            try
            {
                using (IDbConnection db = _dapperDbConnection.CreateConnection())
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@TYPE_CODE", v_Building_Classification.Type_Code);
                    parameters.Add("@MASTER_MKEY", v_Building_Classification.Master_mkey);

                    var result = await db.QueryAsync<V_Building_Classification_TMS_NT>("SP_GET_PROJECT", parameters, commandType: CommandType.StoredProcedure);

                    var successsResult = new List<V_Building_Classification_NT>
                    {
                        new V_Building_Classification_NT
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
                var errorResult = new List<V_Building_Classification_NT>
                    {
                        new V_Building_Classification_NT
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
        public async Task<IEnumerable<V_Building_Classification_New_NT>> GetSubProjectNTAsync(GetSubProjectInput_NT getSubProjectInput_NT)
        {
            try
            {
                using (IDbConnection db = _dapperDbConnection.CreateConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@PROJECT_MKEY", getSubProjectInput_NT.Project_Mkey);
                    var ProjectDetails = (await db.QueryAsync<V_Building_Classification_TMS_SESSION_NT>("SP_GET_SUBPROJECT", parmeters, commandType: CommandType.StoredProcedure));
                    //  return ProjectDetails;
                    var successsResult = new List<V_Building_Classification_New_NT>
                    {
                        new V_Building_Classification_New_NT
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
                var errorResult = new List<V_Building_Classification_New_NT>
                    {
                        new V_Building_Classification_New_NT
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
        public async Task<IEnumerable<EmployeeLoginOutput_LIST_Session_NT>> GetEmpNTAsync(Get_EmpInput_NT get_EmpInput_NT)
        {
            try
            {
                using (IDbConnection db = _dapperDbConnection.CreateConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@CURRENT_EMP_MKEY", get_EmpInput_NT.CURRENT_EMP_MKEY);
                    parmeters.Add("@FILTER", get_EmpInput_NT.FILTER);
                    var EmployeeDetails = await db.QueryAsync<EmployeeLoginOutput_NT>("SP_GET_EMP", parmeters, commandType: CommandType.StoredProcedure);
                    var successsResult = new List<EmployeeLoginOutput_LIST_Session_NT>
                    {
                        new EmployeeLoginOutput_LIST_Session_NT
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
                var errorResult = new List<EmployeeLoginOutput_LIST_Session_NT>
                    {
                        new EmployeeLoginOutput_LIST_Session_NT
                        {
                            Status = "Error",
                            Message = ex.Message,
                            Data=null

                        }
                    };
                return errorResult;
            }
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
                DataSet dsTaskDash = new DataSet();
                using (IDbConnection db = _dapperDbConnection.CreateConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@CURRENT_EMP_MKEY", CURRENT_EMP_MKEY);
                    parmeters.Add("@FILTER", FILTER);
                    var result = await db.QueryMultipleAsync("SP_TASK_DASHBOARD", parmeters, commandType: CommandType.StoredProcedure);

                    var data = result.Read<Task_DetailsOutPut>().ToList();
                    var data1 = result.Read<TaskDashboardCount>().ToList();

                    var successsResult = new List<Task_DetailsOutPut_List>
                    {
                        new Task_DetailsOutPut_List
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
                var errorResult = new List<Task_DetailsOutPut_List>
                    {
                        new Task_DetailsOutPut_List
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
        public async Task<IEnumerable<GET_ACTIONS_TYPE_FILE>> GetActionsAsync(string TASK_MKEY, string CURRENT_EMP_MKEY, string CURR_ACTION)
        {
            try
            {
                using (IDbConnection db = _dapperDbConnection.CreateConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@TASK_MKEY", TASK_MKEY);
                    parmeters.Add("@CURRENT_EMP_MKEY", CURRENT_EMP_MKEY);
                    parmeters.Add("@CURR_ACTION", CURR_ACTION);
                    var TaskTreeDetails = await db.QueryMultipleAsync("SP_GET_ACTIONS", parmeters, commandType: CommandType.StoredProcedure);

                    var data = TaskTreeDetails.Read<GetActionsListTypeDesc>().ToList();
                    var data1 = TaskTreeDetails.Read<GetActionsListFile>().ToList();

                    var successsResult = new List<GET_ACTIONS_TYPE_FILE>
                    {
                        new GET_ACTIONS_TYPE_FILE
                        {
                            Status = "Ok",
                            Message = "Message",
                            Data= data,
                            Data1= data1

                        }
                    };
                    return successsResult;
                }
            }
            catch (Exception ex)
            {
                var errorResult = new List<GET_ACTIONS_TYPE_FILE>
                    {
                        new GET_ACTIONS_TYPE_FILE
                        {
                           Status = "Error",
                            Message= ex.Message
                        }
                    };
                return errorResult;
            }
        }
        public async Task<IEnumerable<GET_TASK_TREEOutPut_List>> GetTaskTreeAsync(string Mkey)
        {
            try
            {
                using (IDbConnection db = _dapperDbConnection.CreateConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@TASK_MKEY", Mkey);
                    parmeters.Add("@Completed", null);
                    var TaskTreeDetails = (await db.QueryAsync<GetTaskTreeOutPut>("SP_GET_TASK_TREE", parmeters, commandType: CommandType.StoredProcedure)).ToList();
                    var successsResult = new List<GET_TASK_TREEOutPut_List>
                    {
                        new GET_TASK_TREEOutPut_List
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
                var errorResult = new List<GET_TASK_TREEOutPut_List>
                    {
                        new GET_TASK_TREEOutPut_List
                        {
                           Status = "Error",
                            Message= ex.Message
                        }
                    };
                return errorResult;
            }
        }
        public async Task<IEnumerable<PutChangePasswordOutPut_List>> PutChangePasswordAsync(string LoginName, string Old_Password, string New_Password)
        {
            try
            {
                using (IDbConnection db = _dapperDbConnection.CreateConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@LoginName", LoginName);
                    parmeters.Add("@Old_LOGIN_PASSWORD", Old_Password);
                    parmeters.Add("@New_LOGIN_PASSWORD", New_Password);
                    var ChangePass = await db.QueryAsync<PutChangePasswordOutPut>("Sp_USER_ChangeLOGIN_PASSWORD", parmeters, commandType: CommandType.StoredProcedure);
                    var successsResult = new List<PutChangePasswordOutPut_List>
                    {
                        new PutChangePasswordOutPut_List
                        {
                            Status = "Ok",
                            Message = "Message",
                            Data= ChangePass
                        }
                    };
                    return successsResult;
                }
            }
            catch (Exception ex)
            {
                var errorResult = new List<PutChangePasswordOutPut_List>
                    {
                        new PutChangePasswordOutPut_List
                        {
                           Status = "Error",
                            Message= ex.Message,
                            Data= null
                        }
                    };
                return errorResult;
            }
        }
        public async Task<IEnumerable<ForgotPasswordOutPut_List>> GetForgotPasswordAsync(string LoginName)
        {
            try
            {
                using (IDbConnection db = _dapperDbConnection.CreateConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@LoginName", LoginName);
                    var ForgotPass = await db.QueryAsync<ForgotPasswordOutPut>("Sp_USER_ForgotPassword", parmeters, commandType: CommandType.StoredProcedure);
                    var successsResult = new List<ForgotPasswordOutPut_List>
                    {
                        new ForgotPasswordOutPut_List
                        {
                            Status = "Ok",
                            Message = "Message",
                            Data= ForgotPass
                        }
                    };
                    return successsResult;
                }
            }
            catch (Exception ex)
            {
                var errorResult = new List<ForgotPasswordOutPut_List>
                    {
                        new ForgotPasswordOutPut_List
                        {
                           Status = "Error",
                            Message= ex.Message,
                            Data= null
                        }
                    };
                return errorResult;
            }
        }
        public async Task<IEnumerable<ResetPasswordOutPut_List>> GetResetPasswordAsync(string TEMPPASSWORD, string LoginName)
        {
            try
            {
                using (IDbConnection db = _dapperDbConnection.CreateConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@TEMPPASSWORD", TEMPPASSWORD);
                    parmeters.Add("@LoginName", LoginName);
                    var ResetPass = await db.QueryAsync<ResetPasswordOutPut>("sp_reset_password", parmeters, commandType: CommandType.StoredProcedure);
                    var successsResult = new List<ResetPasswordOutPut_List>
                    {
                        new ResetPasswordOutPut_List
                        {
                            Status = "Ok",
                            Message = "Message",
                            Data= ResetPass
                        }
                    };
                    return successsResult;
                }
            }
            catch (Exception ex)
            {
                var errorResult = new List<ResetPasswordOutPut_List>
                    {
                        new ResetPasswordOutPut_List
                        {
                           Status = "Error",
                            Message= ex.Message,
                            Data= null
                        }
                    };
                return errorResult;
            }
        }
        public async Task<IEnumerable<ChangePasswordOutPut_List>> GetValidateEmailAsync(string Login_ID)
        {
            try
            {
                using (IDbConnection db = _dapperDbConnection.CreateConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@LoginName", Login_ID);
                    var ValidateEmail = await db.QueryAsync<ChangePasswordOutPut>("Sp_validate_login", parmeters, commandType: CommandType.StoredProcedure);
                    var successsResult = new List<ChangePasswordOutPut_List>
                    {
                        new ChangePasswordOutPut_List
                        {
                            Status = "Ok",
                            Message = "Message",
                            Data= ValidateEmail
                        }
                    };
                    return successsResult;
                }
            }
            catch (Exception ex)
            {
                var errorResult = new List<ChangePasswordOutPut_List>
                    {
                        new ChangePasswordOutPut_List
                        {
                           Status = "Error",
                            Message= ex.Message,
                            Data= null
                        }
                    };
                return errorResult;
            }
        }
        public async Task<IEnumerable<GET_TASK_TREEOutPut_List>> GetTaskDashboardDetailsAsync(string CURRENT_EMP_MKEY, string CURR_ACTION)
        {
            try
            {
                using (IDbConnection db = _dapperDbConnection.CreateConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@CURRENT_EMP_MKEY", CURRENT_EMP_MKEY);
                    parmeters.Add("@CURR_ACTION", CURR_ACTION);
                    var ValidateEmail = await db.QueryMultipleAsync("SP_Get_Overall_DB", parmeters, commandType: CommandType.StoredProcedure);

                    var Data = ValidateEmail.Read<GET_TASK_TREEOutPut>().ToList();
                    var Data1 = ValidateEmail.Read<GET_TASK_TREEOutPut>().ToList();
                    var Data2 = ValidateEmail.Read<GET_TASK_TREEOutPut>().ToList();
                    var Data3 = ValidateEmail.Read<GET_TASK_TREEOutPut>().ToList();
                    var Data4 = ValidateEmail.Read<GET_TASK_TREEOutPut>().ToList();
                    var Data5 = ValidateEmail.Read<GET_TASK_TREEOutPut>().ToList();
                    var Data6 = ValidateEmail.Read<GET_TASK_TREEOutPut>().ToList();
                    var Data7 = ValidateEmail.Read<GET_TASK_TREEOutPut>().ToList();
                    var Data8 = ValidateEmail.Read<GET_TASK_TREEOutPut>().ToList();

                    var successsResult = new List<GET_TASK_TREEOutPut_List>
                    {
                        new GET_TASK_TREEOutPut_List
                        {
                            Status = "Ok",
                            Message = "Message",
                             Table = Data,
                             Table1 = Data1,
                             Table2 = Data2,
                             Table3 = Data3,
                             Table4 = Data4,
                             Table5 = Data5,
                             Table6 = Data6,
                             Table7 = Data7,
                             Table8 = Data8
                        }
                    };
                    return successsResult;
                }
            }
            catch (Exception ex)
            {
                var errorResult = new List<GET_TASK_TREEOutPut_List>
                    {
                        new GET_TASK_TREEOutPut_List
                        {
                           Status = "Error",
                            Message= ex.Message,
                            Table = null,
                             Table1 = null,
                             Table2 = null,
                             Table3 = null,
                             Table4 = null,
                             Table5 = null,
                             Table6 = null,
                             Table7 = null,
                             Table8 = null
                        }
                    };
                return errorResult;
            }
        }
        public async Task<IEnumerable<GetTaskTeamOutPut_List>> GetTeamTaskAsync(string CURRENT_EMP_MKEY)
        {
            try
            {
                using (IDbConnection db = _dapperDbConnection.CreateConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@CURRENT_EMP_MKEY", CURRENT_EMP_MKEY);
                    var TeamTask = await db.QueryMultipleAsync("SP_GET_TEAM_PROGRESS", parmeters, commandType: CommandType.StoredProcedure);

                    var Data = TeamTask.Read<GET_TASK_DepartmentOutPut>().ToList();
                    var Data1 = TeamTask.Read<TEAM_PROGRESSOutPut>().ToList();

                    var successsResult = new List<GetTaskTeamOutPut_List>
                    {
                        new GetTaskTeamOutPut_List
                        {
                            Status = "Ok",
                            Message = "Message",
                            Data= Data,
                            Data1= Data1
                        }
                    };
                    return successsResult;
                }
            }
            catch (Exception ex)
            {
                var errorResult = new List<GetTaskTeamOutPut_List>
                    {
                        new GetTaskTeamOutPut_List
                        {
                           Status = "Error",
                            Message= ex.Message,
                            Data= null,
                            Data1 = null
                        }
                    };
                return errorResult;
            }
        }
        public async Task<IEnumerable<TASK_DASHBOARDOutPut_List>> GetTeamTaskDetailsAsync(string CURRENT_EMP_MKEY)
        {
            try
            {
                using (IDbConnection db = _dapperDbConnection.CreateConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@CURRENT_EMP_MKEY", CURRENT_EMP_MKEY);
                    var TeamTask = await db.QueryMultipleAsync("SP_GET_TEAM_PROGRESS", parmeters, commandType: CommandType.StoredProcedure);

                    var Data = TeamTask.Read<GET_TASK_DepartmentOutPut>().ToList();
                    var Data1 = TeamTask.Read<TEAM_PROGRESSOutPut>().ToList();

                    var successsResult = new List<TASK_DASHBOARDOutPut_List>
                    {
                        new TASK_DASHBOARDOutPut_List
                        {
                            Status = "Ok",
                            Message = "Message",
                            Data = Data,
                            Data1= Data1,

                        }
                    };
                    return successsResult;

                    //return TeamTask;
                }
            }
            catch (Exception ex)
            {
                var errorResult = new List<TASK_DASHBOARDOutPut_List>
                    {
                        new TASK_DASHBOARDOutPut_List
                        {
                            Status = "Error",
                            Message = ex.Message
                        }
                    };
                return errorResult;
            }
        }
        public async Task<IEnumerable<Get_Project_DetailsWithSubprojectOutPut_List>> GetProjectDetailsWithSubProjectAsync(string ProjectID, string SubProjectID)
        {
            try
            {
                using (IDbConnection db = _dapperDbConnection.CreateConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@PROJECTID", ProjectID);
                    parmeters.Add("@SUBPROJECTID", SubProjectID);
                    var ProjectDetails = await db.QueryAsync<Get_Project_DetailsWithSubprojectOutPut>("USP_GET_pROJECTPREVIEW", parmeters, commandType: CommandType.StoredProcedure);

                    // var SUBTAsyncASKDetails = await db.QueryAsync("select Project_id,sub_project_id from V_RootTasks", commandType: CommandType.Text);

                    var successsResult = new List<Get_Project_DetailsWithSubprojectOutPut_List>
                    {
                        new Get_Project_DetailsWithSubprojectOutPut_List
                        {
                            Status = "Ok",
                            Message = "Message",
                            Data= ProjectDetails
                        }
                    };
                    return successsResult;
                }
            }
            catch (Exception ex)
            {
                var errorResult = new List<Get_Project_DetailsWithSubprojectOutPut_List>
                    {
                        new Get_Project_DetailsWithSubprojectOutPut_List
                        {
                           Status = "Error",
                            Message= ex.Message,
                            Data= null
                        }
                    };
                return errorResult;
            }
        }
        //public async Task<IEnumerable<TASK_HDR>> GetTaskTreeExportAsync(string Task_Mkey)
        //{
        //    try
        //    {
        //        using (IDbConnection db = _dapperDbConnection.CreateConnection())
        //        {
        //            var parmeters = new DynamicParameters();
        //            parmeters.Add("@TASK_MKEY", Task_Mkey);
        //            var TaskDetails = (await db.QueryAsync<TASK_HDR>("SP_GET_TASK_TREE", parmeters, commandType: CommandType.StoredProcedure)).ToList();
        //            return TaskDetails;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        var errorResult = new List<TASK_HDR>
        //            {
        //                new TASK_HDR
        //                {
        //                    STATUS = "Error",
        //                    Message = ex.Message
        //                }
        //            };
        //        return errorResult;
        //    }
        //}
        public async Task<IEnumerable<TASK_NESTED_GRIDOutPut_List>> GetTaskTreeExportAsync(string Task_Mkey)
        {
            try
            {
                using (IDbConnection db = _dapperDbConnection.CreateConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@TASK_MKEY", Task_Mkey);
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
        public async Task<IEnumerable<Add_TaskOutPut_List>> CreateAddTaskAsync(Add_TaskInput tASK_HDR)
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

                    if (tASK_HDR.TASK_NO.ToString() == "0000".ToString())
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
                        var InsertTaskDetails = (await db.QueryAsync<Add_TaskOutPut>("Sp_insert_task_details", parmeters, commandType: CommandType.StoredProcedure, transaction: transaction)).ToList();

                        var sqlTransaction = (SqlTransaction)transaction;
                        await sqlTransaction.CommitAsync();
                        transactionCompleted = true;

                        var successsResult = new List<Add_TaskOutPut_List>
                            {
                            new Add_TaskOutPut_List
                                {
                                Status = "Ok",
                                Message = "Inserted Successfully",
                                Data= InsertTaskDetails
                                }
                        };
                        return successsResult;
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

                        var UpdateTaskHDR = await db.QueryAsync<Add_TaskOutPut>("update_task_details", parmeters, commandType: CommandType.StoredProcedure, transaction: transaction);

                        var sqlTransaction = (SqlTransaction)transaction;
                        await sqlTransaction.CommitAsync();
                        transactionCompleted = true;

                        var successsResult = new List<Add_TaskOutPut_List>
                            {
                            new Add_TaskOutPut_List
                                {
                                Status = "Ok",
                                Message = "Updated Successfully",
                                Data= UpdateTaskHDR
                                }
                        };
                        return successsResult;
                    }

                }
            }
            catch (Exception ex)
            {
                var errorResult = new List<Add_TaskOutPut_List>
                    {
                        new Add_TaskOutPut_List
                        {
                            Status = "Error",
                            Message = ex.Message
                        }
                    };
                return errorResult;
            }
        }
        public async Task<IEnumerable<Add_TaskOutPut_List_NT>> CreateAddTaskNTAsync(Add_TaskInput_NT add_TaskInput_NT)
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

                    if (add_TaskInput_NT.TASK_NO.ToString() == "0000".ToString())
                    {
                        var parmeters = new DynamicParameters();
                        parmeters.Add("@TASK_NO", add_TaskInput_NT.TASK_NO);
                        parmeters.Add("@TASK_NAME", add_TaskInput_NT.TASK_NAME);
                        parmeters.Add("@TASK_DESCRIPTION", add_TaskInput_NT.TASK_DESCRIPTION);
                        parmeters.Add("@CATEGORY", add_TaskInput_NT.CATEGORY);
                        parmeters.Add("@PROJECT_ID", add_TaskInput_NT.PROJECT_ID);
                        parmeters.Add("@SUBPROJECT_ID", add_TaskInput_NT.SUBPROJECT_ID);
                        parmeters.Add("@COMPLETION_DATE", add_TaskInput_NT.COMPLETION_DATE);
                        parmeters.Add("@ASSIGNED_TO", add_TaskInput_NT.ASSIGNED_TO);
                        parmeters.Add("@TAGS", add_TaskInput_NT.TAGS);
                        parmeters.Add("@ISNODE", add_TaskInput_NT.ISNODE);
                        parmeters.Add("@CLOSE_DATE", add_TaskInput_NT.CLOSE_DATE);
                        parmeters.Add("@DUE_DATE", add_TaskInput_NT.DUE_DATE);
                        parmeters.Add("@TASK_PARENT_ID", add_TaskInput_NT.TASK_PARENT_ID);
                        parmeters.Add("@STATUS", add_TaskInput_NT.STATUS);
                        parmeters.Add("@STATUS_PERC", add_TaskInput_NT.STATUS_PERC);
                        parmeters.Add("@TASK_CREATED_BY", add_TaskInput_NT.TASK_CREATED_BY);
                        parmeters.Add("@APPROVER_ID", add_TaskInput_NT.APPROVER_ID);
                        parmeters.Add("@IS_ARCHIVE", add_TaskInput_NT.IS_ARCHIVE);
                        parmeters.Add("@ATTRIBUTE1", add_TaskInput_NT.ATTRIBUTE1);
                        parmeters.Add("@ATTRIBUTE2", add_TaskInput_NT.ATTRIBUTE2);
                        parmeters.Add("@ATTRIBUTE3", add_TaskInput_NT.ATTRIBUTE3);
                        parmeters.Add("@ATTRIBUTE4", add_TaskInput_NT.ATTRIBUTE4);
                        parmeters.Add("@ATTRIBUTE5", add_TaskInput_NT.ATTRIBUTE5);
                        parmeters.Add("@CREATED_BY", add_TaskInput_NT.CREATED_BY);
                        parmeters.Add("@CREATION_DATE", add_TaskInput_NT.CREATION_DATE);
                        parmeters.Add("@LAST_UPDATED_BY", add_TaskInput_NT.LAST_UPDATED_BY);
                        parmeters.Add("@APPROVE_ACTION_DATE", add_TaskInput_NT.APPROVE_ACTION_DATE);
                        var InsertTaskDetails = (await db.QueryAsync<Add_TaskOutPut_NT>("Sp_insert_task_details", parmeters, commandType: CommandType.StoredProcedure, transaction: transaction)).ToList();

                        var sqlTransaction = (SqlTransaction)transaction;
                        await sqlTransaction.CommitAsync();
                        transactionCompleted = true;

                        var successsResult = new List<Add_TaskOutPut_List_NT>
                            {
                            new Add_TaskOutPut_List_NT
                                {
                                Status = "Ok",
                                Message = "Inserted Successfully",
                                Data= InsertTaskDetails
                                }
                        };
                        return successsResult;
                    }
                    else
                    {
                        var parmeters = new DynamicParameters();
                        parmeters.Add("@TASK_MKEY", add_TaskInput_NT.TASK_NO);
                        parmeters.Add("@TASK_NAME", add_TaskInput_NT.TASK_NAME);
                        parmeters.Add("@TASK_DESCRIPTION", add_TaskInput_NT.TASK_DESCRIPTION);
                        parmeters.Add("@PROJECT_ID", add_TaskInput_NT.PROJECT_ID);
                        parmeters.Add("@SUBPROJECT_ID", add_TaskInput_NT.SUBPROJECT_ID);
                        parmeters.Add("@COMPLETION_DATE", add_TaskInput_NT.COMPLETION_DATE);
                        parmeters.Add("@ASSIGNED_TO", add_TaskInput_NT.ASSIGNED_TO);
                        parmeters.Add("@TAGS", add_TaskInput_NT.TAGS);
                        parmeters.Add("@LAST_UPDATED_BY", add_TaskInput_NT.LAST_UPDATED_BY);

                        var UpdateTaskHDR = await db.QueryAsync<Add_TaskOutPut_NT>("update_task_details", parmeters, commandType: CommandType.StoredProcedure, transaction: transaction);

                        var sqlTransaction = (SqlTransaction)transaction;
                        await sqlTransaction.CommitAsync();
                        transactionCompleted = true;

                        var successsResult = new List<Add_TaskOutPut_List_NT>
                            {
                            new Add_TaskOutPut_List_NT
                                {
                                Status = "Ok",
                                Message = "Updated Successfully",
                                Data= UpdateTaskHDR
                                }
                        };
                        return successsResult;
                    }

                }
            }
            catch (Exception ex)
            {
                var errorResult = new List<Add_TaskOutPut_List_NT>
                    {
                        new Add_TaskOutPut_List_NT
                        {
                            Status = "Error",
                            Message = ex.Message
                        }
                    };
                return errorResult;
            }
        }
        public async Task<IEnumerable<Add_TaskOutPut_List>> CreateAddSubTaskAsync(Add_Sub_TaskInput tASK_HDR)
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

                    var InsertTaskDetails = await db.QueryAsync<Add_TaskOutPut>("SP_INSERT_TASK_NODE_DETAILS", parmeters, commandType: CommandType.StoredProcedure, transaction: transaction);

                    var sqlTransaction = (SqlTransaction)transaction;
                    await sqlTransaction.CommitAsync();
                    transactionCompleted = true;

                    var successsResult = new List<Add_TaskOutPut_List>
                            {
                            new Add_TaskOutPut_List
                                {
                                Status = "Ok",
                                Message = "Inserted Successfully",
                                Data= InsertTaskDetails
                                }
                        };
                    return successsResult;


                }
            }
            catch (Exception ex)
            {
                var errorResult = new List<Add_TaskOutPut_List>
                    {
                        new Add_TaskOutPut_List
                        {
                            Status = "Error",
                            Message = ex.Message
                        }
                    };
                return errorResult;
            }
        }
        public async Task<IEnumerable<Add_TaskOutPut_List_NT>> CreateAddSubTaskNTAsync(Add_Sub_TaskInput_NT add_TaskInput_NT)
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
                    parmeters.Add("@TASK_NO", add_TaskInput_NT.TASK_NO);
                    parmeters.Add("@TASK_NAME", add_TaskInput_NT.TASK_NAME);
                    parmeters.Add("@TASK_DESCRIPTION", add_TaskInput_NT.TASK_DESCRIPTION);
                    parmeters.Add("@CATEGORY", add_TaskInput_NT.CATEGORY);
                    parmeters.Add("@PROJECT_ID", add_TaskInput_NT.PROJECT_ID);
                    parmeters.Add("@SUBPROJECT_ID", add_TaskInput_NT.SUBPROJECT_ID);
                    parmeters.Add("@COMPLETION_DATE", add_TaskInput_NT.COMPLETION_DATE);
                    parmeters.Add("@ASSIGNED_TO", add_TaskInput_NT.ASSIGNED_TO);
                    parmeters.Add("@TAGS", add_TaskInput_NT.TAGS);
                    parmeters.Add("@ISNODE", add_TaskInput_NT.ISNODE);
                    parmeters.Add("@CLOSE_DATE", add_TaskInput_NT.CLOSE_DATE);
                    parmeters.Add("@DUE_DATE", add_TaskInput_NT.DUE_DATE);
                    parmeters.Add("@TASK_PARENT_ID", add_TaskInput_NT.TASK_PARENT_ID);
                    parmeters.Add("@TASK_PARENT_NODE_ID", add_TaskInput_NT.TASK_PARENT_NODE_ID);
                    parmeters.Add("@TASK_PARENT_NUMBER", add_TaskInput_NT.TASK_PARENT_NUMBER);
                    parmeters.Add("@STATUS", add_TaskInput_NT.STATUS);
                    parmeters.Add("@STATUS_PERC", add_TaskInput_NT.STATUS_PERC);
                    parmeters.Add("@TASK_CREATED_BY", add_TaskInput_NT.TASK_CREATED_BY);
                    parmeters.Add("@APPROVER_ID", add_TaskInput_NT.APPROVER_ID);
                    parmeters.Add("@IS_ARCHIVE", add_TaskInput_NT.IS_ARCHIVE);
                    parmeters.Add("@ATTRIBUTE1", add_TaskInput_NT.ATTRIBUTE1);
                    parmeters.Add("@ATTRIBUTE2", add_TaskInput_NT.ATTRIBUTE2);
                    parmeters.Add("@ATTRIBUTE3", add_TaskInput_NT.ATTRIBUTE3);
                    parmeters.Add("@ATTRIBUTE4", add_TaskInput_NT.ATTRIBUTE4);
                    parmeters.Add("@ATTRIBUTE5", add_TaskInput_NT.ATTRIBUTE5);
                    parmeters.Add("@CREATED_BY", add_TaskInput_NT.CREATED_BY);
                    parmeters.Add("@CREATION_DATE", add_TaskInput_NT.CREATION_DATE);
                    parmeters.Add("@LAST_UPDATED_BY", add_TaskInput_NT.LAST_UPDATED_BY);
                    parmeters.Add("@APPROVE_ACTION_DATE", add_TaskInput_NT.APPROVE_ACTION_DATE);
                    parmeters.Add("@Current_Task_Mkey", add_TaskInput_NT.Current_task_mkey);

                    var InsertTaskDetails = await db.QueryAsync<Add_TaskOutPut_NT>("SP_INSERT_TASK_NODE_DETAILS", parmeters, commandType: CommandType.StoredProcedure, transaction: transaction);

                    var sqlTransaction = (SqlTransaction)transaction;
                    await sqlTransaction.CommitAsync();
                    transactionCompleted = true;

                    var successsResult = new List<Add_TaskOutPut_List_NT>
                            {
                            new Add_TaskOutPut_List_NT
                                {
                                Status = "Ok",
                                Message = "Inserted Successfully",
                                Data= InsertTaskDetails
                                }
                        };
                    return successsResult;


                }
            }
            catch (Exception ex)
            {
                var errorResult = new List<Add_TaskOutPut_List_NT>
                    {
                        new Add_TaskOutPut_List_NT
                        {
                            Status = "Error",
                            Message = ex.Message
                        }
                    };
                return errorResult;
            }
        }
        public async Task<int> TASKFileUpoadAsync(int srNo, int taskMkey, int taskParentId, string fileName, string filePath, int createdBy, char deleteFlag, int taskMainNodeId)
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
        public async Task<int> TASKFileUpoadNTAsync(int srNo, int taskMkey, int taskParentId, string fileName, string filePath, int createdBy, char deleteFlag, int taskMainNodeId)
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

                        var TemplateError = new TASK_FILE_UPLOAD_NT();
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

                var ErrorFileDetails = new TASK_FILE_UPLOAD_NT();
                ErrorFileDetails.STATUS = "Error";
                ErrorFileDetails.MESSAGE = ex.Message;
                return 1;
            }
        }
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
                return 0;
            }
        }
        public async Task<ActionResult<IEnumerable<TASK_COMPLIANCE_list>>> GetTaskComplianceAsync(TASK_COMPLIANCE_INPUT tASK_COMPLIANCE_INPUT)
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
                    parmeters.Add("@PROPERTY_MKEY", tASK_COMPLIANCE_INPUT.PROPERTY_MKEY);
                    parmeters.Add("@BUILDING_MKEY", tASK_COMPLIANCE_INPUT.BUILDING_MKEY);
                    parmeters.Add("@TASK_MKEY", tASK_COMPLIANCE_INPUT.TASK_MKEY);
                    parmeters.Add("@USER_ID", tASK_COMPLIANCE_INPUT.USER_ID);
                    parmeters.Add("@API_NAME", "GetTaskCompliance");
                    parmeters.Add("@API_METHOD", "Get");
                    var GetTaskCompliance = await db.QueryAsync<TASK_COMPLIANCE_OUTPUT>("SP_GET_TASK_COMPLIANCE", parmeters, commandType: CommandType.StoredProcedure, transaction: transaction);

                    var sqlTransaction = (SqlTransaction)transaction;
                    await sqlTransaction.CommitAsync();
                    transactionCompleted = true;

                    foreach (var TaskCompliance in GetTaskCompliance)
                    {
                        if (TaskCompliance.MKEY <= 1)
                        {
                            var errorResult = new List<TASK_COMPLIANCE_list>
                                {
                                    new TASK_COMPLIANCE_list
                                    {
                                        STATUS = "Error",
                                        MESSAGE = "Data not found",
                                        DATA = null
                                    }
                                };
                            return errorResult;
                        }
                    }
                    var successsResult = new List<TASK_COMPLIANCE_list>
                            {
                            new TASK_COMPLIANCE_list
                                {
                                STATUS = "Ok",
                                MESSAGE = "Get data successfully!!!",
                                DATA= GetTaskCompliance
                                }
                        };
                    return successsResult;
                }
            }
            catch (Exception ex)
            {
                var errorResult = new List<TASK_COMPLIANCE_list>
                    {
                        new TASK_COMPLIANCE_list
                        {
                            STATUS = "Error",
                            MESSAGE = ex.Message,
                            DATA = null
                        }
                    };
                return errorResult;
            }
        }
        public async Task<ActionResult<IEnumerable<TaskSanctioningDepartmentOutputList>>> GetTaskSanctioningAuthorityAsync(TASK_COMPLIANCE_INPUT tASK_COMPLIANCE_INPUT)
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
                    parmeters.Add("@PROPERTY_MKEY", tASK_COMPLIANCE_INPUT.PROPERTY_MKEY);
                    parmeters.Add("@BUILDING_MKEY", tASK_COMPLIANCE_INPUT.BUILDING_MKEY);
                    parmeters.Add("@MKEY", tASK_COMPLIANCE_INPUT.TASK_MKEY);
                    parmeters.Add("@USER_ID", tASK_COMPLIANCE_INPUT.USER_ID);
                    parmeters.Add("@API_NAME", "GetTaskCompliance");
                    parmeters.Add("@API_METHOD", "Get");
                    var GetTaskSanDepart = await db.QueryAsync<TaskSanctioningDepartmentOutput>("SP_GET_TASK_SANCTIONING_DEPARTMENT", parmeters, commandType: CommandType.StoredProcedure, transaction: transaction);

                    var sqlTransaction = (SqlTransaction)transaction;
                    await sqlTransaction.CommitAsync();
                    transactionCompleted = true;

                    if (GetTaskSanDepart.Count<TaskSanctioningDepartmentOutput>() == 0)
                    {
                        var errorResult = new List<TaskSanctioningDepartmentOutputList>
                            {
                                new TaskSanctioningDepartmentOutputList
                                {
                                    STATUS = "Error",
                                    MESSAGE = "No data found",
                                    DATA = null
                                }
                            };
                        return errorResult;
                    }
                    else
                    {
                        foreach (var TaskCompliance in GetTaskSanDepart)
                        {
                            if (TaskCompliance.MKEY < 1)
                            {
                                var errorResult = new List<TaskSanctioningDepartmentOutputList>
                                {
                                    new TaskSanctioningDepartmentOutputList
                                    {
                                        STATUS = "Error",
                                        MESSAGE = "Data not found",
                                        DATA = null
                                    }
                                };
                                return errorResult;
                            }
                        }
                        var successsResult = new List<TaskSanctioningDepartmentOutputList>
                            {
                            new TaskSanctioningDepartmentOutputList
                                {
                                STATUS = "Ok",
                                MESSAGE = "Get data successfully!!!",
                                DATA= GetTaskSanDepart
                                }
                        };
                        return successsResult;
                    }

                }
            }
            catch (Exception ex)
            {
                var errorResult = new List<TaskSanctioningDepartmentOutputList>
                    {
                        new TaskSanctioningDepartmentOutputList
                        {
                            STATUS = "Error",
                            MESSAGE = ex.Message,
                            DATA = null
                        }
                    };
                return errorResult;
            }
        }
        public async Task<ActionResult<IEnumerable<TASK_COMPLIANCE_END_CHECK_LIST>>> GetTaskEndListAsync(TASK_COMPLIANCE_INPUT tASK_COMPLIANCE_INPUT)
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
                    parmeters.Add("@PROPERTY_MKEY", tASK_COMPLIANCE_INPUT.PROPERTY_MKEY);
                    parmeters.Add("@BUILDING_MKEY", tASK_COMPLIANCE_INPUT.BUILDING_MKEY);
                    parmeters.Add("@TASK_MKEY", tASK_COMPLIANCE_INPUT.TASK_MKEY);
                    parmeters.Add("@USER_ID", tASK_COMPLIANCE_INPUT.USER_ID);
                    parmeters.Add("@API_NAME", "GetTaskCompliance");
                    parmeters.Add("@API_METHOD", "Get");
                    var GetTaskEnd = await db.QueryAsync<TASK_COMPLIANCE_CHECK_END_LIST_OUTPUT>("SP_GET_TASK_ENDLIST", parmeters, commandType: CommandType.StoredProcedure, transaction: transaction);

                    if (GetTaskEnd.Any())
                    {
                        foreach (var TaskCompliance in GetTaskEnd)
                        {
                            if (TaskCompliance.MKEY < 1)
                            {
                                var errorResult = new List<TASK_COMPLIANCE_END_CHECK_LIST>
                                {
                                    new TASK_COMPLIANCE_END_CHECK_LIST
                                    {
                                        STATUS = "Error",
                                        MESSAGE = "Data not found",
                                        DATA = null
                                    }
                                };
                                return errorResult;
                            }

                            var parmetersMedia = new DynamicParameters();
                            parmetersMedia.Add("@TASK_MKEY", tASK_COMPLIANCE_INPUT.TASK_MKEY);
                            parmetersMedia.Add("@DOC_CATEGORY_MKEY", TaskCompliance.DOC_MKEY);
                            parmetersMedia.Add("@USER_ID", tASK_COMPLIANCE_INPUT.USER_ID);
                            var TaskEndListMedia = await db.QueryAsync<TASK_OUTPUT_MEDIA>("SP_GET_TASK_ENDLIST_MEDIA", parmetersMedia, commandType: CommandType.StoredProcedure, transaction: transaction);

                            TaskCompliance.TASK_OUTPUT_ATTACHMENT = TaskEndListMedia.ToList();
                        }

                        var sqlTransaction = (SqlTransaction)transaction;
                        await sqlTransaction.CommitAsync();
                        transactionCompleted = true;

                        var successsResult = new List<TASK_COMPLIANCE_END_CHECK_LIST>
                            {
                            new TASK_COMPLIANCE_END_CHECK_LIST
                                {
                                STATUS = "Ok",
                                MESSAGE = "Get data successfully!!!",
                                DATA= GetTaskEnd
                                }
                        };
                        return successsResult;
                    }
                    else
                    {
                        var errorResult = new List<TASK_COMPLIANCE_END_CHECK_LIST>
                                {
                                    new TASK_COMPLIANCE_END_CHECK_LIST
                                    {
                                        STATUS = "Error",
                                        MESSAGE = "Data not found",
                                        DATA = null
                                    }
                                };
                        return errorResult;
                    }
                }
            }
            catch (Exception ex)
            {
                var errorResult = new List<TASK_COMPLIANCE_END_CHECK_LIST>
                    {
                        new TASK_COMPLIANCE_END_CHECK_LIST
                        {
                            STATUS = "Error",
                            MESSAGE = ex.Message,
                            DATA = null
                        }
                    };
                return errorResult;
            }
        }
        public async Task<ActionResult<IEnumerable<TASK_ENDLIST_DETAILS_OUTPUT_LIST>>> GetTaskEndListDetailsAsync(TASK_END_LIST_DETAILS tASK_END_LIST_DETAILS)
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
                    parmeters.Add("@PROPERTY_MKEY", tASK_END_LIST_DETAILS.PROPERTY_MKEY);
                    parmeters.Add("@BUILDING_MKEY", tASK_END_LIST_DETAILS.BUILDING_MKEY);
                    parmeters.Add("@DOC_MKEY", tASK_END_LIST_DETAILS.DOC_MKEY);
                    parmeters.Add("@USER_ID", tASK_END_LIST_DETAILS.USER_ID);
                    parmeters.Add("@API_NAME", "GetTaskCompliance");
                    parmeters.Add("@API_METHOD", "Get");
                    var GetTaskEnd = await db.QueryAsync<TASK_ENDLIST_DETAILS_OUTPUT>("SP_GET_TASK_ENDLIST_DETAILS", parmeters, commandType: CommandType.StoredProcedure, transaction: transaction);

                    if (GetTaskEnd.Any())
                    {
                        foreach (var TaskCompliance in GetTaskEnd)
                        {
                            if (TaskCompliance.MKEY < 1)
                            {
                                var errorResult = new List<TASK_ENDLIST_DETAILS_OUTPUT_LIST>
                                {
                                    new TASK_ENDLIST_DETAILS_OUTPUT_LIST
                                    {
                                        STATUS = "Error",
                                        MESSAGE = "Data not found",
                                        DATA = null
                                    }
                                };
                                return errorResult;
                            }
                        }

                        var parmetersMedia = new DynamicParameters();
                        parmetersMedia.Add("@MKEY", tASK_END_LIST_DETAILS.DOC_MKEY);
                        parmetersMedia.Add("@USER_ID", tASK_END_LIST_DETAILS.USER_ID);
                        var TaskEndListMedia = await db.QueryAsync<TASK_OUTPUT_MEDIA>("SP_GET_TASK_ENDLIST_MEDIA", parmetersMedia, commandType: CommandType.StoredProcedure, transaction: transaction);

                        foreach (var OutputMedia in GetTaskEnd)
                        {
                            OutputMedia.TASK_OUTPUT_ATTACHMENT = TaskEndListMedia.ToList();
                        }

                        var sqlTransaction = (SqlTransaction)transaction;
                        await sqlTransaction.CommitAsync();
                        transactionCompleted = true;

                        var successsResult = new List<TASK_ENDLIST_DETAILS_OUTPUT_LIST>
                            {
                            new TASK_ENDLIST_DETAILS_OUTPUT_LIST
                                {
                                STATUS = "Ok",
                                MESSAGE = "Get data successfully!!!",
                                DATA= GetTaskEnd
                                }
                        };
                        return successsResult;
                    }
                    else
                    {
                        var errorResult = new List<TASK_ENDLIST_DETAILS_OUTPUT_LIST>
                                {
                                    new TASK_ENDLIST_DETAILS_OUTPUT_LIST
                                    {
                                        STATUS = "Error",
                                        MESSAGE = "Data not found",
                                        DATA = null
                                    }
                                };
                        return errorResult;
                    }
                }
            }
            catch (Exception ex)
            {
                var errorResult = new List<TASK_ENDLIST_DETAILS_OUTPUT_LIST>
                    {
                        new TASK_ENDLIST_DETAILS_OUTPUT_LIST
                        {
                            STATUS = "Error",
                            MESSAGE = ex.Message,
                            DATA = null
                        }
                    };
                return errorResult;
            }
        }
        public async Task<ActionResult<IEnumerable<TASK_COMPLIANCE_CHECK_LIST>>> GetTaskCheckListAsync(TASK_COMPLIANCE_INPUT tASK_COMPLIANCE_INPUT)
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
                    parmeters.Add("@PROPERTY_MKEY", tASK_COMPLIANCE_INPUT.PROPERTY_MKEY);
                    parmeters.Add("@BUILDING_MKEY", tASK_COMPLIANCE_INPUT.BUILDING_MKEY);
                    parmeters.Add("@TASK_MKEY", tASK_COMPLIANCE_INPUT.TASK_MKEY);
                    parmeters.Add("@USER_ID", tASK_COMPLIANCE_INPUT.USER_ID);
                    parmeters.Add("@API_NAME", "GetTaskCompliance");
                    parmeters.Add("@API_METHOD", "Get");
                    var GetTaskEnd = await db.QueryAsync<TASK_COMPLIANCE_CHECK_LIST_OUTPUT>("SP_GET_TASK_CHECKLIST", parmeters, commandType: CommandType.StoredProcedure, transaction: transaction);

                    if (GetTaskEnd.Any())
                    {
                        //foreach (var item in GetTaskEnd)
                        //{
                        //    if (item.CHECK_DOC_LST == null)
                        //    {
                        //        item.CHECK_DOC_LST = new Dictionary<string, object>(); // Initialize if null
                        //    }
                        //    // Assuming DOCUMENT_NAME is the key and DOCUMENT_CATEGORY is the value
                        //    item.CHECK_DOC_LST.Add(item.DOCUMENT_NAME.ToString(), item.DOCUMENT_CATEGORY);
                        //}

                        var sqlTransaction = (SqlTransaction)transaction;
                        await sqlTransaction.CommitAsync();
                        transactionCompleted = true;

                        foreach (var TaskCompliance in GetTaskEnd)
                        {
                            if (TaskCompliance.MKEY < 1)
                            {
                                var errorResult = new List<TASK_COMPLIANCE_CHECK_LIST>
                                {
                                    new TASK_COMPLIANCE_CHECK_LIST
                                    {
                                        STATUS = "Error",
                                        MESSAGE = "Data not found",
                                        DATA = null
                                    }
                                };
                                return errorResult;
                            }
                        }
                        var successsResult = new List<TASK_COMPLIANCE_CHECK_LIST>
                            {
                            new TASK_COMPLIANCE_CHECK_LIST
                                {
                                STATUS = "Ok",
                                MESSAGE = "Get data successfully!!!",
                                DATA= GetTaskEnd
                                }
                        };
                        return successsResult;
                    }
                    else
                    {
                        var errorResult = new List<TASK_COMPLIANCE_CHECK_LIST>
                                {
                                    new TASK_COMPLIANCE_CHECK_LIST
                                    {
                                        STATUS = "Error",
                                        MESSAGE = "Data not found",
                                        DATA = null
                                    }
                                };
                        return errorResult;
                    }
                }
            }
            catch (Exception ex)
            {
                var errorResult = new List<TASK_COMPLIANCE_CHECK_LIST>
                    {
                        new TASK_COMPLIANCE_CHECK_LIST
                        {
                            STATUS = "Error",
                            MESSAGE = ex.Message,
                            DATA = null
                        }
                    };
                return errorResult;
            }
        }
        public async Task<ActionResult<IEnumerable<TASK_ENDLIST_DETAILS_OUTPUT_LIST>>> PostTaskEndListInsertUpdateAsync(TASK_ENDLIST_INPUT tASK_ENDLIST_INPUT)
        {
            DateTime dateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
            IDbTransaction transaction = null;
            int ProjectDAttach = 0;
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
                        await sqlConnection.OpenAsync();
                    }

                    // Start a new transaction
                    transaction = db.BeginTransaction();
                    transactionCompleted = false;

                    // Prepare the parameters for the stored procedure
                    var parameters = new DynamicParameters();
                    parameters.Add("@MKEY", tASK_ENDLIST_INPUT.MKEY);
                    parameters.Add("@SR_NO", tASK_ENDLIST_INPUT.SR_NO);
                    parameters.Add("@DOC_MKEY", tASK_ENDLIST_INPUT.DOC_MKEY);
                    parameters.Add("@PROPERTY_MKEY", tASK_ENDLIST_INPUT.PROPERTY_MKEY);
                    parameters.Add("@BUILDING_MKEY", tASK_ENDLIST_INPUT.BUILDING_MKEY);
                    parameters.Add("@DOC_NUMBER", tASK_ENDLIST_INPUT.DOC_NUMBER);
                    parameters.Add("@DOC_DATE", tASK_ENDLIST_INPUT.DOC_DATE);
                    parameters.Add("@VALIDITY_DATE", tASK_ENDLIST_INPUT.VALIDITY_DATE);
                    parameters.Add("@CREATED_BY", tASK_ENDLIST_INPUT.CREATED_BY);
                    parameters.Add("@DELETE_FLAG", tASK_ENDLIST_INPUT.DELETE_FLAG);
                    parameters.Add("@API_NAME", "Task-Output-Doc-Insert-Update");
                    parameters.Add("@API_METHOD", "Insert/Update");

                    var GetTaskEnd = await db.QueryAsync<TASK_ENDLIST_DETAILS_OUTPUT>("SP_INSET_UPDATE_TASK_ENDLIST", parameters, commandType: CommandType.StoredProcedure, transaction: transaction);

                    if (GetTaskEnd.Any())
                    {
                        if (tASK_ENDLIST_INPUT.PROJECT_DOC_FILES != null)
                        {
                            var descriptions = GetTaskEnd
                                .Select(x =>
                                {
                                    var value = x.GetType().GetProperty("MKEY").GetValue(x);
                                    return value is int ? (int)value : 0; // Default to 0 if not an int
                                })
                                .ToList();

                            ProjectDAttach = Convert.ToInt32(descriptions[0]);
                            string filePathOpen = string.Empty;

                            // Create directory for file storage if it doesn't exist
                            string filePath = _fileSettings.FilePath;
                            string directoryPath = Path.Combine(filePath, "Attachments", "Document Depository", ProjectDAttach.ToString());
                            if (!Directory.Exists(directoryPath))
                            {
                                Directory.CreateDirectory(directoryPath);
                            }

                            // Save the file to the directory
                            string fileName = $"{DateTime.Now.Day}_{DateTime.Now.ToShortTimeString().Replace(":", "_")}_{tASK_ENDLIST_INPUT.PROJECT_DOC_FILES.FileName}";
                            string fullFilePath = Path.Combine(directoryPath, fileName);
                            using (FileStream fileStream = new FileStream(fullFilePath, FileMode.Create))
                            {
                                await tASK_ENDLIST_INPUT.PROJECT_DOC_FILES.CopyToAsync(fileStream);
                                fileStream.Flush();
                            }

                            filePathOpen = Path.Combine("Attachments", "Document Depository", ProjectDAttach.ToString(), fileName);

                            // Insert file metadata into the database
                            var parametersFiles = new DynamicParameters();
                            parametersFiles.Add("@MKEY", tASK_ENDLIST_INPUT.MKEY);
                            parametersFiles.Add("@SR_NO", tASK_ENDLIST_INPUT.SR_NO);
                            parametersFiles.Add("@DOC_MKEY", tASK_ENDLIST_INPUT.DOC_MKEY);
                            parametersFiles.Add("@FILE_NAME", tASK_ENDLIST_INPUT.PROJECT_DOC_FILES.FileName);
                            parametersFiles.Add("@FILE_PATH", filePathOpen);
                            parametersFiles.Add("@CREATED_BY", tASK_ENDLIST_INPUT.CREATED_BY);
                            parametersFiles.Add("@DELETE_FLAG", "N");
                            parametersFiles.Add("@APINAME", "CreateTaskEndlistAttach");
                            parametersFiles.Add("@API_METHOD", "Create/Update");

                            // Execute stored procedure for file insert
                            var TaskEndListMedia = await db.QueryAsync<TASK_OUTPUT_MEDIA>("SP_INSERT_TASK_ENDLIST_ATTACHMENT", parametersFiles, commandType: CommandType.StoredProcedure, transaction: transaction);

                            // Assign the file metadata to the task end list output
                            foreach (var OutputMedia in GetTaskEnd)
                            {
                                OutputMedia.TASK_OUTPUT_ATTACHMENT = TaskEndListMedia.ToList();
                            }
                        }
                        else
                        {
                            var parametersFiles = new DynamicParameters();
                            parametersFiles.Add("@MKEY", tASK_ENDLIST_INPUT.MKEY);
                            parametersFiles.Add("@SR_NO", tASK_ENDLIST_INPUT.SR_NO);
                            parametersFiles.Add("@DOC_MKEY", tASK_ENDLIST_INPUT.DOC_MKEY);
                            parametersFiles.Add("@CREATED_BY", tASK_ENDLIST_INPUT.CREATED_BY);
                            parametersFiles.Add("@DELETE_FLAG", "Y");
                            parametersFiles.Add("@APINAME", "CreateTaskEndlistAttach");
                            parametersFiles.Add("@API_METHOD", "Create/Update");

                            // Execute stored procedure for file insert
                            var TaskEndListMedia = await db.QueryAsync<TASK_OUTPUT_MEDIA>("SP_INSERT_TASK_ENDLIST_ATTACHMENT", parametersFiles, commandType: CommandType.StoredProcedure, transaction: transaction);
                            foreach (var OutputMedia in GetTaskEnd)
                            {
                                OutputMedia.TASK_OUTPUT_ATTACHMENT = null;
                            }
                        }

                        // Commit the transaction after database operations and file handling
                        var sqlTransaction = (SqlTransaction)transaction;
                        await sqlTransaction.CommitAsync();
                        transactionCompleted = true;

                        // Return success response
                        return new List<TASK_ENDLIST_DETAILS_OUTPUT_LIST>
                    {
                        new TASK_ENDLIST_DETAILS_OUTPUT_LIST
                        {
                            STATUS = "Ok",
                            MESSAGE = "Data processed successfully",
                            DATA = GetTaskEnd
                        }
                    };
                    }
                    else
                    {
                        // If an error occurs, rollback the transaction
                        if (transaction != null && !transactionCompleted)
                        {
                            transaction.Rollback();
                        }

                        // Return error response
                        return new List<TASK_ENDLIST_DETAILS_OUTPUT_LIST>
                            {
                                new TASK_ENDLIST_DETAILS_OUTPUT_LIST
                                {
                                    STATUS = "Error",
                                    MESSAGE = "Error occurd",
                                    DATA = null
                                }
                            };
                    }

                }
            }
            catch (Exception ex)
            {
                // If an error occurs, rollback the transaction
                if (transaction != null && !transactionCompleted)
                {
                    transaction.Rollback();
                }

                // Return error response
                return new List<TASK_ENDLIST_DETAILS_OUTPUT_LIST>
                    {
                        new TASK_ENDLIST_DETAILS_OUTPUT_LIST
                        {
                            STATUS = "Error",
                            MESSAGE = ex.Message,
                            DATA = null
                        }
                    };
            }
        }
        //public async Task<ActionResult<IEnumerable<TASK_ENDLIST_DETAILS_OUTPUT_LIST>>> PostTaskEndListInsertUpdateAsync(TASK_ENDLIST_INPUT tASK_ENDLIST_INPUT)
        //{
        //    DateTime dateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
        //    IDbTransaction transaction = null;
        //    int ProjectDAttach = 0;
        //    bool transactionCompleted = false;
        //    try
        //    {
        //        using (IDbConnection db = _dapperDbConnection.CreateConnection())
        //        {
        //            var sqlConnection = db as SqlConnection;
        //            if (sqlConnection == null)
        //            {
        //                throw new InvalidOperationException("The connection must be a SqlConnection to use OpenAsync.");
        //            }

        //            if (sqlConnection.State != ConnectionState.Open)
        //            {
        //                await sqlConnection.OpenAsync();
        //            }

        //            transaction = db.BeginTransaction();
        //            transactionCompleted = false;

        //            var parameters = new DynamicParameters();
        //            parameters.Add("@MKEY", tASK_ENDLIST_INPUT.MKEY);
        //            parameters.Add("@DOC_MKEY", tASK_ENDLIST_INPUT.DOC_MKEY);
        //            parameters.Add("@PROPERTY_MKEY", tASK_ENDLIST_INPUT.PROPERTY_MKEY);
        //            parameters.Add("@BUILDING_MKEY", tASK_ENDLIST_INPUT.BUILDING_MKEY);
        //            parameters.Add("@DOC_NUMBER", tASK_ENDLIST_INPUT.DOC_NUMBER);
        //            parameters.Add("@DOC_DATE", tASK_ENDLIST_INPUT.DOC_DATE);
        //            parameters.Add("@VALIDITY_DATE", tASK_ENDLIST_INPUT.VALIDITY_DATE);
        //            parameters.Add("@CREATED_BY", tASK_ENDLIST_INPUT.CREATED_BY);
        //            parameters.Add("@DELETE_FLAG", tASK_ENDLIST_INPUT.DELETE_FLAG);
        //            parameters.Add("@API_NAME", "Task-Output-Doc-Insert-Update");
        //            parameters.Add("@API_METHOD", "Insert/Update");

        //            var GetTaskEnd = await db.QueryAsync<TASK_ENDLIST_DETAILS_OUTPUT>("SP_INSET_UPDATE_TASK_ENDLIST", parameters, commandType: CommandType.StoredProcedure, transaction: transaction);

        //            // Handle single file upload
        //            if (tASK_ENDLIST_INPUT.PROJECT_DOC_FILES != null)
        //            {
        //                var descriptions = GetTaskEnd
        //                .Select(x =>
        //                {
        //                    var value = x.GetType().GetProperty("MKEY").GetValue(x);
        //                    return value is int ? (int)value : 0;  // Default to 0 if not an int
        //                })
        //                .ToList();
        //                ProjectDAttach = Convert.ToInt32(descriptions[0]);
        //                int srNo = 0;
        //                string filePathOpen = string.Empty;

        //                // Create directory if it doesn't exist
        //                string filePath = _fileSettings.FilePath;
        //                if (!Directory.Exists(filePath + "\\Attachments\\" + "Document Depository\\" + ProjectDAttach))
        //                {
        //                    Directory.CreateDirectory(filePath + "\\Attachments\\" + "Document Depository\\" + ProjectDAttach);
        //                }

        //                using (FileStream filestream = System.IO.File.Create(filePath + "\\Attachments\\" + "Document Depository\\" +
        //                    ProjectDAttach + "\\" + DateTime.Now.Day + "_" + DateTime.Now.ToShortTimeString().Replace(":", "_") + "_" + tASK_ENDLIST_INPUT.PROJECT_DOC_FILES.FileName))
        //                {
        //                    await tASK_ENDLIST_INPUT.PROJECT_DOC_FILES.CopyToAsync(filestream);
        //                    filestream.Flush();
        //                }

        //                filePathOpen = "\\Attachments\\" + "Document Depository\\" + ProjectDAttach + "\\" +
        //                    DateTime.Now.Day + "_" + DateTime.Now.ToShortTimeString().Replace(":", "_") + "_" + tASK_ENDLIST_INPUT.PROJECT_DOC_FILES.FileName;

        //                // Insert file metadata into the database
        //                var parametersFiles = new DynamicParameters();
        //                parametersFiles.Add("@PROJECT_DOC_MKEY", ProjectDAttach);
        //                parametersFiles.Add("@FILE_NAME", tASK_ENDLIST_INPUT.PROJECT_DOC_FILES.FileName);
        //                parametersFiles.Add("@FILE_PATH", filePathOpen);
        //                parametersFiles.Add("@CREATED_BY", tASK_ENDLIST_INPUT.CREATED_BY);
        //                parametersFiles.Add("@DELETE_FLAG", "N");
        //                parametersFiles.Add("@APINAME", "CreateProjectDocDeositoryAsync");
        //                parametersFiles.Add("@API_METHOD", "Create");

        //                //var InsertProjectDocFile = await db.QueryAsync<DocFileUploadOutPut>("SP_INSERT_PROJECT_DOC_DEPOSITORY_ATTACHMENT", parametersFiles, commandType: CommandType.StoredProcedure, transaction: transaction);

        //                //foreach (var OutputMedia in GetTaskEnd)
        //                //{
        //                //    OutputMedia.TASK_OUTPUT_ATTACHMENT = InsertProjectDocFile.ToList();
        //                //}

        //                var TaskEndListMedia = await db.QueryAsync<TASK_OUTPUT_MEDIA>("SP_INSERT_PROJECT_DOC_DEPOSITORY_ATTACHMENT", parametersFiles, commandType: CommandType.StoredProcedure, transaction: transaction);

        //                foreach (var OutputMedia in GetTaskEnd)
        //                {
        //                    OutputMedia.TASK_OUTPUT_ATTACHMENT = TaskEndListMedia.ToList();
        //                }

        //            }

        //            // Commit the transaction if everything is successful
        //            var sqlTransaction = (SqlTransaction)transaction;
        //            await sqlTransaction.CommitAsync();
        //            transactionCompleted = true;

        //            // Return success
        //            return new List<TASK_ENDLIST_DETAILS_OUTPUT_LIST>
        //            {
        //                new TASK_ENDLIST_DETAILS_OUTPUT_LIST
        //                {
        //                    STATUS = "Ok",
        //                    MESSAGE = "Data processed successfully",
        //                    DATA = GetTaskEnd
        //                }
        //            };
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        if (transaction != null && !transactionCompleted)
        //        {
        //            transaction.Rollback();
        //        }

        //        return new List<TASK_ENDLIST_DETAILS_OUTPUT_LIST>
        //        {
        //            new TASK_ENDLIST_DETAILS_OUTPUT_LIST
        //            {
        //                STATUS = "Error",
        //                MESSAGE = ex.Message,
        //                DATA = null
        //            }
        //        };
        //    }
        //}

        //public async Task<ActionResult<IEnumerable<TASK_ENDLIST_DETAILS_OUTPUT_LIST>>> PostTaskEndListInsertUpdateAsync(TASK_ENDLIST_INPUT tASK_ENDLIST_INPUT)
        //{
        //    DateTime dateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
        //    IDbTransaction transaction = null;
        //    int ProjectDAttach = 0;
        //    bool transactionCompleted = false;  // Track the transaction state
        //    try
        //    {
        //        using (IDbConnection db = _dapperDbConnection.CreateConnection())
        //        {
        //            var sqlConnection = db as SqlConnection;
        //            if (sqlConnection == null)
        //            {
        //                throw new InvalidOperationException("The connection must be a SqlConnection to use OpenAsync.");
        //            }

        //            if (sqlConnection.State != ConnectionState.Open)
        //            {
        //                await sqlConnection.OpenAsync();  // Ensure the connection is open
        //            }

        //            transaction = db.BeginTransaction();
        //            transactionCompleted = false;  // Reset transaction state

        //            var parmeters = new DynamicParameters();
        //            parmeters.Add("@MKEY", tASK_ENDLIST_INPUT.MKEY);
        //            parmeters.Add("@DOC_MKEY", tASK_ENDLIST_INPUT.DOC_MKEY);
        //            parmeters.Add("@PROPERTY_MKEY", tASK_ENDLIST_INPUT.PROPERTY_MKEY);
        //            parmeters.Add("@BUILDING_MKEY", tASK_ENDLIST_INPUT.BUILDING_MKEY);
        //            parmeters.Add("@DOC_NUMBER", tASK_ENDLIST_INPUT.DOC_NUMBER);
        //            parmeters.Add("@DOC_DATE", tASK_ENDLIST_INPUT.DOC_DATE);
        //            parmeters.Add("@VALIDITY_DATE", tASK_ENDLIST_INPUT.VALIDITY_DATE);
        //            parmeters.Add("@CREATED_BY", tASK_ENDLIST_INPUT.CREATED_BY);
        //            parmeters.Add("@DELETE_FLAG", tASK_ENDLIST_INPUT.DELETE_FLAG);
        //            parmeters.Add("@API_NAME", "Task-Output-Doc-Insert-Update");
        //            parmeters.Add("@API_METHOD", "Insert/Update");

        //            var GetTaskEnd = await db.QueryAsync<TASK_ENDLIST_DETAILS_OUTPUT>("SP_INSET_UPDATE_TASK_ENDLIST", parmeters, commandType: CommandType.StoredProcedure, transaction: transaction);

        //            var descriptions = GetTaskEnd
        //                .Select(x =>
        //                {
        //                    var value = x.GetType().GetProperty("MKEY").GetValue(x);
        //                    return value is int ? (int)value : 0;  // Default to 0 if not an int
        //                })
        //                .ToList();
        //            ProjectDAttach = Convert.ToInt32(descriptions[0]);
        //            if (GetTaskEnd.Any())
        //            {
        //                try
        //                {
        //                    int srNo = 0;
        //                    string filePathOpen = string.Empty;

        //                    if (tASK_ENDLIST_INPUT.PROJECT_DOC_FILES != null)
        //                    {

        //                        var parametersProjectAttachMkey = new DynamicParameters();
        //                        parametersProjectAttachMkey.Add("@PROJECT_DOC_MKEY", ProjectDAttach);
        //                        parametersProjectAttachMkey.Add("@CREATED_BY", tASK_ENDLIST_INPUT.CREATED_BY);
        //                        parametersProjectAttachMkey.Add("@APINAME", "Task-Output-Doc-Insert-Update");
        //                        parametersProjectAttachMkey.Add("@APIMETHOD", "Insert");

        //                        var ProjectAttachMkey = await db.QueryAsync<PROJECT_DOC_DEPOSITORY_HDR>("SP_UPDATE_PROJECT_DOC_DEPOSITORY_ATTACHMENT", parametersProjectAttachMkey, commandType: CommandType.StoredProcedure, transaction: transaction);

        //                        foreach (var ProjectDocFile in tASK_ENDLIST_INPUT.PROJECT_DOC_FILES)
        //                        {
        //                            if (ProjectDocFile.Length > 0)
        //                            {
        //                                srNo = srNo + 1;
        //                                string FilePath = _fileSettings.FilePath;
        //                                if (!Directory.Exists(FilePath + "\\Attachments\\" + "Document Depository\\" + ProjectDAttach))
        //                                {
        //                                    Directory.CreateDirectory(FilePath + "\\Attachments\\" + "Document Depository\\" + ProjectDAttach);
        //                                }
        //                                using (FileStream filestream = System.IO.File.Create(FilePath + "\\Attachments\\" + "Document Depository\\"
        //                                    + ProjectDAttach + "\\" + DateTime.Now.Day + "_" + DateTime.Now.ToShortTimeString().Replace(":", "_") + "_" + ProjectDocFile.FileName))
        //                                {
        //                                    ProjectDocFile.CopyTo(filestream);
        //                                    filestream.Flush();
        //                                }

        //                                filePathOpen = "\\Attachments\\" + "Document Depository\\" + ProjectDAttach + "\\"
        //                                    + DateTime.Now.Day + "_" + DateTime.Now.ToShortTimeString().Replace(":", "_") + "_"
        //                                    + ProjectDocFile.FileName;

        //                                var parametersFiles = new DynamicParameters();
        //                                parametersFiles.Add("@PROJECT_DOC_MKEY", ProjectDAttach);
        //                                parametersFiles.Add("@FILE_NAME", ProjectDocFile.FileName);
        //                                parametersFiles.Add("@FILE_PATH", filePathOpen);
        //                                parametersFiles.Add("@CREATED_BY", tASK_ENDLIST_INPUT.CREATED_BY);
        //                                parametersFiles.Add("@DELETE_FLAG", "N");
        //                                parametersFiles.Add("@APINAME", "CreateProjectDocDeositoryAsync");
        //                                parametersFiles.Add("@API_METHOD", "Create");

        //                                var InsertProjectDocFile = await db.QueryAsync<DocFileUploadOutPut>("SP_INSERT_PROJECT_DOC_DEPOSITORY_ATTACHMENT", parametersFiles, commandType: CommandType.StoredProcedure, transaction: transaction);

        //                                if (InsertProjectDocFile.Any())
        //                                {

        //                                }
        //                                if (InsertProjectDocFile == null)
        //                                {
        //                                    // Handle other unexpected exceptions
        //                                    RollbackTransaction(transaction);
        //                                    var errorResult = new List<TASK_ENDLIST_DETAILS_OUTPUT_LIST>
        //                                    {
        //                                        new TASK_ENDLIST_DETAILS_OUTPUT_LIST
        //                                        {
        //                                            STATUS = "Error",
        //                                            MESSAGE = "An error occurred",
        //                                            DATA = null
        //                                        }
        //                                    };
        //                                    return errorResult;
        //                                }
        //                            }
        //                        }
        //                    }

        //                }
        //                catch (Exception ex)
        //                {
        //                    RollbackTransaction(transaction);

        //                    var errorResult = new List<TASK_ENDLIST_DETAILS_OUTPUT_LIST>
        //                    {
        //                        new TASK_ENDLIST_DETAILS_OUTPUT_LIST
        //                        {
        //                            STATUS = "Error",
        //                            MESSAGE = ex.Message,
        //                            DATA = null
        //                        }
        //                    };
        //                    return errorResult;
        //                }

        //                if (tASK_ENDLIST_INPUT.PROJECT_DOC_FILES != null)
        //                {
        //                    //var parametersDepost = new DynamicParameters();
        //                    //parametersDepost.Add("@PROJECT_DOC_MKEY", ProjectDAttach);
        //                    //parametersDepost.Add("@CREATED_BY", tASK_ENDLIST_INPUT.CREATED_BY);
        //                    //parametersDepost.Add("@APINAME", "CreateProjectDocDeositoryAsync");
        //                    //parametersDepost.Add("@API_METHOD", "Create");
        //                    //var GetProjectDocFile = await db.QueryAsync<DocFileUploadOutPut>("SP_GET_PROJECT_DOC_DEPOSITORY_ATTACHMENT", parametersDepost, commandType: CommandType.StoredProcedure, transaction: transaction);

        //                    var parmetersMedia = new DynamicParameters();
        //                    parmetersMedia.Add("@MKEY", ProjectDAttach);
        //                    parmetersMedia.Add("@USER_ID", tASK_ENDLIST_INPUT.CREATED_BY);
        //                    var TaskEndListMedia = await db.QueryAsync<TASK_OUTPUT_MEDIA>("SP_GET_TASK_ENDLIST_MEDIA", parmetersMedia, commandType: CommandType.StoredProcedure, transaction: transaction);

        //                    foreach (var OutputMedia in GetTaskEnd)
        //                    {
        //                        OutputMedia.TASK_OUTPUT_ATTACHMENT = TaskEndListMedia.ToList();
        //                    }
        //                }

        //                var sqlTransaction = (SqlTransaction)transaction;
        //                await sqlTransaction.CommitAsync();
        //                transactionCompleted = true;

        //                var successsResult = new List<TASK_ENDLIST_DETAILS_OUTPUT_LIST>
        //                    {
        //                    new TASK_ENDLIST_DETAILS_OUTPUT_LIST
        //                        {
        //                        STATUS = "Ok",
        //                        MESSAGE = "Get data successfully!!!",
        //                        DATA= GetTaskEnd
        //                        }
        //                };
        //                return successsResult;
        //            }
        //            else
        //            {
        //                if (transaction != null && !transactionCompleted)
        //                {
        //                    try
        //                    {
        //                        // Rollback only if the transaction is not yet completed
        //                        transaction.Rollback();
        //                    }
        //                    catch (InvalidOperationException rollbackEx)
        //                    {

        //                        Console.WriteLine($"Rollback failed: {rollbackEx.Message}");
        //                        //TranError.Message = ex.Message;
        //                        //return TranError;
        //                    }
        //                }
        //                var errorResult = new List<TASK_ENDLIST_DETAILS_OUTPUT_LIST>
        //                {
        //                    new TASK_ENDLIST_DETAILS_OUTPUT_LIST
        //                    {
        //                        STATUS = "Error",
        //                        MESSAGE = "An Error occured",
        //                        DATA = null
        //                    }
        //                };
        //                return errorResult;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
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
        //        var errorResult = new List<TASK_ENDLIST_DETAILS_OUTPUT_LIST>
        //            {
        //                new TASK_ENDLIST_DETAILS_OUTPUT_LIST
        //                {
        //                    STATUS = "Error",
        //                    MESSAGE = ex.Message,
        //                    DATA = null
        //                }
        //            };
        //        return errorResult;
        //    }
        //}
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
        //private List<UpdateProjectDocDepositoryHDROutput_List> GenerateErrorResponse(string message)
        //{
        //    return new List<UpdateProjectDocDepositoryHDROutput_List>
        //    {
        //        new UpdateProjectDocDepositoryHDROutput_List
        //        {
        //            STATUS = "Error",
        //            MESSAGE = message,
        //            DATA = null
        //        }
        //    };
        //}
        public async Task<ActionResult<IEnumerable<TASK_COMPLIANCE_CHECK_LIST>>> PostTaskCheckListInsertUpdateAsync(TASK_CHECKLIST_INPUT tASK_CHECKLIST_INPUT)
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
                    parmeters.Add("@PROPERTY_MKEY", tASK_CHECKLIST_INPUT.PROPERTY_MKEY);
                    parmeters.Add("@BUILDING_MKEY", tASK_CHECKLIST_INPUT.BUILDING_MKEY);
                    parmeters.Add("@SR_NO", tASK_CHECKLIST_INPUT.SR_NO);
                    //parmeters.Add("@DOC_NAME", tASK_CHECKLIST_INPUT.DOC_NAME);
                    parmeters.Add("@DOC_MKEY", tASK_CHECKLIST_INPUT.DOC_MKEY);
                    parmeters.Add("@APP_CHECK", tASK_CHECKLIST_INPUT.APP_CHECK);
                    parmeters.Add("@TASK_MKEY", tASK_CHECKLIST_INPUT.TASK_MKEY);
                    parmeters.Add("@CREATED_BY", tASK_CHECKLIST_INPUT.CREATED_BY);
                    parmeters.Add("@API_NAME", "Task-CheckList-Doc-Insert-Update");
                    parmeters.Add("@API_METHOD", "Insert/Update");

                    var GetTaskEnd = await db.QueryAsync<TASK_COMPLIANCE_CHECK_LIST_OUTPUT>("SP_INSERT_UPDATE_TASK_CHECKLIST", parmeters, commandType: CommandType.StoredProcedure, transaction: transaction);

                    if (GetTaskEnd.Any())
                    {
                        //foreach (var item in GetTaskEnd)
                        //{
                        //    if (item.CHECK_DOC_LST == null)
                        //    {
                        //        item.CHECK_DOC_LST = new Dictionary<string, object>(); // Initialize if null
                        //    }
                        //    // Assuming DOCUMENT_NAME is the key and DOCUMENT_CATEGORY is the value
                        //    item.CHECK_DOC_LST.Add(item.DOCUMENT_NAME.ToString(), item.DOCUMENT_CATEGORY);
                        //}

                        // Commit the transaction if everything is successful
                        var sqlTransaction = (SqlTransaction)transaction;
                        await sqlTransaction.CommitAsync();
                        transactionCompleted = true;

                        foreach (var TaskCompliance in GetTaskEnd)
                        {
                            if (TaskCompliance.MKEY < 1)
                            {
                                var errorResult = new List<TASK_COMPLIANCE_CHECK_LIST>
                                {
                                    new TASK_COMPLIANCE_CHECK_LIST
                                    {
                                        STATUS = "Error",
                                        MESSAGE = "Data not found",
                                        DATA = null
                                    }
                                };
                                return errorResult;
                            }
                        }

                        var successsResult = new List<TASK_COMPLIANCE_CHECK_LIST>
                                {
                                    new TASK_COMPLIANCE_CHECK_LIST
                                    {
                                        STATUS = "Ok",
                                        MESSAGE = "Get data successfully!!!",
                                        DATA = GetTaskEnd
                                    }
                                };
                        return successsResult;
                    }
                    else
                    {
                        var errorResult = new List<TASK_COMPLIANCE_CHECK_LIST>
                                {
                                    new TASK_COMPLIANCE_CHECK_LIST
                                    {
                                        STATUS = "Error",
                                        MESSAGE = "Data not found",
                                        DATA = null
                                    }
                                };
                        return errorResult;
                    }
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
                var errorResult = new List<TASK_COMPLIANCE_CHECK_LIST>
                {
                    new TASK_COMPLIANCE_CHECK_LIST
                    {
                        STATUS = "Error",
                        MESSAGE = $"SQL Error: {sqlEx.Message}",
                        DATA = null
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
                var errorResult = new List<TASK_COMPLIANCE_CHECK_LIST>
                        {
                            new TASK_COMPLIANCE_CHECK_LIST
                            {
                                STATUS = "Error",
                                MESSAGE = ex.Message,
                                DATA = null
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
        public async Task<ActionResult<IEnumerable<TaskSanctioningDepartmentOutputList>>> PostTaskSanctioningAuthorityAsync(TASK_SANCTIONING_AUTHORITY_INPUT tASK_SANCTIONING_AUTHORITY_INPUT)
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
                    parmeters.Add("@MKEY", tASK_SANCTIONING_AUTHORITY_INPUT.MKEY);
                    parmeters.Add("@SR_NO", tASK_SANCTIONING_AUTHORITY_INPUT.SR_NO);
                    parmeters.Add("@LEVEL", tASK_SANCTIONING_AUTHORITY_INPUT.LEVEL);
                    parmeters.Add("@PROPERTY_MKEY", tASK_SANCTIONING_AUTHORITY_INPUT.PROPERTY_MKEY);
                    parmeters.Add("@BUILDING_MKEY", tASK_SANCTIONING_AUTHORITY_INPUT.BUILDING_MKEY);
                    parmeters.Add("@STATUS", tASK_SANCTIONING_AUTHORITY_INPUT.STATUS);
                    parmeters.Add("@APINAME", "UPDATE SANSACTING DEPARTMENT");
                    parmeters.Add("@APIMETHOD", "UPDATE");
                    parmeters.Add("@CREATED_BY", tASK_SANCTIONING_AUTHORITY_INPUT.CREATED_BY);

                    var GetTaskSanDepart = await db.QueryAsync<TaskSanctioningDepartmentOutput>("SP_INERT_TASK_SANCTIONING_AUTHORITY", parmeters, commandType: CommandType.StoredProcedure, transaction: transaction);

                    if (GetTaskSanDepart.Any())
                    {
                        var sqlTransaction = (SqlTransaction)transaction;
                        await sqlTransaction.CommitAsync();
                        transactionCompleted = true;
                        var successsResult = new List<TaskSanctioningDepartmentOutputList>
                            {
                            new TaskSanctioningDepartmentOutputList
                                {
                                STATUS = "Ok",
                                MESSAGE = "Inserted data successfully!!!",
                                DATA= GetTaskSanDepart
                                }
                        };
                        return successsResult;
                    }
                    RollbackTransaction(transaction);
                    var errorResult = new List<TaskSanctioningDepartmentOutputList>
                                {
                                    new TaskSanctioningDepartmentOutputList
                                    {
                                        STATUS = "Error",
                                        MESSAGE = "Error occured",
                                        DATA = null
                                    }
                                };
                    return errorResult;
                }
            }
            catch (Exception ex)
            {
                RollbackTransaction(transaction);
                var errorResult = new List<TaskSanctioningDepartmentOutputList>
                    {
                        new TaskSanctioningDepartmentOutputList
                        {
                            STATUS = "Error",
                            MESSAGE = ex.Message,
                            DATA = null
                        }
                    };
                return errorResult;
            }
        }
        public async Task<ActionResult<IEnumerable<TaskCheckListOutputList>>> PostTaskCheckListTableInsertUpdateAsync(TASK_CHECKLIST_TABLE_INPUT tASK_CHECKLIST_TABLE_INPUT)
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
                    parmeters.Add("@TASK_MKEY", tASK_CHECKLIST_TABLE_INPUT.TASK_MKEY);
                    parmeters.Add("@SR_NO", tASK_CHECKLIST_TABLE_INPUT.SR_NO);
                    parmeters.Add("@DOCUMENT_MKEY", tASK_CHECKLIST_TABLE_INPUT.DOC_MKEY);
                    parmeters.Add("@DOCUMENT_CATEGORY", tASK_CHECKLIST_TABLE_INPUT.DOCUMENT_CATEGORY);
                    parmeters.Add("@CREATED_BY", tASK_CHECKLIST_TABLE_INPUT.CREATED_BY);
                    parmeters.Add("@DELETE_FLAG", tASK_CHECKLIST_TABLE_INPUT.DELETE_FLAG);
                    parmeters.Add("@METHOD_NAME", "Task-CheckList-Doc-Insert-Update");
                    parmeters.Add("@METHOD", "Insert/Update");
                    parmeters.Add("@OUT_STATUS", null);
                    parmeters.Add("@OUT_MESSAGE", null);

                    var GetTaskEnd = await db.QueryAsync<TASK_CHECKLIST_TABLE_OUTPUT>("SP_INSERT_UPDATE_TABLE_TASK_CHECKLIST", parmeters, commandType: CommandType.StoredProcedure, transaction: transaction);

                    if (GetTaskEnd.Any())
                    {
                        foreach (var Response in GetTaskEnd)
                        {
                            if (Response.OUT_STATUS != "OK")
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
                                        Console.WriteLine($"Rollback failed: {rollbackEx.Message}");
                                    }
                                }

                                var errorResult = new List<TaskCheckListOutputList>
                                {
                                    new TaskCheckListOutputList
                                    {
                                        STATUS = "Error",
                                        MESSAGE = Response.OUT_MESSAGE,
                                        DATA = GetTaskEnd
                                    }
                                };
                                return errorResult;
                            }
                        }
                        var sqlTransaction = (SqlTransaction)transaction;
                        await sqlTransaction.CommitAsync();
                        transactionCompleted = true;

                        //foreach (var TaskCompliance in GetTaskEnd)
                        //{
                        //    if (TaskCompliance.TASK_MKEY < 1)
                        //    {
                        //        var errorResult = new List<TaskCheckListOutputList>
                        //        {
                        //            new TaskCheckListOutputList
                        //            {
                        //                STATUS = "Error",
                        //                MESSAGE = "Data not found",
                        //                DATA = null
                        //            }
                        //        };
                        //        return errorResult;
                        //    }
                        //}

                        var successsResult = new List<TaskCheckListOutputList>
                                {
                                    new TaskCheckListOutputList
                                    {
                                        STATUS = "Ok",
                                        MESSAGE = "Get data successfully!!!",
                                        DATA = GetTaskEnd
                                    }
                                };
                        return successsResult;
                    }
                    else
                    {
                        var errorResult = new List<TaskCheckListOutputList>
                                {
                                    new TaskCheckListOutputList
                                    {
                                        STATUS = "Error",
                                        MESSAGE = "Data not found",
                                        DATA = null
                                    }
                                };
                        return errorResult;
                    }
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
                var errorResult = new List<TaskCheckListOutputList>
                {
                    new TaskCheckListOutputList
                    {
                        STATUS = "Error",
                        MESSAGE = $"SQL Error: {sqlEx.Message}",
                        DATA = null
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
                var errorResult = new List<TaskCheckListOutputList>
                        {
                            new TaskCheckListOutputList
                            {
                                STATUS = "Error",
                                MESSAGE = ex.Message,
                                DATA = null
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
        public async Task<ActionResult<IEnumerable<TASK_COMPLIANCE_END_CHECK_LIST>>> PostTaskEndListTableInsertUpdateAsync(TASK_ENDLIST_TABLE_INPUT tASK_ENDLIST_TABLE_INPUT)
        {
            DateTime dateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
            IDbTransaction transaction = null;
            int ProjectDAttach = 0;
            bool transactionCompleted = false;
            bool flagInsert = false;
            string OutStatus = string.Empty;
            string OutMessage = string.Empty;
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
                        await sqlConnection.OpenAsync();
                    }

                    // Start a new transaction
                    transaction = db.BeginTransaction();
                    transactionCompleted = false;

                    // Prepare the parameters for the stored procedure

                    foreach (var docMkey in tASK_ENDLIST_TABLE_INPUT.OUTPUT_DOC_LST)
                    {
                        foreach (var DocCategory in docMkey.Value.ToString().Split(','))
                        {
                            var parameters = new DynamicParameters();
                            parameters.Add("@MKEY", tASK_ENDLIST_TABLE_INPUT.MKEY);
                            parameters.Add("@SR_NO", tASK_ENDLIST_TABLE_INPUT.SR_NO);
                            parameters.Add("@DOCUMENT_CATEGORY_MKEY", Convert.ToInt32(DocCategory));
                            parameters.Add("@DOCUMENT_NAME", docMkey.Key.ToString());
                            //parameters.Add("@DOC_NUM_APP_FLAG", tASK_ENDLIST_TABLE_INPUT.DOC_NUM_DATE_APP_FLAG);
                            //parameters.Add("@DOC_NUM_VALID_FLAG", tASK_ENDLIST_TABLE_INPUT.DOC_NUM_VALID_FLAG);
                            //parameters.Add("@DOC_NUM_DATE_APP_FLAG", tASK_ENDLIST_TABLE_INPUT.DOC_NUM_DATE_APP_FLAG);
                            //parameters.Add("@DOC_ATTACH_APP_FLAG", tASK_ENDLIST_TABLE_INPUT.DOC_ATTACH_APP_FLAG);
                            //parameters.Add("@DOC_NUMBER", tASK_ENDLIST_TABLE_INPUT.DOC_NUMBER);
                            //parameters.Add("@DOC_DATE", tASK_ENDLIST_TABLE_INPUT.DOC_DATE);
                            //parameters.Add("@VALIDITY_DATE", tASK_ENDLIST_TABLE_INPUT.VALIDITY_DATE);
                            parameters.Add("@CREATED_BY", tASK_ENDLIST_TABLE_INPUT.CREATED_BY);
                            parameters.Add("@DELETE_FLAG", tASK_ENDLIST_TABLE_INPUT.DELETE_FLAG);
                            parameters.Add("@API_NAME", "Task-Output-Doc-Insert-Update");
                            parameters.Add("@API_METHOD", "Insert/Update");
                            parameters.Add("@OUT_STATUS", null);
                            parameters.Add("@OUT_MESSAGE", null);

                            var GetTaskEnd = await db.QueryAsync<TASK_ENDLIST_DETAILS_OUTPUT>("SP_INSERT_UPDATE_TASK_ENDLIST_TABLE", parameters, commandType: CommandType.StoredProcedure, transaction: transaction);
                            if (GetTaskEnd.Any())
                            {
                                foreach(var ErrorRespo in GetTaskEnd)
                                {
                                    if (ErrorRespo.OUT_STATUS != "Ok")
                                    {
                                        if (transaction != null && !transactionCompleted)
                                        {
                                            transaction.Rollback();
                                        }

                                        var errorResult = new List<TASK_COMPLIANCE_END_CHECK_LIST>
                                        {
                                            new TASK_COMPLIANCE_END_CHECK_LIST
                                            {
                                                STATUS = "Error",
                                                MESSAGE = ErrorRespo.OUT_MESSAGE,
                                                DATA = null
                                            }
                                        };
                                        return errorResult;
                                    }
                                }
                                flagInsert = false;
                                //break;
                            }
                            if (flagInsert == true)
                            {
                                foreach (var ErrorMsg in GetTaskEnd)
                                {
                                    OutStatus = ErrorMsg.OUT_STATUS;
                                    OutMessage = ErrorMsg.OUT_MESSAGE;
                                }
                                break;
                            }
                        }
                    }

                    if (flagInsert == false)
                    {
                        var parmeters = new DynamicParameters();
                        parmeters.Add("@PROPERTY_MKEY", null);
                        parmeters.Add("@BUILDING_MKEY", null);
                        parmeters.Add("@TASK_MKEY", tASK_ENDLIST_TABLE_INPUT.MKEY);
                        parmeters.Add("@USER_ID", tASK_ENDLIST_TABLE_INPUT.CREATED_BY);
                        parmeters.Add("@API_NAME", "GetTaskEndList");
                        parmeters.Add("@API_METHOD", "Get");
                        var GetTaskEnd = await db.QueryAsync<TASK_COMPLIANCE_CHECK_END_LIST_OUTPUT>("SP_GET_TASK_ENDLIST", parmeters, commandType: CommandType.StoredProcedure, transaction: transaction);

                        if (GetTaskEnd.Any())
                        {
                            foreach (var TaskCompliance in GetTaskEnd)
                            {
                                if (TaskCompliance.MKEY < 1)
                                {
                                    var errorResult = new List<TASK_COMPLIANCE_END_CHECK_LIST>
                                    {
                                        new TASK_COMPLIANCE_END_CHECK_LIST
                                        {
                                            STATUS = "Error",
                                            MESSAGE = "Data not found",
                                            DATA = null
                                        }
                                    };
                                    return errorResult;
                                }

                                var parmetersMedia = new DynamicParameters();
                                parmetersMedia.Add("@TASK_MKEY", tASK_ENDLIST_TABLE_INPUT.MKEY);
                                parmetersMedia.Add("@DOC_CATEGORY_MKEY", TaskCompliance.DOC_MKEY);
                                parmetersMedia.Add("@USER_ID", tASK_ENDLIST_TABLE_INPUT.CREATED_BY);
                                var TaskEndListMedia = await db.QueryAsync<TASK_OUTPUT_MEDIA>("SP_GET_TASK_ENDLIST_MEDIA", parmetersMedia, commandType: CommandType.StoredProcedure, transaction: transaction);

                                TaskCompliance.TASK_OUTPUT_ATTACHMENT = TaskEndListMedia.ToList();
                            }

                            var sqlTransaction = (SqlTransaction)transaction;
                            await sqlTransaction.CommitAsync();
                            transactionCompleted = true;

                            var successsResult = new List<TASK_COMPLIANCE_END_CHECK_LIST>
                            {
                            new TASK_COMPLIANCE_END_CHECK_LIST
                                {
                                STATUS = "Ok",
                                MESSAGE = "Get data successfully!!!",
                                DATA= GetTaskEnd
                                }
                        };
                            return successsResult;
                        }
                        else
                        {
                            var errorResult = new List<TASK_COMPLIANCE_END_CHECK_LIST>
                                {
                                    new TASK_COMPLIANCE_END_CHECK_LIST
                                    {
                                        STATUS = "Error",
                                        MESSAGE = "Data not found",
                                        DATA = null
                                    }
                                };
                            return errorResult;
                        }


                        //var successsResult = new List<TASK_COMPLIANCE_END_CHECK_LIST>
                        //    {
                        //        new TASK_COMPLIANCE_END_CHECK_LIST
                        //        {
                        //            STATUS = "Ok",
                        //            MESSAGE = "Get data successfully!!!",
                        //            DATA = GetTaskEnd
                        //        }
                        //    };
                        //return successsResult;
                    }
                    else
                    {
                        if (transaction != null && !transactionCompleted)
                        {
                            transaction.Rollback();
                        }

                        var errorResult = new List<TASK_COMPLIANCE_END_CHECK_LIST>
                        {
                            new TASK_COMPLIANCE_END_CHECK_LIST
                            {
                                STATUS = "Error",
                                MESSAGE = "An Error occurd",
                                DATA = null
                            }
                        };
                        return errorResult;
                    }
                }
            }
            catch (Exception ex)
            {
                // If an error occurs, rollback the transaction
                if (transaction != null && !transactionCompleted)
                {
                    transaction.Rollback();
                }

                // Return error response
                var errorResult = new List<TASK_COMPLIANCE_END_CHECK_LIST>
                {
                    new TASK_COMPLIANCE_END_CHECK_LIST
                    {
                        STATUS = "Error",
                        MESSAGE = ex.Message,
                        DATA = null
                    }
                };
                return errorResult;
            }
        }
        public async Task<ActionResult<IEnumerable<TaskSanctioningDepartmentOutputList>>> PostTaskSanctioningTableInsertUpdateAsync(TASK_SANCTIONING_INPUT tASK_SANCTIONING_INPUT)
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
                    parmeters.Add("@TASK_MKEY", tASK_SANCTIONING_INPUT.MKEY);
                    parmeters.Add("@SR_NO", tASK_SANCTIONING_INPUT.SR_NO);
                    parmeters.Add("@LEVEL", tASK_SANCTIONING_INPUT.LEVEL);
                    parmeters.Add("@SANCTIONING_DEPARTMENT", tASK_SANCTIONING_INPUT.SANCTIONING_DEPARTMENT);
                    parmeters.Add("@SANCTIONING_AUTHORITY_MKEY", tASK_SANCTIONING_INPUT.SANCTIONING_AUTHORITY_MKEY);
                    parmeters.Add("@CREATED_BY", tASK_SANCTIONING_INPUT.CREATED_BY);
                    parmeters.Add("@DELETE_FLAG", tASK_SANCTIONING_INPUT.DELETE_FLAG);
                    parmeters.Add("@METHOD_NAME", "Task-Sanctioning-Table-Insert-Update");
                    parmeters.Add("@METHOD", "Insert/Update");
                    parmeters.Add("@OUT_STATUS", null);
                    parmeters.Add("@OUT_MESSAGE", null);

                    var GetTaskEnd = await db.QueryAsync<TaskSanctioningDepartmentOutput>("SP_INSERT_UPDATE_TABLE_SANCTIONING_DEPARTMENT", parmeters, commandType: CommandType.StoredProcedure, transaction: transaction);

                    if (GetTaskEnd.Any())
                    {
                        foreach (var ResponseOk in GetTaskEnd)
                        {
                            if (ResponseOk.OUT_STATUS != "OK")
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
                                        Console.WriteLine($"Rollback failed: {rollbackEx.Message}");
                                    }
                                }

                                // Log the SQL error
                                var errorResult = new List<TaskSanctioningDepartmentOutputList>
                                    {
                                        new TaskSanctioningDepartmentOutputList
                                        {
                                            STATUS = "Error",
                                            MESSAGE = ResponseOk.OUT_MESSAGE,
                                            DATA = null
                                        }
                                    };
                                return errorResult;
                            }
                        }
                    }
                    var sqlTransaction = (SqlTransaction)transaction;
                    await sqlTransaction.CommitAsync();
                    transactionCompleted = true;

                    var SuccessResult = new List<TaskSanctioningDepartmentOutputList>
                    {
                        new TaskSanctioningDepartmentOutputList
                        {
                            STATUS = "OK",
                            MESSAGE = "New record created successfully",
                            DATA = GetTaskEnd
                        }
                    };
                    return SuccessResult;
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
                var errorResult = new List<TaskSanctioningDepartmentOutputList>
                {
                    new TaskSanctioningDepartmentOutputList
                    {
                        STATUS = "Error",
                        MESSAGE = $"SQL Error: {sqlEx.Message}",
                        DATA = null
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
                var errorResult = new List<TaskSanctioningDepartmentOutputList>
                {
                    new TaskSanctioningDepartmentOutputList
                    {
                        STATUS = "Error",
                        MESSAGE = ex.Message,
                        DATA = null
                    }
                };
                return errorResult;
            }
        }
    }
}

