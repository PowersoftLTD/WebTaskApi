using System.Text.Json.Serialization;

namespace TaskManagement.API.Model
{
    public class TASK_DETAILS_BY_MKEY_list
    {
        [JsonPropertyName("Status")]
        public string? Status { get; set; }
        [JsonPropertyName("Message")]
        public string? Message { get; set; }
        public IEnumerable<TASK_DETAILS_BY_MKEY> Data { get; set; }
    }


    public class TASK_DETAILS_BY_MKEY
    {
        [JsonPropertyName("MKEY")]
        public string? MKEY { get; set; }
        [JsonPropertyName("TASK_NO")]
        public string TASK_NO { get; set; }
        [JsonPropertyName("TASK_NAME")]
        public string TASK_NAME { get; set; }
        [JsonPropertyName("ISNODE")]
        public char ISNODE { get; set; }
        [JsonPropertyName("TASK_PARENT_ID")]
        public decimal TASK_PARENT_ID { get; set; }
        [JsonPropertyName("TASK_MAIN_NODE_ID")]
        public decimal TASK_MAIN_NODE_ID { get; set; }
        [JsonPropertyName("STATUS")]
        public string STATUS { get; set; }
        [JsonPropertyName("STATUS_PERC")]
        public decimal STATUS_PERC { get; set; }
        [JsonPropertyName("TASK_CREATED_BY")]
        public decimal TASK_CREATED_BY { get; set; }
        [JsonPropertyName("APPROVER_ID")]
        public decimal APPROVER_ID { get; set; }
        [JsonPropertyName("APPROVE_ACTION_DATE")]
        public DateTime APPROVE_ACTION_DATE { get; set; }
        [JsonPropertyName("CAREGORY")]
        public string? CAREGORY { get; set; }
        [JsonPropertyName("PROJECT_MKEY")]
        public string PROJECT_MKEY { get; set; }
        [JsonPropertyName("PROJECT")]
        public string? PROJECT { get; set; }
        [JsonPropertyName("Sub_PROJECT")]
        public string? Sub_PROJECT { get; set; }
        [JsonPropertyName("CATEGORY_MKEY")]
        public int? CATEGORY_MKEY { get; set; }
        [JsonPropertyName("TASK_DESCRIPTION")]
        public string TASK_DESCRIPTION { get; set; }
        [JsonPropertyName("START_DATE")]
        public DateTime? START_DATE { get; set; }
        [JsonPropertyName("COMPLETION_DATE")]
        public DateTime? COMPLETION_DATE { get; set; }
        [JsonPropertyName("CLOSE_DATE")]
        public DateTime? CLOSE_DATE { get; set; }
        [JsonPropertyName("DUE_DATE")]
        public DateTime? DUE_DATE { get; set; }
        [JsonPropertyName("ASSIGNED_TO")]
        public decimal? ASSIGNED_TO { get; set; }
        [JsonPropertyName("TAGS")]
        public string? TAGS { get; set; }
        [JsonPropertyName("CREATION_DATE")]
        public DateTime? CREATION_DATE { get; set; }
        [JsonPropertyName("EMP_FULL_NAME")]
        public string? EMP_FULL_NAME { get; set; }
        [JsonPropertyName("FILE_NAME")]
        public string? FILE_NAME { get; set; }
        [JsonPropertyName("FILE_PATH")]
        public string? FILE_PATH { get; set; }
        [JsonPropertyName("RESPOSIBLE_EMP_MKEY")]
        public string? RESPOSIBLE_EMP_MKEY { get; set; }
        [JsonPropertyName("RESPONSE_STATUS")]
        public string? RESPONSE_STATUS { get; set; }
        [JsonPropertyName("RESPONSE_MESSAGE")]
        public string? RESPONSE_MESSAGE { get; set; }
    }
}
