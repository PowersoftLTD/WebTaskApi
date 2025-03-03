using System.Text.Json.Serialization;

namespace TaskManagement.API.Model
{
    public class EMP_TAGSInput
    {
        [JsonPropertyName("EMP_TAGS")]
        public string EMP_TAGS { get; set; }
    }

    public class EMP_TAGSInput_NT
    {
        [JsonPropertyName("Current_Emp_Mkey")]
        public string EMP_TAGS { get; set; }
        [JsonPropertyName("Session_User_ID")]
        public int Session_User_ID { get; set; }
        [JsonPropertyName("Business_Group_ID")]
        public int Business_Group_ID { get; set; }
    }
}
