using System.Text.Json.Serialization;

namespace TaskManagement.API.Model
{
    public class Add_TaskInput
    {
        [JsonPropertyName("TASK_NO")]
        public string TASK_NO { get; set; }
        [JsonPropertyName("TASK_NAME")]
        public string TASK_NAME { get; set; }
        [JsonPropertyName("TASK_DESCRIPTION")]
        public string TASK_DESCRIPTION { get; set; }
        [JsonPropertyName("CATEGORY")]
        public int CATEGORY { get; set; }
        [JsonPropertyName("PROJECT_ID")]
        public int PROJECT_ID { get; set; }
        [JsonPropertyName("SUBPROJECT_ID")]
        public int SUBPROJECT_ID { get; set; }
        [JsonPropertyName("COMPLETION_DATE")]
        public string COMPLETION_DATE { get; set; }
        [JsonPropertyName("ASSIGNED_TO")]
        public string ASSIGNED_TO { get; set; }
        [JsonPropertyName("TAGS")]
        public string TAGS { get; set; }
        [JsonPropertyName("ISNODE")]
        public string ISNODE { get; set; }
        [JsonPropertyName("START_DATE")]
        public string START_DATE { get; set; }
        [JsonPropertyName("CLOSE_DATE")]
        public string CLOSE_DATE { get; set; }
        [JsonPropertyName("DUE_DATE")]
        public string DUE_DATE { get; set; }
        [JsonPropertyName("TASK_PARENT_ID")]
        public string TASK_PARENT_ID { get; set; }
        [JsonPropertyName("STATUS")]
        public string STATUS { get; set; }
        [JsonPropertyName("STATUS_PERC")]
        public string STATUS_PERC { get; set; }
        [JsonPropertyName("TASK_CREATED_BY")]
        public int TASK_CREATED_BY { get; set; }
        [JsonPropertyName("APPROVER_ID")]
        public int APPROVER_ID { get; set; }
        [JsonPropertyName("IS_ARCHIVE")]
        public string IS_ARCHIVE { get; set; }
        [JsonPropertyName("ATTRIBUTE1")]
        public string ATTRIBUTE1 { get; set; }
        [JsonPropertyName("ATTRIBUTE2")]
        public string ATTRIBUTE2 { get; set; }
        [JsonPropertyName("ATTRIBUTE3")]
        public string ATTRIBUTE3 { get; set; }
        [JsonPropertyName("ATTRIBUTE4")]
        public string ATTRIBUTE4 { get; set; }
        [JsonPropertyName("ATTRIBUTE5")]
        public string ATTRIBUTE5 { get; set; }
        [JsonPropertyName("CREATED_BY")]
        public int CREATED_BY { get; set; }
        [JsonPropertyName("CREATION_DATE")]
        public string CREATION_DATE { get; set; }
        [JsonPropertyName("LAST_UPDATED_BY")]
        public int LAST_UPDATED_BY { get; set; }
        [JsonPropertyName("APPROVE_ACTION_DATE")]
        public string APPROVE_ACTION_DATE { get; set; }
    }
}
