using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.API.Interfaces;
using TaskManagement.API.Model;
using TaskManagement.API.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.AspNetCore.Http.HttpResults;
using Azure;
using Newtonsoft.Json;
using System.Dynamic;

namespace TaskManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApprovalTemplateController : ControllerBase
    {
        private readonly IApprovalTemplate _repository;
        public static IWebHostEnvironment _environment;
        public ApprovalTemplateController(IApprovalTemplate repository, IWebHostEnvironment environment)
        {
            _repository = repository;
            _environment = environment;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<OutPutApprovalTemplates>>> GetApprovalTemplate(int LoggedInID)
        {
            try
            {
                var Task = await _repository.GetAllApprovalTemplateAsync(LoggedInID);
                if (Task != null)
                {
                    //foreach (var ApprvalCount in Task)
                    //{
                    //    if (ApprvalCount == null)
                    //    {
                    //        var response = new ApiResponse<IEnumerable<OutPutApprovalTemplates>>
                    //        {
                    //            Status = "Error",
                    //            Message = "Not found",
                    //            Data = null
                    //        };
                    //    }
                    //}
                    return Ok(Task);

                    //var response = new ApiResponse<IEnumerable<APPROVAL_TEMPLATE_HDR>>
                    //{
                    //    Status = "OK",
                    //    Message = "Approval Template details retrieved successfully.",
                    //    Data = Task
                    //};
                }
                else
                {
                    return Ok("Not found");
                    //var response = new ApiResponse<IEnumerable<APPROVAL_TEMPLATE_HDR>>
                    //{
                    //    Status = "Error",
                    //    Message = "Not found",
                    //    Data = null
                    //};

                    //var responseDict = new Dictionary<string, object>
                    //{
                    //    { nameof(APPROVAL_TEMPLATE_HDR), response.Data }
                    //};

                    //return Ok(new
                    //{
                    //    response.Status,
                    //    response.Message,
                    //    Data = responseDict
                    //});

                }
            }
            catch (Exception ex)
            {
                //var response = new ApiResponse<IEnumerable<APPROVAL_TEMPLATE_HDR>>
                //{
                //    Status = "Error",
                //    Message = ex.Message,
                //    Data = null
                //};
                return Ok(ex.Message);
            }
        }

        [HttpGet("GetByID")]
        [Authorize]
        public async Task<ActionResult<OutPutApprovalTemplates>> GetApprovalTemplateID(int id, int LoggedIN)
        {
            try
            {
                var ApprovalBYID = await _repository.GetApprovalTemplateByIdAsync(id, LoggedIN);
                if (ApprovalBYID == null)
                {
                    return Ok(new
                    {
                        Status = "Ok",
                        Message = "Not found",
                        Data = ApprovalBYID
                    });
                }
                if (ApprovalBYID.Status != null)
                {
                    if (ApprovalBYID.Status.ToString().ToLower() != "Ok".ToString().ToLower())
                    {
                        return Ok(new
                        {
                            Status = "Error",
                            Message = "An error occurred",
                            Data = ApprovalBYID
                        });
                    }
                }

                return Ok(new
                {
                    Status = "Ok",
                    Message = "Data get successfully",
                    Data = ApprovalBYID
                });
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<IEnumerable<APPROVAL_TEMPLATE_HDR>>
                {
                    Status = "Error",
                    Message = ex.Message,
                    Data = null
                };
                return Ok(response);
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<OutPutApprovalTemplates>> CreateTASK([FromBody] InsertApprovalTemplates insertApprovalTemplates)
        {
            try
            {
                bool flagSeq_no = false;
                double IndexSeq_NO = 0.0;
                var model = await _repository.CreateApprovalTemplateAsync(insertApprovalTemplates);
                if (model == null)
                {
                    return StatusCode(400, "Faild to insert data");
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

        [HttpGet("GetCheckABBR")]
        [Authorize]
        public async Task<ActionResult<APPROVAL_TEMPLATE_HDR>> GetCheckABBR(string strABBR)
        {
            try
            {
                var result = await _repository.CheckABBRAsync(strABBR);
                if (result != null)
                {
                    return Ok(true);
                }
                else
                {
                    return Ok(false);
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Please enter the ABBR");
            }
        }

        [HttpGet("GetAbbrAndShortAbbr")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<APPROVAL_TEMPLATE_HDR>>> GetAbbrAndShortAbbr(string Building, string Standard, string Authority)
        {
            try
            {
                var result = await _repository.AbbrAndShortDescAsync(Building, Standard, Authority);
                if (result != null)
                {
                    return Ok(result);
                }
                else
                {
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, "Error in GetAbbrAndShortAbbr method");
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }

        [HttpPut("ApprovalTemplate/Update-ApprovalTemplate")]
        [Authorize]
        public async Task<ActionResult<OutPutApprovalTemplates>> UpdateApprovalTemplate(int MKEY, [FromBody] UpdateApprovalTemplates updateApprovalTemplates)
        {
            try
            {
                bool flagSeq_no = false;
                double IndexSeq_NO = 0.0;

                var ApprovaleTemplateDetails = _repository.GetApprovalTemplateByIdAsync(updateApprovalTemplates.MKEY, Convert.ToInt32(updateApprovalTemplates.CREATED_BY));
                if (ApprovaleTemplateDetails == null)
                {
                    var responseStatus = new OutPutApprovalTemplates
                    {
                        MKEY = 0
                    };
                    return Ok(responseStatus);
                }
                if (MKEY != ApprovaleTemplateDetails.Result.MKEY)
                {
                    var responseStatus = new OutPutApprovalTemplates
                    {
                        MKEY = 0
                    };
                    return Ok(responseStatus);
                }

                var model = await _repository.UpdateApprovalTemplateAsync(updateApprovalTemplates);
                if (model == null)
                {
                    var responseStatus = new OutPutApprovalTemplates
                    {
                        Status = "Error",
                        Message = "failed to update data"
                        // No data in case of exception
                    };
                    return Ok(responseStatus);
                }
                if (model == null)
                {
                    var responseStatus = new OutPutApprovalTemplates
                    {
                        Status = "Error",
                        Message = "Error occured",
                    };
                    return Ok(responseStatus);
                }
                else
                {
                    return Ok(model);
                }
            }
            catch (Exception ex)
            {
                // Handle other unexpected exceptions
                var responseStatus = new OutPutApprovalTemplates
                {
                    Status = "Error",
                    Message = "failed to update data"
                    // No data in case of exception
                };
                return Ok(responseStatus);
            }
        }

        [HttpDelete("ApprovalTemplate/Delete-Task")]
        [Authorize]
        public async Task<IActionResult> DeleteApprovalTemplate(int MKEY, int LAST_UPDATED_BY)
        {
            try
            {
                // Check if the template exists
                var ApprovaleTemplateDetails = await _repository.GetApprovalTemplateByIdAsync(MKEY, Convert.ToInt32(LAST_UPDATED_BY));
                if (ApprovaleTemplateDetails == null)
                {
                    var responseStatus = new ApiResponse<APPROVAL_TEMPLATE_HDR>
                    {
                        Status = "Error",
                        Message = "Mkey not found",
                        Data = null // No data in case of exception
                    };
                    return Ok(responseStatus);
                }

                // If MKEY doesn't match the found template's MKEY
                if (MKEY != ApprovaleTemplateDetails.MKEY)
                {
                    var responseStatus = new ApiResponse<APPROVAL_TEMPLATE_HDR>
                    {
                        Status = "Error",
                        Message = "Data not match",
                        Data = null // No data in case of exception
                    };
                    return Ok(responseStatus);
                }

                // Attempt to delete the approval template
                var Deletemodel = await _repository.DeleteApprovalTemplateAsync(MKEY, LAST_UPDATED_BY);
                if (Deletemodel)
                {
                    var responseStatus = new ApiResponse<APPROVAL_TEMPLATE_HDR>
                    {
                        Status = "Ok",
                        Message = "Row Deleted",
                        Data = null // No data in case of exception
                    };
                    return Ok(responseStatus); // Ensure returning response when deletion is successful
                }
                else
                {
                    var responseStatus = new ApiResponse<APPROVAL_TEMPLATE_HDR>
                    {
                        Status = "Error",
                        Message = "Error occurred",
                        Data = null // No data in case of exception
                    };
                    return Ok(responseStatus); // Ensure returning response in case of failure
                }
            }
            catch (Exception ex)
            {
                // Handle unexpected exceptions
                var responseStatus = new ApiResponse<APPROVAL_TEMPLATE_HDR>
                {
                    Status = "Error",
                    Message = ex.Message,
                    Data = null // No data in case of exception
                };
                return Ok(responseStatus);
            }
        }

    }
}
