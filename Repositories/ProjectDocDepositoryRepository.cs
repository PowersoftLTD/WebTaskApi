using Dapper;
using System.Data;
using System;
using TaskManagement.API.DapperDbConnections;
using TaskManagement.API.Interfaces;
using TaskManagement.API.Model;
using System.Data.SqlClient;
using System.Reflection.Metadata;
using System.Xml.Linq;

namespace TaskManagement.API.Repositories
{
    public class ProjectDocDepositoryRepository : IProjectDocDepository
    {
        public IDapperDbConnection _dapperDbConnection;
        public ProjectDocDepositoryRepository(IDapperDbConnection dapperDbConnection)
        {
            _dapperDbConnection = dapperDbConnection;
        }
        public async Task<IEnumerable<dynamic>> GetAllProjectDocDeositoryAsync(string? ATTRIBUTE1, string? ATTRIBUTE2, string? ATTRIBUTE3)
        {
            try
            {
                using (IDbConnection db = _dapperDbConnection.CreateConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@MKEY", null);
                    parmeters.Add("@ATTRIBUTE1", ATTRIBUTE1);
                    parmeters.Add("@ATTRIBUTE2", ATTRIBUTE2);
                    parmeters.Add("@ATTRIBUTE3", ATTRIBUTE3);
                    var ProjDoc_Desp = await db.QueryAsync("SP_GET_PROJECT_DOC_DEPOSITORY", parmeters, commandType: CommandType.StoredProcedure);
                    return ProjDoc_Desp;
                }
            }
            catch (Exception ex)
            {
                return new List<dynamic>();
            }
        }
        public async Task<dynamic> GetProjectDocDeositoryByIDAsync(int? MKEY, string? ATTRIBUTE1, string? ATTRIBUTE2, string? ATTRIBUTE3)
        {
            try
            {
                using (IDbConnection db = _dapperDbConnection.CreateConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@MKEY", MKEY);
                    parmeters.Add("@ATTRIBUTE1", ATTRIBUTE1);
                    parmeters.Add("@ATTRIBUTE2", ATTRIBUTE2);
                    parmeters.Add("@ATTRIBUTE3", ATTRIBUTE3);
                    var ProjDoc_Desp = await db.QueryAsync("SP_GET_PROJECT_DOC_DEPOSITORY", parmeters, commandType: CommandType.StoredProcedure);
                    return ProjDoc_Desp;
                }
            }
            catch (Exception ex)
            {
                return new List<dynamic>();
            }
        }
        public async Task<dynamic> CreateProjectDocDeositoryAsync(int? BUILDING_TYPE, int? PROPERTY_TYPE, string? DOC_NAME, string?   DOC_NUMBER, string? DOC_DATE
            , string? DOC_ATTACHMENT, string? VALIDITY_DATE, int? CREATED_BY, string? ATTRIBUTE1,string? ATTRIBUTE2, string? ATTRIBUTE3)
        {
            try
            {
                using (IDbConnection db = _dapperDbConnection.CreateConnection())
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@BUILDING_TYPE", BUILDING_TYPE);
                    parameters.Add("@PROPERTY_TYPE", PROPERTY_TYPE);
                    parameters.Add("@DOC_NAME", DOC_NAME);
                    parameters.Add("@DOC_NUMBER", DOC_NUMBER);
                    parameters.Add("@DOC_DATE", DOC_DATE);
                    parameters.Add("@DOC_ATTACHMENT", DOC_ATTACHMENT);
                    parameters.Add("@VALIDITY_DATE", VALIDITY_DATE);
                    parameters.Add("@ATTRIBUTE1", ATTRIBUTE1);
                    parameters.Add("@ATTRIBUTE2", ATTRIBUTE2);
                    parameters.Add("@ATTRIBUTE3", ATTRIBUTE3);
                    parameters.Add("@CREATED_BY", CREATED_BY);

                    var ProjectDocDsp = await db.QueryAsync("SP_INSERT_PROJECT_DOCUMENT_DEPOSITORY", parameters, commandType: CommandType.StoredProcedure);
                    return ProjectDocDsp;
                }
            }
            catch (SqlException ex)
            {
                return ex.Errors;
            }
            catch (Exception ex)
            {
                return Enumerable.Empty<dynamic>();
            }
        }
        public async Task<dynamic> GetDocumentDetailsAsync(int? MKEY, string? ATTRIBUTE1, string? ATTRIBUTE2, string? ATTRIBUTE3)
        {
            try
            {
                using (IDbConnection db = _dapperDbConnection.CreateConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@MKEY", MKEY);
                    parmeters.Add("@ATTRIBUTE1", ATTRIBUTE1);
                    parmeters.Add("@ATTRIBUTE2", ATTRIBUTE2);
                    parmeters.Add("@ATTRIBUTE3", ATTRIBUTE3);
                    var ProjDoc_Desp = await db.QueryAsync("GET_SP_DOCUMENT_TEMPLATE_DETAILS", parmeters, commandType: CommandType.StoredProcedure);
                    return ProjDoc_Desp;
                }
            }
            catch (Exception ex)
            {
                return new List<dynamic>();
            }
        }
    }
}
