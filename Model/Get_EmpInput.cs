using System.Text.Json.Serialization;

namespace TaskManagement.API.Model
{
    public class Get_EmpInput
    {
        [JsonPropertyName("CURRENT_EMP_MKEY")]
        public string CURRENT_EMP_MKEY { get; set; }
        [JsonPropertyName("FILTER")]
        public string FILTER { get; set; }
    }


    public class Get_EmpInput_NT
    {
        [JsonPropertyName("Current_Emp_Mkey")]
        public string CURRENT_EMP_MKEY { get; set; }
        [JsonPropertyName("Filter")]
        public string FILTER { get; set; }
        [JsonPropertyName("Session_User_ID")]
        public int Session_User_ID { get; set; }
        [JsonPropertyName("Business_Group_ID")]
        public int Business_Group_ID { get; set; }
    }
}
