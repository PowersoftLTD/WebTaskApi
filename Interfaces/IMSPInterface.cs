using TaskManagement.API.Model;

namespace TaskManagement.API.Interfaces
{
    public interface IMSPInterface
    {
        Task<IEnumerable<MSPUploadExcelOutPut>> UploadExcel(List<MSPUploadExcelInput> mSPUploadExcelInput);
    }
}
