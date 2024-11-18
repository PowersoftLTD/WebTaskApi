using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TaskManagement.API.Interfaces;
//using static Azure.Core.HttpHeader;
using TaskManagement.API.Model;
using System;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;


namespace TaskManagement.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RecursiveUploaderController : ControllerBase
    {
        public static IWebHostEnvironment _environment;
        private readonly ITASKRepository _repository;

        public RecursiveUploaderController(IWebHostEnvironment environment, ITASKRepository repository)
        {
            _environment = environment;
            _repository = repository;
        }
        [Authorize]
        [HttpPost, DisableRequestSizeLimit]
        public async Task<IActionResult> Post([FromForm] FileUploadAPI objFile)
        {
            try
            {
                if (objFile.files.Length > 0)
                {
                    if (!Directory.Exists(objFile.FILE_PATH + "\\Attachment\\" + objFile.TASK_MKEY))
                    {
                        Directory.CreateDirectory(objFile.FILE_PATH + "\\Attachment\\" + objFile.TASK_MKEY);
                    }
                    using (FileStream filestream = System.IO.File.Create(objFile.FILE_PATH + "\\Attachment\\" + objFile.TASK_MKEY + "\\" + objFile.files.FileName))
                    {
                        objFile.files.CopyTo(filestream);
                        filestream.Flush();
                    }
                    objFile.FILE_NAME = objFile.files.FileName;
                    objFile.FILE_PATH = objFile.FILE_PATH + "\\Attachment\\" + objFile.TASK_MKEY;
                    await _repository.TASKFileUpoadAsync(objFile);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return Ok(objFile);
        }
    }
}
