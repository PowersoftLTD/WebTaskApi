using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using TaskManagement.API.Interfaces;
using TaskManagement.API.Model;

namespace TaskManagement.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserDepartmentReportController : ControllerBase
    {
        //public IActionResult Index()
        //{
        //    return Ok();
        //}

        private readonly IConfiguration _configuration;
        private readonly IUserDepartmentReport _repository;
        public static IWebHostEnvironment _environment;
        private readonly FileSettings _fileSettings;

        public IDapperDbConnection _dapperDbConnection;
        public UserDepartmentReportController(IUserDepartmentReport repository, IConfiguration configuration, IWebHostEnvironment environment, IOptions<FileSettings> fileSettings)
        {
            _repository = repository;
            _configuration = configuration;
            _environment = environment;
            _fileSettings = fileSettings.Value;
        }

        //public IActionResult Index()
        //{
        //    return Ok();
        //}

        [HttpPost("Task-Dashboard-Filter")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<TaskDashBoardFilterOutputListNT>>> TaskDashBoardFilterNT([FromBody] Doc_Type_Doc_CategoryInput doc_Type_Doc_CategoryInput)
        {
            try
            {
                if (doc_Type_Doc_CategoryInput == null)
                {
                    var response = new TaskDashBoardFilterOutputListNT
                    {
                        STATUS = "Error",
                        MESSAGE = "Please Enter the details",
                        User_Filter = null
                    };
                    return Ok(response);
                }

                var RsponseStatus = await _repository.TaskDashBoardFilterAsynNT(doc_Type_Doc_CategoryInput);
                return RsponseStatus;
            }
            catch (Exception ex)
            {
                var response = new TaskDashBoardFilterOutputListNT
                {
                    STATUS = "Error",
                    MESSAGE = ex.Message,
                    User_Filter = null
                };
                return Ok(response);
            }
        }


        [HttpPost("Task-Dashboard_NT")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Task_DetailsOutPutNT_List>>> Task_Details_NT([FromBody] Task_UserDepartmentDetailsInputNT task_UserDepartmentDetailsInputNT)
        {
            try
            {
                var TaskDash = await _repository.GetTaskDetailsNTAsync(task_UserDepartmentDetailsInputNT);
                return Ok(TaskDash);
            }
            catch (Exception ex)
            {
                var response = new Task_DetailsOutPut_List
                {
                    Status = "Error",
                    Message = ex.Message,
                    Data = null,
                    Data1 = null
                };
                return Ok(response);
            }
        }

        [HttpPost("UserDepartmentReport-DepartmentList")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Task_UserDepartmentOutPutNT_List>>> GetDepartmentList_NT()
        {
            try
            {
                var TaskDash = await _repository.GetDepertmentListNTAsyn();
                return Ok(TaskDash);
            }
            catch (Exception ex)
            {
                var response = new Task_UserDepartmentOutPutNT_List
                {
                    Status = "Error",
                    Message = ex.Message,
                    Data = null,
                    //Data1 = null
                };
                return Ok(response);
            }
        }
        [HttpPost("EmployeeList-ByDepartmentId")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Task_UserDepartmentOutPutNT_List>>> GetEmployeeList_ByDepartmentId_NT(string departmentId)
        {
            try
            {
                var TaskDash = await _repository.GetEmployeeDetails_ByDepartmentId(departmentId);
                return Ok(TaskDash);
            }
            catch (Exception ex)
            {
                var response = new Task_UserDepartmentOutPutNT_List
                {
                    Status = "Error",
                    Message = ex.Message,
                    Data = null,
                    //Data1 = null
                };
                return Ok(response);
            }
        }






    }
}
