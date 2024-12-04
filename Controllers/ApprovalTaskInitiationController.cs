using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.API.Model;
using Microsoft.AspNetCore.Authorization;
using TaskManagement.API.Interfaces;

namespace TaskManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ApprovalTaskInitiationController : ControllerBase
    {
        private readonly IApprovalTaskInitiation _repository;
        public ApprovalTaskInitiationController(IApprovalTaskInitiation repository)
        {
            _repository = repository;
        }

        [HttpGet("GetApprovalTemplate")]
        [Authorize]
        public async Task<ActionResult<APPROVAL_TEMPLATE_HDR>> GetApprovalTemplate(int MKEY, int APPROVAL_MKEY)
        {
            try
            {
                var TASK = await _repository.GetApprovalTemplateByIdAsync(MKEY, APPROVAL_MKEY);
                if (TASK == null)
                {
                    var responseApprovalTemplate = new ApiResponse<APPROVAL_TASK_INITIATION>
                    {
                        Status = "Error",
                        Message = "Not found",
                        Data = null
                    };

                    //responseApprovalTemplate.Data. = "APPROVAL_TEMPLATE_HDR";
                    return Ok(responseApprovalTemplate);
                }

                var response = new ApiResponse<APPROVAL_TASK_INITIATION>
                {
                    Status = "OK",
                    Message = "Approval Template details",
                    Data = TASK
                };
                return Ok(response);
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
        public async Task<ActionResult<APPROVAL_TASK_INITIATION>> CreateApprovalTASK([FromBody] APPROVAL_TASK_INITIATION aPPROVAL_TASK_INITIATION)
        {
            try
            {
                bool flagSeq_no = false;
                double IndexSeq_NO = 0.0;

                if (aPPROVAL_TASK_INITIATION.TASK_NO == null)
                {
                    var responseStatus = new ApiResponse<APPROVAL_TASK_INITIATION>
                    {
                        Status = "Error",
                        Message = "Please enter the details of Approval Task Initiation",
                        Data = aPPROVAL_TASK_INITIATION // No data in case of exception
                    };
                    return Ok(responseStatus);
                }
                if (aPPROVAL_TASK_INITIATION.COMPLITION_DATE == null)
                {
                    var responseStatus = new ApiResponse<APPROVAL_TASK_INITIATION>
                    {
                        Status = "Error",
                        Message = "Please enter the details of Approval Task Initiation",
                        Data = aPPROVAL_TASK_INITIATION // No data in case of exception
                    };
                    return Ok(responseStatus);
                }

                if (aPPROVAL_TASK_INITIATION.MAIN_ABBR == null)
                {
                    var responseStatus = new ApiResponse<APPROVAL_TASK_INITIATION>
                    {
                        Status = "Error",
                        Message = "Please enter the details of Approval Task Initiation",
                        Data = aPPROVAL_TASK_INITIATION // No data in case of exception
                    };
                    return Ok(responseStatus);
                }

                if (aPPROVAL_TASK_INITIATION.RESPOSIBLE_EMP_MKEY == null)
                {
                    var responseStatus = new ApiResponse<APPROVAL_TASK_INITIATION>
                    {
                        Status = "Error",
                        Message = "Please enter the details of Approval Task Initiation",
                        Data = aPPROVAL_TASK_INITIATION // No data in case of exception
                    };
                    return Ok(responseStatus);
                }

                else
                {
                    var model = await _repository.CreateTaskApprovalTemplateAsync(aPPROVAL_TASK_INITIATION);
                    if (model == null || model.ResponseStatus == "Error")
                    {
                        var responseStatus = new ApiResponse<APPROVAL_TASK_INITIATION>
                        {
                            Status = "Error",
                            Message = model.Message,
                            Data = aPPROVAL_TASK_INITIATION // No data in case of exception
                        };
                        return Ok(responseStatus);
                    }
                    var response = new ApiResponse<APPROVAL_TASK_INITIATION>
                    {
                        Status = "Ok",
                        Message = "Inserted Successfully",
                        Data = aPPROVAL_TASK_INITIATION // No data in case of exception
                    };
                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<APPROVAL_TASK_INITIATION>
                {
                    Status = "Error",
                    Message = ex.Message,
                    Data = aPPROVAL_TASK_INITIATION // No data in case of exception
                };
                return Ok(response);
            }
        }
    }
}
