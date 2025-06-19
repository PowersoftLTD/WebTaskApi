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

    public class TaskProjectsFilterNT
    {
        [JsonPropertyName("TaskType")]
        public string TaskType { get; set; }
        [JsonPropertyName("Session_User_Id")]
        public int Session_User_Id { get; set; }
        [JsonPropertyName("Business_Group_Id")]
        public int Business_Group_Id { get; set; }
    }

    public class TaskProjectDashboardInput
    {
        [JsonPropertyName("ProjectMkey")]
        public string? ProjectMkey { get; set; }
        [JsonPropertyName("BuildingMkey")]
        public string? BuildingMkey { get; set; }

        [JsonPropertyName("Task_Type")]
        public string? TASK_TYPE { get; set; }
        [JsonPropertyName("Filter")]
        public string? FILTER { get; set; }

        // Approval Filter
        [JsonPropertyName("Building_Type")]
        public string? Building_Type { get; set; }
        [JsonPropertyName("Building_Standard")]
        public string? Building_Standard { get; set; }
        [JsonPropertyName("Statutory_Authority")]
        public string? Statutory_Authority { get; set; }

        // Compliance Filter
        [JsonPropertyName("ResponsibleDepartment")]
        public string? ResponsibleDepart { get; set; }
        [JsonPropertyName("JobRole")]
        public string? JobRole { get; set; }
        [JsonPropertyName("ResponsiblePerson")]
        public string? ResponsiblePerson { get; set; }
        [JsonPropertyName("RaisedAt")]
        public string? RaisedAt { get; set; }
        [JsonPropertyName("RaisedAtBefore")]
        public string? RaisedAtBefore { get; set; }
        [JsonPropertyName("Status")]
        public string? Status { get; set; }

        [JsonPropertyName("Session_User_Id")]
        public int Session_User_Id { get; set; }
        [JsonPropertyName("Business_Group_Id")]
        public int Business_Group_Id { get; set; }
    }
}
