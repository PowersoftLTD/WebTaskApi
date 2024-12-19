using System.Text.Json.Serialization;

namespace TaskManagement.API.Model
{
    public class TASK_DETAILS_BY_MKEYInput
    {
        [JsonPropertyName("Mkey")]
        public string Mkey { get; set; }
        
    }
}
