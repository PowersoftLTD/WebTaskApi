using Microsoft.AspNetCore.Mvc;
using TaskManagement.API.Model;

namespace TaskManagement.API.Interfaces
{
    public interface IApprovalTemplate
    {
        Task<IEnumerable<OutPutApprovalTemplates>> GetAllApprovalTemplateAsync(int LoggedInID);
        Task<OutPutApprovalTemplates> GetApprovalTemplateByIdAsync(int id, int LoggedInID);
        Task<ActionResult<OutPutApprovalTemplates>> CreateApprovalTemplateAsync(InsertApprovalTemplates insertApprovalTemplates);
        Task<APPROVAL_TEMPLATE_HDR> CheckABBRAsync(string chkABBR);
        Task<IEnumerable<APPROVAL_TEMPLATE_HDR>> AbbrAndShortDescAsync(string strBuilding,string strStandard,string Authority);
        Task<ActionResult<OutPutApprovalTemplates>> UpdateApprovalTemplateAsync(UpdateApprovalTemplates updateApprovalTemplates);
        Task<bool> DeleteApprovalTemplateAsync(int MKEY, int LAST_UPDATED_BY);
    }
}
