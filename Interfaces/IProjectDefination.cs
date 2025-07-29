using Microsoft.AspNetCore.Mvc;
using TaskManagement.API.Model;

namespace TaskManagement.API.Interfaces
{
    public interface IProjectDefination
    {
        Task<IEnumerable<PROJECT_HDR>> GetAllProjectDefinationAsync(int LoggedIN, string FormName, string MethodName);

        Task <ActionResult<IEnumerable<PROJECT_HDR_NT_OUTPUT>>> GetAllProjectDefinationAsyncNT(ProjectHdrNT projectHdrNT);

        Task<PROJECT_HDR> GetProjectDefinationByIdAsync(int id, int LoggedIN, string FormName, string MethodName);
        Task<PROJECT_HDR> CreateProjectDefinationAsync(PROJECT_HDR pROJECT_HDR);
        Task<ActionResult<IEnumerable<PROJECT_HDR_NT_OUTPUT>>> CreateProjectDefinationAsyncNT(PROJECT_HDR_INPUT_NT pROJECT_HDR_INPUT_NT);
        Task<PROJECT_HDR> UpdateProjectDefinationAsync(PROJECT_HDR pROJECT_HDR);
        Task<bool> DeleteProjectDefinationAsync(int id, int LastUpatedBy, string FormName, string MethodName);
        Task<IEnumerable<PROJECT_APPROVAL_DETAILS_OUTPUT>> GetApprovalDetails(int LoggedInID, int BUILDING_TYPE, string BUILDING_STANDARD, string STATUTORY_AUTHORITY);
    }
}
