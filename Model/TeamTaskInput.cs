using System.Text.Json.Serialization;

namespace TaskManagement.API.Model
{
    public class TeamTaskInput
    {
        [JsonPropertyName("CURRENT_EMP_MKEY")]
        public string CURRENT_EMP_MKEY { get; set; }
    }
}
