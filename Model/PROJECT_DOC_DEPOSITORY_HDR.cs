using System.Text.Json.Serialization;

namespace TaskManagement.API.Model
{
    public class UpdateProjectDocDepositoryHDROutput_List
    {
        [JsonPropertyName("STATUS")]
        public string? STATUS { get; set; }
        [JsonPropertyName("MESSAGE")]
        public string? MESSAGE { get; set; }
        [JsonPropertyName("DATA")]
        public IEnumerable<UpdateProjectDocDepositoryHDROutput> DATA { get; set; }
    }

    public class PROJECT_DOC_DEPOSITORY_HDR
    {
        [JsonPropertyName("PROJECT_DOC_FILES")]
        public List<IFormFile> PROJECT_DOC_FILES { get; set; }
        [JsonPropertyName("MKEY")]
        public int MKEY { get; set; }
        
        [JsonPropertyName("BUILDING_TYPE")]
        public int? BUILDING_TYPE { get; set; }
        [JsonPropertyName("PROPERTY_TYPE")]
        public int? PROPERTY_TYPE { get; set; }
        [JsonPropertyName("DOC_MKEY")]
        public int? DOC_MKEY { get; set; }
        [JsonPropertyName("DOC_NUMBER")]
        public string? DOC_NUMBER { get; set; }
        [JsonPropertyName("DOC_DATE")]
        public string? DOC_DATE { get; set; }
        [JsonPropertyName("DOC_ATTACHMENT")]
        public string? DOC_ATTACHMENT { get; set; }
        [JsonPropertyName("VALIDITY_DATE")]
        public string? VALIDITY_DATE { get; set; }
        [JsonPropertyName("ATTRIBUTE1")]
        public string? ATTRIBUTE1 { get; set; }
        [JsonPropertyName("ATTRIBUTE2")]
        public string? ATTRIBUTE2 { get; set; }
        [JsonPropertyName("ATTRIBUTE3")]
        public string? ATTRIBUTE3 { get; set; }
        [JsonPropertyName("CREATED_BY")]
        public int? CREATED_BY { get; set; }

        [JsonIgnore]
        public string? Status { get; set; }
        [JsonIgnore]
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
        [JsonPropertyName("PROPERTY_TYPE")]
        public int? PROPERTY_TYPE { get; set; }
        [JsonPropertyName("BUILDING_TYPE")]
        public int? BUILDING_TYPE { get; set; }
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
        public string? ResponseStatus { get; set; }
        public string? Message { get; set; }
    }
}
