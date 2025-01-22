using System.Text.Json.Serialization;
using Newtonsoft.Json;

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

    public class ProjectDocDepositoryInput
    {
        [JsonPropertyName("MKEY")]
        public int? MKEY { get; set; }

        [JsonPropertyName("USER_ID")]
        public int? USER_ID { get; set; }
    }


    public class PROJECT_DOC_DEPOSITORY_HDR
    {
        [JsonPropertyName("PROJECT_DOC_FILES")]
        public List<IFormFile>? PROJECT_DOC_FILES { get; set; }

        [JsonPropertyName("MKEY")]
        public int MKEY { get; set; }

        [JsonPropertyName("PROPERTY_MKEY")]
        public int? PROPERTY_MKEY { get; set; }

        [JsonPropertyName("BUILDING_MKEY")]
        public int? BUILDING_MKEY { get; set; }
       
        [JsonPropertyName("DOC_MKEY")]
        public int? DOC_MKEY { get; set; }

        [JsonPropertyName("DOC_NUMBER")]
        public string? DOC_NUMBER { get; set; }

        [JsonPropertyName("DOC_DATE")]
        public string? DOC_DATE { get; set; }

        [JsonPropertyName("VALIDITY_DATE")]
        public string? VALIDITY_DATE { get; set; }

        [JsonPropertyName("CREATED_BY")]
        public int? CREATED_BY { get; set; }

        [JsonPropertyName("DELETE_FLAG")]
        public char? DELETE_FLAG { get; set; }
    }
    public class UpdateProjectDocDepositoryHDRInput
    {
        [JsonPropertyName("MKEY")]
        public int MKEY { get; set; }

        [JsonPropertyName("PROPERTY_MKEY")]
        public int? PROPERTY_MKEY { get; set; }

        [JsonPropertyName("BUILDING_MKEY")]
        public int? BUILDING_MKEY { get; set; }

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

        [JsonPropertyName("PROPERTY_MKEY")]
        public int? PROPERTY_MKEY { get; set; }

        [JsonPropertyName("BUILDING_MKEY")]
        public int? BUILDING_MKEY { get; set; }

        [JsonPropertyName("DOC_MKEY")]
        public int? DOC_MKEY { get; set; }

        [JsonPropertyName("DOC_NUMBER")]
        public string? DOC_NUMBER { get; set; }

        [JsonPropertyName("DOC_DATE")]
        public string? DOC_DATE { get; set; }

        [JsonPropertyName("VALIDITY_DATE")]
        public string? VALIDITY_DATE { get; set; }

        [JsonPropertyName("PROJECT_DOC_FILES")]
        public IEnumerable<DocFileUploadOutPut>? PROJECT_DOC_FILES { get; set; }

        [JsonPropertyName("CREATED_BY_ID")]
        public int? CREATED_BY_ID { get; set; }

        [JsonPropertyName("CREATED_BY_NAME")]
        public string? CREATED_BY_NAME { get; set; }

        [JsonPropertyName("CREATION_DATE")]
        public string? CREATION_DATE { get; set; }

        [JsonPropertyName("LAST_UPDATED_BY")]
        public int? LAST_UPDATED_BY { get; set; }

        [JsonPropertyName("UPDATED_BY_NAME")]
        public string? UPDATED_BY_NAME { get; set; }

        [JsonPropertyName("LAST_UPDATE_DATE")]
        public string? LAST_UPDATE_DATE { get; set; }
    }

    public class DocFileUploadOutPut
    {
        [JsonPropertyName("MKEY")]
        public int MKEY { get; set; }

        [JsonPropertyName("SR_NO")]
        public int SR_NO { get; set; }
        
        [JsonPropertyName("PROJECT_DOC_MKEY")]
        public int PROJECT_DOC_MKEY { get; set; }
        
        [JsonPropertyName("FILE_NAME")]
        public string FILE_NAME { get; set; }
        
        [JsonPropertyName("FILE_PATH")]
        public string FILE_PATH { get; set; }
        
        [JsonPropertyName("CREATED_BY_ID")]
        public string? CREATED_BY_ID { get; set; }

        [JsonPropertyName("CREATED_BY_NAME")]
        public string? CREATED_BY_NAME { get; set; }

        [JsonPropertyName("CREATION_DATE")]
        public string? CREATION_DATE { get; set; }

        [JsonPropertyName("LAST_UPDATED_BY")]
        public string? LAST_UPDATED_BY { get; set; }

        [JsonPropertyName("UPDATED_BY_NAME")]
        public string? UPDATED_BY_NAME { get; set; }

        [JsonPropertyName("LAST_UPDATE_DATE")]
        public string? LAST_UPDATE_DATE { get; set; }
    }
}
