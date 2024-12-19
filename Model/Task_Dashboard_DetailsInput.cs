using System.Text.Json.Serialization;

namespace TaskManagement.API.Model
{
    public class Task_Dashboard_DetailsInput
    {
        [JsonPropertyName("CURRENT_EMP_MKEY")]
        public string CURRENT_EMP_MKEY { get; set; }
        [JsonPropertyName("CURR_ACTION")]
        public string CURR_ACTION { get; set; }
    }
}
