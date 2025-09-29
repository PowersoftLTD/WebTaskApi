using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.API.Model;
using TaskManagement.API.Repositories;

namespace TaskManagement.API.Interfaces
{
    public interface IProjectEmployee
    {
        Task<IEnumerable<EmployeeLoginOutput_LIST>> Login_Validate(string Login_ID, string LOGIN_PASSWORD);
        Task<IEnumerable<EmployeeLoginOutput_LIST_NT>> Login_Validate_NT(EmployeeCompanyMSTInput_NT employeeCompanyMSTInput_NT);
        Task<IEnumerable<EmployeeMobile_NT>> Login_Mobile_Validate_NT(EmployeeMobileMSTInput_NT employeeMobileMSTInput_NT);
        //Task<IEnumerable<LoginMobileEmail_NT>> LoginMobileEmailNTAsync(EmployeeMobileMSTInput_NT employeeMobileMSTInput_NT);
        Task<IEnumerable<ResetPasswordOutPut_List>> LoginMobileEmailNTAsync(EmployeeMobileMSTInput_NT employeeMobileMSTInput_NT);
        Task<IEnumerable<V_Building_Classification_new>> GetProjectAsync(string TYPE_CODE, string MASTER_MKEY);
        Task<IEnumerable<V_Building_Classification_NT>> GetProjectNTAsync(BuildingClassInput_NT v_Building_Classification);
        Task<IEnumerable<V_Building_Classification_new>> GetSubProjectAsync(string Project_Mkey);
        Task<IEnumerable<V_Building_Classification_New_NT>> GetSubProjectNTAsync(GetSubProjectInput_NT getSubProjectInput_NT);
        Task<IEnumerable<EmployeeLoginOutput_LIST>> GetEmpAsync(string CURRENT_EMP_MKEY, string FILTER);
        Task<IEnumerable<EmployeeLoginOutput_LIST_Session_NT>> GetEmpNTAsync(Get_EmpInput_NT get_EmpInput_NT);
        Task<IEnumerable<EmployeeLoginOutput_LIST>> GetAssignedToAsync(string AssignNameLike);
        Task<IEnumerable<EmployeeTagsOutPut_list>> GetEmpTagsAsync(string EMP_TAGS);
        Task<IEnumerable<EmployeeTagsOutPut_Tags_list_NT>> GetEmpTagsNTAsync(EMP_TAGSInput_NT eMP_TAGSInput_NT);
        Task<IEnumerable<Task_DetailsOutPut_List>> GetTaskDetailsAsync(string CURRENT_EMP_MKEY, string FILTER);
        Task<IEnumerable<Task_DetailsOutPutNT_List>> GetTaskDetailsNTAsync(Task_DetailsInputNT task_DetailsInputNT);
        Task<IEnumerable<TASK_DETAILS_BY_MKEY_list>> GetTaskDetailsByMkeyAsync(string Mkey);
        Task<IEnumerable<TASK_DETAILS_BY_MKEY_list_NT>> GetTaskDetailsByMkeyNTAsync(TASK_DETAILS_BY_MKEYInput_NT tASK_DETAILS_BY_MKEYInput_NT);
        Task<IEnumerable<TASK_NESTED_GRIDOutPut_List>> GetTaskNestedGridAsync(string Mkey);
        Task<IEnumerable<GET_ACTIONS_TYPE_FILE>> GetActionsAsync(string TASK_MKEY, string CURRENT_EMP_MKEY, string CURR_ACTION);
        Task<IEnumerable<GET_ACTIONS_TYPE_FILE_NT>> GetActionsAsync_NT(GET_ACTIONSInput_NT gET_ACTIONSInput_NT);
        Task<IEnumerable<GET_TASK_TREEOutPut_List>> GetTaskTreeAsync(string TASK_MKEY);
        Task<IEnumerable<GET_TASK_TREEOutPut_List_NT>> GetTaskTreeAsync_NT(GET_TASK_TREEInput_NT gET_TASK_TREEInput);
        Task<IEnumerable<PutChangePasswordOutPut_List>> PutChangePasswordAsync(string LoginName, string Old_Password, string New_Password);
        Task<IEnumerable<PutChangePasswordOutPutNT>> PostChangePasswordAsync(ChangePasswordInputNT changePasswordInput);
        Task<IEnumerable<ForgotPasswordOutPut_List>> GetForgotPasswordAsync(string LoginName);
        Task<IEnumerable<ResetPasswordOutPut_List>> GetResetPasswordAsync(string TEMPPASSWORD, string LoginName);
        Task<IEnumerable<ChangePasswordOutPut_List>> GetValidateEmailAsync(string Login_ID);
        Task<IEnumerable<GET_TASK_TREEOutPut_List>> GetTaskDashboardDetailsAsync(string CURRENT_EMP_MKEY, string CURR_ACTION);
        Task<IEnumerable<GET_TASK_TREEOutPut_List_NT>> GetTaskDashboardDetailsAsyncNT(Task_Dashboard_DetailsInput_NT task_Dashboard_DetailsInput);
        Task<IEnumerable<GetTaskTeamOutPut_List>> GetTeamTaskAsync(string CURRENT_EMP_MKEY);
        Task<IEnumerable<GetTaskTeamOutPut_ListNT>> GetTeamTaskAsyncNT(TeamTaskInputNT teamTaskInput);
        Task<IEnumerable<TASK_DASHBOARDOutPut_List>> GetTeamTaskDetailsAsync(string CURRENT_EMP_MKEY);
        Task<IEnumerable<Get_Project_DetailsWithSubprojectOutPut_List>> GetProjectDetailsWithSubProjectAsync(string ProjectID, string SubProjectID);
        Task<IEnumerable<TASK_NESTED_GRIDOutPut_List>> GetTaskTreeExportAsync(string Task_Mkey);
        Task<IEnumerable<Add_TaskOutPut_List>> CreateAddTaskAsync(Add_TaskInput add_TaskInput);
        Task<IEnumerable<Add_TaskOutPut_List_NT>> CreateAddTaskNTAsync(Add_TaskInput_NT add_TaskInput_NT);
        Task<IEnumerable<Add_TaskOutPut_List>> CreateAddSubTaskAsync(Add_Sub_TaskInput add_Sub_TaskInput);
        Task<IEnumerable<Add_TaskOutPut_List_NT>> CreateAddSubTaskNTAsync(Add_Sub_TaskInput_NT add_Sub_TaskInput_NT);
        Task<int> TASKFileUpoadAsync(int srNo, int taskMkey, int taskParentId, string fileName, string filePath, int createdBy, char deleteFlag, int taskMainNodeId);
        Task<ActionResult<Add_TaskOutPut_List_NT>> TASKFileUpoadNTAsync(int srNo, int taskMkey, int taskParentId, string fileName, string filePath, int createdBy, char deleteFlag, int taskMainNodeId);
        Task<int> UpdateTASKFileUpoadAsync(string LastUpdatedBy,string taskMkey, string deleteFlag);
        Task<int> GetPostTaskActionAsync(string Mkey, string TASK_MKEY, string TASK_PARENT_ID, string ACTION_TYPE, string DESCRIPTION_COMMENT, string PROGRESS_PERC, string STATUS, string CREATED_BY, string TASK_MAIN_NODE_ID, string FILE_NAME, string FILE_PATH);

