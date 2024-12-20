using System.Text.Json.Serialization;

namespace TaskManagement.API.Model
{
    public class TASK_NESTED_GRIDOutPut_List
    {
        [JsonPropertyName("Status")]
        public string? Status { get; set; }
        [JsonPropertyName("Message")]
        public string? Message { get; set; }

        public IEnumerable<TASK_NESTED_GRIDOutPut> Data { get; set; }
    }

    public class TASK_NESTED_GRIDOutPut
    {
        [JsonPropertyName("mkey")]
        public string MKEY { get; set; }
        [JsonPropertyName("unique_id")]
        public int unique_id { get; set; }
        [JsonPropertyName("TASK_PARENT_ID")]
        public int TASK_PARENT_ID { get; set; }
        [JsonPropertyName("TASK_NO")]
        public string TASK_NO { get; set; }
        [JsonPropertyName("TASK_NAME")]
        public string? TASK_NAME { get; set; }
        [JsonPropertyName("TASK_DESCRIPTION")]
        public string? TASK_DESCRIPTION { get; set; }
        [JsonPropertyName("COMPLETION_DATE")]
        public string? COMPLETION_DATE { get; set; }
        [JsonPropertyName("assigned_to")]
        public int assigned_to { get; set; }
        [JsonPropertyName("resposible_emp_mkey")]
        public int resposible_emp_mkey { get; set; }
        [JsonPropertyName("status_perc")]
        public decimal status_perc { get; set; }
        [JsonPropertyName("STATUS")]
        public string? STATUS { get; set; }
        [JsonPropertyName("RESPONSIBLE_TAG")]
        public string? RESPONSIBLE_TAG { get; set; }

        [JsonPropertyName("ASSIGNEE")]
        public string? ASSIGNEE { get; set; }
        [JsonPropertyName("END_DATE")]
        public string? END_DATE { get; set; }
        [JsonPropertyName("START_DATE")]
        public string? START_DATE { get; set; }
        [JsonPropertyName("ACTUAL_COMPLETION_DATE")]
        public string? ACTUAL_COMPLETION_DATE { get; set; }
        [JsonPropertyName("CREATOR")]
        public string? CREATOR { get; set; }
    }
}
