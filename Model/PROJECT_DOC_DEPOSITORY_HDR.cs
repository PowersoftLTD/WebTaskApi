using System.Text.Json.Serialization;

namespace TaskManagement.API.Model
{
    public class UpdateProjectDocDepositoryHDROutput_List
    {
        [JsonPropertyName("Status")]
        public string? Status { get; set; }
        [JsonPropertyName("Message")]
        public string? Message { get; set; }

        public IEnumerable<UpdateProjectDocDepositoryHDROutput> Data { get; set; }
    }

    public class PROJECT_DOC_DEPOSITORY_HDR
    {
        public int MKEY { get; set; }
        public int? BUILDING_TYPE { get; set; }
        public int? PROPERTY_TYPE { get; set; }
        public int? DOC_NAME { get; set; }
        public string? DOC_NUMBER { get; set; }
        public string? DOC_DATE { get; set; }
        public string? DOC_ATTACHMENT { get; set; }
        public string? VALIDITY_DATE { get; set; }
        public string? ATTRIBUTE1 { get; set; }
        public string? ATTRIBUTE2 { get; set; }
        public string? ATTRIBUTE3 { get; set; }
        public int? CREATED_BY { get; set; }
        public string? Status { get; set; }
        public string? Message { get; set; }
    }
    public class UpdateProjectDocDepositoryHDRInput
    {
        [JsonPropertyName("MKEY")]
        public int MKEY { get; set; }
        [JsonPropertyName("BUILDING_TYPE")]
        public int? BUILDING_TYPE { get; set; }
        [JsonPropertyName("PROPERTY_TYPE")]
        public int? PROPERTY_TYPE { get; set; }
        [JsonPropertyName("DOC_NAME")]
        public int? DOC_NAME { get; set; }
        [JsonPropertyName("DOC_NUMBER")]
        public string? DOC_NUMBER { get; set; }
        [JsonPropertyName("DOC_DATE")]
        public string? DOC_DATE { get; set; }
        [JsonPropertyName("DOC_ATTACHMENT")]
        public string? DOC_ATTACHMENT { get; set; }
        [JsonPropertyName("VALIDITY_DATE")]
        public string? VALIDITY_DATE { get; set; }
        [JsonPropertyName("CREATED_BY")]
        public int? CREATED_BY { get; set; }
        [JsonPropertyName("DELETE_FLAG")]
        public char? DELETE_FLAG { get; set; }
    }
    public class UpdateProjectDocDepositoryHDROutput
    {
        [JsonPropertyName("MKEY")]
        public int MKEY { get; set; }
        [JsonPropertyName("BUILDING_TYPE")]
        public int? BUILDING_TYPE { get; set; }
        [JsonPropertyName("PROPERTY_TYPE")]
        public int? PROPERTY_TYPE { get; set; }
        [JsonPropertyName("DOC_NAME")]
        public int? DOC_NAME { get; set; }
        [JsonPropertyName("DOC_NUMBER")]
        public string? DOC_NUMBER { get; set; }
        [JsonPropertyName("DOC_DATE")]
        public string? DOC_DATE { get; set; }
        [JsonPropertyName("DOC_ATTACHMENT")]
        public string? DOC_ATTACHMENT { get; set; }
        [JsonPropertyName("VALIDITY_DATE")]
        public string? VALIDITY_DATE { get; set; }
        [JsonPropertyName("CREATED_BY")]
        public int? CREATED_BY { get; set; }
        [JsonPropertyName("DELETE_FLAG")]
        public char? DELETE_FLAG { get; set; }
    }
}