        Task<ActionResult<TaskPostActionAPIOutPut_List_NT>> GetPostTaskActionAsyncNT(string Mkey, string TASK_MKEY, string TASK_PARENT_ID, string ACTION_TYPE, string DESCRIPTION_COMMENT, string PROGRESS_PERC, string STATUS, string CREATED_BY, string TASK_MAIN_NODE_ID, string FILE_NAME, string FILE_PATH);

        Task<ActionResult<IEnumerable<TASK_COMPLIANCE_list>>> GetTaskComplianceAsync(TASK_COMPLIANCE_INPUT tASK_COMPLIANCE_INPUT);
        Task<ActionResult<IEnumerable<TASK_COMPLIANCE_list_NT>>> GetTaskComplianceNTAsync(Task_Compliance_Input_NT task_Compliance_Input_NT);
        Task<ActionResult<IEnumerable<TaskSanctioningDepartmentOutputList>>> GetTaskSanctioningAuthorityAsync(TASK_COMPLIANCE_INPUT tASK_COMPLIANCE_INPUT);

        Task<ActionResult<IEnumerable<TaskSanctioningDepartmentOutputList_NT>>> GetTaskSanctioningAuthorityNTAsync(TASK_COMPLIANCE_INPUT_NT tASK_COMPLIANCE_INPUT);

        Task<ActionResult<IEnumerable<TASK_COMPLIANCE_END_CHECK_LIST>>> GetTaskEndListAsync(TASK_COMPLIANCE_INPUT tASK_COMPLIANCE_INPUT);

        Task<ActionResult<IEnumerable<TASK_COMPLIANCE_END_CHECK_LIST_NT>>> GetTaskEndListNTAsync(TASK_COMPLIANCE_INPUT_NT tASK_COMPLIANCE_INPUT);

        Task<ActionResult<IEnumerable<TASK_ENDLIST_DETAILS_OUTPUT_LIST>>> GetTaskEndListDetailsAsync(TASK_END_LIST_DETAILS tASK_END_LIST_DETAILS);
        Task<ActionResult<IEnumerable<TASK_COMPLIANCE_CHECK_LIST>>> GetTaskCheckListAsync(TASK_COMPLIANCE_INPUT tASK_COMPLIANCE_INPUT);

        Task<ActionResult<IEnumerable<TASK_COMPLIANCE_CHECK_LIST_NT>>> GetTaskCheckListNTAsync(TASK_COMPLIANCE_INPUT_NT tASK_COMPLIANCE_INPUT);

