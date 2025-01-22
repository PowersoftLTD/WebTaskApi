using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.API.Interfaces;
using TaskManagement.API.Model;
using Microsoft.AspNetCore.Authorization;
using System.Collections;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace TaskManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProjectDocumentDepositoryController : ControllerBase
    {
        public static IWebHostEnvironment _environment;
        private readonly FileSettings _fileSettings;
        private readonly IProjectDocDepository _repository;
        public ProjectDocumentDepositoryController(IProjectDocDepository repository, IWebHostEnvironment environment, IOptions<FileSettings> fileSettings)
        {
            _repository = repository;
            _environment = environment;
            _fileSettings = fileSettings.Value;
        }

        [HttpPost("Get-Project-Document-Depsitory")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<UpdateProjectDocDepositoryHDROutput_List>>> GetAllProjDocDepsitory(ProjectDocDepositoryInput projectDocDepositoryInput)
        {
            try
            {
                var RsponseStatus = await _repository.GetAllProjectDocDeositoryAsync(projectDocDepositoryInput);
                return RsponseStatus;
            }
            catch (Exception ex)
            {
                var response = new UpdateProjectDocDepositoryHDROutput_List
                {
                    STATUS = "Error",
                    MESSAGE = ex.Message,
                    DATA = null
                };
                return Ok(response);
            }

        }

        //[HttpPost("Get-Project-Document-Depsitory-BY-ID")]
        //[Authorize]
        //public async Task<ActionResult<dynamic>> GetAllProjDocDepsitoryByID(int? MKEY, string? ATTRIBUTE1, string? ATTRIBUTE2, string? ATTRIBUTE3)
        //{
        //    var ProjectDocDeository = await _repository.GetProjectDocDeositoryByIDAsync(MKEY, ATTRIBUTE1, ATTRIBUTE2, ATTRIBUTE3);
        //    if (ProjectDocDeository == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(ProjectDocDeository);
        //}

        [HttpPost("Post-Project-Document-Depsitory")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<UpdateProjectDocDepositoryHDROutput_List>>> CreateProjDocDepsitory([FromForm] PROJECT_DOC_DEPOSITORY_HDR pROJECT_DOC_DEPOSITORY_HDR)
        {
            try
            {
                if (pROJECT_DOC_DEPOSITORY_HDR.PROJECT_DOC_FILES != null)
                {
                    var RsponseTTStatus = await _repository.CreateProjectDocDeositoryAsync(pROJECT_DOC_DEPOSITORY_HDR);
                    return RsponseTTStatus;
                }
                if (pROJECT_DOC_DEPOSITORY_HDR == null)
                {
                    var response = new UpdateProjectDocDepositoryHDROutput_List
                    {
                        STATUS = "Error",
                        MESSAGE = "Please Enter the details",
                        DATA = null
                    };
                    return Ok(response);
                }

                var RsponseStatus = await _repository.CreateProjectDocDeositoryAsync(pROJECT_DOC_DEPOSITORY_HDR);
                return RsponseStatus;
            }
            catch (Exception ex)
            {
                var response = new UpdateProjectDocDepositoryHDROutput_List
                {
                    STATUS = "Error",
                    MESSAGE = ex.Message,
                    DATA = null
                };
                return Ok(response);
            }

        }

        [HttpPut("Put-Project-Document-Depsitory")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<UpdateProjectDocDepositoryHDROutput_List>>> UpdateProjDocDepsitory(UpdateProjectDocDepositoryHDRInput updateProjectDocDepositoryHDRInput)
        {
            try
            {
                var DocDeository = await _repository.GetAllProjDocDepsitory(Convert.ToInt32(updateProjectDocDepositoryHDRInput.MKEY), updateProjectDocDepositoryHDRInput.CREATED_BY.ToString(), "UpdateProjDocDepsitory".ToString(), "Update".ToString());

                if (DocDeository != null || Convert.ToInt32(DocDeository) > 0)
                {
                    var ProjectDocDeository = await _repository.UpdateProjectDepositoryDocumentAsync(updateProjectDocDepositoryHDRInput);
                    return Ok(ProjectDocDeository);
                }
                else
                {
                    var responseDocumentTemplate = new UpdateProjectDocDepositoryHDROutput_List
                    {
                        STATUS = "Error",
                        MESSAGE = "Not found",
                        DATA = null
                    };
                    return Ok(responseDocumentTemplate);
                }
            }
            catch (Exception ex)
            {
                var responseDocumentTemplate = new UpdateProjectDocDepositoryHDROutput_List
                {
                    STATUS = "Error",
                    MESSAGE = ex.Message,
                    DATA = null
                };
                return Ok(responseDocumentTemplate);
            }

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

        //[HttpPost("Project-Document-Depository-File-Upload"), DisableRequestSizeLimit]
        //[Authorize]
        //public async Task<ActionResult<DocFileUploadOutput_List>> PostDocDepositoryFileUpload([FromForm] DocFileUploadInput docFileUploadInput)
        //{
        //    try
        //    {
        //        int srNo = 0;
        //        string filePathOpen = string.Empty;
        //        if (docFileUploadInput.files != null)
        //        {
        //            if (docFileUploadInput.files.Length > 0)
        //            {
        //                srNo = srNo + 1;
        //                string FilePath = _fileSettings.FilePath;
        //                if (!Directory.Exists(FilePath + "\\Attachments\\" + "Document Depository\\" + docFileUploadInput.MKEY))
        //                {
        //                    Directory.CreateDirectory(FilePath + "\\Attachments\\" + "Document Depository\\" + docFileUploadInput.MKEY);
        //                }
        //                using (FileStream filestream = System.IO.File.Create(FilePath + "\\Attachments\\" + "Document Depository\\"
        //                    + docFileUploadInput.MKEY + "\\" + DateTime.Now.Day + "_" + DateTime.Now.ToShortTimeString().Replace(":", "_") + "_" + docFileUploadInput.files.FileName))
        //                {
        //                    docFileUploadInput.files.CopyTo(filestream);
        //                    filestream.Flush();
        //                }

        //                filePathOpen = "\\Attachments\\" + "Document Depository\\" + docFileUploadInput.MKEY + "\\"
        //                    + DateTime.Now.Day + "_" + DateTime.Now.ToShortTimeString().Replace(":", "_") + "_"
        //                    + docFileUploadInput.files.FileName;

        //                var objDocFileUpload = new DocFileUploadOutPut
        //                {
        //                    MKEY = docFileUploadInput.MKEY,
        //                    FILE_NAME = docFileUploadInput.files.FileName,
        //                    FILE_PATH = filePathOpen
        //                };

        //                var SuccessResult = new DocFileUploadOutput_List
        //                {
        //                    Status = "Ok",
        //                    Message = "Uploaded file",
        //                    Data = objDocFileUpload
        //                };

        //                return SuccessResult;
        //            }
        //            else
        //            {
        //                var ErrorResponse = new DocFileUploadOutput_List
        //                {
        //                    Status = "Error",
        //                    Message = "Please attach the file!!!",
        //                    Data = null
        //                };
        //                return Ok(ErrorResponse);
        //            }
        //        }
        //        var response = new DocFileUploadOutput_List
        //        {
        //            Status = "Error",
        //            Message = "Please attach the file!!!",
        //            Data = null
        //        };
        //        return Ok(response);

        //    }
        //    catch (Exception ex)
        //    {
        //        var response = new DocFileUploadOutput_List
        //        {
        //            Status = "Error",
        //            Message = ex.Message,
        //            Data = null
        //        };
        //        return Ok(response);
        //    }
        //}
    }
}
