using System.Text.Json.Serialization;

namespace TaskManagement.API.Model
{
    public class BuildingClassInput
    {
        [JsonPropertyName("Type_Code")]
        public string Type_Code { get; set; }
        [JsonPropertyName("Master_mkey")]
        public string Master_mkey { get; set; }
        
    }
}
