using System.Text.Json.Serialization;

namespace TaskManagement.API.Model
{
    public class Doc_Type_Doc_CategoryInput
    {
        [JsonPropertyName("Session_User_Id")]
        public int Session_User_Id { get; set; }
        [JsonPropertyName("Business_Group_Id")]
        public int Business_Group_Id { get; set; }
    }

    public class TaskProjectDashboardInput
    {
        [JsonPropertyName("ProjectMkey")]
        public int? ProjectMkey { get; set; }
        [JsonPropertyName("BuildingMkey")]
        public int? BuildingMkey { get; set; }

        [JsonPropertyName("Task_Type")]
        public string? TASK_TYPE { get; set; }
        [JsonPropertyName("Filter")]
        public string? FILTER { get; set; }

        [JsonPropertyName("Session_User_Id")]
        public int Session_User_Id { get; set; }
        [JsonPropertyName("Business_Group_Id")]
        public int Business_Group_Id { get; set; }
    }
}
