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
        public async Task<ActionResult<dynamic>> CreateProjDocDepsitory(int? BUILDING_TYPE, int? PROPERTY_TYPE, string? DOC_NAME, string? DOC_NUMBER, string? DOC_DATE, string? DOC_ATTACHMENT, string? VALIDITY_DATE, int? CREATED_BY, string? ATTRIBUTE1, string? ATTRIBUTE2, string? ATTRIBUTE3)
        {
            var ProjectDocDeository = await _repository.CreateProjectDocDeositoryAsync(BUILDING_TYPE, PROPERTY_TYPE, DOC_NAME, DOC_NUMBER, DOC_DATE, DOC_ATTACHMENT, VALIDITY_DATE, CREATED_BY, ATTRIBUTE1, ATTRIBUTE2, ATTRIBUTE3);
            if (ProjectDocDeository == null)
            {
                return NotFound();
            }
            return Ok(ProjectDocDeository);
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
