using System.Text.Json.Serialization;

namespace TaskManagement.API.Model
{
    public class GET_ACTIONSOutPut_List
    {
        [JsonPropertyName("Status")]
        public string? Status { get; set; }
        [JsonPropertyName("Message")]
        public string? Message { get; set; }
        [JsonPropertyName("Data")]
        public IEnumerable<GET_ACTIONSOutPut> Data { get; set; }
        [JsonPropertyName("Data1")]
        public IEnumerable<GET_ACTIONSOutPut> Data1 { get; set; }
    }
    public class GET_ACTIONSOutPut
    {
        [JsonPropertyName("MKEY")]
        public int MKEY { get; set; }
        [JsonPropertyName("CREATION_DATE")]
        public string? CREATION_DATE { get; set; }
        [JsonPropertyName("PROGRESS_PERC")]
        public decimal? PROGRESS_PERC { get; set; }
        [JsonPropertyName("ACTION_TYPE")]
        public string? ACTION_TYPE { get; set; }
        [JsonPropertyName("COMMENT")]
        public string? COMMENT { get; set; }
        [JsonPropertyName("FILE_NAME")]
        public string? FILE_NAME { get; set; }
        [JsonPropertyName("FILE_PATH")]
        public string? FILE_PATH { get; set; }
        [JsonPropertyName("TASK_MKEY")]
        public int? TASK_MKEY { get; set; }
        [JsonPropertyName("CURRENT_EMP_MKEY")]
        public int? CURRENT_EMP_MKEY { get; set; }
        [JsonPropertyName("CURR_ACTION")]
        public string? CURR_ACTION { get; set; }
        [JsonPropertyName("RESPONSE_STATUS")]
        public string? RESPONSE_STATUS { get; set; }
        [JsonPropertyName("RESPONSE_MESSAGE")]
        public string? RESPONSE_MESSAGE { get; set; }
    }
}
