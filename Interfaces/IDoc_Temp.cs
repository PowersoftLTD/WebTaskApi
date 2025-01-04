using TaskManagement.API.Model;

namespace TaskManagement.API.Interfaces
{
    public interface IDoc_Temp
    {
        Task<IEnumerable<DOC_TEMPLATE_HDR>> GetAllDocumentTempAsync(int LoggedIN);
        Task<DOC_TEMPLATE_HDR> GetDocumentTempByIdAsync(int id, int? LoggedIN);
        Task<DOC_TEMPLATE_HDR> CreateDocumentTemplateAsync(DOC_TEMPLATE_HDR dOC_TEMPLATE_HDR);
        Task<bool> UpdateDocumentTemplateAsync(DOC_TEMPLATE_HDR dOC_TEMPLATE_HDR);
        Task<bool> DeleteDocumentTemplateAsync(int id, int LastUpatedBy);
        Task<DocCategoryOutPut_List> InsertDocumentCategory(DocCategoryInput docCategoryInput);
        Task<DocCategoryOutPut_List> UpdateDocumentCategory(DocCategoryUpdateInput docCategoryUpdateInput);
    }
}
