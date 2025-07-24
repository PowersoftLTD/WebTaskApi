using System.Reflection.Metadata;
using System.Threading.Tasks;
using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.API.Interfaces;
using TaskManagement.API.Model;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TaskManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentTemplateController : ControllerBase
    {
        private readonly IDoc_Temp _repository;
        public DocumentTemplateController(IDoc_Temp repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<DOC_TEMPLATE_HDR>>> GetAllDocumentTemplates(int LoggedIN)
        {
            try
            {
                var Task = await _repository.GetAllDocumentTempAsync(LoggedIN);
                return Ok(Task);
            }
            catch (Exception)
            {
                return new List<DOC_TEMPLATE_HDR>();
            }
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<DOC_TEMPLATE_HDR>> GetDocumentTemplates(int id, int LoggedIN)
        {
            var TASK = await _repository.GetDocumentTempByIdAsync(id, LoggedIN);
            if (TASK == null)
            {
                return NotFound();
            }
            return Ok(TASK);
        }

        [HttpPost("Document-Template-Get-NT")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<DOC_TEMPLATE_HDR_OUTPUT_NT>>> GetDocumentTemplatesNT(DocTemplateGetInputNT docTemplateGetInputNT)
        {
            try
            {
                if (docTemplateGetInputNT == null)
                {
                    var response = new DOC_TEMPLATE_HDR_OUTPUT_NT
                    {
                        STATUS = "Error",
                        MESSAGE = "Please Enter the details",
                        Data = null
                    };
                    return Ok(response);
                }

                var TASK = await _repository.GetDocumentTempByIdAsyncNT(docTemplateGetInputNT);
                return TASK;
            }
            catch (Exception ex)
            {
                var response = new DOC_TEMPLATE_HDR_OUTPUT_NT
                {
                    STATUS = "Error",
                    MESSAGE = ex.Message,
                    Data = null
                };
                return Ok(response);
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<DOC_TEMPLATE_HDR>> CreateTASK(DOC_TEMPLATE_HDR dOC_TEMPLATE_HDR)
        {
            bool flag_check = false;
            string Error_filed = string.Empty;
            try
            {
                if (dOC_TEMPLATE_HDR.DOC_NAME == null)
                {
                    flag_check = true;
                    Error_filed = Error_filed + ", DOC_NAME ";
                }
                if (dOC_TEMPLATE_HDR.DOC_ABBR == null)
                {
                    flag_check = true;
                    Error_filed = Error_filed + ", DOC_ABBR ";
                }
                if (dOC_TEMPLATE_HDR.DOC_CATEGORY == null)
                {
                    flag_check = true;
                    Error_filed = Error_filed + ", DOC_CATEGORY ";
                }
                if (dOC_TEMPLATE_HDR.DOC_NUM_FIELD_NAME == null)
                {
                    flag_check = true;
                    Error_filed = Error_filed + ", DOC_NUM_FIELD_NAME ";
                }

                if (dOC_TEMPLATE_HDR.DOC_NUM_DATE_NAME == null)
                {
                    flag_check = true;
                    Error_filed = Error_filed + ", DOC_NUM_DATE_NAME ";
                }

                if (flag_check == false)
                {
                    var model = await _repository.CreateDocumentTemplateAsync(dOC_TEMPLATE_HDR);
                    if (model == null)
                    {
                        return model;
                    }
                    else
                    {
                        return model;
                    }
                }
                else
                {
                    var model = new DOC_TEMPLATE_HDR();
                    model.Status = "Error";
                    model.Message = "Error occurd";
                    return model;
                }
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.Message);
            }
        }

        [HttpPost("Document-Template-Insert-Update-NT")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<DOC_TEMPLATE_HDR_OUTPUT_NT>>> InsertUpdateDocTemplateNT(DOC_TEMPLATE_HDR_NT_INPUT dOC_TEMPLATE_HDR_NT_INPUT)
        {
            bool flag_check = false;
            string Error_filed = string.Empty;
            try
            {
                if (dOC_TEMPLATE_HDR_NT_INPUT.DOC_NAME == null)
                {
                    flag_check = true;
                    Error_filed = Error_filed + ", DOC_NAME ";
                }
                if (dOC_TEMPLATE_HDR_NT_INPUT.DOC_ABBR == null)
                {
                    flag_check = true;
                    Error_filed = Error_filed + ", DOC_ABBR ";
                }
                if (dOC_TEMPLATE_HDR_NT_INPUT.DOC_CATEGORY == null)
                {
                    flag_check = true;
                    Error_filed = Error_filed + ", DOC_CATEGORY ";
                }
                if (dOC_TEMPLATE_HDR_NT_INPUT.DOC_NUM_FIELD_NAME == null)
                {
                    flag_check = true;
                    Error_filed = Error_filed + ", DOC_NUM_FIELD_NAME ";
                }

                if (dOC_TEMPLATE_HDR_NT_INPUT.DOC_NUM_DATE_NAME == null)
                {
                    flag_check = true;
                    Error_filed = Error_filed + ", DOC_NUM_DATE_NAME ";
                }

                if (flag_check == false)
                {
                    var model = await _repository.InsertUpdateDocTemplateAsyncNT(dOC_TEMPLATE_HDR_NT_INPUT);
                    return model;
                }
                else
                {
                    //var model = new DOC_TEMPLATE_HDR();
                    //model.Status = "Error";
                    //model.Message = "Error occurd";
                    //return model;

                    var response = new DOC_TEMPLATE_HDR_OUTPUT_NT
                    {
                        STATUS = "Error",
                        MESSAGE = "Error occurd",
                        Data = null
                    };
                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
                var response = new DOC_TEMPLATE_HDR_OUTPUT_NT
                {
                    STATUS = "Error",
                    MESSAGE = ex.Message,
                    Data = null
                };
                return Ok(response);
            }
        }

        [HttpPut("{MKEY}")]
        [Authorize]
        public async Task<IActionResult> UpdateTASK(int MKEY, [FromBody] DOC_TEMPLATE_HDR dOC_TEMPLATE_HDR)
        {
            try
            {
                if (dOC_TEMPLATE_HDR == null)
                {
                    var ErrorDoc = new DOC_TEMPLATE_HDR();
                    var response = new ApiResponse<DOC_TEMPLATE_HDR>
                    {
                        Status = "Error",
                        Message = "Error occured",
                        Data = ErrorDoc // No data in case of exception
                    };
                    return Ok(response);
                }
                var TASK = await _repository.GetDocumentTempByIdAsync(MKEY, dOC_TEMPLATE_HDR.CREATED_BY);
                if (TASK == null)
                {
                    var ErrorDoc = new DOC_TEMPLATE_HDR();
                    var response = new ApiResponse<DOC_TEMPLATE_HDR>
                    {
                        Status = "Error",
                        Message = "Error occured",
                        Data = ErrorDoc // No data in case of exception
                    };
                    return Ok(response);
                }
                if (MKEY != dOC_TEMPLATE_HDR.MKEY)
                {
                    var ErrorDoc = new DOC_TEMPLATE_HDR();
                    var response = new ApiResponse<DOC_TEMPLATE_HDR>
                    {
                        Status = "Error",
                        Message = "Doc Mkey missing",
                        Data = ErrorDoc // No data in case of exception
                    };
                    return Ok(response);
                }
                await _repository.UpdateDocumentTemplateAsync(dOC_TEMPLATE_HDR);
                TASK = null;
                TASK = await _repository.GetDocumentTempByIdAsync(MKEY, dOC_TEMPLATE_HDR.CREATED_BY);
                if (TASK == null)
                {
                    return NotFound();
                }

                var responseDelete = new ApiResponse<DOC_TEMPLATE_HDR>
                {
                    Status = "Ok",
                    Message = "Row Deleted",
                    Data = TASK // No data in case of exception
                };
                return Ok(responseDelete);
            }
            catch (Exception ex)
            {
                var ErrorDoc = new DOC_TEMPLATE_HDR();
                var response = new ApiResponse<DOC_TEMPLATE_HDR>
                {
                    Status = "Error",
                    Message = ex.Message,
                    Data = ErrorDoc // No data in case of exception
                };
                return Ok(response);
            }
        }

        [HttpDelete("{id}/{LastUpatedBy}")]
        [Authorize]
        public async Task<IActionResult> DeleteTASK(int id, int LastUpatedBy, int LoggedIN)
        {
            try
            {
                bool deleteTask = await _repository.DeleteDocumentTemplateAsync(id, LastUpatedBy);
                if (deleteTask)
                {
                    var TASK = await _repository.GetDocumentTempByIdAsync(id, LoggedIN);
                    if (TASK == null)
                    {
                        var responseDelete = new ApiResponse<DOC_TEMPLATE_HDR>
                        {
                            Status = "Ok",
                            Message = "Row Deleted",
                            Data = TASK // No data in case of exception
                        };
                        return Ok(responseDelete);
                    }
                }

                var DeleteDoc = new DOC_TEMPLATE_HDR();
                var response = new ApiResponse<DOC_TEMPLATE_HDR>
                {
                    Status = "Ok",
                    Message = "Row Deleted",
                    Data = DeleteDoc // No data in case of exception
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                var ErrorDoc = new DOC_TEMPLATE_HDR();
                var response = new ApiResponse<DOC_TEMPLATE_HDR>
                {
                    Status = "Error",
                    Message = ex.Message,
                    Data = ErrorDoc // No data in case of exception
                };
                return Ok(response);
            }
        }

        [HttpPut("DocumentTemplate-Put-Doc-Category")]
        [Authorize]
        public async Task<DocCategoryOutPut_List> PutDocCategory(DocCategoryUpdateInput docCategoryUpdateInput)
        {
            try
            {
                var InsertDoc_Category = await _repository.UpdateDocumentCategory(docCategoryUpdateInput);

                return InsertDoc_Category;
            }
            catch (Exception ex)
            {
                var response = new DocCategoryOutPut_List
                {
                    Status = "Error",
                    Message = ex.Message,
                    Data = null
                };
                return response;
            }
        }

        [HttpPut("DocumentTemplate-Put-Instruction")]
        [Authorize]
        public async Task<DocCategoryOutPut_List> PutDocCategoryInstruction(UpdateInstructionInput updateInstructionInput)
        {
            try
            {
                var InsertDoc_Category = await _repository.UpdateInstruction(updateInstructionInput);

                return InsertDoc_Category;
            }
            catch (Exception ex)
            {
                var response = new DocCategoryOutPut_List
                {
                    Status = "Error",
                    Message = ex.Message,
                    Data = null
                };
                return response;
            }
        }

        [HttpPost("DocumentTemplate-Insert-Doc-Category")]
        [Authorize]
        public async Task<DocCategoryOutPut_List> InsertDocCategory(DocCategoryInput docCategoryInput)
        {
            try
            {
                var InsertDoc_Category = await _repository.InsertDocumentCategory(docCategoryInput);

                return InsertDoc_Category;
            }
            catch (Exception ex)
            {
                var response = new DocCategoryOutPut_List
                {
                    Status = "Error",
                    Message = ex.Message,
                    Data = null
                };
                return response;
            }
        }

        [HttpPost("DocumentTemplate-Insert-Instruction")]
        [Authorize]
        public async Task<DocCategoryOutPut_List> InsertInstruction(InsertInstructionInput insertInstructionInput)
        {
            bool flagCheck = false;
            string strMessage = string.Empty;

            try
            {
                if (insertInstructionInput.DOC_INSTR == "")
                {
                    flagCheck = true;
                    strMessage = strMessage + " Please enter Doc Instruction, ";
                }
                if (insertInstructionInput.CREATED_BY == 0)
                {
                    flagCheck = true;
                    strMessage = strMessage + " Please enter Created By, ";
                }
                if (insertInstructionInput.COMPANY_ID == 0)
                {
                    flagCheck = true;
                    strMessage = strMessage + " Please enter Company ID ";
                }

                if (flagCheck == true)
                {
                    var ErrorResponse = new DocCategoryOutPut_List
                    {
                        Status = "Error",
                        Message = strMessage,
                        Data = null
                    };
                    return ErrorResponse;

                }
                var InsertDoc_Category = await _repository.InsertInstructionAsyn(insertInstructionInput);

                return InsertDoc_Category;
            }
            catch (Exception ex)
            {
                var response = new DocCategoryOutPut_List
                {
                    Status = "Error",
                    Message = ex.Message,
                    Data = null
                };
                return response;
            }
        }

        [HttpPost("DocumentTemplate-Insert-Instruction-NT")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<DocCategoryOutPutNT>>> InsertInstructionNT(InsertInstructionInputNT insertInstructionInputNT)
        {
            bool flagCheck = false;
            string strMessage = string.Empty;

            try
            {
                if (insertInstructionInputNT.DOC_INSTR == "")
                {
                    flagCheck = true;
                    strMessage = strMessage + " Please enter Doc Instruction, ";
                }
                if (insertInstructionInputNT.CREATED_BY == 0)
                {
                    flagCheck = true;
                    strMessage = strMessage + " Please enter Created By, ";
                }
                if (insertInstructionInputNT.COMPANY_ID == 0)
                {
                    flagCheck = true;
                    strMessage = strMessage + " Please enter Company ID ";
                }

                if (flagCheck == true)
                {
                    var ErrorResult = new List<DocCategoryOutPutNT>
                    {
                        new DocCategoryOutPutNT
                        {
                            Status = "Error",
                            Message = strMessage,
                            Data = null
                        }
                    };
                    return ErrorResult;

                }
                var InsertDoc_Category = await _repository.InsertInstructionAsynNT(insertInstructionInputNT);

                return InsertDoc_Category;
            }
            catch (Exception ex)
            {
                var ErrorResult = new List<DocCategoryOutPutNT>
                {
                    new DocCategoryOutPutNT
                    {
                        Status = "Error",
                        Message = ex.Message,
                        Data = null
                    }
                };
                return ErrorResult;
            }
        }

    }
}
