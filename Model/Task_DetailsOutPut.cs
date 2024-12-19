using System.Text.Json.Serialization;

namespace TaskManagement.API.Model
{
    public class Task_DetailsOutPut_List
    {
        [JsonPropertyName("Status")]
        public string? Status { get; set; }
        [JsonPropertyName("Message")]
        public string? Message { get; set; }
        public IEnumerable<Task_DetailsOutPut> Data { get; set; }
        public IEnumerable<TaskDashboardCount> Data1 { get; set; }
    }

    public class Task_DetailsOutPut
    {
        [JsonPropertyName("MKEY")]
        public string MKEY { get; set; }
        [JsonPropertyName("TASK_NO")]
        public string TASK_NO { get; set; }
        [JsonPropertyName("CATEGORY")]
        public string? CATEGORY { get; set; }
        [JsonPropertyName("CREATOR")]
        public string? CREATOR { get; set; }
        [JsonPropertyName("RESPONSIBLE")]
        public string? RESPONSIBLE { get; set; }
        [JsonPropertyName("ACTIONABLE")]
        public string? ACTIONABLE { get; set; }
        [JsonPropertyName("CREATION_DATE")]
        public string? CREATION_DATE { get; set; }
        [JsonPropertyName("COMPLETION_DATE")]
        public string COMPLETION_DATE { get; set; }
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
        [JsonPropertyName("PROJECT_NAME")]
        public string? PROJECT_NAME { get; set; }
    }
}
