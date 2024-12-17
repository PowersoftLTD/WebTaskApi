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

namespace TaskManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProjectEmployeeController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IProjectEmployee _repository;
        public IDapperDbConnection _dapperDbConnection;
        public ProjectEmployeeController(IProjectEmployee repository, IConfiguration configuration)
        {
            _repository = repository;
            _configuration = configuration;
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

        [HttpGet("Task-Management/Get-Sub_Project")]
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
        public async Task<ActionResult<IEnumerable<TASK_DASHBOARD>>> Task_Dashboard_Details(EmployeeCompanyMST employeeCompanyMST)
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

        [HttpGet("Task-Management/TeamTask")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<TASK_DASHBOARD>>> TeamTask(EmployeeCompanyMST employeeCompanyMST)
        {
            try
            {
                var TaskTeamTask = await _repository.GetTeamTaskAsync(employeeCompanyMST.CURRENT_EMP_MKEY);

                if (TaskTeamTask == null)
                {
                    var responseTeamTask = new ApiResponse<TASK_DASHBOARD>
                    {
                        Status = "Error",
                        Message = "Error Occurd",
                        Data = null
                    };
                    return Ok(responseTeamTask);
                }
                return Ok(TaskTeamTask);
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

        [HttpGet("Task-Management/Team_Task_Details")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<TASK_DASHBOARD>>> Team_Task_Details(TASK_DASHBOARD tASK_DASHBOARD)
        {
            try
            {
                DataTable Temptable = new DataTable();
                var TaskTeamTask = await _repository.GetTeamTaskDetailsAsync(tASK_DASHBOARD.CURRENT_EMP_MKEY);

                if (TaskTeamTask == null)
                {
                    var responseTeamTask = new ApiResponse<TASK_DASHBOARD>
                    {
                        Status = "Error",
                        Message = "Error Occurd",
                        Data = null
                    };
                    return Ok(responseTeamTask);
                }

                DataTable TeamTaskDT = new DataTable();

                TeamTaskDT.Columns.Add("TASKTYPE", typeof(string));
                TeamTaskDT.Columns.Add("TASKTYPE_DESC", typeof(string));
                TeamTaskDT.Columns.Add("CURRENT_EMP_MKEY", typeof(string)); // Adjust types as necessary

                // Populate DataTable with the List<TASK_DASHBOARD>
                foreach (var task in TaskTeamTask)
                {
                    DataRow row = TeamTaskDT.NewRow();
                    row["TASKTYPE"] = task.TASKTYPE;
                    row["TASKTYPE_DESC"] = task.TASKTYPE_DESC;
                    row["CURRENT_EMP_MKEY"] = task.CURRENT_EMP_MKEY;
                    TeamTaskDT.Rows.Add(row);
                }

                var searchStr = tASK_DASHBOARD.TASKTYPE.ToString().Trim() + " " + tASK_DASHBOARD.TASKTYPE_DESC.ToString().Trim() + " " + tASK_DASHBOARD.mKEY;
                var splitSearchString = searchStr.Split(' ');
                var columnNameStr = "TASKTYPE TASKTYPE_DESC CURRENT_EMP_MKEY";
                var splitcolumnNameStr = columnNameStr.Split(' ');
                var expression = new List<string>();
                DataTable table = new DataTable();

                int iCnt = 0;
                foreach (var searchElement in splitSearchString)
                {
                    expression.Add(
                        string.Format("[{0}] = '{1}'", splitcolumnNameStr[iCnt], searchElement));
                    iCnt++;
                }
                var searchExpressionString = string.Join(" and ", expression.ToArray());
                DataRow[] rows = TeamTaskDT.Select(searchExpressionString);

                Temptable = TeamTaskDT.Clone();
                for (int i = 0; i < rows.Length; i++)
                    Temptable.Rows.Add(rows[i].ItemArray);

                return Ok(Temptable);
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

        [HttpGet("Task-Management/Get_Project_Details")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<TASK_HDR>>> Get_Project_DetailsWithSubproject(TASK_HDR tASK_HDR)
        {
            try
            {
                var TaskTeamTask = await _repository.GetProjectDetailsWithSubProjectAsync(tASK_HDR.ProjectID, tASK_HDR.SubProjectID);

                if (TaskTeamTask == null)
                {
                    var responseTeamTask = new ApiResponse<TASK_HDR>
                    {
                        Status = "Error",
                        Message = "Error Occurd",
                        Data = null
                    };
                    return Ok(responseTeamTask);
                }
                return Ok(TaskTeamTask);
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<TASK_HDR>
                {
                    Status = "Error",
                    Message = ex.Message,
                    Data = null
                };
                return Ok(response);
            }
        }

        [HttpGet("Task-Management/GET-TASK_TREEExport")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<TASK_HDR>>> GET_TASK_TREEExport(TASK_HDR tASK_HDR)
        {
            try
            {
                DataTable dsTaskTree = new DataTable();
                var TaskTreeExport = await _repository.GetTaskTreeExportAsync(tASK_HDR.TASK_MKEY.ToString());

                if (TaskTreeExport == null)
                {
                    var responseTeamTask = new ApiResponse<TASK_HDR>
                    {
                        Status = "Error",
                        Message = "Error Occurd",
                        Data = null
                    };
                    return Ok(responseTeamTask);
                }

                dsTaskTree = ConvertToDataTable(TaskTreeExport);

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
                var response = new ApiResponse<TASK_HDR>
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

        [HttpGet("Task-Management/Get_ExportProject_DetailsWithSubproject")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<TASK_HDR>>> Get_ExportProject_DetailsWithSubproject(TASK_HDR tASK_HDR)
        {
            try
            {
                DataTable dsTaskTree = new DataTable();
                var ProjectTask = await _repository.GetProjectDetailsWithSubProjectAsync(tASK_HDR.ProjectID, tASK_HDR.SubProjectID);

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
                dsTaskTree = ConvertToDataTable(ProjectTask);

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
                var response = new ApiResponse<TASK_HDR>
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
        public async Task<ActionResult<TASK_HDR>> Add_Task([FromBody] TASK_HDR tASK_HDR)
        {
            try
            {
                var modelTask = await _repository.CreateAddTaskAsync(tASK_HDR);
                if (modelTask == null)
                {
                    var responseStatus = new ApiResponse<TASK_HDR>
                    {
                        Status = "Error",
                        Message = "Error Occurd",
                        Data = null // No data in case of exception
                    };
                    return Ok(responseStatus);
                }
                var response = new ApiResponse<TASK_HDR>
                {
                    Status = "Ok",
                    Message = "Inserted Successfully",
                    Data = modelTask // No data in case of exception
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<TASK_HDR>
                {
                    Status = "Error",
                    Message = ex.Message,
                    Data = null
                };
                return Ok(response);
            }
        }

        [HttpGet("Task-Management/Add-Sub-Task")]
        [Authorize]
        public async Task<ActionResult<TASK_HDR>> Add_Sub_Task([FromBody] TASK_HDR tASK_HDR)
        {
            //(string TASK_NO, string TASK_NAME, string TASK_DESCRIPTION, string CATEGORY, string PROJECT_ID, string SUBPROJECT_ID, string COMPLETION_DATE, string ASSIGNED_TO, string TAGS, string ISNODE, string START_DATE, string CLOSE_DATE, string DUE_DATE, string TASK_PARENT_ID, string TASK_PARENT_NODE_ID, string TASK_PARENT_NUMBER, string STATUS, string STATUS_PERC, string TASK_CREATED_BY, string APPROVER_ID, string IS_ARCHIVE, string ATTRIBUTE1, string ATTRIBUTE2, string ATTRIBUTE3, string ATTRIBUTE4, string ATTRIBUTE5, string CREATED_BY, string CREATION_DATE, string LAST_UPDATED_BY, string APPROVE_ACTION_DATE, string Current_task_mkey)
            try
            {
                var modelTask = await _repository.CreateAddSubTaskAsync(tASK_HDR);
                if (modelTask == null)
                {
                    var responseStatus = new ApiResponse<TASK_HDR>
                    {
                        Status = "Error",
                        Message = "Error Occurd",
                        Data = null // No data in case of exception
                    };
                    return Ok(responseStatus);
                }
                var response = new ApiResponse<TASK_HDR>
                {
                    Status = "Ok",
                    Message = "Inserted Successfully",
                    Data = modelTask // No data in case of exception
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<TASK_HDR>
                {
                    Status = "Error",
                    Message = ex.Message,
                    Data = null
                };
                return Ok(response);
            }
        }


        [Authorize]
        [HttpPost("Task-Management/FileUpload"), DisableRequestSizeLimit]
        public async Task<IActionResult> Post([FromForm] IFormCollection form)
        {
            try
            {
                string uploadFilePath = _configuration["UploadFile_Path"];
                var files = form.Files;
                string taskMkey = form["Mkey"];
                string createdBy = form["CREATED_BY"];
                string deleteFlag = form["DELETE_FLAG"];
                string taskParentId = form["TASK_PARENT_ID"];
                string taskMainNodeId = form["TASK_MAIN_NODE_ID"];
                int srNo = 0;
                var uploadedFiles = new List<string>();
                string fileName, filePath;

                if (files.Count > 0)
                {
                    foreach (var file in files)
                    {
                        srNo++;
                        fileName = file.FileName;
                        string fileDirectory = Path.Combine(uploadFilePath, taskMainNodeId);

                        if (!Directory.Exists(fileDirectory))
                        {
                            Directory.CreateDirectory(fileDirectory);
                        }

                        // Generate new file name with timestamp
                        string newFileName = DateTime.Now.Day + "_" + DateTime.Now.ToShortTimeString().Replace(":", "_") + "_" + fileName;
                        filePath = Path.Combine(fileDirectory, newFileName);

                        // Save the file
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(fileStream);
                        }

                        uploadedFiles.Add(filePath);

                        string fileRelativePath = Path.Combine(_configuration["Refer_UploadFile_Path"], taskMainNodeId, newFileName);

                        if (file.Length > 0)
                        {
                            // Perform the database insertion or other operations
                            await _repository.TASKFileUpoadAsync(srNo.ToString(), taskMkey, taskParentId, fileName, filePath, createdBy, deleteFlag, taskMainNodeId);
                        }
                    }

                    // Return the response
                    return Ok(new { Status = "Success", Files = uploadedFiles });  // You can return a custom response
                }
                else
                {
                    if (taskMkey != "0000")
                    {
                        await _repository.UpdateTASKFileUpoadAsync(taskMkey, deleteFlag);
                    }
                }

                return BadRequest("No files were uploaded.");
            }
            catch (Exception ex)
            {
                // Handle exception (could be logging or returning an error response)
                return StatusCode(400, new { Message = "An error occurred", Error = ex.Message });
            }
        }

    }
}
