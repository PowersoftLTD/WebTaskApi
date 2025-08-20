using System;
using System.Data;
using System.Data.SqlClient;
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
        private static TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        public IDapperDbConnection _dapperDbConnection;
        private readonly string _connectionString;
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
        public async Task<IEnumerable<BuildingTypeNT>> GetViewBuildingClassificationNTAsync(ClassificationNT classificationNT)
        {
            try
            {
                using (IDbConnection db = _dapperDbConnection.CreateConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Session_User_Id", classificationNT.Session_User_Id);
                    parmeters.Add("@Business_Group_Id", classificationNT.Business_Group_Id);
                    var BuildingClass = await db.QueryAsync<V_Building_Classification>("SP_GET_BUILDINGCLASS_NT", parmeters, commandType: CommandType.StoredProcedure);
                    if (BuildingClass.Any())
                    {
                        var successsResult = new List<BuildingTypeNT>
                        {
                            new BuildingTypeNT
                            {
                                Status = "Ok",
                                Message = "Message",
                                Data= BuildingClass
                            }
                        };
                        return successsResult;
                    }
                    else
                    {
                        var errorResult = new List<BuildingTypeNT>
                        {
                            new BuildingTypeNT
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
                var errorResult = new List<BuildingTypeNT>
                    {
                        new BuildingTypeNT
                        {
                            Status = "Error",
                            Message = ex.Message,
                            Data = null
                        }
                    };
                return errorResult;
            }
        }
        public async Task<IEnumerable<V_Building_Classification>> GetViewDoc_TypeAsync()
        {
            using (IDbConnection db = _dapperDbConnection.CreateConnection())
            {
                return await db.QueryAsync<V_Building_Classification>("SELECT * FROM V_Doc_Type");
            }
        }
        public async Task<IEnumerable<V_Doc_Type_OutPut_NT>> GetViewDoc_TypeNTAsync(Doc_Type_Doc_CategoryInput doc_Type_Doc_CategoryInput)
        {
            //using (IDbConnection db = _dapperDbConnection.CreateConnection())
            //{
            //    return await db.QueryAsync<V_Building_Classification>("SELECT * FROM V_Doc_Type");
            //}

            try
            {
                using (IDbConnection db = _dapperDbConnection.CreateConnection())
                {
                    var AssignToDetails = await db.QueryAsync<V_Doc_Type_NT>("SELECT * FROM V_Doc_Type_NT", commandType: CommandType.Text);
                    if (AssignToDetails.Any())
                    {

                        var successsResult = new List<V_Doc_Type_OutPut_NT>
                        {
                            new V_Doc_Type_OutPut_NT
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
                        var errorResult = new List<V_Doc_Type_OutPut_NT>
                    {
                        new V_Doc_Type_OutPut_NT
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
                var errorResult = new List<V_Doc_Type_OutPut_NT>
                    {
                        new V_Doc_Type_OutPut_NT
                        {
                            Status = "Error",
                            Message = ex.Message,
                            Data = null
                        }
                    };
                return errorResult;
            }
        }
        public async Task<IEnumerable<V_Building_Classification>> GetViewDoc_Type_CheckListAsync()
        {
            using (IDbConnection db = _dapperDbConnection.CreateConnection())
            {
                return await db.QueryAsync<V_Building_Classification>("SELECT * FROM V_Doc_Type_CHECK_LIST");
            }
        }
       // public async Task<IEnumerable<V_Building_Classification_OutPut_NT>> GetViewDoc_Type_CheckList_NTAsync(Doc_Type_Doc_CategoryInput doc_Type_Doc_CategoryInput)
        public async Task<IEnumerable<V_Building_Classification_OutPut_NT>> GetViewDoc_Type_CheckList_NTAsync(Doc_Type_Doc_CategoryInput doc_Type_Doc_CategoryInput)
        {
            try
            {
                using (IDbConnection db = _dapperDbConnection.CreateConnection())
                {
                    var AssignToDetails = await db.QueryAsync<V_Doc_Type_NT>("Select * from V_Doc_Type_CHECK_LIST_NT", commandType: CommandType.Text);
                    if (AssignToDetails.Any())
                    {

                        var successsResult = new List<V_Building_Classification_OutPut_NT>
                    {
                        new V_Building_Classification_OutPut_NT
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
                        var errorResult = new List<V_Building_Classification_OutPut_NT>
                    {
                        new V_Building_Classification_OutPut_NT
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
                var errorResult = new List<V_Building_Classification_OutPut_NT>
                    {
                        new V_Building_Classification_OutPut_NT
                        {
                            Status = "Error",
                            Message = ex.Message,
                            Data = null
                        }
                    };
                return errorResult;
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
        public async Task<IEnumerable<StatutoryTypeNT>> GetViewStandard_TypeAsyncNT(ClassificationNT classificationNT)
        {
            DateTime dateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
            IDbTransaction transaction = null;
            bool transactionCompleted = false;  // Track the transaction state
            try
            {
                using (IDbConnection db = _dapperDbConnection.CreateConnection())
                {
                    var sqlConnection = db as SqlConnection;
                    if (sqlConnection == null)
                    {
                        throw new InvalidOperationException("The connection must be a SqlConnection to use OpenAsync.");
                    }

                    if (sqlConnection.State != ConnectionState.Open)
                    {
                        await sqlConnection.OpenAsync();  // Ensure the connection is open
                    }

                    transaction = db.BeginTransaction();
                    transactionCompleted = false;  // Reset transaction state

                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Session_User_Id", classificationNT.Session_User_Id);
                    parmeters.Add("@Business_Group_Id", classificationNT.Business_Group_Id);

                    var TaskDashFilter = await db.QueryAsync<StatutoryTypeOutNT>("SP_GET_STANDARD_TYPE_NT", parmeters, commandType: CommandType.StoredProcedure, transaction: transaction);

                    var sqlTransaction = (SqlTransaction)transaction;
                    await sqlTransaction.CommitAsync();
                    transactionCompleted = true;

                    var successsResult = new List<StatutoryTypeNT>
                    {
                        new StatutoryTypeNT
                        {
                            Status = "Ok",
                            Message = "Get data successfully!!!",
                            Data = TaskDashFilter
                        }
                    };
                    return successsResult;
                }
            }
            catch (SqlException sqlEx)
            {
                // Handle SQL exceptions specifically
                if (transaction != null && !transactionCompleted)
                {
                    try
                    {
                        // Rollback only if the transaction is not yet completed
                        transaction.Rollback();
                    }
                    catch (InvalidOperationException rollbackEx)
                    {
                        Console.WriteLine($"Rollback failed: {rollbackEx.Message}");
                    }
                }

                // Log the SQL error
                var errorResult = new List<StatutoryTypeNT>
                {
                    new StatutoryTypeNT
                    {
                        Status = "Error",
                        Message = $"SQL Error: {sqlEx.Message}",
                        Data = null
                    }
                };
                return errorResult;
            }
            catch (Exception ex)
            {
                // Generic error handling for non-SQL related issues
                if (transaction != null && !transactionCompleted)
                {
                    try
                    {
                        // Rollback only if the transaction is not yet completed
                        transaction.Rollback();
                    }
                    catch (InvalidOperationException rollbackEx)
                    {
                        Console.WriteLine($"Rollback failed: {rollbackEx.Message}");
                    }
                }

                // Log the generic error
                var errorResult = new List<StatutoryTypeNT>
                {
                    new StatutoryTypeNT
                    {
                        Status = "Error",
                        Message = $"Error: {ex.Message}",
                        Data = null
                    }
                };
                return errorResult;
            }
            finally
            {
                // Ensure transaction is committed or rolled back appropriately
                if (transaction != null && !transactionCompleted)
                {
                    try
                    {
                        transaction.Rollback();  // Rollback in case of any issues
                    }
                    catch (Exception rollbackEx)
                    {
                        Console.WriteLine($"Final rollback failed: {rollbackEx.Message}");
                    }
                }
            }
        }
        public async Task<IEnumerable<V_Building_Classification>> GetViewStatutory_AuthAsync()
        {
            using (IDbConnection db = _dapperDbConnection.CreateConnection())
            {
                return await db.QueryAsync<V_Building_Classification>("SELECT * FROM V_Statutory_Auth");
            }
        }
        public async Task<IEnumerable<StatutoryTypeNT>> GetViewStatutory_AuthAsyncNT(ClassificationNT classificationNT)
        {
            DateTime dateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
            IDbTransaction transaction = null;
            bool transactionCompleted = false;  // Track the transaction state
            try
            {
                using (IDbConnection db = _dapperDbConnection.CreateConnection())
                {
                    var sqlConnection = db as SqlConnection;
                    if (sqlConnection == null)
                    {
                        throw new InvalidOperationException("The connection must be a SqlConnection to use OpenAsync.");
                    }

                    if (sqlConnection.State != ConnectionState.Open)
                    {
                        await sqlConnection.OpenAsync();  // Ensure the connection is open
                    }

                    transaction = db.BeginTransaction();
                    transactionCompleted = false;  // Reset transaction state

                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Session_User_Id", classificationNT.Session_User_Id);
                    parmeters.Add("@Business_Group_Id", classificationNT.Business_Group_Id);

                    var TaskDashFilter = await db.QueryAsync<StatutoryTypeOutNT>("SP_GET_STATUTORY_AUTHORITY_NT", parmeters, commandType: CommandType.StoredProcedure, transaction: transaction);

                    var sqlTransaction = (SqlTransaction)transaction;
                    await sqlTransaction.CommitAsync();
                    transactionCompleted = true;

                    var successsResult = new List<StatutoryTypeNT>
                    {
                        new StatutoryTypeNT
                        {
                            Status = "Ok",
                            Message = "Get data successfully!!!",
                            Data = TaskDashFilter
                        }
                    };
                    return successsResult;
                }
            }
            catch (SqlException sqlEx)
            {
                // Handle SQL exceptions specifically
                if (transaction != null && !transactionCompleted)
                {
                    try
                    {
                        // Rollback only if the transaction is not yet completed
                        transaction.Rollback();
                    }
                    catch (InvalidOperationException rollbackEx)
                    {
                        Console.WriteLine($"Rollback failed: {rollbackEx.Message}");
                    }
                }

                // Log the SQL error
                var errorResult = new List<StatutoryTypeNT>
                {
                    new StatutoryTypeNT
                    {
                        Status = "Error",
                        Message = $"SQL Error: {sqlEx.Message}",
                        Data = null
                    }
                };
                return errorResult;
            }
            catch (Exception ex)
            {
                // Generic error handling for non-SQL related issues
                if (transaction != null && !transactionCompleted)
                {
                    try
                    {
                        // Rollback only if the transaction is not yet completed
                        transaction.Rollback();
                    }
                    catch (InvalidOperationException rollbackEx)
                    {
                        Console.WriteLine($"Rollback failed: {rollbackEx.Message}");
                    }
                }

                // Log the generic error
                var errorResult = new List<StatutoryTypeNT>
                {
                    new StatutoryTypeNT
                    {
                        Status = "Error",
                        Message = $"Error: {ex.Message}",
                        Data = null
                    }
                };
                return errorResult;
            }
            finally
            {
                // Ensure transaction is committed or rolled back appropriately
                if (transaction != null && !transactionCompleted)
                {
                    try
                    {
                        transaction.Rollback();  // Rollback in case of any issues
                    }
                    catch (Exception rollbackEx)
                    {
                        Console.WriteLine($"Final rollback failed: {rollbackEx.Message}");
                    }
                }
            }
        }
        public async Task<IEnumerable<V_Building_Classification>> GetViewJOB_ROLEAsync()
        {
            using (IDbConnection db = _dapperDbConnection.CreateConnection())
            {
                return await db.QueryAsync<V_Building_Classification>("SELECT * FROM V_JOB_ROLE");
            }
        }
        public async Task<IEnumerable<V_Job_Role_NT_OutPut>> GetViewJOB_ROLE_NTAsync(V_Department_NT_Input v_Department_NT_Input)
        {
            //using (IDbConnection db = _dapperDbConnection.CreateConnection())
            //{
            //    return await db.QueryAsync<V_Building_Classification>("SELECT * FROM V_JOB_ROLE");
            //}
            try
            {
                using (IDbConnection db = _dapperDbConnection.CreateConnection())
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@Session_User_Id", v_Department_NT_Input.Session_User_Id);
                    parameters.Add("@Business_Group_Id", v_Department_NT_Input.Business_Group_Id);

                    var GetRaisetAT = await db.QueryAsync<V_JobRole_NT>("SP_GET_JOBROLE_NT", parameters, commandType: CommandType.StoredProcedure);

                    if (GetRaisetAT != null)
                    {
                        var successsResult = new List<V_Job_Role_NT_OutPut>
                            {
                                new V_Job_Role_NT_OutPut
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
                        var ErrorResponse = new List<V_Job_Role_NT_OutPut>
                            {
                                new V_Job_Role_NT_OutPut
                                {
                                    Status = "Ok",
                                    Message = "Data Not found",
                                    Data= null

                                }
                            };
                        return ErrorResponse;
                    }
                }
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
        public async Task<IEnumerable<V_Building_Classification>> GetViewDepartment()
        {
            using (IDbConnection db = _dapperDbConnection.CreateConnection())
            {
                return await db.QueryAsync<V_Building_Classification>("SELECT * FROM V_Department");
            }
        }
        public async Task<IEnumerable<V_Department_NT_OutPut>> GetAllDepartmentNTAsync(V_Department_NT_Input v_Department_NT_Input)
        {
            try
            {
                using (IDbConnection db = _dapperDbConnection.CreateConnection())
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@Session_User_Id", v_Department_NT_Input.Session_User_Id);
                    parameters.Add("@Business_Group_Id", v_Department_NT_Input.Business_Group_Id);

                    var GetRaisetAT = await db.QueryAsync<V_Department_NT>("SP_GET_DEPARTMENT_NT", parameters, commandType: CommandType.StoredProcedure);

                    if (GetRaisetAT != null)
                    {
                        var successsResult = new List<V_Department_NT_OutPut>
                            {
                                new V_Department_NT_OutPut
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
                        var ErrorResponse = new List<V_Department_NT_OutPut>
                            {
                                new V_Department_NT_OutPut
                                {
                                    Status = "Ok",
                                    Message = "Data Not found",
                                    Data= null

                                }
                            };
                        return ErrorResponse;
                    }
                }
            }
            catch (Exception ex)
            {
                var ErrorResponse = new List<V_Department_NT_OutPut>
                        {
                            new V_Department_NT_OutPut
                            {
                                Status = "Error",
                                Message = ex.Message,
                                Data= null
                            }
                        };
                return ErrorResponse;
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
        public async Task<IEnumerable<RAISED_AT_OUTPUT_LIST_NT>> GetRaiseATNTAsync(RAISED_AT_INPUT_NT rAISED_AT_INPUT)
        {
            try
            {
                using (IDbConnection db = _dapperDbConnection.CreateConnection())
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@PROPERTY_MKEY", rAISED_AT_INPUT.PROPERTY_MKEY);
                    parameters.Add("@BUILDING_MKEY", rAISED_AT_INPUT.BUILDING_MKEY);

                    var GetRaisetAT = await db.QueryAsync<RAISED_AT_OUTPUT_NT>("SP_GET_RAISED_AT", parameters, commandType: CommandType.StoredProcedure);

                    if (GetRaisetAT != null)
                    {
                        var successsResult = new List<RAISED_AT_OUTPUT_LIST_NT>
                            {
                                new RAISED_AT_OUTPUT_LIST_NT
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
                        var ErrorResponse = new List<RAISED_AT_OUTPUT_LIST_NT>
                            {
                                new RAISED_AT_OUTPUT_LIST_NT
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
        public async Task<IEnumerable<RAISED_AT_OUTPUT_LIST_NT>> GetRaiseATBeforeNTAsync(RAISED_AT_INPUT_NT rAISED_AT_INPUT_NT)
        {
            try
            {
                using (IDbConnection db = _dapperDbConnection.CreateConnection())
                {
                    //var GetRaisetAT = await db.QueryAsync<RAISED_AT_OUTPUT>("SELECT * FROM V_RaisedAT " +
                    //     "WHERE BUILDING_MKEY in (-1,@BUILDING_MKEY) AND PROPERTY in (-1,@PROPERTY_MKEY);", new { BUILDING_MKEY = rAISED_AT_INPUT.BUILDING_MKEY, PROPERTY_MKEY = rAISED_AT_INPUT.PROPERTY_MKEY });

                    var parameters = new DynamicParameters();
                    parameters.Add("@PROPERTY_MKEY", rAISED_AT_INPUT_NT.PROPERTY_MKEY);
                    parameters.Add("@BUILDING_MKEY", rAISED_AT_INPUT_NT.BUILDING_MKEY);
                    parameters.Add("@Session_User_Id", rAISED_AT_INPUT_NT.Session_User_Id);
                    parameters.Add("@Business_Group_Id", rAISED_AT_INPUT_NT.Business_Group_Id);

                    var GetRaisetAT = await db.QueryAsync<RAISED_AT_OUTPUT_NT>("SP_GET_RAISED_AT_BEFORE_NT", parameters, commandType: CommandType.StoredProcedure);

                    if (GetRaisetAT != null)
                    {
                        var successsResult = new List<RAISED_AT_OUTPUT_LIST_NT>
                            {
                                new RAISED_AT_OUTPUT_LIST_NT
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
                        var ErrorResponse = new List<RAISED_AT_OUTPUT_LIST_NT>
                            {
                                new RAISED_AT_OUTPUT_LIST_NT
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
        public async Task<IEnumerable<V_Building_Classification>> GetViewSanctioningAuthority()
        {
            using (IDbConnection db = _dapperDbConnection.CreateConnection())
            {
                return await db.QueryAsync<V_Building_Classification>("SELECT * FROM V_Sanctioning_Authority");
            }
        }
        public async Task<IEnumerable<V_Sanctioning_Authority_OutPut_NT>> GetViewSanctioningAuthority_NT(Doc_Type_Doc_CategoryInput doc_Type_Doc_CategoryInput)
        {
            try
            {
                using (IDbConnection db = _dapperDbConnection.CreateConnection())
                {
                    var Sanctioning_List = await db.QueryAsync<Sanctioning_Authority_List>("SELECT * FROM V_Sanctioning_Authority", commandType: CommandType.Text);
                    if (Sanctioning_List.Any())
                    {

                        var successsResult = new List<V_Sanctioning_Authority_OutPut_NT>
                    {
                        new V_Sanctioning_Authority_OutPut_NT
                        {
                            Status = "Ok",
                            Message = "Message",
                            Data= Sanctioning_List
                        }
                    };
                        return successsResult;
                    }
                    else
                    {
                        var errorResult = new List<V_Sanctioning_Authority_OutPut_NT>
                    {
                        new V_Sanctioning_Authority_OutPut_NT
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
                var errorResult = new List<V_Sanctioning_Authority_OutPut_NT>
                    {
                        new V_Sanctioning_Authority_OutPut_NT
                        {
                            Status = "Error",
                            Message = ex.Message,
                            Data=null
                        }
                    };
                return errorResult;
            }

        }
        public async Task<IEnumerable<V_Building_Classification>> GetViewDocument_Category()
        {
            using (IDbConnection db = _dapperDbConnection.CreateConnection())
            {
                return await db.QueryAsync<V_Building_Classification>("SELECT * FROM V_Doc_Category");
            }
        }

        public async Task<ActionResult<IEnumerable<StatutoryTypeNT>>> GetViewDocumentCategoryNT(Document_CategoryOutPutNT document_CategoryOutPutNT)
        {
            try
            {
                using (IDbConnection db = _dapperDbConnection.CreateConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Session_User_Id", document_CategoryOutPutNT.Session_User_Id);
                    parmeters.Add("@Business_Group_Id", document_CategoryOutPutNT.Business_Group_Id);
                    var GetComplianceStatus = await db.QueryAsync<StatutoryTypeOutNT>("SP_GET_DOC_CATEGORY_NT", parmeters, commandType: CommandType.StoredProcedure);
                    
                    if (!GetComplianceStatus.Any())  // If no records are returned
                    {
                        var ErrorResponse = new List<StatutoryTypeNT>
                        {
                            new StatutoryTypeNT
                            {
                                Status = "Error",
                                Message = "No data found",
                                Data = null
                            }
                        };
                        return ErrorResponse;
                    }
                    var SuccessResponse = new List<StatutoryTypeNT>
                    {
                        new StatutoryTypeNT
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
                var ErrorResponse = new List<StatutoryTypeNT>
                {
                    new StatutoryTypeNT
                    {
                        Status = "Error",
                        Message =ex.Message,
                        Data= null
                    }
                };
                return ErrorResponse;
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
        public async Task<ActionResult<IEnumerable<COMPLIANCE_STATUS_OUTPUT_LIST_NT>>> GetComplianceStatusNTAsync(V_Department_NT_Input v_Department_NT_Input)
        {
            try
            {
                using (IDbConnection db = _dapperDbConnection.CreateConnection())
                {
                    var GetComplianceStatus = await db.QueryAsync<COMPLIANCE_STATUS_OUTPUT_NT>("SELECT * FROM V_COMPLIANCE_STATUS ");

                    if (!GetComplianceStatus.Any())
                    {
                        var ErrorResponse = new List<COMPLIANCE_STATUS_OUTPUT_LIST_NT>
                        {
                            new COMPLIANCE_STATUS_OUTPUT_LIST_NT
                            {
                                Status = "Error",
                                Message = "No data found",
                                Data = null
                            }
                        };
                        return ErrorResponse;
                    }
                    var SuccessResponse = new List<COMPLIANCE_STATUS_OUTPUT_LIST_NT>
                        {
                            new COMPLIANCE_STATUS_OUTPUT_LIST_NT
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
        public async Task<ActionResult<IEnumerable<GetTaskTypeListNT>>> GetTaskTypeNTAsync(GetTaskTypeInPut getTaskTypeInPut)
        {
            try
            {
                using (IDbConnection db = _dapperDbConnection.CreateConnection())
                {
                    var GetComplianceStatus = await db.QueryAsync<GetTaskTypeOutPutNT>("SELECT * FROM V_TASK_TYPE;");

                    if (!GetComplianceStatus.Any())  // If no records are returned
                    {
                        var ErrorResponse = new List<GetTaskTypeListNT>
                        {
                            new GetTaskTypeListNT
                            {
                                Status = "Error",
                                Message = "No data found",
                                Data = null
                            }
                        };
                        return ErrorResponse;
                    }
                    var SuccessResponse = new List<GetTaskTypeListNT>
                        {
                            new GetTaskTypeListNT
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
        public async Task<ActionResult<IEnumerable<GetAuthorityStatusNT>>> GetAuthorityStatusNTAsync(GetTaskTypeInPut getTaskTypeInPut)
        {
            try
            {
                using (IDbConnection db = _dapperDbConnection.CreateConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Session_User_ID", getTaskTypeInPut.Session_User_ID);
                    parmeters.Add("@Business_Group_ID", getTaskTypeInPut.Business_Group_ID);
                    var AssignToDetails = await db.QueryAsync<V_AuthorityStatusNT>("SP_GET_AUHTORITY_STATUS", parmeters, commandType: CommandType.StoredProcedure);
                    if (AssignToDetails.Any())
                    {

                        var successsResult = new List<GetAuthorityStatusNT>
                    {
                        new GetAuthorityStatusNT
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
                        var errorResult = new List<GetAuthorityStatusNT>
                    {
                        new GetAuthorityStatusNT
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
                var errorResult = new List<GetAuthorityStatusNT>
                    {
                        new GetAuthorityStatusNT
                        {
                            Status = "Error",
                            Message = ex.Message,
                            Data=null
                        }
                    };
                return errorResult;
            }
        }
        public async Task<ActionResult<IEnumerable<GetAuthorityStatus>>> GetAuthorityStatusAsync()
        {
            try
            {
                using (IDbConnection db = _dapperDbConnection.CreateConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Session_User_ID", null);
                    parmeters.Add("@Business_Group_ID", null);
                    var AssignToDetails = await db.QueryAsync<V_AuthorityStatus>("SP_GET_AUHTORITY_STATUS", parmeters, commandType: CommandType.StoredProcedure);
                    if (AssignToDetails.Any())
                    {

                        var successsResult = new List<GetAuthorityStatus>
                    {
                        new GetAuthorityStatus
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
                        var errorResult = new List<GetAuthorityStatus>
                    {
                        new GetAuthorityStatus
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
                var errorResult = new List<GetAuthorityStatus>
                    {
                        new GetAuthorityStatus
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
