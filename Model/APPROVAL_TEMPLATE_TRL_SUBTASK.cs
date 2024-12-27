using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TaskManagement.API.Model
{
   

    public class APPROVAL_TEMPLATE_TRL_SUBTASK
    {
        public int? HEADER_MKEY { get; set; }
        public string? SEQ_NO { get; set; }
        public string? SUBTASK_ABBR { get; set; }
        public int? SUBTASK_MKEY { get; set; }
        //public string? ATTRIBUTE1 { get; set; }
        //public string? ATTRIBUTE2 { get; set; }
        //public string? ATTRIBUTE3 { get; set; }
        //public string? ATTRIBUTE4 { get; set; }
        //public string? ATTRIBUTE5 { get; set; }
        //public string? ATTRIBUTE6 { get; set; }
        //public string? ATTRIBUTE7 { get; set; }
        //public string? ATTRIBUTE8 { get; set; }
        public int? CREATED_BY { get; set; }
        public char? DELETE_FLAG { get; set; }
        public string? Status { get; set; }
        public string? Message { get; set; }
    }
}
