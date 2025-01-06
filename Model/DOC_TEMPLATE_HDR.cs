using System.Text.Json.Serialization;

namespace TaskManagement.API.Model
{
    public class DocFileUploadOutput_List
    {
        [JsonPropertyName("Status")]
        public string? Status { get; set; }
        [JsonPropertyName("Message")]
        public string? Message { get; set; }

        public DocFileUploadOutPut Data { get; set; }
    }
    public class DocFileUploadInput
    {
        public IFormFile files { get; set; }
        [JsonPropertyName("MKEY")]
        public int MKEY { get; set; }
    }

    public class DocFileUploadOutPut
    {
        [JsonPropertyName("MKEY")]
        public int MKEY { get; set; }
        [JsonPropertyName("FILE_NAME")]
        public string FILE_NAME { get; set; }
        [JsonPropertyName("FILE_PATH")]
        public string FILE_PATH { get; set; }
    }

    public class DOC_TEMPLATE_HDR
    {
        public int? MKEY { get; set; }
        public int? DOC_CATEGORY { get; set; }
        public string? DOC_NAME { get; set; }
        public string? DOC_ABBR { get; set; }
        public string? DOC_NUM_FIELD_NAME { get; set; }
        public string? DOC_NUM_DATE_NAME { get; set; }
        public char? DOC_NUM_APP_FLAG { get; set; }
        public char? DOC_NUM_VALID_FLAG { get; set; }
        public char? DOC_NUM_DATE_APP_FLAG { get; set; }
        public char? DOC_ATTACH_APP_FLAG { get; set; }
        public int? COMPANY_ID { get; set; }
        public string? ATTRIBUTE1 { get; set; }
        public string? ATTRIBUTE2 { get; set; }
        public int? CREATED_BY { get; set; }
        public int? LAST_UPDATED_BY { get; set; }
        public string? Status { get; set; }
        public string? Message { get; set; }
    }
}
