﻿using TaskManagement.API.Model;

namespace TaskManagement.API.Interfaces
{
    public interface IApprovalTaskInitiation
    {
        Task<APPROVAL_TASK_INITIATION> GetApprovalTemplateByIdAsync(int MKEY, int APPROVAL_MKEY);
        Task<APPROVAL_TASK_INITIATION> CreateTaskApprovalTemplateAsync(APPROVAL_TASK_INITIATION aPPROVAL_TASK_INITIATION);
    }
}
