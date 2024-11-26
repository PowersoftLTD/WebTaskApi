using TaskManagement.API.Model;

namespace TaskManagement.API.Interfaces
{
    public interface IProjectDefination
    {
        Task<IEnumerable<PROJECT_HDR>> GetAllProjectDefinationAsync(int LoggedIN, string FormName, string MethodName);
        Task<PROJECT_HDR> GetProjectDefinationByIdAsync(int id, int LoggedIN, string FormName, string MethodName);
        Task<PROJECT_HDR> CreateProjectDefinationAsync(PROJECT_HDR pROJECT_HDR);
        Task<bool> UpdateProjectDefinationAsync(PROJECT_HDR pROJECT_HDR);
        Task<bool> DeleteProjectDefinationAsync(int id, int LastUpatedBy, string FormName, string MethodName);
        Task<IEnumerable<dynamic>> GetApprovalDetails(int LoggedInID, int BUILDING_TYPE, string BUILDING_STANDARD, string STATUTORY_AUTHORITY);
    }
}
