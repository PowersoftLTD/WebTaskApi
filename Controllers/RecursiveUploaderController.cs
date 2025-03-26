using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TaskManagement.API.Interfaces;
using TaskManagement.API.Model;
using System;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Dapper;
using System.Data;
using Azure;
using Microsoft.Extensions.Options;


namespace TaskManagement.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RecursiveUploaderController : ControllerBase
    {
        public static IWebHostEnvironment _environment;
        private readonly ITASKRepository _repository;
        private readonly FileSettings _fileSettings;
        public RecursiveUploaderController(IWebHostEnvironment environment, ITASKRepository repository, IOptions<FileSettings> fileSettings)
        {
            _environment = environment;
            _repository = repository;
            _fileSettings = fileSettings.Value;
        }

        [Authorize]
        [HttpPost, DisableRequestSizeLimit]
        public async Task<IEnumerable<FileUploadAPIOutPut>> Post([FromForm] FileUploadAPI objFile)
        {
            try
            {
                if (objFile.files != null)
                {
                    string filePathOpen = string.Empty;

                    // Create directory for file storage if it doesn't exist
                    string filePath = _fileSettings.FilePath;
                    string directoryPath = Path.Combine(filePath, "Attachments", objFile.TASK_MKEY.ToString());
                    //string directoryPath1 = Path.Combine(filePath, "Attachments", "Document Depository", objFile.files.FileName.ToString());
                    if (!Directory.Exists(directoryPath))
                    {
                        Directory.CreateDirectory(directoryPath);
                    }

                    // Save the file to the directory
                    string fileName = $"{DateTime.Now.Day}_{DateTime.Now.ToShortTimeString().Replace(":", "_")}_{objFile.files.FileName}";
                    string fullFilePath = Path.Combine(directoryPath, fileName);
                    using (FileStream fileStream = new FileStream(fullFilePath, FileMode.Create))
                    {
                        await (objFile.files.CopyToAsync(fileStream));
                        fileStream.Flush();
                    }

                    filePathOpen = Path.Combine("Attachments", objFile.TASK_MKEY.ToString(), fileName);
                    objFile.FILE_NAME = fileName;// objFile.files.FileName;
                    objFile.FILE_PATH = "\\" + filePathOpen;//  "\\Attachment\\" + objFile.TASK_MKEY;
                    var taskAttach = await _repository.TASKFileUpoadAsync(objFile);
                    return taskAttach;
                }
                else
                {
                    var ErroResult = new List<FileUploadAPIOutPut>
                        {
                        new FileUploadAPIOutPut
                            {
                            Status = "Error",
                            Message = "Not found"
                            }
                    };
                    return ErroResult;
                }

            }
            catch (Exception ex)
            {
                var ErroResult = new List<FileUploadAPIOutPut>
                        {
                        new FileUploadAPIOutPut
                            {
                            Status = "Error",
                            Message = ex.Message
                            }
                    };
                return ErroResult;
            }
        }
    }
}
