using Microsoft.AspNetCore.Mvc;
using TaskManagement.API.Model;

namespace TaskManagement.API.Interfaces
{
    public interface IUserDepartmentReport
    {
        Task<ActionResult<IEnumerable<TaskDashBoardFilterOutputListNT>>> TaskDashBoardFilterAsynNT(Doc_Type_Doc_CategoryInput doc_Type_Doc_CategoryInput);
        Task<IEnumerable<Task_DetailsOutPutNT_List>> GetTaskDetailsNTAsync(Task_UserDepartmentDetailsInputNT task_DetailsInputNT);
        Task<IEnumerable<Task_UserDepartmentOutPutNT_List>> GetDepertmentListNTAsyn();
        Task<IEnumerable<Task_userDepartMentResponseOutPUt_NT>> GetEmployeeDetails_ByDepartmentId(userDepartment departmen);
    }
}
