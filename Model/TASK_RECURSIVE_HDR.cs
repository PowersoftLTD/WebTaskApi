
using System.Diagnostics.CodeAnalysis;

namespace TaskManagement.API.Model
{
    public class TASK_RECURSIVE_HDR
    {
        public int MKEY { get; set; }
        public string TASK_NAME { get; set; }
        public string TASK_DESCRIPTION { get; set; }
        public int? CAREGORY { get; set; }
        public decimal? PROJECT_ID { get; set; }
        public decimal? SUB_PROJECT_ID { get; set; }
        public decimal? ASSIGNED_TO { get; set; }
        public string? TAGS { get; set; }
        public string? TERM { get; set; }
        public int? NO_DAYS { get; set; }
        public DateTime START_DATE { get; set; }
        public string? ENDS { get; set; }
        public DateTime? END_DATE { get; set; }
        public decimal? CREATED_BY { get; set; }
        public decimal? LAST_UPDATED_BY { get; set; }
        public string? ATTRIBUTE1 { get; set; }
        public string? ATTRIBUTE2 { get; set; }
        public string? ATTRIBUTE3 { get; set; }
        public string? ATTRIBUTE4 { get; set; }
        public string? ATTRIBUTE5 { get; set; }
        public string? ATTRIBUTE6 { get; set; }
        public string? ATTRIBUTE7 { get; set; }
        
        public string? ATTRIBUTE8 { get; set; }
        
        public string? ATTRIBUTE9 { get; set; }
        
        public string? ATTRIBUTE10 { get; set; }
        
        public string? ATTRIBUTE11 { get; set; }
        
        public string? ATTRIBUTE12 { get; set; }
        public string? ATTRIBUTE13 { get; set; }

        public string? ATTRIBUTE14 { get; set; }

        public string? ATTRIBUTE15 { get; set; }
        public string? ATTRIBUTE16 { get; set; }
        public string? FILE_NAME { get; set; }
        public string? FILE_PATH { get; set; }
        public int? FILE_MKEY { get; set; }
        public int? FILE_SR_NO { get; set; }

        public IFormFile? files { get; set; }
    }
}
