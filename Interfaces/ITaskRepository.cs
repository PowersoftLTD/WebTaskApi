using Microsoft.AspNetCore.Mvc;
using TaskManagement.API.Model;

namespace TaskManagement.API.Interfaces
{
    public interface ITASKRepository
    {
        Task<IEnumerable<TASK_RECURSIVE_HDR>> GetAllTASKsAsync();
        Task<TASK_RECURSIVE_HDR> GetTaskByIdAsync(int id);
        Task<TASK_RECURSIVE_HDR> CreateTASKAsync(TASK_RECURSIVE_HDR  tASK_RECURSIVE_HDR);
        Task<bool> UpdateTASKAsync(TASK_RECURSIVE_HDR tASK_RECURSIVE_HDR);
        Task<bool> DeleteTASKAsync(int id,int LastUpatedBy);
        Task<int> TASKFileUpoadAsync(FileUploadAPI fileUploadAPI);
    }
}
