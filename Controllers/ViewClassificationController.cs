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

        [HttpGet("Responsible-Department")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<V_Building_Classification>>> GetResponsibleDepartment()
        {
            try
            {
                var classifications = await _repository.GetViewResponsibleDepartment();
                return Ok(classifications);
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.Message);
            }
        }

        [HttpPost("Raised-AT")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<RAISED_AT_OUTPUT_LIST>>> GetRaiseAT(RAISED_AT_INPUT rAISED_AT_INPUT)
        {
            try
            {
                if (rAISED_AT_INPUT.PROPERTY_MKEY == 0 || rAISED_AT_INPUT.BUILDING_MKEY == 0)
                {
                    var ErrorResponse = new List<RAISED_AT_OUTPUT_LIST>
                        {
                            new RAISED_AT_OUTPUT_LIST
                            {
                                Status = "Error",
                                Message = "Please enter the details",
                                Data= null
                            }
                        };
                    return ErrorResponse;
                }
                var classifications = await _repository.GetRaiseATAsync(rAISED_AT_INPUT);
                return Ok(classifications);
            }
            catch (Exception ex)
            {
                var ErrorResponse = new List<RAISED_AT_OUTPUT_LIST>
                        {
                            new RAISED_AT_OUTPUT_LIST
                            {
                                Status = "Error",
                                Message =ex.Message,
                                Data= null
                            }
                        };
                return ErrorResponse;
            }
        }

        [HttpPost("Raised-AT-Before")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<RAISED_AT_OUTPUT_LIST>>> GetRaiseATBefore(RAISED_AT_INPUT rAISED_AT_INPUT)
        {
            try
            {
                if (rAISED_AT_INPUT.PROPERTY_MKEY == 0 || rAISED_AT_INPUT.BUILDING_MKEY == 0)
                {
                    var ErrorResponse = new List<RAISED_AT_OUTPUT_LIST>
                        {
                            new RAISED_AT_OUTPUT_LIST
                            {
                                Status = "Error",
                                Message = "Please enter the details",
                                Data= null
                            }
                        };
                    return ErrorResponse;
                }
                var classifications = await _repository.GetRaiseATBeforeAsync(rAISED_AT_INPUT);
                return Ok(classifications);
            }
            catch (Exception ex)
            {
                var ErrorResponse = new List<RAISED_AT_OUTPUT_LIST>
                        {
                            new RAISED_AT_OUTPUT_LIST
                            {
                                Status = "Error",
                                Message =ex.Message,
                                Data= null
                            }
                        };
                return ErrorResponse;
            }
        }

        [HttpGet("Compliance-Status")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<COMPLIANCE_STATUS_OUTPUT_LIST>>> GetComplianceStatus()
        {
            try
            {
                var classifications = await _repository.GetComplianceStatusAsync();
                return classifications;
            }
            catch (Exception ex)
            {
                var ErrorResponse = new List<COMPLIANCE_STATUS_OUTPUT_LIST>
                        {
                            new COMPLIANCE_STATUS_OUTPUT_LIST
                            {
                                Status = "Error",
                                Message =ex.Message,
                                Data= null
                            }
                        };
                return ErrorResponse;
            }
        }

        [HttpGet("Task-Type")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<GetTaskTypeList>>> GetTaskType()
        {
            try
            {
                var classifications = await _repository.GetTaskTypeAsync();
                return classifications;
            }
            catch (Exception ex)
            {
                var ErrorResponse = new List<GetTaskTypeList>
                {
                    new GetTaskTypeList
                    {
                        Status = "Error",
                        Message =ex.Message,
                        Data= null
                    }
                };
                return ErrorResponse;
            }
        }

        [HttpPost("Responsible-Person-By-JobRole-Department")]
        [Authorize]
        public async Task<ActionResult<EmployeeCompanyMST>> GetResponsiblePersonByJobRoleDepartment(RESPONSIBLE_PERSON_INPUT rESPONSIBLE_PERSON_INPUT)
        {
            try
            {
                var classifications = await _repository.GetResponsiblePersonByJobRoleDepartmentAsync(rESPONSIBLE_PERSON_INPUT);
                return Ok(classifications);
            }
            catch (Exception ex)
            {
                var response = new V_Building_Classification_new
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
