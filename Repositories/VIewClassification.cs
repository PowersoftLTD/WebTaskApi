using System;
using System.Data;
using System.Linq;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using TaskManagement.API.Interfaces;
using TaskManagement.API.Model;

namespace TaskManagement.API.Repositories
{
    public class VIewClassification : IViewClassification
    {
        public IDapperDbConnection _dapperDbConnection;
        public VIewClassification(IDapperDbConnection dapperDbConnection)
        {
            _dapperDbConnection = dapperDbConnection;
        }
        public async Task<IEnumerable<V_Building_Classification>> GetViewBuildingClassificationAsync()
        {
            using (IDbConnection db = _dapperDbConnection.CreateConnection())
            {
                return await db.QueryAsync<V_Building_Classification>("SELECT * FROM V_Building_Classification");
            }
        }
        public async Task<IEnumerable<V_Building_Classification>> GetViewDoc_TypeAsync()
        {
            using (IDbConnection db = _dapperDbConnection.CreateConnection())
            {
                return await db.QueryAsync<V_Building_Classification>("SELECT * FROM V_Doc_Type");
            }
        }
        public async Task<IEnumerable<V_Building_Classification>> GetViewDoc_Type_CheckListAsync()
        {
            using (IDbConnection db = _dapperDbConnection.CreateConnection())
            {
                return await db.QueryAsync<V_Building_Classification>("SELECT * FROM V_Doc_Type_CHECK_LIST");
            }
        }
        public async Task<IEnumerable<V_Instruction>> GetAllInstruction()
        {
            using (IDbConnection db = _dapperDbConnection.CreateConnection())
            {
                return await db.QueryAsync<V_Instruction>("SELECT * FROM V_Instruction ORDER BY MKEY ASC;");
            }
        }
        public async Task<IEnumerable<V_Building_Classification>> GetViewStandard_TypeAsync()
        {
            using (IDbConnection db = _dapperDbConnection.CreateConnection())
            {
                return await db.QueryAsync<V_Building_Classification>("SELECT * FROM V_Standard_Type");
            }
        }
        public async Task<IEnumerable<V_Building_Classification>> GetViewStatutory_AuthAsync()
        {
            using (IDbConnection db = _dapperDbConnection.CreateConnection())
            {
                return await db.QueryAsync<V_Building_Classification>("SELECT * FROM V_Statutory_Auth");
            }
        }
        public async Task<IEnumerable<V_Building_Classification>> GetViewJOB_ROLEAsync()
        {
            using (IDbConnection db = _dapperDbConnection.CreateConnection())
            {
                return await db.QueryAsync<V_Building_Classification>("SELECT * FROM V_JOB_ROLE");
            }
        }
        public async Task<IEnumerable<V_Building_Classification>> GetViewDepartment()
        {
            using (IDbConnection db = _dapperDbConnection.CreateConnection())
            {
                return await db.QueryAsync<V_Building_Classification>("SELECT * FROM V_Department");
            }
        }
        public async Task<IEnumerable<V_Building_Classification>> GetViewResponsibleDepartment()
        {
            using (IDbConnection db = _dapperDbConnection.CreateConnection())
            {
                return await db.QueryAsync<V_Building_Classification>("SELECT * FROM V_ResponsibleDepartment");
            }
        }
        public async Task<IEnumerable<RAISED_AT_OUTPUT_LIST>> GetRaiseATAsync(RAISED_AT_INPUT rAISED_AT_INPUT)
        {
            try
            {
                using (IDbConnection db = _dapperDbConnection.CreateConnection())
                {
                    //var GetRaisetAT = await db.QueryAsync<RAISED_AT_OUTPUT>("SELECT * FROM V_RaisedAT " +
                    //     "WHERE BUILDING_MKEY in (-1,@BUILDING_MKEY) AND PROPERTY in (-1,@PROPERTY_MKEY);", new { BUILDING_MKEY = rAISED_AT_INPUT.BUILDING_MKEY, PROPERTY_MKEY = rAISED_AT_INPUT.PROPERTY_MKEY });

                    var parameters = new DynamicParameters();
                    parameters.Add("@PROPERTY_MKEY", rAISED_AT_INPUT.PROPERTY_MKEY);
                    parameters.Add("@BUILDING_MKEY", rAISED_AT_INPUT.BUILDING_MKEY);

                    var GetRaisetAT = await db.QueryAsync<RAISED_AT_OUTPUT>("SP_GET_RAISED_AT", parameters, commandType: CommandType.StoredProcedure);

                    if (GetRaisetAT != null)
                    {
                        var successsResult = new List<RAISED_AT_OUTPUT_LIST>
                            {
                                new RAISED_AT_OUTPUT_LIST
                                {
                                    Status = "Ok",
                                    Message = "Message",
                                    Data= GetRaisetAT

                                }
                            };
                        return successsResult;
                    }
                    else
                    {
                        var ErrorResponse = new List<RAISED_AT_OUTPUT_LIST>
                            {
                                new RAISED_AT_OUTPUT_LIST
                                {
                                    Status = "Ok",
                                    Message = "Message",
                                    Data= null

                                }
                            };
                        return ErrorResponse;
                    }
                }
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
        public async Task<IEnumerable<RAISED_AT_OUTPUT_LIST>> GetRaiseATBeforeAsync(RAISED_AT_INPUT rAISED_AT_INPUT)
        {
            try
            {
                using (IDbConnection db = _dapperDbConnection.CreateConnection())
                {
                    //var GetRaisetAT = await db.QueryAsync<RAISED_AT_OUTPUT>("SELECT * FROM V_RaisedAT " +
                    //     "WHERE BUILDING_MKEY in (-1,@BUILDING_MKEY) AND PROPERTY in (-1,@PROPERTY_MKEY);", new { BUILDING_MKEY = rAISED_AT_INPUT.BUILDING_MKEY, PROPERTY_MKEY = rAISED_AT_INPUT.PROPERTY_MKEY });

                    var parameters = new DynamicParameters();
                    parameters.Add("@PROPERTY_MKEY", rAISED_AT_INPUT.PROPERTY_MKEY);
                    parameters.Add("@BUILDING_MKEY", rAISED_AT_INPUT.BUILDING_MKEY);

                    var GetRaisetAT = await db.QueryAsync<RAISED_AT_OUTPUT>("SP_GET_RAISED_AT_BEFORE", parameters, commandType: CommandType.StoredProcedure);

                    if (GetRaisetAT != null)
                    {
                        var successsResult = new List<RAISED_AT_OUTPUT_LIST>
                            {
                                new RAISED_AT_OUTPUT_LIST
                                {
                                    Status = "Ok",
                                    Message = "Message",
                                    Data= GetRaisetAT
                                }
                            };
                        return successsResult;
                    }
                    else
                    {
                        var ErrorResponse = new List<RAISED_AT_OUTPUT_LIST>
                            {
                                new RAISED_AT_OUTPUT_LIST
                                {
                                    Status = "Ok",
                                    Message = "Message",
                                    Data= null

                                }
                            };
                        return ErrorResponse;
                    }
                }
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
        public async Task<IEnumerable<V_Building_Classification>> GetViewSanctioningAuthority()
        {
            using (IDbConnection db = _dapperDbConnection.CreateConnection())
            {
                return await db.QueryAsync<V_Building_Classification>("SELECT * FROM V_Sanctioning_Authority");
            }
        }
        public async Task<IEnumerable<V_Building_Classification>> GetViewDocument_Category()
        {
            using (IDbConnection db = _dapperDbConnection.CreateConnection())
            {
                return await db.QueryAsync<V_Building_Classification>("SELECT * FROM V_Doc_Category");
            }
        }
        public async Task<ActionResult<IEnumerable<COMPLIANCE_STATUS_OUTPUT_LIST>>> GetComplianceStatusAsync()
        {
            try
            {
                using (IDbConnection db = _dapperDbConnection.CreateConnection())
                {
                    var GetComplianceStatus = await db.QueryAsync<COMPLIANCE_STATUS_OUTPUT>("SELECT * FROM V_COMPLIANCE_STATUS ");

                    if (!GetComplianceStatus.Any())  // If no records are returned
                    {
                        var ErrorResponse = new List<COMPLIANCE_STATUS_OUTPUT_LIST>
                {
                    new COMPLIANCE_STATUS_OUTPUT_LIST
                    {
                        Status = "Error",
                        Message = "No data found",
                        Data = null
                    }
                };
                        return ErrorResponse;
                    }
                    var SuccessResponse = new List<COMPLIANCE_STATUS_OUTPUT_LIST>
                        {
                            new COMPLIANCE_STATUS_OUTPUT_LIST
                            {
                                Status = "Ok",
                                Message = "Data Get Successfuly",
                                Data= GetComplianceStatus
                            }
                        };
                    return SuccessResponse;
                }
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
        public async Task<ActionResult<IEnumerable<GetTaskTypeList>>> GetTaskTypeAsync()
        {
            try
            {
                using (IDbConnection db = _dapperDbConnection.CreateConnection())
                {
                    var GetComplianceStatus = await db.QueryAsync<GetTaskTypeOutPut>("SELECT * FROM V_TASK_TYPE;");

                    if (!GetComplianceStatus.Any())  // If no records are returned
                    {
                        var ErrorResponse = new List<GetTaskTypeList>
                        {
                            new GetTaskTypeList
                            {
                                Status = "Error",
                                Message = "No data found",
                                Data = null
                            }
                        };
                        return ErrorResponse;
                    }
                    var SuccessResponse = new List<GetTaskTypeList>
                        {
                            new GetTaskTypeList
                            {
                                Status = "Ok",
                                Message = "Data Get Successfuly",
                                Data= GetComplianceStatus
                            }
                        };
                    return SuccessResponse;
                }
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
        public async Task<IEnumerable<EmployeeLoginOutput_LIST>> GetResponsiblePersonByJobRoleDepartmentAsync(RESPONSIBLE_PERSON_INPUT rESPONSIBLE_PERSON_INPUT)
        {
            try
            {
                using (IDbConnection db = _dapperDbConnection.CreateConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@DEPARTMENT_MKEY", rESPONSIBLE_PERSON_INPUT.DEPARTMENT_MKEY);
                    parmeters.Add("@JOB_ROLE_MKEY", rESPONSIBLE_PERSON_INPUT.JOB_ROLE_MKEY);
                    parmeters.Add("@USER_ID", rESPONSIBLE_PERSON_INPUT.USER_ID);
                    var AssignToDetails = await db.QueryAsync<EmployeeLoginOutput>("SP_GET_RESPONSIBLE_PERSON_BY_JOBROLE", parmeters, commandType: CommandType.StoredProcedure);
                    if (AssignToDetails.Any())
                    {

                        var successsResult = new List<EmployeeLoginOutput_LIST>
                    {
                        new EmployeeLoginOutput_LIST
                        {
                            Status = "Ok",
                            Message = "Message",
                            Data= AssignToDetails
                        }
                    };
                        return successsResult;
                    }
                    else
                    {
                        var errorResult = new List<EmployeeLoginOutput_LIST>
                    {
                        new EmployeeLoginOutput_LIST
                        {
                            Status = "Error",
                            Message = "No found",
                            Data=null
                        }
                    };
                        return errorResult;
                    }
                }
            }
            catch (Exception ex)
            {
                var errorResult = new List<EmployeeLoginOutput_LIST>
                    {
                        new EmployeeLoginOutput_LIST
                        {
                            Status = "Error",
                            Message = ex.Message,
                            Data=null
                        }
                    };
                return errorResult;
            }
        }
    }
}
