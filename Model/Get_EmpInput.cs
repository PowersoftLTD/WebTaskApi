using System.Text.Json.Serialization;

namespace TaskManagement.API.Model
{
    public class Get_EmpInput
    {
        [JsonPropertyName("CURRENT_EMP_MKEY")]
        public string CURRENT_EMP_MKEY { get; set; }
        [JsonPropertyName("FILTER")]
        public string FILTER { get; set; }
    }
}
