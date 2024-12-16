using Microsoft.AspNetCore.Mvc;
using TaskManagement.API.Model;

namespace TaskManagement.API.Interfaces
{
    public interface IProjectEmployee
    {
        Task<EmployeeCompanyMST> Login_Validate(string Login_ID, string LOGIN_PASSWORD);

        Task<IEnumerable<V_Building_Classification>> GetProjectAsync(string TYPE_CODE, decimal MASTER_MKEY);
        Task<IEnumerable<V_Building_Classification>> GetSubProjectAsync(string Project_Mkey);
        Task<IEnumerable<EmployeeCompanyMST>> GetEmpAsync(int CURRENT_EMP_MKEY, string FILTER);
        Task<IEnumerable<EmployeeCompanyMST>> GetAssignedToAsync(string AssignNameLike);
        Task<IEnumerable<EmployeeCompanyMST>> GetEmpTagsAsync(string EMP_TAGS);
        Task<IEnumerable<TASK_DASHBOARD>> GetTaskDetailsAsync(int CURRENT_EMP_MKEY, string FILTER);
        Task<IEnumerable<TASK_DETAILS_BY_MKEY>> GetTaskDetailsByMkeyAsync(string Mkey);
        Task<IEnumerable<TASK_DASHBOARD>> GetTaskNestedGridAsync(string Mkey);
        Task<IEnumerable<TASK_ACTION_TRL>> GetActionsAsync(int TASK_MKEY, int CURRENT_EMP_MKEY, string CURR_ACTION);

        Task<TASK_HDR> AddTaskAsync(TASK_HDR tASK_HDR);
        Task<TASK_HDR> UpdateTaskAsync(TASK_HDR tASK_HDR);
    }
}
