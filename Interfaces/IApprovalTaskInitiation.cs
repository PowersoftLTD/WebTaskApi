using Microsoft.AspNetCore.Mvc;
using TaskManagement.API.Model;

namespace TaskManagement.API.Interfaces
{
    public interface IApprovalTaskInitiation
    {
        Task<APPROVAL_TASK_INITIATION> GetApprovalTemplateByIdAsync(int MKEY, int APPROVAL_MKEY);

        Task<ActionResult<IEnumerable<APPROVAL_TASK_INITIATION_NT_OUTPUT>>> GetApprovalTemplateByIdAsyncNT(APPROVAL_TASK_INITIATION_NT_INUT aPPROVAL_TASK_INITIATION_NT_INUT);

        Task<APPROVAL_TASK_INITIATION> CreateTaskApprovalTemplateAsync(APPROVAL_TASK_INITIATION aPPROVAL_TASK_INITIATION);

        Task<APPROVAL_TASK_INITIATION_TRL_SUBTASK> UpdateApprovalSubtask(APPROVAL_TASK_INITIATION_TRL_SUBTASK aPPROVAL_TASK_INITIATION_TRL_SUBTASK);
    }
}
