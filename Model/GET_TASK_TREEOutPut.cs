using System.Text.Json.Serialization;

namespace TaskManagement.API.Model
{
    public class GET_TASK_TREEOutPut_List
    {
        [JsonPropertyName("Status")]
        public string? Status { get; set; }
        [JsonPropertyName("Message ")]
        public string? Message { get; set; }

        public IEnumerable<GET_TASK_TREEOutPut> Data { get; set; }
    }
    public class GET_TASK_TREEOutPut
    {

        [JsonPropertyName("MKEY")]
        public int MKEY { get; set; }
        [JsonPropertyName("CATEGORY")]
        public string? CATEGORY { get; set; }

        [JsonPropertyName("TASK_MKEY")]
        public string TASK_MKEY { get; set; }

        [JsonPropertyName("TASK_NO")]
        public string TASK_NO { get; set; }
        [JsonPropertyName("CREATOR")]
        public string? CREATOR { get; set; }

        [JsonPropertyName("StatusVal")]
        public string? StatusVal { get; set; }

        [JsonPropertyName("ACTIONABLE")]
        public string? ACTIONABLE { get; set; }
        [JsonPropertyName("CREATION_DATE")]
        public string CREATION_DATE { get; set; }
        [JsonPropertyName("COMPLETION_DATE")]
        public string? COMPLETION_DATE { get; set; }
        [JsonPropertyName("TASK_NAME")]
        public string? TASK_NAME { get; set; }
        [JsonPropertyName("TASK_DESCRIPTION")]
        public string? TASK_DESCRIPTION { get; set; }
        [JsonPropertyName("TAGS")]
        public string? TAGS { get; set; }
        [JsonPropertyName("STATUS")]
        public string? STATUS { get; set; }
        [JsonPropertyName("RESPONSIBLE_TAG")]
        public string? RESPONSIBLE_TAG { get; set; }

        [JsonPropertyName("ASSIGNEE")]
        public string? ASSIGNEE { get; set; }

        [JsonPropertyName("ASSIGNEE_DEPARTMENT_ID")]
        public int? ASSIGNEE_DEPARTMENT_ID { get; set; }

        [JsonPropertyName("PROJECT_NAME")]
        public string? PROJECT_NAME { get; set; }
        [JsonPropertyName("TASK_HISTORY")]
        public int TASK_HISTORY { get; set; }
        [JsonPropertyName("CURRENT_EMP_MKEY")]
        public string? CURRENT_EMP_MKEY { get; set; }
        [JsonPropertyName("TASKTYPE")]
        public string? TASKTYPE { get; set; }
        [JsonPropertyName("TASKTYPE_DESC")]
        public string? TASKTYPE_DESC { get; set; }
        //[JsonPropertyName("mkey")]
        //public string? mkey { get; set; }
        [JsonPropertyName("unique_id")]
        public int unique_id { get; set; }
        [JsonPropertyName("TASK_PARENT_ID")]
        public int TASK_PARENT_ID { get; set; }
        [JsonPropertyName("assigned_to")]
        public int assigned_to { get; set; }
        [JsonPropertyName("resposible_emp_mkey")]
        public int resposible_emp_mkey { get; set; }
        [JsonPropertyName("status_perc")]
        public decimal status_perc { get; set; }
        [JsonPropertyName("RESPONSIBLE")]
        public string? RESPONSIBLE { get; set; }
        [JsonPropertyName("END_DATE")]
        public string? END_DATE { get; set; }
        [JsonPropertyName("START_DATE")]
        public string START_DATE { get; set; }
        [JsonPropertyName("ACTUAL_COMPLETION_DATE")]
        public string ACTUAL_COMPLETION_DATE { get; set; }
        [JsonPropertyName("RESPONE_STATUS")]
        public string? RESPONE_STATUS { get; set; }
        [JsonPropertyName("RESPONSE_MESSAGE")]
        public string? RESPONSE_MESSAGE { get; set; }
    }
}
