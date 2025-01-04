using System.Text.Json.Serialization;

namespace TaskManagement.API.Model
{
    public class DocCategoryOutPut_List
    {
        [JsonPropertyName("Status")]
        public string? Status { get; set; }
        [JsonPropertyName("Message")]
        public string? Message { get; set; }

        public IEnumerable<V_Building_Classification> Data { get; set; }
    }
    public class DocCategoryInput
    {
        [JsonPropertyName("DOC_CATEGORY")]
        public string DOC_CATEGORY { get; set; }
        [JsonPropertyName("CREATED_BY")]
        public int CREATED_BY { get; set; }
        [JsonPropertyName("COMPANY_ID")]
        public int COMPANY_ID { get; set; }
        
    }

    public class DocCategoryUpdateInput
    {
        [JsonPropertyName("MKEY")]
        public int MKEY { get; set; }
        [JsonPropertyName("DOC_CATEGORY")]
        public string? DOC_CATEGORY { get; set; }
        [JsonPropertyName("CREATED_BY")]
        public int CREATED_BY { get; set; }
        [JsonPropertyName("DELETE_FLAG")]
        public char DELETE_FLAG { get; set; }
    }

    public class DocCategoryOutPut
    {
        [JsonPropertyName("DOC_CATEGORY")]
        public string DOC_CATEGORY { get; set; }
    }
}
