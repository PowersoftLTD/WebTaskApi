using Microsoft.AspNetCore.Mvc;
using TaskManagement.API.Model;

namespace TaskManagement.API.Interfaces
{
    public interface IApprovalTemplate
    {
        Task<IEnumerable<OutPutApprovalTemplates_NT>> GetAllApprovalTemplateNTAsync(APPROVAL_TEMPLATE_HDR_INPUT_NT aPPROVAL_TEMPLATE_HDR_NT);
        Task<IEnumerable<OutPutApprovalTemplates>> GetAllApprovalTemplateAsync(int LoggedInID);
        Task<OutPutApprovalTemplates> GetApprovalTemplateByIdAsync(int id, int LoggedInID);

        Task<ActionResult<OutPutApprovalTemplates>> CreateApprovalTemplateAsync(InsertApprovalTemplates insertApprovalTemplates);

        Task<ActionResult<OutPutApprovalTemplates>> CreateApprovalTemplateAsyncNT(InsertApprovalTemplatesNT insertApprovalTemplatesNT);
        Task<APPROVAL_TEMPLATE_HDR> CheckABBRAsync(string chkABBR);
        Task<ActionResult<IEnumerable<APPROVAL_TEMPLATE_HDR_NT_OUTPUT>>> CheckABBRAsyncNT(APPROVAL_TEMPLATE_HDR_INPUT aPPROVAL_TEMPLATE_HDR_INPUT);
        Task<IEnumerable<APPROVAL_TEMPLATE_HDR>> AbbrAndShortDescAsync(string strBuilding,string strStandard,string Authority);
        Task<ActionResult<IEnumerable<APPROVAL_TEMPLATE_HDR_NT_OUTPUT>>> AbbrAndShortDescAsyncNT(GetAbbrAndShortAbbrOutPutNT getAbbrAndShortAbbrOutPutNT);
        Task<ActionResult<OutPutApprovalTemplates>> UpdateApprovalTemplateAsync(UpdateApprovalTemplates updateApprovalTemplates);
        Task<bool> DeleteApprovalTemplateAsync(int MKEY, int LAST_UPDATED_BY);
    }
}
