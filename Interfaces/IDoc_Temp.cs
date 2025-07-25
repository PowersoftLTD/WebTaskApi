using Microsoft.AspNetCore.Mvc;
using TaskManagement.API.Model;

namespace TaskManagement.API.Interfaces
{
    public interface IDoc_Temp
    {
        Task<IEnumerable<DOC_TEMPLATE_HDR>> GetAllDocumentTempAsync(int LoggedIN);
        Task<DOC_TEMPLATE_HDR> GetDocumentTempByIdAsync(int id, int? LoggedIN);
        Task<ActionResult<IEnumerable<DOC_TEMPLATE_HDR_OUTPUT_NT>>> GetDocumentTempByIdAsyncNT(DocTemplateGetInputNT docTemplateGetInputNT);
        Task<DOC_TEMPLATE_HDR> CreateDocumentTemplateAsync(DOC_TEMPLATE_HDR dOC_TEMPLATE_HDR);

        Task<ActionResult<IEnumerable<DOC_TEMPLATE_HDR_OUTPUT_NT>>> InsertUpdateDocTemplateAsyncNT (DOC_TEMPLATE_HDR_NT_INPUT dOC_TEMPLATE_HDR_NT_INPUT);

        Task<bool> UpdateDocumentTemplateAsync(DOC_TEMPLATE_HDR dOC_TEMPLATE_HDR);
        Task<bool> DeleteDocumentTemplateAsync(int id, int LastUpatedBy);
        Task<DocCategoryOutPut_List> InsertDocumentCategory(DocCategoryInput docCategoryInput);

        Task<ActionResult<IEnumerable<DocCategoryOutPutNT>>> DocTypeAsynNT(DocTypeInputNT docTypeInputNT);

        Task<DocCategoryOutPut_List> InsertInstructionAsyn(InsertInstructionInput insertInstructionInput);

        Task<ActionResult<IEnumerable<DocCategoryOutPutNT>>> InsertInstructionAsynNT(InsertInstructionInputNT insertInstructionInputNT);

        Task<DocCategoryOutPut_List> UpdateDocumentCategory(DocCategoryUpdateInput docCategoryUpdateInput);
        Task<DocCategoryOutPut_List> UpdateInstruction(UpdateInstructionInput updateInstructionInput);
    }
}
