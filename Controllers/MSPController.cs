using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
//using System.Web.Http;
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
        private readonly IProjectEmployee _Projectrepository;
        public MSPController(IMSPInterface repository, IProjectEmployee projectrepository)
        {
            _repository = repository;
            _Projectrepository = projectrepository;
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

        #region
        // Gantt Chart API's
        [HttpPost("MSP/Impact_Attach_Hdr")]
        public async Task<ActionResult> GetImpactAttachHDR([FromBody] ImpactAttachHDR_InputResponse impactAttachHDR_)
        {
            var response = new Commonresponse();
            try
            {
                var result = await _repository.GetLatestImpactAttachHdr(impactAttachHDR_.projectMkey,impactAttachHDR_.buildingMkey ,impactAttachHDR_.sessionUserId ,impactAttachHDR_.businessGroupId);
                if (result.Status!.Contains("Success"))
                {
                    //result.Data.FILE_PATH = "C:\\Users\\itemadh\\Desktop\\ImpactAttachment";
                    if (result.Data != null)
                    {
                        result.Data.File_Path_Url = Path.Combine(
                            result.Data.FILE_PATH ?? string.Empty,
                            result.Data.FILE_NAME ?? string.Empty
                        ).Replace("\\", "/");
                    }

                    response.Status = "Success";
                    response.Message = result.Message;
                    response.Data = result.Data;
                }
                
            }
            catch (Exception ex)
            {

                response.Status = "Error";
                response.Message = $"Error Due to {ex.Message}";
                response.Data = null;

            }
            return Ok(response);
        }

        [HttpPost("MSP/GhantChart-FileUpload")]
        [AllowAnonymous]
        public async Task<ActionResult> GhantChartFileUpload([FromForm] Ghantchart_UploadAPI_NT objFile)
        {
            var response = new Commonresponse();

            try
            {
                // ✅ Validate file
                if (objFile.files == null || objFile.files.Length == 0)
                {
                    return Ok(new Commonresponse
                    {
                        Status = "Error",
                        Message = "File is missing",
                        Data = null
                    });
                }

                var file = objFile.files;

                // ✅ Get base path from config/repository
                var basePathResult = await _Projectrepository.FileDownload();
                string basePath = basePathResult.Value;

                // ✅ Create folder
                //string folderPath = Path.Combine(basePath, "ImpactAttachment");
                string folderPath = Path.Combine(basePath, "Attachments", "ImpactAttachment");
                string relativeFolderPath = Path.Combine("Attachments", "ImpactAttachment");
                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);

                // ✅ Generate unique file name
                string fileName = $"{DateTime.Now:dd_HH_mm_ss}_{Path.GetFileName(file.FileName)}";
                string returnFullPath = Path.Combine(relativeFolderPath, fileName);
                // ✅ Full physical path
                string fullPath = Path.Combine(folderPath, fileName);

                // ✅ Save file
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                // ✅ Set values for DB
                objFile.FILE_NAME = fileName;
                objFile.FILE_PATH = returnFullPath;   // store folder only (BEST PRACTICE)

                // ✅ Call repository
                var result = await _repository.GhantChartTASKFileUpoadNTAsync(objFile);

                if (result != null && result.Value != null)
                {
                    return Ok(new GhantChart_TaskOutPut_List_NT
                    {
                        Status = "Success",
                        Message = "File Uploaded Successfully",
                        Mkey = result.Value.Mkey,
                        //FILE_PATH = Path.Combine(objFile.FILE_PATH, objFile.FILE_NAME).Replace("\\", "/")
                        FILE_PATH = returnFullPath   //.Replace("\\", "/")
                    });
                }
                else
                {
                    return Ok(new Commonresponse
                    {
                        Status = "Error",
                        Message = "Database insert failed",
                        Data = null
                    });
                }
            }
            catch (Exception ex)
            {
                return Ok(new Commonresponse
                {
                    Status = "Error",
                    Message = ex.Message,
                    Data = null
                });
            }
        }


        #endregion


    }
}