        Task<ActionResult<IEnumerable<TASK_ENDLIST_DETAILS_OUTPUT_LIST>>> PostTaskEndListInsertUpdateAsync(TASK_ENDLIST_INPUT tASK_ENDLIST_INPUT);

        Task<ActionResult<IEnumerable<TASK_ENDLIST_DETAILS_OUTPUT_LIST_NT>>> PostTaskEndListInsertUpdateNTAsync(TASK_ENDLIST_INPUT_NT tASK_ENDLIST_INPUT);

        Task<ActionResult<IEnumerable<TASK_COMPLIANCE_CHECK_LIST>>> PostTaskCheckListInsertUpdateAsync(TASK_CHECKLIST_INPUT tASK_CHECKLIST_INPUT);

        Task<ActionResult<IEnumerable<TASK_COMPLIANCE_CHECK_LIST_NT>>> PostTaskCheckListInsertUpdateNTAsync(TASK_CHECKLIST_CHECK_INPUT_NT tASK_CHECKLIST_INPUT);

        Task<ActionResult<IEnumerable<TaskSanctioningDepartmentOutputList>>> PostTaskSanctioningAuthorityAsync(TASK_SANCTIONING_AUTHORITY_INPUT tASK_SANCTIONING_AUTHORITY_INPUT);

        Task<ActionResult<IEnumerable<TaskSanctioningDepartmentOutputList_NT>>> PostTaskSanctioningAuthorityNTAsync(TASK_SANCTIONING_AUTHORITY_INPUT_NT tASK_SANCTIONING_AUTHORITY_INPUT);

        Task<ActionResult<IEnumerable<TaskCheckListOutputList>>> PostTaskCheckListTableInsertUpdateAsync(TASK_CHECKLIST_TABLE_INPUT tASK_CHECKLIST_TABLE_INPUT);
        Task<ActionResult<IEnumerable<TaskCheckListNTOutputList_NT>>> PostTaskCheckListTableInsertUpdateNTAsync(TASK_CHECKLIST_INPUT_NT tASK_CHECKLIST_TABLE_INPUT);
        Task<ActionResult<IEnumerable<TASK_COMPLIANCE_END_CHECK_LIST>>> PostTaskEndListTableInsertUpdateAsync(TASK_ENDLIST_TABLE_INPUT tASK_ENDLIST_TABLE_INPUT);

        Task<ActionResult<IEnumerable<TASK_COMPLIANCE_END_CHECK_LIST_NT>>> PostTaskEndListTableInsertUpdateNTAsync(TASK_ENDLIST_TABLE_INPUT_NT tASK_ENDLIST_TABLE_INPUT);

        Task<ActionResult<IEnumerable<TaskSanctioningDepartmentOutputList>>> PostTaskSanctioningTableInsertUpdateAsync(TASK_SANCTIONING_INPUT tASK_SANCTIONING_INPUT);

        Task<ActionResult<IEnumerable<TaskSanctioningDepartmentOutputList_NT>>> PostTaskSanctioningTableInsertUpdateNTAsync(TASK_SANCTIONING_TABLE_INPUT_NT tASK_SANCTIONING_INPUT);

        Task<ActionResult<IEnumerable<TaskSanctioningMovmentOutputList>>> PostTaskSanctioningMovmentAsync(TASK_SANCTIONING_MOVMENT tASK_SANCTIONING_INPUT);

        Task<ActionResult<IEnumerable<TaskDashBoardFilterOutputListNT>>> TaskDashBoardFilterAsynNT(Doc_Type_Doc_CategoryInput doc_Type_Doc_CategoryInput);

        Task<ActionResult<IEnumerable<TaskOverduePriorityOutputNT>>> TaskOverDueByPriorityNTAsync(Doc_Type_Doc_CategoryInput doc_Type_Doc_CategoryInput);

        Task<ActionResult<IEnumerable<TaskStatusDistributionOutputNT>>> TaskStatusDistributionNTAsync(Doc_Type_Doc_CategoryInput doc_Type_Doc_CategoryInput);

        Task<ActionResult<IEnumerable<TaskProjectsDashboardOutputNT>>> TaskProjectsDashboardNTAsync(TaskProjectDashboardInput taskProjectDashboardInput);

        Task<ActionResult<IEnumerable<TaskDashBoardFilterOutputListNT>>> TaskProjectsFilterNTAsync(TaskProjectsFilterNT taskProjectsFilterNT);

        Task<ActionResult<IEnumerable<UserProjectBuildingActivityOutputNT>>> UserProjectBuildingActivityAsyncNT(UserProjectBuildingActivityNT userProjectBuildingActivityNT);

        Task<ActionResult<IEnumerable<UserProjectBuildingActivityOutputNT>>> UserProjectBuildingActivityPostAsyncNT(UserProjectBuildingActivityPostNT userProjectBuildingActivityPostNT);

        Task<ActionResult<string>> FileDownload(FileDownloadNT fileDownloadNT);
        Task<ActionResult<string>> FileDownload();
    }
}
