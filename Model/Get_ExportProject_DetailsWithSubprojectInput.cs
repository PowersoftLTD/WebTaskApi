using System.Text.Json.Serialization;

namespace TaskManagement.API.Model
{
    public class Get_ExportProject_DetailsWithSubprojectInput
    {
        [JsonPropertyName("ProjectID")]
        public string ProjectID { get; set; }
        [JsonPropertyName("SubProjectID")]
        public string SubProjectID { get; set; }
    }
}
