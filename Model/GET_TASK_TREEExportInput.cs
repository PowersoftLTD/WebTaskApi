using System.Text.Json.Serialization;

namespace TaskManagement.API.Model
{
    public class GET_TASK_TREEExportInput
    {
        [JsonPropertyName("TASK_MKEY")]
        public string TASK_MKEY { get; set; }
    }
}
