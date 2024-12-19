using System.Text.Json.Serialization;

namespace TaskManagement.API.Model
{
    public class Get_Project_DetailsWithSubprojectOutPut_List
    {
        [JsonPropertyName("Status")]
        public string? Status { get; set; }
        [JsonPropertyName("Message ")]
        public string? Message { get; set; }

        public IEnumerable<Get_Project_DetailsWithSubprojectOutPut> Data { get; set; }
    }
    public class Get_Project_DetailsWithSubprojectOutPut
    {
        [JsonPropertyName("TaskDetails")]
        public string TaskDetails { get; set; }
        [JsonPropertyName("Mkey")]
        public int Mkey { get; set; }

    }
}
