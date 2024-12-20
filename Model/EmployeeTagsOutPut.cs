using System.Text.Json.Serialization;

namespace TaskManagement.API.Model
{
    public class EmployeeTagsOutPut_list
    {
        [JsonPropertyName("Status")]
        public string? Status { get; set; }
        [JsonPropertyName("Message")]
        public string? Message { get; set; }

        public IEnumerable<EmployeeTagsOutPut> Data { get; set; }
    }
    public class EmployeeTagsOutPut
    {
        [JsonPropertyName("name")]
        public string name { get; set; }
    }
}
