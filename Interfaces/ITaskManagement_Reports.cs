using Microsoft.AspNetCore.Mvc;
using TaskManagement.API.Model;

namespace TaskManagement.API.Interfaces
{
    public interface ITaskManagement_Reports
    {
        Task<ActionResult<IEnumerable<TaskDashBoardFilterOutputListNT>>> TaskDashBoardFilterAsynNT(Doc_Type_Doc_CategoryInput doc_Type_Doc_CategoryInput);
        Task<IEnumerable<Task_DetailsOutPutNT_List>> GetTaskDetailsNTAsync(Task_DetailsInputNT task_DetailsInputNT);
    }
}
