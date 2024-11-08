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
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{MKEY}")]
        [Authorize]
        public async Task<IActionResult> UpdateTASK(int MKEY, [FromBody] PROJECT_HDR pROJECT_HDR)
        {
            try
            {
                var ProjectDetails = await _repository.GetProjectDefinationByIdAsync(MKEY,pROJECT_HDR.LAST_UPDATED_BY, pROJECT_HDR.ATTRIBUTE2, pROJECT_HDR.ATTRIBUTE3);
                if (ProjectDetails == null)
                {
                    return NotFound();
                }
                if (MKEY != pROJECT_HDR.MKEY)
                {
                    return BadRequest();
                }
                await _repository.UpdateProjectDefinationAsync(pROJECT_HDR);
                ProjectDetails = null;
                ProjectDetails = await _repository.GetProjectDefinationByIdAsync(MKEY, pROJECT_HDR.LAST_UPDATED_BY, pROJECT_HDR.ATTRIBUTE2, pROJECT_HDR.ATTRIBUTE3);
                if (ProjectDetails == null)
                {
                    return NotFound();
                }
                return Ok(ProjectDetails);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}/{LastUpatedBy}")]
        [Authorize]
        public async Task<IActionResult> DeleteTASK(int id, int LastUpatedBy,string FormName,string MethodName)
        {
            bool deleteTask = await _repository.DeleteProjectDefinationAsync(id, LastUpatedBy,FormName,MethodName);
            if (deleteTask)
            {
                var TASK = await _repository.GetProjectDefinationByIdAsync(id,LastUpatedBy,FormName,MethodName);
                if (TASK == null)
                {
                    return Ok("Row deleted");
                }
            }
            return NoContent();

        }
    }
}
