using System.Text.Json.Serialization;

namespace TaskManagement.API.Model
{
    public class TaskDashboardCount
    {
        [JsonPropertyName("Default")]
        public int Default { get; set; }
        [JsonPropertyName("ALLOCATEDBYME")]
        public int ALLOCATEDBYME { get; set; }
        [JsonPropertyName("ALLOCATEDTOME")]
        public int ALLOCATEDTOME { get; set; }
        [JsonPropertyName("COMPLETEDBYME")]
        public int COMPLETEDBYME { get; set; }
        [JsonPropertyName("COMPLETEDFORME")]
        public int COMPLETEDFORME { get; set; }
        [JsonPropertyName("CANCELCLOSE")]
        public int CANCELCLOSE { get; set; }
        [JsonPropertyName("ACTIVE")]
        public int ACTIVE { get; set; }
    }
}
