using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.API.Model;
using TaskManagement.API.Repositories;

namespace TaskManagement.API.Interfaces
{
    public interface IProjectEmployee
    {
        Task<IEnumerable<EmployeeLoginOutput_LIST>> Login_Validate(string Login_ID, string LOGIN_PASSWORD);

        Task<IEnumerable<V_Building_Classification_new>> GetProjectAsync(string TYPE_CODE, string MASTER_MKEY);
        Task<IEnumerable<V_Building_Classification_new>> GetSubProjectAsync(string Project_Mkey);
        Task<IEnumerable<EmployeeLoginOutput_LIST>> GetEmpAsync(string CURRENT_EMP_MKEY, string FILTER);
        Task<IEnumerable<EmployeeLoginOutput_LIST>> GetAssignedToAsync(string AssignNameLike);
        Task<IEnumerable<EmployeeTagsOutPut_list>> GetEmpTagsAsync(string EMP_TAGS);
        Task<IEnumerable<Task_DetailsOutPut_List>> GetTaskDetailsAsync(string CURRENT_EMP_MKEY, string FILTER);
        Task<IEnumerable<TASK_DETAILS_BY_MKEY_list>> GetTaskDetailsByMkeyAsync(string Mkey);
        Task<IEnumerable<TASK_NESTED_GRIDOutPut_List>> GetTaskNestedGridAsync(string Mkey);
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
        Task<TASK_HDR> CreateAddTaskAsync(Add_TaskInput add_TaskInput);
        Task<TASK_HDR> CreateAddSubTaskAsync(Add_Sub_TaskInput add_Sub_TaskInput);
        Task<int> TASKFileUpoadAsync(string srNo, string taskMkey, string taskParentId, string fileName, string filePath, string createdBy, string deleteFlag, string taskMainNodeId);
        Task<TASK_HDR> AddTaskAsync(TASK_HDR tASK_HDR);
        Task<TASK_HDR> UpdateTaskAsync(TASK_HDR tASK_HDR);
        Task<int> UpdateTASKFileUpoadAsync(string taskMkey, string deleteFlag);
        Task<int> GetPostTaskActionAsync(string Mkey, string TASK_MKEY, string TASK_PARENT_ID, string ACTION_TYPE, string DESCRIPTION_COMMENT, string PROGRESS_PERC, string STATUS, string CREATED_BY, string TASK_MAIN_NODE_ID, string FILE_NAME, string FILE_PATH);

    }
}
