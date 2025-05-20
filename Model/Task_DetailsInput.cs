using System.Text.Json.Serialization;

namespace TaskManagement.API.Model
{
    public class Task_DetailsInput
    {
        [JsonPropertyName("CURRENT_EMP_MKEY")]
        public string CURRENT_EMP_MKEY { get; set; }
        [JsonPropertyName("FILTER")]
        public string FILTER { get; set; }
    }

    public class Task_DetailsInputNT
    {
        [JsonPropertyName("Current_Emp_Mkey")]
        public int CURRENT_EMP_MKEY { get; set; }
        [JsonPropertyName("Filter")]
        public string? FILTER { get; set; }
        [JsonPropertyName("Status_Filter")]
        public string? Status_Filter { get; set; }
        [JsonPropertyName("PriorityFilter")]
        public string? PriorityFilter { get; set; }
        [JsonPropertyName("TypeFilter")]
        public string? TypeFilter { get; set; }

        [JsonPropertyName("Session_User_ID")]
        public int Session_User_ID { get; set; }
        [JsonPropertyName("Business_Group_ID")]
        public int Business_Group_ID { get; set; }

    }
}
