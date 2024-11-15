using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.API.Interfaces;
using TaskManagement.API.Model;
using TaskManagement.API.Repositories;
using Microsoft.AspNetCore.Authorization;

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
        public async Task<ActionResult<IEnumerable<APPROVAL_TEMPLATE_HDR>>> GetApprovalTemplate()
        {
            try
            {
                var Task = await _repository.GetAllApprovalTemplateAsync();
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
                return new List<APPROVAL_TEMPLATE_HDR>();
            }
        }
       
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<APPROVAL_TEMPLATE_HDR>> GetApprovalTemplate(int id)
        {
            var TASK = await _repository.GetApprovalTemplateByIdAsync(id);
            if (TASK == null)
            {
                return NotFound();
            }
            return Ok(TASK);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<APPROVAL_TEMPLATE_HDR>> CreateTASK([FromBody] APPROVAL_TEMPLATE_HDR aPPROVAL_TEMPLATE_HDR)
        {
            try
            {
                var model = await _repository.CreateApprovalTemplateAsync(aPPROVAL_TEMPLATE_HDR);
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
                return StatusCode(500, ex.Message);
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
                return BadRequest("Please enter the ABBR"); ;
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
    }
}
