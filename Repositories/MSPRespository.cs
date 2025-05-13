using Dapper;
using System.Data;
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


        public async Task<IEnumerable<MSPUploadExcelOutPut>> UploadExcel(MSPUploadExcelInput mSPUploadExcelInput)
        {
            DateTime dateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
            IDbTransaction transaction = null;
            bool transactionCompleted = false;

            try
            {
                using (IDbConnection db = _dapperDbConnection.CreateConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@WBS", mSPUploadExcelInput.WBS);
                    parmeters.Add("@Name", mSPUploadExcelInput.Name);
                    parmeters.Add("@Duration", mSPUploadExcelInput.Duration);
                    parmeters.Add("@Start_Date", mSPUploadExcelInput.Start_Date);
                    parmeters.Add("@Finish_Date", mSPUploadExcelInput.Finish_Date);
                    parmeters.Add("@Predecessors", mSPUploadExcelInput.Predecessors);
                    parmeters.Add("@Resource_Name", mSPUploadExcelInput.Resource_Names);
                    parmeters.Add("@Text1", mSPUploadExcelInput.Text1);
                    parmeters.Add("@Outline_Level", mSPUploadExcelInput.Outline_Level);
                    parmeters.Add("@Number1", mSPUploadExcelInput.Number1);
                    parmeters.Add("@Unique_ID", mSPUploadExcelInput.Unique_ID);
                    parmeters.Add("@Created_By", mSPUploadExcelInput.Created_By);
                    parmeters.Add("@Creation_Date", mSPUploadExcelInput.Creation_Date);
                    parmeters.Add("@Updated_By", mSPUploadExcelInput.Updated_By);
                    parmeters.Add("@Updation_Date", mSPUploadExcelInput.Updation_Date);
                    parmeters.Add("@Process_Flag", mSPUploadExcelInput.Process_Flag);
                    parmeters.Add("@Remarks", mSPUploadExcelInput.Remarks);
                    parmeters.Add("@FileName", mSPUploadExcelInput.FileName);
                    parmeters.Add("@mpp_name", mSPUploadExcelInput.mpp_name);

                    var UploadExcel = await db.QueryAsync<MSPUploadExcel>("SP_INSERT_MSP_UPLOAD_EXCEL", parmeters, commandType: CommandType.StoredProcedure);

                    if (UploadExcel.Any())
                    {
                        var parmetersMSP = new DynamicParameters();
                        parmetersMSP.Add("@Parameter1", 323);
                        parmetersMSP.Add("@Parameter2", 324);
                        parmetersMSP.Add("@Parameter3", null);
                        parmetersMSP.Add("@Parameter4", null);
                        parmetersMSP.Add("@Parameter5", null);
                        parmetersMSP.Add("@Parameter6", null);
                        parmetersMSP.Add("@Parameter7", null);

                        var ScheduleMSP = await db.QueryAsync<MSPUploadExcel>("SP_INSERT_SCHEDULED_MSP", parmetersMSP, commandType: CommandType.StoredProcedure);

                        var ResultMSP = new List<MSPUploadExcelOutPut>
                        {
                            new MSPUploadExcelOutPut
                            {
                                Status = "Ok",
                                Message = "Message",
                                Data= ScheduleMSP

                            }
                        };
                    }
                    var successsResult = new List<MSPUploadExcelOutPut>
                    {
                        new MSPUploadExcelOutPut
                        {
                            Status = "Ok",
                            Message = "Message",
                            Data= UploadExcel

                        }
                    };
                    return successsResult;
                }
            }
            catch (Exception ex)
            {
                var errorResult = new List<MSPUploadExcelOutPut>
                    {
                        new MSPUploadExcelOutPut
                        {
                            Status = "Error",
                            Message = ex.Message
                        }
                    };
                return errorResult;
            }
        }
    }
}
