using System.Data;
using Dapper;
using System.Reflection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.API.Interfaces;
using TaskManagement.API.Model;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Microsoft.AspNetCore.Authorization;
using System;

namespace TaskManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProjectEmployeeController : ControllerBase
    {
        private readonly IProjectEmployee _repository;
        public IDapperDbConnection _dapperDbConnection;
        public ProjectEmployeeController(IProjectEmployee repository)
        {
            _repository = repository;
        }

        [HttpGet("Task-Management/Login")]
        public async Task<ActionResult<EmployeeCompanyMST>> Login_Validate(EmployeeCompanyMST employeeCompanyMST)
        {
            try
            {
                var LoginValidate = await _repository.Login_Validate(employeeCompanyMST.Login_ID, employeeCompanyMST.LOGIN_PASSWORD);
                if (LoginValidate == null)
                {
                    var responseApprovalTemplate = new ApiResponse<EmployeeCompanyMST>
                    {
                        Status = "Error",
                        Message = "Error Occurd",
                        Data = null
                    };
                    return Ok(responseApprovalTemplate);
                }
                else if (LoginValidate.STATUS != "Ok")
                {
                    var responseApprovalTemplate = new ApiResponse<EmployeeCompanyMST>
                    {
                        Status = LoginValidate.STATUS,
                        Message = LoginValidate.MESSAGE,
                        Data = null
                    };

                    return Ok(responseApprovalTemplate);
                }

                var response = new ApiResponse<EmployeeCompanyMST>
                {
                    Status = "OK",
                    Message = "Login details",
                    Data = LoginValidate
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<EmployeeCompanyMST>
                {
                    Status = "Error",
                    Message = ex.Message,
                    Data = null
                };
                return Ok(response);
            }
        }

        [HttpGet("Task-Management/Get-Option")]
        [Authorize]
        public async Task<ActionResult<ApiResponse<V_Building_Classification>>> Get_Project(V_Building_Classification v_Building_Classification)
        {
            try
            {
                var classifications = await _repository.GetProjectAsync(v_Building_Classification.TYPE_CODE, v_Building_Classification.MASTER_MKEY);
                if (classifications == null)
                {
                    var responseApprovalTemplate = new ApiResponse<V_Building_Classification>
                    {
                        Status = "Error",
                        Message = "Error Occurd",
                        Data = null
                    };
                    return Ok(responseApprovalTemplate);
                }

                return Ok(classifications);
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<V_Building_Classification>
                {
                    Status = "Error",
                    Message = ex.Message,
                    Data = null
                };
                return Ok(response);
            }
        }

        [HttpGet("Task - Management / Get - Sub_Project")]
        [Authorize]
        public async Task<ActionResult<ApiResponse<V_Building_Classification>>> Get_Sub_Project(EmployeeCompanyMST employeeCompanyMST)
        {
            try
            {
                var classifications = await _repository.GetSubProjectAsync(employeeCompanyMST.PROJECT_ID);
                if (classifications == null)
                {
                    var ErrorResponse = new ApiResponse<V_Building_Classification>
                    {
                        Status = "Error",
                        Message = "Error Occurd",
                        Data = null
                    };
                    return Ok(ErrorResponse);
                }

                return Ok(classifications);
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<V_Building_Classification>
                {
                    Status = "Error",
                    Message = ex.Message,
                    Data = null
                };
                return Ok(response);
            }
        }

        [HttpGet("Task-Management/Get-Emp")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<EmployeeCompanyMST>>> Get_Emp(EmployeeCompanyMST employeeCompanyMST)
        {
            try
            {
                var classifications = await _repository.GetEmpAsync(employeeCompanyMST.CURRENT_EMP_MKEY, employeeCompanyMST.FILTER);
                if (classifications == null)
                {
                    var responseApprovalTemplate = new ApiResponse<V_Building_Classification>
                    {
                        Status = "Error",
                        Message = "Error Occurd",
                        Data = null
                    };
                    return Ok(responseApprovalTemplate);
                }

                return Ok(classifications);
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<V_Building_Classification>
                {
                    Status = "Error",
                    Message = ex.Message,
                    Data = null
                };
                return Ok(response);
            }
        }

        [HttpGet("Task-Management/Assigned_To")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<EmployeeCompanyMST>>> AssignedTo(EmployeeCompanyMST employeeCompanyMST)
        {
            try
            {
                var classifications = await _repository.GetAssignedToAsync(employeeCompanyMST.AssignNameLike);
                if (classifications == null)
                {
                    var responseApprovalTemplate = new ApiResponse<EmployeeCompanyMST>
                    {
                        Status = "Error",
                        Message = "Error Occurd",
                        Data = null
                    };
                    return Ok(responseApprovalTemplate);
                }

                return Ok(classifications);
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<EmployeeCompanyMST>
                {
                    Status = "Error",
                    Message = ex.Message,
                    Data = null
                };
                return Ok(response);
            }
        }

        [HttpGet("Task-Management/EMP_TAGS")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<EMPLOYEE_TAGS>>> EMP_TAGS(string EMP_TAGS)
        {
            try
            {
                var classifications = await _repository.GetEmpTagsAsync(EMP_TAGS);
                if (classifications == null)
                {
                    var responseApprovalTemplate = new ApiResponse<EmployeeCompanyMST>
                    {
                        Status = "Error",
                        Message = "Error Occurd",
                        Data = null
                    };
                    return Ok(responseApprovalTemplate);
                }
                return Ok(classifications);
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<EmployeeCompanyMST>
                {
                    Status = "Error",
                    Message = ex.Message,
                    Data = null
                };
                return Ok(response);
            }
        }


        [HttpGet("Task-Management/TASK-DASHBOARD")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<TASK_DASHBOARD>>> Task_Details(EmployeeCompanyMST employeeCompanyMST)
        {
            try
            {
                var TaskDash = await _repository.GetTaskDetailsAsync(employeeCompanyMST.CURRENT_EMP_MKEY, employeeCompanyMST.FILTER);
                if (TaskDash == null)
                {
                    var responseApprovalTemplate = new ApiResponse<TASK_DASHBOARD>
                    {
                        Status = "Error",
                        Message = "Error Occurd",
                        Data = null
                    };
                    return Ok(responseApprovalTemplate);
                }
                return Ok(TaskDash);
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<TASK_DASHBOARD>
                {
                    Status = "Error",
                    Message = ex.Message,
                    Data = null
                };
                return Ok(response);
            }
        }


        [HttpGet("Task-Management/TASK-DETAILS_BY_MKEY")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<TASK_DETAILS_BY_MKEY>>> TASK_DETAILS_BY_MKEY(TASK_DETAILS_BY_MKEY tASK_DETAILS_BY_MKEY)
        {
            try
            {
                var TaskDash = await _repository.GetTaskDetailsByMkeyAsync(tASK_DETAILS_BY_MKEY.MKEY);
                if (TaskDash == null)
                {
                    var responseApprovalTemplate = new ApiResponse<TASK_DASHBOARD>
                    {
                        Status = "Error",
                        Message = "Error Occurd",
                        Data = null
                    };
                    return Ok(responseApprovalTemplate);
                }
                return Ok(TaskDash);
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<TASK_DASHBOARD>
                {
                    Status = "Error",
                    Message = ex.Message,
                    Data = null
                };
                return Ok(response);
            }
        }

        [HttpGet("Task-Management/TASK-NESTED-GRID")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<TASK_DASHBOARD>>> TASK_NESTED_GRID(TASK_DASHBOARD tASK_DASHBOARD)
        {
            try
            {
                var TaskDash = await _repository.GetTaskNestedGridAsync(tASK_DASHBOARD.MKEY);
                if (TaskDash == null)
                {
                    var responseTaskTree = new ApiResponse<TASK_DASHBOARD>
                    {
                        Status = "Error",
                        Message = "Error Occurd",
                        Data = null
                    };
                    return Ok(responseTaskTree);
                }
                return Ok(TaskDash);
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<TASK_DASHBOARD>
                {
                    Status = "Error",
                    Message = ex.Message,
                    Data = null
                };
                return Ok(response);
            }
        }

        [HttpGet("Task-Management/GET-ACTIONS")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<TASK_ACTION_TRL>>> GET_ACTIONS(TASK_ACTION_TRL tASK_ACTION_TRL)
        {
            try
            {
                var TaskAction = await _repository.GetActionsAsync(Convert.ToInt32(tASK_ACTION_TRL.TASK_MKEY), Convert.ToInt32(tASK_ACTION_TRL.CURRENT_EMP_MKEY), tASK_ACTION_TRL.CURR_ACTION);
                if (TaskAction == null)
                {
                    var responseTaskAction = new ApiResponse<TASK_ACTION_TRL>
                    {
                        Status = "Error",
                        Message = "Error Occurd",
                        Data = tASK_ACTION_TRL
                    };
                    return Ok(responseTaskAction);
                }
                return Ok(TaskAction);
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<TASK_ACTION_TRL>
                {
                    Status = "Error",
                    Message = ex.Message,
                    Data = tASK_ACTION_TRL
                };
                return Ok(response);
            }
        }

        [HttpGet("Task-Management/GET-TASK_TREE")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<TASK_DASHBOARD>>> GET_TASK_TREE(TASK_DASHBOARD tASK_DASHBOARD)
        {
            try
            {
                var TaskTree = await _repository.GetTaskTreeAsync(tASK_DASHBOARD.TASK_MKEY);
                if (TaskTree == null)
                {
                    var responseTaskAction = new ApiResponse<TASK_DASHBOARD>
                    {
                        Status = "Error",
                        Message = "Error Occurd",
                        Data = null
                    };
                    return Ok(responseTaskAction);
                }
                return Ok(TaskTree);
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<TASK_DASHBOARD>
                {
                    Status = "Error",
                    Message = ex.Message,
                    Data = tASK_DASHBOARD
                };
                return Ok(response);
            }
        }

        [HttpPut("Task-Management/Change_Password")]
        [Authorize]
        public async Task<ActionResult<EmployeeCompanyMST>> ChangePassword(EmployeeCompanyMST employeeCompanyMST)
        {
            try
            {
                var ChangePass = await _repository.PutChangePasswordAsync(employeeCompanyMST.LoginName, employeeCompanyMST.Old_Password, employeeCompanyMST.New_Password);

                if (ChangePass == null)
                {
                    var responseTaskAction = new ApiResponse<EmployeeCompanyMST>
                    {
                        Status = "Error",
                        Message = "Error Occurd",
                        Data = employeeCompanyMST
                    };
                    return Ok(responseTaskAction);
                }
                return Ok(ChangePass);
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<EmployeeCompanyMST>
                {
                    Status = "Error",
                    Message = ex.Message,
                    Data = null
                };
                return Ok(response);
            }
        }

        [HttpGet("Task-Management/Forgot_Password")]
        [Authorize]
        public async Task<ActionResult<EmployeeCompanyMST>> ForgotPassword(EmployeeCompanyMST employeeCompanyMST)
        {
            try
            {
                var ForgotPass = await _repository.GetForgotPasswordAsync(employeeCompanyMST.LoginName);

                if (ForgotPass == null)
                {
                    var responseTaskAction = new ApiResponse<EmployeeCompanyMST>
                    {
                        Status = "Error",
                        Message = "Error Occurd",
                        Data = employeeCompanyMST
                    };
                    return Ok(responseTaskAction);
                }


                var ResetPass = await _repository.GetResetPasswordAsync(ForgotPass.TEMPPASSWORD, employeeCompanyMST.LoginName);

                if (ResetPass == null)
                {
                    var responseTaskAction = new ApiResponse<EmployeeCompanyMST>
                    {
                        Status = "Error",
                        Message = "Error Occurd",
                        Data = employeeCompanyMST
                    };
                    return Ok(responseTaskAction);
                }

                return Ok(ResetPass);
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<EmployeeCompanyMST>
                {
                    Status = "Error",
                    Message = ex.Message,
                    Data = null
                };
                return Ok(response);
            }
        }

        public static string GetMailBody(string User_Name, string Temp_Password, string SiteURL)
        {
            string messageBody = "<html>";
            messageBody += "<body>";
            messageBody += "<table>";
            messageBody += "<tr><td>Dear <strong>" + User_Name + ",</strong></td></tr>";
            messageBody += "<tr><td>The password for your Task Management account has been reset,your temporary password is <strong>" + Temp_Password + "</strong></td></tr>";
            messageBody += "<tr><td>Please click on <a href=" + SiteURL + ">Login</a> to reset your pasword</td></tr>";
            messageBody += "<tr><td></td></tr><tr><td></td></tr><tr><td></td></tr><tr><td>Thank  you</td></tr><tr><td>The Powersoft Team</td></tr>";
            messageBody += "</table>";
            messageBody += "</body></html>";
            return messageBody;
        }

        [HttpGet("Task-Management/Validate_Email")]
        [Authorize]
        public async Task<ActionResult<EmployeeCompanyMST>> ValidateEmail(EmployeeCompanyMST employeeCompanyMST)
        {
            try
            {
                var ValidateEmailVar = await _repository.GetValidateEmailAsync(employeeCompanyMST.Login_ID);

                if (ValidateEmailVar == null)
                {
                    var responseTaskAction = new ApiResponse<EmployeeCompanyMST>
                    {
                        Status = "Error",
                        Message = "Error Occurd",
                        Data = employeeCompanyMST
                    };
                    return Ok(responseTaskAction);
                }
                return Ok(ValidateEmailVar);
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<EmployeeCompanyMST>
                {
                    Status = "Error",
                    Message = ex.Message,
                    Data = null
                };
                return Ok(response);
            }
        }

        [HttpGet("Task-Management/TASK-DASHBOARD_DETAILS")]
        [Authorize]
        public async Task<ActionResult<TASK_DASHBOARD>> Task_Dashboard_Details(EmployeeCompanyMST employeeCompanyMST)
        {
            try
            {
                var TaskDashboardDetails = await _repository.GetTaskDashboardDetailsAsync(Convert.ToString(employeeCompanyMST.CURRENT_EMP_MKEY), employeeCompanyMST.CURR_ACTION);

                if (TaskDashboardDetails == null)
                {
                    var responseTaskAction = new ApiResponse<TASK_DASHBOARD>
                    {
                        Status = "Error",
                        Message = "Error Occurd",
                        Data = null
                    };
                    return Ok(responseTaskAction);
                }
                return Ok(TaskDashboardDetails);
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<TASK_DASHBOARD>
                {
                    Status = "Error",
                    Message = ex.Message,
                    Data = null
                };
                return Ok(response);
            }
        }





        [HttpGet("Task-Management/Add-Task")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<TASK_HDR>>> Add_Task([FromBody] TASK_HDR tASK_HDR)
        {
            try
            {
                if (tASK_HDR.TASK_NO == "0000")
                {
                    using (IDbConnection db = _dapperDbConnection.CreateConnection())
                    {
                        var parmeters = new DynamicParameters();
                        parmeters.Add("@TASK_NO", tASK_HDR.TASK_NO);
                        parmeters.Add("@TASK_NAME", tASK_HDR.TASK_NAME);
                        parmeters.Add("@TASK_DESCRIPTION", tASK_HDR.TASK_DESCRIPTION);
                        parmeters.Add("@CATEGORY", tASK_HDR.CAREGORY);
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
                        parmeters.Add("@CREATION_DATE", tASK_HDR.CREATED_DATE);
                        parmeters.Add("@LAST_UPDATED_BY", tASK_HDR.LAST_UPDATED_BY);
                        parmeters.Add("@APPROVE_ACTION_DATE", tASK_HDR.APPROVE_ACTION_DATE);
                        var ewmployeeMkey = db.Query("Sp_insert_task_details", parmeters, commandType: CommandType.StoredProcedure).ToList();
                        return Ok(ewmployeeMkey);
                    }
                }
                else
                {
                    using (IDbConnection db = _dapperDbConnection.CreateConnection())
                    {
                        var parmeters = new DynamicParameters();
                        parmeters.Add("@EMP_MKEY", EMP_TAGS);
                        var ewmployeeMkey = db.Query("sp_EMP_TAGS", parmeters, commandType: CommandType.StoredProcedure).ToList();
                        return Ok(ewmployeeMkey);
                    }
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }


        [HttpGet("Task-Management/Add-Sub-Task")]
        [Authorize]
        public ActionResult<IEnumerable<dynamic>> Add_Sub_Task(string TASK_NO, string TASK_NAME, string TASK_DESCRIPTION, string CATEGORY, string PROJECT_ID, string SUBPROJECT_ID, string COMPLETION_DATE, string ASSIGNED_TO, string TAGS, string ISNODE, string START_DATE, string CLOSE_DATE, string DUE_DATE, string TASK_PARENT_ID, string TASK_PARENT_NODE_ID, string TASK_PARENT_NUMBER, string STATUS, string STATUS_PERC, string TASK_CREATED_BY, string APPROVER_ID, string IS_ARCHIVE, string ATTRIBUTE1, string ATTRIBUTE2, string ATTRIBUTE3, string ATTRIBUTE4, string ATTRIBUTE5, string CREATED_BY, string CREATION_DATE, string LAST_UPDATED_BY, string APPROVE_ACTION_DATE, string Current_task_mkey)
        {
            try
            {
                if (TASK_NO == "0000")
                {
                    using (IDbConnection db = _dapperDbConnection.CreateConnection())
                    {
                        var parmeters = new DynamicParameters();
                        parmeters.Add("@TASK_NO", TASK_NO);
                        parmeters.Add("@TASK_NAME", TASK_NAME);
                        parmeters.Add("@TASK_DESCRIPTION", TASK_DESCRIPTION);
                        parmeters.Add("@CATEGORY", CATEGORY);
                        parmeters.Add("@PROJECT_ID", PROJECT_ID);
                        parmeters.Add("@SUBPROJECT_ID", SUBPROJECT_ID);
                        parmeters.Add("@COMPLETION_DATE", COMPLETION_DATE);
                        parmeters.Add("@ASSIGNED_TO", ASSIGNED_TO);
                        parmeters.Add("@TAGS", TAGS);
                        parmeters.Add("@ISNODE", ISNODE);
                        parmeters.Add("@CLOSE_DATE", CLOSE_DATE);
                        parmeters.Add("@DUE_DATE", DUE_DATE);
                        parmeters.Add("@TASK_PARENT_ID", TASK_PARENT_ID);
                        parmeters.Add("@STATUS", STATUS);
                        parmeters.Add("@STATUS_PERC", STATUS_PERC);
                        parmeters.Add("@TASK_CREATED_BY", TASK_CREATED_BY);
                        parmeters.Add("@APPROVER_ID", APPROVER_ID);
                        parmeters.Add("@IS_ARCHIVE", IS_ARCHIVE);
                        parmeters.Add("@ATTRIBUTE1", ATTRIBUTE1);
                        parmeters.Add("@ATTRIBUTE2", ATTRIBUTE2);
                        parmeters.Add("@ATTRIBUTE3", ATTRIBUTE3);
                        parmeters.Add("@ATTRIBUTE4", ATTRIBUTE4);
                        parmeters.Add("@ATTRIBUTE5", ATTRIBUTE5);
                        parmeters.Add("@CREATED_BY", CREATED_BY);
                        parmeters.Add("@CREATION_DATE", CREATION_DATE);
                        parmeters.Add("@LAST_UPDATED_BY", LAST_UPDATED_BY);
                        parmeters.Add("@APPROVE_ACTION_DATE", APPROVE_ACTION_DATE);
                        var ewmployeeMkey = db.Query("Sp_insert_task_details", parmeters, commandType: CommandType.StoredProcedure).ToList();
                        return Ok(ewmployeeMkey);
                    }
                }
                else
                {
                    using (IDbConnection db = _dapperDbConnection.CreateConnection())
                    {
                        var parmeters = new DynamicParameters();
                        parmeters.Add("@EMP_MKEY", EMP_TAGS);
                        var ewmployeeMkey = db.Query("sp_EMP_TAGS", parmeters, commandType: CommandType.StoredProcedure).ToList();
                        return Ok(ewmployeeMkey);
                    }
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
    }
}
