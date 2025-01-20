using Microsoft.AspNetCore.Mvc;
using TaskManagement.API.Model;

namespace TaskManagement.API.Interfaces
{
    public interface IViewClassification
    {
        Task<IEnumerable<V_Building_Classification>> GetViewBuildingClassificationAsync();
        Task<IEnumerable<V_Building_Classification>> GetViewDoc_TypeAsync();
        Task<IEnumerable<V_Building_Classification>> GetViewDoc_Type_CheckListAsync();
        Task<IEnumerable<V_Instruction>> GetAllInstruction();
        Task<IEnumerable<V_Building_Classification>> GetViewStandard_TypeAsync();
        Task<IEnumerable<V_Building_Classification>> GetViewStatutory_AuthAsync();
        Task<IEnumerable<V_Building_Classification>> GetViewJOB_ROLEAsync();
        Task<IEnumerable<V_Building_Classification>> GetViewDepartment();
        Task<IEnumerable<V_Building_Classification>> GetViewSanctioningAuthority();
        Task<IEnumerable<V_Building_Classification>> GetViewDocument_Category();
        Task<IEnumerable<V_Building_Classification>> GetViewResponsibleDepartment();
        Task<IEnumerable<RAISED_AT_OUTPUT_LIST>> GetRaiseATAsync(RAISED_AT_INPUT rAISED_AT_INPUT);
        Task<IEnumerable<RAISED_AT_OUTPUT_LIST>> GetRaiseATBeforeAsync(RAISED_AT_INPUT rAISED_AT_INPUT);
        Task<ActionResult<IEnumerable<COMPLIANCE_STATUS_OUTPUT_LIST>>> GetComplianceStatusAsync();
        Task<ActionResult<IEnumerable<GetTaskTypeList>>> GetTaskTypeAsync(); 
    }
}
