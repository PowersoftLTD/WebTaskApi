﻿using Dapper;
using System.Data;
using System.Data.SqlClient;
using TaskManagement.API.DapperDbConnections;
using TaskManagement.API.Interfaces;
using TaskManagement.API.Model;

namespace TaskManagement.API.Repositories
{
    public class MSPRespository : IMSPInterface
    {
        private static TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        public IDapperDbConnection _dapperDbConnection;
        private readonly string _connectionString;

        public MSPRespository(IDapperDbConnection dapperDbConnection, string connectionString)
        {
            _dapperDbConnection = dapperDbConnection;
            _connectionString = connectionString;
        }
        public async Task<IEnumerable<MSPUploadExcelOutPut>> UploadExcel(List<MSPUploadExcelInput> mSPUploadExcelInput)
        {
            DateTime dateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
            IDbTransaction transaction = null;
            bool transactionCompleted = false;

            try
            {
                using (IDbConnection db = _dapperDbConnection.CreateConnection())
                {
                    var sqlConnection = db as SqlConnection;
                    if (sqlConnection == null)
                        throw new InvalidOperationException("The connection must be a SqlConnection to use OpenAsync.");

                    if (sqlConnection.State != ConnectionState.Open)
                        await sqlConnection.OpenAsync();

                    transaction = db.BeginTransaction();

                    if (mSPUploadExcelInput != null)
                    {
                        var dataTableExcel = new DataTable();
                        dataTableExcel.Columns.Add("WBS", typeof(string));
                        dataTableExcel.Columns.Add("Name", typeof(string));
                        dataTableExcel.Columns.Add("Duration", typeof(string));
                        dataTableExcel.Columns.Add("Start_Date", typeof(DateTime));
                        dataTableExcel.Columns.Add("Finish_Date", typeof(DateTime));
                        dataTableExcel.Columns.Add("Predecessors", typeof(string));
                        dataTableExcel.Columns.Add("Resource_Names", typeof(string));
                        dataTableExcel.Columns.Add("Text1", typeof(string));
                        dataTableExcel.Columns.Add("Outline_Level", typeof(int));
                        dataTableExcel.Columns.Add("Number1", typeof(int));
                        dataTableExcel.Columns.Add("Unique_ID", typeof(decimal));
                        dataTableExcel.Columns.Add("Created_By", typeof(decimal));
                        dataTableExcel.Columns.Add("Creation_Date", typeof(DateTime));
                        dataTableExcel.Columns.Add("Updated_By", typeof(decimal));
                        dataTableExcel.Columns.Add("Updation_Date", typeof(DateTime));
                        dataTableExcel.Columns.Add("Process_Flag", typeof(char));
                        dataTableExcel.Columns.Add("FileName", typeof(string));
                        dataTableExcel.Columns.Add("Percent_Complete", typeof(string));
                        dataTableExcel.Columns.Add("mpp_name", typeof(string));

                        foreach (var input in mSPUploadExcelInput)
                        {
                            dataTableExcel.Rows.Add(
                                input.WBS, input.Name, input.Duration, input.Start_Date, input.Finish_Date,
                                input.Predecessors, input.Resource_Names, input.Text1, input.Outline_Level,
                                input.Number1, input.Unique_ID, input.Created_By, input.Creation_Date,
                                input.Updated_By, input.Updation_Date, input.Process_Flag, input.FileName,
                                input.Percent_Complete,input.mpp_name
                            );
                        }

                        using var bulkCopy = new SqlBulkCopy(sqlConnection, SqlBulkCopyOptions.Default, (SqlTransaction)transaction)
                        {
                            DestinationTableName = "MSP_Default_Migration_Table"
                        };

                        foreach (DataColumn column in dataTableExcel.Columns)
                        {
                            bulkCopy.ColumnMappings.Add(column.ColumnName, column.ColumnName);
                        }

                        await bulkCopy.WriteToServerAsync(dataTableExcel);

                        var parametersMSP = new DynamicParameters();
                        parametersMSP.Add("@Parameter1", mSPUploadExcelInput[0].Project);
                        parametersMSP.Add("@Parameter2", mSPUploadExcelInput[0].Sub_Project);
                        parametersMSP.Add("@Parameter3", null);
                        parametersMSP.Add("@Parameter4", null);
                        parametersMSP.Add("@Parameter5", null);
                        parametersMSP.Add("@Parameter6", mSPUploadExcelInput[0].Session_User_Id);
                        parametersMSP.Add("@Parameter7", mSPUploadExcelInput[0].Business_Group_Id);

                        var mSPUploadExcel = await db.QueryAsync<MSPUploadExcel>(
                            "SP_INSERT_SCHEDULED_MSP", parametersMSP, commandType: CommandType.StoredProcedure, transaction: transaction);

                        var errorRemarks = (await db.QueryAsync<string>(
                            "SELECT DISTINCT remarks FROM msp_default_migration_table WHERE remarks LIKE 'Error:%'",
                            transaction: transaction
                        )).ToList();

                        if (errorRemarks.Any())
                        {
                            if (!transactionCompleted && transaction != null)
                                transaction.Rollback();



                            return new List<MSPUploadExcelOutPut>
                            {
                                new MSPUploadExcelOutPut
                                {
                                    Status = "Error",
                                    Message = string.Join("; ", errorRemarks),
                                    Data = null
                                }
                            };
                        }

                        await ((SqlTransaction)transaction).CommitAsync();
                        transactionCompleted = true;

                        return new List<MSPUploadExcelOutPut>
                        {
                            new MSPUploadExcelOutPut
                            {
                                Status = "Ok",
                                Message = "Success",
                                Data =  mSPUploadExcel
                            }
                        };
                    }
                    else
                    {
                        return new List<MSPUploadExcelOutPut>
                        {
                            new MSPUploadExcelOutPut
                            {
                                Status = "Error",
                                Message = "Input data is null",
                                Data = null
                            }
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                if (!transactionCompleted && transaction != null)
                {
                    try
                    {
                        transaction.Rollback();
                    }
                    catch (InvalidOperationException)
                    {
                        // Log rollback error if needed
                    }
                }

                return new List<MSPUploadExcelOutPut>
        {
            new MSPUploadExcelOutPut
            {
                Status = "Error",
                Message = ex.Message,
                Data = null
            }
        };
            }
        }
        public async Task<IEnumerable<MSPUploadExcelOutPut>> GetTaskMspAsync(MSPTaskInput mSPTaskInput)
        {
            DateTime dateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
            IDbTransaction transaction = null;
            bool transactionCompleted = false;

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

                    var parmetersMSP = new DynamicParameters();
                    parmetersMSP.Add("@ProjectMkey", mSPTaskInput.Project);
                    parmetersMSP.Add("@BuildingMkey", mSPTaskInput.Sub_Project);
                    parmetersMSP.Add("@Session_User_Id", mSPTaskInput.Session_User_Id);
                    parmetersMSP.Add("@Business_Group_Id", mSPTaskInput.Business_Group_Id);

                    var ScheduleMSP = await db.QueryAsync<MSPUploadExcel>("SP_GET_SCHEDULED_MSP", parmetersMSP, commandType: CommandType.StoredProcedure, transaction: transaction);

                    var sqlTransaction = (SqlTransaction)transaction;
                    await sqlTransaction.CommitAsync();
                    transactionCompleted = true;

                    var ResultMSP = new List<MSPUploadExcelOutPut>
                        {
                            new MSPUploadExcelOutPut
                            {
                                Status = "Ok",
                                Message = "Message",
                                Data = ScheduleMSP
                            }
                        };
                    return ResultMSP;
                }

            }
            catch (Exception ex)
            {
                if (transaction != null && !transactionCompleted)
                {
                    try
                    {
                        // Rollback only if the transaction is not yet completed
                        transaction.Rollback();
                    }
                    catch (InvalidOperationException rollbackEx)
                    {
                        // Handle rollback exception (may occur if transaction is already completed)
                        // Log or handle the rollback failure if needed
                        var ErrorResult = new List<MSPUploadExcelOutPut>
                            {
                                new MSPUploadExcelOutPut
                                {
                                    Status = "Error",
                                    Message = rollbackEx.Message,
                                    Data = null
                                }
                            };
                        return ErrorResult;
                    }
                }

                // Return error result if there was an exception during the upload process
                var errorResult = new List<MSPUploadExcelOutPut>
                {
                    new MSPUploadExcelOutPut
                    {
                        Status = "Error",
                        Message = ex.Message,
                        Data = null
                    }
                };
                return errorResult;
            }
        }
        public async Task<IEnumerable<MSPUploadExcelOutPut>> UpdateTaskMspAsync(List<MSPUploadExcelInput> mSPUploadExcelInput)
        {
            DateTime dateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
            IDbTransaction transaction = null;
            bool transactionCompleted = false;

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

                    var dataTableExcel = new DataTable();
                    dataTableExcel.Columns.Add("WBS", typeof(string));
                    dataTableExcel.Columns.Add("Name", typeof(string));
                    dataTableExcel.Columns.Add("Duration", typeof(string));
                    dataTableExcel.Columns.Add("Start_Date", typeof(DateTime));
                    dataTableExcel.Columns.Add("Finish_Date", typeof(DateTime));
                    dataTableExcel.Columns.Add("Predecessors", typeof(string));
                    dataTableExcel.Columns.Add("Resource_Names", typeof(string));
                    dataTableExcel.Columns.Add("Text1", typeof(string));
                    dataTableExcel.Columns.Add("Outline_Level", typeof(int));
                    dataTableExcel.Columns.Add("Number1", typeof(int));
                    dataTableExcel.Columns.Add("Unique_ID", typeof(decimal));
                    dataTableExcel.Columns.Add("Created_By", typeof(decimal));
                    dataTableExcel.Columns.Add("Creation_Date", typeof(DateTime));
                    dataTableExcel.Columns.Add("Updated_By", typeof(decimal));
                    dataTableExcel.Columns.Add("Updation_Date", typeof(DateTime));
                    dataTableExcel.Columns.Add("Process_Flag", typeof(char));
                    dataTableExcel.Columns.Add("FileName", typeof(string));
                    dataTableExcel.Columns.Add("mpp_name", typeof(string));
                    dataTableExcel.Columns.Add("Percent_Complete", typeof(string));
                    if (mSPUploadExcelInput != null)
                    {
                        foreach (var mSPUploadExcelInput1 in mSPUploadExcelInput)
                        {
                            dataTableExcel.Rows.Add(mSPUploadExcelInput1.WBS, mSPUploadExcelInput1.Name, mSPUploadExcelInput1.Duration, mSPUploadExcelInput1.Start_Date,
                                mSPUploadExcelInput1.Finish_Date, mSPUploadExcelInput1.Predecessors, mSPUploadExcelInput1.Resource_Names, mSPUploadExcelInput1.Text1,
                                mSPUploadExcelInput1.Outline_Level, mSPUploadExcelInput1.Number1, mSPUploadExcelInput1.Unique_ID, mSPUploadExcelInput1.Created_By,
                                mSPUploadExcelInput1.Creation_Date, mSPUploadExcelInput1.Updated_By, mSPUploadExcelInput1.Updation_Date, mSPUploadExcelInput1.Process_Flag,
                                mSPUploadExcelInput1.FileName, mSPUploadExcelInput1.mpp_name, mSPUploadExcelInput1.Percent_Complete);
                        }

                        // Use SqlBulkCopy for bulk insert
                        using var bulkCopy = new SqlBulkCopy(sqlConnection, SqlBulkCopyOptions.Default, (SqlTransaction)transaction)
                        {
                            DestinationTableName = "MSP_Default_Migration_Table"
                        };

                        bulkCopy.ColumnMappings.Add("WBS", "WBS");
                        bulkCopy.ColumnMappings.Add("Name", "Name");
                        bulkCopy.ColumnMappings.Add("Duration", "Duration");
                        bulkCopy.ColumnMappings.Add("Start_Date", "Start_Date");
                        bulkCopy.ColumnMappings.Add("Finish_Date", "Finish_Date");
                        bulkCopy.ColumnMappings.Add("Predecessors", "Predecessors");
                        bulkCopy.ColumnMappings.Add("Resource_Names", "Resource_Names");
                        bulkCopy.ColumnMappings.Add("Text1", "Text1");
                        bulkCopy.ColumnMappings.Add("Outline_Level", "Outline_Level");
                        bulkCopy.ColumnMappings.Add("Number1", "Number1");
                        bulkCopy.ColumnMappings.Add("Unique_ID", "Unique_ID");
                        bulkCopy.ColumnMappings.Add("Created_By", "Created_By");
                        bulkCopy.ColumnMappings.Add("Creation_Date", "Creation_Date");
                        bulkCopy.ColumnMappings.Add("Updated_By", "Updated_By");
                        bulkCopy.ColumnMappings.Add("Updation_Date", "Updation_Date");
                        bulkCopy.ColumnMappings.Add("Process_Flag", "Process_Flag");
                        bulkCopy.ColumnMappings.Add("FileName", "FileName");
                        bulkCopy.ColumnMappings.Add("mpp_name", "mpp_name");
                        bulkCopy.ColumnMappings.Add("Percent_Complete", "Percent_Complete");
                        await bulkCopy.WriteToServerAsync(dataTableExcel);

                        var parmetersMSP = new DynamicParameters();
                        parmetersMSP.Add("@Parameter1", mSPUploadExcelInput[0].Project);
                        parmetersMSP.Add("@Parameter2", mSPUploadExcelInput[0].Sub_Project);
                        parmetersMSP.Add("@Parameter3", null);
                        parmetersMSP.Add("@Parameter4", null);
                        parmetersMSP.Add("@Parameter5", null);
                        parmetersMSP.Add("@Parameter6", mSPUploadExcelInput[0].Session_User_Id);
                        parmetersMSP.Add("@Parameter7", mSPUploadExcelInput[0].Business_Group_Id);

                        var ScheduleMSP = await db.QueryAsync<MSPUploadExcel>("SP_UPDATE_SCHEDULED_MSP", parmetersMSP, commandType: CommandType.StoredProcedure, transaction: transaction);

                        var sqlTransaction = (SqlTransaction)transaction;
                        await sqlTransaction.CommitAsync();
                        transactionCompleted = true;

                        var ResultMSP = new List<MSPUploadExcelOutPut>
                        {
                            new MSPUploadExcelOutPut
                            {
                                Status = "Ok",
                                Message = "Message",
                                Data = ScheduleMSP
                            }
                        };
                        return ResultMSP;
                    }
                    else
                    {
                        var ResultMSP = new List<MSPUploadExcelOutPut>
                        {
                            new MSPUploadExcelOutPut
                            {
                                Status = "Error",
                                Message = "Message",
                                Data = null
                            }
                        };
                        return ResultMSP;
                    }
                }
            }
            catch (Exception ex)
            {
                if (transaction != null && !transactionCompleted)
                {
                    try
                    {
                        // Rollback only if the transaction is not yet completed
                        transaction.Rollback();
                    }
                    catch (InvalidOperationException rollbackEx)
                    {
                        // Handle rollback exception (may occur if transaction is already completed)
                        // Log or handle the rollback failure if needed
                        var ErrorResult = new List<MSPUploadExcelOutPut>
                        {
                            new MSPUploadExcelOutPut
                            {
                                Status = "Error",
                                Message = ex.Message,
                                Data = null
                            }
                        };
                        return ErrorResult;
                    }
                }

                // Return error result if there was an exception during the upload process
                var errorResult = new List<MSPUploadExcelOutPut>
                {
                    new MSPUploadExcelOutPut
                    {
                        Status = "Error",
                        Message = ex.Message,
                        Data = null
                    }
                };
                return errorResult;
            }
        }
    }
}
