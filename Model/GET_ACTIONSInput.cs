using System.Text.Json.Serialization;

namespace TaskManagement.API.Model
{
    public class GET_ACTIONSInput
    {
        [JsonPropertyName("TASK_MKEY")]
        public string TASK_MKEY { get; set; }
        [JsonPropertyName("CURRENT_EMP_MKEY")]
        public string CURRENT_EMP_MKEY { get; set; }
        [JsonPropertyName("CURR_ACTION")]
        public string CURR_ACTION { get; set; }
    }

    public class GET_ACTIONSInput_NT
    {
        [JsonPropertyName("Task_Mkey")]
        public string TASK_MKEY { get; set; }
        [JsonPropertyName("Current_Emp_Mkey")]
        public string CURRENT_EMP_MKEY { get; set; }
        [JsonPropertyName("Curr_Action")]
        public string CURR_ACTION { get; set; }

        [JsonPropertyName("Session_User_ID")]
        public int Session_User_ID { get; set; }
        [JsonPropertyName("Business_Group_ID")]
        public int Business_Group_ID { get; set; }
    }
}
