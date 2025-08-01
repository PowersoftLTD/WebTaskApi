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


        [HttpPost("Get-Project-Document-Depsitory-NT")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<UpdateProjectDocDepositoryHDROutput_List_NT>>> GetAllProjDocDepsitoryNT(ProjectDocDepositoryInputNT projectDocDepositoryInputNT)
        {
            try
            {
                var RsponseStatus = await _repository.GetAllProjectDocDeositoryAsyncNT(projectDocDepositoryInputNT);
                return RsponseStatus;
            }
            catch (Exception ex)
            {
                //var response = new UpdateProjectDocDepositoryHDROutput_List_NT
                //{
                //    STATUS = "Error",
                //    MESSAGE = ex.Message,
                //    DATA = null
                //};
                //return Ok(response);

                var errorResult = new List<UpdateProjectDocDepositoryHDROutput_List_NT>
                {
                    new UpdateProjectDocDepositoryHDROutput_List_NT
                    {
                        STATUS = "Ok",
                        MESSAGE = "Data not found",
                        DATA = null
                    }
                };
                return errorResult;
            }

        }


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

        [HttpPost("Post-Project-Document-Depsitory-NT")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<UpdateProjectDocDepositoryHDROutput_List_NT>>> CreateProjDocDepsitoryNT([FromForm] PROJECT_DOC_DEPOSITORY_HDR_NT pROJECT_DOC_DEPOSITORY_HDR)
        {
            try
            {
                if (pROJECT_DOC_DEPOSITORY_HDR.PROJECT_DOC_FILES != null)
                {
                    var RsponseTTStatus = await _repository.CreateProjectDocDeositoryAsyncNT(pROJECT_DOC_DEPOSITORY_HDR);
                    return RsponseTTStatus;
                }

                if (pROJECT_DOC_DEPOSITORY_HDR == null)
                {
                    var errorResult = new List<UpdateProjectDocDepositoryHDROutput_List_NT>
                    {
                        new UpdateProjectDocDepositoryHDROutput_List_NT
                        {
                            STATUS = "Error",
                            MESSAGE = "Please Enter the details",
                            DATA = null
                        }
                    };
                    return errorResult;
                }

                var RsponseStatus = await _repository.CreateProjectDocDeositoryAsyncNT(pROJECT_DOC_DEPOSITORY_HDR);
                return RsponseStatus;
            }
            catch (Exception ex)
            {
                var errorResult = new List<UpdateProjectDocDepositoryHDROutput_List_NT>
                {
                    new UpdateProjectDocDepositoryHDROutput_List_NT
                    {
                        STATUS = "Error",
                        MESSAGE = ex.Message,
                        DATA = null
                    }
                };
                return errorResult;
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

        [HttpPost("Get-Document-Details-NT")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<ProjectDocOutput_NT>>> GetDocumentDetailsNT(ProjectDocInput_NT projectDocInput_NT)
        {
            try
            {
                var ProjectDocDeository = await _repository.GetDocumentDetailsAsyncNT(projectDocInput_NT);
                return ProjectDocDeository;
            }
            catch (Exception ex) {
                var errorResult = new List<ProjectDocOutput_NT>
                {
                    new ProjectDocOutput_NT
                    {
                        STATUS = "Error",
                        MESSAGE = ex.Message,
                        DATA = null
                    }
                };
                return errorResult;
            }
        }
    }
}
