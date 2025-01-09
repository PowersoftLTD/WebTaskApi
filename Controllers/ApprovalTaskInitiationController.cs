using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.API.Model;
using Microsoft.AspNetCore.Authorization;
using TaskManagement.API.Interfaces;

namespace TaskManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ApprovalTaskInitiationController : ControllerBase
    {
        private readonly IApprovalTaskInitiation _repository;
        public ApprovalTaskInitiationController(IApprovalTaskInitiation repository)
        {
            _repository = repository;
        }

        [HttpGet("GetApprovalTemplate")]
        [Authorize]
        public async Task<ActionResult<APPROVAL_TEMPLATE_HDR>> GetApprovalTemplate(int MKEY, int APPROVAL_MKEY)
        {
            try
            {
                var TASK = await _repository.GetApprovalTemplateByIdAsync(MKEY, APPROVAL_MKEY);
                if (TASK == null)
                {
                    var responseApprovalTemplate = new ApiResponse<APPROVAL_TASK_INITIATION>
                    {
                        Status = "Error",
                        Message = "Error Occurd",
                        Data = null
                    };
                    return Ok(responseApprovalTemplate);
                }
                else if (TASK.STATUS != "Ok")
                {
                    var responseApprovalTemplate = new ApiResponse<APPROVAL_TASK_INITIATION>
                    {
                        Status = TASK.STATUS,
                        Message = TASK.Message,
                        Data = null
                    };

                    //responseApprovalTemplate.Data. = "APPROVAL_TEMPLATE_HDR";
                    return Ok(responseApprovalTemplate);
                }

                var response = new ApiResponse<APPROVAL_TASK_INITIATION>
                {
                    Status = "OK",
                    Message = "Approval Template details",
                    Data = TASK
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<IEnumerable<APPROVAL_TEMPLATE_HDR>>
                {
                    Status = "Error",
                    Message = ex.Message,
                    Data = null
                };
                return Ok(response);
            }
        }
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<APPROVAL_TASK_INITIATION>> CreateApprovalTASK([FromBody] APPROVAL_TASK_INITIATION aPPROVAL_TASK_INITIATION)
        {
            try
            {
                bool flagSeq_no = false, flagRequired = false;
                double IndexSeq_NO = 0.0;
                string RequiredColumn = string.Empty;

                //if (aPPROVAL_TASK_INITIATION.TASK_NO == null)
                //{
                //    flagRequired = true;
                //    RequiredColumn = RequiredColumn + " ,TASK_NO ";
                //}
                if (aPPROVAL_TASK_INITIATION.BUILDING_MKEY == null)
                {
                    flagRequired = true;
                    RequiredColumn = RequiredColumn + " ,BUILDING_MKEY ";
                }
                if (aPPROVAL_TASK_INITIATION.CAREGORY == null)
                {
                    flagRequired = true;
                    RequiredColumn = RequiredColumn + " ,CAREGORY ";
                }
                if (aPPROVAL_TASK_INITIATION.MAIN_ABBR == null)
                {
                    flagRequired = true;
                    RequiredColumn = RequiredColumn + " ,MAIN_ABBR ";
                }

                if (aPPROVAL_TASK_INITIATION.PROPERTY == null)
                {
                    flagRequired = true;
                    RequiredColumn = RequiredColumn + " ,PROPERTY ";
                }
                if (aPPROVAL_TASK_INITIATION.TENTATIVE_START_DATE == null)
                {
                    flagRequired = true;
                    RequiredColumn = RequiredColumn + " ,START_DATE ";
                }

                if (aPPROVAL_TASK_INITIATION.TENTATIVE_END_DATE == null)
                {
                    flagRequired = true;
                    RequiredColumn = RequiredColumn + " ,END_DATE ";
                }

                if (aPPROVAL_TASK_INITIATION.INITIATOR == null)
                {
                    flagRequired = true;
                    RequiredColumn = RequiredColumn + " ,INITIATOR ";
                }

                if (aPPROVAL_TASK_INITIATION.COMPLITION_DATE == null)
                {
                    flagRequired = true;
                    RequiredColumn = RequiredColumn + " ,COMPLITION_DATE ";
                }
                //if (aPPROVAL_TASK_INITIATION.SANCTION_AUTHORITY == null)
                //{
                //    flagRequired = true;
                //    RequiredColumn = RequiredColumn + " ,SANCTION_AUTHORITY ";
                //}

                if (aPPROVAL_TASK_INITIATION.RESPOSIBLE_EMP_MKEY == null)
                {
                    flagRequired = true;
                    RequiredColumn = RequiredColumn + " ,RESPOSIBLE_EMP_MKEY ";
                }

                if (aPPROVAL_TASK_INITIATION.JOB_ROLE == null)
                {
                    flagRequired = true;
                    RequiredColumn = RequiredColumn + " ,JOB_ROLE ";
                }

                if (aPPROVAL_TASK_INITIATION.RESPOSIBLE_EMP_MKEY == null)
                {
                    flagRequired = true;
                    RequiredColumn = RequiredColumn + " ,RESPOSIBLE_EMP_MKEY ";
                }

                foreach (var ChkDate in aPPROVAL_TASK_INITIATION.SUBTASK_LIST)
                {
                    if (ChkDate != null)
                    {

                        if (ChkDate.APPROVAL_ABBRIVATION == null)
                        {
                            flagRequired = true;
                            RequiredColumn = RequiredColumn + " ,APPROVAL_ABBRIVATION ";
                        }
                        if (ChkDate.DAYS_REQUIRED == null)
                        {
                            flagRequired = true;
                            RequiredColumn = RequiredColumn + " ,DAYS_REQUIRED ";
                        }

                        if (ChkDate.DEPARTMENT == null)
                        {
                            flagRequired = true;
                            RequiredColumn = RequiredColumn + " ,DEPARTMENT ";
                        }
                        if (ChkDate.JOB_ROLE == null)
                        {
                            flagRequired = true;
                            RequiredColumn = RequiredColumn + " ,JOB_ROLE ";
                        }

                        if (ChkDate.TENTATIVE_START_DATE == null)
                        {
                            flagRequired = true;
                            RequiredColumn = RequiredColumn + " ,TENTATIVE_START_DATE ";
                        }

                        if (ChkDate.TENTATIVE_END_DATE == null)
                        {
                            flagRequired = true;
                            RequiredColumn = RequiredColumn + " ,TENTATIVE_END_DATE ";
                        }

                        if (aPPROVAL_TASK_INITIATION.TENTATIVE_START_DATE >= ChkDate.TENTATIVE_START_DATE)
                        {
                            flagRequired = true;
                            RequiredColumn = RequiredColumn + " ,TENTATIVE_START_DATE ";
                        }
                        if (aPPROVAL_TASK_INITIATION.TENTATIVE_END_DATE <= ChkDate.TENTATIVE_END_DATE)
                        {
                            flagRequired = true;
                            RequiredColumn = RequiredColumn + " ,TENTATIVE_END_DATE ";
                        }
                    }
                    else
                    {
                        flagRequired = true;
                    }
                }

                if (flagRequired == true)
                {
                    var responseStatus = new ApiResponse<APPROVAL_TASK_INITIATION>
                    {
                        Status = "Error",
                        Message = "Please enter the details of Approval Task Initiation " + RequiredColumn,
                        Data = aPPROVAL_TASK_INITIATION // No data in case of exception
                    };
                    return Ok(responseStatus);
                }
                else
                {
                    var model = await _repository.CreateTaskApprovalTemplateAsync(aPPROVAL_TASK_INITIATION);
                    if (model == null || model.ResponseStatus == "Error")
                    {
                        var responseStatus = new ApiResponse<APPROVAL_TASK_INITIATION>
                        {
                            Status = "Error",
                            Message = model.Message,
                            Data = aPPROVAL_TASK_INITIATION // No data in case of exception
                        };
                        return Ok(responseStatus);
                    }
                    var response = new ApiResponse<APPROVAL_TASK_INITIATION>
                    {
                        Status = "Ok",
                        Message = "Inserted Successfully",
                        Data = aPPROVAL_TASK_INITIATION // No data in case of exception
                    };
                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<APPROVAL_TASK_INITIATION>
                {
                    Status = "Error",
                    Message = ex.Message,
                    Data = aPPROVAL_TASK_INITIATION // No data in case of exception
                };
                return Ok(response);
            }
        }

        [HttpPut("Put-Approval-Template-Subtask")]
        [Authorize]
        public async Task<IActionResult> UpdateApprovalSubtask(APPROVAL_TASK_INITIATION_TRL_SUBTASK aPPROVAL_TASK_INITIATION_TRL_SUBTASK)
        {
            try
            {
                bool flagSeq_no = false, flagRequired = false;
                double IndexSeq_NO = 0.0;
                string RequiredColumn = string.Empty;

                if (aPPROVAL_TASK_INITIATION_TRL_SUBTASK == null)
                {
                    var responseStatus = new ApiResponse<APPROVAL_TASK_INITIATION_TRL_SUBTASK>
                    {
                        Status = "Error",
                        Message = "Please enter the details of Approval Task Initiation " + RequiredColumn,
                        Data = null // No data in case of exception
                    };
                    return Ok(responseStatus);
                }
                else
                {
                    var model = await _repository.UpdateApprovalSubtask(aPPROVAL_TASK_INITIATION_TRL_SUBTASK);
                    if (model == null)
                    {
                        var responseStatus = new ApiResponse<APPROVAL_TASK_INITIATION_TRL_SUBTASK>
                        {
                            Status = "Error",
                            Message = model.Message,
                            Data = null // No data in case of exception
                        };
                        return Ok(responseStatus);
                    }
                    else if (model.TRLStatus.ToString().ToLower() != "Ok".ToString().ToLower())
                    {
                        var responseStatus = new ApiResponse<APPROVAL_TASK_INITIATION_TRL_SUBTASK>
                        {
                            Status = "Error",
                            Message = model.Message,
                            Data = null // No data in case of exception
                        };
                        return Ok(responseStatus);
                    }

                    var response = new ApiResponse<APPROVAL_TASK_INITIATION_TRL_SUBTASK>
                    {
                        Status = "Ok",
                        Message = "Update Successfully",
                        Data = model // No data in case of exception
                    };
                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<APPROVAL_TASK_INITIATION_TRL_SUBTASK>
                {
                    Status = "Error",
                    Message = ex.Message,
                    Data = aPPROVAL_TASK_INITIATION_TRL_SUBTASK // No data in case of exception
                };
                return Ok(response);
            }
        }
    }
}
