using System.Text.Json.Serialization;

namespace TaskManagement.API.Model
{
    public class TASK_NESTED_GRIDInput
    {
        [JsonPropertyName("Mkey")]
        public string Mkey { get; set; }
    }
}
