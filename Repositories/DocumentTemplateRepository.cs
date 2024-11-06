using Dapper;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using TaskManagement.API.Interfaces;
using TaskManagement.API.Model;

namespace TaskManagement.API.Repositories
{
    public class DocumentTemplateRepository : IDoc_Temp
    {
        public IDapperDbConnection _dapperDbConnection;
        public DocumentTemplateRepository(IDapperDbConnection dapperDbConnection)
        {
            _dapperDbConnection = dapperDbConnection;
        }

        public async Task<IEnumerable<DOC_TEMPLATE_HDR>> GetAllDocumentTempAsync(int LoggedIN)
        {
            using (IDbConnection db = _dapperDbConnection.CreateConnection())
            {
                var parmeters = new DynamicParameters();
                parmeters.Add("@MKEY", null);
                parmeters.Add("@ATTRIBUT1", LoggedIN);
                parmeters.Add("@ATTRIBUT2", null);
                parmeters.Add("@ATTRIBUT3", null);
                return await db.QueryAsync<DOC_TEMPLATE_HDR>("SP_GET_DOCUMENT_TEMPLATES", parmeters, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<DOC_TEMPLATE_HDR> GetDocumentTempByIdAsync(int id, int? LoggedIN)
        {
            try
            {
                using (IDbConnection db = _dapperDbConnection.CreateConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@MKEY", id);
                    parmeters.Add("@ATTRIBUT1", LoggedIN);
                    parmeters.Add("@ATTRIBUT2", null);
                    parmeters.Add("@ATTRIBUT3", null);
                    return await db.QueryFirstOrDefaultAsync<DOC_TEMPLATE_HDR>("SP_GET_DOCUMENT_TEMPLATES", parmeters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        public async Task<DOC_TEMPLATE_HDR> CreateDocumentTemplateAsync(DOC_TEMPLATE_HDR dOC_TEMPLATE_HDR)
        {
            try
            {
                using (IDbConnection db = _dapperDbConnection.CreateConnection())
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@DOC_ABBR", dOC_TEMPLATE_HDR.DOC_ABBR);
                    parameters.Add("@DOC_NUM_FIELD_NAME", dOC_TEMPLATE_HDR.DOC_NUM_FIELD_NAME);
                    parameters.Add("@DOC_NUM_DATE_NAME", dOC_TEMPLATE_HDR.DOC_NUM_DATE_NAME);
                    parameters.Add("@DOC_NUM_APP_FLAG", dOC_TEMPLATE_HDR.DOC_NUM_APP_FLAG);
                    parameters.Add("@DOC_NUM_VALID_FLAG", dOC_TEMPLATE_HDR.DOC_NUM_VALID_FLAG);
                    parameters.Add("@DOC_NUM_DATE_APP_FLAG", dOC_TEMPLATE_HDR.DOC_NUM_DATE_APP_FLAG);
                    parameters.Add("@DOC_ATTACH_APP_FLAG", dOC_TEMPLATE_HDR.DOC_ATTACH_APP_FLAG);
                    parameters.Add("@CREATED_BY", dOC_TEMPLATE_HDR.CREATED_BY);
                    parameters.Add("@LAST_UPDATED_BY", dOC_TEMPLATE_HDR.CREATED_BY);
                    parameters.Add("@DELETE_FLAG", dOC_TEMPLATE_HDR.DELETE_FLAG);
                    parameters.Add("@ATTRIBUTE1", dOC_TEMPLATE_HDR.ATTRIBUTE1);
                    parameters.Add("@ATTRIBUTE2", dOC_TEMPLATE_HDR.ATTRIBUTE2);

                    dOC_TEMPLATE_HDR = await db.QueryFirstOrDefaultAsync<DOC_TEMPLATE_HDR>("SP_INSERT_DOCUMENT_TEMPLATES", parameters, commandType: CommandType.StoredProcedure);
                    return dOC_TEMPLATE_HDR;
                }
            }
            catch (SqlException ex)
            {
                return dOC_TEMPLATE_HDR;
            }
            catch (Exception ex)
            {
                return dOC_TEMPLATE_HDR;
            }
        }
        public async Task<bool> UpdateDocumentTemplateAsync(DOC_TEMPLATE_HDR dOC_TEMPLATE_HDR)
        {
            try
            {
                using (IDbConnection db = _dapperDbConnection.CreateConnection())
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@MKEY", dOC_TEMPLATE_HDR.MKEY);
                    parameters.Add("@DOC_ABBR", dOC_TEMPLATE_HDR.DOC_ABBR);
                    parameters.Add("@DOC_NUM_FIELD_NAME", dOC_TEMPLATE_HDR.DOC_NUM_FIELD_NAME);
                    parameters.Add("@DOC_NUM_DATE_NAME", dOC_TEMPLATE_HDR.DOC_NUM_DATE_NAME);
                    parameters.Add("@DOC_NUM_APP_FLAG", dOC_TEMPLATE_HDR.DOC_NUM_APP_FLAG);
                    parameters.Add("@DOC_NUM_VALID_FLAG", dOC_TEMPLATE_HDR.DOC_NUM_VALID_FLAG);
                    parameters.Add("@DOC_NUM_DATE_APP_FLAG", dOC_TEMPLATE_HDR.DOC_NUM_DATE_APP_FLAG);
                    parameters.Add("@DOC_ATTACH_APP_FLAG", dOC_TEMPLATE_HDR.DOC_ATTACH_APP_FLAG);
                    parameters.Add("@LAST_UPDATED_BY", dOC_TEMPLATE_HDR.CREATED_BY);
                    parameters.Add("@DELETE_FLAG", dOC_TEMPLATE_HDR.DELETE_FLAG);
                    parameters.Add("@ATTRIBUTE1", dOC_TEMPLATE_HDR.ATTRIBUTE1);
                    parameters.Add("@ATTRIBUTE2", dOC_TEMPLATE_HDR.ATTRIBUTE2);
                    await db.ExecuteAsync("SP_UPDATE_DOCUMENT_TEMPLATES", parameters, commandType: CommandType.StoredProcedure);
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteDocumentTemplateAsync(int id, int LastUpatedBy)
        {
            try
            {
                using (IDbConnection db = _dapperDbConnection.CreateConnection())
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@MKEY", id);
                    parameters.Add("@LAST_UPDATED_BY", LastUpatedBy);
                    await db.ExecuteAsync("SP_DELETE_DOCUMENT_TEMPLATES", parameters, commandType: CommandType.StoredProcedure);
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        
    }
}
