using System.Text.Json.Serialization;

namespace TaskManagement.API.Model
{
    public class TeamTaskInput
    {
        [JsonPropertyName("CURRENT_EMP_MKEY")]
        public string CURRENT_EMP_MKEY { get; set; }
    }

    public class TeamTaskInputNT
    {
        [JsonPropertyName("Current_Emp_Mkey")]
        public string CURRENT_EMP_MKEY { get; set; }
        [JsonPropertyName("Tasktype")]
        public string? TASKTYPE { get; set; }
        [JsonPropertyName("Tasktype_Desc")]
        public string? TASKTYPE_DESC { get; set; }
        [JsonPropertyName("Filter")]
        public string? Filter { get; set; }
        [JsonPropertyName("Mkey")]
        public string mKEY { get; set; }
        [JsonPropertyName("Session_User_Id")]
        public int Session_User_Id { get; set; }
        [JsonPropertyName("Business_Group_Id")]
        public int Business_Group_Id { get; set; }
    }
}
