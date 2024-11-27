using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.API.Interfaces;
using TaskManagement.API.Model;
using Microsoft.AspNetCore.Authorization;

namespace TaskManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProjectDocumentDepositoryController : ControllerBase
    {
        private readonly IProjectDocDepository _repository;
        public ProjectDocumentDepositoryController(IProjectDocDepository repository)
        {
            _repository = repository;
        }

        [HttpGet("Get-Project-Document-Depsitory")]
        [Authorize]
        public async Task<IEnumerable<dynamic>> GetAllProjDocDepsitory(string? ATTRIBUTE1, string? ATTRIBUTE2, string? ATTRIBUTE3)
        {
            var ProjectDocDeository = await _repository.GetAllProjectDocDeositoryAsync(ATTRIBUTE1, ATTRIBUTE2, ATTRIBUTE3);
            if (ProjectDocDeository == null)
            {
                return null;
            }
            return ProjectDocDeository;
        }

        [HttpGet("Get-Project-Document-Depsitory-BY-ID")]
        [Authorize]
        public async Task<ActionResult<dynamic>> GetAllProjDocDepsitoryByID(int? MKEY, string? ATTRIBUTE1, string? ATTRIBUTE2, string? ATTRIBUTE3)
        {
            var ProjectDocDeository = await _repository.GetProjectDocDeositoryByIDAsync(MKEY, ATTRIBUTE1, ATTRIBUTE2, ATTRIBUTE3);
            if (ProjectDocDeository == null)
            {
                return NotFound();
            }
            return Ok(ProjectDocDeository);
        }

        [HttpPost("Post-Project-Document-Depsitory")]
        [Authorize]
        public async Task<ActionResult<dynamic>> CreateProjDocDepsitory(PROJECT_DOC_DEPOSITORY_HDR pROJECT_DOC_DEPOSITORY_HDR) 
        {
            try
            {
                int DocDeository = await _repository.GetPROJECT_DEPOSITORY_DOCUMENTAsync(pROJECT_DOC_DEPOSITORY_HDR.BUILDING_TYPE, pROJECT_DOC_DEPOSITORY_HDR.PROPERTY_TYPE, pROJECT_DOC_DEPOSITORY_HDR.DOC_NAME);
                if (DocDeository == null || Convert.ToInt32(DocDeository) == 0)
                {
                    var ProjectDocDeository = await _repository.CreateProjectDocDeositoryAsync(pROJECT_DOC_DEPOSITORY_HDR); //(BUILDING_TYPE, PROPERTY_TYPE, DOC_NAME, DOC_NUMBER, DOC_DATE, DOC_ATTACHMENT, VALIDITY_DATE, CREATED_BY, ATTRIBUTE1, ATTRIBUTE2, ATTRIBUTE3);
                    if (ProjectDocDeository == null)
                    {
                        return NotFound();
                    }
                    return Ok(ProjectDocDeository);
                }
                else
                {
                    return Ok(true);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(("Failed to do stuff.", ex.Message));
            }

        }

        [HttpGet("Get-Document-Details")]
        [Authorize]
        public async Task<ActionResult<dynamic>> GetDocumentDetails(int? MKEY, string? ATTRIBUTE1, string? ATTRIBUTE2, string? ATTRIBUTE3)
        {
            var ProjectDocDeository = await _repository.GetDocumentDetailsAsync(MKEY, ATTRIBUTE1, ATTRIBUTE2, ATTRIBUTE3);
            if (ProjectDocDeository == null)
            {
                return NotFound();
            }
            return Ok(ProjectDocDeository);
        }

    }
}
