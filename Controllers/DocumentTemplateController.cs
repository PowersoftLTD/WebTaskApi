using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.API.Interfaces;
using TaskManagement.API.Model;

namespace TaskManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentTemplateController : ControllerBase
    {
        private readonly IDoc_Temp _repository;
        public DocumentTemplateController(IDoc_Temp repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<DOC_TEMPLATE_HDR>>> GetAllDocumentTemplates(int LoggedIN)
        {
            try
            {
                var Task = await _repository.GetAllDocumentTempAsync(LoggedIN);
                return Ok(Task);
            }
            catch (Exception)
            {
                return new List<DOC_TEMPLATE_HDR>();
            }
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<DOC_TEMPLATE_HDR>> GetDocumentTemplates(int id, int LoggedIN)
        {
            var TASK = await _repository.GetDocumentTempByIdAsync(id, LoggedIN);
            if (TASK == null)
            {
                return NotFound();
            }
            return Ok(TASK);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<DOC_TEMPLATE_HDR>> CreateTASK(DOC_TEMPLATE_HDR dOC_TEMPLATE_HDR)
        {
            try
            {
                var model = await _repository.CreateDocumentTemplateAsync(dOC_TEMPLATE_HDR);
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
        public async Task<IActionResult> UpdateTASK(int MKEY, [FromBody] DOC_TEMPLATE_HDR dOC_TEMPLATE_HDR)
        {
            try
            {
                var TASK = await _repository.GetDocumentTempByIdAsync(MKEY, dOC_TEMPLATE_HDR.CREATED_BY);
                if (TASK == null)
                {
                    return NotFound();
                }
                if (MKEY != dOC_TEMPLATE_HDR.MKEY)
                {
                    return BadRequest();
                }
                await _repository.UpdateDocumentTemplateAsync(dOC_TEMPLATE_HDR);
                TASK = null;
                TASK = await _repository.GetDocumentTempByIdAsync(MKEY, dOC_TEMPLATE_HDR.CREATED_BY);
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
        public async Task<IActionResult> DeleteTASK(int id, int LastUpatedBy, int LoggedIN)
        {
            bool deleteTask = await _repository.DeleteDocumentTemplateAsync(id, LastUpatedBy);
            if (deleteTask)
            {
                var TASK = await _repository.GetDocumentTempByIdAsync(id, LoggedIN);
                if (TASK == null)
                {
                    return Ok("Row deleted");
                }
            }
            return NoContent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>


        //[HttpGet]
        //[Authorize]
        //public async Task<ActionResult<IEnumerable<DOC_TEMPLATE_HDR>>> GetAllViewDoc_Type()
        //{
        //    try
        //    {
        //        var Task = await _repository.GetViewDoc_TypeAsync();
        //        return Ok(Task);
        //    }
        //    catch (Exception)
        //    {
        //        return new List<DOC_TEMPLATE_HDR>();
        //    }
        //}

        //[HttpGet]
        //[Authorize]
        //public async Task<ActionResult<IEnumerable<DOC_TEMPLATE_HDR>>> GetAllViewStandard_Type()
        //{
        //    try
        //    {
        //        var Task = await _repository.GetViewStandard_TypeAsync();
        //        return Ok(Task);
        //    }
        //    catch (Exception)
        //    {
        //        return new List<DOC_TEMPLATE_HDR>();
        //    }
        //}

        //[HttpGet]
        //[Authorize]
        //public async Task<ActionResult<IEnumerable<DOC_TEMPLATE_HDR>>> GetAllViewStatutory_Auth()
        //{
        //    try
        //    {
        //        var Task = await _repository.GetViewStatutory_AuthAsync();
        //        return Ok(Task);
        //    }
        //    catch (Exception)
        //    {
        //        return new List<DOC_TEMPLATE_HDR>();
        //    }
        //}

        //[HttpGet]
        //[Authorize]
        //public async Task<ActionResult<IEnumerable<DOC_TEMPLATE_HDR>>> GetAllViewJOB_ROLE()
        //{
        //    try
        //    {
        //        var Task = await _repository.GetViewJOB_ROLEAsync();
        //        return Ok(Task);
        //    }
        //    catch (Exception)
        //    {
        //        return new List<DOC_TEMPLATE_HDR>();
        //    }
        //}
    }
}
