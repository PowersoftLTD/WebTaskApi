using System.Text.Json.Serialization;

namespace TaskManagement.API.Model
{
    public class FileUploadAPI
    {
        public IFormFile? files { get; set; }
        public string? FILE_NAME { get; set; }
        public string? FILE_PATH { get; set; }
        public int? FILE_MKEY { get; set; }
        public int? FILE_SR_NO { get; set; }
        public int? TASK_MKEY { get; set; }
        public int? CREATED_BY { get; set; }
        public string? ATTRIBUTE14 { get; set; }
        public string? ATTRIBUTE15 { get; set; }
        public string? ATTRIBUTE16 { get; set; }
        public string? Status { get; set; }
        public string? Message { get; set; }
    }

    public class TaskFileUploadAPI
    {
        public IFormFile files { get; set; }
        [JsonPropertyName("FILE_NAME")]
        public string? FILE_NAME { get; set; }
        [JsonPropertyName("FILE_PATH")]
        public string? FILE_PATH { get; set; }
        [JsonPropertyName("TASK_MKEY")]
        public int TASK_MKEY { get; set; }
        [JsonPropertyName("TASK_MAIN_NODE_ID")]
        public int TASK_MAIN_NODE_ID { get; set; }
        [JsonPropertyName("MKEY")]
        public int TASK_PARENT_ID { get; set; }
        [JsonPropertyName("DELETE_FLAG")]
        public char? DELETE_FLAG { get; set; }
        [JsonPropertyName("CREATED_BY")]
        public int CREATED_BY { get; set; }
    }
    public class FileSettings
    {
        public string FilePath { get; set; }
    }
    public class TaskPostActionFileUploadAPI
    {
        public IFormFile? files { get; set; }
        [JsonPropertyName("Mkey")]
        public int Mkey { get; set; }
        [JsonPropertyName("TASK_MKEY")]
        public int TASK_MKEY { get; set; }
        [JsonPropertyName("TASK_PARENT_ID")]
        public int TASK_PARENT_ID { get; set; }
        [JsonPropertyName("FILE_NAME")]
        public string? FILE_NAME { get; set; }
        [JsonPropertyName("FILE_PATH")]
        public string? FILE_PATH { get; set; }
        [JsonPropertyName("ACTION_TYPE")]
        public string ACTION_TYPE { get; set; }
        [JsonPropertyName("DESCRIPTION_COMMENT")]
        public string DESCRIPTION_COMMENT { get; set; }
        [JsonPropertyName("PROGRESS_PERC")]
        public string PROGRESS_PERC { get; set; }
        [JsonPropertyName("STATUS")]
        public string STATUS { get; set; }
        [JsonPropertyName("CREATED_BY")]
        public int CREATED_BY { get; set; }
        [JsonPropertyName("TASK_MAIN_NODE_ID")]
        public int TASK_MAIN_NODE_ID { get; set; }
    }
}
