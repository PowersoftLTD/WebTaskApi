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

    public class Task_DetailsOutPutNT_List
    {
        [JsonPropertyName("Status")]
        public string? Status { get; set; }
        [JsonPropertyName("Message")]
        public string? Message { get; set; }
        [JsonPropertyName("Data")]
        public IEnumerable<Task_DetailsOutPutNT> Data { get; set; }
    }

    public class Task_DetailsOutPutNT
    {
        [JsonPropertyName("Mkey")]
        public string MKEY { get; set; }
        [JsonPropertyName("Task_No")]
        public string TASK_NO { get; set; }
        [JsonPropertyName("Category")]
        public string? CATEGORY { get; set; }
        [JsonPropertyName("Creator")]
        public string? CREATOR { get; set; }
        [JsonPropertyName("Responsible")]
        public string? RESPONSIBLE { get; set; }
        [JsonPropertyName("Dashboard_Status")]
        public string? Dashboard_Status { get; set; }
        [JsonPropertyName("Actionable")]
        public string? ACTIONABLE { get; set; }
        [JsonPropertyName("Creation_Date")]
        public string? CREATION_DATE { get; set; }
        [JsonPropertyName("Completion_Date")]
        public string COMPLETION_DATE { get; set; }
        [JsonPropertyName("Task_Name")]
        public string? TASK_NAME { get; set; }
        [JsonPropertyName("Task_Description")]
        public string? TASK_DESCRIPTION { get; set; }
        [JsonPropertyName("Tags")]
        public string? TAGS { get; set; }
        [JsonPropertyName("Status")]
        public string? STATUS { get; set; }
        [JsonPropertyName("Responsible_Tag")]
        public string? RESPONSIBLE_TAG { get; set; }
        [JsonPropertyName("Project_Name")]
        public string? PROJECT_NAME { get; set; }
        [JsonPropertyName("Priority")]
        public string? Priority { get; set; }
        [JsonPropertyName("Progress_Percentage")]
        public string? Progress_Percentage { get; set; }
        [JsonPropertyName("Subtask_Count")]
        public string? Subtask_Count { get; set; }
    }

}
