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
        Task<IEnumerable<GET_ACTIONSOutPut_List>> GetActionsAsync(string TASK_MKEY, string CURRENT_EMP_MKEY, string CURR_ACTION);
        Task<IEnumerable<GET_TASK_TREEOutPut_List>> GetTaskTreeAsync(string TASK_MKEY);
        Task<IEnumerable<PutChangePasswordOutPut_List>> PutChangePasswordAsync(string LoginName, string Old_Password, string New_Password);
        Task<IEnumerable<ForgotPasswordOutPut_List>> GetForgotPasswordAsync(string LoginName);
        Task<IEnumerable<ResetPasswordOutPut_List>> GetResetPasswordAsync(string TEMPPASSWORD, string LoginName);
        Task<IEnumerable<ChangePasswordOutPut_List>> GetValidateEmailAsync(string Login_ID);
        Task<IEnumerable<GET_TASK_TREEOutPut_List>> GetTaskDashboardDetailsAsync(string CURRENT_EMP_MKEY, string CURR_ACTION);
        Task<IEnumerable<GetTaskTeamOutPut_List>> GetTeamTaskAsync(string CURRENT_EMP_MKEY);
        Task<IEnumerable<TASK_DASHBOARD>> GetTeamTaskDetailsAsync(string CURRENT_EMP_MKEY);
        Task<IEnumerable<Get_Project_DetailsWithSubprojectOutPut_List>> GetProjectDetailsWithSubProjectAsync(string ProjectID, string SubProjectID);
        Task<IEnumerable<TASK_NESTED_GRIDOutPut_List>> GetTaskTreeExportAsync(string Task_Mkey);
        Task<IEnumerable<Add_TaskOutPut_List>> CreateAddTaskAsync(Add_TaskInput add_TaskInput);
        Task<IEnumerable<Add_TaskOutPut_List>> CreateAddSubTaskAsync(Add_Sub_TaskInput add_Sub_TaskInput);
        Task<int> TASKFileUpoadAsync(string srNo, string taskMkey, string taskParentId, string fileName, string filePath, string createdBy, string deleteFlag, string taskMainNodeId);
        //Task<TASK_HDR> AddTaskAsync(TASK_HDR tASK_HDR);
        //Task<TASK_HDR> UpdateTaskAsync(TASK_HDR tASK_HDR);
        Task<int> UpdateTASKFileUpoadAsync(string taskMkey, string deleteFlag);
        Task<int> GetPostTaskActionAsync(string Mkey, string TASK_MKEY, string TASK_PARENT_ID, string ACTION_TYPE, string DESCRIPTION_COMMENT, string PROGRESS_PERC, string STATUS, string CREATED_BY, string TASK_MAIN_NODE_ID, string FILE_NAME, string FILE_PATH);

    }
}
