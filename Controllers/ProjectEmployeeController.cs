using System.Data;
using Dapper;
using System.Reflection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.API.Interfaces;
using TaskManagement.API.Model;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Microsoft.AspNetCore.Authorization;
using System;

namespace TaskManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProjectEmployeeController : ControllerBase
    {
        public IDapperDbConnection _dapperDbConnection;
        public ProjectEmployeeController(IDapperDbConnection dapperDbConnection)
        {
            _dapperDbConnection = dapperDbConnection;
        }

        [HttpGet("Task-Management/Get-Option")]
        [Authorize]
        public ActionResult<IEnumerable<dynamic>> Get_Project(string TYPE_CODE, string MASTER_MKEY)
        {
            try
            {
                using (IDbConnection db = _dapperDbConnection.CreateConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@TYPE_CODE", TYPE_CODE);
                    parmeters.Add("@MASTER_MKEY", MASTER_MKEY);
                    var ProjectDetails = db.Query("SP_GET_PROJECT", parmeters, commandType: CommandType.StoredProcedure).ToList();

                    return Ok(ProjectDetails);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpGet("Task-Management/Get-Sub_Project")]
        [Authorize]
        public ActionResult<IEnumerable<dynamic>> Get_Sub_Project(string Project_Mkey)
        {
            try
            {
                using (IDbConnection db = _dapperDbConnection.CreateConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@PROJECT_MKEY", Project_Mkey);
                    var ProjectDetails = db.Query("SP_GET_SUBPROJECT", parmeters, commandType: CommandType.StoredProcedure).ToList();
                    return Ok(ProjectDetails);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpGet("Task-Management/Get-Emp")]
        [Authorize]
        public ActionResult<IEnumerable<dynamic>> Get_Emp(string CURRENT_EMP_MKEY, string FILTER)
        {
            try
            {
                using (IDbConnection db = _dapperDbConnection.CreateConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@CURRENT_EMP_MKEY", CURRENT_EMP_MKEY);
                    parmeters.Add("@FILTER", FILTER);
                    var ProjectDetails = db.Query("SP_GET_EMP", parmeters, commandType: CommandType.StoredProcedure).ToList();
                    return Ok(ProjectDetails);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpGet("Task-Management/Assigned_To")]
        [Authorize]
        public ActionResult<IEnumerable<dynamic>> AssignedTo(string AssignNameLike)
        {
            try
            {
                using (IDbConnection db = _dapperDbConnection.CreateConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@term", AssignNameLike);
                    var ProjectDetails = db.Query("SP_AssignedTo", parmeters, commandType: CommandType.StoredProcedure).ToList();
                    return Ok(ProjectDetails);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpGet("Task-Management/EMP_TAGS")]
        [Authorize]
        public ActionResult<IEnumerable<dynamic>> EMP_TAGS(string EMP_TAGS)
        {
            try
            {
                using (IDbConnection db = _dapperDbConnection.CreateConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@EMP_MKEY", EMP_TAGS);
                    var ewmployeeMkey = db.Query("sp_EMP_TAGS", parmeters, commandType: CommandType.StoredProcedure).ToList();
                    return Ok(ewmployeeMkey);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpGet("Task-Management/Add-Task")]
        [Authorize]
        public ActionResult<IEnumerable<dynamic>> Add_Task(string TASK_NO, string TASK_NAME, string TASK_DESCRIPTION,
            string CATEGORY, string PROJECT_ID, string SUBPROJECT_ID, string COMPLETION_DATE, string ASSIGNED_TO,
            string TAGS, string ISNODE, string START_DATE, string CLOSE_DATE, string DUE_DATE, string TASK_PARENT_ID,
            string STATUS, string STATUS_PERC, string TASK_CREATED_BY, string APPROVER_ID, string IS_ARCHIVE, string ATTRIBUTE1,
            string ATTRIBUTE2, string ATTRIBUTE3, string ATTRIBUTE4, string ATTRIBUTE5, string CREATED_BY, string CREATION_DATE,
            string LAST_UPDATED_BY, string APPROVE_ACTION_DATE)
        {
            try
            {
                if (TASK_NO == "0000")
                {
                    using (IDbConnection db = _dapperDbConnection.CreateConnection())
                    {
                        var parmeters = new DynamicParameters();
                        parmeters.Add("@TASK_NO", TASK_NO);
                        parmeters.Add("@TASK_NAME", TASK_NAME);
                        parmeters.Add("@TASK_DESCRIPTION", TASK_DESCRIPTION);
                        parmeters.Add("@CATEGORY", CATEGORY);
                        parmeters.Add("@PROJECT_ID", PROJECT_ID);
                        parmeters.Add("@SUBPROJECT_ID", SUBPROJECT_ID);
                        parmeters.Add("@COMPLETION_DATE", COMPLETION_DATE);
                        parmeters.Add("@ASSIGNED_TO", ASSIGNED_TO);
                        parmeters.Add("@TAGS", TAGS);
                        parmeters.Add("@ISNODE", ISNODE);
                        parmeters.Add("@CLOSE_DATE", CLOSE_DATE);
                        parmeters.Add("@DUE_DATE", DUE_DATE);
                        parmeters.Add("@TASK_PARENT_ID", TASK_PARENT_ID);
                        parmeters.Add("@STATUS", STATUS);
                        parmeters.Add("@STATUS_PERC", STATUS_PERC);
                        parmeters.Add("@TASK_CREATED_BY", TASK_CREATED_BY);
                        parmeters.Add("@APPROVER_ID", APPROVER_ID);
                        parmeters.Add("@IS_ARCHIVE", IS_ARCHIVE);
                        parmeters.Add("@ATTRIBUTE1", ATTRIBUTE1);
                        parmeters.Add("@ATTRIBUTE2", ATTRIBUTE2);
                        parmeters.Add("@ATTRIBUTE3", ATTRIBUTE3);
                        parmeters.Add("@ATTRIBUTE4", ATTRIBUTE4);
                        parmeters.Add("@ATTRIBUTE5", ATTRIBUTE5);
                        parmeters.Add("@CREATED_BY", CREATED_BY);
                        parmeters.Add("@CREATION_DATE", CREATION_DATE);
                        parmeters.Add("@LAST_UPDATED_BY", LAST_UPDATED_BY);
                        parmeters.Add("@APPROVE_ACTION_DATE", APPROVE_ACTION_DATE);
                        var ewmployeeMkey = db.Query("Sp_insert_task_details", parmeters, commandType: CommandType.StoredProcedure).ToList();
                        return Ok(ewmployeeMkey);
                    }
                }
                else
                {
                    using (IDbConnection db = _dapperDbConnection.CreateConnection())
                    {
                        var parmeters = new DynamicParameters();
                        parmeters.Add("@EMP_MKEY", EMP_TAGS);
                        var ewmployeeMkey = db.Query("sp_EMP_TAGS", parmeters, commandType: CommandType.StoredProcedure).ToList();
                        return Ok(ewmployeeMkey);
                    }
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }


        [HttpGet("Task-Management/Add-Sub-Task")]
        [Authorize]
        public ActionResult<IEnumerable<dynamic>> Add_Sub_Task(string TASK_NO, string TASK_NAME, string TASK_DESCRIPTION, string CATEGORY, string PROJECT_ID, string SUBPROJECT_ID, string COMPLETION_DATE, string ASSIGNED_TO, string TAGS, string ISNODE, string START_DATE, string CLOSE_DATE, string DUE_DATE, string TASK_PARENT_ID, string TASK_PARENT_NODE_ID, string TASK_PARENT_NUMBER, string STATUS, string STATUS_PERC, string TASK_CREATED_BY, string APPROVER_ID, string IS_ARCHIVE, string ATTRIBUTE1, string ATTRIBUTE2, string ATTRIBUTE3, string ATTRIBUTE4, string ATTRIBUTE5, string CREATED_BY, string CREATION_DATE, string LAST_UPDATED_BY, string APPROVE_ACTION_DATE, string Current_task_mkey)
        {
            try
            {
                if (TASK_NO == "0000")
                {
                    using (IDbConnection db = _dapperDbConnection.CreateConnection())
                    {
                        var parmeters = new DynamicParameters();
                        parmeters.Add("@TASK_NO", TASK_NO);
                        parmeters.Add("@TASK_NAME", TASK_NAME);
                        parmeters.Add("@TASK_DESCRIPTION", TASK_DESCRIPTION);
                        parmeters.Add("@CATEGORY", CATEGORY);
                        parmeters.Add("@PROJECT_ID", PROJECT_ID);
                        parmeters.Add("@SUBPROJECT_ID", SUBPROJECT_ID);
                        parmeters.Add("@COMPLETION_DATE", COMPLETION_DATE);
                        parmeters.Add("@ASSIGNED_TO", ASSIGNED_TO);
                        parmeters.Add("@TAGS", TAGS);
                        parmeters.Add("@ISNODE", ISNODE);
                        parmeters.Add("@CLOSE_DATE", CLOSE_DATE);
                        parmeters.Add("@DUE_DATE", DUE_DATE);
                        parmeters.Add("@TASK_PARENT_ID", TASK_PARENT_ID);
                        parmeters.Add("@STATUS", STATUS);
                        parmeters.Add("@STATUS_PERC", STATUS_PERC);
                        parmeters.Add("@TASK_CREATED_BY", TASK_CREATED_BY);
                        parmeters.Add("@APPROVER_ID", APPROVER_ID);
                        parmeters.Add("@IS_ARCHIVE", IS_ARCHIVE);
                        parmeters.Add("@ATTRIBUTE1", ATTRIBUTE1);
                        parmeters.Add("@ATTRIBUTE2", ATTRIBUTE2);
                        parmeters.Add("@ATTRIBUTE3", ATTRIBUTE3);
                        parmeters.Add("@ATTRIBUTE4", ATTRIBUTE4);
                        parmeters.Add("@ATTRIBUTE5", ATTRIBUTE5);
                        parmeters.Add("@CREATED_BY", CREATED_BY);
                        parmeters.Add("@CREATION_DATE", CREATION_DATE);
                        parmeters.Add("@LAST_UPDATED_BY", LAST_UPDATED_BY);
                        parmeters.Add("@APPROVE_ACTION_DATE", APPROVE_ACTION_DATE);
                        var ewmployeeMkey = db.Query("Sp_insert_task_details", parmeters, commandType: CommandType.StoredProcedure).ToList();
                        return Ok(ewmployeeMkey);
                    }
                }
                else
                {
                    using (IDbConnection db = _dapperDbConnection.CreateConnection())
                    {
                        var parmeters = new DynamicParameters();
                        parmeters.Add("@EMP_MKEY", EMP_TAGS);
                        var ewmployeeMkey = db.Query("sp_EMP_TAGS", parmeters, commandType: CommandType.StoredProcedure).ToList();
                        return Ok(ewmployeeMkey);
                    }
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
    }
}
