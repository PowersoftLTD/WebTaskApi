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

namespace TaskManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CommonApiController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IProjectEmployee _repository;
        public IDapperDbConnection _dapperDbConnection;
        public CommonApiController(IProjectEmployee repository, IConfiguration configuration)
        {
            _repository = repository;
            _configuration = configuration;
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


        [HttpPost("Task-Management/TASK-DASHBOARD")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Task_DetailsOutPut_List>>> Task_Details([FromBody] Task_DetailsInput task_DetailsInput)
        {
            try
            {
                var TaskDash = await _repository.GetTaskDetailsAsync(task_DetailsInput.CURRENT_EMP_MKEY, task_DetailsInput.FILTER);
                //if (TaskDash == null)
                //{
                //    var responseApprovalTemplate = new ApiResponse<TASK_DASHBOARD>
                //    {
                //        Status = "Error",
                //        Message = "Error Occurd",
                //        Data = null
                //    };
                //    return Ok(responseApprovalTemplate);
                //}
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
        public async Task<ActionResult<IEnumerable<GET_ACTIONSOutPut_List>>> GET_ACTIONS([FromBody] GET_ACTIONSInput gET_ACTIONSInput)
        {
            try
            {
                var TaskAction = await _repository.GetActionsAsync(gET_ACTIONSInput.TASK_MKEY, gET_ACTIONSInput.CURRENT_EMP_MKEY, gET_ACTIONSInput.CURR_ACTION);
                //if (TaskAction == null)
                //{
                //    var responseTaskAction = new ApiResponse<GET_ACTIONSOutPut_List>
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
                var response = new GET_ACTIONSOutPut_List
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
                foreach(var checkerror in TaskTree)
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
                    Table =  null,
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
        public async Task<ActionResult<IEnumerable<TASK_DASHBOARD>>> Team_Task_Details([FromBody] Team_Task_DetailsInput team_Task_DetailsInput)
        {
            try
            {
                DataTable Temptable = new DataTable();
                var TaskTeamTask = await _repository.GetTeamTaskDetailsAsync(team_Task_DetailsInput.CURRENT_EMP_MKEY);

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

                var searchStr = team_Task_DetailsInput.TASKTYPE.ToString().Trim() + " " + team_Task_DetailsInput.TASKTYPE_DESC.ToString().Trim() + " " + team_Task_DetailsInput.mKEY;
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

         
        [HttpPost("Task-Management/FileUpload"), DisableRequestSizeLimit]
        [Authorize]
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

         
        [HttpPost("Task-Management/TASK-ACTION-TRL-Insert-Update"), DisableRequestSizeLimit]
        [Authorize]
        public async Task<IActionResult> Post_TASK_ACTION([FromForm] IFormCollection form)
        {
            try
            {
                string uploadFilePath = _configuration["UploadFile_Path"];
                var files = form.Files;
                string Mkey = form["Mkey"];
                string TASK_MKEY = form["TASK_MKEY"];
                string TASK_PARENT_ID = form["TASK_PARENT_ID"];
                string ACTION_TYPE = form["ACTION_TYPE"];
                string DESCRIPTION_COMMENT = form["DESCRIPTION_COMMENT"];
                string PROGRESS_PERC = form["PROGRESS_PERC"];
                string STATUS = form["STATUS"];
                string CREATED_BY = form["CREATED_BY"];
                string TASK_MAIN_NODE_ID = form["TASK_MAIN_NODE_ID"];


                int srNo = 0;
                var uploadedFiles = new List<string>();
                string fileName, filePath;

                if (files.Count > 0)
                {
                    foreach (var file in files)
                    {
                        srNo++;
                        fileName = file.FileName;
                        string fileDirectory = Path.Combine(uploadFilePath, TASK_MAIN_NODE_ID);

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

                        string fileRelativePath = Path.Combine(_configuration["Refer_UploadFile_Path"], TASK_MAIN_NODE_ID, newFileName);

                        if (file.Length > 0)
                        {
                            // Perform the database insertion or other operations
                            await _repository.GetPostTaskActionAsync(Mkey, TASK_MKEY, TASK_PARENT_ID, ACTION_TYPE, DESCRIPTION_COMMENT, PROGRESS_PERC, STATUS, CREATED_BY, TASK_MAIN_NODE_ID, fileName, filePath);
                        }
                    }

                    // Return the response
                    return Ok(new { Status = "Success", Files = uploadedFiles });  // You can return a custom response
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
