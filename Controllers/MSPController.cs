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
        public async Task<ActionResult<MSPUploadExcelOutPut>> Upload_Excel(List<MSPUploadExcelInput> mSPUploadExcelInput)
        {
            try
            {
                var UploadExcel = await _repository.UploadExcel(mSPUploadExcelInput);
                return Ok(UploadExcel);
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

        [HttpPost("MSP/Get-Task-MSP")]
        public async Task<ActionResult<MSPUploadExcelOutPut>> GetTaskMSP(MSPTaskInput mSPTaskInput)
        {
            try
            {
                var UploadExcel = await _repository.GetTaskMspAsync(mSPTaskInput);
                return Ok(UploadExcel);
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

        [HttpPost("MSP/Update-Task-MSP")]
        public async Task<ActionResult<MSPUploadExcelOutPut>> UpdateTaskMSP(List<MSPUploadExcelInput> mSPUploadExcelInput)
        {
            try
            {
                var UploadExcel = await _repository.UpdateTaskMspAsync(mSPUploadExcelInput);
                return Ok(UploadExcel);
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
