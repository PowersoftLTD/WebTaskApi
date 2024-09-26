using TaskManagement.API.Model;

namespace TaskManagement.API.Interfaces
{
    public interface ITASKRepository
    {
        Task<IEnumerable<TASK_RECURSIVE_HDR>> GetAllTASKsAsync();
        Task<TASK_RECURSIVE_HDR> GetTaskByIdAsync(int id);
        Task<int> CreateTASKAsync(TASK_RECURSIVE_HDR  tASK_RECURSIVE_HDR);
        Task UpdateTASKAsync(TASK_RECURSIVE_HDR tASK_RECURSIVE_HDR);
        Task DeleteTASKAsync(int id,int LastUpatedBy);
    }
}
