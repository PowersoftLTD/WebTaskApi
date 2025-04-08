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

        [HttpPost("Doc-Type_NT")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<V_Doc_Type_OutPut_NT>>> GetAllViewDoc_Type_NT(Doc_Type_Doc_CategoryInput doc_Type_Doc_CategoryInput)
        {
            try
            {
                var classifications = await _repository.GetViewDoc_TypeNTAsync(doc_Type_Doc_CategoryInput);
                return Ok(classifications);
            }
            catch (Exception ex)
            {
                var response = new V_Doc_Type_OutPut_NT
                {
                    Status = "Error",
                    Message = ex.Message,
                    Data = null
                };
                return Ok(response);
            }
        }

        [HttpPost("Doc-Type-Instruction_NT")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<V_Building_Classification_OutPut_NT>>> GetAllViewDoc_Type_CheckList_NT(Doc_Type_Doc_CategoryInput doc_Type_Doc_CategoryInput)
        {
            try
            {
                var classifications = await _repository.GetViewDoc_Type_CheckList_NTAsync(doc_Type_Doc_CategoryInput);
                return Ok(classifications);
            }
            catch (Exception ex)
            {
                var response = new V_Building_Classification_OutPut_NT
                {
                    Status = "Error",
                    Message = ex.Message,
                    Data = null
                };
                return Ok(response);
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

        [HttpPost("Job-Role-Type_NT")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<V_Job_Role_NT_OutPut>>> GetAllViewJOB_ROLE_NT(V_Department_NT_Input v_Department_NT_Input)
        {
            try
            {
                if (v_Department_NT_Input.Business_Group_Id == 0 || v_Department_NT_Input.Session_User_Id == 0)
                {
                    var ErrorResponse = new List<V_Job_Role_NT_OutPut>
                        {
                            new V_Job_Role_NT_OutPut
                            {
                                Status = "Error",
                                Message = "Please enter the details",
                                Data= null
                            }
                        };
                    return ErrorResponse;
                }
                var classifications = await _repository.GetViewJOB_ROLE_NTAsync(v_Department_NT_Input);
                return Ok(classifications);
            }
            catch (Exception ex)
            {
                var ErrorResponse = new List<V_Job_Role_NT_OutPut>
                {
                    new V_Job_Role_NT_OutPut
                    {
                        Status = "Error",
                        Message = ex.Message,
                        Data= null
                    }
                };
                return ErrorResponse;
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

        [HttpPost("Department_NT")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<V_Department_NT_OutPut>>> GetAllDepartmentNT(V_Department_NT_Input v_Department_NT_Input)
        {
            try
            {
                if (v_Department_NT_Input.Business_Group_Id == 0 || v_Department_NT_Input.Session_User_Id == 0)
                {
                    var ErrorResponse = new List<V_Department_NT_OutPut>
                        {
                            new V_Department_NT_OutPut
                            {
                                Status = "Error",
                                Message = "Please enter the details",
                                Data= null
                            }
                        };
                    return ErrorResponse;
                }
                var classifications = await _repository.GetAllDepartmentNTAsync(v_Department_NT_Input);
                return Ok(classifications);
            }
            catch (Exception ex)
            {
                var ErrorResponse = new List<V_Department_NT_OutPut>
                {
                    new V_Department_NT_OutPut
                    {
                        Status = "Error",
                        Message =ex.Message,
                        Data= null
                    }
                };
                return ErrorResponse;
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

        [HttpPost("Sanctioning-Authority_NT")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<V_Sanctioning_Authority_OutPut_NT>>> GetAllSanctioningAuthority_NT(Doc_Type_Doc_CategoryInput doc_Type_Doc_CategoryInput)
        {
            try
            {
                var classifications = await _repository.GetViewSanctioningAuthority_NT(doc_Type_Doc_CategoryInput);
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

        [HttpPost("Raised-AT_NT")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<RAISED_AT_OUTPUT_LIST_NT>>> GetRaiseAT(RAISED_AT_INPUT_NT rAISED_AT_INPUT)
        {
            try
            {
                if (rAISED_AT_INPUT.PROPERTY_MKEY == 0 || rAISED_AT_INPUT.BUILDING_MKEY == 0)
                {
                    var ErrorResponse = new List<RAISED_AT_OUTPUT_LIST_NT>
                        {
                            new RAISED_AT_OUTPUT_LIST_NT
                            {
                                Status = "Error",
                                Message = "Please enter the details",
                                Data= null
                            }
                        };
                    return ErrorResponse;
                }
                var classifications = await _repository.GetRaiseATNTAsync(rAISED_AT_INPUT);
                return Ok(classifications);
            }
            catch (Exception ex)
            {
                var ErrorResponse = new List<RAISED_AT_OUTPUT_LIST_NT>
                {
                    new RAISED_AT_OUTPUT_LIST_NT
                    {
                        Status = "Error",
                        Message =ex.Message,
                        Data= null
                    }
                };
                return ErrorResponse;
            }
        }

        [HttpPost("Raised-At")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<RAISED_AT_OUTPUT_LIST>>> GetRaiseAT_NT(RAISED_AT_INPUT rAISED_AT_INPUT)
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

        [HttpGet("Compliance-Status_NT")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<COMPLIANCE_STATUS_OUTPUT_LIST_NT>>> GetComplianceStatus_NT()
        {
            try
            {
                var classifications = await _repository.GetComplianceStatusNTAsync();
                return classifications;
            }
            catch (Exception ex)
            {
                var ErrorResponse = new List<COMPLIANCE_STATUS_OUTPUT_LIST_NT>
                        {
                            new COMPLIANCE_STATUS_OUTPUT_LIST_NT
                            {
                                Status = "Error",
                                Message = ex.Message,
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

        [HttpPost("Task-Type_NT")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<GetTaskTypeListNT>>> GetTaskType_NT(GetTaskTypeInPut getTaskTypeInPut)
        {
            try
            {
                var classifications = await _repository.GetTaskTypeNTAsync(getTaskTypeInPut);
                return classifications;
            }
            catch (Exception ex)
            {
                var ErrorResponse = new List<GetTaskTypeListNT>
                {
                    new GetTaskTypeListNT
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
