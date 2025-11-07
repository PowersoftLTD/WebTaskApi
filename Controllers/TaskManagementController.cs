using TaskManagement.API.Interfaces;
using TaskManagement.API.Model;
using TaskManagement.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace TaskManagement.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TaskManagementController : ControllerBase
    {
        private readonly ITASKRepository _repository;
        public static IWebHostEnvironment _environment;
        public TaskManagementController(ITASKRepository repository, IWebHostEnvironment environment)
        {
            _repository = repository;
            _environment = environment;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<TASK_RECURSIVE_HDR>>> GetTask()
        {
            try
            {
                var Task = await _repository.GetAllTASKsAsync();
                return Ok(Task);
            }
            catch (Exception)
            {
                return new List<TASK_RECURSIVE_HDR>();
            }
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<TASK_RECURSIVE_HDR>> GetTask(int id)
        {
            var TASK = await _repository.GetTaskByIdAsync(id);
            if (TASK == null)
            {
                return NotFound();
            }
            return Ok(TASK);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<TASK_RECURSIVE_HDR>> CreateTASK(TASK_RECURSIVE_HDR tASK_RECURSIVE_HDR)
        {
            try
            {
                var model = await _repository.CreateTASKAsync(tASK_RECURSIVE_HDR);
                if (model == null)
                {
                    return StatusCode(500);
                }
                else
                {
                    return model;
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{MKEY}")]
        [Authorize]
        public async Task<IActionResult> UpdateTASK(int MKEY, [FromBody] TASK_RECURSIVE_HDR tASK_RECURSIVE_HDR)
        {
            try
            {
                var TASK = await _repository.GetTaskByIdAsync(MKEY);
                if (TASK == null)
                {
                    return NotFound();
                }
                if (MKEY != tASK_RECURSIVE_HDR.MKEY)
                {
                    return BadRequest();
                }
                await _repository.UpdateTASKAsync(tASK_RECURSIVE_HDR);
                TASK = null;
                TASK = await _repository.GetTaskByIdAsync(MKEY);
                if (TASK == null)
                {
                    return NotFound();
                }
                return Ok(TASK);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}/{LastUpatedBy}")]
        [Authorize]
        public async Task<IActionResult> DeleteTASK(int id, int LastUpatedBy)
        {
            bool deleteTask = await _repository.DeleteTASKAsync(id, LastUpatedBy);
            if (deleteTask)
            {
                var TASK = await _repository.GetTaskByIdAsync(id);
                if (TASK == null)
                {
                    return Ok("Row deleted");
                }
            }
            return NoContent();

        }

        #region
        // Changes Done by Itemad Hyder                                                                                                                                                                                                                                                                                                                                                     

        [HttpPost("GetTask_NT")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<TASK_RECURSIVE_HDR>>> GetTaskNT([FromBody] TASK_DETAILS_BY_MKEYInput_NT dETAILS_BY_MKEYInput_NT)
        {
            try
            {
                var Task = await _repository.GetTaskDetailsByMkeyNTAsync(dETAILS_BY_MKEYInput_NT);
                return Ok(Task);
            }
            catch (Exception)
            {
                return new List<TASK_RECURSIVE_HDR>();
            }
        }

        [HttpPost("Task-Management/CreateRecursive")]
        [Authorize]
        public async Task<ActionResult<Add_TaskOutPut_List>> CreateRecursiveTASK(TASK_RECURSIVE_HDR tASK_RECURSIVE_HDR)
        {
            try
            {
                var model = await _repository.CreateRecursiveTASKAsync(tASK_RECURSIVE_HDR);
                if (model == null)
                {
                    return StatusCode(500);
                }
                else
                {
                    var response = new Add_Recursive_TaskOutPut_List
                    {
                        Status = model.Status,
                        Message = model.Message,
                        Data = model.Data
                    };
                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("Update_RecursiveTask")]
        [Authorize]
        public async Task<IActionResult> Update_RecursiveTask(int MKEY, [FromBody] TASK_RECURSIVE_HDR tASK_RECURSIVE_HDR)
        {
            try
            {
                var TASK = await _repository.GetTaskByIdAsync(MKEY);
                if (TASK == null)
                {
                    return NotFound();
                }
                if (MKEY != tASK_RECURSIVE_HDR.MKEY)
                {
                    return BadRequest();
                }
                var response = await _repository.UpdateRecuriveTASKAsync(tASK_RECURSIVE_HDR);
                TASK = null;
                TASK = await _repository.GetTaskByIdAsync(MKEY);
                if (TASK == null)
                {
                    return NotFound();
                }
                return Ok(TASK);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize]
        [HttpPost("Task-Management/FileUpload_NT"), DisableRequestSizeLimit]

        public async Task<ActionResult<Add_RecursiveTaskOutPut_List_NT>> Post([FromForm] RecursiveTaskFileUploadAPI_NT objFile)
        {
            try
            {
                int srNo = 0;
                string filePathOpen = string.Empty;
                bool flagAttachment = false;
                if (objFile.files == null)
                {
                    //var ResultCount = await _repository.UpdateTASKFileUpoadAsync(objFile.CREATED_BY.ToString(), objFile.TASK_MKEY.ToString(), objFile.DELETE_FLAG.ToString());
                    var ResultCount = await _repository.UpdateDELETETASKFileUpoadAsync(objFile.CREATED_BY.ToString(), objFile.TASK_MKEY.ToString(), objFile.DELETE_FLAG.ToString(), objFile.Sr_No.ToString());
                    if (ResultCount.Status.Contains("Success"))
                    {

                        var response = new Add_RecursiveTaskOutPut_List_NT
                        {
                            Status = "Ok",
                            Message = "File is deleted!",
                            Data2 = objFile
                        };
                        return Ok(response);
                    }
                }

                foreach (var TaskFiles in objFile.files)
                {
                    if (TaskFiles.Length > 0)
                    {
                        if (objFile.Sr_No != 0)
                        {
                            srNo = Convert.ToInt32(objFile.Sr_No);
                        }
                        //objFile.FILE_PATH = "D:\\DATA\\Projects\\Task_Mangmt\\Task_Mangmt\\Task\\";
                        var RsponseStatus = await _repository.FileDownload();
                        string FilePath = RsponseStatus.Value;
                        if (!Directory.Exists(FilePath + "\\Attachments\\" + objFile.TASK_MKEY))
                        {
                            Directory.CreateDirectory(FilePath + "\\Attachments\\" + objFile.TASK_MKEY);
                        }
                        using (FileStream filestream = System.IO.File.Create(FilePath + "\\Attachments\\" + objFile.TASK_MKEY + "\\" + DateTime.Now.Day + "_" + DateTime.Now.ToShortTimeString().Replace(":", "_") + "_" + TaskFiles.FileName))
                        {
                            TaskFiles.CopyTo(filestream);
                            filestream.Flush();
                        }

                        filePathOpen = "Attachments\\" + objFile.TASK_MKEY + "\\" + DateTime.Now.Day + "_" + DateTime.Now.ToShortTimeString().Replace(":", "_") + "_" + TaskFiles.FileName;
                        var ResultCount = await _repository.TASKFileUpoadNTAsync(srNo, objFile.TASK_MKEY, objFile.TASK_PARENT_ID, TaskFiles.FileName, filePathOpen, objFile.CREATED_BY, Convert.ToChar(objFile.DELETE_FLAG), objFile.TASK_MAIN_NODE_ID);   // , objFile.TASK_PARENT_ID , objFile.TASK_MAIN_NODE_ID
                        FilePath = filePathOpen;
                        objFile.FILE_NAME = TaskFiles.FileName;
                        objFile.FILE_PATH = filePathOpen;
                        if (ResultCount.Value.Status != "Success")
                        {
                            var Successresponse = new Add_RecursiveTaskOutPut_List_NT
                            {
                                Status = "Error",
                                Message = "File Uploaded Failed",
                                Data2 = objFile
                            };
                            flagAttachment = false;
                            return Ok(Successresponse);
                        }
                        else
                        {
                            flagAttachment = true;
                        }
                    }
                }

                if (flagAttachment == true)
                {
                    //var ResultCount = await _repository.UpdateTASKFileUpoadAsync(objFile.CREATED_BY.ToString(), objFile.TASK_MKEY.ToString(), objFile.DELETE_FLAG.ToString());
                    var Successresponse = new Add_RecursiveTaskOutPut_List_NT
                    {
                        Status = "ok",
                        Message = "File Uploaded",
                        Data2 = objFile
                    };
                    flagAttachment = true;
                    return Ok(Successresponse);
                }
                else
                {
                    var response = new Add_RecursiveTaskOutPut_List_NT
                    {
                        Status = "Error",
                        Message = "please attach the file!!!",
                        Data1 = null
                    };
                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
                var response = new Add_RecursiveTaskOutPut_List_NT
                {
                    Status = "Error",
                    Message = ex.Message,
                    Data1 = null
                };
                return Ok(response);
            }
        }


        //public async Task<ActionResult<Add_TaskOutPut_List_NT>> Post([FromForm] RecursiveTaskFileUploadAPI_NT objFile)
        //{
        //    try
        //    {
        //        int srNo = 0;
        //        string filePathOpen = string.Empty;
        //        bool flagAttachment = false;
        //        if (objFile.files == null)
        //        {
        //            var ResultCount = await _repository.UpdateTASKFileUpoadAsync(objFile.CREATED_BY.ToString(), objFile.TASK_MKEY.ToString(), objFile.DELETE_FLAG.ToString());
        //            if (ResultCount.Status.Contains("Success"))
        //            {
        //                var response = new Add_TaskOutPut_List_NT
        //                {
        //                    Status = "Ok",
        //                    Message = "File is deleted!!!",
        //                    Data1 = null
        //                };
        //                return Ok(response);
        //            }
        //        }

        //        foreach (var TaskFiles in objFile.files)
        //        {
        //            if (TaskFiles.Length > 0)
        //            {
        //                if (objFile.Sr_No != 0)
        //                {
        //                    srNo = Convert.ToInt32(objFile.Sr_No);
        //                }
        //                //objFile.FILE_PATH = "D:\\DATA\\Projects\\Task_Mangmt\\Task_Mangmt\\Task\\";
        //                var RsponseStatus = await _repository.FileDownload();
        //                string FilePath = RsponseStatus.Value;
        //                if (!Directory.Exists(FilePath + "\\Attachments\\" + objFile.TASK_MKEY))
        //                {
        //                    Directory.CreateDirectory(FilePath + "\\Attachments\\" + objFile.TASK_MKEY);
        //                }
        //                using (FileStream filestream = System.IO.File.Create(FilePath + "\\Attachments\\" + objFile.TASK_MKEY + "\\" + DateTime.Now.Day + "_" + DateTime.Now.ToShortTimeString().Replace(":", "_") + "_" + TaskFiles.FileName))
        //                {
        //                    TaskFiles.CopyTo(filestream);
        //                    filestream.Flush();
        //                }

        //                filePathOpen = "Attachments\\" + objFile.TASK_MKEY + "\\" + DateTime.Now.Day + "_" + DateTime.Now.ToShortTimeString().Replace(":", "_") + "_" + TaskFiles.FileName;
        //                var ResultCount = await _repository.TASKFileUpoadNTAsync(srNo, objFile.TASK_MKEY, objFile.TASK_PARENT_ID, TaskFiles.FileName, filePathOpen, objFile.CREATED_BY, Convert.ToChar(objFile.DELETE_FLAG), objFile.TASK_MAIN_NODE_ID);   // , objFile.TASK_PARENT_ID , objFile.TASK_MAIN_NODE_ID
        //                FilePath = filePathOpen;
        //                objFile.FILE_NAME = TaskFiles.FileName;
        //                objFile.FILE_PATH = filePathOpen;
        //                if (ResultCount != null & ResultCount.Value.Status != "Error")
        //                {
        //                    var Successresponse = new Add_RecursiveTaskOutPut_List_NT
        //                    {
        //                        Status = "ok",
        //                        Message = "File Uploaded",
        //                        Data2 = objFile
        //                    };
        //                    flagAttachment = true;
        //                    // return Ok(Successresponse);
        //                }
        //                else
        //                {
        //                    var Errorresponse = new Add_RecursiveTaskOutPut_List_NT
        //                    {
        //                        Status = "Error",
        //                        Message = "Error occurred",
        //                        Data1 = null
        //                    };
        //                    return Ok(Errorresponse);
        //                }
        //            }
        //        }

        //        if (flagAttachment == true)
        //        {
        //            var ResultCount = await _repository.UpdateTASKFileUpoadAsync(objFile.CREATED_BY.ToString(), objFile.TASK_MKEY.ToString(), objFile.DELETE_FLAG.ToString());
        //            var Successresponse = new Add_RecursiveTaskOutPut_List_NT
        //            {
        //                Status = "ok",
        //                Message = "File Uploaded",
        //                Data2 = objFile
        //            };
        //            flagAttachment = true;
        //            return Ok(Successresponse);
        //        }
        //        else
        //        {
        //            var response = new Add_RecursiveTaskOutPut_List_NT
        //            {
        //                Status = "Error",
        //                Message = "please attach the file!!!",
        //                Data1 = null
        //            };
        //            return Ok(response);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        var response = new Add_RecursiveTaskOutPut_List_NT
        //        {
        //            Status = "Error",
        //            Message = ex.Message,
        //            Data1 = null
        //        };
        //        return Ok(response);
        //    }
        //}

        #endregion
    }
}
