using Microsoft.AspNetCore.Mvc;
using TaskManagement.API.Model;

namespace TaskManagement.API.Interfaces
{
    public interface IApprovalTemplate
    {
        Task<IEnumerable<APPROVAL_TEMPLATE_HDR>> GetAllApprovalTemplateAsync();
        Task<APPROVAL_TEMPLATE_HDR> GetApprovalTemplateByIdAsync(int id);
        Task<APPROVAL_TEMPLATE_HDR> CreateApprovalTemplateAsync(APPROVAL_TEMPLATE_HDR aPPROVAL_TEMPLATE_HDR);
        Task<APPROVAL_TEMPLATE_HDR> CheckABBRAsync(string chkABBR);
        Task<IEnumerable<APPROVAL_TEMPLATE_HDR>> AbbrAndShortDescAsync(string strBuilding,string strStandard,string Authority);
    }
}
