using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TaskManagement.API.Model
{
    public class APPROVAL_TEMPLATE_TRL_SANCTIONING_DEPARTMENT
    {
        [JsonPropertyName("MKEY")]
        public int MKEY { get; set; }
        [JsonPropertyName("SR_NO")]
        public int SR_NO { get; set; }
        [JsonPropertyName("LEVEL")]
        public string LEVEL { get; set; }
        [JsonPropertyName("SANCTIONING_DEPARTMENT")]
        public string SANCTIONING_DEPARTMENT { get; set; }
        [JsonPropertyName("SANCTIONING_AUTHORITY")]
        public string SANCTIONING_AUTHORITY { get; set; }
        [JsonPropertyName("START_DATE")]
        public DateTime START_DATE { get; set; }
        [JsonPropertyName("END_DATE")]
        public DateTime END_DATE { get; set; }
        [JsonPropertyName("CREATED_BY")]
        public int CREATED_BY { get; set; }
        [JsonPropertyName("CREATION_DATE")]
        public int CREATION_DATE { get; set; }
    }


    public class APPROVAL_TEMPLATE_TRL_SUBTASK
    {
        public int? HEADER_MKEY { get; set; }
        public string? SEQ_NO { get; set; }
        public string? SUBTASK_ABBR { get; set; }
        public int? SUBTASK_MKEY { get; set; }
        public string? ATTRIBUTE1 { get; set; }
        public string? ATTRIBUTE2 { get; set; }
        public string? ATTRIBUTE3 { get; set; }
        public string? ATTRIBUTE4 { get; set; }
        public string? ATTRIBUTE5 { get; set; }
        public string? ATTRIBUTE6 { get; set; }
        public string? ATTRIBUTE7 { get; set; }
        public string? ATTRIBUTE8 { get; set; }
        public int? CREATED_BY { get; set; }
        public char? DELETE_FLAG { get; set; }
        public string? Status { get; set; }
        public string? Message { get; set; }
    }
}
