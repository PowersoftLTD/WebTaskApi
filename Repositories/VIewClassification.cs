using System.Data;
using Dapper;
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
    }
}
