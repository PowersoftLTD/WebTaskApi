using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.API.Interfaces;
using TaskManagement.API.Model;

namespace TaskManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MSPController : ControllerBase
    {
        private readonly IMSPInterface _repository;
        public MSPController(IMSPInterface repository)
        {
            _repository = repository;
        }

        [HttpPost("MSP/Upload-Excel")]
        public async Task<ActionResult<MSPUploadExcelOutPut>> Upload_Excel(MSPUploadExcelInput mSPUploadExcelInput)
        {
            try
            {
                var LoginValidate = await _repository.UploadExcel(mSPUploadExcelInput);
                return Ok(LoginValidate);
            }
            catch (Exception ex)
            {
                var response = new MSPUploadExcelOutPut
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
