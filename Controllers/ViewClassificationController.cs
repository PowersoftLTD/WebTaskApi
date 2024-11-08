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
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
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
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
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
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
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
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
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
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
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
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
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
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        //[HttpGet("Sanctioning-Department")]
        //[Authorize]
        //public async Task<ActionResult<IEnumerable<V_Building_Classification>>> GetAllSanctioningDepartment()
        //{
        //    try
        //    {
        //        var classifications = await _repository.GetViewSanctioningDepartment();
        //        return Ok(classifications);
        //    }
        //    catch (Exception)
        //    {
        //        return StatusCode(500, "Internal server error");
        //    }
        //}
    }
}
