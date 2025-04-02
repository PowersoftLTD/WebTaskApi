using Microsoft.AspNetCore.Mvc;
using TaskManagement.API.Model;

namespace TaskManagement.API.Interfaces
{
    public interface ICompliance
    {
        Task<ActionResult<IEnumerable<ComplianceOutput_LIST>>> GetComplianceAsync(ComplianceGetInput  complianceGetInput);
        Task<ActionResult<IEnumerable<ComplianceOutput_LIST>>> InsertUpdateComplianceAsync(ComplianceInsertUpdateInput complianceInsertUpdateInput);
        Task<ActionResult<IEnumerable<ComplianceOutput_LIST_NT>>> InsertUpdateComplianceNTAsync(ComplianceInsertUpdateInputNT complianceInsertUpdateInput);
    }
}
