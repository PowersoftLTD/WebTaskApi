using TaskManagement.API.Model;

namespace TaskManagement.API.Interfaces
{
    public interface IProjectEmployee
    {
        Task<TASK_HDR> CreateTaskByApprovalTaskInitiation(int MKEY, int APPROVAL_MKEY);
    }
}
