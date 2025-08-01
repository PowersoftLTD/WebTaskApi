using Microsoft.AspNetCore.Mvc;
using TaskManagement.API.Model;

namespace TaskManagement.API.Interfaces
{
    public interface IProjectDocDepository
    {
        Task<ActionResult<IEnumerable<UpdateProjectDocDepositoryHDROutput_List>>> GetAllProjectDocDeositoryAsync (ProjectDocDepositoryInput projectDocDepositoryInput);

        Task<ActionResult<IEnumerable<UpdateProjectDocDepositoryHDROutput_List_NT>>> GetAllProjectDocDeositoryAsyncNT(ProjectDocDepositoryInputNT projectDocDepositoryInput);

        Task<ActionResult<IEnumerable<UpdateProjectDocDepositoryHDROutput_List>>> CreateProjectDocDeositoryAsync(PROJECT_DOC_DEPOSITORY_HDR pROJECT_DOC_DEPOSITORY_HDR); //(int? BUILDING_TYPE, int? PROPERTY_TYPE, string? DOC_NAME, string? DOC_NUMBER, string? DOC_DATE, string? DOC_ATTACHMENT,string? VALIDITY_DATE, int? CREATED_BY, string? ATTRIBUTE1, string? ATTRIBUTE2, string? ATTRIBUTE3);

        Task<ActionResult<IEnumerable<UpdateProjectDocDepositoryHDROutput_List_NT>>> CreateProjectDocDeositoryAsyncNT(PROJECT_DOC_DEPOSITORY_HDR_NT pROJECT_DOC_DEPOSITORY_HDR); //(int? BUILDING_TYPE, int? PROPERTY_TYPE, string? DOC_NAME, string? DOC_NUMBER, string? DOC_DATE, string? DOC_ATTACHMENT,string? VALIDITY_DATE, int? CREATED_BY, string? ATTRIBUTE1, string? ATTRIBUTE2, string? ATTRIBUTE3);

        Task<dynamic> GetDocumentDetailsAsync(int? MKEY, string? ATTRIBUTE1, string? ATTRIBUTE2, string? ATTRIBUTE3);
        Task<ActionResult<IEnumerable<ProjectDocOutput_NT>>> GetDocumentDetailsAsyncNT(ProjectDocInput_NT projectDocInput_NT);

        Task<dynamic> GetPROJECT_DEPOSITORY_DOCUMENTAsync(int? BUILDING_TYPE, int? PROPERTY_TYPE, int? DOC_MKEY);
        Task<dynamic> GetProjectDocDeositoryByIDAsync(int? MKEY, string? ATTRIBUTE1, string? ATTRIBUTE2, string? ATTRIBUTE3);
        //Task<ActionResult<IEnumerable<UpdateProjectDocDepositoryHDROutput_List>>> UpdateProjectDepositoryDocumentAsync(UpdateProjectDocDepositoryHDRInput updateProjectDocDepositoryHDRInput);
    }
}
