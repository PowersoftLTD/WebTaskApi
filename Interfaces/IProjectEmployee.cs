using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.API.Model;

namespace TaskManagement.API.Interfaces
{
    public interface IProjectEmployee
    {
        Task<EmployeeCompanyMST> Login_Validate(string Login_ID, string LOGIN_PASSWORD);

        Task<IEnumerable<V_Building_Classification>> GetProjectAsync(string TYPE_CODE, decimal MASTER_MKEY);
        Task<IEnumerable<V_Building_Classification>> GetSubProjectAsync(string Project_Mkey);
        Task<IEnumerable<EmployeeCompanyMST>> GetEmpAsync(string CURRENT_EMP_MKEY, string FILTER);
        Task<IEnumerable<EmployeeCompanyMST>> GetAssignedToAsync(string AssignNameLike);
        Task<IEnumerable<EmployeeCompanyMST>> GetEmpTagsAsync(string EMP_TAGS);
        Task<IEnumerable<TASK_DASHBOARD>> GetTaskDetailsAsync(string CURRENT_EMP_MKEY, string FILTER);
        Task<IEnumerable<TASK_DETAILS_BY_MKEY>> GetTaskDetailsByMkeyAsync(string Mkey);
        Task<IEnumerable<TASK_DASHBOARD>> GetTaskNestedGridAsync(string Mkey);
        Task<IEnumerable<TASK_ACTION_TRL>> GetActionsAsync(int TASK_MKEY, int CURRENT_EMP_MKEY, string CURR_ACTION);
        Task<IEnumerable<TASK_DASHBOARD>> GetTaskTreeAsync(string TASK_MKEY);
        Task<EmployeeCompanyMST> PutChangePasswordAsync(string LoginName, string Old_Password, string New_Password);
        Task<EmployeeCompanyMST> GetForgotPasswordAsync(string LoginName);
        Task<EmployeeCompanyMST> GetResetPasswordAsync(string TEMPPASSWORD, string LoginName);
        Task<EmployeeCompanyMST> GetValidateEmailAsync(string Login_ID);
        Task<IEnumerable<TASK_DASHBOARD>> GetTaskDashboardDetailsAsync(string CURRENT_EMP_MKEY, string CURR_ACTION);
        Task<IEnumerable<TASK_DASHBOARD>> GetTeamTaskAsync(string CURRENT_EMP_MKEY);
        Task<IEnumerable<TASK_DASHBOARD>> GetTeamTaskDetailsAsync(string CURRENT_EMP_MKEY);
        Task<IEnumerable<TASK_HDR>> GetProjectDetailsWithSubProjectAsync(string ProjectID, string SubProjectID);
        Task<IEnumerable<TASK_HDR>> GetTaskTreeExportAsync(string Task_Mkey);
        Task<TASK_HDR> CreateAddTaskAsync(TASK_HDR tASK_HDR);
        Task<TASK_HDR> CreateAddSubTaskAsync(TASK_HDR tASK_HDR);
        Task<int> TASKFileUpoadAsync(string srNo, string taskMkey, string taskParentId, string fileName, string filePath, string createdBy, string deleteFlag, string taskMainNodeId);
        Task<TASK_HDR> AddTaskAsync(TASK_HDR tASK_HDR);
        Task<TASK_HDR> UpdateTaskAsync(TASK_HDR tASK_HDR);

        Task<int> UpdateTASKFileUpoadAsync(string taskMkey,string deleteFlag);
        
    }
}
