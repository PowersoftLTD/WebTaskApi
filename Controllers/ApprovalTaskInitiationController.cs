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
    }
}
