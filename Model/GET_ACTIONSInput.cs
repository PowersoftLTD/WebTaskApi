using System.Text.Json.Serialization;

namespace TaskManagement.API.Model
{
    public class GET_ACTIONSInput
    {
        [JsonPropertyName("TASK_MKEY")]
        public string TASK_MKEY { get; set; }
        [JsonPropertyName("CURRENT_EMP_MKEY")]
        public string CURRENT_EMP_MKEY { get; set; }
        [JsonPropertyName("CURR_ACTION")]
        public string CURR_ACTION { get; set; }
    }
}
