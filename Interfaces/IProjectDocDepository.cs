using TaskManagement.API.Model;

namespace TaskManagement.API.Interfaces
{
    public interface IProjectDocDepository
    {
        Task<IEnumerable<dynamic>> GetAllProjectDocDeositoryAsync(string? ATTRIBUTE1, string? ATTRIBUTE2, string? ATTRIBUTE3);
        Task<dynamic> GetProjectDocDeositoryByIDAsync(int? MKEY, string? ATTRIBUTE1, string? ATTRIBUTE2, string? ATTRIBUTE3);
        Task<dynamic> CreateProjectDocDeositoryAsync(int? BUILDING_TYPE, int? PROPERTY_TYPE, string? DOC_NAME, string? DOC_NUMBER, string? DOC_DATE, string? DOC_ATTACHMENT,string? VALIDITY_DATE, int? CREATED_BY, string? ATTRIBUTE1, string? ATTRIBUTE2, string? ATTRIBUTE3);

        Task<dynamic> GetDocumentDetailsAsync(int? MKEY, string? ATTRIBUTE1, string? ATTRIBUTE2, string? ATTRIBUTE3);
    }
}
