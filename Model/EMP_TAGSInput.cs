using System.Text.Json.Serialization;

namespace TaskManagement.API.Model
{
    public class EMP_TAGSInput
    {
        [JsonPropertyName("EMP_TAGS")]
        public string EMP_TAGS { get; set; }
    }
}
