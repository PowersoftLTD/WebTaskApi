using Microsoft.AspNetCore.Mvc;
using TaskManagement.API.Model;

namespace TaskManagement.API.Interfaces
{
    public interface IApprovalTaskInitiation
    {
        Task<APPROVAL_TASK_INITIATION> GetApprovalTemplateByIdAsync(int MKEY, int APPROVAL_MKEY);

        Task<ActionResult<IEnumerable<APPROVAL_TASK_INITIATION_NT_OUTPUT>>> GetApprovalTemplateByIdAsyncNT(APPROVAL_TASK_INITIATION_NT_INUT aPPROVAL_TASK_INITIATION_NT_INUT);

        Task<APPROVAL_TASK_INITIATION> CreateTaskApprovalTemplateAsync(APPROVAL_TASK_INITIATION aPPROVAL_TASK_INITIATION);

        Task<ActionResult<IEnumerable<APPROVAL_TASK_INITIATION_NT_OUTPUT>>> CreateTaskApprovalTemplateAsyncNT(APPROVAL_TASK_INITIATION_INPUT_NT aPPROVAL_TASK_INITIATION_INPUT_NT);

        Task<APPROVAL_TASK_INITIATION_TRL_SUBTASK> UpdateApprovalSubtask(APPROVAL_TASK_INITIATION_TRL_SUBTASK aPPROVAL_TASK_INITIATION_TRL_SUBTASK);

        Task<ActionResult<IEnumerable<APPROVAL_TASK_INITIATION_TRL_SUBTASK_NT_OUTPUT>>> UpdateApprovalSubtaskNT(APPROVAL_TASK_INITIATION_TRL_SUBTASK_NT aPPROVAL_TASK_INITIATION_TRL_SUBTASK);

        // New Method Added by Itemad hyder 90-02-2025
        Task<Approval_Task_Initiation_OutPutResponse> GetApprovalTemplateByIdAsync_PS(int MKEY, int APPROVAL_MKEY);
        Task<Add_ApprovalTemplateInitiation_OutPut_PS> UpdateApprovalSubtask_PS(APPROVAL_TASK_INITIATION_TRL_SUBTASK_PS aPPROVAL_TASK_INITIATION_TRL_SUBTASK);
        Task<APPROVAL_TASK_INITIATION_PS> CreateTaskApprovalTemplateAsync_PS(APPROVAL_TASK_INITIATION_PS aPPROVAL_TASK_INITIATION);

    }
}
