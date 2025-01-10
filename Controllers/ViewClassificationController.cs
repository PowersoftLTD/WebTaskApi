using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.API.Interfaces;
using TaskManagement.API.Model;
using TaskManagement.API.Repositories;

namespace TaskManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ViewClassificationController : ControllerBase
    {
        private readonly IViewClassification _repository;

        public ViewClassificationController(IViewClassification repository)
        {
            _repository = repository;
        }
        [HttpGet("building-classification")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<V_Building_Classification>>> GetAllViewBuildingClassification()
        {
            try
            {
                var classifications = await _repository.GetViewBuildingClassificationAsync();
                return Ok(classifications);
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.Message);
            }
        }

        [HttpGet("doc-type")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<V_Building_Classification>>> GetAllViewDoc_Type()
        {
            try
            {
                var classifications = await _repository.GetViewDoc_TypeAsync();
                return Ok(classifications);
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.Message);
            }
        }


        [HttpGet("doc-type-Instruction")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<V_Building_Classification>>> GetAllViewDoc_Type_CheckList()
        {
            try
            {
                var classifications = await _repository.GetViewDoc_Type_CheckListAsync();
                return Ok(classifications);
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.Message);
            }
        }

        [HttpGet("Instruction-List")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<V_Instruction>>> GetAllInstruction()
        {
            try
            {
                var classifications = await _repository.GetAllInstruction();
                return Ok(classifications);
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.Message);
            }
        }

        [HttpGet("Standard-Type")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<V_Building_Classification>>> GetAllViewStandard_Type()
        {
            try
            {
                var classifications = await _repository.GetViewStandard_TypeAsync();
                return Ok(classifications);
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.Message);
            }
        }

        [HttpGet("Statutory-type")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<V_Building_Classification>>> GetAllViewStatutory_Auth()
        {
            try
            {
                var classifications = await _repository.GetViewStatutory_AuthAsync();
                return Ok(classifications);
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.Message);
            }
        }

        [HttpGet("JOB-ROLE-type")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<V_Building_Classification>>> GetAllViewJOB_ROLE()
        {
            try
            {
                var classifications = await _repository.GetViewJOB_ROLEAsync();
                return Ok(classifications);
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.Message);
            }
        }

        [HttpGet("DEPARTMENT")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<V_Building_Classification>>> GetAllDepartment()
        {
            try
            {
                var classifications = await _repository.GetViewDepartment();
                return Ok(classifications);
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.Message);
            }
        }

        [HttpGet("Sanctioning-Authority")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<V_Building_Classification>>> GetAllSanctioningAuthority()
        {
            try
            {
                var classifications = await _repository.GetViewSanctioningAuthority();
                return Ok(classifications);
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.Message);
            }
        }

        [HttpGet("Document-Category")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<V_Building_Classification>>> GetAllDocument_Category()
        {
            try
            {
                var classifications = await _repository.GetViewDocument_Category();
                return Ok(classifications);
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.Message);
            }
        }

    }
}
