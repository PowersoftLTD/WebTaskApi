using Microsoft.AspNetCore.Mvc;
using TaskManagement.API.Model;

namespace TaskManagement.API.Interfaces
{
    public interface IApprovalTemplate
    {
        Task<IEnumerable<APPROVAL_TEMPLATE_HDR>> GetAllApprovalTemplateAsync(int LoggedInID);
        Task<APPROVAL_TEMPLATE_HDR> GetApprovalTemplateByIdAsync(int id, int LoggedInID);
        Task<APPROVAL_TEMPLATE_HDR> CreateApprovalTemplateAsync(APPROVAL_TEMPLATE_HDR aPPROVAL_TEMPLATE_HDR);
        Task<APPROVAL_TEMPLATE_HDR> CheckABBRAsync(string chkABBR);
        Task<IEnumerable<APPROVAL_TEMPLATE_HDR>> AbbrAndShortDescAsync(string strBuilding,string strStandard,string Authority);
        Task<APPROVAL_TEMPLATE_HDR> UpdateApprovalTemplateAsync(APPROVAL_TEMPLATE_HDR aPPROVAL_TEMPLATE_HDR);
        Task<bool> DeleteApprovalTemplateAsync(int MKEY, int LAST_UPDATED_BY);
    }
}
