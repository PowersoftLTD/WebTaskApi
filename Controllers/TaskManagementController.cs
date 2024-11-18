using TaskManagement.API.Interfaces;
using TaskManagement.API.Model;
using TaskManagement.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace TaskManagement.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TaskManagementController : ControllerBase
    {
        private readonly ITASKRepository _repository;
        public static IWebHostEnvironment _environment;
        public TaskManagementController(ITASKRepository repository, IWebHostEnvironment environment)
        {
            _repository = repository;
            _environment = environment;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<TASK_RECURSIVE_HDR>>> GetTask()
        {
            try
            {
                var Task = await _repository.GetAllTASKsAsync();
                return Ok(Task);
            }
            catch (Exception)
            {
                return new List<TASK_RECURSIVE_HDR>();
            }
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<TASK_RECURSIVE_HDR>> GetTask(int id)
        {
            var TASK = await _repository.GetTaskByIdAsync(id);
            if (TASK == null)
            {
                return NotFound();
            }
            return Ok(TASK);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<TASK_RECURSIVE_HDR>> CreateTASK(TASK_RECURSIVE_HDR tASK_RECURSIVE_HDR)
        {
            try
            {
                var model = await _repository.CreateTASKAsync(tASK_RECURSIVE_HDR);
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
        public async Task<IActionResult> UpdateTASK(int MKEY, [FromBody] TASK_RECURSIVE_HDR tASK_RECURSIVE_HDR)
        {
            try
            {
                var TASK = await _repository.GetTaskByIdAsync(MKEY);
                if (TASK == null)
                {
                    return NotFound();
                }
                if (MKEY != tASK_RECURSIVE_HDR.MKEY)
                {
                    return BadRequest();
                }
                await _repository.UpdateTASKAsync(tASK_RECURSIVE_HDR);
                TASK = null;
                TASK = await _repository.GetTaskByIdAsync(MKEY);
                if (TASK == null)
                {
                    return NotFound();
                }
                return Ok(TASK);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}/{LastUpatedBy}")]
        [Authorize]
        public async Task<IActionResult> DeleteTASK(int id, int LastUpatedBy)
        {
            bool deleteTask = await _repository.DeleteTASKAsync(id, LastUpatedBy);
            if (deleteTask)
            {
                var TASK = await _repository.GetTaskByIdAsync(id);
                if (TASK == null)
                {
                    return Ok("Row deleted");
                }
            }
            return NoContent();

        }
    }
}
