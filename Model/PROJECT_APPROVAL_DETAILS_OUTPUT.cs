using System.Text.Json.Serialization;

namespace TaskManagement.API.Model
{
    public class PROJECT_APPROVAL_DETAILS_OUTPUT
    {
        [JsonPropertyName("HEADER_MKEY")]
        public int HEADER_MKEY { get; set; }
        [JsonPropertyName("TASK_NO")]
        public string TASK_NO { get; set; }  // SEQ_NO IS TASK_NO
        [JsonPropertyName("MAIN_ABBR")]
        public string MAIN_ABBR { get; set; }
        [JsonPropertyName("ABBR_SHORT_DESC")]
        public string ABBR_SHORT_DESC { get; set; }
        [JsonPropertyName("DAYS_REQUIERD")]
        public string DAYS_REQUIERD { get; set; }
        [JsonPropertyName("AUTHORITY_DEPARTMENT")]
        public string AUTHORITY_DEPARTMENT { get; set; }
        [JsonPropertyName("END_RESULT_DOC")]
        public string END_RESULT_DOC { get; set; }

        [JsonPropertyName("JOB_ROLE")]
        public int JOB_ROLE { get; set; }
        [JsonPropertyName("SUBTASK_PARENT_ID")]
        public int SUBTASK_PARENT_ID { get; set; }
        [JsonPropertyName("RESPOSIBLE_EMP_MKEY")]
        public int RESPOSIBLE_EMP_MKEY { get; set; }
        [JsonPropertyName("LONG_DESCRIPTION")]
        public string LONG_DESCRIPTION { get; set; }
        [JsonPropertyName("Status")]
        public string? Status { get; set; }
        [JsonPropertyName("Message")]
        public string? Message { get; set; }

    }
}
