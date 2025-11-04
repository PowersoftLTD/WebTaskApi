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
        Task<IEnumerable<FileUploadAPIOutPut>> TASKFileUpoadAsync(FileUploadAPI fileUploadAPI);

        #region
        // Changes Done By Itemad  Hyder 
        Task<Add_Recursive_TaskOutPut_List_NT> CreateRecursiveTASKAsync(TASK_RECURSIVE_HDR tASK_RECURSIVE_HDR);
        Task<Add_TaskOutPut_List_NT> UpdateRecuriveTASKAsync(TASK_RECURSIVE_HDR tASK_RECURSIVE_HDR);
        Task<IEnumerable<RECURSIVE_TASK_DETAILS_BY_MKEY_list_NT>> GetTaskDetailsByMkeyNTAsync(TASK_DETAILS_BY_MKEYInput_NT tASK_DETAILS_BY_MKEYInput_NT);
        //Task<int> UpdateTASKFileUpoadAsync(string LastUpdatedBy, string taskMkey, string deleteFlag);
        Task<Add_TaskOutPut_List_NT> UpdateTASKFileUpoadAsync(string LastUpdatedBy, string taskMkey, string deleteFlag);
        Task<ActionResult<string>> FileDownload();
        Task<ActionResult<Add_TaskOutPut_List_NT>> TASKFileUpoadNTAsync(int srNo, int taskMkey, int taskParentId, string fileName, string filePath, int createdBy, char deleteFlag, int taskMainNodeId);   //, int taskParentId ,int taskMainNodeId
        #endregion

    }
}
