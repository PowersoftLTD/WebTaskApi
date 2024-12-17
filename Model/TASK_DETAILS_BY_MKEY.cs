using System.Text.Json.Serialization;

namespace TaskManagement.API.Model
{
    public class TASK_DETAILS_BY_MKEY
    {
        [JsonPropertyName("MKEY")]
        public string? MKEY { get; set; }
        public string TASK_NO { get; set; }
        public string TASK_NAME { get; set; }
        public char ISNODE { get; set; }
        public decimal TASK_PARENT_ID { get; set; }
        public decimal TASK_MAIN_NODE_ID { get; set; }
        public string STATUS { get; set; }
        public decimal STATUS_PERC { get; set; }
        public decimal TASK_CREATED_BY { get; set; }
        public decimal APPROVER_ID { get; set; }
        public DateTime APPROVE_ACTION_DATE { get; set; }
        public string? CAREGORY { get; set; }
        public string PROJECT_MKEY { get; set; }
        public string? PROJECT { get; set; }
        public string? Sub_PROJECT { get; set; }
        public int? CATEGORY_MKEY { get; set; }
        public string TASK_DESCRIPTION { get; set; }
        public DateTime? START_DATE { get; set; }
        public DateTime? COMPLETION_DATE { get; set; }
        public DateTime? CLOSE_DATE { get; set; }
        public DateTime? DUE_DATE { get; set; }
        public decimal? ASSIGNED_TO { get; set; }
        public string? TAGS { get; set; }
        public DateTime? CREATION_DATE { get; set; }
        public string? EMP_FULL_NAME { get; set; }
        public string? FILE_NAME { get; set; }
        public string? FILE_PATH { get; set; }
        public string? RESPOSIBLE_EMP_MKEY { get; set; }
        public string? RESPONSE_STATUS { get; set; }
        public string? RESPONSE_MESSAGE { get; set; }
    }
}
