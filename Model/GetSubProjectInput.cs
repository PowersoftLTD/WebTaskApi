using System.Text.Json.Serialization;

namespace TaskManagement.API.Model
{
    public class GetSubProjectInput
    {
        [JsonPropertyName("Project_Mkey")]
        public string Project_Mkey { get; set; }
    }
}
