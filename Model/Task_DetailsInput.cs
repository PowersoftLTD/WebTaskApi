using System.Text.Json.Serialization;

namespace TaskManagement.API.Model
{
    public class Task_DetailsInput
    {
        [JsonPropertyName("CURRENT_EMP_MKEY")]
        public string CURRENT_EMP_MKEY { get; set; }
        [JsonPropertyName("FILTER")]
        public string FILTER { get; set; }
    }
}
