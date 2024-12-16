using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.API.Interfaces;
using TaskManagement.API.Model;

namespace TaskManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectDefinationController : ControllerBase
    {
        private readonly IProjectDefination _repository;
        public ProjectDefinationController(IProjectDefination repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<PROJECT_HDR>>> GetAllProjectDefination(int LoggedIN, string FormName, string MethodName)
        {
            try
            {
                var Task = await _repository.GetAllProjectDefinationAsync(LoggedIN, FormName, MethodName);
                if (Task != null)
                {
                    return Ok(Task);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception)
            {
                return new List<PROJECT_HDR>();
            }
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<PROJECT_HDR>>> GetProjectDefination(int id, int LoggedIN, string FormName, string MethodName)
        {
            try
            {
                var Task = await _repository.GetProjectDefinationByIdAsync(id, LoggedIN, FormName, MethodName);
                if (Task != null)
                {
                    return Ok(Task);
                }
                else
                {
                    return NotFound();
                }

            }
            catch (Exception)
            {
                return new List<PROJECT_HDR>();
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<PROJECT_HDR>> CreateProjectDefination(PROJECT_HDR pROJECT_HDR)
        {
            try
            {
                if (pROJECT_HDR.PROJECT_ABBR == null)
                {
                    return StatusCode(400, "Please insert details of " + pROJECT_HDR.PROJECT_ABBR);
                }
                if (pROJECT_HDR.PROPERTY == null)
                {
                    return StatusCode(400, "Please insert details of  " + pROJECT_HDR.PROPERTY);
                }
                if (pROJECT_HDR.BUILDING_CLASSIFICATION == null)
                {
                    return StatusCode(400, "Please insert details of  " + pROJECT_HDR.BUILDING_CLASSIFICATION);
                }
                if (pROJECT_HDR.PROJECT_NAME == null)
                {
                    return StatusCode(400, "Please insert details of  " + pROJECT_HDR.PROJECT_NAME);
                }

                if (pROJECT_HDR.BUILDING_STANDARD == null)
                {
                    return StatusCode(400, "Please insert details of " + pROJECT_HDR.BUILDING_STANDARD);
                }
                if (pROJECT_HDR.STATUTORY_AUTHORITY == null)
                {
                    return StatusCode(400, "Please insert details of  " + pROJECT_HDR.STATUTORY_AUTHORITY);
                }

                foreach (var subtaskdetails in pROJECT_HDR.APPROVALS_ABBR_LIST)
                {
                    if (subtaskdetails.TENTATIVE_START_DATE == null)
                    {
                        return StatusCode(400, "Please insert details of  " + subtaskdetails.TENTATIVE_START_DATE);
                    }
                    if (subtaskdetails.TENTATIVE_END_DATE == null)
                    {
                        return StatusCode(400, "Please insert details of  " + subtaskdetails.TENTATIVE_END_DATE);
                    }

                    if (subtaskdetails.DAYS_REQUIRED == null)
                    {
                        return StatusCode(400, "Please insert details of  " + subtaskdetails.DAYS_REQUIRED);
                    }
                }
                var model = await _repository.CreateProjectDefinationAsync(pROJECT_HDR);
                if (model == null)
                {
                    return StatusCode(500);
                }
                else
                {
                    return model;
                }
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.Message);
            }
        }

        [HttpPut("ProjectDefination/Update-Project-Defination")]
        [Authorize]
        public async Task<IActionResult> UpdateProjectDefination(int MKEY, PROJECT_HDR pROJECT_HDR)
        {
            try
            {
                var ProjectDetails = await _repository.GetProjectDefinationByIdAsync(MKEY, pROJECT_HDR.LAST_UPDATED_BY, pROJECT_HDR.ATTRIBUTE2, pROJECT_HDR.ATTRIBUTE3);

                if (ProjectDetails == null)
                {
                    var ErrorDoc = new PROJECT_HDR();
                    var response = new ApiResponse<PROJECT_HDR>
                    {
                        Status = "Error",
                        Message = "Error occured",
                        Data = ErrorDoc // No data in case of exception
                    };
                    return Ok(response);
                }
                if (MKEY != pROJECT_HDR.MKEY)
                {
                    var ErrorDoc = new PROJECT_HDR();
                    var response = new ApiResponse<PROJECT_HDR>
                    {
                        Status = "Error",
                        Message = "Not found",
                        Data = ErrorDoc // No data in case of exception
                    };
                    return Ok(response);
                }

                var UpadateProjectDefiniation = await _repository.UpdateProjectDefinationAsync(pROJECT_HDR);
                if (UpadateProjectDefiniation != null)
                {
                    ProjectDetails = null;
                    ProjectDetails = await _repository.GetProjectDefinationByIdAsync(MKEY, pROJECT_HDR.LAST_UPDATED_BY, pROJECT_HDR.ATTRIBUTE2, pROJECT_HDR.ATTRIBUTE3);
                    if (ProjectDetails == null)
                    {
                        var ErrorDoc = new PROJECT_HDR();
                        var response = new ApiResponse<PROJECT_HDR>
                        {
                            Status = "Error",
                            Message = "Error occured",
                            Data = ErrorDoc // No data in case of exception
                        };
                        return Ok(response);
                    }
                    else
                    {
                        var response = new ApiResponse<PROJECT_HDR>
                        {
                            Status = "Ok",
                            Message = "Data Updated",
                            Data = ProjectDetails // No data in case of exception
                        };
                        return Ok(response);
                    }
                }
                else
                {
                    var ErrorDoc = new PROJECT_HDR();
                    var response = new ApiResponse<PROJECT_HDR>
                    {
                        Status = "Error",
                        Message = "Not found",
                        Data = ErrorDoc // No data in case of exception
                    };
                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
                var ErrorDoc = new PROJECT_HDR();
                var response = new ApiResponse<PROJECT_HDR>
                {
                    Status = "Error",
                    Message = ex.Message,
                    Data = ErrorDoc // No data in case of exception
                };
                return Ok(response);
            }
        }

        [HttpDelete("ProjectDefination/Delete-TASK")]
        [Authorize]
        public async Task<IActionResult> DeleteTASK(int id, int LastUpatedBy, string FormName, string MethodName)
        {
            try
            {
                var FindTask = await _repository.GetProjectDefinationByIdAsync(id, LastUpatedBy, FormName, MethodName);
                if (FindTask == null)
                {
                    var response = new ApiResponse<PROJECT_HDR>
                    {
                        Status = "Error",
                        Message = "Data not found",
                        Data = null // No data in case of exception
                    };
                    return Ok(response);
                }
                else
                {
                    bool deleteTask = await _repository.DeleteProjectDefinationAsync(id, LastUpatedBy, FormName, MethodName);
                    if (deleteTask)
                    {
                        var DeletedTask = await _repository.GetProjectDefinationByIdAsync(id, LastUpatedBy, FormName, MethodName);
                        if (DeletedTask == null)
                        {
                            var ErrorDoc = new PROJECT_HDR();
                            var response = new ApiResponse<PROJECT_HDR>
                            {
                                Status = "Ok",
                                Message = "Row Deleted",
                                Data = ErrorDoc // No data in case of exception
                            };
                            return Ok(response);
                        }
                    }
                    else
                    {
                        // Add return for failed deletion
                        var response = new ApiResponse<PROJECT_HDR>
                        {
                            Status = "Error",
                            Message = "An error occurred during deletion",
                            Data = null // No data in case of exception
                        };
                        return Ok(response); // Ensure that a response is returned
                    }
                }

                // In case of success, ensure that a response is returned
                var successResponse = new ApiResponse<PROJECT_HDR>
                {
                    Status = "Error",
                    Message = "Task deletion unsuccessful.",
                    Data = null
                };
                return Ok(successResponse);
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<PROJECT_HDR>
                {
                    Status = "Error",
                    Message = ex.Message,
                    Data = null // No data in case of exception
                };
                return Ok(response);
            }
        }

        [HttpGet("ProjectDefination/Get-Approval-Details")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<PROJECT_TRL_APPROVAL_ABBR_LIST>>> GetApprovalDetails(int LoggedInID, int BUILDING_TYPE, string BUILDING_STANDARD, string STATUTORY_AUTHORITY)
        {
            try
            {
                var Task = await _repository.GetApprovalDetails(LoggedInID, BUILDING_TYPE, BUILDING_STANDARD, STATUTORY_AUTHORITY);
                if (Task != null & Task.Count() > 0)
                {
                    return Ok(Task);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception)
            {
                return new List<PROJECT_TRL_APPROVAL_ABBR_LIST>();
            }
        }
    }
}
