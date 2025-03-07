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
using OfficeOpenXml;
using TaskManagement.API.Repositories;
using Newtonsoft.Json;
using Azure;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static System.Runtime.InteropServices.JavaScript.JSType;
using FastMember;
using Microsoft.Extensions.Options;
using System.Collections;

namespace TaskManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class CommonApiController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IProjectEmployee _repository;
        public static IWebHostEnvironment _environment;
        private readonly FileSettings _fileSettings;

        public IDapperDbConnection _dapperDbConnection;
        public CommonApiController(IProjectEmployee repository, IConfiguration configuration, IWebHostEnvironment environment, IOptions<FileSettings> fileSettings)
        {
            _repository = repository;
            _configuration = configuration;
            _environment = environment;
            _fileSettings = fileSettings.Value;
        }

        [HttpPost("Task-Management/Login")]
        public async Task<ActionResult<EmployeeLoginOutput_LIST>> Login_Validate([FromBody] EmployeeCompanyMSTInput employeeCompanyMSTInput)
        {
            try
            {
                var LoginValidate = await _repository.Login_Validate(employeeCompanyMSTInput.Login_ID, employeeCompanyMSTInput.Login_Password);
                return Ok(LoginValidate);
            }
            catch (Exception ex)
            {
                var response = new EmployeeLoginOutput_LIST
                {
                    Status = "Error",
                    Message = ex.Message,
                    Data = null
                };
                return Ok(response);
            }
        }
        //[HttpPost("Task-Management/Login_NT")]
        //public async Task<ActionResult<EmployeeLoginOutput_LIST_NT>> Login_Validate_NT([FromBody] EmployeeCompanyMSTInput_NT employeeCompanyMSTInput_NT)
        //{
        //    try
        //    {
        //        var LoginValidate = await _repository.Login_Validate_NT(employeeCompanyMSTInput_NT);
        //        return Ok(LoginValidate);
        //    }
        //    catch (Exception ex)
        //    {
        //        var response = new EmployeeLoginOutput_LIST
        //        {
        //            Status = "Error",
        //            Message = ex.Message,
        //            Data = null
        //        };
        //        return Ok(response);
        //    }
        //}

        [HttpPost("Task-Management/Get-Option")]
        [Authorize]
        public async Task<ActionResult<V_Building_Classification_new>> Get_Project([FromBody] BuildingClassInput v_Building_Classification)
        {
            try
            {
                // Get the project classifications (a collection)
                var classifications = await _repository.GetProjectAsync(v_Building_Classification.Type_Code, v_Building_Classification.Master_mkey);

                //if (classifications == null || !classifications.Any()) // Check if the result is null or empty
                //{
                //    //var responseApprovalTemplate = new ApiResponse<IEnumerable<V_Building_Classification_new>>
                //    //{
                //    //    Status = "Error",
                //    //    Message = "Error Occurred or No Data Found",
                //    //    Data = classifications
                //    //};
                //    return Ok(classifications);
                //}

                //var responseProject = new ApiResponse<IEnumerable<V_Building_Classification_new>>
                //{
                //    Status = "Ok",
                //    Message = "Data Retrieved Successfully",
                //    Data = classifications
                //};
                return Ok(classifications);
            }
            catch (Exception ex)
            {
                var response = new V_Building_Classification_new
                {
                    Status = "Error",
                    Message = ex.Message,
                    Data = null
                };
                return Ok(response);
            }
        }

        [HttpPost("Task-Management/Get-Option_NT")]
        [Authorize]
        public async Task<ActionResult<V_Building_Classification_NT>> Get_Project_NT([FromBody] BuildingClassInput_NT v_Building_Classification)
        {
            try
            {
                // Get the project classifications (a collection)
                var classifications = await _repository.GetProjectNTAsync(v_Building_Classification);
              
                return Ok(classifications);
            }
            catch (Exception ex)
            {
                var response = new V_Building_Classification_NT
                {
                    Status = "Error",
                    Message = ex.Message,
                    Data = null
                };
                return Ok(response);
            }
        }
        [HttpPost("Task-Management/Get-Sub_Project")]
        [Authorize]
        public async Task<ActionResult<V_Building_Classification_new>> Get_Sub_Project([FromBody] GetSubProjectInput getSubProjectInput)
        {
            try
            {
                var classifications = await _repository.GetSubProjectAsync(getSubProjectInput.Project_Mkey);
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
                var response = new V_Building_Classification_new
                {
                    Status = "Error",
                    Message = ex.Message,
                    Data = null
                };
                return Ok(response);
            }
        }

        [HttpPost("Task-Management/Get-Sub_Project_NT")]
        [Authorize]
        public async Task<ActionResult<V_Building_Classification_New_NT>> Get_Sub_Project_NT([FromBody] GetSubProjectInput_NT getSubProjectInput_NT)
        {
            try
            {
                var classifications = await _repository.GetSubProjectNTAsync(getSubProjectInput_NT);
                if (classifications == null)
                {
                    var ErrorResponse = new ApiResponse<V_Building_Classification_New_NT>
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
                var response = new V_Building_Classification_New_NT
                {
                    Status = "Error",
                    Message = ex.Message,
                    Data = null
                };
                return Ok(response);
            }
        }
        [HttpPost("Task-Management/Get-Emp")]
        [Authorize]
        public async Task<ActionResult<EmployeeLoginOutput_LIST>> Get_Emp([FromBody] Get_EmpInput get_EmpInput)
        {
            try
            {
                var classifications = await _repository.GetEmpAsync(get_EmpInput.CURRENT_EMP_MKEY, get_EmpInput.FILTER);
                //if (classifications == null)
                //{
                //    var responseApprovalTemplate = new ApiResponse<V_Building_Classification>
                //    {
                //        Status = "Error",
                //        Message = "Error Occurd",
                //        Data = null
                //    };
                //    return Ok(classifications);
                //}

                return Ok(classifications);
            }
            catch (Exception ex)
            {
                var response = new EmployeeLoginOutput_LIST
                {
                    Status = "Error",
                    Message = ex.Message,
                    Data = null
                };
                return Ok(response);
            }
        }
        [HttpPost("Task-Management/Get-Emp_NT")]
        [Authorize]
        public async Task<ActionResult<EmployeeLoginOutput_LIST_Session_NT>> Get_Emp_NT([FromBody] Get_EmpInput_NT get_EmpInput_NT)
        {
            try
            {
                var classifications = await _repository.GetEmpNTAsync(get_EmpInput_NT);
                return Ok(classifications);
            }
            catch (Exception ex)
            {
                var response = new EmployeeLoginOutput_LIST_Session_NT
                {
                    Status = "Error",
                    Message = ex.Message,
                    Data = null
                };
                return Ok(response);
            }
        }
        [HttpPost("Task-Management/Assigned_To")]
        [Authorize]
        public async Task<ActionResult<EmployeeCompanyMST>> AssignedTo([FromBody] AssignedToInput assignedToInput)
        {
            try
            {
                var classifications = await _repository.GetAssignedToAsync(assignedToInput.AssignNameLike);
                //if (classifications == null)
                //{
                //    var responseApprovalTemplate = new ApiResponse<EmployeeCompanyMST>
                //    {
                //        Status = "Error",
                //        Message = "Error Occurd",
                //        Data = null
                //    };
                //    return Ok(responseApprovalTemplate);
                //}

                return Ok(classifications);
            }
            catch (Exception ex)
            {
                var response = new V_Building_Classification_new
                {
                    Status = "Error",
                    Message = ex.Message,
                    Data = null
                };
                return Ok(response);
            }
        }
        [HttpPost("Task-Management/EMP_TAGS")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<EmployeeLoginOutput_LIST>>> EMP_TAGS([FromBody] EMP_TAGSInput eMP_TAGSInput)
        {
            try
            {
                var classifications = await _repository.GetEmpTagsAsync(eMP_TAGSInput.EMP_TAGS);
                //if (classifications == null)
                //{
                //    var responseApprovalTemplate = new ApiResponse<EmployeeCompanyMST>
                //    {
                //        Status = "Error",
                //        Message = "Error Occurd",
                //        Data = null
                //    };
                //    return Ok(responseApprovalTemplate);
                //}
                return Ok(classifications);
            }
            catch (Exception ex)
            {
                var response = new EmployeeLoginOutput_LIST
                {
                    Status = "Error",
                    Message = ex.Message,
                    Data = null
                };
                return Ok(response);
            }
        }

        [HttpPost("Task-Management/EMP_TAGS_NT")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<EmployeeTagsOutPut_Tags_list_NT>>> EMP_TAGS_NT([FromBody] EMP_TAGSInput_NT eMP_TAGSInput_NT)
        {
            try
            {
                var classifications = await _repository.GetEmpTagsNTAsync(eMP_TAGSInput_NT);
                return Ok(classifications);
            }
            catch (Exception ex)
            {
                var response = new EmployeeTagsOutPut_Tags_list_NT
                {
                    Status = "Error",
                    Message = ex.Message,
                    Data = null
                };
                return Ok(response);
            }
        }

        [HttpPost("Task-Management/TASK-DASHBOARD")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Task_DetailsOutPut_List>>> Task_Details([FromBody] Task_DetailsInput task_DetailsInput)
        {
            try
            {
                var TaskDash = await _repository.GetTaskDetailsAsync(task_DetailsInput.CURRENT_EMP_MKEY, task_DetailsInput.FILTER);
                return Ok(TaskDash);
            }
            catch (Exception ex)
            {
                var response = new Task_DetailsOutPut_List
                {
                    Status = "Error",
                    Message = ex.Message,
                    Data = null,
                    Data1 = null
                };
                return Ok(response);
            }
        }

        [HttpPost("Task-Management/Task-Dashboard_NT")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Task_DetailsOutPutNT_List>>> Task_Details_NT([FromBody] Task_DetailsInputNT task_DetailsInputNT)
        {
            try
            {
                var TaskDash = await _repository.GetTaskDetailsNTAsync(task_DetailsInputNT);
                return Ok(TaskDash);
            }
            catch (Exception ex)
            {
                var response = new Task_DetailsOutPut_List
                {
                    Status = "Error",
                    Message = ex.Message,
                    Data = null,
                    Data1 = null
                };
                return Ok(response);
            }
        }

        [HttpPost("Task-Management/TASK-DETAILS_BY_MKEY")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<TASK_DETAILS_BY_MKEY_list>>> TASK_DETAILS_BY_MKEY([FromBody] TASK_DETAILS_BY_MKEYInput tASK_DETAILS_BY_MKEYInput)
        {
            try
            {
                var TaskDash = await _repository.GetTaskDetailsByMkeyAsync(tASK_DETAILS_BY_MKEYInput.Mkey);
                ////if (TaskDash == null)
                ////{
                ////    var responseApprovalTemplate = new ApiResponse<TASK_DASHBOARD>
                ////    {
                ////        Status = "Error",
                ////        Message = "Error Occurd",
                ////        Data = null
                ////    };
                ////    return Ok(responseApprovalTemplate);
                ////}
                return Ok(TaskDash);
            }
            catch (Exception ex)
            {
                var response = new TASK_DETAILS_BY_MKEY_list
                {
                    Status = "Error",
                    Message = ex.Message,
                    Data = null
                };
                return Ok(response);
            }
        }
        [HttpPost("Task-Management/TASK-NESTED-GRID")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<TASK_DASHBOARD>>> TASK_NESTED_GRID([FromBody] TASK_NESTED_GRIDInput tASK_NESTED_GRIDInput)
        {
            try
            {
                var TaskDash = await _repository.GetTaskNestedGridAsync(tASK_NESTED_GRIDInput.Mkey);
                //if (TaskDash == null)
                //{
                //    var responseTaskTree = new ApiResponse<TASK_DASHBOARD>
                //    {
                //        Status = "Error",
                //        Message = "Error Occurd",
                //        Data = null
                //    };
                //    return Ok(responseTaskTree);
                //}
                return Ok(TaskDash);
            }
            catch (Exception ex)
            {
                var response = new TASK_NESTED_GRIDOutPut_List
                {
                    Status = "Error",
                    Message = ex.Message,
                    Data = null
                };
                return Ok(response);
            }
        }

        [HttpPost("Task-Management/GET-ACTIONS")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<GET_ACTIONS_TYPE_FILE>>> GET_ACTIONS([FromBody] GET_ACTIONSInput gET_ACTIONSInput)
        {
            try
            {
                var TaskAction = await _repository.GetActionsAsync(gET_ACTIONSInput.TASK_MKEY, gET_ACTIONSInput.CURRENT_EMP_MKEY, gET_ACTIONSInput.CURR_ACTION);
                //if (TaskAction == null)
                //{
                //    var responseTaskAction = new ApiResponse<GET_ACTIONS_TYPE_FILE>
                //    {
                //        Status = "Error",
                //        Message = "Error Occurd",
                //        Data = null
                //    };
                //    return Ok(responseTaskAction);
                //}
                return Ok(TaskAction);
            }
            catch (Exception ex)
            {
                var response = new GET_ACTIONS_TYPE_FILE
                {
                    Status = "Error",
                    Message = ex.Message,
                    Data = null
                };
                return Ok(response);
            }
        }

        [HttpPost("Task-Management/GET-TASK_TREE")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<GET_TASK_TREEOutPut_List>>> GET_TASK_TREE([FromBody] GET_TASK_TREEInput gET_TASK_TREEInput)
        {
            try
            {
                var TaskTree = await _repository.GetTaskTreeAsync(gET_TASK_TREEInput.TASK_MKEY);
                //if (TaskTree == null)
                //{
                //    var responseTaskAction = new ApiResponse<GET_TASK_TREEOutPut_List>
                //    {
                //        Status = "Error",
                //        Message = "Error Occurd",
                //        Data = null
                //    };
                //    return Ok(responseTaskAction);
                //}
                foreach (var checkerror in TaskTree)
                {
                    if (checkerror.Status != "Ok")
                    {
                        var response = new GET_ACTIONSOutPut_List
                        {
                            Status = "Error",
                            Message = checkerror.Message,
                            Data = null
                        };
                        return Ok(response);
                    }
                }
                return Ok(TaskTree);
            }
            catch (Exception ex)
            {
                var response = new GET_ACTIONSOutPut_List
                {
                    Status = "Error",
                    Message = ex.Message,
                    Data = null
                };
                return Ok(response);
            }
        }

        [HttpPut("Task-Management/Change_Password")]
        [Authorize]
        public async Task<ActionResult<PutChangePasswordOutPut_List>> ChangePassword([FromBody] ChangePasswordInput changePasswordInput)
        {
            try
            {
                var ChangePass = await _repository.PutChangePasswordAsync(changePasswordInput.LoginName, changePasswordInput.Old_Password, changePasswordInput.New_Password);

                //if (ChangePass == null)
                //{
                //    var responseTaskAction = new ApiResponse<EmployeeCompanyMST>
                //    {
                //        Status = "Error",
                //        Message = "Error Occurd",
                //        Data = null
                //    };
                //    return Ok(responseTaskAction);
                //}
                return Ok(ChangePass);
            }
            catch (Exception ex)
            {
                var response = new PutChangePasswordOutPut_List
                {
                    Status = "Error",
                    Message = ex.Message,
                    Data = null
                };
                return Ok(response);
            }
        }

        [HttpPost("Task-Management/Forgot_Password")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<ResetPasswordOutPut_List>>> ForgotPassword([FromBody] ForgotPasswordInput forgotPasswordInput)
        {
            try
            {
                var ForgotPass = await _repository.GetForgotPasswordAsync(forgotPasswordInput.LoginName);

                if (ForgotPass == null)
                {
                    var responseTaskAction = new ApiResponse<EmployeeCompanyMST>
                    {
                        Status = "Error",
                        Message = "Error Occurd",
                        Data = null
                    };
                    return Ok(responseTaskAction);
                }
                if (forgotPasswordInput.LoginName == null)
                {
                    var responseTaskAction = new ForgotPasswordOutPut_List
                    {
                        Status = "Error",
                        Message = "Error Occurd LoginName",
                        Data = null
                    };
                    return Ok(responseTaskAction);
                }
                string TempararyPass = string.Empty;
                foreach (var TempPaass in ForgotPass)
                {
                    TempararyPass = TempPaass.Data.Select(x => x.MessageText.ToString()).First().ToString();
                }


                var ResetPass = await _repository.GetResetPasswordAsync(TempararyPass, forgotPasswordInput.LoginName);

                //if (ResetPass == null)
                //{
                //    var responseTaskAction = new ApiResponse<EmployeeCompanyMST>
                //    {
                //        Status = "Error",
                //        Message = "Error Occurd",
                //        Data = null
                //    };
                //    return Ok(responseTaskAction);
                //}

                return Ok(ResetPass);
            }
            catch (Exception ex)
            {
                var response = new ResetPasswordOutPut_List
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

        [HttpPost("Task-Management/Validate_Email")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<ChangePasswordOutPut_List>>> ValidateEmail([FromBody] ValidateEmailInput validateEmailInput)
        {
            try
            {
                var ValidateEmailVar = await _repository.GetValidateEmailAsync(validateEmailInput.Login_ID);

                //if (ValidateEmailVar == null)
                //{
                //    var responseTaskAction = new ApiResponse<EmployeeCompanyMST>
                //    {
                //        Status = "Error",
                //        Message = "Error Occurd",
                //        Data = null
                //    };
                //    return Ok(responseTaskAction);
                //}
                return Ok(ValidateEmailVar);
            }
            catch (Exception ex)
            {
                var response = new ChangePasswordOutPut_List
                {
                    Status = "Error",
                    Message = ex.Message,
                    Data = null
                };
                return Ok(response);
            }
        }

        [HttpPost("Task-Management/TASK-DASHBOARD_DETAILS")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<GET_TASK_TREEOutPut_List>>> Task_Dashboard_Details([FromBody] Task_Dashboard_DetailsInput task_Dashboard_DetailsInput)
        {
            try
            {
                var TaskDashboardDetails = await _repository.GetTaskDashboardDetailsAsync(Convert.ToString(task_Dashboard_DetailsInput.CURRENT_EMP_MKEY), task_Dashboard_DetailsInput.CURR_ACTION);

                //if (TaskDashboardDetails == null)
                //{
                //    var responseTaskAction = new ApiResponse<TASK_DASHBOARD>
                //    {
                //        Status = "Error",
                //        Message = "Error Occurd",
                //        Data = null
                //    };
                //    return Ok(responseTaskAction);
                //}
                return Ok(TaskDashboardDetails);
            }
            catch (Exception ex)
            {
                var response = new GET_TASK_TREEOutPut_List
                {
                    Status = "Error",
                    Message = ex.Message,
                    Table = null,
                    Table1 = null,
                    Table2 = null,
                    Table3 = null,
                    Table4 = null,
                    Table5 = null,
                    Table6 = null,
                    Table7 = null,
                    Table8 = null
                };
                return Ok(response);
            }
        }

        [HttpPost("Task-Management/TeamTask")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<GetTaskTeamOutPut_List>>> TeamTask([FromBody] TeamTaskInput teamTaskInput)
        {
            try
            {
                var TaskTeamTask = await _repository.GetTeamTaskAsync(teamTaskInput.CURRENT_EMP_MKEY);

                //if (TaskTeamTask == null)
                //{
                //    var responseTeamTask = new ApiResponse<TASK_DASHBOARD>
                //    {
                //        Status = "Error",
                //        Message = "Error Occurd",
                //        Data = null
                //    };
                //    return Ok(responseTeamTask);
                //}
                return Ok(TaskTeamTask);
            }
            catch (Exception ex)
            {
                var response = new GetTaskTeamOutPut_List
                {
                    Status = "Error",
                    Message = ex.Message,
                    Data = null,
                    Data1 = null
                };
                return Ok(response);
            }
        }

        [HttpPost("Task-Management/Team_Task_Details")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<TASK_DASHBOARDOutPut_List>>> Team_Task_Details([FromBody] Team_Task_DetailsInput team_Task_DetailsInput)
        {
            try
            {
                DataTable Temptable = new DataTable();
                var TaskTeamTask = await _repository.GetTeamTaskDetailsAsync(team_Task_DetailsInput.CURRENT_EMP_MKEY);

                if (TaskTeamTask == null)
                {
                    var responseTeamTask = new TASK_DASHBOARDOutPut_List
                    {
                        Status = "Error",
                        Message = "Error Occured",
                        Data = null
                    };
                    return Ok(responseTeamTask);
                }

                //DataTable TeamTaskDT = new DataTable();

                //TeamTaskDT.Columns.Add("TASKTYPE", typeof(string));
                //TeamTaskDT.Columns.Add("TASKTYPE_DESC", typeof(string));
                //TeamTaskDT.Columns.Add("CURRENT_EMP_MKEY", typeof(string));

                foreach (var task in TaskTeamTask)
                {
                    if (task.Data != null)
                    {
                        // Apply LINQ filter to the Data property of each task
                        var filteredData = task.Data1.Where(a =>
                            a.TASKTYPE == team_Task_DetailsInput.TASKTYPE &&
                            a.TASKTYPE_DESC == team_Task_DetailsInput.TASKTYPE_DESC &&
                            a.CURRENT_EMP_MKEY.ToString() == team_Task_DetailsInput.mKEY.ToString())
                            .ToList();
                        task.Data1 = filteredData;
                        // For each filtered item, add a row to the DataTable
                        //foreach (var item in filteredData)
                        //{
                        //    DataRow row = TeamTaskDT.NewRow();
                        //    row["TASKTYPE"] = item.TASKTYPE;
                        //    row["TASKTYPE_DESC"] = item.TASKTYPE_DESC;
                        //    row["CURRENT_EMP_MKEY"] = item.CURRENT_EMP_MKEY;
                        //    TeamTaskDT.Rows.Add(row);
                        //}
                    }
                }


                // Populate DataTable with the List<TASK_DASHBOARD>
                //foreach (var task in TaskTeamTask)
                //{
                //    DataRow row = TeamTaskDT.NewRow();
                //    //row["TASKTYPE"] = task.Data.Where(x => x.TASKTYPE = "gfgf").FirstOrDefault();
                //    //row["TASKTYPE_DESC"] = task.Data.Select(x => x.TASKTYPE_DESC).FirstOrDefault();
                //    //row["CURRENT_EMP_MKEY"] = task.Data.Select(x => x.CURRENT_EMP_MKEY).FirstOrDefault();

                //    var query = from a in task.Data
                //                    //where a.TASKTYPE  == team_Task_DetailsInput.TASKTYPE && a.TASKTYPE_DESC == team_Task_DetailsInput.TASKTYPE_DESC && a.CURRENT_EMP_MKEY.ToString() == team_Task_DetailsInput.mKEY.ToString()
                //                select a;

                //    var filterresult = from a in query
                //                       where a.TASKTYPE == team_Task_DetailsInput.TASKTYPE && a.TASKTYPE_DESC == team_Task_DetailsInput.TASKTYPE_DESC && a.CURRENT_EMP_MKEY.ToString() == team_Task_DetailsInput.mKEY.ToString()
                //                       select a;

                //    TeamTaskDT.Rows.Add(row);
                //}

                //var searchStr = team_Task_DetailsInput.TASKTYPE.ToString().Trim() + " " + team_Task_DetailsInput.TASKTYPE_DESC.ToString().Trim() + " " + team_Task_DetailsInput.mKEY;
                //var splitSearchString = searchStr.Split(' ');
                //var columnNameStr = "TASKTYPE TASKTYPE_DESC CURRENT_EMP_MKEY";
                //var splitcolumnNameStr = columnNameStr.Split(' ');
                //var expression = new List<string>();
                //DataTable table = new DataTable();

                //int iCnt = 0;
                //foreach (var searchElement in splitSearchString)
                //{
                //    expression.Add(
                //        string.Format("[{0}] = '{1}'", splitcolumnNameStr[iCnt], searchElement));
                //    iCnt++;
                //}
                //var searchExpressionString = string.Join(" and ", expression.ToArray());
                //DataRow[] rows = TeamTaskDT.Select(searchExpressionString);

                //Temptable = TeamTaskDT.Clone();
                //for (int i = 0; i < rows.Length; i++)
                //    Temptable.Rows.Add(rows[i].ItemArray);

                // Convert the DataTable to a list of dictionaries (which is serializable)
                // var result = DataTableToList(Temptable);

                return Ok(TaskTeamTask);
            }
            catch (Exception ex)
            {
                var response = new TASK_DASHBOARDOutPut_List
                {
                    Status = "Error",
                    Message = ex.Message,
                    Data = null
                };
                return Ok(response);
            }
        }

        // Helper method to convert DataTable to a list of dictionaries
        private List<Dictionary<string, object>> DataTableToList(DataTable dt)
        {
            var rows = new List<Dictionary<string, object>>();

            foreach (DataRow row in dt.Rows)
            {
                var dict = new Dictionary<string, object>();
                foreach (DataColumn column in dt.Columns)
                {
                    dict[column.ColumnName] = row[column];
                }
                rows.Add(dict);
            }

            return rows;
        }

        [HttpPost("Task-Management/Get_Project_Details")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Get_Project_DetailsWithSubprojectOutPut_List>>> Get_Project_DetailsWithSubproject([FromBody] Get_Project_DetailsWithSubprojectInput get_Project_DetailsWithSubprojectInput)
        {
            try
            {
                var TaskTeamTask = await _repository.GetProjectDetailsWithSubProjectAsync(get_Project_DetailsWithSubprojectInput.ProjectID, get_Project_DetailsWithSubprojectInput.SubProjectID);

                //if (TaskTeamTask == null)
                //{
                //    var responseTeamTask = new ApiResponse<TASK_HDR>
                //    {
                //        Status = "Error",
                //        Message = "Error Occurd",
                //        Data = null
                //    };
                //    return Ok(responseTeamTask);
                //}
                return Ok(TaskTeamTask);
            }
            catch (Exception ex)
            {
                var response = new Get_Project_DetailsWithSubprojectOutPut_List
                {
                    Status = "Error",
                    Message = ex.Message,
                    Data = null
                };
                return Ok(response);
            }
        }

        [HttpPost("Task-Management/GET-TASK_TREEExport")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<TASK_NESTED_GRIDOutPut_List>>> GET_TASK_TREEExport([FromBody] GET_TASK_TREEExportInput gET_TASK_TREEExportInput)
        {
            try
            {
                DataTable dsTaskTree = new DataTable();
                var TaskTreeExport = await _repository.GetTaskTreeExportAsync(gET_TASK_TREEExportInput.TASK_MKEY.ToString());

                //if (TaskTreeExport == null)
                //{
                //    var responseTeamTask = new ApiResponse<TASK_HDR>
                //    {
                //        Status = "Error",
                //        Message = "Error Occurd",
                //        Data = null
                //    };
                //    return Ok(responseTeamTask);
                //}
                DataTable dsTaskTreeExport2 = new DataTable();
                foreach (var TaskTtree in TaskTreeExport)
                {
                    dsTaskTreeExport2 = ConvertToDataTable(TaskTtree.Data);
                    using (var reader = ObjectReader.Create(TaskTtree.Data))
                    {
                        dsTaskTreeExport2.Load(reader);
                    }
                }


                dsTaskTree = dsTaskTreeExport2;// ConvertToDataTable(dsTaskTreeExport2);

                var fileName = $"Task_Details_{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.xlsx";

                // Create a MemoryStream to hold the Excel file in memory
                using (var memoryStream = new MemoryStream())
                {
                    // Create an Excel package (ExcelPackage is the class from EPPlus)
                    using (var package = new ExcelPackage(memoryStream))
                    {
                        // Create a new worksheet in the Excel file
                        var worksheet = package.Workbook.Worksheets.Add("TaskDetails");

                        // Add headers to the worksheet
                        for (int col = 1; col <= dsTaskTree.Columns.Count; col++)
                        {
                            worksheet.Cells[1, col].Value = dsTaskTree.Columns[col - 1].ColumnName;
                        }

                        // Add data to the worksheet
                        for (int row = 0; row < dsTaskTree.Rows.Count; row++)
                        {
                            for (int col = 0; col < dsTaskTree.Columns.Count; col++)
                            {
                                worksheet.Cells[row + 2, col + 1].Value = dsTaskTree.Rows[row][col].ToString();
                            }
                        }

                        // Save the Excel file to the memory stream
                        package.Save();
                    }

                    // Set the memory stream position to the beginning before returning the file
                    memoryStream.Position = 0;

                    // Return the file as an Excel file
                    return File(memoryStream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
                }
                return Ok(dsTaskTree);
            }
            catch (Exception ex)
            {
                var response = new TASK_NESTED_GRIDOutPut_List
                {
                    Status = "Error",
                    Message = ex.Message,
                    Data = null
                };
                return Ok(response);
            }
        }
        private DataTable ConvertToDataTable(IEnumerable<dynamic> result)
        {
            var table = new DataTable();
            var firstRow = result.FirstOrDefault();

            if (firstRow != null)
            {
                foreach (var property in firstRow)
                {
                    table.Columns.Add(property.Key);
                }

                foreach (var row in result)
                {
                    var dataRow = table.NewRow();
                    foreach (var property in row)
                    {
                        dataRow[property.Key] = property.Value;
                    }
                    table.Rows.Add(dataRow);
                }
            }

            return table;
        }

        [HttpPost("Task-Management/Get_ExportProject_DetailsWithSubproject")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Get_Project_DetailsWithSubprojectOutPut_List>>> Get_ExportProject_DetailsWithSubproject([FromBody] Get_ExportProject_DetailsWithSubprojectInput get_ExportProject_DetailsWithSubprojectInput)
        {
            try
            {
                DataTable dsTaskTree = new DataTable();
                var ProjectTask = await _repository.GetProjectDetailsWithSubProjectAsync(get_ExportProject_DetailsWithSubprojectInput.ProjectID, get_ExportProject_DetailsWithSubprojectInput.SubProjectID);

                if (ProjectTask == null)
                {
                    var responseTeamTask = new ApiResponse<TASK_HDR>
                    {
                        Status = "Error",
                        Message = "Error Occurd",
                        Data = null
                    };
                    return Ok(responseTeamTask);
                }
                DataTable dsTaskTreeExport2 = new DataTable();
                foreach (var TaskTtree in ProjectTask)
                {
                    dsTaskTreeExport2 = ConvertToDataTable(TaskTtree.Data);
                    using (var reader = ObjectReader.Create(TaskTtree.Data))
                    {
                        dsTaskTreeExport2.Load(reader);
                    }
                }

                dsTaskTree = dsTaskTreeExport2;

                var fileName = $"Task_Details_{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.xlsx";

                // Create a MemoryStream to hold the Excel file in memory
                using (var memoryStream = new MemoryStream())
                {
                    // Create an Excel package (ExcelPackage is the class from EPPlus)
                    using (var package = new ExcelPackage(memoryStream))
                    {
                        // Create a new worksheet in the Excel file
                        var worksheet = package.Workbook.Worksheets.Add("TaskDetails");

                        // Add headers to the worksheet
                        for (int col = 1; col <= dsTaskTree.Columns.Count; col++)
                        {
                            worksheet.Cells[1, col].Value = dsTaskTree.Columns[col - 1].ColumnName;
                        }

                        // Add data to the worksheet
                        for (int row = 0; row < dsTaskTree.Rows.Count; row++)
                        {
                            for (int col = 0; col < dsTaskTree.Columns.Count; col++)
                            {
                                worksheet.Cells[row + 2, col + 1].Value = dsTaskTree.Rows[row][col].ToString();
                            }
                        }

                        // Save the Excel file to the memory stream
                        package.Save();
                    }

                    // Set the memory stream position to the beginning before returning the file
                    memoryStream.Position = 0;

                    // Return the file as an Excel file
                    return File(memoryStream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
                }

                return Ok(dsTaskTree);
            }
            catch (Exception ex)
            {
                var response = new Get_Project_DetailsWithSubprojectOutPut_List
                {
                    Status = "Error",
                    Message = ex.Message,
                    Data = null
                };
                return Ok(response);
            }
        }

        [HttpPost("Task-Management/Add-Task")]
        [Authorize]
        public async Task<ActionResult<Add_TaskOutPut_List>> Add_Task([FromBody] Add_TaskInput add_TaskInput)
        {
            try
            {
                var modelTask = await _repository.CreateAddTaskAsync(add_TaskInput);
                //if (modelTask == null)
                //{
                //    var responseStatus = new ApiResponse<TASK_HDR>
                //    {
                //        Status = "Error",
                //        Message = "Error Occurd",
                //        Data = null // No data in case of exception
                //    };
                //    return Ok(responseStatus);
                //}
                //var response = new ApiResponse<TASK_HDR>
                //{
                //    Status = "Ok",
                //    Message = "Inserted Successfully",
                //    Data = modelTask // No data in case of exception
                //};
                return Ok(modelTask);
            }
            catch (Exception ex)
            {
                var response = new Add_TaskOutPut_List
                {
                    Status = "Error",
                    Message = ex.Message,
                    Data = null
                };
                return Ok(response);
            }
        }

        [HttpPost("Task-Management/Add-Sub-Task")]
        [Authorize]
        public async Task<ActionResult<Add_TaskOutPut_List>> Add_Sub_Task([FromBody] Add_Sub_TaskInput add_Sub_TaskInput)
        {
            //(string TASK_NO, string TASK_NAME, string TASK_DESCRIPTION, string CATEGORY, string PROJECT_ID, string SUBPROJECT_ID, string COMPLETION_DATE, string ASSIGNED_TO, string TAGS, string ISNODE, string START_DATE, string CLOSE_DATE, string DUE_DATE, string TASK_PARENT_ID, string TASK_PARENT_NODE_ID, string TASK_PARENT_NUMBER, string STATUS, string STATUS_PERC, string TASK_CREATED_BY, string APPROVER_ID, string IS_ARCHIVE, string ATTRIBUTE1, string ATTRIBUTE2, string ATTRIBUTE3, string ATTRIBUTE4, string ATTRIBUTE5, string CREATED_BY, string CREATION_DATE, string LAST_UPDATED_BY, string APPROVE_ACTION_DATE, string Current_task_mkey)
            try
            {
                var modelTask = await _repository.CreateAddSubTaskAsync(add_Sub_TaskInput);
                //if (modelTask == null)
                //{
                //    var responseStatus = new ApiResponse<TASK_HDR>
                //    {
                //        Status = "Error",
                //        Message = "Error Occurd",
                //        Data = null // No data in case of exception
                //    };
                //    return Ok(responseStatus);
                //}
                //var response = new ApiResponse<TASK_HDR>
                //{
                //    Status = "Ok",
                //    Message = "Inserted Successfully",
                //    Data = modelTask // No data in case of exception
                //};
                return Ok(modelTask);
            }
            catch (Exception ex)
            {
                var response = new Add_TaskOutPut_List
                {
                    Status = "Error",
                    Message = ex.Message,
                    Data = null
                };
                return Ok(response);
            }
        }

        //[HttpPost("Task-Management/FileUpload"), DisableRequestSizeLimit]
        //[Authorize]
        //public async Task<IActionResult> Post([FromForm] IFormCollection form)
        //{
        //    try
        //    {
        //        string uploadFilePath = _configuration["UploadFile_Path"];
        //        var files = form.Files;
        //        string taskMkey = form["Mkey"];
        //        string createdBy = form["CREATED_BY"];
        //        string deleteFlag = form["DELETE_FLAG"];
        //        string taskParentId = form["TASK_PARENT_ID"];
        //        string taskMainNodeId = form["TASK_MAIN_NODE_ID"];
        //        int srNo = 0;
        //        var uploadedFiles = new List<string>();
        //        string fileName, filePath;

        //        if (files.Count > 0)
        //        {
        //            foreach (var file in files)
        //            {
        //                srNo++;
        //                fileName = file.FileName;
        //                string fileDirectory = Path.Combine(uploadFilePath, taskMainNodeId);

        //                if (!Directory.Exists(fileDirectory))
        //                {
        //                    Directory.CreateDirectory(fileDirectory);
        //                }

        //                // Generate new file name with timestamp
        //                string newFileName = DateTime.Now.Day + "_" + DateTime.Now.ToShortTimeString().Replace(":", "_") + "_" + fileName;
        //                filePath = Path.Combine(fileDirectory, newFileName);

        //                // Save the file
        //                using (var fileStream = new FileStream(filePath, FileMode.Create))
        //                {
        //                    await file.CopyToAsync(fileStream);
        //                }

        //                uploadedFiles.Add(filePath);

        //                string fileRelativePath = Path.Combine(_configuration["Refer_UploadFile_Path"], taskMainNodeId, newFileName);

        //                if (file.Length > 0)
        //                {
        //                    // Perform the database insertion or other operations
        //                    await _repository.TASKFileUpoadAsync(srNo.ToString(), taskMkey, taskParentId, fileName, filePath, createdBy, deleteFlag, taskMainNodeId);
        //                }
        //            }

        //            // Return the response
        //            return Ok(new { Status = "Success", Files = uploadedFiles });  // You can return a custom response
        //        }
        //        else
        //        {
        //            if (taskMkey != "0000")
        //            {
        //                await _repository.UpdateTASKFileUpoadAsync(taskMkey, deleteFlag);
        //            }
        //        }

        //        return BadRequest("No files were uploaded.");
        //    }
        //    catch (Exception ex)
        //    {
        //        // Handle exception (could be logging or returning an error response)
        //        return StatusCode(400, new { Message = "An error occurred", Error = ex.Message });
        //    }
        //}

        [Authorize]
        [HttpPost("Task-Management/FileUpload"), DisableRequestSizeLimit]
        public async Task<IActionResult> Post([FromForm] TaskFileUploadAPI objFile)
        {
            try
            {
                int srNo = 0;
                string filePathOpen = string.Empty;
                if (objFile.files.Length > 0)
                {
                    srNo = srNo + 1;
                    //objFile.FILE_PATH = "D:\\DATA\\Projects\\Task_Mangmt\\Task_Mangmt\\Task\\";
                    objFile.FILE_PATH = _fileSettings.FilePath;
                    if (!Directory.Exists(objFile.FILE_PATH + "\\Attachments\\" + objFile.TASK_MAIN_NODE_ID))
                    {
                        Directory.CreateDirectory(objFile.FILE_PATH + "\\Attachments\\" + objFile.TASK_MAIN_NODE_ID);
                    }
                    using (FileStream filestream = System.IO.File.Create(objFile.FILE_PATH + "\\Attachments\\" + objFile.TASK_MAIN_NODE_ID + "\\" + DateTime.Now.Day + "_" + DateTime.Now.ToShortTimeString().Replace(":", "_") + "_" + objFile.files.FileName))
                    {
                        objFile.files.CopyTo(filestream);
                        filestream.Flush();
                    }
                    objFile.FILE_NAME = objFile.files.FileName;
                    filePathOpen = "Attachments\\" + objFile.TASK_MAIN_NODE_ID + "\\" + DateTime.Now.Day + "_" + DateTime.Now.ToShortTimeString().Replace(":", "_") + "_" + objFile.files.FileName;
                    int ResultCount = await _repository.TASKFileUpoadAsync(srNo, objFile.TASK_MKEY, objFile.TASK_PARENT_ID, objFile.FILE_NAME, filePathOpen, objFile.CREATED_BY, Convert.ToChar(objFile.DELETE_FLAG), objFile.TASK_MAIN_NODE_ID);
                    objFile.FILE_PATH = filePathOpen;
                    if (ResultCount > 0)
                    {
                        var Successresponse = new Add_TaskOutPut_List
                        {
                            Status = "ok",
                            Message = "File Uploaded",
                            Data2 = objFile
                        };
                        return Ok(Successresponse);
                    }
                    else
                    {
                        var Errorresponse = new Add_TaskOutPut_List
                        {
                            Status = "Error",
                            Message = "Error occurred",
                            Data1 = null
                        };
                        return Ok(Errorresponse);
                    }
                }
                var response = new Add_TaskOutPut_List
                {
                    Status = "Error",
                    Message = "please attach the file!!!",
                    Data1 = null
                };
                return Ok(response);

            }
            catch (Exception ex)
            {
                var response = new Add_TaskOutPut_List
                {
                    Status = "Error",
                    Message = ex.Message,
                    Data1 = null
                };
                return Ok(response);
            }
        }
        [HttpPost("Task-Management/Add-Task_NT")]
        [Authorize]
        public async Task<ActionResult<Add_TaskOutPut_List_NT>> Add_Task_NT([FromBody] Add_TaskInput_NT add_TaskInput_NT)
        {
            try
            {
                
                var modelTask = await _repository.CreateAddTaskNTAsync(add_TaskInput_NT);
                return Ok(modelTask);
            }
            catch (Exception ex)
            {
                var response = new Add_TaskOutPut_List_NT
                {
                    Status = "Error",
                    Message = ex.Message,
                    Data = null
                };
                return Ok(response);
            }
        }

        [HttpPost("Task-Management/Add-Sub-Task_NT")]
        [Authorize]
        public async Task<ActionResult<Add_TaskOutPut_List_NT>> Add_Sub_Task_NT([FromBody] Add_Sub_TaskInput_NT add_Sub_TaskInput_NT)
        {
            try
            {
                var modelTask = await _repository.CreateAddSubTaskNTAsync(add_Sub_TaskInput_NT);
                return Ok(modelTask);
            }
            catch (Exception ex)
            {
                var response = new Add_TaskOutPut_List_NT
                {
                    Status = "Error",
                    Message = ex.Message,
                    Data = null
                };
                return Ok(response);
            }
        }

        [Authorize]
        [HttpPost("Task-Management/FileUpload_NT"), DisableRequestSizeLimit]
        public async Task<IActionResult> Post([FromForm] TaskFileUploadAPI_NT objFile)
        {
            try
            {
                int srNo = 0;
                string filePathOpen = string.Empty;
                if (objFile.files.Length > 0)
                {
                    srNo = srNo + 1;
                    //objFile.FILE_PATH = "D:\\DATA\\Projects\\Task_Mangmt\\Task_Mangmt\\Task\\";
                    objFile.FILE_PATH = _fileSettings.FilePath;
                    if (!Directory.Exists(objFile.FILE_PATH + "\\Attachments\\" + objFile.TASK_MAIN_NODE_ID))
                    {
                        Directory.CreateDirectory(objFile.FILE_PATH + "\\Attachments\\" + objFile.TASK_MAIN_NODE_ID);
                    }
                    using (FileStream filestream = System.IO.File.Create(objFile.FILE_PATH + "\\Attachments\\" + objFile.TASK_MAIN_NODE_ID + "\\" + DateTime.Now.Day + "_" + DateTime.Now.ToShortTimeString().Replace(":", "_") + "_" + objFile.files.FileName))
                    {
                        objFile.files.CopyTo(filestream);
                        filestream.Flush();
                    }
                    objFile.FILE_NAME = objFile.files.FileName;
                    filePathOpen = "Attachments\\" + objFile.TASK_MAIN_NODE_ID + "\\" + DateTime.Now.Day + "_" + DateTime.Now.ToShortTimeString().Replace(":", "_") + "_" + objFile.files.FileName;
                    int ResultCount = await _repository.TASKFileUpoadNTAsync(srNo, objFile.TASK_MKEY, objFile.TASK_PARENT_ID, objFile.FILE_NAME, filePathOpen, objFile.CREATED_BY, Convert.ToChar(objFile.DELETE_FLAG), objFile.TASK_MAIN_NODE_ID);
                    objFile.FILE_PATH = filePathOpen;
                    if (ResultCount > 0)
                    {
                        var Successresponse = new Add_TaskOutPut_List_NT
                        {
                            Status = "ok",
                            Message = "File Uploaded",
                            Data2 = objFile
                        };
                        return Ok(Successresponse);
                    }
                    else
                    {
                        var Errorresponse = new Add_TaskOutPut_List_NT
                        {
                            Status = "Error",
                            Message = "Error occurred",
                            Data1 = null
                        };
                        return Ok(Errorresponse);
                    }
                }
                var response = new Add_TaskOutPut_List_NT
                {
                    Status = "Error",
                    Message = "please attach the file!!!",
                    Data1 = null
                };
                return Ok(response);

            }
            catch (Exception ex)
            {
                var response = new Add_TaskOutPut_List_NT
                {
                    Status = "Error",
                    Message = ex.Message,
                    Data1 = null
                };
                return Ok(response);
            }
        }

        //[HttpPost("Task-Management/TASK-ACTION-TRL-Insert-Update"), DisableRequestSizeLimit]
        //[Authorize]
        //public async Task<IActionResult> Post_TASK_ACTION([FromForm] TaskPostActionFileUploadAPI objFile)
        //{
        //    try
        //    {
        //        int srNo = 0;
        //        string filePathOpen = string.Empty;
        //        if (objFile.files != null)
        //        {
        //            if (objFile.files.Length > 0)
        //            {
        //                srNo = srNo + 1;
        //                //objFile.FILE_PATH = "D:\\DATA\\Projects\\Task_Mangmt\\Task_Mangmt\\Task\\";
        //                objFile.FILE_PATH = _fileSettings.FilePath;
        //                if (!Directory.Exists(objFile.FILE_PATH + "\\Attachments\\" + objFile.TASK_MAIN_NODE_ID))
        //                {
        //                    Directory.CreateDirectory(objFile.FILE_PATH + "\\Attachments\\" + objFile.TASK_MAIN_NODE_ID);
        //                }
        //                using (FileStream filestream = System.IO.File.Create(objFile.FILE_PATH + "\\Attachments\\" + objFile.TASK_MAIN_NODE_ID + "\\" + DateTime.Now.Day + "_" + DateTime.Now.ToShortTimeString().Replace(":", "_") + "_" + objFile.files.FileName))
        //                {
        //                    objFile.files.CopyTo(filestream);
        //                    filestream.Flush();
        //                }
        //                objFile.FILE_NAME = objFile.files.FileName;
        //                filePathOpen = "Attachments\\" + objFile.TASK_MAIN_NODE_ID + "\\" + DateTime.Now.Day + "_" + DateTime.Now.ToShortTimeString().Replace(":", "_")
        //                    + "_" + objFile.files.FileName;
        //                int ResultCount = await _repository.GetPostTaskActionAsync(objFile.Mkey.ToString(), objFile.TASK_MKEY.ToString(), objFile.TASK_PARENT_ID.ToString(),
        //                    objFile.ACTION_TYPE, objFile.DESCRIPTION_COMMENT, objFile.PROGRESS_PERC, objFile.STATUS, objFile.CREATED_BY.ToString(),
        //                    objFile.TASK_MAIN_NODE_ID.ToString(), objFile.FILE_NAME, filePathOpen);
        //                objFile.FILE_PATH = filePathOpen;
        //                if (ResultCount > 0)
        //                {
        //                    var Successresponse = new Add_TaskOutPut_List
        //                    {
        //                        Status = "ok",
        //                        Message = "File Uploaded",
        //                        Data1 = objFile
        //                    };
        //                    return Ok(Successresponse);
        //                }
        //                else
        //                {
        //                    filePathOpen = null;
        //                    int Result = await _repository.GetPostTaskActionAsync(objFile.Mkey.ToString(), objFile.TASK_MKEY.ToString(), objFile.TASK_PARENT_ID.ToString(),
        //                        objFile.ACTION_TYPE, objFile.DESCRIPTION_COMMENT, objFile.PROGRESS_PERC, objFile.STATUS, objFile.CREATED_BY.ToString(),
        //                        objFile.TASK_MAIN_NODE_ID.ToString(), null, null);
        //                    objFile.FILE_PATH = filePathOpen;

        //                    var Errorresponse = new Add_TaskOutPut_List
        //                    {
        //                        Status = "Error",
        //                        Message = "Error occurred",
        //                        Data1 = null
        //                    };
        //                    return Ok(Errorresponse);
        //                }
        //            }
        //        }
        //        else
        //        {
        //            filePathOpen = null;
        //            int Result = await _repository.GetPostTaskActionAsync(objFile.Mkey.ToString(), objFile.TASK_MKEY.ToString(), objFile.TASK_PARENT_ID.ToString(),
        //                objFile.ACTION_TYPE, objFile.DESCRIPTION_COMMENT, objFile.PROGRESS_PERC, objFile.STATUS, objFile.CREATED_BY.ToString(),
        //                objFile.TASK_MAIN_NODE_ID.ToString(), null, null);
        //            objFile.FILE_PATH = filePathOpen;

        //            var Successresponse = new Add_TaskOutPut_List
        //            {
        //                Status = "Ok",
        //                Message = "Updated Successfuly",
        //                Data1 = null
        //            };
        //            return Ok(Successresponse);
        //        }

        //        var response = new Add_TaskOutPut_List
        //        {
        //            Status = "Error",
        //            Message = "Please attach the file!!!",
        //            Data1 = null
        //        };
        //        return Ok(response);

        //    }
        //    catch (Exception ex)
        //    {
        //        var response = new Add_TaskOutPut_List
        //        {
        //            Status = "Error",
        //            Message = ex.Message,
        //            Data1 = null
        //        };
        //        return Ok(response);
        //    }
        //}

        [HttpPost("Task-Management/TASK-ACTION-TRL-Insert-Update"), DisableRequestSizeLimit]
        [Authorize]
        public async Task<ActionResult<TaskPostActionFileUploadAPIOutPut_List>> Post_TASK_ACTION([FromForm] TaskPostActionFileUploadAPI objFile)
        {
            try
            {
                int srNo = 0;
                string filePathOpen = string.Empty;
                if (objFile.files != null)
                {
                    if (objFile.files.Length > 0)
                    {
                        srNo = srNo + 1;
                        string FilePath = _fileSettings.FilePath;
                        if (!Directory.Exists(FilePath + "\\Attachments\\" + objFile.TASK_MAIN_NODE_ID))
                        {
                            Directory.CreateDirectory(FilePath + "\\Attachments\\" + objFile.TASK_MAIN_NODE_ID);
                        }
                        using (FileStream filestream = System.IO.File.Create(FilePath + "\\Attachments\\" + objFile.TASK_MAIN_NODE_ID + "\\" + DateTime.Now.Day + "_" + DateTime.Now.ToShortTimeString().Replace(":", "_") + "_" + objFile.files.FileName))
                        {
                            objFile.files.CopyTo(filestream);
                            filestream.Flush();
                        }

                        filePathOpen = "Attachments\\" + objFile.TASK_MAIN_NODE_ID + "\\" + DateTime.Now.Day + "_" + DateTime.Now.ToShortTimeString().Replace(":", "_") + "_" + objFile.files.FileName;
                        var FileUploadDetails = new TaskPostActionFileUploadAPIOutPut
                        {
                            FILE_NAME = objFile.files.FileName,
                            FILE_PATH = filePathOpen,
                            TASK_MAIN_NODE_ID = objFile.TASK_MAIN_NODE_ID
                        };

                        var SuccessResult = new TaskPostActionFileUploadAPIOutPut_List
                        {
                            Status = "Ok",
                            Message = "Uploaded file",
                            Data = new List<TaskPostActionFileUploadAPIOutPut> { FileUploadDetails }
                        };

                        return SuccessResult;
                    }
                }
                var response = new TaskPostActionFileUploadAPIOutPut_List
                {
                    Status = "Error",
                    Message = "Please attach the file!!!",
                    Data = null
                };
                return Ok(response);

            }
            catch (Exception ex)
            {
                var response = new TaskPostActionFileUploadAPIOutPut_List
                {
                    Status = "Error",
                    Message = ex.Message,
                    Data = null
                };
                return Ok(response);
            }
        }

        [HttpPost("Task-Management/TASK-ACTION-TRL-Update"), DisableRequestSizeLimit]
        [Authorize]
        public async Task<ActionResult<TaskPostActionAPIOutPut_List>> UPDATE_TASK_ACTION([FromBody] TaskPostActionInput taskPostActionInput)
        {
            try
            {
                if (taskPostActionInput.FILE_NAME != null)
                {
                    taskPostActionInput.FILE_PATH = "Attachments\\" + taskPostActionInput.TASK_MAIN_NODE_ID + "\\" + DateTime.Now.Day + "_" + DateTime.Now.ToShortTimeString().Replace(":", "_") + "_" + taskPostActionInput.FILE_NAME;
                }
                else
                {
                    taskPostActionInput.FILE_PATH = string.Empty;
                }

                int ResultCount = await _repository.GetPostTaskActionAsync(taskPostActionInput.Mkey.ToString(), taskPostActionInput.TASK_MKEY.ToString(),
                    taskPostActionInput.TASK_PARENT_ID.ToString(),
                    taskPostActionInput.ACTION_TYPE, taskPostActionInput.DESCRIPTION_COMMENT, taskPostActionInput.PROGRESS_PERC, taskPostActionInput.STATUS,
                    taskPostActionInput.CREATED_BY.ToString(),
                    taskPostActionInput.TASK_MAIN_NODE_ID.ToString(), taskPostActionInput.FILE_NAME, taskPostActionInput.FILE_PATH);

                if (ResultCount > 0)
                {
                    var objFileDetails = new TaskPostActionOutput()
                    {

                        FILE_PATH = "Attachments\\" + taskPostActionInput.TASK_MAIN_NODE_ID + "\\" + DateTime.Now.Day + "_" + DateTime.Now.ToShortTimeString().Replace(":", "_") + "_" + taskPostActionInput.FILE_NAME,
                        FILE_NAME = taskPostActionInput.FILE_NAME
                    };

                    var response = new TaskPostActionAPIOutPut_List
                    {
                        Status = "Ok",
                        Message = "File attach successfuly!!!",
                        Data = objFileDetails
                    };
                    return Ok(response);
                }
                else
                {
                    var response = new TaskPostActionAPIOutPut_List
                    {
                        Status = "Error",
                        Message = "Please attach the file!!!",
                        Data = null
                    };
                    return Ok(response);

                }
            }
            catch (Exception ex)
            {
                var response = new TaskPostActionAPIOutPut_List
                {
                    Status = "Error",
                    Message = ex.Message,
                    Data = null
                };
                return Ok(response);
            }
        }

        [HttpPost("Task-Management/Get-Task-Compliance")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<TASK_COMPLIANCE_list>>> GetTaskCompliance(TASK_COMPLIANCE_INPUT tASK_COMPLIANCE_INPUT)
        {
            bool FlagError = false;
            string ErrorMessage = string.Empty;
            try
            {
                if (tASK_COMPLIANCE_INPUT == null)
                {
                    var response = new TASK_COMPLIANCE_list
                    {
                        STATUS = "Error",
                        MESSAGE = "Please Enter the details",
                        DATA = null
                    };
                    return Ok(response);
                }
                if (tASK_COMPLIANCE_INPUT.PROPERTY_MKEY == null || tASK_COMPLIANCE_INPUT.PROPERTY_MKEY == 0)
                {
                    FlagError = true;
                    ErrorMessage = ErrorMessage + "Property Mkey is required,";
                }
                if (tASK_COMPLIANCE_INPUT.BUILDING_MKEY == null || tASK_COMPLIANCE_INPUT.BUILDING_MKEY == 0)
                {
                    FlagError = true;
                    ErrorMessage = ErrorMessage + "Building Mkey is required,";
                }

                if (tASK_COMPLIANCE_INPUT.USER_ID == null || tASK_COMPLIANCE_INPUT.USER_ID == 0)
                {
                    FlagError = true;
                    ErrorMessage = ErrorMessage + "User ID is required,";
                }

                if (FlagError == true)
                {
                    var response = new ComplianceOutput_LIST
                    {
                        STATUS = "Error",
                        MESSAGE = ErrorMessage,
                        DATA = null
                    };
                    return Ok(response);
                }

                var RsponseStatus = await _repository.GetTaskComplianceAsync(tASK_COMPLIANCE_INPUT);
                return RsponseStatus;
            }
            catch (Exception ex)
            {
                var response = new ComplianceOutput_LIST
                {
                    STATUS = "Error",
                    MESSAGE = ex.Message,
                    DATA = null
                };
                return Ok(response);
            }
        }

        [HttpPost("Task-Management/Get-Task-EndList")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<TASK_COMPLIANCE_END_CHECK_LIST>>> GetTaskEndList(TASK_COMPLIANCE_INPUT tASK_COMPLIANCE_INPUT)
        {
            bool FlagError = false;
            string ErrorMessage = string.Empty;
            try
            {
                if (tASK_COMPLIANCE_INPUT == null)
                {
                    var response = new TASK_COMPLIANCE_list
                    {
                        STATUS = "Error",
                        MESSAGE = "Please Enter the details",
                        DATA = null
                    };
                    return Ok(response);
                }
                if (tASK_COMPLIANCE_INPUT.PROPERTY_MKEY == null || tASK_COMPLIANCE_INPUT.PROPERTY_MKEY == 0)
                {
                    FlagError = true;
                    ErrorMessage = ErrorMessage + "Property Mkey is required,";
                }
                if (tASK_COMPLIANCE_INPUT.BUILDING_MKEY == null || tASK_COMPLIANCE_INPUT.BUILDING_MKEY == 0)
                {
                    FlagError = true;
                    ErrorMessage = ErrorMessage + "Building Mkey is required,";
                }

                if (tASK_COMPLIANCE_INPUT.USER_ID == null || tASK_COMPLIANCE_INPUT.USER_ID == 0)
                {
                    FlagError = true;
                    ErrorMessage = ErrorMessage + "User ID is required,";
                }

                if (FlagError == true)
                {
                    var response = new TASK_COMPLIANCE_END_CHECK_LIST
                    {
                        STATUS = "Error",
                        MESSAGE = ErrorMessage,
                        DATA = null
                    };
                    return Ok(response);
                }

                var RsponseStatus = await _repository.GetTaskEndListAsync(tASK_COMPLIANCE_INPUT);
                return RsponseStatus;
            }
            catch (Exception ex)
            {
                var response = new TASK_COMPLIANCE_END_CHECK_LIST
                {
                    STATUS = "Error",
                    MESSAGE = ex.Message,
                    DATA = null
                };
                return Ok(response);
            }
        }

        [HttpPost("Task-Management/Get-Task-EndList-Details")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<TASK_ENDLIST_DETAILS_OUTPUT_LIST>>> GetTaskEndListDetails(TASK_END_LIST_DETAILS tASK_END_LIST_DETAILS)
        {
            bool FlagError = false;
            string ErrorMessage = string.Empty;
            try
            {
                if (tASK_END_LIST_DETAILS == null)
                {
                    var response = new TASK_COMPLIANCE_list
                    {
                        STATUS = "Error",
                        MESSAGE = "Please Enter the details",
                        DATA = null
                    };
                    return Ok(response);
                }
                if (tASK_END_LIST_DETAILS.PROPERTY_MKEY == null || tASK_END_LIST_DETAILS.PROPERTY_MKEY == 0)
                {
                    FlagError = true;
                    ErrorMessage = ErrorMessage + "Property Mkey is required,";
                }
                if (tASK_END_LIST_DETAILS.BUILDING_MKEY == null || tASK_END_LIST_DETAILS.BUILDING_MKEY == 0)
                {
                    FlagError = true;
                    ErrorMessage = ErrorMessage + "Building Mkey is required,";
                }

                if (tASK_END_LIST_DETAILS.USER_ID == null || tASK_END_LIST_DETAILS.USER_ID == 0)
                {
                    FlagError = true;
                    ErrorMessage = ErrorMessage + "User ID is required,";
                }

                if (FlagError == true)
                {
                    var response = new TASK_ENDLIST_DETAILS_OUTPUT_LIST
                    {
                        STATUS = "Error",
                        MESSAGE = ErrorMessage,
                        DATA = null
                    };
                    return Ok(response);
                }

                var RsponseStatus = await _repository.GetTaskEndListDetailsAsync(tASK_END_LIST_DETAILS);
                return RsponseStatus;
            }
            catch (Exception ex)
            {
                var response = new TASK_ENDLIST_DETAILS_OUTPUT_LIST
                {
                    STATUS = "Error",
                    MESSAGE = ex.Message,
                    DATA = null
                };
                return Ok(response);
            }
        }

        [HttpPost("Task-Management/Get-Task-CheckList")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<TASK_COMPLIANCE_CHECK_LIST>>> GetTaskCheckList(TASK_COMPLIANCE_INPUT tASK_COMPLIANCE_INPUT)
        {
            bool FlagError = false;
            string ErrorMessage = string.Empty;
            try
            {
                if (tASK_COMPLIANCE_INPUT == null)
                {
                    var response = new TASK_COMPLIANCE_list
                    {
                        STATUS = "Error",
                        MESSAGE = "Please Enter the details",
                        DATA = null
                    };
                    return Ok(response);
                }
                if (tASK_COMPLIANCE_INPUT.PROPERTY_MKEY == null || tASK_COMPLIANCE_INPUT.PROPERTY_MKEY == 0)
                {
                    FlagError = true;
                    ErrorMessage = ErrorMessage + "Property Mkey is required,";
                }
                if (tASK_COMPLIANCE_INPUT.BUILDING_MKEY == null || tASK_COMPLIANCE_INPUT.BUILDING_MKEY == 0)
                {
                    FlagError = true;
                    ErrorMessage = ErrorMessage + "Building Mkey is required,";
                }

                if (tASK_COMPLIANCE_INPUT.USER_ID == null || tASK_COMPLIANCE_INPUT.USER_ID == 0)
                {
                    FlagError = true;
                    ErrorMessage = ErrorMessage + "User ID is required,";
                }

                if (FlagError == true)
                {
                    var response = new TASK_COMPLIANCE_CHECK_LIST
                    {
                        STATUS = "Error",
                        MESSAGE = ErrorMessage,
                        DATA = null
                    };
                    return Ok(response);
                }

                var RsponseStatus = await _repository.GetTaskCheckListAsync(tASK_COMPLIANCE_INPUT);
                return RsponseStatus;
            }
            catch (Exception ex)
            {
                var response = new TASK_COMPLIANCE_CHECK_LIST
                {
                    STATUS = "Error",
                    MESSAGE = ex.Message,
                    DATA = null
                };
                return Ok(response);
            }
        }

        [HttpPost("Task-Management/Get-Task-Sanctioning-Authority")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<TaskSanctioningDepartmentOutputList>>> GetTaskSanctioningAuthority(TASK_COMPLIANCE_INPUT tASK_COMPLIANCE_INPUT)
        {
            bool FlagError = false;
            string ErrorMessage = string.Empty;
            try
            {
                if (tASK_COMPLIANCE_INPUT == null)
                {
                    var response = new TaskSanctioningDepartmentOutputList
                    {
                        STATUS = "Error",
                        MESSAGE = "Please Enter the details",
                        DATA = null
                    };
                    return Ok(response);
                }
                if (tASK_COMPLIANCE_INPUT.PROPERTY_MKEY == null || tASK_COMPLIANCE_INPUT.PROPERTY_MKEY == 0)
                {
                    FlagError = true;
                    ErrorMessage = ErrorMessage + "Property Mkey is required,";
                }
                if (tASK_COMPLIANCE_INPUT.BUILDING_MKEY == null || tASK_COMPLIANCE_INPUT.BUILDING_MKEY == 0)
                {
                    FlagError = true;
                    ErrorMessage = ErrorMessage + "Building Mkey is required,";
                }

                if (tASK_COMPLIANCE_INPUT.USER_ID == null || tASK_COMPLIANCE_INPUT.USER_ID == 0)
                {
                    FlagError = true;
                    ErrorMessage = ErrorMessage + "User ID is required,";
                }

                if (FlagError == true)
                {
                    var response = new TaskSanctioningDepartmentOutputList
                    {
                        STATUS = "Error",
                        MESSAGE = ErrorMessage,
                        DATA = null
                    };
                    return Ok(response);
                }

                var RsponseStatus = await _repository.GetTaskSanctioningAuthorityAsync(tASK_COMPLIANCE_INPUT);
                return RsponseStatus;
            }
            catch (Exception ex)
            {
                var response = new TaskSanctioningDepartmentOutputList
                {
                    STATUS = "Error",
                    MESSAGE = ex.Message,
                    DATA = null
                };
                return Ok(response);
            }
        }

        [HttpPost("Task-Management/Task-Output-Doc-Insert-Update")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<TASK_ENDLIST_DETAILS_OUTPUT_LIST>>> PostTaskOutputDocInsertUpdate([FromForm] TASK_ENDLIST_INPUT tASK_ENDLIST_INPUT)
        {
            bool FlagError = false;
            string ErrorMessage = string.Empty;
            try
            {
                if (tASK_ENDLIST_INPUT == null)
                {
                    var response = new TASK_ENDLIST_DETAILS_OUTPUT_LIST
                    {
                        STATUS = "Error",
                        MESSAGE = "Please Enter the details",
                        DATA = null
                    };
                    return Ok(response);
                }
                if (tASK_ENDLIST_INPUT.PROPERTY_MKEY == null || tASK_ENDLIST_INPUT.PROPERTY_MKEY == 0)
                {
                    FlagError = true;
                    ErrorMessage = ErrorMessage + "Property Mkey is required,";
                }
                if (tASK_ENDLIST_INPUT.BUILDING_MKEY == null || tASK_ENDLIST_INPUT.BUILDING_MKEY == 0)
                {
                    FlagError = true;
                    ErrorMessage = ErrorMessage + "Building Mkey is required,";
                }

                if (tASK_ENDLIST_INPUT.CREATED_BY == null || tASK_ENDLIST_INPUT.CREATED_BY == "0")
                {
                    FlagError = true;
                    ErrorMessage = ErrorMessage + "Created BY is required,";
                }

                if (FlagError == true)
                {
                    var response = new TASK_COMPLIANCE_CHECK_LIST
                    {
                        STATUS = "Error",
                        MESSAGE = ErrorMessage,
                        DATA = null
                    };
                    return Ok(response);
                }

                var RsponseStatus = await _repository.PostTaskEndListInsertUpdateAsync(tASK_ENDLIST_INPUT);
                return RsponseStatus;
            }
            catch (Exception ex)
            {
                var response = new TASK_ENDLIST_DETAILS_OUTPUT_LIST
                {
                    STATUS = "Error",
                    MESSAGE = ex.Message,
                    DATA = null
                };
                return Ok(response);
            }
        }

        [HttpPost("Task-Management/Task-CheckList-Doc-Insert-Update")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<TASK_COMPLIANCE_CHECK_LIST>>> PostTaskCheckListDocInsertUpdate(TASK_CHECKLIST_INPUT tASK_CHECKLIST_INPUT)
        {
            bool FlagError = false;
            string ErrorMessage = string.Empty;
            try
            {
                if (tASK_CHECKLIST_INPUT == null)
                {
                    var response = new TASK_COMPLIANCE_CHECK_LIST
                    {
                        STATUS = "Error",
                        MESSAGE = "Please Enter the details",
                        DATA = null
                    };
                    return Ok(response);
                }
                if (tASK_CHECKLIST_INPUT.PROPERTY_MKEY == null || tASK_CHECKLIST_INPUT.PROPERTY_MKEY == 0)
                {
                    FlagError = true;
                    ErrorMessage = ErrorMessage + "Property Mkey is required,";
                }
                if (tASK_CHECKLIST_INPUT.BUILDING_MKEY == null || tASK_CHECKLIST_INPUT.BUILDING_MKEY == 0)
                {
                    FlagError = true;
                    ErrorMessage = ErrorMessage + "Building Mkey is required,";
                }

                if (tASK_CHECKLIST_INPUT.CREATED_BY == null || tASK_CHECKLIST_INPUT.CREATED_BY == "0")
                {
                    FlagError = true;
                    ErrorMessage = ErrorMessage + "Created BY is required,";
                }

                if (FlagError == true)
                {
                    var response = new TASK_COMPLIANCE_CHECK_LIST
                    {
                        STATUS = "Error",
                        MESSAGE = ErrorMessage,
                        DATA = null
                    };
                    return Ok(response);
                }

                var RsponseStatus = await _repository.PostTaskCheckListInsertUpdateAsync(tASK_CHECKLIST_INPUT);
                return RsponseStatus;
            }
            catch (Exception ex)
            {
                var response = new TASK_COMPLIANCE_CHECK_LIST
                {
                    STATUS = "Error",
                    MESSAGE = ex.Message,
                    DATA = null
                };
                return Ok(response);
            }
        }

        [HttpPost("Task-Management/Task-Sanctioning-Authority-Insert-Update")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<TaskSanctioningDepartmentOutputList>>> PostSanctioningAuthority(TASK_SANCTIONING_AUTHORITY_INPUT tASK_SANCTIONING_AUTHORITY_INPUT)
        {
            bool FlagError = false;
            string ErrorMessage = string.Empty;
            try
            {
                if (tASK_SANCTIONING_AUTHORITY_INPUT == null)
                {
                    var response = new TASK_COMPLIANCE_CHECK_LIST
                    {
                        STATUS = "Error",
                        MESSAGE = "Please Enter the details",
                        DATA = null
                    };
                    return Ok(response);
                }
                if (tASK_SANCTIONING_AUTHORITY_INPUT.PROPERTY_MKEY == null || tASK_SANCTIONING_AUTHORITY_INPUT.PROPERTY_MKEY == 0)
                {
                    FlagError = true;
                    ErrorMessage = ErrorMessage + "Property Mkey is required,";
                }
                if (tASK_SANCTIONING_AUTHORITY_INPUT.BUILDING_MKEY == null || tASK_SANCTIONING_AUTHORITY_INPUT.BUILDING_MKEY == 0)
                {
                    FlagError = true;
                    ErrorMessage = ErrorMessage + "Building Mkey is required,";
                }

                if (tASK_SANCTIONING_AUTHORITY_INPUT.CREATED_BY == null || tASK_SANCTIONING_AUTHORITY_INPUT.CREATED_BY == 0)
                {
                    FlagError = true;
                    ErrorMessage = ErrorMessage + "Created BY is required,";
                }

                if (FlagError == true)
                {
                    var response = new TASK_COMPLIANCE_CHECK_LIST
                    {
                        STATUS = "Error",
                        MESSAGE = ErrorMessage,
                        DATA = null
                    };
                    return Ok(response);
                }

                var RsponseStatus = await _repository.PostTaskSanctioningAuthorityAsync(tASK_SANCTIONING_AUTHORITY_INPUT);
                return RsponseStatus;
            }
            catch (Exception ex)
            {
                var response = new TASK_COMPLIANCE_CHECK_LIST
                {
                    STATUS = "Error",
                    MESSAGE = ex.Message,
                    DATA = null
                };
                return Ok(response);
            }
        }

        [HttpPost("Task-Management/Task-CheckList-Table-Insert-Update")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<TaskCheckListOutputList>>> PostTaskCheckListTableInsertUpdate(TASK_CHECKLIST_TABLE_INPUT tASK_CHECKLIST_TABLE_INPUT )
        {
            bool FlagError = false;
            string ErrorMessage = string.Empty;
            try
            {
                if (tASK_CHECKLIST_TABLE_INPUT == null)
                {
                    var response = new TaskCheckListOutputList
                    {
                        STATUS = "Error",
                        MESSAGE = "Please Enter the details",
                        DATA = null
                    };
                    return Ok(response);
                }

                var RsponseStatus = await _repository.PostTaskCheckListTableInsertUpdateAsync(tASK_CHECKLIST_TABLE_INPUT);
                return RsponseStatus;
            }
            catch (Exception ex)
            {
                var response = new TaskCheckListOutputList
                {
                    STATUS = "Error",
                    MESSAGE = ex.Message,
                    DATA = null
                };
                return Ok(response);
            }
        }

        [HttpPost("Task-Management/Task-Output-Table-Insert-Update")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<TASK_COMPLIANCE_END_CHECK_LIST>>> PostTaskOutputTableInsertUpdate(TASK_ENDLIST_TABLE_INPUT tASK_ENDLIST_TABLE_INPUT)
        {
            try
            {
                if (tASK_ENDLIST_TABLE_INPUT == null)
                {
                    var response = new TASK_COMPLIANCE_END_CHECK_LIST
                    {
                        STATUS = "Error",
                        MESSAGE = "Please Enter the details",
                        DATA = null
                    };
                    return Ok(response);
                }

                var RsponseStatus = await _repository.PostTaskEndListTableInsertUpdateAsync(tASK_ENDLIST_TABLE_INPUT);
                return RsponseStatus;
            }
            catch (Exception ex)
            {
                var response = new TASK_COMPLIANCE_END_CHECK_LIST
                {
                    STATUS = "Error",
                    MESSAGE = ex.Message,
                    DATA = null
                };
                return Ok(response);
            }
        }

        [HttpPost("Task-Management/Task-Sanctioning-Table-Insert-Update")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<TaskSanctioningDepartmentOutputList>>> PostTaskSanctioningTableInsertUpdate(TASK_SANCTIONING_INPUT tASK_SANCTIONING_INPUT)
        {
            try
            {
                if (tASK_SANCTIONING_INPUT == null)
                {
                    var response = new TaskSanctioningDepartmentOutputList
                    {
                        STATUS = "Error",
                        MESSAGE = "Please Enter the details",
                        DATA = null
                    };
                    return Ok(response);
                }

                var RsponseStatus = await _repository.PostTaskSanctioningTableInsertUpdateAsync(tASK_SANCTIONING_INPUT);
                return RsponseStatus;
            }
            catch (Exception ex)
            {
                var response = new TaskSanctioningDepartmentOutputList
                {
                    STATUS = "Error",
                    MESSAGE = ex.Message,
                    DATA = null
                };
                return Ok(response);
            }
        }

    }
}
