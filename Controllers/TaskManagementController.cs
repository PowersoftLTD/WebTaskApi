using TaskManagement.API.Interfaces;
using TaskManagement.API.Model;
using TaskManagement.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using TaskManagement.API.CustomActionFilters;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using static Azure.Core.HttpHeader;

namespace TaskManagement.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TaskManagementController : ControllerBase
    {
        public static IWebHostEnvironment _environment;
        private readonly ITASKRepository _repository;
        public TaskManagementController(ITASKRepository repository, IWebHostEnvironment environment)
        {
            _repository = repository;
            _environment = environment;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<TASK_RECURSIVE_HDR>>> GetTask()
        {
            var Task = await _repository.GetAllTASKsAsync();
            return Ok(Task);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<TASK_RECURSIVE_HDR>> GetTask(int id)
        {
            var TASK = await _repository.GetTaskByIdAsync(id);
            
            //using (FileStream filestream = System.IO.File.OpenRead(_environment.ContentRootPath + "\\Attachment\\" + "\\" + id.ToString() + "\\" + TASK.FILE_NAME.ToString();))
            //{
            //    //tASK_RECURSIVE_HDR.files.CopyTo(filestream);
            //    //filestream.Flush();
            //}

            if (TASK == null)
            {
                return NotFound();
            }
            return Ok(TASK);
        }
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<TASK_RECURSIVE_HDR>> CreateTASK([FromForm] TASK_RECURSIVE_HDR tASK_RECURSIVE_HDR)
        {
            int newTASKId = await _repository.CreateTASKAsync(tASK_RECURSIVE_HDR);
            tASK_RECURSIVE_HDR.MKEY = Convert.ToInt32(newTASKId);

            if (tASK_RECURSIVE_HDR.files.Length > 0)
            {
                if (!Directory.Exists(_environment.ContentRootPath + "\\Attachment" + "\\" + newTASKId.ToString() + "\\"))
                {
                    Directory.CreateDirectory(_environment.ContentRootPath + "\\Attachment\\" + "\\" + newTASKId.ToString() + "\\");
                }
                using (FileStream filestream = System.IO.File.Create(_environment.ContentRootPath + "\\Attachment\\" + "\\" + newTASKId.ToString() + "\\" + tASK_RECURSIVE_HDR.files.FileName.ToString()))
                {
                    tASK_RECURSIVE_HDR.files.CopyTo(filestream);
                    filestream.Flush();
                }
            }

            return CreatedAtAction(nameof(GetTask), new { newTASKId }, tASK_RECURSIVE_HDR);
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
            await _repository.DeleteTASKAsync(id, LastUpatedBy);
            var TASK = await _repository.GetTaskByIdAsync(id);
            if (TASK == null)
            {
                return Ok("Row deleted");
            }
            return NoContent();
        }

       
    }
}
