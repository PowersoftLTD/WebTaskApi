using System.Text.Json.Serialization;

namespace TaskManagement.API.Model
{
    public class GET_TASK_TREEInput
    {
        [JsonPropertyName("TASK_MKEY")]
        public string TASK_MKEY { get; set; }
        
    }

    public class GET_TASK_TREEInput_NT
    {
        [JsonPropertyName("Task_Mkey")]
        public string TASK_MKEY { get; set; }

        [JsonPropertyName("Session_User_ID")]
        public int Session_User_ID { get; set; }
        [JsonPropertyName("Business_Group_ID")]
        public int Business_Group_ID { get; set; }

    }
}
